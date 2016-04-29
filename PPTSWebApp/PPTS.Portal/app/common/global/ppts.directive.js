// 功能权限
/*
ppts.ng.directive('pptsRoles', ['utilService', function (util) {
    return {
        restrict: 'A',
        link: function ($scope, $elem, $attrs, $ctrl) {
            if (!$attrs.pptsRoles) return;
            var hasPermisson = false;
            if (!mcs.util.hasAttr($elem, 'permission')) {
                if (util.contains(ppts.user.roles, $attrs.pptsRoles)) {
                    $elem.attr('permission', true);
                    hasPermisson = true;
                }
                // hide or disable the feature when no permisson
                if (!hasPermisson) {
                    if (mcs.util.hasAttr($elem, 'support-disabled')) {
                        $elem.attr('disabled', 'disabled');
                    } else {
                        $elem.addClass('hide');
                    }
                }
            }
        }
    };
}]);

ppts.ng.directive('pptsJobFunctions', ['utilService', function (util) {
    return {
        restrict: 'A',
        link: function ($scope, $elem, $attrs, $ctrl) {
            if (!$attrs.pptsJobFunctions) return;
            var hasPermisson = false;
            if (!mcs.util.hasAttr($elem, 'permission')) {
                if (util.contains(ppts.user.jobFunctions[ppts.user.currentJobId], $attrs.pptsJobFunctions)) {
                    $elem.attr('permission', true);
                    hasPermisson = true;
                }
                // hide or disable the feature when no permisson
                if (!hasPermisson) {
                    if (mcs.util.hasAttr($elem, 'support-disabled')) {
                        $elem.attr('disabled', 'disabled');
                    } else {
                        $elem.addClass('hide');
                    }
                }
            }
        }
    };
}]);

ppts.ng.directive('pptsFunctions', ['utilService', function (util) {
    return {
        restrict: 'A',
        link: function ($scope, $elem, $attrs, $ctrl) {
            if (!$attrs.pptsFunctions) return;
            var hasPermisson = false;
            if (!mcs.util.hasAttr($elem, 'permission')) {
                if (util.contains(ppts.user.functions, $attrs.pptsFunctions)) {
                    $elem.attr('permission', true);
                    hasPermisson = true;
                }
                // hide or disable the feature when no permisson
                if (!hasPermisson) {
                    if (mcs.util.hasAttr($elem, 'support-disabled')) {
                        $elem.attr('disabled', 'disabled');
                    } else {
                        $elem.addClass('hide');
                    }
                }
            }
        }
    };
}]);
*/
ppts.ng.directive('pptsCheckboxGroup', ['$compile', function ($compile) {
    return {
        restrict: 'E',
        scope: {
            category: '@',
            model: '=',
            async: '@', 
            clear: '&?'
        },
        template: '<span><mcs-checkbox-group data="data" model="model"></mcs-checkbox-group></span>',
        link: function ($scope, $elem) {
            $scope.async = mcs.util.bool($scope.async || true);
            if ($scope.async) {
                $scope.$on('dictionaryReady', function () {
                    $scope.data = ppts.dict[ppts.config.dictMappingConfig[$scope.category]];
                });
            } else {
                $scope.data = ppts.dict[ppts.config.dictMappingConfig[$scope.category]];
            }
          
            $scope.model = $scope.model || [];
            if (angular.isFunction($scope.clear)) {
                $elem.prepend($compile(angular.element('<button class="btn btn-link" ng-click="clear()">清空</button>'))($scope));
            }
        }
    }
}]);

ppts.ng.directive('pptsRadiobuttonGroup', ['$compile', function ($compile) {
    return {
        restrict: 'E',
        scope: {
            category: '@',
            showAll: '@',
            model: '=',
            async: '@'
        },
        template: '<mcs-radiobutton-group data="data" model="model"/>',
        link: function ($scope, $elem, $attrs, $ctrl) {
            $scope.showAll = mcs.util.bool($scope.showAll || false);
            $scope.async = mcs.util.bool($scope.async || true);
            if ($scope.async) {
                $scope.$on('dictionaryReady', function () {
                    $scope.data = ppts.dict[ppts.config.dictMappingConfig[$scope.category]];
                });
            } else {
                $scope.data = ppts.dict[ppts.config.dictMappingConfig[$scope.category]];
            }
            $scope.model = $scope.model || '';
            if ($scope.showAll) {
                var groupName = $elem.children().attr('groupname');
                $elem.prepend($compile(angular.element('<label class="radio-inline"><input name="' + groupName + '" type="radio" class="ace" ng-checked="true" ng-click="model=0" checked="checked"><span class="lbl"></span> 全部</label>'))($scope));
            }
        }
    }
}]);

