define([ppts.config.modules.customer],
    function (customer) {
        customer.registerController('studentViewController', ['$state', '$stateParams', '$location',
            function ($state, $stateParams, $location) {

                var vm = this;
                vm.page = $location.$$search.prev;
                vm.customerId = $stateParams.id;

                vm.tabs = [{
                    title: '基础信息',
                    active: true,
                    permission: '',
                    menus: [{
                        url: 'ppts.student-view.profiles({prev:vm.page})',
                        title: '基本信息',
                        permission: ''
                    }, {
                        url: 'ppts.student-view.parents({prev:vm.page})',
                        title: '学员家长',
                        permission: ''
                    }, {
                        url: 'ppts.student-view.parent-new({prev:vm.page})',
                        title: '新建家长',
                        permission: ''
                    }, {
                        url: 'ppts.student-view.returnExpense({prev:vm.page})',
                        title: '返还服务费',
                        permission: ''
                    }]
                }, {
                    title: '接触信息',
                    active: false,
                    menus: [{
                        url: 'ppts.student-view.follows({prev:vm.page})',
                        title: '跟进记录'
                    }, {
                        url: 'ppts.student-view.visits({prev:vm.page})',
                        title: '回访记录'
                    }, {
                        url: 'ppts.student-view.score({prev:vm.page})',
                        title: '考试成绩'
                    }, {
                        url: 'ppts.student-view.studentmeetinglist({prev:vm.page})',
                        title: '教学服务会'
                    }, {
                        url: 'ppts.student-view.feedbacks({prev:vm.page})',
                        title: '家校互动'
                    }]
                }, {
                    title: '账户信息',
                    active: false,
                    menus: [{
                        url: 'ppts.student-view.account({prev:vm.page})',
                        title: '账户信息'
                    }, {
                        url: 'ppts.student-view.accountRecordList({prev:vm.page})',
                        title: '账户日志'
                    }, {
                        url: 'ppts.student-view.accountChargeEdit({prev:vm.page,path:\'tabnew\'})',
                        title: '充值'
                    }, {
                        url: 'ppts.student-view.accountTransferEdit({prev:vm.page})',
                        title: '转让'
                    }, {
                        url: 'ppts.student-view.accountRefundEdit({prev:vm.page})',
                        title: '退费'
                    }, {
                        url: 'ppts.student-view.accountChargeList({prev:vm.page})',
                        title: '充值记录'
                    }, {
                        url: 'ppts.student-view.accountTransferList({prev:vm.page})',
                        title: '转让记录'
                    }, {
                        url: 'ppts.student-view.purchaseHistory({stuId:"' + vm.customerId + '",prev:vm.page})',
                        title: '订购历史'
                    }]
                }, {
                    title: '排课信息',
                    active: false,
                    menus: [{
                        url: 'ppts.student-view.stuAsgmtConditionEdit({prev:vm.page})',
                        title: '排课条件'
                    }, {
                        url: 'ppts.student-view.stuWeekCourse({prev:vm.page})',
                        title: '课表'
                    }, {
                        url: 'ppts.student-view.studentRecordList({prev:vm.page})',
                        title: '上课记录'
                    }, {
                        url: 'ppts.student-view.classList({prev:vm.page})',
                        title: '班组班级'
                    }]
                }, {
                    title: '预警信息',
                    active: false,
                    menus: [{
                        url: 'ppts.student-view.stopalerts({prev:vm.page})',
                        title: '停课休学'
                    }, {
                        url: 'ppts.student-view.refundalerts({prev:vm.page})',
                        title: '退费预警'
                    }]
                }];

                vm.switchView = function (scope) {
                    angular.forEach(vm.tabs, function (tab) {
                        tab.active = false;
                    });
                    scope.$parent.tab.active = true;
                };
            }]);
    });
