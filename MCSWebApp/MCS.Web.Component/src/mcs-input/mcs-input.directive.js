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
                customStyle: '@',
                datatype: '@', //int, float
                model: '='
            },
            template: '<input placeholder="{{placeholder}}" class="mcs-default-size-input mcs-margin-right-20 {{css}}" ng-model="model" style="{{customStyle}}"/>',
            link: function ($scope, $elem, $attrs) {
                var dataType = ($scope.datatype || 'string').toLowerCase();
                if ($scope.id) {
                    $elem.attr('id', $scope.id);
                }
                if (dataType == 'int') {
                    $elem.bind('keyup afterpaste', function () {
                        mcs.util.limit($elem);
                    });
                } else if (dataType == 'float') {
                    $elem.bind('keyup afterpaste', function () {
                        mcs.util.number($elem);
                    });
                }
                $elem.attr('type', $scope.type || 'text');
                var readonly = mcs.util.bool($scope.readonly);
                if (readonly) {
                    //$elem.attr('readonly', 'readonly');
                    $elem.attr('disabled', 'disabled');
                }
                // 执行验证
                $scope.$watch('$parent.required', function () {
                    if ($scope.$parent.required) {
                        $elem.attr('required', 'required');
                    }
                    var events = $elem.data('events');
                    if (!events || !events['blur']) {
                        $elem.blur(function () {
                            validationService.validate($elem, $scope.$parent);
                        });
                    }
                });
            }
        };
    }]);
})();