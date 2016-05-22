(function () {
    'use strict';

    angular.module('app.widget').directive('pptsSelect', function () {
        return {
            restrict: 'E',
            scope: {
                category: '@',
                css: '@',
                caption: '@',
                filter: '@',
                prop: '@',
                model: '=',
                selected: '=?',
                async: '@',
                callback: '&?',
                disabled: '@',
                parseType: '@'
            },
            template: '<div class="col-sm-12 {{css}}"><select style="width:100%;"></select></div>',
            link: function ($scope, $elem) {
                var select = $elem.find('select');
                var prepareDataDict = function () {
                    $scope.data = [];
                    var items = ppts.dict[ppts.config.dictMappingConfig[$scope.category]];

                    if ($scope.filter) {
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
                    } else {
                        $scope.data = items;
                    }

                    $.each($scope.data, function (k, v) {
                        var option = '<option value="' + v.key + '">' + v.value + '</option>';
                        select.append(option);
                    })
                };

                $scope.caption = $scope.caption || '请选择';
                $scope.async = mcs.util.bool($scope.async || true);
                $scope.parseType = $scope.parseType || 'string';

                // 构建option
                var option = '<option value="">' + $scope.caption + '</option>';
                // 返回已选择的数据
                $scope.model = select.val();
                if ($scope.customStyle) {
                    select.attr('style', $scope.customStyle);
                }

                select.append(option).select2().change(function () {
                    // 返回已选择的数据
                    $scope.model = select.val();
                    $scope.selected = $scope.selected || {};
                    $scope.selected.key = select.val();
                    $scope.selected.value = select.select2('data').text;
                    // 注册回调事件
                    if ($scope.parseType == 'int') {
                        $scope.model = parseInt($scope.model);
                    }
                    if ($scope.parseType == 'float') {
                        $scope.model = parseFloat($scope.model);
                    }
                    if (angular.isFunction($scope.callback)) {
                        $scope.callback({ item: $scope.selected, model: $scope.model });
                    }
                    // 触发$digest
                    $scope.$apply('$scope.model');
                });

                if ($scope.async) {
                    $scope.$on('dictionaryReady', prepareDataDict);
                } else {
                    prepareDataDict();
                }
            }
        }
    });
})();