ppts.ng.directive('pptsSelect', function () {
    return {
        restrict: 'E',
        scope: {
            category: '@',
            css: '@',
            style: '@',
            caption: '@',
            model: '=',
            async: '@',
            callback: '&?',
            disabled: '@'
        },
        templateUrl: 'app/common/tpl/ctrls/select.tpl.html',
        link: function ($scope, $elem) {
            $scope.caption = $scope.caption || '请选择';
            $scope.async = mcs.util.bool($scope.async || true);
            if ($scope.async) {
                $scope.$on('dictionaryReady', function () {
                    $scope.data = ppts.dict[ppts.config.dictMappingConfig[$scope.category]];
                });
            } else {
                $scope.data = ppts.dict[ppts.config.dictMappingConfig[$scope.category]];
            }
            $scope.onSelectCallback = function (item, model) {
                $scope.model = model;
                if (angular.isFunction($scope.callback)) {
                    $scope.callback({ item: item, model: model });
                }
            };
        }
    }
});

ppts.ng.directive('pptsDatepicker', function () {
    return {
        restrict: 'E',
        scope: {
            css: '@',
            model: '='
        },
        templateUrl: 'app/common/tpl/ctrls/datepicker.tpl.html',
        link: function ($scope, $elem) {
            var $this = $elem.find('.date-picker');

            $this.datepicker({
                autoclose: true,
                todayHighlight: true,
                format: ppts.config.datePickerFormat,
                language: ppts.config.datePickerLang
            }) //show datepicker when clicking on the icon
			.next().on('click', function () {
			    $(this).prev().focus();
			});
        }
    }
});

ppts.ng.directive('pptsDaterangepicker', function () {
    return {
        restrict: 'E',
        scope: {
            css: '@',
            size: '@',
            startDate: '=',
            endDate: '='
        },
        templateUrl: 'app/common/tpl/ctrls/daterangepicker.tpl.html',
        link: function ($scope, $elem) {
            var $this = $elem.find('.input-daterange');

            $scope.size = $scope.size || 'lg';

            $this.datepicker({
                autoclose: true,
                todayHighlight: true,
                format: ppts.config.datePickerFormat,
                language: ppts.config.datePickerLang
            }).find('.date-picker').next().on('click', function () {
                $(this).prev().focus();
            });
        }
    }
});

ppts.ng.directive('pptsTimepicker', function () {
    return {
        restrict: 'E',
        scope: {
            css: '@',
            step: '@',
            model: '='
        },
        templateUrl: 'app/common/tpl/ctrls/timepicker.tpl.html',
        link: function ($scope, $elem) {
            var $this = $elem.find('.time-picker');

            $this.timepicker({
                minuteStep: $scope.step || 30,
                showSeconds: true,
                showMeridian: false
            }).next().on('click', function () {
                $(this).prev().focus();
            });
        }
    }
});

ppts.ng.directive('pptsDatetimepicker', function () {
    return {
        restrict: 'E',
        scope: {
            css: '@',
            model: '='
        },
        templateUrl: 'app/common/tpl/ctrls/datetimepicker.tpl.html',
        link: function ($scope, $elem) {
            var $this = $elem.find('.date-timepicker');

            $this.datetimepicker({
                format: ppts.config.datetimePickerFormat,
                showSeconds: true,
                language: ppts.config.datePickerLang
            }).next().on('click', function () {
                $(this).prev().focus();
            });
        }
    }
});

ppts.ng.directive('pptsDatarange', function () {
    return {
        restrict: 'E',
        scope: {
            css: '@',
            min: '=',
            minText: '@',
            max: '=',
            maxText: '@'
        },
        templateUrl: 'app/common/tpl/ctrls/datarange.tpl.html',
        link: function ($scope, $elem) {
            var $this = $elem.find('.data-range');

            //$this.datetimepicker({
            //    format: ppts.config.datetimePickerFormat,
            //    showSeconds: true,
            //    language: ppts.config.datePickerLang
            //}).next().on('click', function () {
            //    $(this).prev().focus();
            //});
        }
    }
});

ppts.ng.directive('pptsSearch', function () {
    return {
        restrict: 'E',
        scope: {
            css: '@',
            model: '=',
            placeholder: '@',
            click: '&'
        },
        templateUrl: 'app/common/tpl/ctrls/search.tpl.html'
    }
});

ppts.ng.directive("pptsCompileHtml", ['$parse', '$sce', '$compile', function ($parse, $sce, $compile) {
    return {
        restrict: "A",
        link: function ($scope, $elem, $attrs) {
            var expression = $sce.parseAsHtml($attrs.pptsCompileHtml);
            var getResult = function () {
                return expression($scope);
            };
            $scope.$watch(getResult, function (newValue) {
                var linker = $compile(newValue);
                $elem.append(linker($scope));
            });
        }
    }
}]);