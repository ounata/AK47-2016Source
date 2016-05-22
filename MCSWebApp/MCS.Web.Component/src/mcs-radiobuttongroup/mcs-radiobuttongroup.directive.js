(function () {
    'use strict';

    mcs.ng.directive('mcsRadiobuttonGroup', function () {

        return {
            restrict: 'E',
            scope: {
                data: '=',
                model: '=',
                value: '='
            },

            template: '<label class="radio-inline" ng-repeat="item in data" ng-show="item.state"><input name="{{groupName}}" type="radio" class="ace" ng-checked="item.key==model" ng-click="change(item)"><span class="lbl"></span> {{item.value}}</label>',
            controller: function ($scope) {
                $scope.groupName = mcs.util.newGuid();
                $scope.change = function (item) {
                    $scope.model = item.key;
                    $scope.value = item.value;
                };
            },
            link: function ($scope, $elem) {
                $elem.attr('groupName', $scope.groupName);
            }
        }
    });
})();