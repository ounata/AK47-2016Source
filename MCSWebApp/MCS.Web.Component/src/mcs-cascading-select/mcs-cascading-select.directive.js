(function () {
    mcs.ng.directive('mcsCascadingSelect', ['$http', 'mcsValidationService', function ($http, validationService) {
        return {
            restrict: 'E',
            scope: {
                level: '@',
                caption: '@',
                data: '=?',
                model: '=?',
                async: '@',
                url: '@',
                path: '@',
                root: '@',
                params: '=?',
                otherParams: '=?',
                parentKey: '@',
                customStyle: '@',
                disabledLevel: '=?',
                callback: '&'
            },
            templateUrl: mcs.app.config.mcsComponentBaseUrl + '/src/tpl/mcs-cascading-select.tpl.html',
            replace: true,
            controller: function ($scope) {
                if (!$scope.caption) {
                    $scope.level = $scope.level || 3;
                    $scope.caption = [];
                    for (var i = 0; i < $scope.level; i++) {
                        $scope.caption.push('请选择');
                    }
                } else {
                    $scope.level = mcs.util.toArray($scope.caption).length;
                }
            },
            link: function ($scope, $elem, $attrs, $ctrl) {
                $scope.path = mcs.util.bool($scope.path || false);
                $scope.root = $scope.root || '0';
                $scope.async = mcs.util.bool($scope.async || true);
                $scope.model = $scope.model || {};
                $scope.params = $scope.params || {};
                $scope.parentKey = $scope.parentKey || 'parentKey';

                var loadData = function (elem, id) {
                    if (!id) return;
                    // 支持异步
                    var $$model = mcs.util.clone($scope.model);
                    if (mcs.util.bool($scope.async)) {
                        if (!$scope.url) return;
                        $http({
                            method: 'post',
                            url: $scope.url,
                            data: $scope.params,
                            cache: true
                        }).then(function (result) {
                            if (!result.data) return;
                            $scope.data = $scope.data || {};
                            $scope.model = $$model;
                            for (var i in result.data) {
                                var item = result.data[i];
                                $scope.data[id] = $scope.data[id] || {};
                                $scope.data[id][item.key] = item.value;
                            }
                            loadDataCallback(elem, $scope.data[id]);
                        });
                    } else {
                        if ($scope.data == undefined || typeof ($scope.data[id]) == undefined)
                            return false;
                        loadDataCallback(elem, $scope.data[id]);
                    }
                };

                var captions = mcs.util.toArray($scope.caption);
                // 添加验证消息
                $scope.$watch('$parent.required', function () {
                    if ($scope.$parent.required) {
                        $elem.attr('required', 'required');
                    }
                    if ($scope.$parent.requiredLevel) {
                        $elem.attr('required-level', $scope.$parent.requiredLevel);
                    }
                    loadWatchDataCallback();
                });

                var loadDataCallback = function (elem, json) {
                    var width = ($elem.width() - ($scope.level * 10)) / $scope.level;
                    // 获取当前下拉框在级联容器中的索引
                    var index = elem.closest('ul').children().index(elem.parent());
                    var selectedValue = '';
                    $elem.find('.select2-container').width(width);
                    if (json) {
                        $.each(json, function (k, v) {
                            var option = '<option value="' + k + '">' + v + '</option>';
                            if (($scope.model && k == $scope.model[index]) ||
                                ($scope.$parent && k == $scope.$parent.model[index])) {
                                selectedValue = k;
                            }
                            elem.append(option);
                        });
                    }
                    // 默认选中
                    elem.select2('val', selectedValue);
                    // 如果选中则加载下一下拉框
                    if (selectedValue) {
                        var next = $elem.find('select').eq(index + 1);
                        var parent = $scope.model[index];
                        if ($scope.path) {
                            switch (index) {
                                case 0:
                                    parent = $scope.root + ',' + parent;
                                    break;
                                case 1:
                                    parent = $scope.root + ',' + $elem.find('select').eq(index - 1).val() + ',' + parent;
                                    break;
                                case 2:
                                    parent = $scope.root + ',' + $elem.find('select').eq(index - 2).val() + ',' + $elem.find('select').eq(index - 1).val() + ',' + parent;
                                    break;
                            }
                        }
                        // 回传当前选中的数据
                        $scope.model['selected'] = $scope.model['selected'] || {};
                        $scope.model['selected'][index] = $scope.model['selected'][index] || {};
                        $scope.model['selected'][index]['key'] = parent;
                        $scope.model['selected'][index]['value'] = $elem.find('select').eq(index).select2('data').text;
                        // 注册回调事件
                        var isLast = (index == captions.length - 1);
                        if (isLast) {
                            $scope.$watch('$scope.model', $scope.callback);
                        }
                        // 扩展其他参数配置
                        if (mcs.util.bool($scope.async)) {
                            $scope.params[$scope.parentKey] = parent;
                            if ($scope.otherParams) {
                                for (var prop in $scope.params) {
                                    if ($scope.otherParams[prop]) {
                                        $scope.params[prop] = $scope.otherParams[prop];
                                    }
                                }
                            }
                        }

                        loadData(next, parent);
                        //next.change();
                    }
                };

                var loadWatchDataCallback = function () {
                    if (mcs.util.hasAttr($elem, 'required') || mcs.util.hasAttr($elem, 'required-level')) {
                        var parent = $elem.parent();
                        if ($scope.$parent.required || $scope.$parent.requiredLevel) {
                            parent = parent.parent();
                        }
                        var validationItem = $elem.closest('.form-group');
                        var message = validationItem.find('.help-block');
                        var validateRow = $elem.closest('.row');
                        var validationItems = validateRow.find('.form-group');
                        if (!message || !message.length) {
                            // 对于单行中只有一个验证项则附加水平消息框
                            if (validationItems.length == 1) {
                                validationItem.append('<div class="help-block horizontal"></div>');
                            } else {
                                parent.append('<div class="help-block"></div>');
                            }
                        }
                    }

                    // 构建option
                    $.each(captions, function (k, v) {
                        var option = '<option value="">' + v + '</option>';
                        var select = $elem.find('select').eq(k);
                        var length = captions.length;
                        var isLast = (k == captions.length - 1);

                        // 返回已选择的数据
                        $scope.model[k] = select.val() || $scope.model[k];

                        if ($scope.customStyle) {
                            select.attr('style', $scope.customStyle);
                        }

                        // 添加必选规则
                        if (mcs.util.hasAttr($elem, 'required') ||
                           (mcs.util.hasAttr($elem, 'required-level') && $elem.attr('required-level') == length)) {
                            select.attr('required', 'required');
                        } else {
                            // 添加部分必选规则
                            if (mcs.util.hasAttr($elem, 'required-level')) {
                                var requiredLevel = parseInt($elem.attr('required-level'));
                                if (requiredLevel > 0 && requiredLevel < length) {
                                    if (k < requiredLevel) {
                                        select.attr('required-level', $elem.attr('required-level'));
                                    }
                                }
                            }
                        }

                        select.append(option).select2().change(function () {
                            // 返回已选择的数据
                            $scope.model[k] = select.val();
                            $scope.model['selected'] = $scope.model['selected'] || {};
                            // 注册回调事件
                            $scope.$watch('$scope.model', $scope.callback);
                            switch (k) {
                                case 0:
                                    $scope.model['selected'].key = select.val();
                                    $scope.model['selected'].value = select.select2('data').text;
                                    break;
                                case 1:
                                    var prev = $elem.find('select').eq(k - 1);
                                    $scope.model['selected'].key = prev.val() + ',' + select.val();
                                    $scope.model['selected'].value = prev.select2('data').text + ',' + select.select2('data').text;
                                    break;
                                case 2:
                                    var prev = $elem.find('select').eq(k - 1);
                                    var last = $elem.find('select').eq(k - 2);
                                    $scope.model['selected'].key = last.val() + ',' + prev.val() + ',' + select.val();
                                    $scope.model['selected'].value = last.select2('data').text + ',' + prev.select2('data').text + ',' + select.select2('data').text;
                                    break;
                            }

                            if (!isLast) {
                                var next = $elem.find('select').eq(k + 1);
                                next.empty().append('<option value="">' + captions[k + 1] + '</option>');
                                var parent = select.val();

                                if ($scope.path) {
                                    switch (k) {
                                        case 0:
                                            parent = $scope.root + ',' + parent;
                                            break;
                                        case 1:
                                            parent = $scope.root + ',' + $elem.find('select').eq(k - 1).val() + ',' + parent;
                                            break;
                                        case 2:
                                            parent = $scope.root + ',' + $elem.find('select').eq(k - 2).val() + ',' + $elem.find('select').eq(k - 1).val() + ',' + parent;
                                            break;
                                    }
                                }
                                // 判断是否为异步且只加载当前项的数据
                                if (mcs.util.bool($scope.async) && select.val()) {
                                    $scope.params[$scope.parentKey] = parent;
                                    if ($scope.otherParams) {
                                        for (var prop in $scope.params) {
                                            if ($scope.otherParams[prop]) {
                                                $scope.params[prop] = $scope.otherParams[prop];
                                            }
                                        }
                                    }
                                }
                                loadData(next, parent);
                                next.change();
                            } else {
                                var validateElem = select;
                                if (mcs.util.hasAttr($elem, 'required-level')) {
                                    validateElem = select.parent().parent().find('select[required-level]:last');
                                }
                                validationService.validate(validateElem, $scope);
                            }
                            // 触发$digest
                            $scope.$apply('$scope.model');
                        });
                    });

                    // 默认加载第一级
                    loadData($elem.find('select').eq(0), $scope.root);
                };
            }
        }
    }]);
})();