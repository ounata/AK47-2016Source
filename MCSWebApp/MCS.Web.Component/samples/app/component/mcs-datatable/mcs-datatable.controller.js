(function() {
    angular.module('app.component').controller('MCSDatatableController', [
        '$scope', '$q', '$timeout', 'printService', '$http', 'excelImportService',
        function($scope, $q, $timeout, printService, $http, excelImportService) {
            var vm = this;

            vm.reorder = function(field, direction) {
                var a = direction;

            };

            vm.importExcel = function() {
                excelImportService.import({
                    id: 2,
                    name: 'tom',
                    birthday: 29
                });
            };



            vm.exportExcel = function() {

                mcs.util.postMockForm('http://localhost/MCSWebApp/MCS.Web.API/api/Sample/ExportAllBooksPost', {

                    name: 'tom',
                    birthday: new Date(2011, 12, 23, 12, 14, 15, 33)

                });
            }

            vm.print = function() {
                printService.print();
            }

            vm.removeRows = function() {
                mcs.util.removeByObjectsWithKeys(vm.page1Data, vm.data.rowsSelected);
            }

            vm.show = function(data) {
                alert(data);
            }

            vm.clearSelections = function() {

                if (vm.data.rowsSelected.length == 0) {
                    alert('你没有选任何东东');
                    return;
                }

                alert('之前的选择为：' + vm.data.rowsSelected.map(function(cur) {
                    return cur.customerId;
                }));


                vm.data.rowsSelected = [];

                vm.page1Data.forEach(function(rowData) {
                    rowData.selected = false;
                });

                vm.page2Data.forEach(function(rowData) {
                    rowData.selected = false;
                });


            }

            vm.currentPageIndex = 1;

            vm.pageChange = function() {
                var deferred = $q.defer();

                $timeout(function() {
                    if (vm.currentPageIndex == 1) {
                        vm.currentPageIndex = 2;

                        vm.data.rows = vm.page2Data;
                    } else {
                        vm.currentPageIndex = 1;
                        vm.data.rows = vm.page1Data;
                    }

                    deferred.resolve();


                }, 2000);



                return deferred.promise;


                //alert('跳转到第' + vm.data.pager.pageIndex + '页,按照' + vm.data.orderBy[0].sortField + '排序顺序:' + vm.data.orderBy[0].direction);
            }

            vm.editRow = function(rowData) {
                rowData.customerName = "JSSON";
            }

            vm.addRow = function() {
                vm.data.rows.push({
                    customerId: 2,
                    parentId: 22,
                    customerName: 'jack',
                    age: 17,
                    money: 28

                });
            }

            vm.removeRow = function() {
                vm.data.rows.shift();
            }

            vm.totalAge = function() {
                var total = 0;

                vm.data.rows.forEach(function(row) {
                    total += parseInt(row.age) || 0;
                });

                return total;
            }


            vm.page1Data = [{
                    customerId: 1,
                    parentId: 11,
                    customerName: 'tom',
                    age: 10,
                    money: 12



                }, {
                    customerId: 2,
                    parentId: 22,
                    customerName: 'jack',
                    age: 15,
                    money: 28

                }, {
                    customerId: 3,
                    parentId: 11,
                    customerName: 'tom',
                    age: 88,
                    money: 12


                }, {
                    customerId: 4,
                    parentId: 22,
                    customerName: 'jack',
                    age: 56,
                    money: 28

                }, {
                    customerId: 5,
                    parentId: 11,
                    customerName: 'tom',
                    age: 33,
                    money: 12


                }, {
                    customerId: 6,
                    parentId: 22,
                    customerName: 'jack',
                    age: 19,
                    money: 28

                }



            ];

            vm.page2Data = [{
                    customerId: 7,
                    parentId: 11,
                    customerName: 'zhangsan',
                    age: 13,
                    money: 12



                }, {
                    customerId: 8,
                    parentId: 22,
                    customerName: 'lisi',
                    age: 65,
                    money: 28

                }, {
                    customerId: 9,
                    parentId: 11,
                    customerName: 'wangwu',
                    age: 8,
                    money: 12


                }, {
                    customerId: 10,
                    parentId: 32,
                    customerName: 'zhaoliu',
                    age: 56,
                    money: 28

                }, {
                    customerId: 11,
                    parentId: 11,
                    customerName: 'maqi',
                    age: 3,
                    money: 12


                }, {
                    customerId: 12,
                    parentId: 22,
                    customerName: 'houba',
                    age: 19,
                    money: 28

                }



            ];

            vm.dataNoPaging = {
                selection: 'checkbox',
                rowsSelected: [],
                keyFields: ['customerId'],
                orderBy: [{
                    dataField: 'money',
                    sortDirection: 1
                }],
                headers: [{
                        field: "customerName",
                        name: "姓名",
                        headerCss: 'mcs-datatable-column-currency',
                        sortable: false,
                        description: 'customer name'
                    },

                    {
                        field: "money",
                        name: "账户",
                        headerCss: 'mcs-datatable-column-number',
                        template: '<span ng-click="vm.show(row.money)" >click me!!{{row.money|currency}}</span>',
                        sortable: true,
                        description: 'customer account'
                    },

                    {
                        field: 'age',
                        name: '年龄',

                        sortable: false,
                        description: 'age'

                    }
                ],
                rows: [{
                        customerId: 1,
                        parentId: 11,
                        customerName: 'tom',
                        age: 10,
                        money: 12



                    }, {
                        customerId: 2,
                        parentId: 22,
                        customerName: 'jack',
                        age: 15,
                        money: 28

                    }, {
                        customerId: 3,
                        parentId: 11,
                        customerName: 'tom',
                        age: 88,
                        money: 12


                    }, {
                        customerId: 4,
                        parentId: 22,
                        customerName: 'jack',
                        age: 19,
                        money: 28

                    }



                ],
                pager: {

                    pagable: false,

                }
            };

            vm.data = {
                selection: 'checkbox',
                rowsSelected: [],
                keyFields: ['customerId'],
                orderBy: [{
                    dataField: 'money',
                    sortDirection: 1
                }],
                headers: [{
                        field: "customerName",
                        name: "姓名",
                        headerCss: 'mcs-datatable-column-currency',
                        sortable: false,
                        description: 'customer name'
                    },

                    {
                        field: "money",
                        name: "账户",
                        headerCss: 'mcs-datatable-column-number',
                        template: '<span>{{row.money|currency}}</span>',
                        sortable: true,
                        description: 'customer account'
                    },

                    {
                        field: 'age',
                        name: '年龄',
                        template: '<input type="text" ng-model="row.age" />',
                        sortable: false,
                        description: 'age'

                    },

                    {
                        name: "操作1",
                        template: '<a class="btn" ng-click="vm.editRow(row)">hello</a>'
                    }, {
                        name: "操作2",
                        template: '<a>hello</a>'
                    }
                ],
                rows: vm.page1Data,
                pager: {

                    pagable: true,
                    pageIndex: 1,
                    pageSize: 6,
                    totalCount: 12,
                    pageChange: vm.pageChange
                }
            }
        }
    ]);
})();
