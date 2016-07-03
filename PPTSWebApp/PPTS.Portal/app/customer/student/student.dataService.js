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

        resource.assertStudentTransfer = function (customerID, success, error) {
            resource.query({ operation: 'AssertStudentTransfer', customerID: customerID }, success, error);
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

        //根据工作流信息获取转学信息
        resource.getStudentTransferApplyByWorkflow = function (wfParams, success, error) {
            resource.post({ operation: 'GetStudentTransferApplyByWorkflow' }, wfParams, success, error);
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

        //解冻
        resource.studentThawSave = function (model, success, error) {
            resource.save({ operation: 'SaveMaterial' }, model, success, error);
        }

        //查看解冻
        resource.ShowStudentThaw = function (criteria, success, error) {
            resource.post({ operation: 'GetMaterial' }, criteria, success, error);
        }

        resource.downloadMaterial = function (criteria, success, error) {
            resource.post({ operation: 'DownloadMaterial' }, criteria, success, error);
        }

        return resource;
    }]);

    customer.registerValue('studentAdvanceSearchItems', [
        { name: '当前年级：', template: '<ppts-checkbox-group category="grade" model="vm.criteria.grade" async="false"/>' },
        { name: '建档日期：', template: '<mcs-daterangepicker start-date="vm.criteria.createTimeStart" end-date="vm.criteria.createTimeEnd" width="41%" css="mcs-margin-left-10"/>' },
        { name: '首次签约日期：', template: '<mcs-daterangepicker start-date="vm.criteria.firstSignTimeStart" end-date="vm.criteria.firstSignTimeEnd" width="41%" css="mcs-margin-left-10"/>' },
        { name: '账户价值：', template: '<mcs-datarange min="vm.criteria.accountAmountStart" max="vm.criteria.accountAmountEnd" min-text="账户价值起" max-text="账户价值止" width="41.8%"/>' },
        { name: '可用金额：', template: '<mcs-datarange min="vm.criteria.avaiableAmountStart" max="vm.criteria.avaiableAmountEnd" min-text="可用金额起" max-text="可用金额止" width="41.8%"/>' },
        { name: '剩余课时：', template: '<mcs-datarange min="vm.criteria.assetAmountStart" max="vm.criteria.assetAmountEnd" min-text="剩余课时起" max-text="剩余课时止" unit="个" width="41.8%"/>' },
        { name: '学员状态：', template: '<ppts-radiobutton-group category="studentType" model="vm.criteria.customerType" show-all="true" async="false" css="mcs-padding-left-10" />' },
        {
            name: '', template: '<ppts-radiobutton-group category="studentValid" model="vm.criteria.validType" show-all="true" async="false" css="mcs-padding-left-10" ng-show="vm.criteria.customerType==1" />' +
                               '<ppts-radiobutton-group category="studentAttend" model="vm.criteria.attendType" show-all="true" async="false" css="mcs-padding-left-10" ng-show="vm.criteria.customerType==2" style="display:block"/>' +
                               '<ppts-radiobutton-group category="studentCancel" model="vm.criteria.stopType" show-all="true" async="false" css="mcs-padding-left-10" ng-show="vm.criteria.customerType==3" style="display:block"/>' +
                               '<ppts-radiobutton-group category="studentSuspend" model="vm.criteria.suspendType" show-all="true" async="false" css="mcs-padding-left-10" ng-show="vm.criteria.customerType==4" style="display:block"/>' +
                               '<ppts-radiobutton-group category="studentCompleted" model="vm.criteria.completedType" show-all="true" async="false" css="mcs-padding-left-10" ng-show="vm.criteria.customerType==5" style="display:block"/>' +
                               '<ppts-radiobutton-group category="studentAttendRange" model="vm.selectedAttendRange" async="false" css="mcs-padding-left-10" ng-show="vm.criteria.customerType==2"/>' +
                               '<ppts-radiobutton-group category="studentRange" model="vm.selectedRange" async="false" css="mcs-padding-left-10" ng-show="vm.criteria.customerType>2"/>', hide: 'vm.criteria.customerType.length!=1'
        },
        { name: '据最后上课时长：', template: '<ppts-radiobutton-group category="lastCourseType" model="vm.criteria.lastCourseType" show-all="true" async="false" css="mcs-margin-left-10"/>' },
        { name: '信息来源：', template: '<ppts-checkbox-group category="source" model="vm.criteria.sourceMainType" parent="0" async="false"/>' },
        { name: '信息来源二：', template: '<ppts-checkbox-group category="source" model="vm.criteria.sourceSubType" parent="vm.criteria.sourceMainType" ng-show="vm.criteria.sourceMainType.length==1" async="false"/>', hide: 'vm.criteria.sourceMainType.length!=1' },
        { name: 'VIP客户：', template: '<ppts-checkbox-group category="vipLevel" model="vm.criteria.vipLevels" async="false" width="60px"/>' },
        { name: '有过转介绍的学员：', template: '<mcs-datarange min="vm.criteria.referralCountStart" max="vm.criteria.referralCountEnd" datatype="int" min-text="转介绍过学员的个数" max-text="转介绍过学员的个数" unit="个" width="42%" css="mcs-padding-left-10"/>' },
        { name: '高三毕业库学员：', template: '<ppts-radiobutton-group category="graduated" model="vm.criteria.graduatedParam" show-all="true" async="false" css="mcs-margin-left-10"/>' },
        { name: '归属关系：', template: '<ppts-checkbox-group category="relation" model="vm.criteria.belongs" async="false" css="mcs-padding-left-10"/><mcs-input placeholder="归属人姓名" model="vm.criteria.belongName" uib-popover="归属人姓名" popover-trigger="mouseenter" custom-style="width:28%"/>' },
        { name: '建档关系：', template: '<ppts-checkbox-group category="creation" model="vm.criteria.creation" async="false" css="mcs-padding-left-10"/><mcs-input placeholder="建档人姓名" model="vm.criteria.creatorName" uib-popover="建档人姓名" popover-trigger="mouseenter" custom-style="width:28%"/>' },
        { name: '查询部门：', template: '<ppts-radiobutton-group category="dept" model="vm.criteria.dept" show-all="true" async="false" css="mcs-padding-left-10"/>' },
        { name: '在读学校：', template: '<ppts-school name="vm.criteria.schoolName" css="mcs-padding-left-10 mcs-width-half"/>' },
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
            name: "学员编号"
        }, {
            field: "parentName",
            name: "家长姓名"
        }, {
            field: "firstSignTime",
            name: "首次签约日期",
            template: '<span>{{row.firstSignTime | date:"yyyy-MM-dd" | normalize}}</span>'
        }, {
            field: "customerSchoolName",
            name: "在读学校"
        }, {
            field: "grade",
            name: "当前年级",
            template: '<span>{{ row.grade | grade | normalize}}</span>'
        }, {
            field: "consultantName",
            name: "归属咨询师"
        }, {
            field: "educatorName",
            name: "归属学管师"
        }, {
            field: "assignedAmount",
            name: "签约课时",
            template: '<span>{{ row.assignedAmount | currency }}</span>',
            sortable: true
        }, {
            field: "assetRemainAmount",
            name: "剩余课时",
            sortable: true
        }, {
            field: "accountMoney",
            name: "账户价值",
            template: '<span>{{ row.accountMoney | currency }}</span>',
            sortable: true
        }, {
            field: "orderMoney",
            name: "订购资金余额",
            template: '<span>{{ row.orderMoney | currency }}</span>',
            sortable: true
        }, {
            field: "avaiableMoney",
            name: "可用金额",
            template: '<span>{{ row.avaiableMoney | currency }}</span>',
            sortable: true
        }, {
            field: "lastAssignDays",
            name: "距最后上课",
            sortable: true
        }],
        orderBy: [{ dataField: 'CreateTime', sortDirection: 1 }]
    });

    customer.registerFactory('studentDataViewService', ['$state', 'studentDataService', 'dataSyncService', 'mcsDialogService', 'studentListDataHeader',
        function ($state, studentDataService, dataSyncService, mcsDialogService, studentListDataHeader) {
            var service = this;

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

                dataSyncService.injectDynamicDict('messageType');
            };

            return service;
        }]);

    customer.registerValue('thawData', {
        customAction: 'edit',
        rowsSelected: [],
        keyFields: ['resourceID'],
        headers: [{
            field: 'fileName',
            name: '文件名',
            headerCss: 'datatable-header',
            template: '<label class="ppts-datatable-solidlineheight">{{row.fileName}}</label>'
        },
        {
            field: 'downLoad',
            name: '点击下载',
            headerCss: 'datatable-header',
            template: '<a ng-click="row.downLoad()" style="cursor:pointer">点击下载</a>'
        }],
        pager: {
            pagable: false,
        }
    });
});