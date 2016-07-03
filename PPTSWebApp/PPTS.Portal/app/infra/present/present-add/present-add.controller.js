define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.presentDataService,
        ppts.config.dataServiceConfig.customerService],
        function (customer) {
            customer.registerController('presentAddController', ['$scope', '$state', '$stateParams', 'presentDataService', 'presentDataViewService', 'mcsDialogService', 'presentMaximum', 'presentEditData', 'presentCampusData', 'mcsValidationService',
            function ($scope, $state, $stateParams, presentDataService, presentDataViewService, mcsDialogService, presentMaximum, presentData, presentCampusData, mcsValidationService) {
                var vm = this;
                mcsValidationService.init($scope);
                // 页面初始化加载
                (function () {
                    presentDataService.getPresentForCreate(function (result) {
                        vm.present = result.present;
                        presentData.rows = [];
                        presentCampusData.rows = [];
                        for (var i = 0; i < presentMaximum; i++) {
                            presentData.rows.push({
                                stall: i + 1,
                                presentStandard: '',
                                presentValue: '',
                                validStandard: true,
                                validValue: true
                            });
                            presentCampusData.rows.push({
                                campusName: '',
                                usedState: '-',
                                endTime: '-'
                            });
                        }
                        presentDataViewService.configPresentAddDataTable(vm, presentData, presentCampusData);
                    });

                })();

                vm.updatePresentStandardRank = function (row, index) {
                    var result = vm.checkZero(row.presentStandard, 0);
                    if (result.result) {
                        var last = presentData.rows[index - 1];
                        var next = presentData.rows[index + 1];

                        var checkResult = vm.check(row.presentStandard, last ? last.presentStandard : null, next ? next.presentStandard : null, 0, index);
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

                vm.updatePresentValueRank = function (row, index) {
                    var result = vm.checkZero(row.presentValue, 0);
                    if (result.result) {
                        var last = presentData.rows[index - 1];
                        var next = presentData.rows[index + 1];


                        var checkResult = vm.check(row.presentValue, last ? last.presentValue : null, next ? next.presentValue : null, 0, index);
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
                        presentStandard: '',
                        presentValue: '',
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
                    if (vm.relationData.rows.length > presentMaximum) {
                        vm.relationData.rows.pop();
                        vm.campusData.rows.pop();
                    }
                    else {
                        vm.errorMessage = mcs.util.format('行数不能少于{0}行！', presentMaximum);
                    }
                }

                vm.save = function () {

                        if (presentData.rows[0].presentStandard == "" || presentData.rows[0].presentValue == "") {
                            vm.errorMessage = '买赠表主要内容不得为空，请填写后再保存！';
                            return;
                        }
                        for (var value in presentData.rows) {
                            if (!presentData.rows[value].validStandard || !presentData.rows[value].validValue) {
                                vm.errorMessage = '购买与赠送关系表填写的值验证不通过,请重新填写！';
                                return;
                            }
                            if (presentData.rows[value].presentStandard && !presentData.rows[value].presentValue) {
                                vm.errorMessage = '购买数量和赠送数量应该配对，请填写后再提交审批！';
                                return;
                            }
                            if (!presentData.rows[value].presentStandard && presentData.rows[value].presentValue) {
                                vm.errorMessage = '购买数量和赠送数量应该配对，请填写后再提交审批！';
                                return;
                            }
                        }
                        if (mcsValidationService.run($scope)) {
                        var createPresentModel = { present: { startDate: vm.criteria.startDate }, presentPermissionsApplieCollection: [], presentItemCollection: presentData.rows };
                        for (var i in vm.criteria.orgIds) {
                            createPresentModel.presentPermissionsApplieCollection.push({ campusID: vm.criteria.orgIds[i] });
                        }
                        presentDataService.createPresent(createPresentModel, function () {
                            $state.go('ppts.present');
                        });
                    }
                };
            }]);
        });