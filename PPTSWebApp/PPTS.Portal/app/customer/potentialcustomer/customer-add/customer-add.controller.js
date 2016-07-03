define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.customerDataService],
    function (customer) {
        customer.registerController('customerAddController', [
            '$scope',
            '$state',
            'dataSyncService',
            'customerDataService',
            'customerDataViewService',
            'customerParentService',
            '$http',
            'mcsValidationService',
            function ($scope, $state, dataSyncService, customerDataService, customerDataViewService, customerParentService, $http, mcsValidationService) {
                var vm = this, orginalParent, lastIndex = 0;
                var orgType = {
                    branch: 2,
                    campus: 3
                };

                // 分公司 校区
                var permisstionFilter = function () {
                    if (!vm.customer) vm.customer = {};
                    vm.branchID = ppts.user.branchId;
                    vm.campusID = ppts.user.campusId;
                    if (ppts.user.branchId) { vm.disabledlevel = 1; }
                    if (ppts.user.campusId) { vm.disabledlevel = 2; }
                };

                // 页面初始化加载
                (function () {
                    permisstionFilter();
                    customerDataViewService.initCreateCustomerInfo(orginalParent, vm, function () {
                        // 亲属关系切换
                        customerParentService.initCustomerParentRelation($scope, vm, lastIndex);
                        $scope.$broadcast('dictionaryReady');
                        $scope.$broadcast('dataReady');
                    });
                })();

                // 清空家长信息
                vm.reset = function () {
                    vm.parent = mcs.util.clone(orginalParent);
                };

                // 保存数据
                vm.save = function () {
                    if (mcsValidationService.run($scope)) {
                        vm.parent.addressDetail = vm.getAddressDetail();
                        customerDataService.createCustomer({
                            customer: vm.customer,
                            parent: vm.parent,
                            customerRole: vm.customerRole,
                            parentRole: vm.parentRole
                        }, function () {
                            $state.go('ppts.customer');
                        });
                    }
                };

                // 添加已有家长
                vm.parentAdd = function (title) {
                    customerParentService.popupParentAdd(vm, 'add', function () {
                        mcsValidationService.check($('#parentName'));
                        mcsValidationService.check($('#parentPrimaryPhone'));
                        $scope.$broadcast('dictionaryReady');
                    });
                };

                // 转介绍员工OA
                vm.getStaffByOA = function (referralStaffOACode) {
                    var valid = true;
                    vm.customer.referralStaffJobName = '';
                    ppts.dict['c_codE_ABBR_referralStaffJobName'] = [];
                    $scope.$broadcast('dictionaryReady');
                    if (!referralStaffOACode) {
                        vm.customer.referralStaffOACode = '';
                        mcsValidationService.check($('#referralStaffOACode'), {
                            validate: valid
                        });
                        return valid;
                    };
                    customerDataService.getStaffByOA(referralStaffOACode, function (result) {
                        if (result.length) {
                            dataSyncService.injectDynamicDict(result, {
                                key: 'id', value: 'name', category: 'referralStaffJobName'
                            });
                            $scope.$broadcast('dictionaryReady');
                        } else {
                            valid = false;
                        }
                        mcsValidationService.check($('#referralStaffOACode'), {
                            validate: valid
                        });
                        return valid;
                    });
                };

                // 获取转介绍学员信息
                vm.getCustomerByCode = function (customerCode) {
                    var valid = true;
                    vm.customer.referralCustomerName = '';
                    if (!customerCode) {
                        vm.customer.referralCustomerName = '';
                        mcsValidationService.check($('#referralCustomerCode'), {
                            validate: valid
                        });
                        return valid;
                    };
                    customerDataService.getCustomerByCode(customerCode, function (result) {
                        if (result.customerID) {
                            vm.customer.referralCustomerName = result.customerName;
                        } else {
                            valid = false;
                        }
                        mcsValidationService.check($('#referralCustomerCode'), {
                            validate: valid
                        });
                        return valid;
                    });
                };

                // 身份证号码验证
                vm.isIdCard = function (idType, idNumber, targetID) {
                    return customerDataViewService.isIdCard(idType, idNumber, targetID);
                };

                // 家长学员姓名验证
                vm.checkName = function (target) {
                    var valid = !(vm.parent.parentName && vm.customer.customerName && vm.parent.parentName.trim() == vm.customer.customerName.trim());
                    mcsValidationService.check($(target), {
                        validate: valid
                    });
                    return valid;
                };

                // 获取现住址详细地址
                vm.getAddressDetail = function () {
                    return customerDataViewService.getAddressDetail(vm);
                };

                // 信息来源切换
                $scope.$watch('vm.customer.sourceMainType', function () {
                    if (!vm.customer || !vm.customer.sourceMainType) return;
                    if (vm.customer.sourceMainType != 617) {
                        vm.customer.referralStaffOACode = '';
                        vm.customer.referralStaffJobID = '';
                        vm.customer.referralCustomerCode = '';
                        vm.getStaffByOA(vm.customer.referralStaffOACode);
                        vm.getCustomerByCode(vm.customer.referralCustomerCode);
                        ppts.dict['c_codE_ABBR_referralStaffJobName'] = [];
                        $scope.$broadcast('dictionaryReady');
                    }
                });

                // 家长证件类型切换
                $scope.$watch('vm.parent.idType', function (idType) {
                    if (vm.parent && vm.parent.idNumber) {
                        vm.isIdCard(idType, vm.parent.idNumber, '#parentIdNumber');
                    }
                });
                // 学员证件类型切换
                $scope.$watch('vm.customer.idType', function (idType) {
                    if (vm.customer && vm.customer.idNumber) {
                        vm.isIdCard(idType, vm.customer.idNumber, '#customerIdNumber');
                    }
                });

                // vip客户
                $scope.$watch('vm.customer.vipType', function (vipType) {
                    if (vipType == 3) {
                        vm.customer.vipLevel = '';
                        $scope.$broadcast('dictionaryReady');
                    }
                });

                // 家长、学员姓名只能输入英文和汉字
                $scope.$watch('vm.parent.parentName', function (parentName) {
                    if (!parentName) return;
                    vm.parent.parentName = parentName.replace(/[^\a-zA-Z\u4E00-\u9FA5]/g, '');
                });

                $scope.$watch('vm.customer.customerName', function (customerName) {
                    if (!customerName) return;
                    vm.customer.customerName = customerName.replace(/[^\a-zA-Z\u4E00-\u9FA5]/g, '');
                });

                // 在读学校
                $scope.$watch('vm.customer.schoolName', function (schoolName) {
                    if (vm.customer.schoolName) {
                    }
                });

                // 客户资源归属地
                $scope.$watch('vm.orgInfoSelected', function (selected) {
                    if (selected) {
                        vm.customer.orgID = vm.campusID || vm.branchID;
                        vm.customer.orgName = vm.campusID ? selected[1].value : selected[0].value;
                        vm.customer.orgType = vm.campusID ? orgType.campus : (vm.branchID ? orgType.branch : '');
                        vm.customer.campusID = vm.campusID || '';
                        vm.customer.campusName = vm.campusID ? selected[1].value: '';
                    }
                });
            }]);
    });