(function () {
    'use strict';

    mcs.ng.directive('mcsSelect', function () {

        return {
            restrict: 'E',
            scope: {
                data: '=',
                model: '=',
                caption: '@'
            },

            templateUrl: mcs.app.config.componentBaseUrl + '/src/tpl/mcs-select.tpl.html',
            link: function ($scope, $elem, $attrs, $ctrl) {
                
            }
        }
    });
})();