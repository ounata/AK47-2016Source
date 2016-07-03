define(['angular', ppts.config.modules.customer], function (ng, customer) {

    customer.registerFactory('weeklyFeedbackDataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.customerApiBaseUrl + 'api/weeklyfeedback/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false }
            });

        resource.GetCustomerFeedbackList = function (criteria, success, error) {
            resource.post({ operation: 'GetCustomerFeedbackList' }, criteria, success, error);
        }

        resource.GetPagedCustomerFeedbackList = function (criteria, success, error) {
            resource.post({ operation: 'GetPagedCustomerFeedbackList' }, criteria, success, error);
        }

        return resource;
    }]);
    //客户反馈结果集列头
    customer.registerValue('customerFeedbackListDataHeader', {
        selection: 'checkbox',
        rowsSelected: [],
        keyFields: ['replyID'],
        headers: [{
            field: "feedbackTime",
            name: "反馈时间",
            template: '<a ui-sref="ppts.feedback-view({rId:row.replyID,customerId:row.customerId,prev:\'ppts\'})"><span>{{row.replyTime | date:"yyyy-MM-dd HH:mm"}}</span></a>'
        }, {
            field: "branchName",
            name: "分公司"
        }, {
            field: "campusName",
            name: "校区"
        }, {
            field: "customerName",
            name: "学员姓名",
            sortable: true,
            template: '<a ui-sref="ppts.student-view.profiles({id:row.customerId,prev:\'ppts.feedback\'})">{{row.customerName}}</a>'
        }, {
            field: "customerCode",
            name: "学员编号",
            //sortable: true,
            //template: '<a ui-sref="ppts.student-view.profiles({id:row.customerId,prev:\'ppts.feedback\'})">{{row.customerCode}}</a>'
        }, {
            field: "parentName",
            name: "家长姓名"
        }, {
            field: "grade",
            name: "当前年级",
            template: '<span>{{ row.grade | grade }}</span>'
        }, {
            field: "feedbackContent",
            name: "员工姓名"
        }, {
            field: "feedbackContent",
            name: "反馈对象",
            //template: '<span>{{ row.replyType | replyType }}</span>'
        },
        {
            field: "feedbackContent",
            name: "反馈内容"
        }],
        pager: {
            pageIndex: 1,
            pageSize: ppts.config.pageSizeItem,
            totalCount: -1
        },
        orderBy: [{ dataField: 'feedbackTime', sortDirection: 1 }]
    });
    customer.registerFactory('weeklyFeedbackDataViewService', ['dataSyncService', 'customerFeedbackListDataHeader', 'weeklyFeedbackDataService',
        function (dataSyncService, customerFeedbackListDataHeader, weeklyFeedbackDataService) {
            var service = this;
            //初始化
            service.configFeedbackListHeaders = function (vm) {
                vm.data = customerFeedbackListDataHeader;
                vm.data.pager.pageChange = function () {
                    dataSyncService.initCriteria(vm);
                    weeklyFeedbackDataService.GetPagedCustomerFeedbackList(vm.criteria, function (result) {
                        vm.data.rows = result.pagedData;
                    });
                }
            }
            //查询
            service.initFeedbackList = function (vm, callback) {
                dataSyncService.initCriteria(vm);
                weeklyFeedbackDataService.GetCustomerFeedbackList(vm.criteria, function (result) {
                    vm.data.rows = result.queryResult.pagedData;
                    dataSyncService.updateTotalCount(vm, result.queryResult);
                    if (ng.isFunction(callback)) {
                        callback();
                    }
                });
            }
            return service;
        }]);
});