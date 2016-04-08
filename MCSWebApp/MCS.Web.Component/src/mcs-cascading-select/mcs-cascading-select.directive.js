﻿(function() {
    mcs.ng.directive('mcsLinkList', function($compile) {
        return {
            restrict: 'A',
            scope: {
                data: '=',
                click: '&'

            },
            template: '<button type="button" class="btn btn-link" data-toggle="button"  ng-repeat="item in data" ng-click="click({eventArgs:$event})">{{item.id}}</button>',

            link: function($scope, ele, attrs, ctrl) {


            }

        }
    })

    .directive('mcsCascadingSelect', function($compile) {

        return {
            restrict: 'E',
            scope: {
                division: '=data',
                ngModel: '=',
                required: '@'
            },

            templateUrl: mcs.app.config.componentBaseUrl + '/src/tpl/mcs-cascading-select.tpl.html ',
            replace: true,

            link: function($scope, iElm, iAttrs) {


            }
        };
    });
})();