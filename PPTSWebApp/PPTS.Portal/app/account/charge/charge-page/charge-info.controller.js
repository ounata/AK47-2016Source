define([ppts.config.modules.account,
        ppts.config.dataServiceConfig.accountChargeDataService],
        function (account) {
            account.registerController('accountChargeInfoController', [
                '$scope', '$state', '$stateParams', '$location', 'mcsDialogService', 'accountChargeDataService',
                function ($scope, $state, $stateParams, $location, mcsDialogService, accountDataService) {
                    var vm = this;
                    vm.page = $location.$$search.prev;
                    vm.applyID = $stateParams.applyID;

                    vm.data = {
                        selection: 'checkbox',
                        rowsSelected: [],
                        keyFields: ['sortNo'],
                        headers: [{
                            field: "teacherOACode",
                            name: "OA账号",
                            headerCss: "col-sm-2",
                            template: '<span>{{row.teacherOACode}}</span>'
                        }, {
                            field: "teacherName",
                            name: "教师姓名",
                            headerCss: "col-sm-2",
                            template: '<span>{{row.teacherName}}</span>'
                        }, {
                            field: "subject",
                            name: "科目",
                            headerCss: "col-sm-2",
                            template: '<span>{{row.subject | subject}}</span>'
                        }, {
                            field: "categoryType",
                            name: "产品类型",
                            headerCss: "col-sm-2",
                            template: '<span>{{row.categoryType | categoryType}}</span>'
                        }, {
                            name: "岗位",
                            headerCss: "col-sm-1",
                            template: '<span>校教学教师</span>'
                        }, {
                            field: "teacherType",
                            name: "教师类型",
                            headerCss: "col-sm-1",
                            template: '<span>{{row.tearcherType | teacherType}}</span>'
                        }, {
                            field: "allotMoney",
                            name: "金额",
                            template: '<span>{{row.allotMoney | currency:"￥"}}</span>'
                        }, {
                            field: "allotAmount",
                            name: "课时",
                            template: '<span>{{row.allotAmount}}</span>'
                        }],
                        pager: {
                            pagable: false
                        },
                        orderBy: [{ dataField: 'sortNo', sortDirection: 1 }]
                    }
                    // 页面初始化加载或重新搜索时查询
                    vm.init = function () {
                        accountDataService.getChargeApplyByApplyID4Allot(vm.applyID, function (result) {
                            vm.apply = result.apply
                            vm.customer = result.customer;                            
                            vm.data.rows = result.apply.allot.items;

                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.init();
                    
                    //定向到编辑页面
                    vm.goEdit = function () {

                        $state.go('ppts.accountCharge-view.edit', {
                            customerID: vm.customer.customerID,
                            prev: vm.page,
                            path: 'tabupd'
                        })
                    }
                }]);
        });