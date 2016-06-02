(function () {
    'use strict';

    angular.module('app.widget').directive('pptsSelect', ['mcsValidationService', function (validationService) {
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
                showAll: '@',
                customStyle: '@?'
            },
            template: '<div class="col-sm-12 {{css}}"><select style="width:100%;" ng-disabled="disabled" ng-style="customStyle"></select></div>',
            link: function ($scope, $elem) {
                $scope.caption = $scope.caption || '请选择';
                $scope.disabled = mcs.util.bool($scope.disabled || false);
                $scope.showDefault = mcs.util.bool($scope.showDefault || true);
                $scope.showAll = mcs.util.bool($scope.showAll || true);
                $scope.async = mcs.util.bool($scope.async || true);
                $scope.parseType = $scope.parseType || 'string';

                var select = $elem.find('select');
                if (mcs.util.hasAttr($elem, 'required')) {
                    select.attr('required', 'required');
                    $elem.parent().append('<p class="help-block mcs-padding-left-15"></p>');
                }
                var prepareDataDict = function () {
                    var items = ppts.dict[ppts.config.dictMappingConfig[$scope.category]];

                    $scope.$watch('filter', function () {
                        if (!mcs.util.bool($scope.showAll) && $scope.filter == '') {
                            $scope.data = [];
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
                            var option = '<option value="' + v.key + '" parent="' + v.parentKey + '">' + v.value + '</option>';
                            if (v.key == $scope.model) {
                                option = '<option value="' + v.key + '" parent="' + v.parentKey + '" selected="selected">' + v.value + '</option>';
                            }
                            select.append(option);
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
                                $scope.callback({ item: $scope.selected, model: $scope.model });
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
    }]);
})();