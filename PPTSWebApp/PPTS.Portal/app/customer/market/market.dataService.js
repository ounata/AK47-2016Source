define([ppts.config.modules.customer], function (customer) {
    customer.registerValue('marketAdvanceSearchItems', [
        { name: '入学年级：', template: '<ppts-checkbox-group category="grade" model="vm.criteria.entranceGrades" async="false"/>' },
        { name: '建档日期：', template: '<ppts-daterangepicker start-date="vm.criteria.createTimeStart" end-date="vm.criteria.createTimeEnd" width="41.5%" css="mcs-margin-left-10"/>' },
        { name: '充值日期：', template: '<ppts-daterangepicker start-date="vm.criteria.payTimeStart" end-date="vm.criteria.payTimeEnd" width="41.5%" css="mcs-margin-left-10"/>' },
        { name: '充值金额：', template: '<ppts-datarange min="vm.criteria.payAmountMin" max="vm.criteria.payAmountMax" min-text="充值金额起" max-text="充值金额止" width="42%" css="mcs-padding-left-10"/>' },
        { name: '跟进阶段：', template: '<ppts-checkbox-group category="followStage" model="vm.criteria.followStages" async="false"/>' },
        { name: '客户级别：', template: '<ppts-checkbox-group category="customerLevel" model="vm.criteria.customerLevels" width="60px" async="false"/>' },
        { name: '未跟进时长：', template: '<ppts-radiobutton-group category="period" model="vm.followPeriodValue" async="false" css="mcs-padding-left-10"/> <span ng-show="vm.followPeriodValue == 5"><input type="text" ng-model="vm.followDays" class="mcs-input-small" onkeyup="mcs.util.limit(this)" onafterpaste="mcs.util.limit(this)"/>天未跟进</span>' },
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

    customer.registerValue('marketListDataHeader', {
        selection: 'checkbox',
        rowsSelected: [],
        keyFields: ['customerID', 'customerName'],
        headers: [{
            field: "customerName",
            name: "学员姓名",
            template: '<a ui-sref="ppts.customer-view.profiles({id:row.customerID,prev:\'ppts.market\'})">{{row.customerName}}</a>',
            sortable: true
        }, {
            field: "customerCode",
            name: "学员编号",
            template: '<a ui-sref="ppts.customer-view.profiles({id:row.customerID,prev:\'ppts.market\'})">{{row.customerCode}}</a>',
        }, {
            field: "parentName",
            name: "家长姓名"
        }, {
            field: "grade",
            name: "当前年级",
            template: '<span>{{row.entranceGrade | grade}}</span>'
        }, {
            field: "sourceMainType",
            name: "信息来源",
            template: '<span>{{row.sourceMainType | source}}</span>'
        }, {
            name: "归属地",
            template: '<span></span>'
        }, {
            field: "createTime",
            name: "建档日期",
            template: '<span>{{row.createTime | date:"yyyy-MM-dd"}}</span>'
        }, {
            field: "createTime",
            name: "建档人",
            template: '<span>{{row.creatorName}}</span>',
            description: 'customer creatorName'
        }, {
            name: "建档人岗位",
            template: '<span></span>'
        }, {
            name: "归属咨询师",
            template: '<span></span>'
        }, {
            name: "跟进次数",
            template: '<span></span>'
        }, {
            field: "lastFollowupTime",
            name: "最后一次跟进时间",
            template: '<span>{{row.lastFollowupTime | date:"yyyy-MM-dd"}}</span>',
            description: 'customer followTime'
        }, {
            name: "归属市场专员",
            template: '<span></span>'
        }, {
            name: "购买意原",
            template: '<span></span>'
        }, {
            name: "跟进阶段",
            template: '<span></span>'
        }, {
            field: "vipLevel",
            name: "客户级别",
            template: '<span>{{row.vipLevel | vipLevel}}</span>',
            description: 'customer vipLevel'
        }, {
            name: "下次沟通时间",
            template: '<span></span>'
        }, {
            name: "是否签约",
            template: '<span></span>'
        }],
        pager: {
            pageIndex: 1,
            pageSize: ppts.config.pageSizeItem,
            totalCount: -1
        },
        orderBy: [{ dataField: 'PotentialCustomers.CreateTime', sortDirection: 1 }]
    });
});