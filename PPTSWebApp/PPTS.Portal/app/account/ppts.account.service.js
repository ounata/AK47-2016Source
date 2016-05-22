define([ppts.config.modules.account], function (account) {
    
    account.registerFactory('accountDataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.customerApiBaseUrl + 'api/accounts/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false }
            });

        //根据oa编码获取老师信息
        resource.getTeacher = function (oaCode, success, error) {
            resource.query({ operation: 'GetTeacher', oaCode: oaCode }, success, error);
        }
    });

    account.registerFactory('accountAllotDataService', ['accountDataService', 'mcsDialogService', 
        function (accountDataService, mcsDialogService) {
            var service = this;

            // 配置业绩分派编辑表头
            service.configEditHeaders = function (vm) {

                vm.data = {
                    selection: 'checkbox',
                    rowsSelected: [],
                    keyFields: ['sortNo'],
                    headers: [{
                        field: "teacherOACode",
                        name: "OA账号",
                        headerCss: "col-sm-2",
                        template: '<span class="col-sm-2"><mcs-input model="row.teacherOACode" ng-blur="vm.fetchTeacher(row)" required="true" /></span>'
                    }, {
                        field: "teacherName",
                        name: "教师姓名",
                        headerCss: "col-sm-2",
                        template: '<span>{{row.teacherName}}</span>'
                    }, {
                        field: "subject",
                        name: "科目",
                        headerCss: "col-sm-2",
                        template: '<span><ppts-select category="subject" model="row.subject" caption="科目" async="false"  required="true"/></span>'
                    }, {
                        field: "categoryType",
                        name: "产品类型",
                        headerCss: "col-sm-2",
                        template: '<span><ppts-select category="categoryType" model="row.categoryType" caption="产品类型"  async="false"  required="true"/></span>'
                    }, {
                        name: "岗位",
                        headerCss: "col-sm-1",
                        template: '<span>校教学教师</span>'
                    }, {
                        field: "teacherType",
                        name: "教师类型",
                        headerCss: "col-sm-1",
                        template: '<span><ppts-select category="teacherType" model="row.teacherType" caption="教师类型"  async="false"  required="true"/></span>'
                    }, {
                        field: "allotMoney",
                        name: "金额",
                        template: '<span><mcs-input model="row.allotMoney" type="number" ng-blur="vm.calcAllot()"  required="true"/></span>'
                    }, {
                        field: "allotAmount",
                        name: "课时",
                        template: '<span><mcs-input model="row.allotAmount" type="number" ng-blur="vm.calcAllot()"  required="true"/></span>'
                    }],
                    pager: {
                        pagable: false
                    },
                    orderBy: [{ dataField: 'sortNo', sortDirection: 1 }]
                }
            };

            // 配置业绩分派查看表头
            service.configInfoHeaders = function (vm)
            {
                vm.data = {
                    selection: 'checkbox',
                    rowsSelected: [],
                    keyFields: ['sortNo'],
                    headers: [{
                        field: "teacherOACode",
                        name: "OA账号",
                        headerCss: "col-sm-2",
                        template: '<span>{{row.teacherOACode}}</span>'
                    }, {
                        field: "teacherName",
                        name: "教师姓名",
                        headerCss: "col-sm-2",
                        template: '<span>{{row.teacherName}}</span>'
                    }, {
                        field: "subject",
                        name: "科目",
                        headerCss: "col-sm-2",
                        template: '<span>{{row.subject | subject}}</span>'
                    }, {
                        field: "categoryType",
                        name: "产品类型",
                        headerCss: "col-sm-2",
                        template: '<span>{{row.categoryType | categoryType}}</span>'
                    }, {
                        name: "岗位",
                        headerCss: "col-sm-1",
                        template: '<span>校教学教师</span>'
                    }, {
                        field: "teacherType",
                        name: "教师类型",
                        headerCss: "col-sm-1",
                        template: '<span>{{row.tearcherType | teacherType}}</span>'
                    }, {
                        field: "allotMoney",
                        name: "金额",
                        template: '<span>{{row.allotMoney | currency:"￥"}}</span>'
                    }, {
                        field: "allotAmount",
                        name: "课时",
                        template: '<span>{{row.allotAmount}}</span>'
                    }],
                    pager: {
                        pagable: false
                    },
                    orderBy: [{ dataField: 'sortNo', sortDirection: 1 }]
                }
            }

            //业绩分配表添加行
            service.addRow = function (vm) {

                var index = 0;
                if (vm.data.rows.length != 0) {
                    index = vm.data.rows[vm.data.rows.length - 1].sortNo + 1;
                }
                vm.data.rows.push({
                    applyID: vm.apply.applyID,
                    sortNo: index,
                    teacherID: null,
                    teacherName: null,
                    teacherType: null,
                    teacherOACode: null,
                    subject: null,
                    categoryType: null,
                    allotAmount: null,
                    allotMoney: null
                });
            }
            
            //业绩分配表删除行
            service.removeRow = function (vm) {

                mcs.util.removeByObjectsWithKeys(vm.data.rows, vm.data.rowsSelected);
                vm.calcTotal();
            }
            
            //获取老师信息
            service.removeRow = function (vm, row) {
                if (row.teacherOACode && row.teacherOACode != '') {
                    accountDataService.getTeacher(row.teacherOACode, function (result) {
                        row.teacherID = null;
                        row.teacherName = null;
                        if (result) {
                            row.teacherID = result.teacherID;
                            row.teacherName = result.teacherName;
                            row.teacherOACode = result.teacherOACode;
                        }
                    });
                }
            }

            //计算业绩分配总额
            service.calcAllot = function (vm) {

                var totalMoney = 0;
                var totalAmount = 0;

                for (var i = 0; i < vm.apply.allot.items.length; i++) {

                    var item = vm.apply.allot.items[i];
                    if (item.allotMoney) {
                        totalMoney += item.allotMoney;
                    }
                    if (item.allotAmount) {
                        totalAmount += item.allotAmount;
                    }
                }
                vm.apply.allot.totalMoney = totalMoney;
                vm.apply.allot.totalAmount = totalAmount;
            }
            return service;
        }]);

});