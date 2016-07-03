(function() {
    'use strict';

    mcs.ng.directive('mcsDaterangepicker', ['mcsValidationService', function(validationService) {
        return {
            restrict: 'E',
            scope: {
                width: '@',
                css: '@',
                size: '@',
                mode: '@', //date, datetime
                startDate: '=?',
                endDate: '=?',
                startText: '@',
                endText: '@',
                rangeRestrict: '@?', //开始日期和结束日期的关系限制
                offset: '@?', //偏移量
                minutes: '=?', //计算时间差（仅用于同一天）,
                disabled: '@?', //禁用(默认none,both,before,after) 
                zIndex: '@'
            },
            templateUrl: function($elem, $attrs) {
                return mcs.app.config.mcsComponentBaseUrl + ($attrs['mode'] && $attrs['mode'].toLowerCase() === 'datetime' ?
                    '/src/tpl/datetimerangepicker.tpl.html' : '/src/tpl/daterangepicker.tpl.html');
            },
            controller: function($scope) {
                $scope.dateStyle = {};

                if ($scope.zIndex) {
                    $scope.dateStyle['z-index'] = $scope.zIndex;
                }
                if ($scope.width) {
                    $scope.dateStyle['width'] = $scope.width;
                }
            },
            link: function($scope, $elem) {
                var $this = $elem.find('.date-picker,.date-timepicker');
                var $start = $this.filter('input[name=start]');
                var $end = $this.filter('input[name=end]');
                var offset = parseInt($scope.offset);
                var restrict = parseInt($scope.rangeRestrict);
                $scope.disabled = $scope.disabled || 'none';

                $scope.startText = $scope.startText || ($scope.mode == 'datetime' ? '开始时间' : '开始日期');
                $scope.endText = $scope.endText || ($scope.mode == 'datetime' ? '结束时间' : '结束日期');
                $scope.size = $scope.size || 'lg';
                $scope.mode = $scope.mode || 'date';
                if (mcs.util.hasAttr($elem, 'required')) {
                    $scope.required = true;
                    $elem.find('input[type=text]').attr('required', 'required');

                    mcs.util.appendMessage($elem);
                }

                var config = {
                    autoclose: true,
                    todayBtn: true,
                    todayHighlight: true,
                    language: ppts.config.datePickerLang,
                    startDate: mcs.date.getLeftBoundDatetime(offset >= 0 ? $scope.startDate : $scope.endDate, offset),
                    endDate: mcs.date.getRightBoundDatetime(offset >= 0 ? $scope.startDate : $scope.endDate, offset)
                };

                if ($scope.mode == 'date') {
                    config.minView = 'month'; //选择日期后，不会再跳转去选择时分秒 
                    config.maxView = 'decade';
                    config.format = ppts.config.datePickerFormat;
                } else {
                    config.format = ppts.config.datetimePickerFormat;
                }

                $start.datetimepicker(config).on('changeDate', function(selected) {
                    $end.datetimepicker('setStartDate', new Date(selected.date.valueOf()));

                    if (!isNaN(restrict)) {
                        $end.datetimepicker('setEndDate', mcs.date.getRightBoundDatetime($scope.startDate, restrict));
                    }


                }).on('hide', function() {
                    var _this = $(this);
                    $scope.$apply(function() {
                        $scope.startDate = _this.val() == '' ? null : moment(_this.val()).utc()._d;
                        // 仅针对同一天进行计算
                        if (offset == 0 || restrict == 0) {
                            if ($start.val() && $end.val()) {
                                $scope.minutes = mcs.date.datepart($start.val(), $end.val(), 'm');
                            }
                        }
                    });
                    validationService.validate($elem.find('input[required]'), $scope);
                }).next().on('click', function() {
                    $scope.$apply(function() {

                        $scope.startDate = null;
                        $end.datetimepicker('setStartDate', mcs.date.getLeftBoundDatetime(offset >= 0 ? $scope.startDate : $scope.endDate, offset));
                        $end.datetimepicker('setEndDate', mcs.date.getRightBoundDatetime(offset >= 0 ? $scope.startDate : $scope.endDate, offset));

                    });
                }).next().on('click', function() {
                    $start.focus();
                });


                $end.datetimepicker(config).on('changeDate', function(selected) {
                    $start.datetimepicker('setEndDate', new Date(selected.date.valueOf()));
                    if (!isNaN(restrict)) {
                        $start.datetimepicker('setStartDate', mcs.date.getLeftBoundDatetime($scope.endDate, restrict));
                    }
                }).on('hide', function() {
                    var _this = $(this);
                    $scope.$apply(function() {
                        $scope.endDate = _this.val() == '' ? null : moment(_this.val()).utc()._d;
                        // 仅针对同一天进行计算
                        if (offset == 0 || restrict == 0) {
                            if ($start.val() && $end.val()) {
                                $scope.minutes = mcs.date.datepart($start.val(), $end.val(), 'm');
                            }
                        }
                    });
                    validationService.validate($elem.find('input[required]'), $scope);
                }).next().on('click', function() {
                    $scope.$apply(function() {

                        $scope.endDate = null;

                        $start.datetimepicker('setEndDate', mcs.date.getRightBoundDatetime(offset >= 0 ? $scope.startDate : $scope.endDate, offset));
                        $start.datetimepicker('setStartDate', mcs.date.getLeftBoundDatetime(offset >= 0 ? $scope.startDate : $scope.endDate, offset));
                    });
                }).next().on('click', function() {
                    $end.focus();
                });
            }
        }
    }]);
})();
