// 依赖于mcs.js, angularjs, 将会在应用程序启动之后执行
(function() {
    'use strict';

    mcs.ng = mcs.ng || angular.module('mcs.ng', ['mcs.ng.datatable', 'mcs.ng.paging', 'mcs.ng.treeControl']);
    mcs.ng.constant('mcsComponentConfig', {
        rootUrl: mcs.app.config.componentBaseUrl
    });
})();

(function () {
    'use strict';

    mcs.ng.filter('props', function () {
        return function (items, props) {
            var out = [];
            if (angular.isArray(items)) {
                var keys = Object.keys(props);
                items.forEach(function (item) {
                    var itemMatches = false;
                    for (var i = 0; i < keys.length; i++) {
                        var prop = keys[i];
                        var text = props[prop].toLowerCase();
                        if (item[prop].toString().toLowerCase().indexOf(text) !== -1) {
                            itemMatches = true;
                            break;
                        }
                    }
                    if (itemMatches) {
                        out.push(item);
                    }
                });
            } else {
                // Let the output be the input untouched
                out = items;
            }
            return out;
        };
    });
})();
(function () {
    'use strict';

    mcs.ng.constant('buttonConfig', {
        types: ['add', 'edit', 'delete', 'search', 'save', 'cancel'],
        defaultTexts: ['新 增', '编 辑', '删 除', '查 询', '保 存', '取 消'],
        sizes: ['mini', 'medium', 'large'],
        sizeCss: ['btn-xs', 'btn-sm', 'btn-lg'],
        //buttonClass: ['btn-primary', 'btn-success', 'btn-danger', 'btn-info'],
        iconClass: ['fa-plus', 'fa-pencil', 'fa-trash-o', 'fa-search', 'fa-save', 'fa-undo']
    })
    .directive('mcsButton', function (buttonConfig) {

        return {
            restrict: 'E',
            scope: {
                type: '@',
                disable: '=',
                text: '@',
                icon: '@',
                size: '@',
                css: '@',
                click: '&'
            },

            template: '<button class="btn mcs-width-130" ng-click="click()" ng-disabled="disable"><i class="ace-icon fa"></i><span></span></button>',
            replace: true,
            transclude: true,
            link: function ($scope, $elem, $attrs, $ctrl) {
                var index = buttonConfig.types.indexOf($scope.type);
                var buttonCss = 'btn-info';
                switch ($scope.type) {
                    case 'search':
                        buttonCss = 'btn-primary';
                        break;
                    case 'delete':
                        buttonCss = 'btn-danger';
                        break;
                    case 'save':
                        buttonCss = 'btn-success';
                        break;
                    case 'cancel':
                        buttonCss = 'btn-light';
                        break;
                }
                $elem.addClass(buttonCss);
                if ($scope.size) {
                    var sizeIndex = buttonConfig.sizes.indexOf($scope.size);
                    $elem.addClass(buttonConfig.sizeCss[sizeIndex]);
                }
                if ($scope.icon && index === -1) {
                    if ($scope.icon.indexOf('fa-') == 0) {
                        $elem.find('i').addClass($scope.icon);
                    } else {
                        $elem.find('i').addClass('fa-' + $scope.icon);
                    }
                } else {
                    $elem.find('i').addClass(buttonConfig.iconClass[index]);
                }
                if ($scope.css) {
                    $elem.addClass($scope.css);
                }
                $elem.find('span').text($scope.text || buttonConfig.defaultTexts[index]);
            }
        };
    });
})();
(function() {
    mcs.ng.directive('mcsLinkList', function($compile) {
        return {
            restrict: 'A',
            scope: {
                data: '=',
                click: '&'

            },
            template: '<button type="button" class="btn btn-link" data-toggle="button"  ng-repeat="item in data" ng-click="click({eventArgs:$event})">{{item.id}}</button>',

            link: function($scope, ele, attrs, ctrl) {


            }

        }
    })

    .directive('mcsCascadingSelect', function($compile) {

        return {
            restrict: 'E',
            scope: {
                division: '=data',
                ngModel: '=',
                required: '@'
            },

            templateUrl: mcs.app.config.componentBaseUrl + '/src/tpl/mcs-cascading-select.tpl.html ',
            replace: true,

            link: function($scope, iElm, iAttrs) {


            }
        };
    });
})();
(function () {
    'use strict';

    mcs.ng.directive('mcsCheckboxGroup', function () {
        return {
            restrict: 'E',
            scope: {
                data: '=',
                model: '='
            },          
            template: '<label class="checkbox-inline" ng-repeat="item in data"><input type="checkbox" class="ace" ng-checked="model && model.indexOf(item.key)!=-1" ng-click="change(item, $event)"><span class="lbl"></span> {{item.value}}</label>',
            controller: function ($scope) {
                $scope.change = function (item, event) {
                    mcs.util.setSelectedItems($scope.model, item, event);
                };
            }
        }
    });

})();
/**
 * '每页<select ng-model="conf.pageSize" ng-options="option for option in conf.perPageOptions "></select>'
 * you can use this for specify items in one page
 */
