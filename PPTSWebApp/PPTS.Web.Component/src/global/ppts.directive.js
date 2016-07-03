// 功能权限
ppts.ng.directive('pptsRoles', function () {
    return {
        restrict: 'A',
        link: function ($scope, $elem, $attrs, $ctrl) {
            if (!$attrs.pptsRoles) return;
            if (!mcs.util.bool(ppts.config.enablePermission)) return;
            var hasPermisson = false;
            if (!mcs.util.hasAttr($elem, 'permission')) {
                if (mcs.util.contains(ppts.user.roles, $attrs.pptsRoles)) {
                    $elem.attr('permission', true);
                    hasPermisson = true;
                }
                // hide or disable the feature when no permisson
                if (!hasPermisson) {
                    if (mcs.util.hasAttr($elem, 'support-disabled')) {
                        if ($elem.hasClass('btn-group')) {
                            $elem = $elem.find('.dropdown-toggle');
                        }
                        $elem.attr('disabled', 'disabled');
                    } else {
                        $elem.addClass('hide');
                    }
                }
            }
        }
    };
});

ppts.ng.directive('pptsJobFunctions', ['storage', function (storage) {
    return {
        restrict: 'A',
        link: function ($scope, $elem, $attrs, $ctrl) {
            if (!$attrs.pptsJobFunctions) return;
            if (!mcs.util.bool(ppts.config.enablePermission)) return;
            var hasPermisson = false;
            if (!mcs.util.hasAttr($elem, 'permission')) {
                var currentJobId = storage.get('ppts.user.currentJobId_' + ppts.user.id);
                if (mcs.util.contains(ppts.user.jobFunctions[currentJobId], $attrs.pptsJobFunctions)) {
                    $elem.attr('permission', true);
                    hasPermisson = true;
                }
                // hide or disable the feature when no permisson
                if (!hasPermisson) {
                    if (mcs.util.hasAttr($elem, 'support-disabled')) {
                        if ($elem.hasClass('btn-group')) {
                            $elem = $elem.find('.dropdown-toggle');
                        }
                        $elem.attr('disabled', 'disabled');
                    } else {
                        $elem.addClass('hide');
                    }
                }
            }
        }
    };
}]);

ppts.ng.directive('pptsFunctions', function () {
    return {
        restrict: 'A',
        link: function ($scope, $elem, $attrs, $ctrl) {
            if (!$attrs.pptsFunctions) return;
            if (!mcs.util.bool(ppts.config.enablePermission)) return;
            var hasPermisson = false;
            if (!mcs.util.hasAttr($elem, 'permission')) {
                if (mcs.util.contains(ppts.user.functions, $attrs.pptsFunctions)) {
                    $elem.attr('permission', true);
                    hasPermisson = true;
                }
                // hide or disable the feature when no permisson
                if (!hasPermisson) {
                    if (mcs.util.hasAttr($elem, 'support-disabled')) {
                        if ($elem.hasClass('btn-group')) {
                            $elem = $elem.find('.dropdown-toggle');
                        }
                        $elem.attr('disabled', 'disabled');
                    } else {
                        $elem.addClass('hide');
                    }
                }
            }
        }
    };
});

