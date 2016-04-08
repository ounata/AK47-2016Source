(function () {
    'use strict';

    mcs.ng.constant('inputConfig', {
        types: ['text']
    }).directive('mcsInput', ['inputConfig', 'mcsComponentConfig', function (config, mcsComponentConfig) {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                id: '@',
                type: '@',
                placeholder: '@',
                fixed: '@',
                readonly: '@',
                css: '@',
                model: '=',
            },
            template: '<input placeholder="{{placeholder}}" class="mcs-default-size-input mcs-margin-right-20 {{css}}" ng-model="model" />',
            link: function ($scope, $elem, $attrs) {
                if ($scope.id) {
                    $elem.attr('id', $scope.id);
                }
                $elem.attr('type', $scope.type || 'text');
                if ($scope.fixed) {
                    $elem.addClass('fixed');
                }
                if ($scope.readonly) {
                    $elem.attr('readonly', 'readonly');
                }
            }
        };
    }]);
})();