(function () {
    'use strict';

    mcs.ng.directive('mcsInput', ['mcsValidationService', function (validationService) {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                id: '@',
                type: '@',
                placeholder: '@',
                readonly: '@',
                css: '@',
                integer: '@',
                model: '='
            },
            template: '<input placeholder="{{placeholder}}" class="mcs-default-size-input mcs-margin-right-20 {{css}}" ng-model="model" />',
            link: function ($scope, $elem, $attrs) {
                var integer = mcs.util.bool($scope.integer);
                if ($scope.id) {
                    $elem.attr('id', $scope.id);
                }
                if (integer) {
                    $elem.keyup(function () {
                        mcs.util.limit($elem);
                    }).afterpaste(function () {
                        mcs.util.limit($elem);
                    });
                }
                $elem.attr('type', $scope.type || 'text');
                var readonly = mcs.util.bool($scope.readonly);
                if (readonly) {
                    //$elem.attr('readonly', 'readonly');
                    $elem.attr('disabled', 'disabled');
                }
                // 执行验证
                $elem.blur(function () {
                    validationService.validate($elem);
                });
            }
        };
    }]);
})();