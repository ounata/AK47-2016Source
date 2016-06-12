(function () {
    'use strict';

    mcs.ng.directive('mcsRadiobuttonGroup', ['mcsValidationService', function (validationService) {

        return {
            restrict: 'E',
            scope: {
                data: '=',
                model: '=',
                value: '='
            },

            template: '<label class="radio-inline" ng-repeat="item in data" ng-show="item.state"><span uib-tooltip="{{item.tooltip}}"><input name="{{groupName}}" type="radio" class="ace" ng-checked="item.key==model" ng-click="change(item, $event)"><span class="lbl"></span> {{item.value}}</span></label>',
            controller: function ($scope) {
                $scope.groupName = mcs.util.newGuid();
                $scope.change = function (item, event) {
                    $scope.model = item.key;
                    $scope.value = item.value;
                    validationService.validate($(event.target), $scope);
                };
            },
            link: function ($scope, $elem) {
                $elem.attr('groupName', $scope.groupName);
                $scope.$watch('$parent.required', function () {
                    if ($scope.$parent.required) {
                        $elem.find(':radio').attr('required', 'required');
                    }
                });
            }
        }
    }]);
})();