define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.discountDataService,
        ppts.config.dataServiceConfig.customerService],
        function (customer) {
            customer.registerController('discountAddController', ['$scope', '$state', '$stateParams', 'discountDataService', 'discountDataViewService', 'mcsDialogService', 'discountMaximum', 'discountEditData', 'discountCampusData', 'mcsValidationService',
            function ($scope, $state, $stateParams, discountDataService, discountDataViewService, mcsDialogService, discountMaximum, discountData, discountCampusData, mcsValidationService) {
                var vm = this;
                mcsValidationService.init($scope);
                // 页面初始化加载
                (function () {
                    discountDataService.getDiscountForCreate(function (result) {
                        vm.discount = result.discount;
                        discountData.rows = [];
                        discountCampusData.rows = [];
                        for (var i = 0; i < discountMaximum; i++) {
                            discountData.rows.push({
                                stall: i + 1,
                                discountStandard: '',
                                discountValue: '',
                                validStandard: true,
                                validValue: true
                            });
                            discountCampusData.rows.push({
                                campusName: '',
                                usedState: '-',
                                endTime: '-'
                            });
                        }
                        discountDataViewService.configDiscountAddDataTable(vm, discountData, discountCampusData);
                    });
                })();

                vm.updateDiscountStandardRank = function (row, index) {
                    if (!row.discountStandard) return;
                    var result = true;
                    if (index == 0) {
                        var next = discountData.rows[index + 1];
                        if (!next.discountStandard) return;
                        result = row.discountStandard < next.discountStandard;
                    } else if (index == discountData.rows.length - 1) {
                        var last = discountData.rows[index - 1];
                        if (!last.discountStandard) {
                            last.validStandard = false;
                            return;
                        }
                        result = row.discountStandard > last.discountStandard;
                    } else {
                        var last = discountData.rows[index - 1];
                        var next = discountData.rows[index + 1];
                        if (!last.discountStandard) {
                            last.validStandard = false;
                            return;
                        } else {
                            result = row.discountStandard > last.discountStandard;
                            if (result) {
                                if (next.discountStandard) {
                                    result = row.discountStandard < next.discountStandard;
                                }
                            }
                        }
                    }
                    row.validStandard = result;
                    $scope.$apply('discountData');
                    return result;
                };

                vm.updateDiscountValueRank = function (row, index) {
                    vm.errorRowMessage = '折扣率不能大于1!';
                    if (!row.discountValue) return;
                    var result = (row.discountValue < 1);
                    row.validValue = result;
                    if (result)
                        vm.errorRowMessage = '需小于上档大于下档!';
                    if (index == 0) {
                        var next = discountData.rows[index + 1];
                        if (next.discountValue != "")
                            result = row.discountValue > next.discountValue;
                    } else if (index == discountData.rows.length - 1) {
                        var last = discountData.rows[index - 1];
                        if (!last.discountValue) {
                            last.validValue = false;
                            return;
                        }
                        result = row.discountValue < last.discountValue;
                    } else {
                        var last = discountData.rows[index - 1];
                        var next = discountData.rows[index + 1];
                        if (!last.discountValue) {
                            last.validValue = false;
                            return;
                        } else {
                            result = row.discountValue < last.discountValue;
                            if (result) {
                                if (next.discountValue) {
                                    result = row.discountValue > next.discountValue;
                                }
                            }
                        }
                    }
                    row.validValue = result;
                    $scope.$apply('discountData');
                    return result;
                };

                vm.addRow = function () {
                    vm.relationData.rows.push({
                        stall: vm.relationData.rows.length + 1,
                        discountStandard: '',
                        discountValue: '',
                        validStandard: true,
                        validValue: true
                    });
                    vm.campusData.rows.push({
                        campusName: '',
                        usedState: '-',
                        endTime: '-'
                    });
                }

                vm.removeRow = function () {
                    if (vm.relationData.rows.length > discountMaximum) {
                        vm.relationData.rows.pop();
                        vm.campusData.rows.pop();
                    }
                    else {
                        vm.errorMessage = mcs.util.format('行数不能少于{0}行！', discountMaximum);
                    }
                }

                vm.save = function () {
                    if (mcsValidationService.run($scope)) {
                        if (discountData.rows[0].discountStandard == "" || discountData.rows[0].discountValue == "") {
                            vm.errorMessage = '充值额与折扣率至少輸入一条记录！';
                            return;
                        }
                        for (var value in discountData.rows) {
                            if (!discountData.rows[value].validStandard || !discountData.rows[value].validValue) {
                                vm.errorMessage = '充值折扣关系表填写的值验证不通过,请重新填写！';
                                return;
                            }
                            if (discountData.rows[value].discountStandard && !discountData.rows[value].discountValue) {
                                vm.errorMessage = '充值额与折扣率对应关系有误，请重新填写！';
                                return;
                            }
                            if (!discountData.rows[value].discountStandard && discountData.rows[value].discountValue) {
                                vm.errorMessage = '充值额与折扣率对应关系有误，请重新填写！';
                                return;
                            }
                        }
                        var createDiscountModel = { discount: { startDate: vm.criteria.startDate }, discountPermissionsApplieCollection: [], discountItemCollection: discountData.rows };
                        for (var i in vm.criteria.orgIds) {
                            createDiscountModel.discountPermissionsApplieCollection.push({ campusID: vm.criteria.orgIds[i] });
                        }
                        discountDataService.createDiscount(createDiscountModel, function () {
                            $state.go('ppts.discount');
                        });
                    }
                };
            }]);
        });