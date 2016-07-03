define([ppts.config.modules.schedule], function (schedule) {

    schedule.registerFactory('studentCourseDataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.orderApiBaseUrl + 'api/studentcourse/:operation/:id',
           { operation: '@operation', id: '@id' },
           {
               'post': { method: 'POST' },
               'query': { method: 'GET', isArray: false }
           });

        resource.getStuCourse = function (criteria, success, error) {
            resource.post({ operation: 'getStuCourse' }, criteria, success, error);
        }

        resource.getStuCoursePaged = function (criteria, success, error) {
            resource.post({ operation: 'getStuCoursePaged' }, criteria, success, error);
        }

        resource.deleteAssign = function (criteria, success, error) {
            resource.post({ operation: 'deleteAssign' }, criteria, success, error);
        }

        resource.confirmAssign = function (criteria, success, error) {
            resource.post({ operation: 'confirmAssign' }, criteria, success, error);
        }

        resource.markupAssign = function (criteria, success, error) {
            resource.post({ operation: 'markupAssign' }, criteria, success, error);
        }

        resource.getStuClassRecord = function (criteria, success, error) {
            resource.post({ operation: 'getStuClassRecord' }, criteria, success, error);
        }

        resource.getStuClassRecordPaged = function (criteria, success, error) {
            resource.post({ operation: 'getStuClassRecordPaged' }, criteria, success, error);
        }

        return resource;
    }]);

    schedule.registerValue('studentCourseAdvanceSearchItems', [
        { name: '上课时间：', template: '<mcs-daterangepicker start-date="vm.criteria.startTime" end-date="vm.criteria.endTime" css="mcs-margin-left-10"/>' },
        { name: '教师编制：', template: '<ppts-checkbox-group category="teacherType" model="vm.criteria.isFullTimeTeacher" async="false"/>' },
    ]);


    schedule.registerValue('studentCourseDataHeader', {
        selection: 'checkbox',
        rowsSelected: [],
        keyFields: ['assignID'],
        headers: [{
            field: "teacherName",
            name: "教师姓名"
        }, {
            field: "customerName",
            name: "学员姓名"
        }, {
            field: "customerCode",
            name: "学员编号"
        }, {
            field: "startTime",
            name: "上课日期",
            template: '<span>{{ vm.getCourseDate(row.startTime) }}</span>'
        }, {
            field: "courseSE",
            name: "上课时段"
        }, {
            field: "amount",
            name: "课时"
        }, {
            field: "realTime",
            name: "实际小时"
        }, {
            field: "subjectName",
            name: "上课科目"
        }, {
            field: "gradeName",
            name: "上课年级"
        }, {
            field: "educatorName",
            name: "学管师"
        }, {
            field: "consultantName",
            name: "咨询师"
        }, {
            field: "assignStatus",
            name: "课时状态",
            template: '<span>{{row.assignStatus | assignStatus }}</span>'
        }, {
            field: "categoryTypeName",
            name: "课时类型"
        }, {
            field: "assetCode",
            name: "订单编号"
        }],
        orderBy: [{ dataField: 'startTime', sortDirection: 1 }]
    });



});