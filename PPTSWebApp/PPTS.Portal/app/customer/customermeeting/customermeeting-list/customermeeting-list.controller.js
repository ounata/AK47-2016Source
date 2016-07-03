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
            customer.registerController('customerMeetingListController', ['$scope', 'customerMeetingDataService', 'customerMeetingListDataHeader', 'dataSyncService', 'customerMeetingsAdvanceSearchItems',
                function ($scope, customerMeetingDataService, customerMeetingListDataHeader, dataSyncService, searchItems) {
                	var vm = this;

                	//// 配置跟列表数据表头
                	//customerMeetingDataViewService.configCustomerMeetingListHeaders(vm);

                	//// 页面初始化加载或重新搜索时查询
                	//vm.search = function () {
                	//    customerMeetingDataViewService.initCustomerMeetingList(vm, function () {
                	//		vm.searchItems = searchItems;
                	//	});
                	//};
                    // 配置数据表头 
                	dataSyncService.configDataHeader(vm, customerMeetingListDataHeader, customerMeetingDataService.getPagedCustomerMeetings, function () {
                	    $scope.$broadcast('dictionaryReady');
                	});

                    // 页面初始化加载或重新搜索时查询
                	vm.search = function () {
                	    dataSyncService.initDataList(vm, customerMeetingDataService.getAllCustomerMeetings, function () {
                	        vm.searchItems = searchItems;
                	        $scope.$broadcast('dictionaryReady');
                	    });
                	};
                	vm.downloadFile = function (file) {
                	    mcs.util.postMockForm(ppts.config.customerApiBaseUrl + "api/customermeetings/downloadmaterial", file);
                	};
                	vm.search();
                	vm.export = function () {
                	    mcs.util.postMockForm(ppts.config.customerApiBaseUrl + 'api/customermeetings/exportCustomerMeetings', vm.criteria);
                	}
                }]);
        });