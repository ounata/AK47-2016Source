/*按学员排课列表*/
define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.studentAssignmentDataService],
        function (schedule) {
            schedule.registerController('stuAsgmtListController', [
                '$scope', '$state', 'dataSyncService', 'studentassignmentDataService', 'mcsDialogService',
                function ($scope, $state, dataSyncService, studentassignmentDataService, mcsDialogService) {
                    var vm = this;
                    vm.data = {
                        selection: 'radio',
                        rowsSelected: [],
                        keyFields: ['customerID'],
                        headers: [{
                            field: "customerName",
                            name: "学员姓名",
                            template: '<span>{{row.customerName}}</span>'
                        }, {
                            field: "customerCode",
                            name: "学员编号",
                            template: '<span>{{row.customerCode}}</span>'
                        }, {
                            field: "gender",
                            name: "性别",
                            template: '<span>{{row.gender | gender }}</span>'
                        }, {
                            field: "birthday",
                            name: "出生日期",
                            template: '<span>{{row.birthday | date:"yyyy-MM-dd" | normalize }}</span>'
                        }, {
                            field: "schoolName",
                            name: "在读学校",
                            template: '<span>{{row.schoolName}}</span>'
                        }, {
                            field: "grade",
                            name: "当前年级",
                            template: '<span>{{row.grade | grade}}</span>'
                        }, {
                            field: "educatorName",
                            name: "学管师",
                            template: '<span>{{row.educatorName}}</span>'
                        }, {
                            field: "assetOneToOneAmount",
                            name: "剩余数量",
                            template: '<span>{{row.assetOneToOneAmount}}</span>'
                        }],
                        pager: {
                            pageIndex: 1,
                            pageSize: 20,
                            totalCount: -1,
                            pageChange: function () {
                                dataSyncService.initCriteria(vm);
                                studentassignmentDataService.getPagedStuUnAsgmt(vm.criteria, function (result) {
                                    vm.data.rows = result.pagedData;
                                });
                            }
                        },
                        orderBy: [{ dataField: 'CustomerCode', sortDirection: 1 }]
                    }

                    /*页面初始化加载或重新搜索时查询*/
                    vm.search = function () {
                        dataSyncService.initCriteria(vm);
                        studentassignmentDataService.getAllStuUnAsgmt(vm.criteria, function (result) {
                            vm.data.rows = result.queryResult.pagedData;
                            dataSyncService.injectDictData();
                            dataSyncService.updateTotalCount(vm, result.queryResult);
                            $scope.$broadcast('dictionaryReady');
                        }, function (error) {
                        });
                    };
                    vm.search();

                    /*跳转排课*/
                    vm.stuAssignClick = function () {
                        if (vm.data.rowsSelected[0] == undefined) {
                            vm.showMsg("请选择一个学员");
                            return;
                        }
                        $state.go('ppts.stuasgmt-course', { id: vm.data.rowsSelected[0].customerID });
                    };


                    vm.showMsg = function (msg) {
                        mcsDialogService.error({ title: '提示信息', message: msg });
                    };


                }]);
        });