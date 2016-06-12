define(['angular', ppts.config.modules.customer], function (ng, customer) {

    customer.registerFactory('customerDataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.customerApiBaseUrl + 'api/potentialcustomers/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false }
            });

        resource.getAllCustomers = function (criteria, success, error) {
            resource.post({ operation: 'getAllCustomers' }, criteria, success, error);
        }

        resource.getPagedCustomers = function (criteria, success, error) {
            resource.post({ operation: 'getPagedCustomers' }, criteria, success, error);
        }

        resource.getCustomerForCreate = function (success, error) {
            resource.query({ operation: 'createCustomer' }, success, error);
        }

        resource.getCustomerForUpdate = function (id, success, error) {
            resource.query({ operation: 'updateCustomer', id: id }, success, error);
        }

        resource.createCustomer = function (model, success, error) {
            resource.save({ operation: 'createCustomer' }, model, success, error);
        };

        resource.updateCustomer = function (model, success, error) {
            resource.save({ operation: 'updateCustomer' }, model, success, error);
        };

        resource.getCustomerInfo = function (id, success, error) {
            resource.query({ operation: 'getCustomerInfo', id: id }, success, error);
        }

        resource.getCustomerByCode = function (customerCode, success, error) {
            resource.query({ operation: 'getCustomerByCode', customerCode: customerCode }, success, error);
        };

        resource.getStaffRelations = function (criteria, success, error) {
            resource.post({ operation: 'getStaffRelations' }, criteria, success, error);
        };

        resource.getTeacherRelations = function (criteria, success, error) {
            resource.post({ operation: 'getTeacherRelations' }, criteria, success, error);
        };

        resource.getPagedStaffRelations = function (criteria, success, error) {
            resource.post({ operation: 'getPagedStaffRelations' }, criteria, success, error);
        }

        resource.getCustomerParents = function (id, success, error) {
            resource.query({ operation: 'getCustomerParents', id: id }, success, error);
        }

        resource.getCustomerParent = function (id, customerId, success, error) {
            resource.query({ operation: 'getCustomerParent', id: id, customerId: customerId }, success, error);
        }

        resource.getParentInfo = function (id, success, error) {
            resource.query({ operation: 'getParentInfo', id: id }, success, error);
        }

        resource.updateParent = function (model, success, error) {
            resource.save({ operation: 'updateParent' }, model, success, error);
        }

        resource.getAllParents = function (model, success, error) {
            resource.post({ operation: 'getAllParents' }, model, success, error);
        }

        resource.initParentForCreate = function (studentId, success, error) {
            resource.query({ operation: 'createParent', id: studentId }, success, error);
        }

        resource.createParent = function (model, success, error) {
            resource.save({ operation: 'createParent' }, model, success, error);
        }

        resource.getPagedParents = function (model, success, error) {
            resource.post({ operation: 'getPagedParents' }, model, success, error);
        }

        resource.addParent = function (model, success, error) {
            resource.post({ operation: 'addParent' }, model, success, error);
        }

        resource.assertAccountCharge = function (customerID, success, error) {
            resource.query({ operation: 'AssertAccountCharge', customerID: customerID }, success, error);
        }

        resource.getTransferResources = function (customerIds, success, error) {
            resource.post({ operation: 'getTransferResources' }, customerIds, success, error);
        }

        resource.transferCustomers = function (model, success, error) {
            resource.post({ operation: 'transferCustomers' }, model, success, error);
        };
        resource.transferCustomerTransferResources = function (model, success, error) {
            resource.post({ operation: 'transferCustomerTransferResources' }, model, success, error);
        };
        resource.createCustomerStaffRelations = function (model, success, error) {
            resource.post({ operation: 'createCustomerStaffRelations' }, model, success, error);
        };
        return resource;
    }]);

    customer.registerValue('customerRelationType', {
        creator: '0', // 建档关系
        consultant: '1', // 1 咨询关系: 学生, 销售（咨询师）
        educator: '2', // 2 学管关系：学生, 学管（班主任）
        teacher: '3', // 3 教学关系: 学生, 老师
        callcenter: '4', // 4 电销关系: 学生, 坐席人员
        market: '5' // 5 市场关系: 学生, 市场专员
    });

    customer.registerValue('customerAdvanceSearchItems', [
        { name: '入学年级：', template: '<ppts-checkbox-group category="grade" model="vm.criteria.entranceGrades" async="false"/>' },
        { name: '建档日期：', template: '<ppts-daterangepicker start-date="vm.criteria.createTimeStart" end-date="vm.criteria.createTimeEnd" width="41.5%" css="mcs-margin-left-10"/>' },
        { name: '跟进阶段：', template: '<ppts-checkbox-group category="followStage" model="vm.criteria.followStages" async="false"/>' },
        { name: '客户级别：', template: '<ppts-checkbox-group category="customerLevel" model="vm.criteria.customerLevels" width="60px" async="false"/>' },
        { name: '未跟进时长：', template: '<ppts-radiobutton-group category="period" model="vm.followPeriodValue" show-all="true" async="false" css="mcs-padding-left-10"/> <span ng-show="vm.followPeriodValue == 5"><input type="text" ng-model="vm.followDays" class="mcs-input-small" onkeyup="mcs.util.limit(this)" onafterpaste="mcs.util.limit(this)"/>天未跟进</span>' },
        { name: '在读学校：', template: '<mcs-input model="vm.criteria.schoolName" css="mcs-margin-left-10" custom-style="width:40%"/>' },
        { name: '家庭住址：', template: '<mcs-input model="vm.criteria.addressDetail" css="mcs-margin-left-10" custom-style="width:40%"/>' },
        { name: '信息来源：', template: '<ppts-checkbox-group category="source" model="vm.criteria.sourceMainType" parent="0" async="false"/>' },
        { name: '信息来源二：', template: '<ppts-checkbox-group category="source" model="vm.criteria.sourceSubType" parent="vm.criteria.sourceMainType" ng-show="vm.criteria.sourceMainType.length==1" async="false"/>', hide: 'vm.criteria.sourceMainType.length!=1' },
        { name: '归属坐席：', template: '<ppts-radiobutton-group category="assignment" model="vm.criteria.isAssignCallcenter" show-all="true" async="false" css="mcs-padding-left-10"/> <mcs-input placeholder="坐席姓名" model="vm.criteria.callcenterName" custom-style="width:28%" ng-disabled="vm.criteria.isAssignCallcenter==0"/>' },
        { name: '归属咨询师：', template: '<ppts-radiobutton-group category="assignment" model="vm.criteria.isAssignConsultant" show-all="true" async="false" css="mcs-padding-left-10"/> <mcs-input placeholder="咨询师姓名" model="vm.criteria.consultantName" custom-style="width:28%" ng-disabled="vm.criteria.isAssignConsultant==0"/>' },
        { name: '归属市场专员：', template: '<ppts-radiobutton-group category="assignment" model="vm.criteria.isAssignMarket" show-all="true" async="false" css="mcs-padding-left-10"/> <mcs-input placeholder="市场专员姓名" model="vm.criteria.marketName" custom-style="width:28%" ng-disabled="vm.criteria.isAssignMarket==0"/>' },
        { name: '建档人：', template: '<ppts-checkbox-group category="people" model="vm.criteria.creatorJobs" async="false" css="mcs-padding-left-10"/> <mcs-input placeholder="建档人姓名" model="vm.criteria.creatorName" custom-style="width:28%"/>' },
        { name: '有效/无效客户：', template: '<ppts-radiobutton-group category="valid" model="vm.criteria.isValids" show-all="true" async="false" css="mcs-padding-left-10"/>' }
    ]);

    customer.registerValue('customerListDataHeader', {
        selection: 'checkbox',
        rowsSelected: [],
        keyFields: ['customerID', 'customerName'],
        headers: [{
            field: "customerName",
            name: "学员姓名",
            template: '<a ui-sref="ppts.customer-view.profiles({id:row.customerID,prev:\'ppts.customer\'})">{{row.customerName}}</a>',
            sortable: true
        }, {
            field: "customerCode",
            name: "学员编号",
            template: '<a ui-sref="ppts.customer-view.profiles({id:row.customerID,prev:\'ppts.customer\'})">{{row.customerCode}}</a>',
        }, {
            field: "parentName",
            name: "家长姓名"
        }, {
            field: "grade",
            name: "当前年级",
            template: '<span>{{row.entranceGrade | grade | normalize}}</span>'
        }, {
            field: "sourceMainType",
            name: "信息来源",
            template: '<span>{{row.sourceMainType | source | normalize}}</span>'
        }, {
            field: 'orgName',
            name: "归属地"
        }, {
            field: "createTime",
            name: "建档日期",
            template: '<span>{{row.createTime | date:"yyyy-MM-dd HH:mm:ss" | normalize}}</span>'
        }, {
            field: "creatorName",
            name: "建档人"
        }, {
            field: "creatorName",
            name: "建档人岗位"
        }, {
            field: 'consultantStaff',
            name: "归属咨询师"
        }, {
            field: "followedCount",
            name: "跟进次数"
        }, {
            field: "followTime",
            name: "最后一次跟进时间",
            template: '<span>{{row.followTime | date:"yyyy-MM-dd" | normalize}}</span>'
        }, {
            field: 'marketStaff',
            name: "归属市场专员"
        }, {
            field: 'purchaseIntention',
            name: "购买意原",
            template: '<span>{{row.purchaseIntention | purchaseIntention | normalize}}</span>'
        }, {
            field: 'followStage',
            name: "跟进阶段",
            template: '<span>{{row.followStage | followStage | normalize}}</span>'
        }, {
            field: "customerLevel",
            name: "客户级别",
            template: '<span>{{row.customerLevel | customerLevel | normalize}}</span>'
        }, {
            field: 'nextFollowTime',
            name: "下次沟通时间",
            template: '<span>{{row.nextFollowTime | date:"yyyy-MM-dd" | normalize}}</span>'
        }],
        pager: {
            pageIndex: 1,
            pageSize: ppts.config.pageSizeItem,
            totalCount: -1
        },
        orderBy: [{ dataField: 'PotentialCustomers.CreateTime', sortDirection: 1 }]
    });

    customer.registerFactory('customerDataViewService', ['$state', 'customerDataService', 'dataSyncService', 'mcsDialogService', 'customerRelationType',
        function ($state, customerDataService, dataSyncService, mcsDialogService, customerRelationType) {
            var service = this;

            // 配置潜客列表表头
            service.configCustomerListHeaders = function (vm, header) {
                vm.data = header;

                vm.data.pager.pageChange = function () {
                    dataSyncService.initCriteria(vm);
                    customerDataService.getPagedCustomers(vm.criteria, function (result) {
                        vm.data.rows = result.pagedData;
                    });
                }
            };

            // 初始化潜客列表
            service.initCustomerList = function (vm, callback) {
                dataSyncService.initCriteria(vm);
                customerDataService.getAllCustomers(vm.criteria, function (result) {
                    vm.data.rows = result.queryResult.pagedData;
                    dataSyncService.injectDictData({
                        c_codE_ABBR_Customer_Assign: [{ key: 0, value: '未分配' }, { key: 1, value: '已分配' }],
                        c_codE_ABBR_Customer_Valid: [{ key: 0, value: '无效客户' }, { key: 1, value: '有效客户' }]
                    });
                    dataSyncService.injectPageDict(['people', 'period', 'ifElse']);
                    dataSyncService.updateTotalCount(vm, result.queryResult);
                    if (ng.isFunction(callback)) {
                        callback();
                    }
                });
            };

            service.initDatePeriod = function ($scope, vm, watchExps) {
                if (!watchExps && !watchExps.length) return;
                for (var index in watchExps) {
                    (function () {
                        var temp = index, exp = watchExps[index];
                        $scope.$watch(exp.watchExp, function (value) {
                            var period = dataSyncService.selectPageDict('period', vm[exp.selectedValue]);
                            if (period) {
                                vm.criteria[exp.end] = period.end || ((!value || value == '-1') ? null : mcs.date.lastDay(-value));
                            }
                        });
                    })();
                }
            };

            service.initWatchExps = function ($scope, vm, watchExps) {
                if (!watchExps && !watchExps.length) return;
                for (var index in watchExps) {
                    (function () {
                        var exp = watchExps[index];
                        $scope.$watch(exp.watchExp, function (value) {
                            if (value == exp.selectedValue) {
                                if (vm.criteria[exp.watch]) {
                                    vm.criteria[exp.watch] = '';
                                } else {
                                    vm[exp.watch] = '';
                                }
                            }
                        });
                    })();
                }
            };

            // 配置搜索潜客列表表头
            service.configCustomerSearchHeaders = function (vm) {
                vm.data = {
                    selection: 'radio',
                    rowsSelected: [],
                    keyFields: ['customerID', 'customerName', 'parentName', 'grade', 'consultantStaff', 'consultant', 'Market'],
                    headers: [{
                        field: "orgName",
                        name: "校区",
                        sortable: true
                    }, {
                        field: "customerName",
                        name: "学员姓名",
                        sortable: true
                    }, {
                        field: "customerCode",
                        name: "学员编号"
                    }, {
                        field: "parentName",
                        name: "家长姓名"
                    }, {
                        field: "grade",
                        name: "当前年级",
                        template: '<span>{{row.entranceGrade | grade}}</span>'
                    }, {
                        field: "consultantStaff",
                        name: "咨询师"
                    }, {
                        field: "primaryPhone",
                        name: "家长联系方式"
                    }
                    ],
                    pager: {
                        pageIndex: 1,
                        pageSize: ppts.config.pageSizeItem,
                        totalCount: -1,
                        pageChange: function () {
                            dataSyncService.initCriteria(vm, false);
                            customerDataService.getPagedCustomers(vm.criteria, function (result) {
                                vm.data.rows = result.pagedData;
                            });
                        }
                    },
                    orderBy: [{ dataField: 'PotentialCustomers.CreateTime', sortDirection: 1 }]
                }
            };

            // 初始化搜索潜客列表
            service.initSearchCustomerList = function (vm, callback) {
                dataSyncService.initCriteria(vm, false);
                customerDataService.getAllCustomers(vm.criteria, function (result) {
                    vm.data.rows = result.queryResult.pagedData;
                    dataSyncService.updateTotalCount(vm, result.queryResult);
                    if (ng.isFunction(callback)) {
                        callback();
                    }
                });
            };

            // 初始化新增潜客信息
            service.initCreateCustomerInfo = function (orginalParent, vm, callback) {
                customerDataService.getCustomerForCreate(function (result) {
                    orginalParent = result.primaryParent;
                    dataSyncService.injectPageDict(['ifElse']);
                    dataSyncService.setDefaultValue(vm.customer, result.customer, ['idType', 'subjectType', 'vipType']);
                    dataSyncService.setDefaultValue(vm.parent, result.primaryParent, 'idType');
                    if (ng.isFunction(callback)) {
                        callback();
                    }
                });
            };

            // 获取对话框标题
            service.getStaffRelationModalTitle = function (relationType) {
                switch (relationType) {
                    case customerRelationType.consultant:
                        return '咨询师';
                    case customerRelationType.educator:
                        return '学管师';
                    case customerRelationType.teacher:
                        return '教师';
                    case customerRelationType.callcenter:
                        return '坐席';
                    case customerRelationType.market:
                        return '市场专员';
                };
            };

            // 分配咨询师/学管师/坐席/市场专员
            service.assignStaffRelation = function (customers, relationType, vm) {
                mcsDialogService.create('app/customer/potentialcustomer/customer-staff-relations/assign-staff-relation.tpl.html', {
                    controller: 'customerStaffRelationController',
                    params: {
                        customers: customers,
                        relationType: relationType,
                        type: 'assignStaff'
                    }
                }).result.then(function () {
                    vm.search();
                });
            };

            // 资源划转
            service.transferCustomers = function (vm, customers) {
                mcsDialogService.create('app/customer/potentialcustomer/customer-transfer/customer-transfer.tpl.html', {
                    controller: 'customerTransferController',
                    params: {
                        customers: customers
                    }
                }).result.then(function () {
                    vm.search();
                });
            };

            // 查看咨询师/学管师/坐席/市场专员历史
            service.viewStaffRelation = function (id, relationType) {
                mcsDialogService.create('app/customer/potentialcustomer/customer-staff-relations/customer-staff-relations.tpl.html', {
                    controller: 'customerStaffRelationController',
                    params: {
                        customerId: id,
                        relationType: relationType,
                        type: 'viewHistory'
                    },
                    settings: { size: 'lg' }
                });
            };

            // 配置咨询师/学管师/坐席/市场专员Table表头
            service.configStaffRelationHeaders = function (vm) {
                vm.data = {
                    headers: [{
                        field: "createTime",
                        name: "开始时间",
                        template: '<span>{{ row.createTime | date:"yyyy-MM-dd" }}</span>'
                    }, {
                        field: "createTime",
                        name: "结束时间",
                        template: '<span>{{ row.createTime | date:"yyyy-MM-dd" }}</span>'
                    }, {
                        field: "orgName",
                        name: "所属分公司"
                    }, {
                        field: "orgName",
                        name: "所属校区"
                    }, {
                        field: "staffJobName",
                        name: "所属岗位"
                    }, {
                        field: "staffName",
                        name: "当前在岗者",
                        template: '<span>{{ row.staffName }}</span>'
                    }],
                    pager: {
                        pageIndex: 1,
                        pageSize: ppts.config.pageSizeItem,
                        totalCount: -1,
                        pageChange: function () {
                            dataSyncService.initCriteria(vm);
                            customerDataService.getPagedStaffRelations(vm.criteria, function (result) {
                                vm.data.rows = result.pagedData;
                            });
                        }
                    },
                    orderBy: [{
                        dataField: 'CreateTime', sortDirection: 1
                    }]
                }
            };

            // 加载咨询师/学管师/坐席/市场专员历史弹出框
            service.getStaffRelationInfo = function (vm, data) {
                dataSyncService.initCriteria(vm);
                vm.criteria.id = data.customerId;
                vm.criteria.relationType = data.relationType;
                vm.title = service.getStaffRelationModalTitle(data.relationType);
                customerDataService.getStaffRelations(vm.criteria, function (result) {
                    vm.data.rows = result.queryResult.pagedData;
                    dataSyncService.updateTotalCount(vm, result.queryResult);
                });
            };

            // 加载分配咨询师/学管师/坐席/市场专员弹出框
            service.getAssignStaffRelationInfo = function (vm, data, callback) {
                vm.displayNames = '';
                vm.customerNames = '';
                vm.customerIds = [];
                vm.title = service.getStaffRelationModalTitle(data.relationType);

                angular.forEach(data.customers, function (item, index) {
                    var length = data.customers.length;
                    if (index == 0) {
                        vm.displayNames += length == 1 ? item.customerName : item.customerName + '...';
                    }
                    vm.customerNames += index == 0 ? item.customerName : ',' + item.customerName;
                    vm.customerIds.push(item.customerID);
                });

                dataSyncService.injectPageDict(['messageType']);

                if (ng.isFunction(callback)) {
                    callback();
                }

                //customerDataService.getStaffRelations(vm.criteria, function (result) {
                //    vm.data.rows = result.queryResult.pagedData;
                //    dataSyncService.updateTotalCount(vm, result.queryResult);
                //});
            };

            // 加载资源划转弹出框
            service.getTransferCustomerInfo = function (vm, data, callback) {
                vm.displayNames = '';
                vm.customerNames = '';
                vm.customerIds = [];

                angular.forEach(data.customers, function (item, index) {
                    var length = data.customers.length;
                    if (index == 0) {
                        vm.displayNames += length == 1 ? item.customerName : item.customerName + '...';
                    }
                    vm.customerNames += index == 0 ? item.customerName : ',' + item.customerName;
                    vm.customerIds.push(item.customerID);
                });

                dataSyncService.injectPageDict(['messageType']);

                customerDataService.getTransferResources(vm.customerIds, function (result) {
                    // 初始化分公司和校区下拉框
                });
            };

            // 归属教师
            service.viewHistoryTeachers = function (customerID) {
                mcsDialogService.create('app/customer/potentialcustomer/customer-teacher-relations/customer-teacher-relations.tpl.html', {
                    controller: 'customerTeacherRelationController',
                    params: {
                        customerID: customerID
                    },
                    settings: { size: 'lg' }
                });
            };

            // 配置归属教师Table表头
            service.configTeacherRelationsHeaders = function (vm) {
                vm.data = {
                    headers: [{
                        field: "applyType",
                        name: "操作类型",
                        template: '<span>{{ row.applyType | teacherApplyType }}</span>'
                    }, {
                        field: "createTime",
                        name: "操作日期",
                        template: '<span>{{ row.createTime | date:"yyyy-MM-dd" }}</span>'
                    }, {
                        field: "orgName",
                        name: "所属机构",
                        template: '<span ng-if="row.applyType==0 || row.applyType==1">{{ row.newTeacherJobOrgName }}</span>'
                                + '<span ng-if="row.applyType==2">{{ row.oldTeacherJobOrgName }}</span>'
                    }, /*{
                        field: "orgName",
                        name: "岗位性质"
                    },*/ {
                        field: "staffJobName",
                        name: "教师姓名",
                        template: '<span ng-if="row.applyType==0">{{ row.newTeacherName }}</span>'
                                + '<span ng-if="row.applyType==1">{{ row.oldTeacherName }}>{{ row.newTeacherName }}</span>'
                                + '<span ng-if="row.applyType==2">{{ row.oldTeacherName }}</span>'
                    }],
                    pager: {
                        pageIndex: 1,
                        pageSize: ppts.config.pageSizeItem,
                        totalCount: -1,
                        pageChange: function () {
                            dataSyncService.initCriteria(vm);
                            customerDataService.getPagedStaffRelations(vm.criteria, function (result) {
                                vm.data.rows = result.pagedData;
                            });
                        }
                    },
                    orderBy: [{
                        dataField: 'CreateTime', sortDirection: 1
                    }]
                }
            };

            // 加载归属教师弹出框
            service.getTeacherRelationsInfo = function (vm, data) {
                dataSyncService.initCriteria(vm);
                vm.criteria.customerID = vm.customerID;
                customerDataService.getTeacherRelations(vm.criteria, function (result) {
                    vm.data.rows = result.queryResult.pagedData;
                    dataSyncService.updateTotalCount(vm, result.queryResult);
                });
            };

            return service;
        }]);

    customer.registerFactory('customerParentService', ['customerDataService', 'customerRelationService', 'dataSyncService', 'mcsDialogService', 'utilService', function (customerDataService, customerRelationService, dataSyncService, mcsDialogService, util) {
        var service = this;

        // 添加已有家长
        service.popupParentAdd = function (vm, title, type, callback) {
            mcsDialogService.create('app/customer/potentialcustomer/customer-parent-add/parent-add.tpl.html', {
                controller: 'customerParentAddController',
                params: {
                    title: title,
                    type: type,
                    customer: vm.customer
                },
                settings: {
                    size: 'lg'
                }
            }).result.then(function (parent) {
                vm.parent = parent;
                if (ng.isFunction(callback)) {
                    callback();
                }
            });
        };

        // 配置添加已有家长Table表头
        service.configParentAddHeaders = function (vm) {
            vm.data = {
                selection: 'radio',
                rowsSelected: [],
                keyFields: ['parentID', 'parentName', 'gender'],
                headers: [{
                    field: "parentName",
                    name: "家长姓名"
                }, {
                    field: "gender",
                    name: "性别",
                    template: '<span>{{ row.gender | gender }}</span>'
                }, {
                    name: "家长手机",
                    template: '<span ng-if="row.primaryPhone.phoneNumber != \'\'">{{ row.primaryPhone.phoneNumber }}</span><span ng-if="row.secondaryPhone.phoneNumber != \'\'"> {{ row.secondaryPhone.phoneNumber }}</span>'
                }, {
                    field: "createTime",
                    name: "建档时间",
                    template: '<span>{{ row.createTime | date:"yyyy-MM-dd" }}</span>'
                }],
                pager: {
                    pageIndex: 1,
                    pageSize: ppts.config.pageSizeItem,
                    totalCount: -1,
                    pageChange: function () {
                        dataSyncService.initCriteria(vm);
                        customerDataService.getPagedParents(vm.criteria, function (result) {
                            vm.data.rows = result.pagedData;
                        });
                    }
                },
                orderBy: [{ dataField: 'a.CreateTime', sortDirection: 1 }]
            }
        };

        // 加载家长弹出框
        service.getAllParents = function (vm, data, callback) {
            dataSyncService.initCriteria(vm);
            vm.title = data.title;
            vm.type = data.type;
            vm.customer = data.customer;
            vm.parent = {};
            customerDataService.getAllParents(vm.criteria, function (result) {
                vm.data.rows = result.queryResult.pagedData;
                dataSyncService.updateTotalCount(vm, result.queryResult);
                if (ng.isFunction(callback)) {
                    callback();
                }
            });
        };

        // 亲属关系切换
        service.initCustomerParentRelation = function ($scope, vm, lastIndex) {
            $scope.$watch('vm.parent.gender', function () {
                if (!vm.parent || !vm.parent.gender) return;
                service.updateParentRole(vm);
            });

            $scope.$watchCollection('vm.data.rowsSelected', function () {
                if (vm.type == 'add') return;
                var selectedRows = vm.data.rowsSelected;
                if (selectedRows && selectedRows.length == 1) {
                    vm.parent = selectedRows[0];
                }
            });

            $scope.$watch('vm.customer.gender', function () {
                if (!vm.customer || !vm.customer.gender) return;
                vm.customerRoles = mcs.util.mapping(customerRelationService.children(vm.customer.gender), { key: 'sid', value: 'sr' });
                vm.customerRole = vm.customerRoles && vm.customerRoles.length > 0 && lastIndex > -1 ? vm.customerRoles[lastIndex].key : 0;
                service.updateParentRole(vm);
            });

            $scope.selectCustomerRole = function (item, model) {
                vm.parentRoles = mcs.util.mapping(customerRelationService.myParents(model, vm.parent.gender), { key: 'pid', value: 'pr' });
                lastIndex = mcs.util.indexOf(vm.customerRoles, 'key', vm.customerRole);

                service.updateParentRole(vm, false);
            };
        };

        // 更新亲属关系
        service.updateParentRole = function (vm, needUpdateParentRoles) {
            needUpdateParentRoles = needUpdateParentRoles || true;
            if (needUpdateParentRoles) {
                vm.parentRole = 0;
                if (vm.customerRole) {
                    vm.parentRoles = mcs.util.mapping(customerRelationService.myParents(vm.customerRole, vm.parent.gender), { key: 'pid', value: 'pr' });
                } else {
                    vm.parentRoles = mcs.util.mapping(customerRelationService.parents(vm.parent.gender), { key: 'pid', value: 'pr' });
                }
            }

            if (vm.parentRoles.length == 1) {
                vm.parentRole = vm.parentRoles[0].key;
            } else {
                vm.parentRole = 0;
            }
        };

        return service;
    }]);
});