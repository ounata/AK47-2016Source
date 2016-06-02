define(['angular',
    ppts.config.modules.customer,
    ppts.config.dataServiceConfig.customerDataService
    ],
    function (ng, customer) {

    //web api后端交互
        customer.registerFactory('feedbackDataService', ['$resource', 'customerDataService', function ($resource, customerDataService) {

        var resource = $resource(ppts.config.customerApiBaseUrl + 'api/feedback/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false }
            });

        resource.GetCustomerRepliesList = function (criteria, success, error) {
            resource.post({ operation: 'GetCustomerRepliesList' }, criteria, success, error);
        }

        resource.GetPagedCustomerRepliesList = function (criteria, success, error) {
            resource.post({ operation: 'GetPagedCustomerRepliesList' }, criteria, success, error);
        }

        resource.loadDictionaries = function (success, error) {
            resource.query({ operation: 'loadDictionaries' }, success, error);
        }
        resource.getCustomerInfo = function (id, success, error) {
            customerDataService.getCustomerInfo(id, success, error);
        }
        //resource.getCustomerForUpdate = function (id, success, error) {
        //    resource.query({ operation: 'updateCustomer', id: id }, success, error);
        //}

        resource.createCustomerReplies = function (model, success, error) {
            resource.save({ operation: 'createCustomerReplies' }, model, success, error);
        };

        //resource.updateCustomer = function (model, success, error) {
        //    resource.save({ operation: 'updateCustomer' }, model, success, error);
        //};

        return resource;
    }]);
    //学大反馈结果集列头
    customer.registerValue('feedbackListDataHeader', {
        selection: 'checkbox',
        rowsSelected: [],
        keyFields: ['replyID'],
        headers: [{
            field: "replyTime",
            name: "反馈时间",
            template: '<a ui-sref="ppts.feedback-view({customerId:row.customerId,replyTime:(row.replyTime.getTime()),prev:\'ppts.feedback\'})">{{row.replyTime | date:"yyyy-MM-dd HH:mm"}}</a>'
        }, {
            field: "campusName",
            name: "所属校区"
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
            field: "replierName",
            name: "员工姓名"
        }, 
        {
            field: "replyType",
            name: "反馈类型",
            template: '<span>{{ row.replyType | replyType }}</span>'
        },{
            field: "replyObject1",
            name: "互动岗位",
            template: '<span>{{ row.replyObject1 | replyObject }}</span>'
        },
        {
            field: "replyContent",
            name: "反馈内容"
        }],
        pager: {
            pageIndex: 1,
            pageSize: 10,
            totalCount: -1
        },
        orderBy: [{ dataField: 'replyTime', sortDirection: 1 }]
    });
    customer.registerFactory('feedbackDataViewService', ['dataSyncService', 'feedbackListDataHeader', 'feedbackDataService',
        function (dataSyncService, feedbackListDataHeader, feedbackDataService) {
            var service = this;
            //初始化
            service.configFeedbackListHeaders = function (vm) {
                vm.data = feedbackListDataHeader;
                vm.data.pager.pageChange = function () {
                    dataSyncService.initCriteria(vm);
                    feedbackDataService.GetPagedCustomerRepliesList(vm.criteria, function (result) {
                        vm.data.rows = result.pagedData;
                    });
                }
            }

            //查询
            service.initFeedbackList = function (vm, callback) {
                dataSyncService.initCriteria(vm);
                feedbackDataService.GetCustomerRepliesList(vm.criteria, function (result) {
                    vm.data.rows = result.queryResult.pagedData;
                    dataSyncService.injectDictData();
                    dataSyncService.updateTotalCount(vm, result.queryResult);
                    if (ng.isFunction(callback)) {
                        callback();
                    }
                });
            }
            return service;
        }]);

    //联系家长
    customer.registerFactory('feedbackAddDataViewService', ['feedbackDataService',  function (feedbackDataService) {
        var service = this;
        service.init = function (vm) {
           feedbackDataService.getCustomerInfo(vm.customerId, function (result) {
               vm.customerName = result.customer.customerName;
               vm.parentName = result.parent.parentName;
            });
        }

        return service;
    }]);
   
    //家校互动
    //dataSyncService必须注入第一位，dataSyncService注入最后，feedbackViewDataHeader变为函数集合?
    customer.registerFactory('feedbackViewDataViewService', ['dataSyncService', 'feedbackDataService', 
        function (dataSyncService, feedbackDataService) {
            var service = this;
            service.initConfig = function (vm) {
                vm.data = {};
                vm.data.pager = {
                    pageIndex: 0,
                    pageSize:10
                };
                vm.data.keyFields = ['replyID'];//vm.data.rows.length
                vm.data.orderBy = [{ dataField: 'replyTime', sortDirection: 1 }];
                vm.data.pager.pageChange = function () {
                    dataSyncService.initCriteria(vm);
                    feedbackDataService.GetPagedCustomerRepliesList(vm.criteria, function (result) {
                        vm.data.rows = result.pagedData;
                    });
                }
               
            }
            service.initData = function (vm, callback) {
                //初始化词典
                feedbackDataService.loadDictionaries(function (result) {
                    //vm.dictionaries = result.dictionaries;
                    dataSyncService.injectDictData();
                    //填充选项卡
                    var replyObjects = ppts.dict[ppts.config.dictMappingConfig.replyObject];
                    for (var index in replyObjects) {
                        vm.tabs.push({
                            title: replyObjects[index].value,
                            key: replyObjects[index].key,
                            active: index == 0
                        });
                    }
                    //加载数据到第一个选项卡
                    dataSyncService.initCriteria(vm);
                    if (vm.criteria) {
                        vm.criteria.replyObjects = [];
                        if (!vm.criteria.replyObject) {
                            vm.criteria.replyObjects.push(vm.tabs[0].key);
                        }
                        else {
                            //控制搜索查询选项卡变动
                            for (var index in vm.tabs) {
                                if (vm.tabs[index].key == vm.criteria.replyObject) {
                                    vm.tabs[index].active = true;
                                    break;
                                }
                            }
                            vm.criteria.replyObjects.push(vm.criteria.replyObject);
                        }
                    }
                    feedbackDataService.GetCustomerRepliesList(vm.criteria, function (result) {
                        vm.data.rows = result.queryResult.pagedData;
                        //vm.dictionaries = result.dictionaries;
                        //dataSyncService.injectDictData();
                        vm.data.pager.totalCount = result.queryResult.totalCount;
                        //dataSyncService.updateTotalCount(vm, result.queryResult);
                        if (ng.isFunction(callback)) {
                            callback();
                        }
                    });
                    //if (ng.isFunction(callback)) {
                    //    callback();
                    //}
                });

               
            }

            return service;
        }]);
});