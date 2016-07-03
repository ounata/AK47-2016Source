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
                    vm.assignTeacherUrl = ppts.config.customerApiBaseUrl + 'api/students/getTeachers';
                    vm.tags = [];
                    vm.customerName = [];
                    if (data.customers) {
                        for (var i in data.customers) {
                            vm.customerName.push( data.customers[i].customerName);
                        }                        
                    }

                    dataSyncService.injectDynamicDict('messageType');

                    //查询教师列表
                    vm.searchTeacher = function () {
                        studentDataService.getTeachers(function (result) {
                            for (var i in result) {
                                var item = result[i];
                                vm.tags.push({ teacherID: item.teacherID, teacherName: item.teacherName, teacherJobID: item.jobID, teacherJobOrgID: item.jobOrgID, teacherJobOrgName: item.jobOrgName, viewName: getShortOrgName(item.jobOrgName) + '-' + item.teacherName + '(' + item.teacherOACode + ')', teacherOACode: item.teacherOACode });
                            }
                            dataSyncService.injectDynamicDict(vm.tags, { key: 'teacherID', value: 'viewName', category: 'teacher' });
                            $scope.$broadcast('dictionaryReady');
                        });
                    };

                    var getShortOrgName = function (orgName) {
                        if (!orgName)
                            return "";
                        var part = orgName.split("-");
                        for (var i in part) {
                            if (part[i].indexOf("组") != -1)
                                return part[i];
                        }
                        return "";
                    }
                    

                    //列表
                    vm.data = {
                        rowsSelected: [],
                        keyFields: ['teacherID'],
                        orderBy: [{ dataField: 'teacherID', sortDirection: 1 }],
                        headers: [{
                            field: "viewName",
                            name: "现教师姓名",
                            template: '<span uib-popover="{{row.viewName|tooltip:10}}" popover-trigger="mouseenter">{{row.viewName | truncate:10}}</span>'
                        }, {
                            field: "teacherName",
                            name: "目标教师姓名",
                            headerCss: 'datatable-header-align-right',
                            sortable: false,
                            template: '<mcs-select category="teacher" model="row.newTeacher" async="false" />',
                            description: ''
                        }, {
                            field: "teacherName",
                            name: "调换/调出原因",
                            headerCss: 'datatable-header-align-right',
                            template: '<mcs-select category="changeTeacherReason" caption="请选择调换原因" name="changeTeacherReason" ng-required  model="row.changeTeacherReason" async="false"  />',
                            sortable: false,
                            description: ''
                        }, {
                            field: "teacherName",
                            name: "其他（备注原因）",
                            headerCss: 'datatable-header-align-right',
                            template: '<mcs-input model="row.qtReason" ng-show="row.changeTeacherReason==6" required="true" /><div class="help-block" ng-show="row.changeTeacherReason==6">',
                            sortable: false,
                            description: ''
                        }, {
                            field: "teacherName",
                            name: "操作",
                            template: '<mcs-button category="icon" icon="exchange" click="vm.changeTeacher(row)" uib-popover="调换" css="btn-primary" popover-trigger="mouseenter"/> <mcs-button category="icon" icon="share" click="vm.calloutTeacher(row)" uib-popover="调出" popover-trigger="mouseenter" css="btn-yellow"/>',
                        }
                        ]
                    }

                    vm.searchCustomerTeacherRelations = function () {
                        vm.searchTeacher();
                        if (data.customers.length == 1) {
                            studentDataService.getAllCustomerTeacherRelations(data.customers[0], function (result) {
                                vm.data.rows = result.customerTeachers;
                                for (var i in vm.data.rows) {
                                    vm.data.rows[i].viewName = getShortOrgName(vm.data.rows[i].teacherJobOrgName) + '-' + vm.data.rows[i].teacherName + '(' + vm.data.rows[i].teacherOACode + ')';
                                }
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
                        if (vm.teacher) {
                            var tc = [];
                            for (var i in vm.tags) {
                                var item = vm.tags[i];
                                if (item.teacherID==vm.teacher) {
                                    for (var j in data.customers) {
                                        var c = data.customers[j];
                                        tc.push({ teacherID: item.teacherID, teacherName: item.teacherName, teacherJobID: item.teacherJobID, teacherJobOrgID: item.teacherJobOrgID, teacherJobOrgName: item.teacherJobOrgName, customerID: c.customerID   });
                                    }
                                }
                            }
                            studentDataService.assignTeacher({ ctrc: tc, sendEmailSMS: vm.sendEmailSMS }, function () {
                                $uibModalInstance.dismiss('Canceled');
                            }, function () { });
                        } else {
                            mcsDialogService.info({ title: '提示', message: "请选择一个教师！" });
                        }
                    }

                    //调出教师
                    vm.calloutTeacher = function (item) {
                        if (data.customers.length == 1) {
                            //检查
                            //请选择调出原因
                            if (!item.changeTeacherReason) {
                                mcsDialogService.info({ title: '提示', message: "请选择调换原因！" });
                                return;
                            }
                            if (item.changeTeacherReason == 6 && !item.qtReason) {
                                mcsDialogService.info({ title: '提示', message: "请填写其他原因！" });
                                return;
                            }
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
                            //检查
                            //请选择调换原因
                            if (!item.changeTeacherReason) {
                                mcsDialogService.info({ title: '提示', message: "请选择调换原因！" });
                                return;
                            }
                            if (item.changeTeacherReason == 6 && !item.qtReason) {
                                mcsDialogService.info({ title: '提示', message: "请填写其他原因！" });
                                return;
                            }
                            //请更换目标教师后再调换
                            var newTeacher = {};
                            if (!item.newTeacher) {
                                mcsDialogService.info({ title: '提示', message: "请选择调换教师！" });
                                return;
                            }                            
                            if (item.newTeacher == item.teacherID) {
                                mcsDialogService.info({ title: '提示', message: "调换教师不能选择原来的教师！" });
                                return;
                            }
                            for (var i in vm.tags) {
                                if (vm.tags[i].teacherID == item.newTeacher)
                                    newTeacher = vm.tags[i];
                            }
                            var model = {
                                applyType: 2, customerID: data.customers[0].customerID, CampusID: data.customers[0].campusID,
                                oldTeacherID: item.teacherID, oldTeacherJobID: item.teacherJobID, oldTeacherOACode: item.viewName, oldTeacherName: item.teacherName, oldTeacherJobOrgID: item.teacherJobOrgID, oldTeacherJobOrgName: item.teacherJobOrgName,
                                newTeacherID: newTeacher.teacherID, newTeacherJobID: newTeacher.teacherJobID, newTeacherOACode: newTeacher.teacherOACode, newTeacherName: newTeacher.teacherName, newTeacherJobOrgID: newTeacher.teacherJobOrgID, newTeacherJobOrgName: newTeacher.teacherJobOrgName,
                                reason: item.changeTeacherReason, reasonDescription: item.qtReason
                            };
                            studentDataService.changeTeacher({ cta: model, sendEmailSMS: vm.sendEmailSMS_ }, function () {                                
                                vm.searchCustomerTeacherRelations();
                            }, function () { });
                        }
                    }
                }]);
        });