// 依赖于mcs.js, angularjs, 将会在应用程序启动之后执行
(function () {
    'use strict';

    mcs.ng = mcs.ng || angular.module('mcs.ng', ['mcs.ng.datatable', 'mcs.ng.treeControl', 'mcs.ng.paging', 'dialogs.main']);
    mcs.ng.constant('mcsComponentConfig', {
        rootUrl: mcs.app.config.componentBaseUrl
    })


    .constant('httpErrorHandleMessage', {
        '404': 'no file!',
        '401': 'unauthenticated access!',
        'other': ''
    })

    .service('httpErrorHandleService', function (httpErrorHandleMessage, dialogs) {

        var httpErrorHandleService = this;
        httpErrorHandleService.process = function (response) {
            dialogs.error('error', httpErrorHandleMessage[response.StatusCode]);
        }

        return this;

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

                function findoutRowFromSelectedSet(targetRow) {
                    $scope.data.rowsSelected.forEach(function(row) {

                    })
                }

                $scope.rowSelect = function(rowSelected) {
                    $scope.data.selectAll = false;
                    if ($scope.data.selection == 'radio') {
                        var rowDataSelected = $scope.data.rowsSelected[0] = $scope.data.rowsSelected[0] || {};

                        $scope.data.keyFields.forEach(function(key) {
                            rowDataSelected[key] = rowSelected[key];
                        });



                        rowSelected.selected = true;
                        $scope.data.rows.forEach(function(row) {
                            if (rowSelected[$scope.data.keyFields[0]] != row[$scope.data.keyFields[0]]) {

                                row.selected = false;
                            }
                        })

                    } else {

                        var rowDataSelected = {};
                        $scope.data.keyFields.forEach(function(key) {
                            rowDataSelected[key] = rowSelected[key];
                        });

                        if (rowSelected.selected) {

                            $scope.data.rowsSelected.push(rowDataSelected);
                        } else {
                            mcs.util.removeByObject($scope.data.rowsSelected, rowDataSelected);
                        }

                    }
                }

                $scope.pageChange = function(callback) {
                    callback();
                    $scope.reMatchRowsSelected();
                }

                $scope.reMatchRowsSelected = function() {

                    if ($scope.data.rowsSelected.length == 0) {
                        return;
                    }

                    for (var rowDataSelected in $scope.data.rowsSelected) {
                        for (var row in $scope.data.rows) {
                            for (var key in $scope.data.keyFields) {
                                if (row[key] !== rowDataSelected[key]) {
                                    row.selected = false;
                                    break;
                                }

                                if (row.selected == undefined) {
                                    row.selected = true;
                                    break;
                                }
                            }
                        }
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
                            $scope.reMatchRowsSelected();
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
                    var td = $compile('<td class="' + header.headerCss +
                        '">' + (header.template || ('<span>{{row.' + header.field + '}}</span>')) + '</td>')(scope);
                    iElement.append(td);
                });

            }
        };
    });
})();

(function () {
        'use strict';

        mcs.ng.service('mcsDialogService', ['dialogs', function (dialogs) {
            var mcsDialogService = this;
            mcsDialogService.messageConfig = {
                wait: {
                    title: '操作中',
                    message: '操作进行中，请稍后！',
                },
                error: {
                    title: '错误',
                    message: '操作发生错误，请重试或联系系统管理员！',
                },
                confirm: {
                    title: '请确认',
                    message: '确认进行此操作吗？'
                }

            };
            this.wait = function (title, msg, opts) {
                return dialogs.wait(title || mcsDialogService.messageConfig.wait.title, msg || mcsDialogService.messageConfig.wait.message);
            }

            this.error = function (title, msg, opts) {
                return dialogs.error(title || mcsDialogService.messageConfig.error.title, msg || mcsDialogService.messageConfig.error.message);
            }


            this.confirm = function (title, msg, opts) {
                return dialogs.confirm(title || mcsDialogService.messageConfig.confirm.title, msg || mcsDialogService.messageConfig.confirm.message);

            }

            this.create = function (url, ctrlr, data, opts) {
                return dialogs.create(url, ctrlr, data, opts);
            }


        }]);
    }

)();
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
(function () {
    'use strict';

    mcs.ng.directive('mcsSelect', function () {

        return {
            restrict: 'E',
            scope: {
                data: '=',
                model: '=',
                caption: '@'
            },

            templateUrl: mcs.app.config.componentBaseUrl + '/src/tpl/mcs-select.tpl.html',
            link: function ($scope, $elem, $attrs, $ctrl) {
                
            }
        }
    });
})();
(function () {
    'use strict';

    angular.module('mcs.ng.treeControl', [])
        .constant('treeSetting', {
            data: {
                key: {
                    checked: 'checked',
                    children: 'children',
                    name: 'name',
                    title: 'name'
                },
                simpleData: {
                    enable: true
                }
            },
            view: {
                selectedMulti: true,
                showIcon: true,
                showLine: true,
                nameIsHTML: false,
                fontCss: {

                }

            },
            check: {
                enable: true
            },
            async: {
                enable: true,
                autoParam: ["id"],
                contentType: "application/json",
                type: 'post',
                dataType: "json",
                url: ''
            }

        })
        .directive('mcsTree', function (treeSetting, $http) {



            return {
                restrict: 'A',
                scope: {
                    setting: '=mcsTree',
                    nodes: '='
                },

                link: function ($scope, iElm, iAttrs, controller) {
                    angular.extend($scope.setting, treeSetting);
                    if ($scope.nodes) {
                        $.fn.zTree.init(iElm, $scope.setting, $scope.nodes);
                    } else {
                        if (setting.async.url) {
                            $http.post(setting.async.url, {
                                id: null
                            }).success(function (data) {
                                $.fn.zTree.init(iElm, setting, data.Data.List);
                            })

                        }

                    }

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