ppts.ng.directive('pptsCheckboxGroup', function () {
    return {
        restrict: 'E',
        scope: {
            category: '@',
            model: '=',
            parent: '=?',
            disabled: '=?',
            async: '@',
            width: '@',
            showAll: '@'
        },
        template: '<span ng-show="data && data.length"><mcs-checkbox-group data="data" model="model"></mcs-checkbox-group></span>',
        link: function ($scope, $elem) {
            $scope.showAll = mcs.util.bool($scope.showAll || true);
            $scope.async = mcs.util.bool($scope.async || true);
            $scope.disabled = mcs.util.bool($scope.disabled || false);

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
                    if (mcs.util.bool($scope.showAll)) {
                        if (!$scope.data) $scope.data = [];
                        if (mcs.util.indexOf($scope.data, function (item) {
                            return item.key == -1 && item.value == '全部';
                        }) < 0) {
                            $scope.data.unshift({
                                key: '-1',
                                value: '全部'
                            });
                        }
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
        }
    }
});
ppts.ng.directive('pptsRadiobuttonGroup', function () {
    return {
        restrict: 'E',
        scope: {
            category: '@',
            showAll: '@',
            model: '=',
            disabled: '=?',
            css: '@',
            async: '@',
            value: '=?',
            parent: '=?'
        },
        template: '<mcs-radiobutton-group data="data" model="model" value="value" class="{{css}}"/>',
        link: function ($scope, $elem, $attrs, $ctrl) {
            $scope.showAll = mcs.util.bool($scope.showAll || false);
            $scope.async = mcs.util.bool($scope.async || true);
            $scope.disabled = mcs.util.bool($scope.disabled || false);

            function prepareDataDict() {
                $scope.data = ppts.dict[ppts.config.dictMappingConfig[$scope.category]];
                if (mcs.util.bool($scope.showAll)) {
                    if (!$scope.data) $scope.data = [];
                    if (mcs.util.indexOf($scope.data, function (item) {
                            return item.key == -1 && item.value == '全部';
                    }) < 0) {
                        $scope.data.unshift({
                            key: '-1',
                            value: '全部'
                        });
                    }
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
                            $scope.model = $scope.model || item.key;
                        }
                    }
                });
                $scope.$watch('model', function (value) {
                    // fix the bug: The radiobutton will be resumed the original state when the page changed.
                    if (!value) {
                        $scope.model = -1;
                    }
                });
            };

            if ($scope.async) {
                $scope.$on('dictionaryReady', prepareDataDict);
            } else {
                prepareDataDict();
            }
            $scope.model = $scope.model || '';
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
            $scope.params = {
                parentKey: '0',
                category: ppts.config.dictMappingConfig.source
            }; //默认加载第一层参数(信息来源一)
            $scope.callback = function (value) {
                $scope.main = value['0'];
                $scope.sub = value['1'];
                $scope.selected = value['selected'];
            };
            // 默认加载model
            $scope.$watch('main', function () {
                $scope.model = {
                    '0': $scope.main,
                    '1': $scope.sub
                };
            });
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
            $scope.params = {
                parentKey: '0',
                category: ppts.config.dictMappingConfig.region
            }; //默认加载第一层参数(省份)
            $scope.callback = function (value) {
                $scope.province = value['0'];
                $scope.city = value['1'];
                $scope.district = value['2'];
                $scope.selected = value['selected'];
            };
            $scope.$watch('province', function () {
                // 默认加载model
                $scope.model = {
                    '0': $scope.province,
                    '1': $scope.city,
                    '2': $scope.district
                };
            });
        },
        link: function ($scope, $elem, $attrs) {
            if (mcs.util.hasAttr($elem, 'required')) {
                $scope.required = true;
            }
            if (mcs.util.hasAttr($elem, 'required-level')) {
                $scope.requiredLevel = $elem.attr('required-level');
            }
            if (mcs.util.hasAttr($elem, 'street')) {
                if (mcs.util.hasAttr($elem, 'maxlength')) {
                    $elem.after($compile(angular.element('<mcs-input model="street" placeholder="详细地址(如街道名称，门牌号等)" css="col-sm-12 mcs-margin-top-5" maxlength="' + $elem.attr('maxlength') + '"/>'))($scope));
                } else {
                    $elem.after($compile(angular.element('<mcs-input model="street" placeholder="详细地址(如街道名称，门牌号等)" css="col-sm-12 mcs-margin-top-5" />'))($scope));
                }
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
            selected: '=?',
            disabledLevel: '=?'
        },
        template: '<mcs-cascading-select caption="分公司,校区" model="model" url="{{requestUrl}}" params="params" other-params="otherParams" disabled-level="disabledLevel" callback="callback(model)"></mcs-cascading-select>',
        controller: function ($scope) {
            $scope.requestUrl = ppts.config.pptsApiBaseUrl + 'api/organization/getchildrenbytype';
            $scope.params = {
                parentKey: ppts.user.orgId,
                departmentType: 2
            }; //默认加载第一层参数(分公司)
            $scope.otherParams = {
                departmentType: 3
            }; // 加载校区
            $scope.callback = function (value) {
                $scope.branch = value['0'];
                $scope.campus = value['1'];
                $scope.selected = value['selected'];
            };
            // 默认加载model
            $scope.$watch('branch', function () {
                $scope.model = {
                    '0': $scope.branch,
                    '1': $scope.campus
                };
            });
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

ppts.ng.directive('pptsBranch', ['$http', function ($http) {
    return {
        restrict: 'E',
        scope: {
            branch: '=',
            selected: '=?',
            disabled: '=?'
        },
        template: '<mcs-select caption="选择分公司" data="data" model="branch" callback="callback(item)" disabled="disabled" css="mcs-padding-0"></ppts-select>',
        controller: function ($scope) {
            $scope.requestUrl = ppts.config.pptsApiBaseUrl + 'api/organization/getchildrenbytype';
            $scope.params = {
                parentKey: ppts.user.orgId,
                departmentType: 2
            }; //默认加载第一层参数(分公司)
            // 默认加载model
            $scope.callback = function (item) {
                $scope.branch = item.key;
                $scope.selected = { branchId: item.key, branchName: item.value };
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

ppts.ng.directive('pptsSchool', function () {
    return {
        restrict: 'E',
        scope: {
            css: '@?',
            customStyle: '@?',
            model: '=?',
            name: '=?',
            selected: '=?'
        },
        template: '<mcs-autocomplete category="school" placeholder="请选择学校" css="{{css}}" custom-style="{{customStyle}}" model="selected" key-property="schoolID" display-property="schoolName" url="{{requestUrl}}"></mcs-autocomplete>',
        controller: function ($scope) {
            $scope.requestUrl = ppts.config.pptsApiBaseUrl + 'api/usergraph/getschools';
            $scope.$watchCollection('selected', function (value) {
                $scope.model = value && value.length ? value[0].schoolID : '';
                $scope.name = value && value.length ? value[0].schoolName : '';
            });
        }
    }
});

ppts.ng.directive('pptsAddress', function () {
    return {
        restrict: 'E',
        scope: {
            css: '@?',
            customStyle: '@?',
            model: '=?'
        },
        template: '<mcs-autocomplete category="address" placeholder="请选择家庭住址" css="{{css}}" custom-style="{{customStyle}}" model="selected" key-property="key" filter="filter" display-property="value" url="{{requestUrl}}"></mcs-autocomplete>',
        controller: function ($scope) {
            $scope.requestUrl = ppts.config.pptsApiBaseUrl + 'api/usergraph/getaddress';
            $scope.filter = function (data) {
                var items = [];
                if (data && data.length) {
                    for (var i in data) {
                        items.push({ key: data[i], value: data[i] });
                    }
                }
                return items;
            };
            $scope.$watchCollection('selected', function (value) {
                $scope.model = value && value.length ? value[0].value : '';
            });
        }
    }
});

ppts.ng.directive('pptsApprover', function () {
    return {
        restrict: 'E',
        scope: {
            css: '@?',
            customStyle: '@?',
            model: '=?',
            maxTags: '@?',
            listMark: '='
        },
        template: '<mcs-autocomplete category="approver" placeholder="请选择审批人" css="{{css}}" custom-style="{{customStyle}}" model="model" max-tags="maxTags" full-path="fullPath" display-property="name" key-property="fullPath" url="{{requestUrl}}" params="params"></mcs-autocomplete>',
        controller: function ($scope) {
            $scope.requestUrl = ppts.config.mcsApiBaseUrl + 'api/usergraph/query';
            $scope.listMark = $scope.listMark || 15;
            $scope.params = {
                listMark: $scope.listMark
            };
        }
    }
});

ppts.ng.directive('pptsSelectCustomer', ['mcsDialogService', 'mcsValidationService', function (mcsDialogService, validationService) {
    return {
        restrict: 'E',
        scope: {
            css: '@',
            model: '=',
            callback: '&?'
        },
        templateUrl: mcs.app.config.pptsComponentBaseUrl + '/src/tpl/selectcustomer.tpl.html',
        link: function ($scope, $elem, $attrs, $ctrl) {
            var input = $elem.find('input[type=text]');
            if (mcs.util.hasAttr($elem, 'required')) {
                $scope.required = true;
                input.attr('required', 'required');

                mcs.util.appendMessage($elem);
            }
            $scope.clear = function () {
                $scope.model = null;
            };
            $scope.select = function () {
                mcsDialogService.create('app/customer/potentialcustomer/customer-search/select-customer.tpl.html', {
                    controller: 'customerSearchController',
                    settings: {
                        size: 'lg'
                    }
                }).result.then(function (customer) {
                    $scope.model = customer[0];
                    input.val($scope.model ? $scope.model.customerName : '');
                    validationService.validate($elem.find('input[required]'), $scope);
                    if (ng.isFunction($scope.callback)) {
                        $scope.callback()(customer[0]);
                    }
                });
            };
        }
    }
}]);


ppts.ng.directive('pptsSelectStaff', ['$http', 'mcsValidationService', function ($http, mcsValidationService) {
    return {
        restrict: 'E',
        scope: {
            staff: '=',
            jobType: '='
        },
        template: '<mcs-select caption="选择人员" data="data" callback="callback(item)" css="mcs-padding-0"></ppts-select>',
        controller: function ($scope) {
            $scope.requestUrl = ppts.config.pptsApiBaseUrl + 'api/usergraph/getdata';
            $scope.params = {
                jobs: ppts.user.jobs,
                jobType: $scope.jobType
            };
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
            $scope.$watch('campusNames', function (value) {
                $scope.displayNames = showDisplayNames($scope.campusNames);
                if (mcs.util.hasAttr($elem, 'required')) {
                    $scope.required = true;
                    $elem.find('input[type=text]').val($scope.displayNames);
                    if (value) {
                        $elem.find('input[type=text]').blur();
                    }
                }
            });
            $scope.clear = function () {
                $scope.branchId = $scope.branchName = $scope.campusNames = $scope.campusIds = '';
                $scope.selected = null;
            };
            $scope.$watch('model', function (value) {
                if (!value || !value.length) {
                    $scope.clear();
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
                    $scope.selected = {
                        ids: $scope.model,
                        names: checkNodeNames
                    };
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

ppts.ng.directive("pptsCompileHtml", ['$sce', '$compile', function ($sce, $compile) {
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

ppts.ng.directive("pptsDynamicContent", function () {
    return {
        restrict: 'E',
        replace: true,
        transclude: true,
        templateUrl: function ($elem, $attrs) {
            return $attrs.template;
        }
    };
});

/*
ppts.ng.directive('pptsSidebarMenu', function () {
    return {
        restrict: 'AC',
        link: function (scope, el, attr) {
            el.find('li.active').parents('li').addClass('active open');

            el.on('click', 'a', function (e) {
                e.preventDefault();
                var isCompact = $("#sidebar").hasClass("menu-compact");
                var menuLink = $(e.target);
                if ($(e.target).is('span'))
                    menuLink = $(e.target).closest('a');
                if (!menuLink || menuLink.length == 0)
                    return;
                if (!menuLink.hasClass("menu-dropdown")) {
                    if (isCompact && menuLink.get(0).parentNode.parentNode == this) {
                        var menuText = menuLink.find(".menu-text").get(0);
                        if (e.target != menuText && !$.contains(menuText, e.target)) {
                            return false;
                        }
                    }
                    return;
                }
                var submenu = menuLink.next().get(0);
                if (!$(submenu).is(":visible")) {
                    var c = $(submenu.parentNode).closest("ul");
                    if (isCompact && c.hasClass("ppts-sidebar-menu"))
                        return;
                    c.find("* > .open > .submenu")
                        .each(function () {
                            if (this != submenu && !$(this.parentNode).hasClass("active"))
                                $(this).slideUp(200).parent().removeClass("open");
                        });
                }
                if (isCompact && $(submenu.parentNode.parentNode).hasClass("ppts-sidebar-menu"))
                    return false;
                $(submenu).slideToggle(200).parent().toggleClass("open");
                return false;
            });
        }
    };
});
*/