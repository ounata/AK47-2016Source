define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.customerDataService],
    function (customer) {
        customer.registerController('customerAddController', [
            '$scope',
            '$state',
            'customerDataService',
            'customerDataViewService',
            'customerParentService',
            '$http',
            function ($scope, $state, customerDataService, customerDataViewService, customerParentService, $http) {
                var vm = this, orginalParent, lastIndex = 0;
                vm.teacher = [];



                vm.queryTeacherList = function (keyword) {
                    var url = 'http://localhost/PPTS.Web.Course/api/Schedule/QueryTeacher';
                    return $http.post(url, { Keyword: keyword }).then(function (response) {
                        var teachers = response.data.Data.List;
                        return teachers;
                    });
                };

                vm.queryStudentList = function (query) {


                    return $http.post('http://localhost/MCSWebApp/MCS.Web.API/api/UserGraph/query', JSON.stringify({
                        searchTerm: query,
                        maxCount: 10,
                        listMark: 15
                    }), {
                        headers: {
                            'Content-Type': 'application/json',
                            'Accept': 'application/json'

                        }
                    }).then(function (result) {
                        if (result.data) {
                            return result.data;
                        }

                    });



                };


                // 页面初始化加载
                (function () {
                    customerDataViewService.initCreateCustomerInfo(orginalParent, vm, function () {
                        $scope.$broadcast('dictionaryReady');
                    });
                })();

                // 清空家长信息
                vm.reset = function () {
                    vm.parent = mcs.util.clone(orginalParent);
                };

                // 保存数据
                vm.save = function () {
                    customerDataService.createCustomer({
                        customer: vm.customer,
                        primaryParent: vm.parent,
                        customerRole: vm.customerRole,
                        parentRole: vm.parentRole
                    }, function () {
                        $state.go('ppts.customer');
                    });
                };

                // 添加已有家长
                vm.parentAdd = function (title) {
                    customerParentService.popupParentAdd(vm, title, 'add', function () {
                        $scope.$broadcast('dictionaryReady');
                    });
                }

                // 获取转介绍员工信息
                vm.getCustomerByCode = function (customerCode) {
                    if (!customerCode) {
                        vm.customer.referralCustomerName = '';
                        return;
                    };
                    customerDataService.getCustomerByCode(customerCode, function (result) {
                        vm.customer.referralCustomerName = result.customerName;
                    });
                };

                // 亲属关系切换
                customerParentService.initCustomerParentRelation($scope, vm, lastIndex);
            }]);
    });