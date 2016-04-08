define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.customerDataService],
        function (customer) {
            customer.registerController('customerListController', [
                '$scope', '$state', 'dataSyncService', 'customerDataService',
                function ($scope, $state, dataSyncService, customerDataService) {
                    var vm = this;

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
                        }, {
                            field: "parentName",
                            name: "家长姓名"
                        }, {
                            field: "grade",
                            name: "当前年级",
                            template: '<span>{{row.entranceGrade | grade}}</span>'
                        }, {
                            field: "sourceMainType",
                            name: "信息来源",
                            template: '<span>{{row.sourceMainType | source}}</span>'
                        }, {
                            name: "归属地",
                            template: '<span></span>'
                        }, {
                            field: "createTime",
                            name: "建档日期",
                            template: '<span>{{row.createTime | date:"yyyy-MM-dd"}}</span>'
                        }, {
                            field: "createTime",
                            name: "建档人",
                            template: '<span>{{row.creatorName}}</span>',
                            description: 'customer creatorName'
                        }, {
                            name: "建档人岗位",
                            template: '<span></span>'
                        }, {
                            name: "归属咨询师",
                            template: '<span></span>'
                        }, {
                            name: "跟进次数",
                            template: '<span></span>'
                        }, {
                            field: "lastFollowupTime",
                            name: "最后一次跟进时间",
                            template: '<span>{{row.lastFollowupTime | date:"yyyy-MM-dd"}}</span>',
                            description: 'customer followTime'
                        }, {
                            name: "归属市场专员",
                            template: '<span></span>'
                        }, {
                            name: "跟进阶段",
                            template: '<span></span>'
                        }, {
                            field: "vipLevel",
                            name: "客户级别",
                            template: '<span>{{row.vipLevel | vipLevel}}</span>',
                            description: 'customer vipLevel'
                        }, {
                            name: "已签约金额",
                            template: '<span></span>'
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
                        customerDataService.getAllCustomers(vm.criteria, function (result) {
                            vm.data.rows = result.queryResult.pagedData;
                            dataSyncService.injectDictData({
                                c_codE_ABBR_Customer_Assign: [{ key: 0, value: '未分配' }, { key: 1, value: '已分配' }],
                                c_codE_ABBR_Customer_Valid: [{ key: 0, value: '无效客户' }, { key: 1, value: '有效客户' }]
                            });
                            dataSyncService.updateTotalCount(vm, result.queryResult);
                            $scope.$broadcast('dictionaryReady');
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