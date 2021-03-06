﻿(function () {
    'use strict';

    mcs.ng.directive('mcsCheckboxGroup', ['mcsValidationService', function (validationService) {
        return {
            restrict: 'E',
            scope: {
                data: '=',
                model: '=',
                defaultKey: '@'
            },          
            template: '<label class="checkbox-inline" ng-repeat="item in data" ng-class="{\'all\':item.key==-1}" ng-style="customStyle" uib-popover="{{item.value | tooltip:6}}" popover-trigger="mouseenter"><input type="checkbox" class="ace" ng-checked="model && model.indexOf(item.key)!=-1" ng-disabled="disabled" ng-click="change(item, $event)"><span class="lbl"></span> {{item.value | truncate:6}}</label>',
            controller: function ($scope) {
                $scope.defaultKey = $scope.defaultKey || '-1';
                $scope.disabled = mcs.util.bool($scope.$parent.disabled);
                $scope.change = function (item, event) {
                    $scope.model = $scope.model || [];
                    if (item.key == $scope.defaultKey) {
                        var items = $scope.data;
                        $scope.model = [];
                        if (event.target.checked) {
                            for (var index in items) {
                                var item = items[index];
                                $scope.model.push(item.key);
                            }
                        } 
                    } else {
                        var length = !$scope.data ? 0 : $scope.data.length;
                        mcs.util.setSelectedItems($scope.model, item, event, length, $scope.defaultKey);
                    }
                    validationService.validate($(event.target), $scope);
                };
                $scope.customStyle = {};
                if ($scope.$parent.width) {
                    $scope.customStyle['width'] = $scope.$parent.width;
                }
            },
            link: function ($scope, $elem) {
                $scope.$watch('data', function (value) {
                    if (value && $scope.$parent.required) {
                        $elem.find(':checkbox').attr('required', 'required');
                    }
                });
            }
        }
    }]);
})();