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
            parent: '=?',
            async: '@',
            clear: '@'
        },
        template: '<span><mcs-checkbox-group data="data" model="model"></mcs-checkbox-group></span>',
        link: function ($scope, $elem) {
            $scope.clear = mcs.util.bool($scope.clear || true);
            $scope.async = mcs.util.bool($scope.async || true);

            function prepareDataDict() {
                var items = ppts.dict[ppts.config.dictMappingConfig[$scope.category]];
                $scope.$watchCollection('parent', function () {
                    if ($scope.parent == undefined) {
                        $scope.data = items;
                    } else {
                        $scope.data = [];
                        $scope.model = [];
                        var array = mcs.util.toArray($scope.parent);
                        if (array.length == 1) {
                            var parentKey = array[0];
                            for (var index in items) {
                                var item = items[index];
                                if (item.parentKey == parentKey) {
                                    $scope.data.push(item);
                                }
                            }
                        }
                    }
                    if (!$scope.data.length) {
                        $elem.hide();
                    } else {
                        $elem.show();
                    }
                });
                if (mcs.util.hasAttr($elem, 'required')) {
                    $scope.required = true;

                    mcs.util.appendMessage($elem);
                }
            }

            if ($scope.async) {
                $scope.$on('dictionaryReady', prepareDataDict);
            } else {
                prepareDataDict();
            }

            $scope.model = $scope.model || [];

            if ($scope.clear) {
                $elem.prepend($compile(angular.element('<button class="btn btn-link" ng-click="model=[]">清空</button>'))($scope));
            }
        }
    }
}]);

