(function () {
    'use strict';

    mcs.ng.directive('mcsCheckboxGroup', ['mcsValidationService', function (validationService) {
        return {
            restrict: 'E',
            scope: {
                data: '=',
                model: '='
            },          
            template: '<label class="checkbox-inline" ng-repeat="item in data"><input type="checkbox" class="ace" ng-checked="model && model.indexOf(item.key)!=-1" ng-click="change(item, $event)"><span class="lbl"></span> {{item.value}}</label>',
            controller: function ($scope) {
                $scope.change = function (item, event) {
                    mcs.util.setSelectedItems($scope.model, item, event);
                    validationService.validate($(event.target), $scope);
                };
            },
            link: function ($scope, $elem) {
                $scope.$watch('$parent.required', function () {
                    if ($scope.$parent.required) {
                        $elem.find(':checkbox').attr('required', 'required');
                    }
                })
            }
        }
    }]);

})();