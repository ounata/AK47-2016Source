﻿define([ppts.config.modules.customer,
    ppts.config.dataServiceConfig.customerDataService,
        ppts.config.dataServiceConfig.studentDataService],
        function (customer) {
            customer.registerController('studentListController', [
                '$scope',
                '$state',
                'utilService',
                'dataSyncService',
                'studentDataService',
                'studentDataViewService',
                'studentAdvanceSearchItems',
                'mcsDialogService',
                'storage',
                'customerDataViewService',
                'customerRelationType',
                function ($scope, $state, util, dataSyncService, studentDataService, studentDataViewService, searchItems, mcsDialogService, storage, customerDataViewService, relationType) {
                    var vm = this;
                    // 获取学员与员工关系
                    vm.relationType = relationType;
                    // 配置数据表头
                    studentDataViewService.configStudentListHeaders(vm);

                    // 页面初始化加载或重新搜索时查询
                    vm.search = function () {
                        studentDataViewService.initStudentList(vm, function () {
                            vm.searchItems = searchItems;
                        });
                        studentDataViewService.initDateRange($scope, vm, [
                                { watchExp: 'vm.selectedAttendRange', selectedValue: 'selectedAttendRange', start: 'attendStartTime', end: 'attendEndTime' },
                                { watchExp: 'vm.selectedRange', selectedValue: 'selectedRange', start: 'statusStartTime', end: 'statusEndTime' }
                        ]);
                    };
                    vm.search();

                    // 订购
                    vm.purchase = function (item) {
                        if (util.selectOneRow(vm)) {
                            var params = {
                                customerId: vm.data.rowsSelected[0].customerID,
                                campusId: vm.data.rowsSelected[0].campusID,
                                //prev: 'ppts.student'
                            };
                            switch (item.text) {
                                case '常规订购':
                                    params.type = 1;
                                    params.grade = vm.data.rowsSelected[0].grade;
                                    break;
                                case '买赠订购':
                                    params.type = 2;
                                    params.grade = vm.data.rowsSelected[0].grade;
                                    break;
                            }
                            $state.go(item.route, params);
                        }
                    };

                    //分配教师/咨询师/学管师
                    vm.assign = function (item) {
                        if (util.selectMultiRows(vm)) {
                            switch (item.text) {
                                case '分配咨询师':
                                    customerDataViewService.assignStaffRelation(vm.data.rowsSelected, vm.relationType.consultant, vm);
                                    break;
                                case '分配学管师':
                                    customerDataViewService.assignStaffRelation(vm.data.rowsSelected, vm.relationType.educator, vm);
                                    break;
                                case '分配教师':
                                    mcsDialogService.create('app/customer/student/student-assignTeacher/student-assignTeacher.html', {
                                        controller: 'assignTeacherController',
                                        params: { customers: vm.data.rowsSelected[0] },
                                        settings: { size: 'lg' }
                                    });
                                    break;
                            }
                        }
                    };

                    vm.products = [
                        { text: '常规订购', route: 'ppts.purchaseProduct', click: vm.purchase },
                        { text: '插班订购', route: 'ppts.purchaseClassGroupList', click: vm.purchase },
                        { text: '买赠订购', route: 'ppts.purchaseProduct', click: vm.purchase }
                    ];

                    vm.relations = [
                        { text: '分配咨询师', click: vm.assign },
                        { text: '分配学管师', click: vm.assign },
                        { text: '分配教师', click: vm.assign }
                    ];

                    // 新增跟进记录
                    vm.addFollow = function () {
                        if (util.selectOneRow(vm)) {
                            $state.go('ppts.follow-add', {
                                customerId: vm.data.rowsSelected[0].customerID,
                                prev: 'ppts.student'
                            });
                        }
                    };

                    // 转学
                    vm.transfer = function () {
                        if (util.selectOneRow(vm)) {
                            studentDataViewService.transferStudent(vm.data.rowsSelected);
                        }
                    };

                    // 充值
                    vm.pay = function () {
                        //当前操作人是潜客的咨询师才可以充值
                        if (util.selectOneRow(vm)) {

                            var customerID = vm.data.rowsSelected[0].customerID;
                            studentDataService.assertAccountCharge(customerID, function (result) {
                                if (result.ok) {
                                    $state.go('ppts.accountChargeEdit', { id: customerID, prev: 'ppts.student' });
                                }
                                else {
                                    vm.errorMessage = result.message;
                                }
                            });
                        }
                    };

                    //批量添加回访
                    vm.batchVisit = function () {
                        if (util.selectMultiRows(vm)) {
                            storage.set('selectedStudents', vm.data.rowsSelected);
                            $state.go('ppts.customervisit-batch', { prev: 'ppts.student' });
                        }
                    };
                }]);
        });