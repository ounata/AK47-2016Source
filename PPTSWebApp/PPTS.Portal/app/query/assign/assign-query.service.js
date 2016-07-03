define(['angular', ppts.config.modules.query], function (ng, query) {


    query.registerFactory('assignQueryService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.orderApiBaseUrl + 'api/query/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false },
                'get': { method: 'GET', isArray: true }
            });

        resource.queryClassGroup = function (criteria, success, error) {
            resource.post({ operation: 'QueryClassGroup' }, criteria, success, error);
        }

        resource.queryAssignByCustomer = function (criteria, success, error) {
            resource.post({ operation: 'QueryAssignByCustomer' }, criteria, success, error);
        }

        resource.queryAssignByTeacher = function (criteria, success, error) {
            resource.post({ operation: 'QueryAssignByTeacher' }, criteria, success, error);
        }

        return resource;
    }]);

    query.registerValue('teacherCourseAdvanceSearchItems', [
     { name: '上课时间：', template: '<mcs-daterangepicker start-date="vm.criteria.startTime" end-date="vm.criteria.endTime" css="mcs-margin-left-10"/>' },
     { name: '教师编制：', template: '<ppts-checkbox-group category="teacherType" model="vm.criteria.isFullTimeTeacher" async="false"/>' },
    ]);
});