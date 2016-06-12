﻿define([ppts.config.modules.schedule], function (schedule) {

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
        { name: '上课时间：', template: '<ppts-daterangepicker start-date="vm.criteria.startTime" end-date="vm.criteria.endTime" css="mcs-margin-left-10"/>' },
        { name: '教师编制：', template: '<ppts-checkbox-group category="teacherType" model="vm.criteria.isFullTimeTeacher" async="false"/>' },
    ]);
});