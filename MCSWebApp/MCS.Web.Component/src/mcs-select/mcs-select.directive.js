(function () {
    'use strict';

    mcs.ng.directive('mcsSelect', ['mcsValidationService', function (validationService) {
        return {
            restrict: 'E',
            scope: {
                category: '@',
                css: '@',
                caption: '@',
                filter: '@',
                prop: '@',
                model: '=?',
                data: '=?',
                value: '=?',
                selected: '=?',
                async: '@',
                callback: '&?',
                disabled: '=?',
                parseType: '@',
                showDefault: '@',
                isDynamic: '@', // 数据源是否是动态获取
                ignoreAsync: '@', // 始终加载数据（不管异步还是同步）
                defaultKey: '@',
                customStyle: '@?'
            },
            template: '<div class="col-sm-12 {{css}}"><select style="width:100%;" ng-disabled="disabled" ng-style="customStyle"></select></div>',
            link: function ($scope, $elem) {
                $scope.caption = $scope.caption || '请选择';
                $scope.disabled = mcs.util.bool($scope.disabled || false);
                $scope.showDefault = mcs.util.bool($scope.showDefault || true);
                $scope.isDynamic = mcs.util.bool($scope.isDynamic || false);
                $scope.ignoreAsync = mcs.util.bool($scope.ignoreAsync || false);
                $scope.async = mcs.util.bool($scope.async || true);
                $scope.parseType = $scope.parseType || 'string';
                $scope.defaultKey = $scope.defaultKey || '-1';

                var select = $elem.find('select');
                if (mcs.util.hasAttr($elem, 'required')) {
                    $scope.required = true;
                    select.attr('required', 'required');

                    mcs.util.appendMessage($elem, 'mcs-padding-left-15');
                }
                var prepareDataDict = function () {
                    var items = ppts.dict[ppts.config.dictMappingConfig[$scope.category]];

                    $scope.$watch('filter', function () {
                        if (mcs.util.bool($scope.isDynamic) && ($scope.filter == '' || $scope.filter == undefined)) {
                            $scope.data = $scope.data || [];
                        } else {
                            if ($scope.filter && $scope.prop) {
                                $scope.data = [];
                                for (var index in items) {
                                    var item = items[index];
                                    var prop = $scope.prop;
                                    if (mcs.util.containsElement(item[prop], $scope.filter)) {
                                        $scope.data.push(item);
                                    }
                                }
                            } else {
                                $scope.data = items;
                            }
                        }
                    });
                    $scope.$watch('data', function () {
                        select.empty();
                        if (mcs.util.bool($scope.showDefault)) {
                            select.append('<option value="">' + $scope.caption + '</option>');
                        }
                        $.each($scope.data, function (k, v) {
                            v.parentKey = v.parentKey || '';
                            // 过滤掉全部选项
                            if (v.key != $scope.defaultKey) {
                                var option = '<option value="' + v.key + '" parent="' + v.parentKey + '">' + v.value + '</option>';
                                if (v.key == $scope.model) {
                                    option = '<option value="' + v.key + '" parent="' + v.parentKey + '" selected="selected">' + v.value + '</option>';
                                }
                                select.append(option);
                            }
                        });

                        select.select2().change(function () {
                            // 返回已选择的数据
                            $scope.model = select.val();
                            $scope.value = !select.val() ? '' : select.select2('data').text;
                            $scope.selected = $scope.selected || {};
                            $scope.selected.key = select.val();
                            $scope.selected.value = select.select2('data').text == $scope.caption ? '' : select.select2('data').text;
                            $scope.selected.parentKey = $(select.select2('data').element).attr('parent');
                            // 注册回调事件
                            if ($scope.parseType == 'int') {
                                $scope.model = parseInt($scope.model);
                            }
                            if ($scope.parseType == 'float') {
                                $scope.model = parseFloat($scope.model);
                            }
                            if (angular.isFunction($scope.callback)) {
                                $.each($scope.data, function (k, v) {
                                    if (v.key == $scope.model) {
                                        $scope.selected.selectItem = v.selectItem;
                                        return false;
                                    }
                                });
                                $scope.callback({
                                    item: $scope.selected,
                                    model: $scope.model
                                });
                            }
                            // 执行验证
                            validationService.validate(select, $scope);
                            // 触发$digest
                            $scope.$apply('$scope.model');
                        });
                    });
                };

                // 默认加载当前选择的数据
                $scope.model = $scope.model == undefined ? select.val() : $scope.model;

                if ($scope.customStyle) {
                    select.attr('style', $scope.customStyle);
                }

                if (mcs.util.bool($scope.ignoreAsync)) {
                    prepareDataDict();
                    if ($scope.category) {
                        $scope.$on('dictionaryReady', prepareDataDict);
                    } else {
                        $scope.$on('dataReady', prepareDataDict);
                    }
                } else {
                    if ($scope.async) {
                        if ($scope.category) {
                            $scope.$on('dictionaryReady', prepareDataDict);
                        } else {
                            $scope.$on('dataReady', prepareDataDict);
                        }
                    } else {
                        prepareDataDict();
                    }
                }
            }
        }
    }]);
})();