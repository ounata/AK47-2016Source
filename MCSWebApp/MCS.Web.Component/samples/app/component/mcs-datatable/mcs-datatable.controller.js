(function() {
    angular.module('app.component').controller('MCSDatatableController', [
        '$scope',
        function($scope) {
            var vm = this;

            vm.reorder = function(field, direction) {
                var a = direction;

            };

            vm.pageChange = function() {
                alert('跳转到第' + vm.data.pager.pageIndex + '页,按照' + vm.data.orderBy[0].sortField + '排序顺序:' + vm.data.orderBy[0].direction);
            }


            vm.data = {
                selection: 'radio',
                keyField: 'customerId',
                orderBy: [{
                    dataField: 'customerAge',
                    sortDirection: 1
                }],
                headers: [{
                        field: "customerName",
                        name: "姓名",
                        headerCss: 'datatable-header-align-right',
                        sortable: false,
                        template: '<input type="text" ng-model="row.customerName" />',
                        description: 'customer name'
                    },

                    {
                        field: "customerAge",
                        name: "年龄",
                        headerCss: 'datatable-header-align-right',
                        template: '<span>{{row.customerAge|currency}}</span>',
                        sortable: true,
                        description: 'customer age'
                    },

                    {
                        name: "操作1",
                        template: '<a class="btn" ng-click="editRow(row)">hello</a>'
                    }, {
                        name: "操作2",
                        template: '<a>hello</a>'
                    }
                ],
                rows: [{
                        customerId: 1,
                        parentId: 11,
                        customerName: 'tom',
                        customerAge: 12


                    }, {
                        customerId: 2,
                        parentId: 22,
                        customerName: 'jack',
                        customerAge: 28

                    }, {
                        customerId: 3,
                        parentId: 11,
                        customerName: 'tom',
                        customerAge: 12


                    }, {
                        customerId: 4,
                        parentId: 22,
                        customerName: 'jack',
                        customerAge: 28

                    }, {
                        customerId: 5,
                        parentId: 11,
                        customerName: 'tom',
                        customerAge: 12


                    }, {
                        customerId: 6,
                        parentId: 22,
                        customerName: 'jack',
                        customerAge: 28

                    }



                ],
                pager: {

                    pagesLength: 15,


                    pageIndex: 1,
                    pageSize: 10,
                    totalCount: 30,
                    pageChange: vm.pageChange
                }
            }
        }
    ]);
})();
