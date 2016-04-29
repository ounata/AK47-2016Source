(function () {
    'use strict';

    mcs.ng.directive('mcsRadiobuttonGroup', function () {

        return {
            restrict: 'E',
            scope: {
                data: '=',
                model: '='
            },

            template: '<label class="radio-inline" ng-repeat="item in data"><input name="{{groupName}}" type="radio" class="ace" ng-checked="item.key==model" ng-click="change(item)"><span class="lbl"></span> {{item.value}}</label>',
            controller: function ($scope) {
                $scope.groupName = mcs.util.newGuid();
                $scope.change = function (item) {
                    $scope.model = item.key;
                };
            },
            link: function ($scope, $elem) {
                $elem.attr('groupName', $scope.groupName);
            }
        }
    });

    //mcs.ng.directive('mcsRadiobuttonGroup', ['$compile', function ($compile) {

    //    return {
    //        restrict: 'E',
    //        replace: true,
    //        scope: {
    //            data: '=',
    //            model: '='
    //        },
    //        template: '<div></div>',
    //        link: function ($scope, $elem, $attrs, $ctrl) {
    //            if (!$scope.data) return;
    //            $scope.groupName = mcs.util.newGuid();
    //            $scope.data.forEach(function (item, index) {
    //                $elem.append($compile(angular.element('<label class="radio-inline"><input type="radio" name="{{groupName}}" class="ace" ng-model="model" value="' + item.key + '"/><span class="lbl"></span> ' + item.value + '</label>'))($scope));
    //            });
    //        }
    //    }
    //}]);
})();