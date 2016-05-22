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
            async: '@',
            value: '=?',
            parent: '=?'
        },
        template: '<mcs-radiobutton-group data="data" model="model" value="value"/>',
        link: function ($scope, $elem, $attrs, $ctrl) {
            function prepareDataDict() {
                $scope.data = ppts.dict[ppts.config.dictMappingConfig[$scope.category]];
                if ($scope.showAll) {
                    $scope.data.unshift({ key: '-1', value: '全部' });
                }
                $scope.$watch('parent', function () {
                    for (var index in $scope.data) {
                        var item = $scope.data[index];
                        item.state = function () {
                            if (!mcs.util.bool($scope.parent) || $scope.parent == '-1') return true;
                            return !mcs.util.bool(item.parentKey) || mcs.util.containsElement(item.parentKey, $scope.parent);
                        }();
                        if (item.key == '-1') {
                            $scope.model = item.key;
                        }
                    }
                });
            };
            $scope.showAll = mcs.util.bool($scope.showAll || false);
            $scope.async = mcs.util.bool($scope.async || true);
            if ($scope.async) {
                $scope.$on('dictionaryReady', prepareDataDict);
            } else {
                prepareDataDict();
            }
            $scope.model = $scope.model || '';
        }
    }
}]);

ppts.ng.directive('pptsSelect', function () {
    return {
        restrict: 'E',
        scope: {
            category: '@',
            css: '@',
            caption: '@',
            filter: '@',
            prop: '@',
            model: '=',
            data: '=?',
            selected: '=?',
            async: '@',
            callback: '&?',
            disabled: '=?',
            parseType: '@',
            showDefault: '@',
            customStyle: '@?'
        },
        template: '<div class="col-sm-12 {{css}}"><select style="width:100%;" ng-disabled="disabled" ng-style="customStyle"></select></div>',
        link: function ($scope, $elem) {
            $scope.caption = $scope.caption || '请选择';
            $scope.disabled = mcs.util.bool($scope.disabled || false);
            $scope.showDefault = mcs.util.bool($scope.showDefault || true);
            $scope.async = mcs.util.bool($scope.async || true);
            $scope.parseType = $scope.parseType || 'string';

            var select = $elem.find('select');
            var prepareDataDict = function () {
                var items = ppts.dict[ppts.config.dictMappingConfig[$scope.category]];

                $scope.$watch('filter', function () {
                    if ($scope.filter && $scope.prop) {
                        $scope.data = [];
                        for (var index in items) {
                            var item = items[index];
                            var prop = $scope.prop;
                            if (mcs.util.containsElement(item[prop], $scope.filter)) {
                                $scope.data.push(item);
                            }
                        }
                    } else {
                        $scope.data = items;
                    }
                });
                $scope.$watch('data', function () {
                    select.empty();
                    if (mcs.util.bool($scope.showDefault)) {
                        select.append('<option value="">' + $scope.caption + '</option>');
                    }
                    $.each($scope.data, function (k, v) {
                        var option = '<option value="' + v.key + '" parent="' + v.parentKey + '">' + v.value + '</option>';
                        if (v.key == $scope.model) {
                            option = '<option value="' + v.key + '" parent="' + v.parentKey + '" selected="selected">' + v.value + '</option>';
                        }
                        select.append(option);
                    });

                    select.select2().change(function () {
                        // 返回已选择的数据
                        $scope.model = select.val();
                        $scope.selected = $scope.selected || {};
                        $scope.selected.key = select.val();
                        $scope.selected.value = select.select2('data').text;
                        $scope.selected.parentKey = $(select.select2('data').element).attr('parent');
                        // 注册回调事件
                        if ($scope.parseType == 'int') {
                            $scope.model = parseInt($scope.model);
                        }
                        if ($scope.parseType == 'float') {
                            $scope.model = parseFloat($scope.model);
                        }
                        if (angular.isFunction($scope.callback)) {
                            $scope.callback({ item: $scope.selected, model: $scope.model });
                        }
                        // 触发$digest
                        $scope.$apply('$scope.model');
                    });
                });
            };

            // 默认加载当前选择的数据
            $scope.model = $scope.model || select.val();
           
            if ($scope.customStyle) {
                select.attr('style', $scope.customStyle);
            }

            if ($scope.async) {
                $scope.$on('dictionaryReady', prepareDataDict);
            } else {
                prepareDataDict();
            }
        }
    }
});

