﻿define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.studentAssignmentDataService],
        function (customer) {
            customer.registerController('stuAsgmtListController', [
                '$scope', '$state', 'dataSyncService', 'studentassignmentDataService',
                function ($scope, $state, dataSyncService, studentassignmentDataService) {
                    var vm = this;
                    vm.data = {
                        selection: 'checkbox',
                        headers: [{
                            field: "customerName",
                            name: "学员姓名",
                            template: '<span>{{row.customerName}}</span>',
                            sortable: true
                        }, {
                            field: "customerCode",
                            name: "学员编号",   
                            template: '<span>{{row.customerCode}}</span>'
                        },{
                            field: "gender",
                            name: "性别",
                            template: '<span>{{row.gender | gender }}</span>'
                        }, {
                            field: "birthday",
                            name: "出生日期",
                            template: '<span>{{row.birthday | date:"yyyy-MM-dd"}}</span>'
                        }, {
                            field: "schoolName",
                            name: "在读学校",
                            template: '<span>{{row.schoolName}}</span>'
                        }, {
                            field: "grade",
                            name: "当前年级",
                            template: '<span>{{row.grade | grade}}</span>'
                        }, {
                            field: "educatorName",
                            name: "学管师",
                            template: '<span>{{row.educatorName}}</span>'
                        }, {
                            field: "remainOne2Ones",
                            name: "剩余数量",
                            template: '<span>{{row.remainOne2Ones}}</span>'
                        }],
                        pager: {
                            pageIndex: 1,
                            pageSize: 10,
                            totalCount: -1,
                            pageChange: function () {
                                dataSyncService.initCriteria(vm);
                                studentassignmentDataService.getPagedStuUnAsgmt(vm.criteria, function (result) {
                                    vm.data.rows = result.pagedData;
                                });
                            }
                        },
                        orderBy: [{ dataField: 'CustomerCode', sortDirection: 1 }]
                    }

                    // 页面初始化加载或重新搜索时查询
                    vm.init = function () {
                        dataSyncService.initCriteria(vm);
                        studentassignmentDataService.getAllStuUnAsgmt(vm.criteria, function (result) {
                            vm.data.rows = result.queryResult.pagedData;
                            dataSyncService.injectDictData();
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