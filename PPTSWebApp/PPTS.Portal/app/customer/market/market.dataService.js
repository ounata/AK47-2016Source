define([ppts.config.modules.customer], function (customer) {
    customer.registerFactory('marketDataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.customerApiBaseUrl + 'api/potentialcustomers/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false },
                'fetch': { method: 'GET', isArray: true }
            });

        resource.getAllMarketCustomers = function (criteria, success, error) {
            resource.post({ operation: 'getAllMarketCustomers' }, criteria, success, error);
        }

        resource.getPagedMarketCustomers = function (criteria, success, error) {
            resource.post({ operation: 'getPagedMarketCustomers' }, criteria, success, error);
        }

        return resource;
    }]);

    customer.registerValue('marketAdvanceSearchItems', [
        { name: '入学年级：', template: '<ppts-checkbox-group category="grade" model="vm.criteria.entranceGrades" async="false"/>' },
        { name: '建档日期：', template: '<mcs-daterangepicker start-date="vm.criteria.createTimeStart" end-date="vm.criteria.createTimeEnd" width="41%" css="mcs-margin-left-10"/>' },
        { name: '充值日期：', template: '<mcs-daterangepicker start-date="vm.criteria.payTimeStart" end-date="vm.criteria.payTimeEnd" width="41%" css="mcs-margin-left-10"/>' },
        { name: '充值金额：', template: '<mcs-datarange min="vm.criteria.payAmountMin" max="vm.criteria.payAmountMax" min-text="充值金额起" max-text="充值金额止" width="41.8%"/>' },
        { name: '跟进阶段：', template: '<ppts-checkbox-group category="followStage" model="vm.criteria.followStages" async="false"/>' },
        { name: '客户级别：', template: '<ppts-checkbox-group category="customerLevel" model="vm.criteria.customerLevels" width="60px" async="false"/>' },
        { name: '未跟进时长：', template: '<ppts-radiobutton-group category="period" model="vm.criteria.followPeriodValue" async="false" show-all="true" css="mcs-padding-left-10"/> <span ng-show="vm.criteria.followPeriodValue == 5"><input type="text" ng-model="vm.followDays" class="mcs-input-small" onkeyup="mcs.util.limit(this)" onafterpaste="mcs.util.limit(this)"/>天未跟进</span>' },
        { name: '在读学校：', template: '<ppts-school name="vm.criteria.schoolName" css="mcs-padding-left-10 mcs-width-half"/>' },
        { name: '家庭住址：', template: '<ppts-address model="vm.criteria.addressDetail" extra="true" css="mcs-padding-left-10 mcs-width-half"/>' },
        { name: '信息来源：', template: '<ppts-checkbox-group category="source" model="vm.criteria.sourceMainType" parent="0" async="false"/>' },
        { name: '信息来源二：', template: '<ppts-checkbox-group category="source" model="vm.criteria.sourceSubType" parent="vm.criteria.sourceMainType" ng-show="vm.criteria.sourceMainType.length==1" async="false"/>', hide: 'vm.criteria.sourceMainType.length!=1' },
        { name: '归属坐席：', template: '<ppts-radiobutton-group category="assignment" model="vm.criteria.isAssignCallcenter" show-all="true" async="false" css="mcs-padding-left-10"/> <mcs-input placeholder="坐席姓名" uib-popover="坐席姓名" model="vm.criteria.callcenterName" css="mcs-margin-left-5" custom-style="width:35%" ng-disabled="vm.criteria.isAssignCallcenter==0"/>' },
        { name: '归属咨询师：', template: '<ppts-radiobutton-group category="assignment" model="vm.criteria.isAssignConsultant" show-all="true" async="false" css="mcs-padding-left-10"/> <mcs-input placeholder="咨询师姓名" uib-popover="咨询师姓名" model="vm.criteria.consultantName" css="mcs-margin-left-5" custom-style="width:35%" ng-disabled="vm.criteria.isAssignConsultant==0"/>' },
        { name: '归属市场专员：', template: '<ppts-radiobutton-group category="assignment" model="vm.criteria.isAssignMarket" show-all="true" async="false" css="mcs-padding-left-10"/> <mcs-input placeholder="市场专员姓名" uib-popover="市场专员姓名" model="vm.criteria.marketName" css="mcs-margin-left-5" custom-style="width:35%" ng-disabled="vm.criteria.isAssignMarket==0"/>' },
        { name: '有效/无效客户：', template: '<ppts-radiobutton-group category="valid" model="vm.criteria.isValids" show-all="true" async="false" css="mcs-padding-left-10"/>' },
        { name: '客户分类：', template: '<ppts-radiobutton-group category="customerType" model="vm.criteria.customerType" show-all="true" async="false" css="mcs-padding-left-10"/>' },
        { name: '建档人：', template: '<mcs-input placeholder="建档人姓名" uib-popover="建档人姓名" model="vm.criteria.creatorName" css="mcs-margin-left-10 mcs-width-half"/>' }
    ]);

    customer.registerValue('marketListDataHeader', {
        selection: 'checkbox',
        rowsSelected: [],
        keyFields: ['customerID', 'customerName'],
        headers: [{
            field: "customerName",
            name: "学员姓名",
            template: '<a ui-sref="ppts.customer-view.profiles({id:row.customerID,prev:\'ppts.customer\'})">{{row.customerName}}</a>'
        }, {
            field: "customerCode",
            name: "学员编号"
        }, {
            field: "parentName",
            name: "家长姓名"
        }, {
            field: "grade",
            name: "当前年级",
            template: '<span>{{row.grade | grade | normalize}}</span>'
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
            template: '<span>{{row.createTime | date:"yyyy-MM-dd" | normalize}}</span>'
        }, {
            field: "creatorName",
            name: "建档人"
        }, {
            field: "createJobName",
            name: "建档人岗位"
        }, {
            field: 'marketStaff',
            name: "归属市场专员"
        }, {
            field: "followedCount",
            name: "跟进次数",
            template: '<a ng-show="row.followedCount" ui-sref="ppts.customer-view.follows({id:row.customerID,prev:\'ppts.customer\'})">{{row.followedCount}}</a>' +
                      '<span ng-show="!row.followedCount">{{row.followedCount}}</span>'
        }, {
            field: "followTime",
            name: "最后一次跟进时间",
            template: '<span>{{row.followTime | date:"yyyy-MM-dd" | normalize}}</span>'
        }, {
            field: "customerLevel",
            name: "客户级别",
            template: '<span>{{row.customerLevel | customerLevel | normalize}}</span>'
        }, {
            field: 'nextFollowTime',
            name: "是否签约",
            template: '<span>{{row.nextFollowTime | date:"yyyy-MM-dd" | normalize}}</span>'
        }],
        orderBy: [{ dataField: 'PotentialCustomers.CreateTime', sortDirection: 1 }]
    });
});