ppts.ng.directive('pptsDatepicker', function () {
    return {
        restrict: 'E',
        scope: {
            css: '@',
            model: '=',
            zIndex: '@'
        },
        templateUrl: mcs.app.config.pptsComponentBaseUrl + '/src/tpl/datepicker.tpl.html',
        controller: function ($scope) {
            if ($scope.zIndex) {
                $scope.dateStyle = { 'z-index': $scope.zIndex };
            }
        },
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
            width: '@',
            css: '@',
            size: '@',
            startDate: '=',
            endDate: '=',
            zIndex: '@'
        },
        templateUrl: mcs.app.config.pptsComponentBaseUrl + '/src/tpl/daterangepicker.tpl.html',
        controller: function ($scope) {
            $scope.dateStyle = {};

            if ($scope.zIndex) {
                $scope.dateStyle['z-index'] = $scope.zIndex;
            }
            if ($scope.width) {
                $scope.dateStyle['width'] = $scope.width;
            }
        },
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
            model: '=',
            zIndex: '@'
        },
        templateUrl: mcs.app.config.pptsComponentBaseUrl + '/src/tpl/timepicker.tpl.html',
        controller: function ($scope) {
            if ($scope.zIndex) {
                $scope.dateStyle = { 'z-index': $scope.zIndex };
            }
        },
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
            model: '=',
            zIndex: '@'
        },
        templateUrl: mcs.app.config.pptsComponentBaseUrl + '/src/tpl/datetimepicker.tpl.html',
        controller: function ($scope) {
            if ($scope.zIndex) {
                $scope.dateStyle = { 'z-index': $scope.zIndex };
            }
        },
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

ppts.ng.directive('datetimeType', function ($filter) {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function ($scope, ele, attrs, ngModelController) {
            ngModelController.$parsers.push(function (data) {
                return new Date(data);
            });

            ngModelController.$formatters.push(function (data) {
                return $filter('date')(data, 'yyyy-MM-dd HH:mm:ss');
            });

        }
    }
});

