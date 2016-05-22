(function () {
    'use strict';

    angular.module('app.widget').directive('pptsCheckboxGroup', ['$compile', function ($compile) {
        return {
            restrict: 'E',
            scope: {
                category: '@',
                model: '=',
                async: '@',
                clear: '&?'
            },
            template: '<span><mcs-checkbox-group data="data" model="model"></mcs-checkbox-group></span>',
            link: function ($scope, $elem) {
                $scope.async = mcs.util.bool($scope.async || true);
                if ($scope.async) {
                    $scope.$on('dictionaryReady', function () {
                        $scope.data = ppts.dict[ppts.config.dictMappingConfig[$scope.category]];
                    });
                } else {
                    $scope.data = ppts.dict[ppts.config.dictMappingConfig[$scope.category]];
                }

                $scope.model = $scope.model || [];
                if (angular.isFunction($scope.clear)) {
                    $elem.prepend($compile(angular.element('<button class="btn btn-link" ng-click="clear()">清空</button>'))($scope));
                }
            }
        }
    }]);

})();