(function() {
    angular.module('mcs.ng.paging', [])
        .controller('pagingController', function($scope) {

        })

    .directive('tmPagination', function() {
        return {
            restrict: 'EA',
            template: '<div class="page-list">' +
                '<ul class="pagination" ng-show="conf.totalCount > 0">' +
                '<li ng-class="{disabled: conf.pageIndex == 1}" ng-click="prevPage()"><span>&laquo;</span></li>' +
                '<li ng-repeat="item in pageList track by $index" ng-class="{active: item == conf.pageIndex, separate: item == \'...\'}" ' +
                'ng-click="changepageIndex(item)">' +
                '<span>{{ item }}</span>' +
                '</li>' +
                '<li ng-class="{disabled: conf.pageIndex == conf.numberOfPages}" ng-click="nextPage()"><span>&raquo;</span></li>' +
                '</ul>' +
                '<div class="page-total" ng-show="conf.totalCount > 0">' +
                '第<input type="text" ng-model="jumpPageNum"  ng-keypress="jumpToPage($event)"/>页 ' +

                '共<strong>{{ conf.totalCount }}</strong>条记录' +
                '</div>' +
                '<div class="no-items" ng-show="conf.totalCount <= 0">暂无数据</div>' +
                '</div>',
            replace: true,
            scope: {
                conf: '=',
                pageChange: '&'
            },
            link: function(scope, element, attrs) {

                // 变更当前页
                scope.changepageIndex = function(item) {
                    if (item == '...') {
                        return;
                    } else {
                        scope.conf.pageIndex = item;
                        scope.pageChange()
                    }
                };

                // 定义分页的长度必须为奇数 (default:9)
                scope.conf.pagesLength = parseInt(scope.conf.pagesLength) ? parseInt(scope.conf.pagesLength) : 9;
                if (scope.conf.pagesLength % 2 === 0) {
                    // 如果不是奇数的时候处理一下
                    scope.conf.pagesLength = scope.conf.pagesLength - 1;
                }

                // conf.erPageOptions
                if (!scope.conf.perPageOptions) {
                    scope.conf.perPageOptions = [10, 15, 20, 30, 50];
                }

                // pageList数组
                function getPagination(newValue, oldValue) {


                    // conf.pageIndex
                    scope.conf.pageIndex = parseInt(scope.conf.pageIndex) ? parseInt(scope.conf.pageIndex) : 1;



                    // conf.totalCount
                    scope.conf.totalCount = parseInt(scope.conf.totalCount) ? parseInt(scope.conf.totalCount) : 0;

                    // conf.pageSize (default:15)
                    scope.conf.pageSize = parseInt(scope.conf.pageSize) ? parseInt(scope.conf.pageSize) : 15;


                    // numberOfPages
                    scope.conf.numberOfPages = Math.ceil(scope.conf.totalCount / scope.conf.pageSize);

                    // judge pageIndex > scope.numberOfPages
                    if (scope.conf.pageIndex < 1) {
                        scope.conf.pageIndex = 1;
                    }

                    // 如果分页总数>0，并且当前页大于分页总数
                    if (scope.conf.numberOfPages > 0 && scope.conf.pageIndex > scope.conf.numberOfPages) {
                        scope.conf.pageIndex = scope.conf.numberOfPages;
                    }

                    // jumpPageNum
                    scope.jumpPageNum = scope.conf.pageIndex;

                    // 如果pageSize在不在perPageOptions数组中，就把pageSize加入这个数组中
                    var perPageOptionsLength = scope.conf.perPageOptions.length;
                    // 定义状态
                    var perPageOptionsStatus;
                    for (var i = 0; i < perPageOptionsLength; i++) {
                        if (scope.conf.perPageOptions[i] == scope.conf.pageSize) {
                            perPageOptionsStatus = true;
                        }
                    }
                    // 如果pageSize在不在perPageOptions数组中，就把pageSize加入这个数组中
                    if (!perPageOptionsStatus) {
                        scope.conf.perPageOptions.push(scope.conf.pageSize);
                    }

                    // 对选项进行sort
                    scope.conf.perPageOptions.sort(function(a, b) {
                        return a - b
                    });

                    scope.pageList = [];
                    if (scope.conf.numberOfPages <= scope.conf.pagesLength) {
                        // 判断总页数如果小于等于分页的长度，若小于则直接显示
                        for (i = 1; i <= scope.conf.numberOfPages; i++) {
                            scope.pageList.push(i);
                        }
                    } else {
                        // 总页数大于分页长度（此时分为三种情况：1.左边没有...2.右边没有...3.左右都有...）
                        // 计算中心偏移量
                        var offset = (scope.conf.pagesLength - 1) / 2;
                        if (scope.conf.pageIndex <= offset) {
                            // 左边没有...
                            for (i = 1; i <= offset + 1; i++) {
                                scope.pageList.push(i);
                            }
                            scope.pageList.push('...');
                            scope.pageList.push(scope.conf.numberOfPages);
                        } else if (scope.conf.pageIndex > scope.conf.numberOfPages - offset) {
                            scope.pageList.push(1);
                            scope.pageList.push('...');
                            for (i = offset + 1; i >= 1; i--) {
                                scope.pageList.push(scope.conf.numberOfPages - i);
                            }
                            scope.pageList.push(scope.conf.numberOfPages);
                        } else {
                            // 最后一种情况，两边都有...
                            scope.pageList.push(1);
                            scope.pageList.push('...');

                            for (i = Math.ceil(offset / 2); i >= 1; i--) {
                                scope.pageList.push(scope.conf.pageIndex - i);
                            }
                            scope.pageList.push(scope.conf.pageIndex);
                            for (i = 1; i <= offset / 2; i++) {
                                scope.pageList.push(scope.conf.pageIndex + i);
                            }

                            scope.pageList.push('...');
                            scope.pageList.push(scope.conf.numberOfPages);
                        }
                    }

                    if (scope.conf.onChange) {


                        // 防止初始化两次请求问题
                        if (!(oldValue != newValue && oldValue[0] == 0)) {
                            scope.conf.onChange();
                        }

                    }
                    scope.$parent.conf = scope.conf;
                }

                // prevPage
                scope.prevPage = function() {
                    if (scope.conf.pageIndex > 1) {
                        scope.conf.pageIndex -= 1;
                        scope.pageChange();

                    }
                };
                // nextPage
                scope.nextPage = function() {
                    if (scope.conf.pageIndex < scope.conf.numberOfPages) {
                        scope.conf.pageIndex += 1;
                        scope.pageChange();
                    }
                };

                // 跳转页
                scope.jumpToPage = function() {
                    scope.jumpPageNum = scope.jumpPageNum.replace(/[^0-9]/g, '');
                    if (scope.jumpPageNum !== '') {
                        scope.conf.pageIndex = scope.jumpPageNum;
                        scope.pageChange();
                    }
                };



                scope.$watch(function() {


                    if (!scope.conf.totalCount) {
                        scope.conf.totalCount = 0;
                    }


                    var newValue = scope.conf.totalCount + ' ' + scope.conf.pageIndex + ' ' + scope.conf.pageSize;


                    return newValue;



                }, getPagination);

            }
        };
    });

})();