ppts.ng.directive('dateType', function ($filter) {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function ($scope, ele, attrs, ngModelController) {
            ngModelController.$parsers.push(function (data) {
                return new Date(data);
            });

            ngModelController.$formatters.push(function (data) {
                return $filter('date')(data, 'yyyy-MM-dd');
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
            maxText: '@',
            unit: '@'
        },
        templateUrl: mcs.app.config.pptsComponentBaseUrl + '/src/tpl/datarange.tpl.html',
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

ppts.ng.directive('pptsSource', function () {
    return {
        restrict: 'E',
        scope: {
            main: '=',
            sub: '=',
            selected: '=?'
        },
        template: '<mcs-cascading-select caption="信息来源一,信息来源二" model="model" url="{{requestUrl}}" params="params" callback="callback(model)"></mcs-cascading-select>',
        controller: function ($scope) {
            $scope.requestUrl = ppts.config.pptsApiBaseUrl + 'api/metadata/getdata';
            $scope.params = { parentKey: '0', category: ppts.config.dictMappingConfig.source }; //默认加载第一层参数(信息来源一)
            $scope.callback = function (value) {
                $scope.main = value['0'];
                $scope.sub = value['1'];
                $scope.selected = value['selected'];
            }
        }
    }
});

ppts.ng.directive('pptsRegion', ['$compile', function ($compile) {
    return {
        restrict: 'E',
        scope: {
            province: '=',
            city: '=',
            district: '=',
            street: '=?',
            selected: '=?'
        },
        template: '<mcs-cascading-select caption="省份,城市,区县" model="model" url="{{requestUrl}}" params="params" callback="callback(model)"></mcs-cascading-select>',
        controller: function ($scope) {
            $scope.requestUrl = ppts.config.pptsApiBaseUrl + 'api/metadata/getdata';
            $scope.params = { parentKey: '0', category: ppts.config.dictMappingConfig.region }; //默认加载第一层参数(省份)
            $scope.callback = function (value) {
                $scope.province = value['0'];
                $scope.city = value['1'];
                $scope.district = value['2'];
                $scope.selected = value['selected'];
            }
        },
        link: function ($scope, $elem, $attrs) {
            if (mcs.util.hasAttr($elem, 'street')) {
                $elem.after($compile(angular.element('<mcs-input model="street" placeholder="详细地址(如街道名称，门牌号等)" css="col-sm-12 mcs-margin-top-5" />'))($scope));
            }
        }
    }
}]);

ppts.ng.directive('pptsOrganization', function () {
    return {
        restrict: 'E',
        scope: {
            branch: '=',
            campus: '=',
            selected: '=?'
        },
        template: '<mcs-cascading-select caption="分公司,校区" model="model" url="{{requestUrl}}" params="params" other-params="otherParams" callback="callback(model)" custom-style="width:48.5%"></mcs-cascading-select>',
        controller: function ($scope) {
            $scope.requestUrl = ppts.config.pptsApiBaseUrl + 'api/organization/getchildrenbytype';
            $scope.params = { parentKey: ppts.user.orgId, departmentType: 2 }; //默认加载第一层参数(分公司)
            $scope.otherParams = { departmentType: 3 }; // 加载校区
            $scope.callback = function (value) {
                $scope.branch = value['0'];
                $scope.campus = value['1'];
                $scope.selected = value['selected'];
            }
        }
    }
});

ppts.ng.directive('pptsSelectCustomer', ['mcsDialogService', function (mcsDialogService) {
    return {
        restrict: 'E',
        scope: {
            model: '=',
            callback: '&?'
        },
        templateUrl: mcs.app.config.pptsComponentBaseUrl + '/src/tpl/selectcustomer.tpl.html',
        link: function ($scope, $elem, $attrs, $ctrl) {
            $scope.select = function () {
                mcsDialogService.create('app/customer/potentialcustomer/customer-search/select-customer.tpl.html', {
                    controller: 'customerSearchController',
                    settings: {
                        size: 'lg'
                    }
                }).result.then(function (customer) {
                    $scope.model = customer[0];
                    if (ng.isFunction($scope.callback)) {
                        $scope.callback()(customer[0]);
                    }
                });
            };
        }
    }
}]);

ppts.ng.directive('pptsSelectBranch', ['mcsDialogService', function (mcsDialogService) {
    return {
        restrict: 'E',
        scope: {
            //nodes: '=?',
            model: '=',
            title: '@'
        },
        templateUrl: mcs.app.config.pptsComponentBaseUrl + '/src/tpl/selectbranch.tpl.html',
        link: function ($scope, $elem, $attrs, $ctrl) {
            $scope.title = $scope.title || '选择大区/分公司/校区';
            $scope.select = function () {
                mcsDialogService.create(mcs.app.config.pptsComponentBaseUrl + '/src/tpl/tree.tpl.html', {
                    controller: 'treeController',
                    params: {
                        title: $scope.title,
                        id: ppts.user.orgId
                        //fullPath: '机构人员\\总公司'
                    }
                }).result.then(function (treeSettings) {
                    //$scope.nodes = treeSettings.getRawNodesChecked();
                    $scope.model = treeSettings.getIdsOfNodesChecked();
                    var checkNodes = treeSettings.getNamesOfNodesChecked();
                    $scope.names = checkNodes.join(',');
                    $scope.displayNames = checkNodes.length > 1 ? checkNodes[0] + '...' : checkNodes[0];
                });
            };
        }
    }
}]);

ppts.ng.directive('pptsSearch', function () {
    return {
        restrict: 'E',
        scope: {
            css: '@',
            model: '=',
            placeholder: '@',
            click: '&'
        },
        templateUrl: mcs.app.config.pptsComponentBaseUrl + '/src/tpl/search.tpl.html'
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