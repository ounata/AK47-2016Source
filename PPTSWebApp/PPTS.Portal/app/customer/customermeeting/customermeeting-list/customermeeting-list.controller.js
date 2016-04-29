// Descripcion: 教学服务会Controll
// Create Date: 2016-04-26 15:19:48
// Author:      Lucifer
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
                			//vm.searchItems = searchItems;
                			$scope.$broadcast('dictionaryReady');
                		});
                	};
                	vm.search();
                }]);
        });