(function () {
    mcs.ng.directive('mcsCascadingSelect', ['$http', function ($http) {
        return {
            restrict: 'E',
            scope: {
                level: '@',
                caption: '@',
                data: '=?',
                model: '=',
                async: '@',
                url: '@',
                path: '@',
                root: '@',
                params: '=',
                otherParams: '=?',
                parentKey: '@',
                customStyle: '@?',
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

                $scope.percentage = '';
                switch ($scope.level) {
                    case 1:
                        $scope.portion = 12;
                        break;
                    case 2:
                        $scope.portion = 6;
                        $scope.percentage = 'mcs-select-width-50';
                        break;
                    case 3:
                    default:
                        $scope.portion = 4;
                        $scope.percentage = 'mcs-select-width-40';
                        break;
                    case 4:
                        $scope.portion = 3;
                        $scope.percentage = 'mcs-select-width-30';
                        break;
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
                            for (var i in result.data) {
                                var item = result.data[i];
                                $scope.data[id] = $scope.data[id] || {};
                                $scope.data[id][item.key] = item.value;
                            }
                            loadDataCallback(elem, $scope.data[id]);
                        });
                    } else {
                        if (typeof ($scope.data[id]) == "undefined")
                            return false;
                        loadDataCallback(elem, $scope.data[id]);
                    }
                    elem.select2("val", "");
                };

                var loadDataCallback = function (elem, json, selected_id) {
                    if (json) {
                        var index = 1;
                        var selected_index = 0;
                        $.each(json, function (k, v) {
                            var option = '<option value="' + k + '">' + v + '</option>';
                            elem.append(option);

                            if (k == selected_id) {
                                selected_index = index;
                            }

                            index++;
                        })
                        //el.attr('selectedIndex' , selected_index); 
                    }
                }

                var captions = mcs.util.toArray($scope.caption);
                // 构建option
                $.each(captions, function (k, v) {
                    var option = '<option value="">' + v + '</option>';
                    var select = $elem.find('select').eq(k);
                    var length = captions.length;
                    var last = (k == captions.length - 1);

                    // 返回已选择的数据
                    $scope.model[k] = select.val();

                    if ($scope.customStyle) {
                        select.attr('style', $scope.customStyle);
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

                        if (!last) {
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
                                loadData(next, parent);
                            } else {
                                loadData(next, parent);
                            }
                            next.change();
                        }
                        // 触发$digest
                        $scope.$apply('$scope.model');
                    });
                });
                // 默认加载第一级
                loadData($elem.find('select').eq(0), $scope.root);
            }
        }
    }]);
})();