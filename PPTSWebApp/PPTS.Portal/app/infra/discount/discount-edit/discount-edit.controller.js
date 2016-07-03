define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.discountDataService,
        ppts.config.dataServiceConfig.customerService],
        function (customer) {
            customer.registerController('discountEditController', ['$scope', '$state', '$stateParams', 'discountDataService', 'discountDataViewService', 'mcsDialogService', 'discountMaximum', 'discountEditData', 'discountCampusData', 'mcsValidationService',
            function ($scope, $state, $stateParams, discountDataService, discountDataViewService, mcsDialogService, discountMaximum, discountData, discountCampusData, mcsValidationService) {
                var vm = this;
                mcsValidationService.init($scope);
                // 页面初始化加载
                (function () {
                    discountData.rows = [];
                    discountCampusData.rows = [];
                    var paramsData = [];
                    vm.criteria = {};
                    paramsData.discountId = $stateParams.discountId;
                    discountDataService.getDiscountForView($stateParams.discountId, function (result) {
                        vm.criteria = result.discount;
                        var discountPermissions = result.discountPermissionCollection;
                        vm.criteria.campusNames = [];
                        vm.criteria.usedStates = [];
                        vm.criteria.endTimes = [];
                        vm.criteria.campusIds = [];
                        if (discountPermissions && discountPermissions.length > 0) {
                            for (var i in discountPermissions) {
                                vm.criteria.campusIds.push(discountPermissions[i].campusID);
                                vm.criteria.campusNames.push(discountPermissions[i].campusName);
                                vm.criteria.usedStates.push(discountPermissions[i].campusUseStatus);
                                vm.criteria.endTimes.push(discountPermissions[i].endDateView);

                            }
                        }
                        else {
                            var discountApplies = result.discountPermissionsApplieCollection;
                            if (discountApplies && discountApplies.length > 0) {
                                for (var i in discountApplies) {
                                    vm.criteria.campusIds.push(discountApplies[i].campusID);
                                    vm.criteria.campusNames.push(discountApplies[i].campusName);
                                    vm.criteria.usedStates.push(discountApplies[i].campusUseStatus);
                                    vm.criteria.endTimes.push(discountApplies[i].endDateView);
                                }
                            }
                        }
                        discountDataViewService.configDiscountAddDataTable(vm, discountData, discountCampusData);
                        vm.selectBranch(vm.criteria);

                        var discountItem = result.discountItemCollection;
                        if (discountItem && discountItem.length > 0) {
                            var rowCount = discountItem.length;
                            if (discountItem.length < discountMaximum)
                                rowCount = rowCount + (discountMaximum - rowCount);
                            for (var i = 0; i < rowCount; i++) {
                                discountData.rows.push({
                                    stall: i + 1,
                                    discountStandard: (discountItem[i] ? discountItem[i].discountStandard : 0),
                                    discountValue: (discountItem[i] ? discountItem[i].discountValue : 0),
                                    validStandard: true,
                                    validValue: true
                                });
                            }
                        }
                    });
                })();

                vm.updateDiscountStandardRank = function (row, index) {
                    var result = vm.checkZero(row.discountStandard, 0);
                    if (result.result) {
                        var last = discountData.rows[index - 1];
                        var next = discountData.rows[index + 1];


                        var checkResult = vm.check(row.discountStandard, last ? last.discountStandard : null, next ? next.discountStandard : null, 0, index);
                        if (!checkResult.result) {
                            row.errorStandardMessage = checkResult.message;
                        }
                        row.validStandard = !(!checkResult.result && checkResult.message != '');
                        if (row.validStandard && last) {
                            last.validStandard = true;
                        }
                        if (row.validStandard && next) {
                            next.validStandard = true;
                        }
                    }
                    else {
                        row.errorStandardMessage = result.message;
                        row.validStandard = !(!result.result && result.message != '');
                        if (row.validStandard && last) {
                            last.validStandard = true;
                        }
                        if (row.validStandard && next) {
                            next.validStandard = true;
                        }
                    }
                }

                vm.updateDiscountValueRank = function (row, index) {
                    var result = vm.checkZero(row.discountValue, 1);
                    if (result.result) {
                        var last = discountData.rows[index - 1];
                        var next = discountData.rows[index + 1];


                        var checkResult = vm.check(row.discountValue, last ? last.discountValue : null, next ? next.discountValue : null, 1, index);
                        if (!checkResult.result) {
                            row.errorValueMessage = checkResult.message;
                        }
                        row.validValue = !(!checkResult.result && checkResult.message != '');
                        if (row.validValue && last) {
                            last.validValue = true;
                        }
                        if (row.validValue && next) {
                            next.validValue = true;
                        }
                    }
                    else {
                        row.errorValueMessage = result.message;
                        row.validValue = !(!result.result && result.message != '');
                        if (row.validValue && last) {
                            last.validValue = true;
                        }
                        if (row.validValue && next) {
                            next.validValue = true;
                        }
                    }
                }

                vm.check = function (currentValue, preValue, afterValue, rule, index) {

                    var currentValueGreater = currentValue && currentValue > 0;
                    var currentValueNothing = !currentValue || currentValue == '';

                    var preValueGreater = preValue != undefined && preValue != '' && preValue > 0;
                    var preValueNothing = preValue == undefined || preValue == '';

                    var afterValueGreater = afterValue != undefined && afterValue != '' && afterValue > 0;
                    var afterValueNothing = afterValue || afterValue == '';

                    //检查非第一行
                    var condition1 = (index > 0 && currentValueNothing && afterValueNothing);
                    var condition2 = (index == 0 && currentValueGreater);
                    var condition3 = (currentValueGreater && afterValueGreater) && ((rule == 0 && afterValue > currentValue) || (rule == 1 && afterValue < currentValue));
                    var condition4 = (currentValueGreater && preValueGreater) && ((rule == 0 && preValue < currentValue) || (rule == 1 && preValue > currentValue));

                    if (condition1 || condition2 || condition3 || condition4) {
                        if (preValueGreater && afterValueGreater) {

                            if (condition3 && condition4) {
                                return {
                                    result: true
                                }
                            } else {
                                return {
                                    result: false,
                                    message: (rule == 0 ? '需大于上档小于下档' : '需小于上档大于下档')
                                }
                            }
                        } else {
                            return {
                                result: true
                            }
                        }
                    }
                    else {
                        return {
                            result: false,
                            message: preValueNothing ? '上面不能为空' : (rule == 0 ? '需大于上档小于下档' : '需小于上档大于下档')
                        }
                    }
                }

                vm.checkZero = function (currentValue, rule) {
                    var condition = !(rule == 0 ? currentValue > 0 : currentValue > 0 && currentValue < 1);
                    if (currentValue != undefined && condition) {
                        return {
                            result: false,
                            message: (rule == 0 ? '必须大于0' : '必须大于0小于1')
                        };

                    } else {
                        if (currentValue == NaN || currentValue === '') {
                            return {
                                result: false,
                                message: ''
                            }
                        } else {
                            return {
                                result: true
                            };
                        }
                    }
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
                    if (discountData.rows[0].discountStandard == "" || discountData.rows[0].discountValue == "") {
                        vm.errorMessage = '充值额和折扣率应该配对，请填写后再提交审批！';
                        return;
                    }
                    for (var value in discountData.rows) {
                        if (!discountData.rows[value].validStandard || !discountData.rows[value].validValue) {
                            vm.errorMessage = '充值折扣关系表填写的值验证不通过,请重新填写！';
                            return;
                        }
                        if (discountData.rows[value].discountStandard && !discountData.rows[value].discountValue) {
                            vm.errorMessage = '充值额和折扣率应该配对，请填写后再提交审批！';
                            return;
                        }
                        if (!discountData.rows[value].discountStandard && discountData.rows[value].discountValue) {
                            vm.errorMessage = '充值额和折扣率应该配对，请填写后再提交审批！';
                            return;
                        }
                    }
                    if (mcsValidationService.run($scope)) {
                        var createDiscountModel = { discount: { startDate: vm.criteria.startDate }, discountPermissionsApplieCollection: [], discountItemCollection: discountData.rows };
                        for (var i in vm.criteria.campusIds) {
                            createDiscountModel.discountPermissionsApplieCollection.push({ campusID: vm.criteria.campusIds[i] });
                        }
                        discountDataService.createDiscount(createDiscountModel, function () {
                            $state.go('ppts.discount');
                        });
                    }
                };

            }]);
        });