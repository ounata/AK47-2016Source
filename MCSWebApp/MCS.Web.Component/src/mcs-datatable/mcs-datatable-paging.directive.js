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
