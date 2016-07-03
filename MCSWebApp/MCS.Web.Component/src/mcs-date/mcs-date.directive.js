(function() {
    'use strict';

    mcs.ng.directive('datetimeType', function($filter) {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function($scope, $elem, $attrs, $ctrl) {
                //$ctrl.$parsers.push(function(data) {
                //    return new Date(data);
                //});

                $ctrl.$formatters.push(function(data) {
                    return $filter('date')(data, 'yyyy-MM-dd HH:mm:ss');
                });
            }
        }
    });

    mcs.ng.directive('dateType', function($filter) {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function($scope, $elem, $attrs, $ctrl) {
                // $ctrl.$parsers.push(function(data) {
                //     return new Date(data);
                // });

                $ctrl.$formatters.push(function(data) {
                    return $filter('date')(data, 'yyyy-MM-dd');
                });
            }
        }
    });

    mcs.ng.directive('clearMinDate', function() {
        function clearMinDateFormatter(value) {
            if (!value || (value && (value.toLowerCase() == 'invalid date') || value.toLowerCase().match(/0001-01-01/)) || (value && value.getFullYear && value.getFullYear() == 1)) {
                return '';
            }

            return value;
        }

        return {
            restrict: 'A',
            require: 'ngModel',
            link: function($scope, iElm, iAttrs, ngModelController) {
                ngModelController.$formatters.unshift(clearMinDateFormatter);
            }
        };
    });

})();
