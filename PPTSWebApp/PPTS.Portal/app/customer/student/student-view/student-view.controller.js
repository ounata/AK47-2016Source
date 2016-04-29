define([ppts.config.modules.customer],
    function (customer) {
        customer.registerController('studentViewController', ['$state', '$location',
            function ($state, $location) {
                var vm = this;
                vm.page = $location.$$search.prev;

                vm.tabs = [{
                    title: '基本信息',
                    route: 'ppts.student-view.profiles',
                    active: $state.includes('ppts.student-view.profiles') || $state.includes('ppts.student-view.profiles-edit'),
                    menus: [{
                        url: 'ppts.student-view.profiles({prev:vm.page})',
                        title: '基础信息'
                    }, {
                        url: 'ppts.student-view.parents({prev:vm.page})',
                        title: '学员家长'
                    }]
                }, {
                    title: '接触信息',
                    route: 'parents',
                    active: $state.includes('ppts.student-view.parents') || $state.includes('ppts.student-view.parents-edit'),
                    menus: [{
                        url: '',
                        title: '跟进记录'
                    }, {
                        url: '',
                        title: '回访记录'
                    }, {
                        url: '',
                        title: '考试成绩'
                    }, {
                        url: '',
                        title: '教学服务会'
                    }, {
                        url: '',
                        title: '家校互动'
                    }]
                }, {
                    title: '账户信息',
                    route: 'follows',
                    active: $state.includes('ppts.student-view.follows'),
                    menus: [{
                        url: '',
                        title: '账户信息'
                    }, {
                        url: '',
                        title: '充值记录'
                    }, {
                        url: '',
                        title: '订购历史'
                    }, {
                        url: '',
                        title: '转让记录'
                    }]
                }, {
                    title: '排课信息',
                    route: 'follows',
                    active: $state.includes('ppts.student-view.follows'),
                    menus: [{
                        url: '',
                        title: '排课条件'
                    }, {
                        url: '',
                        title: '课表'
                    }, {
                        url: '',
                        title: '上课记录'
                    }, {
                        url: '',
                        title: '班组班级'
                    }]
                }, {
                    title: '预警信息',
                    route: 'follows',
                    active: $state.includes('ppts.student-view.follows'),
                    menus: [{
                        url: '',
                        title: '停课休学'
                    }, {
                        url: '',
                        title: '退费预警'
                    }]
                }];


            }]);
    });