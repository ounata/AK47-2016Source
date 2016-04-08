ppts.ng.directive('pptsCheckboxGroup', ['$compile', function ($compile) {
    return {
        restrict: 'E',
        scope: {
            category: '@',
            model: '=',
            clear: '&?'
        },
        template: '<span><mcs-checkbox-group data="data" model="model"></mcs-checkbox-group></span>',
        link: function ($scope, $elem) {
            $scope.$on('dictionaryReady', function () {
                $scope.data = ppts.dict[ppts.config.dictMappingConfig[$scope.category]];
            });
            $scope.model = $scope.model || [];
            if (angular.isFunction($scope.clear)) {
                $elem.prepend($compile(angular.element('<button class="btn btn-link" ng-click="clear()">清空</button>'))($scope));
            }
        }
    }
}]);

ppts.ng.directive('pptsRadiobuttonGroup', function () {
    return {
        restrict: 'E',
        scope: {
            category: '@',
            model: '='
        },
        template: '<mcs-radiobutton-group data="data" model="model"/>',
        link: function ($scope, $elem) {
            $scope.$on('dictionaryReady', function () {
                $scope.data = ppts.dict[ppts.config.dictMappingConfig[$scope.category]];
            });
            $scope.model = $scope.model || '';
        }
    }
});

ppts.ng.directive('pptsSelect', function () {
    return {
        restrict: 'E',
        scope: {
            category: '@',
            css: '@',
            style: '@',
            caption: '@',
            model: '=',
            disabled: '@'
        },
        templateUrl: ppts.config.webportalBaseUrl + 'app/common/tpl/ctrls/select.tpl.html',
        link: function ($scope, $elem) {
            $scope.caption = $scope.caption || '请选择';
            $scope.$on('dictionaryReady', function () {
                $scope.data = ppts.dict[ppts.config.dictMappingConfig[$scope.category]];
            });
            $scope.onSelectCallback = function (item, model) {
                $scope.model = model;
            };
        }
    }
});