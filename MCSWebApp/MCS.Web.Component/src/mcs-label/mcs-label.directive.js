(function () {
    'use strict';

    mcs.ng.directive('mcsLabel', function () {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                required: '@',
                icons: '@',//square,caret-up
                text: '@',
                css: '@',
                prefix: '@?',
                suffix: '@?'
            },
            template: '<label class="control-label {{css}}"> {{value || text}}</label>',
            controller: function ($scope) {
                $scope.required = mcs.util.bool($scope.required || false);
                $scope.prefix = $scope.prefix || '';
                $scope.suffix = $scope.suffix || '：';
                $scope.value = '';
            },
            link: function ($scope, $elem, $attrs) {
                if ($scope.required) {
                    $elem.prepend('<span class="required">*</span>');
                }
                if ($scope.icons) {
                    var icons = mcs.util.toArray($scope.icons);
                    icons.reverse();
                    for (var i in icons) {
                        var icon = icons[i];
                        if (icon == 'caret-up') {
                            $elem.prepend('<i class="ace-icon fa fa-' + icon + ' red"/> ');
                        } else {
                            $elem.prepend('<i class="ace-icon fa fa-' + icon + ' red bigger-70"/> ');
                        }
                    }
                }
                if ($scope.css) {
                    $elem.addClass($scope.css);
                }
                if ($scope.prefix != undefined) {
                    $scope.value += $scope.prefix;
                }
                if ($scope.suffix != undefined) {
                    $scope.$watch('text', function (value) {
                        if (!value) return;
                        $scope.value += value.replace(':', $scope.suffix);
                        if (value.indexOf($scope.suffix) == -1) {
                            $scope.value += $scope.suffix;
                        }
                    });
                }
            }
        };
    });
})();