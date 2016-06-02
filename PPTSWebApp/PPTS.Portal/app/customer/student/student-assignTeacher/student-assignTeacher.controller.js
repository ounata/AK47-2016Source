define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.studentDataService],
        function (customer) {
            customer.registerController('assignTeacherController', [
                '$scope',
                'data',
                'dataSyncService',
                'studentDataService',
                '$uibModalInstance',
                'mcsDialogService',
                function ($scope, data, dataSyncService, studentDataService, $uibModalInstance, mcsDialogService) {
                    var vm = this;
                    vm.tags = [];
                    if (data.customers)
                        vm.customerName = data.customers.customerName;
                    dataSyncService.injectDictData({
                        c_codE_ABBR_Student_SendEmailSMS: [{ key: '1', value: '发送邮件' }, { key: '2', value: '发送短信' }]
                    });

                    //查询教师列表
                    vm.searchTeacher = function () {
                        studentDataService.getTeachers(function (result) {
                            vm.teas = result;
                            for (var i in result) {
                                var item = result[i];
                                vm.tags.push({ teacherID: item.teacherID, teacherName: item.teacherName, teacherJobID: item.jobID, teacherJobOrgID: item.jobOrgID, teacherJobOrgName: item.jobOrgName, viewName: item.jobShortOrgName + '-' + item.teacherName + '(' + item.teacherCode + ')' });
                            }
                        }, function () {
                        });
                    }

                    vm.searchTeacher();

                    vm.queryTeacherList = function (query) {
                        var result = [];

                        vm.tags.forEach(function (item) {
                            if (item.viewName && item.viewName.indexOf(query) > -1) {
                                result.push(item);
                            }
                        });

                        return result;
                    };

                    //列表
                    vm.data = {
                        rowsSelected: [],
                        keyFields: ['teacherID'],
                        orderBy: [{ dataField: 'teacherID', sortDirection: 1 }],
                        headers: [{
                            field: "viewName",
                            name: "现教师姓名",
                            headerCss: 'datatable-header-align-right',
                            sortable: false,
                            description: ''
                        }, {
                            field: "teacherName",
                            name: "目标教师姓名",
                            headerCss: 'datatable-header-align-right',
                            sortable: false,
                            template: '<tags-input class="mcs-tags-input-lg" ng-disable="true" ng-model="row.newTeacher" key-property="teacherID" display-property="viewName" placeholder="教师姓名"><auto-complete min-length="1" source="vm.queryTeacherList($query)"></auto-complete> </tags-input>',
                            description: ''
                        }, {
                            field: "teacherName",
                            name: "调换/调出原因",
                            headerCss: 'datatable-header-align-right',
                            template: '<ppts-select category="changeTeacherReason" name="changeTeacherReason" ng-required  model="row.changeTeacherReason" async="false"  />',
                            sortable: false,
                            description: ''
                        }, {
                            field: "teacherName",
                            name: "其他（备注原因）",
                            headerCss: 'datatable-header-align-right',
                            template: '<input ng-model="row.qtReason" />',
                            sortable: false,
                            description: ''
                        }, {
                            field: "teacherName",
                            name: "操作",
                            headerCss: 'datatable-header-align-right',
                            template: '<mcs-button category="save" text="调换" click="vm.changeTeacher(row)"  ></mcs-button><mcs-button category="delete" text="调出" click="vm.calloutTeacher(row)"  ></mcs-button>',
                            sortable: false,
                            description: ''
                        }
                        ]
                    }

                    vm.searchCustomerTeacherRelations = function () {
                        if (data.customers.length == 1) {
                            studentDataService.getAllCustomerTeacherRelations(data.customers[0], function (result) {
                                vm.data.rows = result.customerTeachers;
                            }, function () { });
                        }
                    }
                    vm.searchCustomerTeacherRelations();

                    //关闭窗口
                    vm.close = function () {
                        $uibModalInstance.dismiss('Canceled');
                    };

                    //分配教师
                    vm.assignTeacher = function () {
                        if (vm.teachers.length > 0) {
                            var tc = [];
                            for (var i in vm.teachers) {
                                var item = vm.teachers[i];
                                for (var j in data.customers) {
                                    var c = data.customers[j];
                                    tc.push({ teacherID: item.teacherID, teacherName: item.teacherName, teacherJobID: item.teacherJobID, teacherJobOrgID: item.teacherJobOrgID, teacherJobOrgName: item.teacherJobOrgName, customerID: c.customerID });
                                }
                            }
                            studentDataService.assignTeacher(tc, function () {
                                $uibModalInstance.dismiss('Canceled');
                            }, function () { });
                        } else {
                            mcsDialogService.error({ title: 'Error', message: "请选择一个教师！" });
                        }
                    }

                    //调出教师
                    vm.calloutTeacher = function (item) {
                        if (data.customers.length == 1) {
                            var model = {
                                applyType: 1, customerID: data.customers[0].customerID, CampusID: data.customers[0].campusID,
                                oldTeacherID: item.teacherID, oldTeacherJobID: item.teacherJobID, oldTeacherOACode: item.viewName, oldTeacherName: item.teacherName, oldTeacherJobOrgID: item.teacherJobOrgID, oldTeacherJobOrgName: item.teacherJobOrgName,
                                reason: item.changeTeacherReason, reasonDescription: item.qtReason
                            };
                            studentDataService.calloutTeacher(model, function () {
                                vm.searchCustomerTeacherRelations();
                            }, function () { });
                        }
                    }

                    //调换教师
                    vm.changeTeacher = function (item) {
                        if (data.customers.length == 1) {
                            var newTeacher = {};
                            for (var i in vm.teas) {
                                if (vm.teas[i].jobID == item.newTeacher[0].teacherJobID)
                                    newTeacher = vm.teas[i];
                            }
                            var model = {
                                applyType: 2, customerID: data.customers[0].customerID, CampusID: data.customers[0].campusID,
                                oldTeacherID: item.teacherID, oldTeacherJobID: item.teacherJobID, oldTeacherOACode: item.viewName, oldTeacherName: item.teacherName, oldTeacherJobOrgID: item.teacherJobOrgID, oldTeacherJobOrgName: item.teacherJobOrgName,
                                newTeacherID: newTeacher.teacherID, newTeacherJobID: newTeacher.jobID, newTeacherOACode: newTeacher.teacherOACode, newTeacherName: newTeacher.teacherName, newTeacherJobOrgID: newTeacher.jobOrgID, newTeacherJobOrgName: newTeacher.jobOrgName,
                                reason: item.changeTeacherReason, reasonDescription: item.qtReason
                            };
                            studentDataService.changeTeacher(model, function () {
                                vm.searchCustomerTeacherRelations();
                            }, function () { });
                        }
                    }
                }]);
        });