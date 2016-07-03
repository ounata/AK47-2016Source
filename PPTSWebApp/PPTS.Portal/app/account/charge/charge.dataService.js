define([ppts.config.modules.account], function (account) {

    account.registerFactory('accountChargeDataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.customerApiBaseUrl + 'api/accounts/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false }
            });

        //根据oa编码获取老师信息
        resource.getTeacher = function (campusID, oaCode, success, error) {
            resource.query({ operation: 'GetTeacher', campusID: campusID, oaCode: oaCode }, success, error);
        }

        //查询缴费单列表
        resource.queryChargeApplyList = function (criteria, success, error) {
            resource.post({ operation: 'QueryChargeApplyList' }, criteria, success, error);
        }

        //分页查询缴费单列表
        resource.queryPagedChargeApplyList = function (criteria, success, error) {
            resource.post({ operation: 'QueryPagedChargeApplyList' }, criteria, success, error);
        }

        //查询收款列表
        resource.queryChargePaymentList = function (criteria, success, error) {
            resource.post({ operation: 'QueryChargePaymentList' }, criteria, success, error);
        }

        //分页查询收款列表
        resource.queryPagedChargePaymentList = function (criteria, success, error) {
            resource.post({ operation: 'QueryPagedChargePaymentList' }, criteria, success, error);
        }

        //根据学员ID获取充值记录
        resource.getChargeApplyList = function (customerID, success, error) {
            resource.query({ operation: 'GetChargeApplyList', customerID: customerID }, success, error);
        }

        //根据学员ID获取缴费单申请信息
        resource.getChargeApplyByApplyID = function (id, success, error) {
            resource.query({ operation: 'GetChargeApplyByApplyID', id: id }, success, error);
        }

        //根据学员ID获取缴费单申请信息
        resource.getChargeApplyByCustomerID = function (id, success, error) {
            resource.query({ operation: 'GetChargeApplyByCustomerID', id: id }, success, error);
        }

        //根据缴费申请单ID获取缴费单申请信息（针对支付）
        resource.getChargeApplyByApplyID4Payment = function (id, success, error) {
            resource.query({ operation: 'GetChargeApplyByApplyID4Payment', id: id }, success, error);
        }

        //根据缴费申请单ID获取缴费单申请信息（针对业绩分配）
        resource.getChargeApplyByApplyID4Allot = function (id, success, error) {
            resource.query({ operation: 'GetChargeApplyByApplyID4Allot', id: id }, success, error);
        }

        //根据缴费支付单ID获取缴费单申请信息
        resource.getChargeApplyByPayID = function (id, success, error) {
            resource.query({ operation: 'GetChargeApplyByPayID', id: id }, success, error);
        }

        //根据校区ID，流水号获取刷卡记录
        resource.getPosRecord = function (campusID, payTicket, payType, success, error) {
            resource.query({ operation: 'GetPosRecord', campusID: campusID, payTicket: payTicket, payType: payType }, success, error);
        }

        //保存缴费申请
        resource.saveChargeApply = function (apply, success, error) {
            resource.post({ operation: 'SaveChargeApply' }, apply, success, error);
        }

        //删除缴费申请
        resource.deleteChargeApply = function (applyID, success, error) {
            var apply = { applyID: applyID };
            resource.post({ operation: 'DeleteChargeApply' }, apply, success, error);
        }

        //保存业绩归属
        resource.saveChargeAllot = function (allot, success, error) {
            resource.post({ operation: 'SaveChargeAllot' }, allot, success, error);
        }

        //保存缴费支付单
        resource.saveChargePayment = function (payment, success, error) {
            resource.post({ operation: 'SaveChargePayment' }, payment, success, error);
        }

        //对账缴费支付单
        resource.checkChargePayment = function (payIDs, success, error) {
            resource.post({ operation: 'CheckChargePayment' }, payIDs, success, error);
        }

        //保存缴费支付单打印结果
        resource.printChargePayment = function (payID, success, error) {
            var print = { payID: payID };
            resource.post({ operation: 'PrintChargePayment' }, print, success, error);
        }

        //获取登记发票信息 列表
        resource.getAccountChargeInvoiceCollection = function (criteria, success, error) {
            resource.post({ operation: 'getAccountChargeInvoiceCollection' }, criteria, success, error);
        }
        //获取登记发票信息 分页事件
        resource.getPagedAccountChargeInvoiceCollection = function (criteria, success, error) {
            resource.post({ operation: 'getPagedAccountChargeInvoiceCollection' }, criteria, success, error);
        }
        //初始化登记发票信息
        resource.initAccountChargeInvoice = function (success, error) {
            resource.post({ operation: 'initAccountChargeInvoice' }, success, error);
        }
        //获取单个登记发票信息实体
        resource.getAccountChargeInvoice = function (invoiceID,success, error) {
            resource.query({ operation: 'getAccountChargeInvoice', invoiceID : invoiceID }, success, error);
        }

        //保存登记发票信息
        resource.saveAccountChargeInvoice = function (model, success, error) {
            resource.post({ operation: 'saveAccountChargeInvoice' }, model, success, error);
        }
        
        return resource;
    }]);

    account.registerValue('chargeQueryAdvanceSearchItems', [
        { name: '当时年级：', template: '<ppts-checkbox-group category="grade" model="vm.criteria.grades" async="false"/>' },
        { name: '建档日期：', template: '<mcs-daterangepicker start-date="vm.criteria.customerCreateTimeStart" end-date="vm.criteria.customerCreateTimeEnd" width="41.5%" css="mcs-margin-left-10"/>' },
        { name: '充值日期：', template: '<mcs-daterangepicker start-date="vm.criteria.payTimeStart" end-date="vm.criteria.payTimeEnd" width="41.5%" css="mcs-margin-left-10"/>' },
        { name: '充值金额：', template: '<mcs-datarange min="vm.criteria.chargeMoneyStart" max="vm.criteria.chargeMoneyEnd" min-text="充值金额起" max-text="充值金额止" width="42%" css="mcs-padding-left-10"/>' },
        { name: '充值类型：', template: '<ppts-checkbox-group category="chargeType" model="vm.criteria.chargeTypes" width="150px" async="false"/>' },
        { name: '充值状态：', template: '<ppts-checkbox-group category="payStatus" model="vm.criteria.payStatuses" width="150px" async="false"/>' },
        { name: '审核状态：', template: '<ppts-checkbox-group category="aduitStatus" model="vm.criteria.aduitStatuses" width="150px" async="false"/>' },
        { name: '信息来源：', template: '<ppts-checkbox-group category="source" model="vm.criteria.sourceMainTypes" width="150px" parent="0" async="false"/>' },
        { name: '信息来源二：', template: '<ppts-checkbox-group category="source" model="vm.criteria.sourceSubTypes" width="150px" parent="vm.criteria.sourceMainTypes" ng-show="vm.criteria.sourceMainTypes.length==1" async="false"/>', hide: 'vm.criteria.sourceMainTypes.length!=1' },
        { name: '归属关系：', template: '<ppts-checkbox-group category="relation" model="vm.criteria.belongRelationTypes" width="150px" async="false" css="mcs-padding-left-10"/> <mcs-input placeholder="归属人姓名" model="vm.criteria.belongerName" custom-style="width:28%"/>' },
        { name: '建档关系：', template: '<ppts-checkbox-group category="creation" model="vm.criteria.customerCreatorJobTypes" width="150px" async="false" css="mcs-padding-left-10"/> <mcs-input placeholder="建档人姓名" model="vm.criteria.customerCreatorName" custom-style="width:28%"/>' },
        { name: '查询部门：', template: '<ppts-checkbox-group category="dept" model="vm.criteria.queryDepts" width="150px" async="false" css="mcs-padding-left-10"/>' },
    ]);

    account.registerValue('paymentQueryAdvanceSearchItems', [
        { name: '充值日期：', template: '<mcs-daterangepicker start-date="vm.criteria.payTimeStart" end-date="vm.criteria.payTimeEnd" width="41.5%" css="mcs-margin-left-10"/>' },
        { name: '充值类型：', template: '<ppts-checkbox-group category="chargeType" model="vm.criteria.chargeTypes" width="150px" async="false"/>' },
        { name: '收款类型：', template: '<ppts-checkbox-group category="payType" model="vm.criteria.payTypes" width="150px" async="false"/>' },
        { name: '对账状态：', template: '<ppts-checkbox-group category="checkStatus" model="vm.criteria.checkStatuses" width="150px" async="false"/>' },
    ]);

    account.registerValue('chargeQueryTable', {

        selection: 'checkbox',
        rowsSelected: [],
        keyFields: ['applyID'],
        headers: [{
            field: "customerName",
            name: "学员姓名",
            template: '<a ui-sref="ppts.customer-view.profiles({id:row.customerID,prev:\'ppts.accountCharge-query\'})" ng-show="row.isPotential" >{{row.customerName}}</a><a ui-sref="ppts.student-view.profiles({id:row.customerID,prev:\'ppts.accountCharge-query\'})" ng-show="!row.isPotential" >{{row.customerName}}</a>'
        }, {
            field: "customerCode",
            name: "学员编号"
        }, {
            field: "parentName",
            name: "家长姓名"
        }, {
            field: "applyNo",
            name: "缴费单编号",
            template: '<span><a ui-sref="ppts.accountCharge-view.info({applyID:row.applyID,prev:\'ppts.accountCharge-query\'})">{{row.applyNo}}</span>'
        }, {
            field: "chargeType",
            name: "充值类型",
            template: '<span>{{row.chargeType | chargeType}}</span>'
        }, {
            field: "payStatus",
            name: "充值状态",
            template: '<span>{{row.payStatus | payStatus}}</span>'
        }, {
            field: "auditStatus",
            name: "审核状态",
            template: '<span>{{row.auditStatus | chargeAuditStatus}}</span>'
        }, {
            field: "chargeMoney",
            name: "充值金额",
            template: '<span>{{row.chargeMoney | currency:"￥"}}</span>'
        }, {
            field: "payTime",
            name: "充值日期",
            template: '<span>{{row.payTime | date:"yyyy-MM-dd" | normalize }}</span>'
        }, {
            field: "thisDiscountBase",
            name: "折扣基数",
            template: '<span>{{row.thisDiscountBase | currency:"￥"}}</span>'
        }, {
            field: "thisDiscountRate",
            name: "折扣率",
            template: '<span>{{row.thisDiscountRate | number:"2"}}</span>'
        }, {
            field: "applierName",
            name: "申请人"
        }, {
            field: "applierJobName",
            name: "申请人岗位"
        }, {
            field: "campusName",
            name: "申请人所在地"
        }, {
            field: "customerGrade",
            name: "当时年级",
            template: '<span>{{row.customerGrade | grade}}</span>'
        }],
        orderBy: [{ dataField: 'applyTime', sortDirection: 1 }]
    });

    account.registerValue('chargePaymentQueryTable', {

            selection: 'checkbox',
            rowsSelected: [],
            keyFields: ['payID'],
            headers: [{
                field: "customerName",
                name: "学员姓名",
                template: '<a ui-sref="ppts.student-view.profiles({id:row.customerID,prev:\'ppts.accountChargePayment-query\'})">{{row.customerName}}</a>'
            }, {
                field: "customerCode",
                name: "学员编号",
                template: '<span>{{row.customerCode}}</span>'
            }, {
                field: "applyNo",
                name: "缴费单编号",
                template: '<span><a ui-sref="ppts.accountCharge-view.info({applyID:row.applyID,prev:\'ppts.accountChargePayment-query\'})">{{row.applyNo}}</span>'
            }, {
                field: "chargeMoney",
                name: "充值金额",
                template: '<span>{{row.chargeMoney | currency:"￥"}}</span>'
            }, {
                field: "payeeName",
                name: "收款人",
                template: '<span>{{row.payeeName}}</span>'
            }, {
                field: "payMoney",
                name: "收款金额",
                template: '<span>{{row.payMoney | currency:"￥"}}</span>'
            }, {
                field: "payType",
                name: "收款类型",
                template: '<span>{{row.payType | payType}}</span>'
            }, {
                field: "payTime",
                name: "收款时间",
                template: '<span>{{row.payTime | date:"yyyy-MM-dd HH:mm"}}</span>'
            }, {
                field: "campusName",
                name: "校区",
                template: '<span>{{row.campusName}}</span>'
            }, {
                field: "checkStatus",
                name: "对账状态",
                template: '<span ng-class="{1: \'ppts-checked-color\', 0: \'ppts-unchecked-color\'}[{{row.checkStatus}}]">{{row.checkStatus | checkStatus}}</span>'
            }],
            orderBy: [{ dataField: 'a.payTime', sortDirection: 1 }]
        });
});