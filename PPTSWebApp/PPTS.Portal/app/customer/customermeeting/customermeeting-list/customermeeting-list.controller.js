/*
    名    称: customermeeting-list.controller
    功能概要: 教学服务会Controller js
    作    者: Lucifer
    创建时间: 2016-04-26 15:19:48
    修正履历：
    修正时间:
*/
define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.customerMeetingDataService],
        function (customer) {
            customer.registerController('customerMeetingListController', ['$scope', 'customerMeetingDataViewService', 'customerMeetingsAdvanceSearchItems',
                function ($scope, customerMeetingDataViewService, searchItems) {
                	var vm = this;

                	// 配置跟列表数据表头
                	customerMeetingDataViewService.configCustomerMeetingListHeaders(vm);

                	// 页面初始化加载或重新搜索时查询
                	vm.search = function () {
                	    customerMeetingDataViewService.initCustomerMeetingList(vm, function () {
                			vm.searchItems = searchItems;
                		});
                	};
                	vm.search();
                }]);
        });