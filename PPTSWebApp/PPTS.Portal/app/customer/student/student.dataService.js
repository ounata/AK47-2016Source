define(['angular', ppts.config.modules.customer], function (ng, customer) {

    customer.registerFactory('studentDataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.customerApiBaseUrl + 'api/students/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false },
                'get': { method: 'GET', isArray: true }
            });

        resource.getAllStudents = function (criteria, success, error) {
            resource.post({ operation: 'getAllStudents' }, criteria, success, error);
        }

        resource.getPagedStudents = function (criteria, success, error) {
            resource.post({ operation: 'getPagedStudents' }, criteria, success, error);
        }

        resource.getStudentInfo = function (id, success, error) {
            resource.query({ operation: 'getStudentInfo', id: id }, success, error);
        }

        resource.updateStudent = function (model, success, error) {
            resource.save({ operation: 'updateStudent' }, model, success, error);
        }

        resource.addParent = function (model, success, error) {
            resource.post({ operation: 'addParent' }, model, success, error);
        }

        resource.getStudentParents = function (id, success, error) {
            resource.query({ operation: 'getStudentParents', id: id }, success, error);
        }

        resource.getStudentParent = function (parentId, customerId, success, error) {
            resource.query({ operation: 'getStudentParent', id: parentId, customerId: customerId }, success, error);
        }

        resource.updateParent = function (model, success, error) {
            resource.save({ operation: 'updateParent' }, model, success, error);
        }

        resource.initParentForCreate = function (studentId, success, error) {
            resource.query({ operation: 'createParent', id: studentId }, success, error);
        }

        resource.createParent = function (model, success, error) {
            resource.save({ operation: 'createParent' }, model, success, error);
        }

        resource.assertAccountCharge = function (customerID, success, error) {
            resource.query({ operation: 'AssertAccountCharge', customerID: customerID }, success, error);
        }

        resource.assignTeacher = function (model, success, error) {
            resource.post({ operation: 'assignTeacher' }, model, success, error);
        }

        resource.getTeachers = function (success, error) {
            resource.get({ operation: 'getTeachers' }, success, error);
        }

        resource.calloutTeacher = function (model, success, error) {
            resource.post({ operation: 'calloutTeacher' }, model, success, error);
        }

        resource.changeTeacher = function (model, success, error) {
            resource.post({ operation: 'changeTeacher' }, model, success, error);
        }

        resource.getAllCustomerTeacherRelations = function (model, success, error) {
            resource.post({ operation: 'getAllCustomerTeacherRelations' }, model, success, error);
        }

        //根据学员ID获取转学信息
        resource.getStudentTransferApplyByCustomerID = function (id, success, error) {
            resource.query({ operation: 'GetStudentTransferApplyByCustomerID', id: id }, success, error);
        }

        //根据申请ID获取转学信息
        resource.getStudentTransferApplyByApplyID = function (id, success, error) {
            resource.query({ operation: 'GetStudentTransferApplyByApplyID', id: id }, success, error);
        }

        //保存转学申请
        resource.saveStudentTransferApply = function (apply, success, error) {
            resource.post({ operation: 'SaveStudentTransferApply' }, apply, success, error);
        }

        //审批转学申请
        resource.approveStudentTransferApply = function (applyID, opinion, success, error) {

            var apply = { applyID: applyID, opinion: opinion };
            resource.post({ operation: 'ApproveStudentTransferApply' }, apply, success, error);
        }

        return resource;
    }]);

    customer.registerValue('studentAdvanceSearchItems', [
        { name: '当前年级：', template: '<ppts-checkbox-group category="grade" model="vm.criteria.grade" clear="vm.criteria.grade=[]" async="false"/>' },
        { name: '建档日期：', template: '<ppts-daterangepicker startDate="vm.criteria.createTimeStart" endDate="vm.criteria.createTimeEnd"/>' },
        { name: '首次签约日期：', template: '<ppts-daterangepicker startDate="vm.criteria.firstSignTimeStart" endDate="vm.criteria.firstSignTimeEnd"/>' },
        { name: '账户价值：', template: '<ppts-range-slider start="vm.criteria.accountStart" end="vm.criteria.accountEnd" class="col-xs-6 col-sm-6"/>' },
        { name: '剩余课时：', template: '<ppts-range-slider start="vm.criteria.leftCourseStart" end="vm.criteria.leftCourseEnd" class="col-xs-6 col-sm-6"/>' },
        { name: '可用金额：', template: '<ppts-range-slider start="vm.criteria.availableAccountStart" end="vm.criteria.availableAccountEnd" class="col-xs-6 col-sm-6"/>' },
        { name: '信息来源：', template: '<ppts-datetimepicker model="vm.criteria.test" css="col-xs-4 col-sm-4" />' },
        { name: '客户级别：', template: '<ppts-checkbox-group category="vipLevel" model="vm.criteria.customerLevels" clear="vm.criteria.customerLevels=[]" async="false"/>' },
        { name: '学员类型：', template: '<ppts-radiobutton-group category="studentType" model="vm.selectedStudentType" async="false"/>' },
        { name: '', template: '<ppts-radiobutton-group category="studentValid" model="vm.criteria.valid" async="false" ng-show="vm.selectedStudentType==1"/>' + 
                              '<ppts-radiobutton-group category="studentAttend" model="vm.criteria.attend" async="false" ng-show="vm.selectedStudentType==2" style="display:block"/>' +
                              '<ppts-radiobutton-group category="studentCancel" model="vm.criteria.cancel" async="false" ng-show="vm.selectedStudentType==3" style="display:block"/>' +
                              '<ppts-radiobutton-group category="studentSuspend" model="vm.criteria.suspend" async="false" ng-show="vm.selectedStudentType==4" style="display:block"/>' +
                              '<ppts-radiobutton-group category="studentCompleted" model="vm.criteria.completed" async="false" ng-show="vm.selectedStudentType==5" style="display:block"/>' +
                              '<ppts-radiobutton-group category="studentAttendRange" model="vm.selectedAttendRange" async="false" ng-show="vm.selectedStudentType==2"/>' +
                              '<ppts-radiobutton-group category="studentRange" model="vm.selectedRange" async="false" ng-show="vm.selectedStudentType>2"/>'
        }
    ]);

    customer.registerValue('studentListDataHeader', {
        selection: 'checkbox',
        rowsSelected: [],
        keyFields: ['customerID', 'customerName', 'customerCode', 'grade', 'campusID'],
        headers: [{
            field: "customerName",
            name: "学员姓名",
            template: '<a ui-sref="ppts.student-view.profiles({id:row.customerID,prev:\'ppts.student\'})">{{row.customerName}}</a>'
        }, {
            field: "customerCode",
            name: "学员编号",
            template: '<a ui-sref="ppts.student-view.profiles({id:row.customerID,prev:\'ppts.student\'})">{{row.customerCode}}</a>',
        }, {
            field: "parentName",
            name: "家长姓名"
        }, {
            field: "firstSignTime",
            name: "首次签约日期",
            template: '<span>{{row.firstSignTime | date:"yyyy-MM-dd"}}</span>'
        }, {
            field: "campusName",
            name: "在读学校"
        }, {
            field: "grade",
            name: "当前年级",
            template: '<span ng-if="row.grade > 0">{{ row.grade | grade }}</span>'
        }, {
            field: "",
            name: "归属学管师",
            template: '<span></span>'
        }, {
            field: "",
            name: "归属咨询师",
            template: '<span></span>'
        }, {
            field: "customerCode",
            name: "签约课时",
            template: '<span></span>',
            sortable: true
        }, {
            field: "",
            name: "剩余课时",
            template: '<span></span>'
        }, {
            field: "",
            name: "账户价值",
            template: '<span></span>'
        }, {
            field: "",
            name: "订购资金余额",
            template: '<span></span>',
            sortable: true
        }, {
            field: "",
            name: "可用金额",
            template: '<span></span>',
            sortable: true
        }, {
            name: "距最后上课",
            template: '<span></span>',
            sortable: true
        }],
        pager: {
            pageIndex: 1,
            pageSize: 10,
            totalCount: -1
        },
        orderBy: [{ dataField: 'CreateTime', sortDirection: 1 }]
    });

    customer.registerFactory('studentDataViewService', ['$state', 'studentDataService', 'dataSyncService', 'mcsDialogService', 'studentListDataHeader',
        function ($state, studentDataService, dataSyncService, mcsDialogService, studentListDataHeader) {
            var service = this;

            // 配置学员列表表头
            service.configStudentListHeaders = function (vm) {
                vm.data = studentListDataHeader;
                vm.data.pager.pageChange = function () {
                    dataSyncService.initCriteria(vm);
                    studentDataService.getPagedStudents(vm.criteria, function (result) {
                        vm.data.rows = result.pagedData;
                    });
                }
            };

            // 初始化学员列表
            service.initStudentList = function (vm, callback) {
                dataSyncService.initCriteria(vm);
                studentDataService.getAllStudents(vm.criteria, function (result) {
                    vm.data.rows = result.queryResult.pagedData;
                    dataSyncService.injectDictData({
                        c_codE_ABBR_Student_Type: [{ key: '1', value: '有效学员' }, { key: '2', value: '上课学员' }, { key: '3', value: '停课学员' }, { key: '4', value: '休课学员' } , { key: '5', value: '结课学员' }, { key: '6', value: '无订单学员' }],
                        c_codE_ABBR_Student_Valid: [{ key: '0', value: '全部有效（1对1、班组、非课收）' }, { key: '1', value: '1对1有效' }, { key: '2', value: '班组有效' }, { key: '3', value: '非课收有效' }],
                        c_codE_ABBR_Student_Attend: [{ key: '0', value: '全部上课（1对1、班组、非课收）' }, { key: '1', value: '1对1上课' }, { key: '2', value: '班组上课' }, { key: '3', value: '非课收上课' }],
                        c_codE_ABBR_Student_Cancel: [{ key: '0', value: '全部停课（1对1、班组）' }, { key: '1', value: '1对1停课' }, { key: '2', value: '班组停课' }],
                        c_codE_ABBR_Student_Suspend: [{ key: '0', value: '全部休学（1对1、班组）' }, { key: '1', value: '1对1休学' }, { key: '2', value: '班组休学' }],
                        c_codE_ABBR_Student_Completed: [{ key: '0', value: '全部结课（消耗、退费、转让）' }, { key: '1', value: '消耗结课' }, { key: '2', value: '退费结课' }, { key: '3', value: '转让结课' }],
                        c_codE_ABBR_Student_Attend_Range: [{ key: '1', value: '本月上课' }, { key: '2', value: '1个月内上过课' }],
                        c_codE_ABBR_Student_Range: [{ key: '0', value: '截止到当前' }, { key: '1', value: '本月新增' }]
                    });
                    dataSyncService.updateTotalCount(vm, result.queryResult);
                    if (ng.isFunction(callback)) {
                        callback();
                    }
                });
            };

            // 初始化日期范围
            service.initDateRange = function ($scope, vm, watchExps) {
                if (!watchExps && !watchExps.length) return;
                for (var index in watchExps) {
                    (function () {
                        var temp = index, exp = watchExps[index];
                        $scope.$watch(exp.watchExp, function () {
                            var selectedValue = exp.selectedValue;
                            var dateRange = dataSyncService.selectPageDict('studentRange', vm[selectedValue]);
                            if (dateRange) {
                                vm.criteria[exp.start] = dateRange.start;
                                vm.criteria[exp.end] = dateRange.end;
                            }
                        });
                    })();
                }
            };

            // 转学
            service.transferStudent = function (students) {
                mcsDialogService.create('app/customer/student/student-transfer/student-transfer.tpl.html', {
                    controller: 'studentTransferController',
                    params: {
                        students: students
                    }
                });
            };

            // 加载转学弹出框
            service.getTransferStudentInfo = function (vm, data, callback) {
                vm.displayNames = '';
                vm.customerNames = '';
                vm.customerIds = [];

                angular.forEach(data.students, function (item, index) {
                    var length = data.students.length;
                    if (index == 0) {
                        vm.displayNames += length == 1 ? item.customerName : item.customerName + '...';
                    }
                    vm.customerNames += index == 0 ? item.customerName : ',' + item.customerName;
                    vm.customerIds.push(item.customerID);
                });

                dataSyncService.injectPageDict(['messageType']);
            };

            return service;
        }]);
});