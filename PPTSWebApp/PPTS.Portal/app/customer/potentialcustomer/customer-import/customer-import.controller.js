define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.customerDataService],
    function (customer) {
        customer.registerController('customerImportController', [
            '$scope',
            'storage',
            '$uibModalInstance',
            'mcsValidationService',
            'customerDataViewService',
            function ($scope, storage, $uibModalInstance, mcsValidationService, customerDataViewService) {
                var vm = this;
                vm.action = ppts.config.customerApiBaseUrl + 'api/potentialcustomers/importcustomers';
                vm.orgType = 
                {
                    branch: 2,
                    campus: 3
                }
                mcsValidationService.init($scope);
                // 关闭
                vm.close = function () {
                    $uibModalInstance.dismiss('Canceled');
                };

                // 上传历史
                vm.viewHistory = function () {
                    customerDataViewService.importHistory();
                };

                // 导入
                vm.submitForm = function () {
                    if (mcsValidationService.run($scope)) {
                        vm.logArea = document.getElementById('logArea');
                        vm.progress = document.getElementById('progress');
                        vm.progressTxt = document.getElementById('progressTxt');
                        vm.fields = document.getElementById('fields');

                        var filePath = document.getElementById('file').value;
                        var fileName = filePath.split('\\')[filePath.split('\\').length - 1];

                        var selected = vm.selected ? mcs.util.toArray(vm.selected.value) : '';
                        var currentJobId = storage.get('ppts.user.currentJobId_' + ppts.user.id);
                        vm.setParam({
                            fileName: fileName,
                            sourceMainType: vm.sourceMainType,
                            sourceSubType: vm.sourceSubType,
                            orgID: vm.campusID || vm.branchID,
                            orgName: vm.campusID ? selected[1] : selected[0],
                            orgType: vm.campusID ? vm.orgType.campus : (vm.branchID ? vm.orgType.branch : ''),
                            pptsCurrentJobID: currentJobId
                        });
                        document.getElementById('uploadForm').submit();
                    }
                }
                vm.setParam = function (param) {
                    $(vm.fields).empty();
                    for (var prop in param) {
                        var input = document.createElement('input');
                        input.type = "hidden";
                        input.name = prop;
                        input.value = param[prop] || '';
                        vm.fields.appendChild(input);
                    }
                }

                window.progressUpdate = function (msgJson) {
                    var msg = JSON.parse(msgJson);
                    vm.update(parseInt((msg.currentStep/msg.maxStep)*100), msg.statusText);
                }
                window.processComplete = function (msgJson) {
                    var msg = JSON.parse(msgJson);
                    if (msg.error) {
                        var p = document.createElement('p');
                        p.innerHTML = msg.error;
                        vm.logArea.appendChild(p);
                    }
                    if (msg.processLog) {
                        var p = document.createElement('p');
                        p.innerHTML = msg.processLog;

                        vm.logArea.appendChild(p);
                    }
                    if (msg.closeWindow == true) {
                        window.close();
                    }
                }
                vm.update = function (progressNum, log) {
                    vm.progress.style.width = progressNum * 4 + 'px';
                    var p = document.createElement('p');
                    p.innerHTML = log;
                    vm.logArea.appendChild(p);
                    vm.progressTxt.innerText = progressNum + '%';
                }
                
            }]);
    });