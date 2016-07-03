define([ppts.config.modules.schedule], function (schedule) {

    schedule.registerFactory('classgroupDataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.orderApiBaseUrl + 'api/classgroup/:operation/:id', { operation: '@operation', id: '@id' }, { 'post': { method: 'POST' }, 'query': { method: 'GET', isArray: false } });

        resource.getAllClasses = function (criteria, success, error) {
            resource.post({ operation: 'getAllClasses' }, criteria, success, error);
        }

        resource.getPageClasses = function (criteria, success, error) {
            resource.post({ operation: 'getPageClasses' }, criteria, success, error);
        }

        resource.getCustomerAllClasses = function (criteria, success, error) {
            resource.post({ operation: 'getCustomerAllClasses' }, criteria, success, error);
        }

        resource.createClass = function (model, success, error) {
            resource.post({ operation: 'createClass' }, model, success, error);
        }

        resource.deleteClass = function (id, success, error) {
            var data = { classID: id };
            resource.post({ operation: 'deleteClass' }, data, success, error);
        }

        resource.getAllAssets = function (criteria, success, error) {
            resource.post({ operation: 'getAllAssets' }, criteria, success, error);
        }

        resource.getPageAssets = function (criteria, success, error) {
            resource.post({ operation: 'getPageAssets' }, criteria, success, error);
        }

        resource.getAllTeacherJobs = function (criteria, success, error) {
            resource.post({ operation: 'getAllTeacherJobs' }, criteria, success, error);
        }

        resource.getPageTeacherJobs = function (criteria, success, error) {
            resource.post({ operation: 'getPageTeacherJobs' }, criteria, success, error);
        }
        
        resource.getAllCustomers = function (criteria, success, error) {
            resource.post({ operation: 'getAllCustomers' }, criteria, success, error);
        }

        resource.getPageCustomers = function (criteria, success, error) {
            resource.post({ operation: 'getPageCustomers' }, criteria, success, error);
        }

        resource.deleteCustomer = function (model, success, error) {
            resource.post({ operation: 'deleteCustomer' }, model, success, error);
        }

        resource.getClassDetail = function (criteria, success, error) {
            resource.post({ operation: 'getClassDetail' }, criteria, success, error);
        }

        resource.getPageClassLessons = function (criteria, success, error) {
            resource.post({ operation: 'getPageClassLessons' }, criteria, success, error);
        }

        resource.editClassLessones = function (model, success, error) {
            resource.post({ operation: 'editClassLessones' }, model, success, error);
        }

        resource.editTeacher = function (model, success, error) {
            resource.post({ operation: 'editTeacher' }, model, success, error);
        }

        resource.addCustomer = function (model, success, error) {
            resource.post({ operation: 'addCustomer' }, model, success, error);
        }

        resource.checkCreateClass_Product = function (id, success, error) {
            var data = { productID: id };
            resource.post({ operation: 'checkCreateClass_Product' }, data, success, error);
        }

        return resource;
    }]);


    schedule.registerValue('classSearchItems', [
        { name: "上课科目", template: '<ppts-checkbox-group category="subject" model="vm.criteria.subjects"/>' },
        { name: "上课年级", template: ' <ppts-checkbox-group category="grade" model="vm.criteria.grades"/>' },
        { name: '班级上课日期', template: '<mcs-daterangepicker start-date="vm.criteria.startTime" end-date="vm.criteria.endTime" css="mcs-padding-left-10"/>' },
        { name: "班级状态", template: '<ppts-checkbox-group category="classStatus" model="vm.criteria.classStatuses"/>' }
    ]);

    schedule.registerValue('classListHeader', {
        selection: 'radio',
        rowsSelected: [],
        keyFields: ['classID', 'classStatus'],
        orderBy: [{ dataField: 'createTime', sortDirection: 1 }],
        headers: [{
            field: "campusName",
            name: "校区",
            headerCss: 'datatable-header-align-right',
            sortable: false,
            description: ''
        }, {
            field: "className",
            name: "班级名称",
            headerCss: 'datatable-header-align-right',
            template: '<a ui-sref="ppts.classgroup-detail({classID:row.classID})">{{row.className}}</a>',
            sortable: false,
            description: ''
        }, {
            field: "createTime | date:'yyyy-MM-dd'",
            name: "创建时间",
            headerCss: 'datatable-header-align-right',
            sortable: false,
            description: ''
        }, {
            field: "productCode",
            name: "产品编号",
            headerCss: 'datatable-header-align-right',
            sortable: false,
            description: ''
        }, {
            field: "productName",
            name: "产品名称",
            headerCss: 'datatable-header-align-right',
            sortable: false,
            description: ''
        }, {
            field: "classStatus | classStatus",
            name: "班级状态",
            headerCss: 'datatable-header-align-right',
            sortable: false,
            description: ''
        }, {
            field: "lessonCount",
            name: "总课次数",
            headerCss: 'datatable-header-align-right',
            sortable: false,
            description: ''
        }, {
            field: "finishedLessons",
            name: "已上课次数",
            headerCss: 'datatable-header-align-right',
            sortable: false,
            description: ''
        }, {
            field: "classPeoples",
            name: "上课人数",
            headerCss: 'datatable-header-align-right',
            sortable: false,
            description: ''
        }, {
            field: "startTime | date:'yyyy-MM-dd'",
            name: "班级开班时间",
            headerCss: 'datatable-header-align-right',
            sortable: false,
            description: ''
        }, {
            field: "endTime | date:'yyyy-MM-dd'",
            name: "班级结束时间",
            headerCss: 'datatable-header-align-right',
            sortable: false,
            description: ''
        }]
    });
});
