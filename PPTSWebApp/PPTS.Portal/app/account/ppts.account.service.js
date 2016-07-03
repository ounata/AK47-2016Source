
ppts.ng.service('accountAllotService', ['$resource', function ($resource) {
    var service = this;
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
    
        service.configDataHeader = function (vm, header) {
            if (!vm || !header) 
                return;
            vm.data = header;
        };

    }]);

    account.registerValue('accountAllotEditTable', {

        selection: 'checkbox',
        rowsSelected: [],
        keyFields: ['sortNo'],
        headers: [{
            field: "teacherOACode",
            name: "OA账号",
            template: '<span><mcs-input model="row.teacherOACode" ng-blur="vm.fetchTeacher(row)" required ng-disabled="{{!row.canEdit}}" /></span>'
        }, {
            field: "teacherName",
            name: "教师姓名",
            template: '<span>{{row.teacherName}}</span>'
        }, {
            field: "subject",
            name: "科目",
            template: '<span><mcs-select category="subject" model="row.subject" caption="科目" async="false" required/></span>'
        }, {
            field: "categoryType",
            name: "产品类型",
            template: '<span><mcs-select category="categoryType" model="row.categoryType" caption="产品类型" async="false" required /></span>'
        }, {
            name: "岗位",
            template: '<span><mcs-select category="teacherJobs_{{row.itemNo}}" model="row.teacherJobID" callback="vm.selectTeacherJob(item, model, row)" caption="岗位" ignore-async="true" required /></span>'
        }, {
            field: "teacherType",
            name: "教师类型",
            template: '<span>{{row.teacherType | teacherType}}</span>'
        }, {
            field: "allotMoney",
            name: "金额",
            template: '<span><mcs-input model="row.allotMoney" datatype="number" ng-blur="vm.calcAllot()" required currency less="0" great="999999" /></span>'
        }, {
            field: "allotAmount",
            name: "课时",
            template: '<span><mcs-input model="row.allotAmount" datatype="number" ng-blur="vm.calcAllot()" required positive less="0" great="999999" /></span>'
        }],
        pager: {
            pagable: false
        },
        orderBy: [{ dataField: 'sortNo', sortDirection: 1 }]
    });
        
    account.registerValue('accountAllotInfoTable', {

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
    });

});