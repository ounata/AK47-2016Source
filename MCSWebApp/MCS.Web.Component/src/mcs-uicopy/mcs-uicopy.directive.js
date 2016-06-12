(function function_name(argument) {
    'use strict';

    angular.module('mcs.ng.uiCopy', [])

    .controller('uiCopyController', ['$scope', function() {

    }])

    .directive('mcsUicopy', function($compile, $http) {

        return {
            restrict: 'E',
            scope: {
                data: '=',
                itemTemplateUrl: '@'
            },
            template: '<table style="width:100%" class="table-layout"><tr ng-repeat="row in data track by $index"><td class="mcs-padding-0"><div ng-include="itemTemplateUrl"></div></td></tr></table>',

            link: function($scope, iElm, iAttrs, controller) {


            }
        };
    });
})();
