﻿/*
    名    称: customermeeting.dataService
    功能概要: 教学服务会Service js
    作    者: Lucifer
    创建时间: 2016-04-26 15:19:48
    修正履历：
    修正时间:
*/
define(['angular', ppts.config.modules.customer], function (ng, customer) {

    //教学服务会后端交互
    customer.registerFactory('customerMeetingDataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.customerApiBaseUrl + 'api/customermeetings/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false }
            });
        //默认查询
        resource.getAllCustomerMeetings = function (criteria, success, error) {
            resource.post({ operation: 'getAllCustomerMeetings' }, criteria, success, error);
        }

        //分页查询
        resource.getPagedCustomerMeetings = function (criteria, success, error) {
            resource.post({ operation: 'getPagedCustomerMeetings' }, criteria, success, error);
        }

        resource.saveCustomerMeetings = function (criteria, success, error) {
            resource.post({ operation: 'saveCustomerMeetings' }, criteria, success, error);
        }
        resource.loadCustomerMeetingsDictionaries = function (criteria, success, error) {
            resource.post({ operation: 'loadCustomerMeetingsDictionaries' }, criteria, success, error);
        }

        resource.getCustomerMeeting = function (meetingID, success, error) {
            resource.query({ operation: 'getCustomerMeeting', id: meetingID }, success, error);
        }

        return resource;
    }]);

    ////高级查询条件(暂时写到页面)
    //customer.registerValue('customerMeetingsAdvanceSearchItems', [
    // { name: '入学年级：', template: '<ppts-checkbox-group category="grade" model="vm.criteria.entranceGrades" clear="vm.criteria.entranceGrades=[]" async="false"/>' },
    // { name: '建档日期：', template: '<ppts-daterangepicker start-date="vm.criteria.createTimeStart" end-date="vm.criteria.createTimeEnd"/>' },
    // { name: '充值日期：', template: '<ppts-daterangepicker start-date="vm.criteria.payTimeStart" end-date="vm.criteria.payTimeEnd"/>' },
    // { name: '充值金额：', template: '<ppts-range-slider start="vm.criteria.payAmoutStart" end="vm.criteria.payAmoutEnd" class="col-xs-6 col-sm-6"/>' },
    // { name: '跟进阶段：', template: '<ppts-checkbox-group category="followStage" model="vm.criteria.followStages" clear="vm.criteria.followStages=[]" async="false"/>' },
    // { name: '客户级别：', template: '<ppts-checkbox-group category="vipLevel" model="vm.criteria.customerLevels" clear="vm.criteria.customerLevels=[]" async="false"/>' },
    // { name: '未跟进时长：', template: '<ppts-radiobutton-group category="period" model="vm.followPeriodValue" async="false"/> <span ng-show="vm.followPeriodValue == 5"><input type="text" ng-model="vm.followDays" class="mcs-input-small" onkeyup="mcs.util.limit(this)" onafterpaste="mcs.util.limit(this)"/>天未跟进</span>' },
    // { name: '在读学校：', template: '<mcs-input model="vm.criteria.schoolName" css="col-xs-4 col-sm-4" />' },
    // { name: '家庭住址：', template: '<mcs-input model="vm.criteria.address" css="col-xs-4 col-sm-4" />' },
    // { name: '信息来源：', template: '<ppts-datarange min="vm.criteria.test" max="vm.criteria.test" css="col-xs-4 col-sm-4" />' },
    // { name: '归属坐席：', template: '<ppts-checkbox-group category="assignment" model="vm.criteria.isAssignSeat" clear="vm.criteria.isAssignSeat=[]" async="false"/> <mcs-input placeholder="坐席姓名" model="vm.criteria.callcenterName"/>' },
    // { name: '归属咨询师：', template: '<ppts-checkbox-group category="assignment" model="vm.criteria.isAssignConsultant" clear="vm.criteria.isAssignConsultant=[]" async="false"/> <mcs-input placeholder="咨询师姓名" model="vm.criteria.consultantName"/>' },
    // { name: '归属市场专员：', template: '<ppts-checkbox-group category="assignment" model="vm.criteria.isAssignMarket" clear="vm.criteria.isAssignMarket=[]" async="false"/> <mcs-input placeholder="市场专员姓名" model="vm.criteria.marketName"/>' },
    // { name: '建档人：', template: '<ppts-checkbox-group category="people" model="vm.criteria.creatorJobs" clear="vm.criteria.creatorJobs=[]" async="false"/> <mcs-input placeholder="建档人姓名" model="vm.criteria.creatorName"/>' },
    // { name: '有效/无效客户：', template: '<ppts-radiobutton-group category="valid" model="vm.criteria.isValid" async="false"/>' }
    //]);

    //教学服务会结果集列头
    customer.registerValue('customerMeetingListDataHeader', {
        rowsSelected: [],
        keyFields: ['meetingID'],
        headers: [{
            field: "meetingTime",
            name: "会议时间",
            template: '<a ui-sref="ppts.customermeeting-view({id:row.meetingID,prev:\'ppts.customermeeting\'})"><span>{{row.meetingTime | date:"yyyy-MM-dd HH:mm"}}</span></a>'
        }, {
            field: "campusName",
            name: "所属校区"
        }, {
            field: "customerName",
            name: "学员姓名",
            sortable: true,
            template: '<a ui-sref="ppts.student-view.profiles({id:row.customerId,prev:\'ppts.customermeeting\'})">{{row.customerName}}</a>'
        }, {
            field: "customerCode",
            name: "学员编号"
        }, {
            field: "parentName",
            name: "家长姓名"
        }, {
            field: "grade",
            name: "当前年级",
            template: '<span>{{ row.grade | grade }}</span>'
        }, {
            field: "meetingType",
            name: "会议类型",
            template: '<span>{{ row.meetingType | meetingType }}</span>'
        }, {
            field: "organizerName",
            name: "会议组织人"
        }, {
            field: "satisficing",
            name: "家长满意度",
            template: '<span>{{ row.satisficing | satisfaction }}</span>'
        },
        //{
        //    field: "contentData",
        //    name: "反馈内容/解决方案"
        //},
        {
            field: "attachment",
            name: "附件"
        }],
        pager: {
            pageIndex: 1,
            pageSize: 10,
            totalCount: -1
        },
        orderBy: [{ dataField: 'meetingTime', sortDirection: 1 }]
    });

    customer.registerFactory('customerMeetingDataViewService', ['customerMeetingDataService', 'dataSyncService', 'customerMeetingListDataHeader',
        function (customerMeetingDataService, dataSyncService, customerMeetingListDataHeader) {
            var service = this;

            // 配置教学服务会列表表头
            service.configCustomerMeetingListHeaders = function (vm) {
                vm.data = customerMeetingListDataHeader;

                vm.data.pager.pageChange = function () {
                    dataSyncService.initCriteria(vm);
                    customerMeetingDataService.getPagedCustomerMeetings(vm.criteria, function (result) {
                        vm.data.rows = result.pagedData;
                    });
                }
            };

            // 初始化教学服务会列表数据
            service.initCustomerMeetingList = function (vm, callback) {
                dataSyncService.initCriteria(vm);
                customerMeetingDataService.getAllCustomerMeetings(vm.criteria, function (result) {
                    vm.data.rows = result.queryResult.pagedData;
                    dataSyncService.injectDictData();
                    dataSyncService.updateTotalCount(vm, result.queryResult);
                    if (ng.isFunction(callback)) {
                        callback();
                    }
                });
            };

            // 初始化日期范围
            service.initDateRange = function ($scope, vm, watchExps) {
                if (!watchExps && !watchExps.length) return;
                for (var index in watchExps) {
                    (function () {
                        var temp = index, exp = watchExps[index];
                        $scope.$watch(exp.watchExp, function () {
                            var selectedValue = exp.selectedValue;
                            var dateRange = dataSyncService.selectPageDict('dateRange', vm[selectedValue]);
                            if (dateRange) {
                                vm.criteria[exp.start] = dateRange.start;
                                vm.criteria[exp.end] = dateRange.end;
                            }
                        });
                    })();
                }
            };

            return service;
        }]);




    //学员教学服务会后端交互
    customer.registerFactory('studentMeetingDataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.customerApiBaseUrl + 'api/customermeetings/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false }
            });
        //默认查询
        resource.getAllCustomerMeetings = function (criteria, success, error) {
            resource.post({ operation: 'getAllCustomerMeetings' }, criteria, success, error);
        }

        //分页查询
        resource.getPagedCustomerMeetings = function (criteria, success, error) {
            resource.post({ operation: 'getPagedCustomerMeetings' }, criteria, success, error);
        }
        return resource;
    }]);

    //高级查询条件(暂时写到页面)
    //customer.registerValue('studentMeetingsAdvanceSearchItems', [
    // { name: '入学年级：', template: '<ppts-checkbox-group category="grade" model="vm.criteria.entranceGrades" clear="vm.criteria.entranceGrades=[]" async="false"/>' },
    // { name: '建档日期：', template: '<ppts-daterangepicker start-date="vm.criteria.createTimeStart" end-date="vm.criteria.createTimeEnd"/>' },
    // { name: '充值日期：', template: '<ppts-daterangepicker start-date="vm.criteria.payTimeStart" end-date="vm.criteria.payTimeEnd"/>' },
    // { name: '充值金额：', template: '<ppts-range-slider start="vm.criteria.payAmoutStart" end="vm.criteria.payAmoutEnd" class="col-xs-6 col-sm-6"/>' },
    // { name: '跟进阶段：', template: '<ppts-checkbox-group category="followStage" model="vm.criteria.followStages" clear="vm.criteria.followStages=[]" async="false"/>' },
    // { name: '客户级别：', template: '<ppts-checkbox-group category="vipLevel" model="vm.criteria.customerLevels" clear="vm.criteria.customerLevels=[]" async="false"/>' },
    // { name: '未跟进时长：', template: '<ppts-radiobutton-group category="period" model="vm.followPeriodValue" async="false"/> <span ng-show="vm.followPeriodValue == 5"><input type="text" ng-model="vm.followDays" class="mcs-input-small" onkeyup="mcs.util.limit(this)" onafterpaste="mcs.util.limit(this)"/>天未跟进</span>' },
    // { name: '在读学校：', template: '<mcs-input model="vm.criteria.schoolName" css="col-xs-4 col-sm-4" />' },
    // { name: '家庭住址：', template: '<mcs-input model="vm.criteria.address" css="col-xs-4 col-sm-4" />' },
    // { name: '信息来源：', template: '<ppts-datarange min="vm.criteria.test" max="vm.criteria.test" css="col-xs-4 col-sm-4" />' },
    // { name: '归属坐席：', template: '<ppts-checkbox-group category="assignment" model="vm.criteria.isAssignSeat" clear="vm.criteria.isAssignSeat=[]" async="false"/> <mcs-input placeholder="坐席姓名" model="vm.criteria.callcenterName"/>' },
    // { name: '归属咨询师：', template: '<ppts-checkbox-group category="assignment" model="vm.criteria.isAssignConsultant" clear="vm.criteria.isAssignConsultant=[]" async="false"/> <mcs-input placeholder="咨询师姓名" model="vm.criteria.consultantName"/>' },
    // { name: '归属市场专员：', template: '<ppts-checkbox-group category="assignment" model="vm.criteria.isAssignMarket" clear="vm.criteria.isAssignMarket=[]" async="false"/> <mcs-input placeholder="市场专员姓名" model="vm.criteria.marketName"/>' },
    // { name: '建档人：', template: '<ppts-checkbox-group category="people" model="vm.criteria.creatorJobs" clear="vm.criteria.creatorJobs=[]" async="false"/> <mcs-input placeholder="建档人姓名" model="vm.criteria.creatorName"/>' },
    // { name: '有效/无效客户：', template: '<ppts-radiobutton-group category="valid" model="vm.criteria.isValid" async="false"/>' }
    //]);

    //学员教学服务会结果集列头
    customer.registerValue('studentMeetingListDataHeader', {
        rowsSelected: [],
        keyFields: ['meetingID'],
        headers: [{
            field: "meetingTime",
            name: "会议时间",
            template: '<span>{{row.meetingTime | date:"yyyy-MM-dd HH:mm"}}</span>'
        }, {
            field: "meetingType",
            name: "会议类型",
            template: '<span>{{ row.meetingType | meetingType }}</span>'
        }, {
            field: "organizerName",
            name: "会议组织人"
        }, {
            field: "satisficing",
            name: "家长满意度",
            template: '<span>{{ row.satisficing | satisfaction }}</span>'
        }, 
        //{
        //    field: "contentData",
        //    name: "反馈内容/解决方案"
        //},
        {
            field: "attachment",
            name: "附件"
        }],
        pager: {
            pageIndex: 1,
            pageSize: 10,
            totalCount: -1
        },
        orderBy: [{ dataField: 'meetingTime', sortDirection: 1 }]
    });

    customer.registerFactory('studentMeetingDataViewService', ['studentMeetingDataService', 'dataSyncService', 'studentMeetingListDataHeader',
        function (studentMeetingDataService, dataSyncService, studentMeetingListDataHeader) {
            var service = this;

            // 配置教学服务会列表表头
            service.configStudentMeetingListHeaders = function (vm) {
                vm.data = studentMeetingListDataHeader;

                vm.data.pager.pageChange = function () {
                    dataSyncService.initCriteria(vm);
                    studentMeetingDataService.getPagedCustomerMeetings(vm.criteria, function (result) {
                        vm.data.rows = result.pagedData;
                    });
                }
            };

            // 初始化教学服务会列表数据
            service.initStudentMeetingList = function (vm, customerId, callback) {
                dataSyncService.initCriteria(vm);
                vm.criteria.customerId = customerId;
                studentMeetingDataService.getAllCustomerMeetings(vm.criteria, function (result) {
                    vm.data.rows = result.queryResult.pagedData;
                    dataSyncService.injectDictData();
                    dataSyncService.updateTotalCount(vm, result.queryResult);
                    if (ng.isFunction(callback)) {
                        callback();
                    }
                });
            };

            // 初始化日期范围
            service.initDateRange = function ($scope, vm, watchExps) {
                if (!watchExps && !watchExps.length) return;
                for (var index in watchExps) {
                    (function () {
                        var temp = index, exp = watchExps[index];
                        $scope.$watch(exp.watchExp, function () {
                            var selectedValue = exp.selectedValue;
                            var dateRange = dataSyncService.selectPageDict('dateRange', vm[selectedValue]);
                            if (dateRange) {
                                vm.criteria[exp.start] = dateRange.start;
                                vm.criteria[exp.end] = dateRange.end;
                            }
                        });
                    })();
                }
            };

            return service;
        }]);


    //---新增 教学服务会 相关

    ////web api 交互相关
    //customer.registerFactory('customerMeetingAddDataService', ['$resource', function ($resource) {
    //    var resource = $resource(ppts.config.customerApiBaseUrl + 'api/customermeetings/:operation/:id',
    //       { operation: '@operation', id: '@id' },
    //       {
    //           'post': { method: 'POST' },
    //           'query': { method: 'GET', isArray: false }
    //       });

    //    resource.saveCustomerMeetings = function (criteria, success, error) {
    //        resource.post({ operation: 'saveCustomerMeetings' }, criteria, success, error);
    //    }
    //    resource.loadCustomerMeetingsDictionaries = function (criteria, success, error) {
    //        resource.post({ operation: 'loadCustomerMeetingsDictionaries' }, criteria, success, error);
    //    }
    //    return resource;
    //}]);
    customer.registerFactory('customerMeetingAddDataViewService',
        ['dataSyncService', 'customerMeetingDataService',
        function (dataSyncService, customerMeetingDataService) {
            var service = this;

            // 初始化新增会议初始项
            service.initData = function (vm, callback) {
                customerMeetingDataService.loadCustomerMeetingsDictionaries(vm.criteria, function (result) {
                    if (!vm.customerMeeting) {
                        vm.customerMeeting = {};
                        vm.items = new Array();
                    }
                    vm.dictionaries = result.dictionaries;
                    dataSyncService.injectDictData();

                    if (ng.isFunction(callback)) {
                        callback();
                    }
                });
            };

            //新增会议
            service.saveCustomerMeetings = function (vm, callback) {
                var item = new Object();
                //if (!vm.customerMeeting) vm.customerMeeting = {};
                //item.objectType = vm.customerMeeting.objectType;
                //item.objectName = vm.customerMeeting.objectName;
                //item.contentType = vm.customerMeeting.contentType;
                //item.contentData = vm.customerMeeting.feedbackContentOrSolution;
                //vm.items.push(item);

                var hours = vm.customerMeeting.hours;
                var minutes = vm.customerMeeting.minutes;
                if (hours && "" != hours) {
                    vm.customerMeeting.meetingDuration = hours + "小时"
                }
                if (minutes && "" != minutes) {
                    vm.customerMeeting.meetingDuration += minutes + "分钟 "
                }
                customerMeetingDataService.saveCustomerMeetings(vm, function (result) {
                    if (ng.isFunction(callback)) {
                        callback();
                    }
                });
            }
            return service;
        }]);

    //查看会议
    customer.registerFactory('customerMeetingViewService',
      ['dataSyncService', 'customerMeetingDataService',
      function (dataSyncService, customerMeetingDataService) {
          var service = this;
          service.initData = function (id, vm, callback) {
              customerMeetingDataService.getCustomerMeeting(id, function (result) {
                  //if (!vm.customerMeeting) vm.customerMeeting = {};
                  vm.customerMeeting = result.customerMeeting;
                  vm.items = result.items;
                  if (ng.isFunction(callback)) {
                      callback();
                  }
              });

          }
          return service;
      }]);
    //编辑会议
    customer.registerFactory('customerMeetingEditDataViewService',
      ['dataSyncService', 'customerMeetingDataService',
      function (dataSyncService, customerMeetingDataService) {
          var service = this;
          service.initData = function (id, vm, callback) {
              customerMeetingDataService.getCustomerMeeting(id, function (result) {
                  //if (!vm.customerMeeting) vm.customerMeeting = {};
                  vm.customerMeeting = result.customerMeeting;
                  vm.items = result.items;
                  if (0 != vm.items.length){
                      vm.customerMeeting.objectType = vm.items[0].objectType;
                      vm.customerMeeting.objectName = vm.items[0].objectName;
                      vm.customerMeeting.contentType = vm.items[0].contentType;
                      vm.customerMeeting.feedbackContentOrSolution = vm.items[0].contentData;
                  }
                  if (ng.isFunction(callback)) {
                      callback();
                  }
              });

          }
          //编辑会议
          service.saveCustomerMeetings = function (vm, callback) {
              var item = new Object();
              //if (!vm.customerMeeting) vm.customerMeeting = {};
              //item.objectType = vm.customerMeeting.objectType;
              //item.objectName = vm.customerMeeting.objectName;
              //item.contentType = vm.customerMeeting.contentType;
              //item.contentData = vm.customerMeeting.feedbackContentOrSolution;
              //vm.items.push(item);

              customerMeetingDataService.saveCustomerMeetings(vm, function (result) {
                  if (ng.isFunction(callback)) {
                      callback();
                  }
              });
          }
          return service;
      }]);
});