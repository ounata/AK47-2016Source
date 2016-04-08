(function () {
    'use strict';

    mcs.ng.directive('mcsCheckboxGroup', function () {
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
                };
            }
        }
    });

})();