(function() {

    angular.module('mcs.ng.datatable', [])

    .controller('datatableController', function($scope) {
        $scope.paginationConf = {
            currentPage: 1,
            itemsPerPage: 5,
            totalItems: 20
        };

    })



    .directive('mcsDatatable', function($compile) {

        return {
            restrict: 'E',
            templateUrl: mcs.app.config.componentBaseUrl + '/src/tpl/mcs-datatable.tpl.html ',
            replace: true,
            scope: {

                data: '=',
                onOrder: '&?',
                pageChanged: '&'

            },
            link: function($scope, iElm, iAttrs, controller) {

                $scope.selectAll = function() {
                    if ($scope.data.selectAll) {
                        $scope.data.rows.forEach(function(row) {
                            row.selected = true;
                        })
                    } else {
                        $scope.data.rows.forEach(function(row) {
                            row.selected = false;
                        })
                    }
                }

                $scope.rowSelect = function(rowSelected) {
                    $scope.data.selectAll = false;
                    if ($scope.data.selection == 'radio') {
                        rowSelected.selected = true;
                        $scope.data.rows.forEach(function(row) {
                            if (rowSelected[$scope.data.keyField] != row[$scope.data.keyField]) {
                                row.selected = false;
                            }
                        })

                    }
                }



                $scope.reorder = function(header, callback) {
                    header.orderDirection = (header.orderDirection == 'asc' || header.orderDirection == undefined) ? 'desc' : 'asc';

                    if (header.sortable) {
                        if ($scope.onOrder && angular.isFunction($scope.onOrder())) {
                            $scope.onOrder({
                                dataField: header.field,
                                sortDirection: header.orderDirection == 'asc' ? 0 : 1
                            });
                        } else {
                            $scope.data.orderBy[0].dataField = header.field;
                            $scope.data.orderBy[0].sortDirection = header.orderDirection == 'asc' ? 0 : 1;
                            callback();
                        }

                    }


                }

                var reg = /click=['"]\w*/ig

                $scope.data.headers.forEach(function(header, index) {

                    if (!header.field) {
                        var results = header.template.match(reg);
                        if (results) {
                            results.forEach(function(r) {
                                var fnName = r.replace('"', '\'').split('\'')[1];
                                $scope[fnName] = $scope.$parent[fnName];

                            })
                        }

                    }
                })

            }
        };
    })

    .directive('mcsDatatableRow', function($compile) {
        return {
            restrict: 'A',
            link: function(scope, iElement, iAttrs) {
                var dataHeaders = scope.$parent.data.headers;
                var rowData = scope.row;


                dataHeaders.forEach(function(header, index) {
                    var td = $compile('<td>' + (header.template || ('<span>{{row.' + header.field + '}}</span>')) + '</td>')(scope);
                    iElement.append(td);
                });

            }
        };
    });
})();

(function() {
    'use strict';

    mcs.ng.service('mcsDialogService', ['', function() {

    }]);

})();

(function () {
    'use strict';

    mcs.ng.constant('inputConfig', {
        types: ['text']
    }).directive('mcsInput', ['inputConfig', 'mcsComponentConfig', function (config, mcsComponentConfig) {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                id: '@',
                type: '@',
                placeholder: '@',
                fixed: '@',
                readonly: '@',
                css: '@',
                model: '=',
            },
            template: '<input placeholder="{{placeholder}}" class="mcs-default-size-input mcs-margin-right-20 {{css}}" ng-model="model" />',
            link: function ($scope, $elem, $attrs) {
                if ($scope.id) {
                    $elem.attr('id', $scope.id);
                }
                $elem.attr('type', $scope.type || 'text');
                if ($scope.fixed) {
                    $elem.addClass('fixed');
                }
                if ($scope.readonly) {
                    $elem.attr('readonly', 'readonly');
                }
            }
        };
    }]);
})();
(function () {
    'use strict';

    mcs.ng.directive('mcsLabel', function () {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                required: '@',
                text: '@',
                forInput: '@',
                css: '@'
            },
            template: '<label class="control-label no-padding-right {{css}}" for="{{forInput}}"> {{text}}</label>',
            link: function ($scope, $elem, $attrs) {
                if ($scope.required) {
                    $elem.prepend('<span class="required">*</span>');
                }
                if ($scope.css) {
                    $elem.addClass($scope.css);
                }
            }
        };
    });
})();
(function () {
    'use strict';

    mcs.ng.directive('mcsRadiobuttonGroup', function () {

        return {
            restrict: 'E',
            scope: {
                data: '=',
                model: '='
            },

            template: '<label class="radio-inline" ng-repeat="item in data"><input name="{{groupName}}" type="radio" class="ace" ng-checked="model && item.key==model" ng-click="change(item)"><span class="lbl"></span> {{item.value}}</label>',
            controller: function ($scope) {
                $scope.groupName = mcs.util.newGuid();
                $scope.change = function (item) {
                    $scope.model = item.key;
                };
            }
        }
    });

    //mcs.ng.directive('mcsRadiobuttonGroup', ['$compile', function ($compile) {

    //    return {
    //        restrict: 'E',
    //        replace: true,
    //        scope: {
    //            data: '=',
    //            model: '='
    //        },
    //        template: '<div></div>',
    //        link: function ($scope, $elem, $attrs, $ctrl) {
    //            if (!$scope.data) return;
    //            $scope.groupName = mcs.util.newGuid();
    //            $scope.data.forEach(function (item, index) {
    //                $elem.append($compile(angular.element('<label class="radio-inline"><input type="radio" name="{{groupName}}" class="ace" ng-model="model" value="' + item.key + '"/><span class="lbl"></span> ' + item.value + '</label>'))($scope));
    //            });
    //        }
    //    }
    //}]);
})();
(function() {
    'use strict';

    angular.module('mcs.ng.treeControl', [])
        .constant('treeConfig', {
            templateUrl: null
        })
        .directive('treecontrol', ['$compile', function($compile) {
            /**
             * @param cssClass - the css class
             * @param addClassProperty - should we wrap the class name with class=""
             */
            function classIfDefined(cssClass, addClassProperty) {
                if (cssClass) {
                    if (addClassProperty)
                        return 'class="' + cssClass + '"';
                    else
                        return cssClass;
                } else
                    return "";
            }

            function ensureDefault(obj, prop, value) {
                if (!obj.hasOwnProperty(prop))
                    obj[prop] = value;
            }

            return {
                restrict: 'EA',
                require: "treecontrol",
                transclude: true,
                scope: {
                    treeModel: "=",
                    selectedNode: "=?",
                    selectedNodes: "=?",
                    expandedNodes: "=?",
                    onSelection: "&",
                    onNodeToggle: "&",
                    options: "=?",
                    orderBy: "@",
                    reverseOrder: "@",
                    filterExpression: "=?",
                    filterComparator: "=?"
                },
                controller: ['$scope', '$templateCache', '$interpolate', 'treeConfig', function($scope, $templateCache, $interpolate, treeConfig) {

                    function defaultIsLeaf(node) {
                        return !node[$scope.options.nodeChildren] || node[$scope.options.nodeChildren].length === 0;
                    }

                    function shallowCopy(src, dst) {
                        if (angular.isArray(src)) {
                            dst = dst || [];

                            for (var i = 0; i < src.length; i++) {
                                dst[i] = src[i];
                            }
                        } else if (angular.isObject(src)) {
                            dst = dst || {};

                            for (var key in src) {
                                if (hasOwnProperty.call(src, key) && !(key.charAt(0) === '$' && key.charAt(1) === '$')) {
                                    dst[key] = src[key];
                                }
                            }
                        }

                        return dst || src;
                    }

                    function defaultEquality(a, b) {
                        if (!a || !b)
                            return false;
                        a = shallowCopy(a);
                        a[$scope.options.nodeChildren] = [];
                        b = shallowCopy(b);
                        b[$scope.options.nodeChildren] = [];
                        return angular.equals(a, b);
                    }

                    function defaultIsSelectable() {
                        return true;
                    }

                    $scope.options = $scope.options || {};
                    ensureDefault($scope.options, "multiSelection", false);
                    ensureDefault($scope.options, "nodeChildren", "children");
                    ensureDefault($scope.options, "dirSelectable", "true");
                    ensureDefault($scope.options, "injectClasses", {});
                    ensureDefault($scope.options.injectClasses, "ul", "");
                    ensureDefault($scope.options.injectClasses, "li", "");
                    ensureDefault($scope.options.injectClasses, "liSelected", "");
                    ensureDefault($scope.options.injectClasses, "iExpanded", "");
                    ensureDefault($scope.options.injectClasses, "iCollapsed", "");
                    ensureDefault($scope.options.injectClasses, "iLeaf", "");
                    ensureDefault($scope.options.injectClasses, "label", "");
                    ensureDefault($scope.options.injectClasses, "labelSelected", "");
                    ensureDefault($scope.options, "equality", defaultEquality);
                    ensureDefault($scope.options, "isLeaf", defaultIsLeaf);
                    ensureDefault($scope.options, "allowDeselect", true);
                    ensureDefault($scope.options, "isSelectable", defaultIsSelectable);

                    $scope.selectedNodes = $scope.selectedNodes || [];
                    $scope.expandedNodes = $scope.expandedNodes || [];
                    $scope.expandedNodesMap = {};
                    for (var i = 0; i < $scope.expandedNodes.length; i++) {
                        $scope.expandedNodesMap["" + i] = $scope.expandedNodes[i];
                    }
                    $scope.parentScopeOfTree = $scope.$parent;


                    function isSelectedNode(node) {
                        if (!$scope.options.multiSelection && ($scope.options.equality(node, $scope.selectedNode)))
                            return true;
                        else if ($scope.options.multiSelection && $scope.selectedNodes) {
                            for (var i = 0;
                                (i < $scope.selectedNodes.length); i++) {
                                if ($scope.options.equality(node, $scope.selectedNodes[i])) {
                                    return true;
                                }
                            }
                            return false;
                        }
                    }

                    $scope.headClass = function(node) {
                        var liSelectionClass = classIfDefined($scope.options.injectClasses.liSelected, false);
                        var injectSelectionClass = "";
                        if (liSelectionClass && isSelectedNode(node))
                            injectSelectionClass = " " + liSelectionClass;
                        if ($scope.options.isLeaf(node))
                            return "tree-leaf" + injectSelectionClass;
                        if ($scope.expandedNodesMap[this.$id])
                            return "tree-expanded" + injectSelectionClass;
                        else
                            return "tree-collapsed" + injectSelectionClass;
                    };

                    $scope.iBranchClass = function() {
                        if ($scope.expandedNodesMap[this.$id])
                            return classIfDefined($scope.options.injectClasses.iExpanded);
                        else
                            return classIfDefined($scope.options.injectClasses.iCollapsed);
                    };

                    $scope.nodeExpanded = function() {
                        return !!$scope.expandedNodesMap[this.$id];
                    };

                    $scope.selectNodeHead = function() {
                        var transcludedScope = this;
                        var expanding = $scope.expandedNodesMap[transcludedScope.$id] === undefined;
                        $scope.expandedNodesMap[transcludedScope.$id] = (expanding ? transcludedScope.node : undefined);
                        if (expanding) {
                            $scope.expandedNodes.push(transcludedScope.node);
                        } else {
                            var index;
                            for (var i = 0;
                                (i < $scope.expandedNodes.length) && !index; i++) {
                                if ($scope.options.equality($scope.expandedNodes[i], transcludedScope.node)) {
                                    index = i;
                                }
                            }
                            if (index !== undefined)
                                $scope.expandedNodes.splice(index, 1);
                        }
                        if ($scope.onNodeToggle) {
                            var parentNode = (transcludedScope.$parent.node === transcludedScope.synteticRoot) ? null : transcludedScope.$parent.node;
                            $scope.onNodeToggle({
                                node: transcludedScope.node,
                                $parentNode: parentNode,
                                $index: transcludedScope.$index,
                                $first: transcludedScope.$first,
                                $middle: transcludedScope.$middle,
                                $last: transcludedScope.$last,
                                $odd: transcludedScope.$odd,
                                $even: transcludedScope.$even,
                                expanded: expanding
                            });

                        }
                    };

                    $scope.selectNodeLabel = function(selectedNode) {
                        var transcludedScope = this;
                        if (!$scope.options.isLeaf(selectedNode) && (!$scope.options.dirSelectable || !$scope.options.isSelectable(selectedNode))) {
                            // Branch node is not selectable, expand
                            this.selectNodeHead();
                        } else if ($scope.options.isLeaf(selectedNode) && (!$scope.options.isSelectable(selectedNode))) {
                            // Leaf node is not selectable
                            return;
                        } else {
                            var selected = false;
                            if ($scope.options.multiSelection) {
                                var pos = -1;
                                for (var i = 0; i < $scope.selectedNodes.length; i++) {
                                    if ($scope.options.equality(selectedNode, $scope.selectedNodes[i])) {
                                        pos = i;
                                        break;
                                    }
                                }
                                if (pos === -1) {
                                    $scope.selectedNodes.push(selectedNode);
                                    selected = true;
                                } else {
                                    $scope.selectedNodes.splice(pos, 1);
                                }
                            } else {
                                if (!$scope.options.equality(selectedNode, $scope.selectedNode)) {
                                    $scope.selectedNode = selectedNode;
                                    selected = true;
                                } else {
                                    if ($scope.options.allowDeselect) {
                                        $scope.selectedNode = undefined;
                                    } else {
                                        $scope.selectedNode = selectedNode;
                                        selected = true;
                                    }
                                }
                            }
                            if ($scope.onSelection) {
                                var parentNode = (transcludedScope.$parent.node === transcludedScope.synteticRoot) ? null : transcludedScope.$parent.node;
                                $scope.onSelection({
                                    node: selectedNode,
                                    selected: selected,
                                    $parentNode: parentNode,
                                    $index: transcludedScope.$index,
                                    $first: transcludedScope.$first,
                                    $middle: transcludedScope.$middle,
                                    $last: transcludedScope.$last,
                                    $odd: transcludedScope.$odd,
                                    $even: transcludedScope.$even
                                });
                            }
                        }
                    };

                    $scope.selectedClass = function() {
                        var isThisNodeSelected = isSelectedNode(this.node);
                        var labelSelectionClass = classIfDefined($scope.options.injectClasses.labelSelected, false);
                        var injectSelectionClass = "";
                        if (labelSelectionClass && isThisNodeSelected)
                            injectSelectionClass = " " + labelSelectionClass;

                        return isThisNodeSelected ? "tree-selected" + injectSelectionClass : "";
                    };

                    $scope.unselectableClass = function() {
                        var isThisNodeUnselectable = !$scope.options.isSelectable(this.node);
                        var labelUnselectableClass = classIfDefined($scope.options.injectClasses.labelUnselectable, false);
                        return isThisNodeUnselectable ? "tree-unselectable " + labelUnselectableClass : "";
                    };

                    //tree template
                    $scope.isReverse = function() {
                        return !($scope.reverseOrder === 'false' || $scope.reverseOrder === 'False' || $scope.reverseOrder === '' || $scope.reverseOrder === false);
                    };

                    $scope.orderByFunc = function() {
                        return "'" + $scope.orderBy + "'";
                    };

                    var templateOptions = {
                        orderBy: $scope.orderBy ? " | orderBy:orderByFunc():isReverse()" : '',
                        ulClass: classIfDefined($scope.options.injectClasses.ul, true),
                        nodeChildren: $scope.options.nodeChildren,
                        liClass: classIfDefined($scope.options.injectClasses.li, true),
                        iLeafClass: classIfDefined($scope.options.injectClasses.iLeaf, false),
                        labelClass: classIfDefined($scope.options.injectClasses.label, false)
                    };

                    var template;
                    var templateUrl = $scope.options.templateUrl || treeConfig.templateUrl;

                    if (templateUrl) {
                        template = $templateCache.get(templateUrl);
                    }

                    if (!template) {
                        template =
                            '<ul {{options.ulClass}} >' +
                            '<li ng-repeat="node in node.{{options.nodeChildren}} | filter:filterExpression:filterComparator {{options.orderBy}}" ng-class="headClass(node)" {{options.liClass}}' +
                            'set-node-to-data>' +
                            '<i class="fa fa-folder" ng-class="iBranchClass()" ng-click="selectNodeHead(node)"></i>' +
                            '<i class="tree-leaf-head {{options.iLeafClass}}"></i>' +
                            '<div class="tree-label {{options.labelClass}}" ng-class="[selectedClass(), unselectableClass()]" ng-click="selectNodeLabel(node)" tree-transclude></div>' +
                            '<treeitem ng-if="nodeExpanded()"></treeitem>' +
                            '</li>' +
                            '</ul>';
                    }

                    this.template = $compile($interpolate(template)({
                        options: templateOptions
                    }));
                }],
                compile: function(element, attrs, childTranscludeFn) {
                    return function(scope, element, attrs, treemodelCntr) {

                        scope.$watch("treeModel", function updateNodeOnRootScope(newValue) {
                            if (angular.isArray(newValue)) {
                                if (angular.isDefined(scope.node) && angular.equals(scope.node[scope.options.nodeChildren], newValue))
                                    return;
                                scope.node = {};
                                scope.synteticRoot = scope.node;
                                scope.node[scope.options.nodeChildren] = newValue;
                            } else {
                                if (angular.equals(scope.node, newValue))
                                    return;
                                scope.node = newValue;
                            }
                        });

                        scope.$watchCollection('expandedNodes', function(newValue, oldValue) {
                            var notFoundIds = 0;
                            var newExpandedNodesMap = {};
                            var $liElements = element.find('li');
                            var existingScopes = [];
                            // find all nodes visible on the tree and the scope $id of the scopes including them
                            angular.forEach($liElements, function(liElement) {
                                var $liElement = angular.element(liElement);
                                var liScope = {
                                    $id: $liElement.data('scope-id'),
                                    node: $liElement.data('node')
                                };
                                existingScopes.push(liScope);
                            });
                            // iterate over the newValue, the new expanded nodes, and for each find it in the existingNodesAndScopes
                            // if found, add the mapping $id -> node into newExpandedNodesMap
                            // if not found, add the mapping num -> node into newExpandedNodesMap
                            angular.forEach(newValue, function(newExNode) {
                                var found = false;
                                for (var i = 0;
                                    (i < existingScopes.length) && !found; i++) {
                                    var existingScope = existingScopes[i];
                                    if (scope.options.equality(newExNode, existingScope.node)) {
                                        newExpandedNodesMap[existingScope.$id] = existingScope.node;
                                        found = true;
                                    }
                                }
                                if (!found)
                                    newExpandedNodesMap[notFoundIds++] = newExNode;
                            });
                            scope.expandedNodesMap = newExpandedNodesMap;
                        });

                        //                        scope.$watch('expandedNodesMap', function(newValue) {
                        //
                        //                        });

                        //Rendering template for a root node
                        treemodelCntr.template(scope, function(clone) {
                            element.html('').append(clone);
                        });
                        // save the transclude function from compile (which is not bound to a scope as apposed to the one from link)
                        // we can fix this to work with the link transclude function with angular 1.2.6. as for angular 1.2.0 we need
                        // to keep using the compile function
                        scope.$treeTransclude = childTranscludeFn;
                    };
                }
            };
        }])
        .directive("setNodeToData", ['$parse', function($parse) {
            return {
                restrict: 'A',
                link: function($scope, $element, $attrs) {
                    $element.data('node', $scope.node);
                    $element.data('scope-id', $scope.$id);
                }
            };
        }])

    .directive("treeitem", function() {
            return {
                restrict: 'E',
                require: "^treecontrol",
                link: function(scope, element, attrs, treemodelCntr) {
                    // Rendering template for the current node
                    treemodelCntr.template(scope, function(clone) {
                        element.html('').append(clone);
                    });
                }
            };
        })
        .directive("treeTransclude", function() {
            return {
                link: function(scope, element, attrs, controller) {
                    if (!scope.options.isLeaf(scope.node)) {
                        angular.forEach(scope.expandedNodesMap, function(node, id) {
                            if (scope.options.equality(node, scope.node)) {
                                scope.expandedNodesMap[scope.$id] = scope.node;
                                scope.expandedNodesMap[id] = undefined;
                            }
                        });
                    }
                    if (!scope.options.multiSelection && scope.options.equality(scope.node, scope.selectedNode)) {
                        scope.selectedNode = scope.node;
                    } else if (scope.options.multiSelection) {
                        var newSelectedNodes = [];
                        for (var i = 0;
                            (i < scope.selectedNodes.length); i++) {
                            if (scope.options.equality(scope.node, scope.selectedNodes[i])) {
                                newSelectedNodes.push(scope.node);
                            }
                        }
                        scope.selectedNodes = newSelectedNodes;
                    }

                    // create a scope for the transclusion, whos parent is the parent of the tree control
                    scope.transcludeScope = scope.parentScopeOfTree.$new();
                    scope.transcludeScope.node = scope.node;
                    scope.transcludeScope.$parentNode = (scope.$parent.node === scope.synteticRoot) ? null : scope.$parent.node;
                    scope.transcludeScope.$index = scope.$index;
                    scope.transcludeScope.$first = scope.$first;
                    scope.transcludeScope.$middle = scope.$middle;
                    scope.transcludeScope.$last = scope.$last;
                    scope.transcludeScope.$odd = scope.$odd;
                    scope.transcludeScope.$even = scope.$even;
                    scope.$on('$destroy', function() {
                        scope.transcludeScope.$destroy();
                    });

                    scope.$treeTransclude(scope.transcludeScope, function(clone) {
                        element.empty();
                        element.append(clone);
                    });
                }
            };
        });
})();

(
function () {
  'use strict';
  mcs.ng
.directive('phone', function () {
  return {
    restrict: 'A',
    link: function (scope, iElement, iAttrs) {

    }
  };
})

.directive('email', function () {

  return {
    restrict: 'A',
    link: function ($scope, iElm, iAttrs, controller) {

    }
  };
})

.directive('personCode', function () {
  return {
    restrict: 'A',
    link: function (scope, iElement, iAttrs) {

    }
  };
});

}

)();
(
  
function () {
  'use strict';  
  
  mcs.ng   
    
    .service('validateService', function () {
      var validateService = this;
      validateService.ruleSet = {
        phone: {
          required: true,
          message: 'phone number valid',
          patternDescription: 'phone number should be 15 bit',
          pattern: function (value) {
            if (!angular.isNumber(parseInt(value)) || value.length !=15) {
            return flase;
            }
            return true;
          }

        }
      }
    })
    .service('validateMessageService', function ($dialogs) {
      var validMessageService = this;
      validMessageService.processMessage = function (message, messageTargetElement) {
        if (messageTargetElement) {
          messageTargetElement.innerText = message.content;
        } else {
          $dialogs.error(message.title, message.content);
        }
      }

      
    });
}
)();