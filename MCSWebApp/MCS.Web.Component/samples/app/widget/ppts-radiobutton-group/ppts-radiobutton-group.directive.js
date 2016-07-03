(function () {
    'use strict';

    angular.module('app.widget').directive('pptsRadiobuttonGroup', function () {
        return {
            restrict: 'E',
            scope: {
                category: '@',
                showAll: '@',
                model: '=',
                disabled: '@?',
                css: '@',
                async: '@',
                value: '=?',
                parent: '=?'
            },
            template: '<mcs-radiobutton-group data="data" model="model" value="value" class="{{css}}"/>',
            link: function ($scope, $elem, $attrs, $ctrl) {
                $scope.showAll = mcs.util.bool($scope.showAll || false);
                $scope.async = mcs.util.bool($scope.async || true);
                $scope.disabled = mcs.util.bool($scope.disabled || false);

                function prepareDataDict() {
                    $scope.data = ppts.dict[ppts.config.dictMappingConfig[$scope.category]];
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
                    if (mcs.util.hasAttr($elem, 'required')) {
                        $scope.required = true;
                        mcs.util.appendMessage($elem);
                    }
                    $scope.$watch('parent', function () {
                        for (var index in $scope.data) {
                            var item = $scope.data[index];
                            item.state = function () {
                                if (!mcs.util.bool($scope.parent) || $scope.parent == '-1') return true;
                                return !mcs.util.bool(item.parentKey) || mcs.util.containsElement(item.parentKey, $scope.parent);
                            }();
                            if (item.key == '-1') {
                                $scope.model = $scope.model || item.key;
                            }
                        }
                    });
                };

                if ($scope.async) {
                    $scope.$on('dictionaryReady', prepareDataDict);
                } else {
                    prepareDataDict();
                }
                $scope.model = $scope.model || '';
            }
        }
    });
})();