define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.studentAssignmentDataService],
        function (customer) {
            customer.registerController('stuAsgmtListController', [
                '$scope', '$state', 'dataSyncService', 'studentassignmentDataService',
                function ($scope, $state, dataSyncService, studentassignmentDataService) {
                    var vm = this;

                    //vm.dataContent = "页面固定的内容";

                   
                    //studentassignmentDataService.getAllStuUnAsgmt(
                    //    function (result) {
                    //        vm.dataContent = result;
                    //        var temp = 0;
                    //    },
                    //    function (error) {
                    //        vm.dataContent = error;
                    //    }
                    //    );

                   vm.data = {
                        selection: 'checkbox',
                        headers: [{
                            field: "customerName",
                            name: "学员姓名",
                            template: '<a ui-sref="ppts.customer-view({id:row.customerID,page:\'info\'})">{{row.customerName}}</a>',
                            sortable: true
                        }, {
                            field: "customerCode",
                            name: "学员编号",
                            template: '<a ui-sref="ppts.customer-view({id:row.customerID,page:\'info\'})">{{row.customerCode}}</a>'
                        },
                        //{
                        //    field: "parentName",
                        //    name: "性别"
                        //}, {
                        //    field: "createTime",
                        //    name: "出生日期",
                        //    template: '<span>{{row.createTime | date:"yyyy-MM-dd"}}</span>'
                        //}, {
                        //    field: "sourceMainType",
                        //    name: "在读学校",
                        //    template: '<span>{{row.sourceMainType}}</span>'
                        //}, {
                        //    field: "sourceMainType",
                        //    name: "当前年级",
                        //    template: '<span>{{row.sourceMainType}}</span>'
                        //}, {
                        //    field: "sourceMainType",
                        //    name: "班主任",
                        //    template: '<span>{{row.sourceMainType}}</span>'
                        //},
                        {
                            field: "remainAmount",
                            name: "剩余数量",
                            template: '<span>{{row.remainAmount}}</span>'
                        }],
                        pager: {
                            pageIndex: 1,
                            pageSize: 10,
                            totalCount: -1,
                            pageChange: function () {
                                dataSyncService.initCriteria(vm);
                                customerDataService.getPagedCustomers(vm.criteria, function (result) {
                                    vm.data.rows = result.pagedData;
                                });
                            }
                        },
                        orderBy: [{ dataField: 'CreateTime', sortDirection: 1 }]
                    }

                    // 页面初始化加载或重新搜索时查询
                   vm.init = function () {
                        dataSyncService.initCriteria(vm);
                        customerDataService.getAllStuUnAsgmt(vm.criteria, function (result) {
                            vm.data.rows = result.queryResult.pagedData;
                            dataSyncService.injectDictData();
                            dataSyncService.updateTotalCount(vm, result.queryResult);
                            //$scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.init();

                    // 翻页/排序实现
                    //vm.query = function () {
                    //    dataSyncService.initCriteria(vm);
                    //    customerDataService.getPagedCustomers(vm.criteria, function (result) {
                    //        vm.data.rows = result.pagedData;
                    //    });
                    //};

                    /** 关闭条件搜索项
                    vm.close = function (category, dictionary) {
                        vm.criteria[category].length = 0;

                        vm.dictionaries[dictionary].forEach(function (item, value) {
                            item.checked = false;
                        });
                    };**/
                }]);
        });