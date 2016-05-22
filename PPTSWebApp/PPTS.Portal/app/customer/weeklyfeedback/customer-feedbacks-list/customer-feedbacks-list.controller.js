/*
    名    称: customer-feedbacks-list.controller
    功能概要: 客户反馈列表 Controller js
    作    者: Lucifer
    创建时间: 2016年5月12日 15:48:53
    修正履历：
    修正时间:
*/
define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.weeklyFeedbackDataService],
        function (customer) {
        	customer.registerController('customerFeedbackListController', ['$scope', 'weeklyFeedbackDataViewService',
                function ($scope, weeklyFeedbackDataViewService, searchItems) {
                    var vm = this;

                	alert("该功能已经合并到学大反馈中");
                	return;

                	// 配置跟列表数据表头
                	weeklyFeedbackDataViewService.configFeedbackListHeaders(vm);

                	// 页面初始化加载或重新搜索时查询
                	vm.search = function () {
                		weeklyFeedbackDataViewService.initFeedbackList(vm, function () {
                			//vm.searchItems = searchItems;
                			$scope.$broadcast('dictionaryReady');
                		});
                	};
                	vm.search();
                }]);
        });