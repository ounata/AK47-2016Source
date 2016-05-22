﻿(function () {
    'use strict';

    angular.module('app.widget').directive('pptsDatarange', function () {
        return {
            restrict: 'E',
            scope: {
                css: '@',
                min: '=',
                minText: '@',
                max: '=',
                maxText: '@',
                unit: '@'
            },
            templateUrl: 'app/widget/ppts-datarange/datarange.tpl.html',
            link: function ($scope, $elem) {
                $scope.minText = $scope.minText || '起始金额';
                $scope.maxText = $scope.maxText || '截止金额';
                $scope.unit = $scope.unit || '元';
                $scope.$watch('min', function (data) {
                    if (data == undefined) return;
                    var max = parseFloat($scope.max);
                    if (data > max) {
                        $scope.min = max;
                    }
                });
                $scope.$watch('max', function (data) {
                    if (data == undefined) return;
                    var min = parseFloat($scope.min);
                    if (data < min) {
                        $scope.max = min;
                    }
                });
            }
        }
    });

})();