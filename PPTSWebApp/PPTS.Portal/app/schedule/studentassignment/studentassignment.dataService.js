define([ppts.config.modules.schedule], function (schedule) {

    schedule.registerFactory('studentassignmentDataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.orderApiBaseUrl + 'api/studentassignment/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false }
            });
        
        resource.getAllStuUnAsgmt = function (criteria,success, error) {
            resource.post({ operation: 'getAllStudentAssignment' },criteria,success, error);
        }

        resource.getPagedStuUnAsgmt = function (criteria, success, error) {
            resource.post({ operation: 'getPagedStudentAssignment' }, criteria, success, error);
        }

        resource.getAssignCondition = function (criteria, success, error) {
            resource.post({ operation: 'getAssignCondition' }, criteria, success, error);
        }

        resource.createAssignCondition = function (criteria, success, error) {
            resource.post({ operation: 'createAssignCondition' }, criteria, success, error);
        }

        resource.getStudentWeekCourse = function (criteria, success, error) {
            resource.post({ operation: 'getStudentWeekCourse' }, criteria, success, error);
        }

        resource.cancelAssign = function (criteria, success, error) {
            resource.post({ operation: 'cancelAssign' }, criteria, success, error);
        }

        resource.copyAssign = function (criteria, success, error) {
            resource.post({ operation: 'copyAssign' }, criteria, success, error);
        }

        resource.resetAssignInit = function (success, error) {
            resource.post({ operation: 'resetAssignInit' },success, error);
        }

        resource.resetAssign = function (criteria, success, error) {
            resource.post({ operation: 'resetAssign' }, criteria, success, error);
        }

        resource.getCurMonthStat = function (criteria, success, error) {
            resource.post({ operation: 'getCurMonthStat' }, criteria, success, error);
        }

        resource.getSCLV = function (criteria, success, error) {
            resource.post({ operation: 'getSCLV' }, criteria, success, error);
        }

        resource.getPagedSCLV = function (criteria, success, error) {
            resource.post({ operation: 'getPagedSCLV' }, criteria, success, error);
        }

        resource.getACC = function (criteria, success, error) {
            resource.post({ operation: 'getACC' }, criteria, success, error);
        }

        resource.getACCPaged = function (criteria, success, error) {
            resource.post({ operation: 'getACCPaged' }, criteria, success, error);
        }

        resource.initEditACC = function (criteria, success, error) {
            resource.post({ operation: 'initEditACC' }, criteria, success, error);
        }

        resource.saveAssignConditon = function (criteria, success, error) {
            resource.post({ operation: 'saveAssignConditon' }, criteria, success, error);
        }

        resource.deleteAssignCondition = function (criteria, success, error) {
            resource.post({ operation: 'deleteAssignCondition' }, criteria, success, error);
        }
        
        
        return resource;
    }]);


    schedule.registerValue('studentUnAssignmentDataHeader', {
        selection: 'radio',
        rowsSelected: [],
        keyFields: ['customerID'],
        headers: [{
            field: "customerName",
            name: "学员姓名"
        }, {
            field: "customerCode",
            name: "学员编号"
        }, {
            field: "gender",
            name: "性别",
            template: '<span>{{row.gender | gender }}</span>'
        }, {
            field: "birthday",
            name: "出生日期",
            template: '<span>{{row.birthday | date:"yyyy-MM-dd" | normalize }}</span>'
        }, {
            field: "schoolName",
            name: "在读学校"
        }, {
            field: "grade",
            name: "当前年级",
            template: '<span>{{row.grade | grade}}</span>'
        }, {
            field: "educatorName",
            name: "学管师"
        }, {
            field: "assetOneToOneAmount",
            name: "剩余数量"
        }],
        orderBy: [{ dataField: 'CustomerCode', sortDirection: 1 }]
    });
});