ppts.ng.directive('pptsRadiobuttonGroup', function () {
    return {
        restrict: 'E',
        scope: {
            category: '@',
            showAll: '@',
            model: '=',
            css: '@',
            async: '@',
            value: '=?',
            parent: '=?'
        },
        template: '<mcs-radiobutton-group data="data" model="model" value="value" class="{{css}}"/>',
        link: function ($scope, $elem, $attrs, $ctrl) {
            function prepareDataDict() {
                $scope.data = ppts.dict[ppts.config.dictMappingConfig[$scope.category]];
                if ($scope.showAll) {
                    $scope.data.unshift({ key: '-1', value: '全部' });
                }
                if (mcs.util.hasAttr($elem, 'required')) {
                    $scope.required = true;

                    mcs.util.appendMessage($elem);
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
});

ppts.ng.directive('pptsSelect', ['mcsValidationService', function (validationService) {
    return {
        restrict: 'E',
        scope: {
            category: '@',
            css: '@',
            caption: '@',
            filter: '@',
            prop: '@',
            model: '=?',
            data: '=?',
            value: '=?',
            selected: '=?',
            async: '@',
            callback: '&?',
            disabled: '=?',
            parseType: '@',
            showDefault: '@',
            showAll: '@',
            customStyle: '@?'
        },
        template: '<div class="col-sm-12 {{css}}"><select style="width:100%;" ng-disabled="disabled" ng-style="customStyle"></select></div>',
        link: function ($scope, $elem) {
            $scope.caption = $scope.caption || '请选择';
            $scope.disabled = mcs.util.bool($scope.disabled || false);
            $scope.showDefault = mcs.util.bool($scope.showDefault || true);
            $scope.showAll = mcs.util.bool($scope.showAll || true);
            $scope.async = mcs.util.bool($scope.async || true);
            $scope.parseType = $scope.parseType || 'string';

            var select = $elem.find('select');
            if (mcs.util.hasAttr($elem, 'required')) {
                $scope.required = true;
                select.attr('required', 'required');

                mcs.util.appendMessage($elem, 'mcs-padding-left-15');
            }
            var prepareDataDict = function () {
                var items = ppts.dict[ppts.config.dictMappingConfig[$scope.category]];

                $scope.$watch('filter', function () {
                    if (!mcs.util.bool($scope.showAll) && $scope.filter == '') {
                        $scope.data = [];
                    } else {
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
                    }
                });
                $scope.$watch('data', function () {
                    select.empty();
                    if (mcs.util.bool($scope.showDefault)) {
                        select.append('<option value="">' + $scope.caption + '</option>');
                    }
                    $.each($scope.data, function (k, v) {
                        v.parentKey = v.parentKey || '';
                        var option = '<option value="' + v.key + '" parent="' + v.parentKey + '">' + v.value + '</option>';
                        if (v.key == $scope.model) {
                            option = '<option value="' + v.key + '" parent="' + v.parentKey + '" selected="selected">' + v.value + '</option>';
                        }
                        select.append(option);
                    });

                    select.select2().change(function () {
                        // 返回已选择的数据
                        $scope.model = select.val();
                        $scope.value = !select.val() ? '' : select.select2('data').text;
                        $scope.selected = $scope.selected || {};
                        $scope.selected.key = select.val();
                        $scope.selected.value = select.select2('data').text == $scope.caption ? '' : select.select2('data').text;
                        $scope.selected.parentKey = $(select.select2('data').element).attr('parent');
                        // 注册回调事件
                        if ($scope.parseType == 'int') {
                            $scope.model = parseInt($scope.model);
                        }
                        if ($scope.parseType == 'float') {
                            $scope.model = parseFloat($scope.model);
                        }
                        if (angular.isFunction($scope.callback)) {
                            $.each($scope.data, function (k, v) {
                                if (v.key == $scope.model) {
                                    $scope.selected.selectItem = v.selectItem;
                                    return false;
                                }
                            });
                            $scope.callback({ item: $scope.selected, model: $scope.model });
                        }
                        // 执行验证
                        validationService.validate(select, $scope);
                        // 触发$digest
                        $scope.$apply('$scope.model');
                    });
                });
            };

            // 默认加载当前选择的数据
            $scope.model = $scope.model == undefined ? select.val() : $scope.model;

            if ($scope.customStyle) {
                select.attr('style', $scope.customStyle);
            }

            if ($scope.async) {
                if ($scope.category) {
                    $scope.$on('dictionaryReady', prepareDataDict);
                } else {
                    $scope.$on('dataReady', prepareDataDict);
                }
            } else {
                prepareDataDict();
            }
        }
    }
}]);

ppts.ng.directive('pptsDatepicker', ['mcsValidationService', function (validationService) {
    return {
        restrict: 'E',
        scope: {
            css: '@',
            model: '=',
            placeholder: '@',
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
            $scope.placeholder = $scope.placeholder || '输入日期';
            if (mcs.util.hasAttr($elem, 'required')) {
                $scope.required = true;
                $elem.find('input[type=text]').attr('required', 'required');

                mcs.util.appendMessage($elem);
            }

            $this.datepicker({
                autoclose: true,
                todayHighlight: true,
                format: ppts.config.datePickerFormat,
                language: ppts.config.datePickerLang
            }) //show datepicker when clicking on the icon
            .on("hide", function () {
                validationService.validate($elem.find('input[required]'), $scope);
            }).next().on('click', function () {
                $(this).prev().focus();
            });
        }
    }
}]);

ppts.ng.directive('pptsDatetimepicker', ['mcsValidationService', function (validationService) {
    return {
        restrict: 'E',
        scope: {
            css: '@',
            model: '=',
            placeholder: '@',
            zIndex: '@'
        },
        templateUrl: mcs.app.config.pptsComponentBaseUrl + '/src/tpl/daterangepicker.tpl.html',
        controller: function ($scope) {
            if ($scope.zIndex) {
                $scope.dateStyle = { 'z-index': $scope.zIndex };
            }
        },
        link: function ($scope, $elem) {
            var $this = $elem.find('.date-timepicker');
            $scope.placeholder = $scope.placeholder || '输入时间';
            if (mcs.util.hasAttr($elem, 'required')) {
                $scope.required = true;
                $elem.find('input[type=text]').attr('required', 'required');

                mcs.util.appendMessage($elem);
            }

            $this.datetimepicker({
                format: ppts.config.datetimePickerFormat,
                language: ppts.config.datePickerLang,
                autoclose: true,
            }).on("hide", function () {
                var _this = this;
                $scope.$apply(function () {
                    $scope[$this.attr('ng-model')] = _this.value;
                });
                validationService.validate($elem.find('input[required]'), $scope);
            }).next().on('click', function () {
                $(this).prev().focus();
            });
        }
    }
}]);

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

ppts.ng.directive('pptsDatetimepicker', ['mcsValidationService', function (validationService) {
    return {
        restrict: 'E',
        scope: {
            css: '@',
            model: '=',
            placeholder: '@',
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
            $scope.placeholder = $scope.placeholder || '输入时间';
            if (mcs.util.hasAttr($elem, 'required')) {
                $scope.required = true;
                $elem.find('input[type=text]').attr('required', 'required');

                mcs.util.appendMessage($elem);
            }

            $this.datetimepicker({
                format: ppts.config.datetimePickerFormat,
                language: ppts.config.datePickerLang,
                autoclose: true,
            }).on("hide", function () {
                var _this = this;
                $scope.$apply(function () {
                    $scope[$this.attr('ng-model')] = _this.value;
                });
                validationService.validate($elem.find('input[required]'), $scope);
            }).next().on('click', function () {
                $(this).prev().focus();
            });
        }
    }
}]);

ppts.ng.directive('datetimeType', function ($filter) {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function ($scope, $elem, $attrs, $ctrl) {
            $ctrl.$parsers.push(function (data) {
                return new Date(data);
            });

            $ctrl.$formatters.push(function (data) {
                return $filter('date')(data, 'yyyy-MM-dd HH:mm:ss');
            });
        }
    }
});

ppts.ng.directive('dateType', function ($filter) {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function ($scope, $elem, $attrs, $ctrl) {
            $ctrl.$parsers.push(function (data) {
                return new Date(data);
            });

            $ctrl.$formatters.push(function (data) {
                return $filter('date')(data, 'yyyy-MM-dd');
            });
        }
    }
});

ppts.ng.directive('pptsDatarange', ['mcsValidationService', function (validationService) {
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
            if (mcs.util.hasAttr($elem, 'required')) {
                $scope.required = true;
                $elem.find('input[type=text]').attr('required', 'required');

                mcs.util.appendMessage($elem);
            }

            $scope.$watch('min', function (data) {
                if (data == undefined) return;
                var max = parseFloat($scope.max);
                if (data > max) {
                    $scope.min = max;
                }
                validationService.validate($elem.find('input[type=text]'), $scope);
            });
            $scope.$watch('max', function (data) {
                if (data == undefined) return;
                var min = parseFloat($scope.min);
                if (data < min) {
                    $scope.max = min;
                }
                validationService.validate($elem.find('input[type=text]'), $scope);
            });
        }
    }
}]);

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
        },
        link: function ($scope, $elem, $attrs) {
            if (mcs.util.hasAttr($elem, 'required')) {
                $scope.required = true;
            }
            if (mcs.util.hasAttr($elem, 'required-level')) {
                $scope.requiredLevel = $elem.attr('required-level');
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
            if (mcs.util.hasAttr($elem, 'required')) {
                $scope.required = true;
            }
            if (mcs.util.hasAttr($elem, 'required-level')) {
                $scope.requiredLevel = $elem.attr('required-level');
            }
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
        template: '<mcs-cascading-select caption="分公司,校区" model="model" url="{{requestUrl}}" params="params" other-params="otherParams" callback="callback(model)"></mcs-cascading-select>',
        controller: function ($scope) {
            $scope.requestUrl = ppts.config.pptsApiBaseUrl + 'api/organization/getchildrenbytype';
            $scope.params = { parentKey: ppts.user.orgId, departmentType: 2 }; //默认加载第一层参数(分公司)
            $scope.otherParams = { departmentType: 3 }; // 加载校区
            $scope.callback = function (value) {
                $scope.branch = value['0'];
                $scope.campus = value['1'];
                $scope.selected = value['selected'];
            }
        },
        link: function ($scope, $elem, $attrs) {
            if (mcs.util.hasAttr($elem, 'required')) {
                $scope.required = true;
            }
            if (mcs.util.hasAttr($elem, 'required-level')) {
                $scope.requiredLevel = $elem.attr('required-level');
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

ppts.ng.directive('pptsSelectStaff', ['$http', function ($http) {
    return {
        restrict: 'E',
        scope: {
            staff: '=',
            jobType: '='
        },
        template: '<ppts-select caption="选择人员" data="data" callback="callback(item)" css="mcs-padding-0"></ppts-select>',
        controller: function ($scope) {
            $scope.requestUrl = ppts.config.pptsApiBaseUrl + 'api/usergraph/getdata';
            $scope.params = { jobs: ppts.user.jobs, jobType: $scope.jobType };
            $scope.callback = function (item) {
                $scope.staff = item.selectItem;
            }
        },
        link: function ($scope, $elem) {
            $scope.$broadcast('dataReady');
            $http({
                method: 'post',
                url: $scope.requestUrl,
                data: $scope.params,
                cache: true
            }).then(function (result) {
                if (!result.data) return;
                $scope.data = result.data;
            });
        }
    }
}]);

ppts.ng.directive('pptsSelectBranch', ['mcsDialogService', function (mcsDialogService) {
    return {
        restrict: 'E',
        scope: {
            css: '@',
            parent: '@?',
            nodes: '=?',
            model: '=?',
            campusIds: '=?',
            campusNames: '=?',
            branchId: '=?',
            branchName: '=?',
            selected: '=?',
            selection: '@?',
            distinctLevel: '@?',
            title: '@',
            callback: '&?'
        },
        templateUrl: mcs.app.config.pptsComponentBaseUrl + '/src/tpl/selectbranch.tpl.html',
        link: function ($scope, $elem, $attrs, $ctrl) {
            var showDisplayNames = function (campusNames) {
                var names = mcs.util.toArray(campusNames);
                return names.length > 1 ? names[0] + '...' : names[0];
            }

            $scope.title = $scope.title || '选择大区/分公司/校区';
            $scope.$watch('campusNames', function () {
                $scope.displayNames = showDisplayNames($scope.campusNames);
                if (mcs.util.hasAttr($elem, 'required')) {
                    $scope.required = true;
                    $elem.find('input[type=text]').val($scope.displayNames).blur();
                }
            });

            $scope.select = function () {
                mcsDialogService.create(mcs.app.config.pptsComponentBaseUrl + '/src/tpl/tree.tpl.html', {
                    controller: 'treeController',
                    params: {
                        title: $scope.title,
                        id: ppts.user.orgId,
                        selection: $scope.selection,
                        distinctLevel: $scope.distinctLevel
                    }
                }).result.then(function (treeSettings) {
                    var checkNodeIds = treeSettings.getIdsOfNodesChecked();
                    var checkNodeNames = treeSettings.getNamesOfNodesChecked();
                    var checkNodes = treeSettings.getNodesChecked(mcs.util.bool($scope.parent || false));

                    if ($scope.distinctLevel) {
                        var branch = checkNodes.filter(function (node) {
                            return node.level == '1';
                        });
                        if (branch && branch.length) {
                            $scope.branchId = branch[0].id;
                            $scope.branchName = branch[0].name;
                        }
                    }

                    $scope.nodes = checkNodes;
                    $scope.model = checkNodeIds;
                    $scope.campusIds = checkNodeIds.join(',');
                    $scope.campusNames = checkNodeNames.join(',');
                    $scope.selected = { ids: $scope.model, names: checkNodeNames };
                    $scope.displayNames = showDisplayNames(checkNodeNames);

                    if (ng.isFunction($scope.callback)) {
                        $scope.callback()($scope.selected);
                    }
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