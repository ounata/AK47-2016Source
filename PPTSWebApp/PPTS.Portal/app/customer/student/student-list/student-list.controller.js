define([ppts.config.modules.customer,
    ppts.config.dataServiceConfig.customerDataService,
        ppts.config.dataServiceConfig.studentDataService],
        function (customer) {
            customer.registerController('studentListController', [
                '$scope',
                '$state',
                'utilService',
                'dataSyncService',
                'studentListDataHeader',
                'studentDataService',
                'studentDataViewService',
                'studentAdvanceSearchItems',
                'mcsDialogService',
                'storage',
                'customerDataViewService',
                'customerRelationType',
                'exportExcelService',
                function ($scope, $state, util, dataSyncService, studentListDataHeader, studentDataService, studentDataViewService, searchItems, mcsDialogService, storage, customerDataViewService, relationType, exportExcelService) {
                    var vm = this;
                    // 获取学员与员工关系
                    vm.relationType = relationType;

                    // 学员类型值监控
                    $scope.$watch('vm.criteria.customerType', function (value) {
                        vm.selectedAttendRange = '-1';
                        vm.selectedRange = '-1';
                        vm.criteria.validType = "-1";
                        vm.criteria.attendType = "-1";
                        vm.criteria.stopType = "-1";
                        vm.criteria.suspendType = "-1";
                        vm.criteria.completedType = "-1";
                    });

                    // 配置数据表头 
                    dataSyncService.configDataHeader(vm, studentListDataHeader, studentDataService.getPagedStudents);

                    // 页面初始化加载或重新搜索时查询
                    vm.search = function () {
                        dataSyncService.initDataList(vm, studentDataService.getAllStudents, function () {
                            vm.searchItems = searchItems;

                            dataSyncService.injectDictData({
                                c_codE_ABBR_Customer_Graduated: [{ key: 0, value: '今年新入库学员' }, { key: 1, value: '历史入库学员' }],
                                c_codE_ABBR_Customer_LastCourseType: [{ key: 0, value: '按最后上课时间判断时长' }, { key: 1, value: '未上过课按付款时间判断时长' }]
                            });
                            dataSyncService.injectDictData({
                                c_codE_ABBR_Student_Type: [{ key: '1', value: '有效学员', tooltip: '账户价值金额>=200，或剩余课时>=1；最后一次上课时间据查询时间或付款时间<=180天，不含高三毕业库中的学生' }, { key: '2', value: '上课学员', tooltip: '最后一次上课/付款时间据查询时间<=30天的学员' }, { key: '3', value: '停课学员', tooltip: '账户价值>=200，或剩余课时>=1；最后一次上课/付款时间据查询时间>=31天，<=180天的学员（不含高三毕业库学生）' }, { key: '4', value: '休学学员', tooltip: '账户价值>=200，或剩余课时>=1；最后一次上课/付款时间据查询时间>180天的学员（不含高三毕业库学生）' }, { key: '5', value: '结课学员', tooltip: '账户价值<200，且剩余课时<1的学员' }, { key: '6', value: '无订单学员', tooltip: '账户价值>=200且剩余课时=0的学员' }],
                                c_codE_ABBR_Student_Valid: [{ key: '1', value: '1对1有效', tooltip: '有效学员中有1对1剩余课时的学员' }, { key: '2', value: '班组有效', tooltip: '有效学员中有班组剩余课时的学员' }, { key: '3', value: '非课收有效', tooltip: '有效学员中有非课收剩余课时的学员' }],
                                c_codE_ABBR_Student_Attend: [{ key: '1', value: '1对1上课', tooltip: '上课学员中发生过1对1课时的学员' }, { key: '2', value: '班组上课', tooltip: '上课学员中发生过班组课时的学员' }],
                                c_codE_ABBR_Student_Cancel: [{ key: '1', value: '1对1停课', tooltip: '停课学员中有1对1剩余课时' }, { key: '2', value: '班组停课', tooltip: '停课学员中有班组剩余课时' }],
                                c_codE_ABBR_Student_Suspend: [{ key: '1', value: '1对1休学', tooltip: '休学学员中有1对1剩余课时' }, { key: '2', value: '班组休学', tooltip: '休学学员中有班组剩余课时' }],
                                c_codE_ABBR_Student_Completed: [{ key: '2', value: '上课结课', tooltip: '没有退费记录，有“已上”上课记录' }, { key: '4', value: '退费结课', tooltip: '只要有退费记录，且“分区域财务经理审批通过”，就算退费结课' }, { key: '3', value: '转让结课', tooltip: '没有退费记录，没有“已上”上课记录，有转出记录' }],
                                c_codE_ABBR_Student_Attend_Range: [{ key: '-1', value: '1个月内上过课' }, { key: '1', value: '本月上课' }],
                                c_codE_ABBR_Student_Range: [{ key: '-1', value: '截止到当前' }, { key: '1', value: '本月新增' }]
                            });
                            dataSyncService.injectDynamicDict('people,relation,creation,dept');

                            studentDataViewService.initDateRange($scope, vm, [
                                { watchExp: 'vm.selectedAttendRange', selectedValue: 'selectedAttendRange', start: 'statusStartTime', end: 'statusEndTime' },
                                { watchExp: 'vm.selectedRange', selectedValue: 'selectedRange', start: 'statusStartTime', end: 'statusEndTime' }
                            ]);
                        });
                    };
                    vm.search();

                    // 导出
                    vm.export = function () {
                        exportExcelService.export(ppts.config.customerApiBaseUrl + 'api/students/exportallstudents', vm.criteria);
                    };

                    // 订购
                    vm.purchase = function (item) {
                        if (util.selectOneRow(vm)) {

                            var params = {
                                customerId: vm.data.rowsSelected[0].customerID,
                                campusId: vm.data.rowsSelected[0].campusID,
                                customerName: vm.data.rowsSelected[0].customerName,
                                customerCode: vm.data.rowsSelected[0].customerCode
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
                                        params: { customers: vm.data.rowsSelected },
                                        settings: { backdrop: 'static', size: 'lg' }
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
                        { text: '分配教师', click: vm.assign, permission: '分配教师-本校区' }
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

                            var customerID = vm.data.rowsSelected[0].customerID;
                            studentDataService.assertStudentTransfer(customerID, function (result) {
                                if (result.ok) {
                                    studentDataViewService.transferStudent(vm.data.rowsSelected);
                                }
                                else {
                                    vm.errorMessage = result.message;
                                }
                            });
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