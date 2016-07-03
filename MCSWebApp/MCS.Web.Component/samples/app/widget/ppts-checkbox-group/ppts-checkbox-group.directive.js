(function () {
    'use strict';

    angular.module('app.widget').directive('pptsCheckboxGroup', ['$compile', function ($compile) {
        return {
            restrict: 'E',
            scope: {
                category: '@',
                model: '=',
                parent: '=?',
                disabled: '@?',
                async: '@',
                width: '@',
                showAll: '@'
            },
            template: '<span><mcs-checkbox-group data="data" model="model"></mcs-checkbox-group></span>',
            link: function ($scope, $elem) {
                $scope.showAll = mcs.util.bool($scope.showAll || true);
                $scope.async = mcs.util.bool($scope.async || true);
                $scope.disabled = mcs.util.bool($scope.disabled || false);

                function prepareDataDict() {
                    var items = ppts.dict[ppts.config.dictMappingConfig[$scope.category]];
                    $scope.$watchCollection('parent', function () {
                        if ($scope.parent == undefined) {
                            $scope.data = items;
                        } else {
                            $scope.data = [];
                            $scope.model = [];
                            var array = mcs.util.toArray($scope.parent);
                            if (array.length == 1) {
                                var parentKey = array[0];
                                for (var index in items) {
                                    var item = items[index];
                                    if (item.parentKey == parentKey) {
                                        $scope.data.push(item);
                                    }
                                }
                            }
                        }
                        if (mcs.util.bool($scope.showAll)) {
                            if (!$scope.data) $scope.data = [];
                            if (mcs.util.indexOf($scope.data, function (item) {
                                return item.key == -1 && item.value == '全部';
                            }) < 0) {
                                $scope.data.unshift({
                                    key: '-1',
                                    value: '全部'
                                });
                            }
                        }
                        if (!$scope.data.length) {
                            $elem.hide();
                        } else {
                            $elem.show();
                        }
                    });
                    if (mcs.util.hasAttr($elem, 'required')) {
                        $scope.required = true;
                        mcs.util.appendMessage($elem);
                    }
                }

                if ($scope.async) {
                    $scope.$on('dictionaryReady', prepareDataDict);
                } else {
                    prepareDataDict();
                }

                $scope.model = $scope.model || [];
            }
        }
    }]);
})();