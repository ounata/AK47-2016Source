(function () {
    'use strict';

    mcs.ng.directive('mcsLabel', function () {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                required: '@',
                text: '@',
                forInput: '@',
                css: '@'
            },
            template: '<label class="control-label {{css}}" for="{{forInput}}"> {{text}}</label>',
            link: function ($scope, $elem, $attrs) {
                if ($scope.required) {
                    $elem.prepend('<span class="required">*</span>');
                }
                if ($scope.css) {
                    $elem.addClass($scope.css);
                }
            }
        };
    });
})();