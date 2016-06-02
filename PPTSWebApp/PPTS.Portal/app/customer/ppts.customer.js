define(['angular'], function (ng) {
    var customer = ng.module('ppts.customer', []);

    // 配置provider
    customer.config(function ($controllerProvider, $compileProvider, $filterProvider, $provide) {
        mcs.util.configProvider(customer, $controllerProvider, $compileProvider, $filterProvider, $provide);
    });
    // 配置路由
    customer.config(function ($stateProvider) {
        mcs.util.loadRoute($stateProvider, {
            name: 'ppts.customer',
            url: '/customer',
            templateUrl: 'app/customer/potentialcustomer/customer-list/customer-list.html',
            controller: 'customerListController',
            breadcrumb: {
                label: '潜在客户列表查询',
                parent: 'ppts'
            },
            dependencies: ['app/customer/potentialcustomer/customer-list/customer-list.controller',
                           'app/customer/potentialcustomer/customer-staff-relations/customer-staff-relations.controller',
                           'app/customer/potentialcustomer/customer-transfer/customer-transfer.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.market',
            url: '/customer/market',
            templateUrl: 'app/customer/market/market-list/market-list.html',
            controller: 'marketListController',
            breadcrumb: {
                label: '市场客户资源列表查询',
                parent: 'ppts'
            },
            dependencies: ['app/customer/market/market-list/market-list.controller',
                           'app/customer/potentialcustomer/customer-staff-relations/customer-staff-relations.controller',
                           'app/customer/potentialcustomer/customer-transfer/customer-transfer.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.customer-add',
            url: '/customer/add',
            templateUrl: 'app/customer/potentialcustomer/customer-add/customer-add.html',
            controller: 'customerAddController',
            breadcrumb: {
                label: '新增潜客',
                parent: 'ppts.customer'
            },
            dependencies: ['app/customer/potentialcustomer/customer-add/customer-add.controller',
                           'app/customer/potentialcustomer/customer-add/ppts.customer.relation',
                           'app/customer/potentialcustomer/customer-parent-add/parent-add.controller']
        }).loadRoute($stateProvider, {
            abstract: true,
            name: 'ppts.customer-view',
            url: '/customer/view/:id',
            templateUrl: 'app/customer/potentialcustomer/customer-view/customer-view.html',
            controller: 'customerViewController',
            dependencies: ['app/customer/potentialcustomer/customer-view/customer-view.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.customer-view.profiles',
            url: '/profiles?prev=:page',
            templateUrl: 'app/customer/potentialcustomer/customer-info/customer-info.tpl.html',
            controller: 'customerInfoController',
            breadcrumb: {
                label: '我的基本信息'
            },
            dependencies: ['app/customer/potentialcustomer/customer-info/customer-info.controller',
                           'app/customer/potentialcustomer/customer-parent-add/parent-add.controller',
                           'app/customer/potentialcustomer/customer-staff-relations/customer-staff-relations.controller',
                           'app/customer/potentialcustomer/customer-add/ppts.customer.relation',
                           'app/customer/student/student-thaw/student-thaw.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.customer-view.profile-edit',
            url: '/profiles/edit?prev=:page',
            templateUrl: 'app/customer/potentialcustomer/customer-edit/customer-edit.tpl.html',
            controller: 'customerEditController',
            breadcrumb: {
                label: '修改潜客'
            },
            dependencies: ['app/customer/potentialcustomer/customer-edit/customer-edit.controller',
                           'app/customer/potentialcustomer/customer-staff-relations/customer-staff-relations.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.customer-view.parents',
            url: '/parents?prev=:page',
            templateUrl: 'app/customer/potentialcustomer/customer-parents/parents-view.tpl.html',
            controller: 'parentsViewController',
            breadcrumb: {
                label: '我的家长'
            },
            dependencies: ['app/customer/potentialcustomer/customer-parents/parents-view.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.customer-view.parents-edit',
            url: '/parents/:parentId?prev=:page',
            templateUrl: 'app/customer/potentialcustomer/customer-parents/parent-edit.tpl.html',
            controller: 'parentEditController',
            breadcrumb: {
                label: '修改家长'
            },
            dependencies: ['app/customer/potentialcustomer/customer-parents/parent-edit.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.customer-view.parent-new',
            url: '/parent/new?prev=:page',
            templateUrl: 'app/customer/potentialcustomer/customer-parents/parent-new.tpl.html',
            controller: 'parentNewController',
            breadcrumb: {
                label: '新建家长'
            },
            dependencies: ['app/customer/potentialcustomer/customer-parents/parent-new.controller',
                           'app/customer/potentialcustomer/customer-add/ppts.customer.relation']
        }).loadRoute($stateProvider, {
            name: 'ppts.customer-view.follows',
            url: '/follows?prev=:page',
            templateUrl: 'app/customer/follow/follow-view/follow-view.html',
            controller: 'followListController',
            breadcrumb: {
                label: '我的跟进记录',
            },
            dependencies: ['app/customer/follow/follow-view/follow-view.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.follow',
            url: '/follow',
            templateUrl: 'app/customer/follow/follow-list/follow-list.html',
            controller: 'followListController',
            breadcrumb: {
                label: '跟进管理',
                parent: 'ppts'
            },
            dependencies: ['app/customer/follow/follow-list/follow-list.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.follow-add',
            url: '/follow/add/:customerId?prev=:page',
            templateUrl: 'app/customer/follow/follow-add/follow-add.html',
            controller: 'followAddController',
            breadcrumb: {
                label: '新增跟进记录'
            },
            dependencies: ['app/customer/follow/follow-add/follow-add.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.follow-view',
            url: '/follow/view/:followId?prev=:page',
            templateUrl: 'app/customer/follow/follow-info/follow-info.html',
            controller: 'followInfoController',
            breadcrumb: {
                label: '查看跟进记录',
                parent: 'ppts.follow'
            },
            dependencies: ['app/customer/follow/follow-info/follow-info.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.customerverify',
            url: '/customerverify',
            templateUrl: 'app/customer/customerverify/customerverify-list/customerverify-list.html',
            controller: 'customerVerifyListController',
            breadcrumb: {
                label: '上门管理',
                parent: 'ppts'
            },
            dependencies: ['app/customer/customerverify/customerverify-list/customerverify-list.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.customerverify-add',
            url: '/customerverify/add/:customerId?prev=:page',
            templateUrl: 'app/customer/customerverify/customerverify-add/customerverify-add.html',
            controller: 'customerVerifyAddController',
            breadcrumb: {
                label: '新增上门记录',
                parent: 'ppts.customerverify'
            },
            dependencies: ['app/customer/customerverify/customerverify-add/customerverify-add.controller',
                           'app/customer/potentialcustomer/customer-search/customer-search.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.customer-staffrelation',
            url: '/customer/staffrelation/:id/:type',
            templateUrl: 'app/customer/potentialcustomer/customer-staffrelation/customer-staffrelation.html',
            controller: 'customerStaffRelationController',
            dependencies: ['app/customer/potentialcustomer/customer-staffrelation/customer-staffrelation.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.student',
            url: '/student',
            templateUrl: 'app/customer/student/student-list/student-list.html',
            controller: 'studentListController',
            breadcrumb: {
                label: '学员管理列表查询',
                parent: 'ppts'
            },
            dependencies: ['app/customer/student/student-list/student-list.controller',
                           'app/customer/potentialcustomer/customer-staff-relations/customer-staff-relations.controller',
                           'app/customer/student/student-transfer/student-transfer.controller',
                           'app/customer/student/student-assignTeacher/student-assignTeacher.controller',
                           'app/customer/customervisit/customervisit-addBatch/customervisit-addBatch.controller'
            ]
        }).loadRoute($stateProvider, {
            name: 'ppts.student-view.stopalerts',
            url: '/stopalerts?prev=:page',
            templateUrl: 'app/customer/stopalerts/stop-list/stop-list.html',
            controller: 'stopAlertsListController',
            breadcrumb: {
                label: '停课休学',
            },
            dependencies: ['app/customer/stopalerts/stop-list/stop-list.controller',
                           'app/customer/stopalerts/stop-add/stop-add.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.student-view.refundalerts',
            url: '/refundalerts?prev=:page',
            templateUrl: 'app/customer/refundalerts/refund-list/refund-list.html',
            controller: 'refundAlertsListController',
            breadcrumb: {
                label: '退费预警',
            },
            dependencies: ['app/customer/refundalerts/refund-list/refund-list.controller',
                           'app/customer/refundalerts/refund-add/refund-add.controller',
                           'app/customer/refundalerts/refund-edit/refund-edit.controller']
        }).loadRoute($stateProvider, {
            abstract: true,
            name: 'ppts.student-view',
            url: '/student/view/:id',
            templateUrl: 'app/customer/student/student-view/student-view.html',
            controller: 'studentViewController',
            dependencies: ['app/customer/student/student-view/student-view.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.student-view.profiles',
            url: '/profiles?prev=:page',
            templateUrl: 'app/customer/potentialcustomer/customer-info/customer-info.tpl.html',
            controller: 'customerInfoController',
            breadcrumb: {
                label: '基本信息'
            },
            dependencies: ['app/customer/potentialcustomer/customer-info/customer-info.controller',
                           'app/customer/student/student-thaw/student-thaw.controller',
                           'app/customer/potentialcustomer/customer-parent-add/parent-add.controller',
                           'app/customer/potentialcustomer/customer-staff-relations/customer-staff-relations.controller',
                           'app/customer/potentialcustomer/customer-add/ppts.customer.relation',
                           'app/customer/potentialcustomer/customer-teacher-relations/customer-teacher-relations.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.student-view.parents',
            url: '/parents?prev=:page',
            templateUrl: 'app/customer/potentialcustomer/customer-parents/parents-view.tpl.html',
            controller: 'parentsViewController',
            breadcrumb: {
                label: '学员家长'
            },
            dependencies: ['app/customer/potentialcustomer/customer-parents/parents-view.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.student-view.profile-edit',
            url: '/profiles/edit?prev=:page',
            templateUrl: 'app/customer/potentialcustomer/customer-edit/customer-edit.tpl.html',
            controller: 'customerEditController',
            breadcrumb: {
                label: '修改学员'
            },
            dependencies: ['app/customer/potentialcustomer/customer-edit/customer-edit.controller',
                           'app/customer/potentialcustomer/customer-staff-relations/customer-staff-relations.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.student-view.parents-edit',
            url: '/parents/:parentId?prev=:page',
            templateUrl: 'app/customer/potentialcustomer/customer-parents/parent-edit.tpl.html',
            controller: 'parentEditController',
            breadcrumb: {
                label: '修改家长'
            },
            dependencies: ['app/customer/potentialcustomer/customer-parents/parent-edit.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.student-view.parent-new',
            url: '/parent/new?prev=:page',
            templateUrl: 'app/customer/potentialcustomer/customer-parents/parent-new.tpl.html',
            controller: 'parentNewController',
            breadcrumb: {
                label: '新建家长'
            },
            dependencies: ['app/customer/potentialcustomer/customer-parents/parent-new.controller',
                           'app/customer/potentialcustomer/customer-add/ppts.customer.relation']
        }).loadRoute($stateProvider, {
            name: 'ppts.customermeeting',
            url: '/customermeeting',
            templateUrl: 'app/customer/customermeeting/customermeeting-list/customermeeting-list.html',
            controller: 'customerMeetingListController',
            breadcrumb: {
                label: '教学服务会',
                parent: 'ppts'
            },
            dependencies: ['app/customer/customermeeting/customermeeting-list/customermeeting-list.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.student-view.studentmeetinglist',
            url: '/studentmeeting-list?prev=:page',
            templateUrl: 'app/customer/customermeeting/studentmeeting-list/studentmeeting-list.html',
            controller: 'studentMeetingListController',
            breadcrumb: {
                label: '我的教学服务会'
            },
            dependencies: ['app/customer/customermeeting/studentmeeting-list/studentmeeting-list.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.student-view.customermeeting-add',
            url: '/customermeeting-add?prev=:page',
            templateUrl: 'app/customer/customermeeting/customermeeting-add/customermeeting-add.html',
            controller: 'customerMeetingAddController',
            breadcrumb: {
                label: '新增会议'
            },
            dependencies: ['app/customer/customermeeting/customermeeting-add/customermeeting-add.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.customermeeting-view',
            url: '/customermeeting/view/:id?prev=:page',
            templateUrl: 'app/customer/customermeeting/customermeeting-view/customermeeting-view.html',
            controller: 'customerMeetingViewController',
            breadcrumb: {
                label: '查看会议'
            },
            dependencies: ['app/customer/customermeeting/customermeeting-view/customermeeting-view.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.customermeeting-edit',
            url: '/customermeeting/edit/:id?prev=:page',
            templateUrl: 'app/customer/customermeeting/customermeeting-edit/customermeeting-edit.html',
            controller: 'customerMeetingEditController',
            breadcrumb: {
                label: '编辑会议'
            },
            dependencies: ['app/customer/customermeeting/customermeeting-edit/customermeeting-edit.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.feedback',
            url: '/feedback',
            templateUrl: 'app/customer/feedback/feedback-list/feedback-list.html',
            controller: 'feedbackListController',
            breadcrumb: {
                label: '学大反馈',
                parent: 'ppts'
            },
            dependencies: ['app/customer/feedback/feedback-list/feedback-list.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.feedback-add',
            url: '/feedback/add/:id?prev=:page',
            templateUrl: 'app/customer/feedback/feedback-add/feedback-add.html',
            controller: 'feedbackAddController',
            breadcrumb: {
                label: '联系家长'
            },
            dependencies: ['app/customer/feedback/feedback-add/feedback-add.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.student-view.feedbacks',
            url: '/feedbacks?prev=:page',
            templateUrl: 'app/customer/feedback/feedback-view/feedback-view.html',
            controller: 'feedbackViewController',
            breadcrumb: {
                label: '我的家校互动'
            },
            dependencies: ['app/customer/feedback/feedback-view/feedback-view.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.feedback-view',
            url: '/feedback/view/:customerId/:replyTime?prev=:page',
            templateUrl: 'app/customer/feedback/feedback-view/feedback-view.html',
            controller: 'feedbackViewController',
            breadcrumb: {
                label: '家校互动'
            },
            dependencies: ['app/customer/feedback/feedback-view/feedback-view.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.accountChargeEdit',
            url: '/account/charge/edit/:id?prev=:page',
            templateUrl: 'app/account/charge/charge-page/charge-edit.html',
            controller: 'accountChargeEditController',
            breadcrumb: {
                label: '充值',
            },
            dependencies: ['app/account/charge/charge-page/charge-edit.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.student-view.account',
            url: '/account?prev=:page',
            templateUrl: 'app/account/display/account-page/account-view.html',
            controller: 'accountController',
            breadcrumb: {
                label: '账户信息',
                parent: 'ppts.student'
            },
            dependencies: ['app/account/display/account-page/account-view.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.student-view.accountChargeEdit',
            url: '/account/charge/edit/:path?prev=:page',
            templateUrl: 'app/account/charge/charge-page/charge-edit.html',
            controller: 'accountChargeEditController',
            breadcrumb: {
                label: '充值',
                parent: 'ppts.student'
            },
            dependencies: ['app/account/charge/charge-page/charge-edit.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.student-view.accountTransferEdit',
            url: '/account/transfer/edit?prev=:page',
            templateUrl: 'app/account/transfer/transfer-page/transfer-edit.html',
            controller: 'accountTransferEditController',
            breadcrumb: {
                label: '转让',
                parent: 'ppts.student'
            },
            dependencies: ['app/account/transfer/transfer-page/transfer-edit.controller']

        }).loadRoute($stateProvider, {
            name: 'ppts.student-view.stuAsgmtConditionEdit',
            url: '/schedule/stuasgmt/condition?prev=:page',
            templateUrl: 'app/schedule/studentassignment/stuasgmt-condition/stuasgmt-condition-list.html',
            controller: 'stuAsgmtConditionController',
            breadcrumb: {
                label: '排课条件',
                parent: 'ppts.student'
            },
            dependencies: ['app/schedule/studentassignment/stuasgmt-condition/stuasgmt-condition-list.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.student-view.studentRecordList',
            url: '/schedule/sturecord/list?prev=:page',
            templateUrl: 'app/schedule/studentcourse/sturecord-list.html',
            controller: 'stucrsListController',
            breadcrumb: {
                label: '上课记录',
                parent: 'ppts.student'
            },
            dependencies: ['app/schedule/studentcourse/sturecord-list.controller'
              , 'app/schedule/studentcourse/sturecord-list-markup.controller'
             , 'app/schedule/studentassignment/stuasgmt-add/stuasgmt-add.controller']

        }).loadRoute($stateProvider, {
            name: 'ppts.student-view.accountRefundEdit',
            url: '/account/refund/edit?prev=:page',
            templateUrl: 'app/account/refund/refund-page/refund-edit.html',
            controller: 'accountRefundEditController',
            breadcrumb: {
                label: '退费',
                parent: 'ppts.student'
            },
            dependencies: ['app/account/refund/refund-page/refund-edit.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.student-view.classList',
            url: '/class/list?prev=:page',
            templateUrl: 'app/schedule/classgroup/customerClass-list/customerClass-list.html',
            controller: 'customerClassListController',
            breadcrumb: {
                label: '班级列表',
                parent: 'ppts.student'
            },
            dependencies: ['app/schedule/classgroup/customerClass-list/customerClass-list.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.student-view.accountChargeList',
            url: '/account/charge/list?prev=:page',
            templateUrl: 'app/account/charge/charge-page/charge-list.html',
            controller: 'accountChargeListController',
            breadcrumb: {
                label: '充值记录',
                parent: 'ppts.student'
            },
            dependencies: ['app/account/charge/charge-page/charge-list.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.student-view.accountTransferList',
            url: '/account/transfer/list?prev=:page',
            templateUrl: 'app/account/transfer/transfer-page/transfer-list.html',
            controller: 'accountTransferListController',
            breadcrumb: {
                label: '转让记录',
                parent: 'ppts.student'
            },
            dependencies: ['app/account/transfer/transfer-page/transfer-list.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.student-view.returnExpense',
            url: '/account/return/edit?prev=:page',
            templateUrl: 'app/account/return/return-page/return-edit.html',
            controller: 'accountReturnEditController',
            breadcrumb: {
                label: '服务费返还',
                parent: 'ppts.student'
            },
            dependencies: ['app/account/return/return-page/return-edit.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.student-view.purchaseHistory',
            url: '/order/purchase/history/:stuCode?prev=:page',
            templateUrl: 'app/order/purchase/history/order-history.html',
            controller: 'orderHistoryController',
            breadcrumb: {
                label: '订购历史',
                parent: 'ppts.student'
            },
            dependencies: ['app/order/purchase/history/order-history.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.score',
            url: '/score',
            templateUrl: 'app/customer/score/score-list/score-list.html',
            controller: 'scoreListController',
            breadcrumb: {
                label: '成绩管理列表查询',
                parent: 'ppts'
            },
            dependencies: ['app/customer/score/score-list/score-list.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.score-add',
            url: '/score/add/:id?prev=:page',
            templateUrl: 'app/customer/score/score-add/score-add.html',
            controller: 'scoreAddController',
            breadcrumb: {
                label: '录入成绩'
            },
            dependencies: ['app/customer/score/score-add/score-add.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.score-view',
            url: '/score/view/:id?prev=:page',
            templateUrl: 'app/customer/score/score-view/score-view.html',
            controller: 'scoreViewController',
            breadcrumb: {
                label: '查看成绩'
            },
            dependencies: ['app/customer/score/score-view/score-view.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.score-edit',
            url: '/score/edit/:id?prev=:page',
            templateUrl: 'app/customer/score/score-edit/score-edit.html',
            controller: 'scoreEditController',
            breadcrumb: {
                label: '编辑成绩'
            },
            dependencies: ['app/customer/score/score-edit/score-edit.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.score-batch-add',
            url: '/score/batchadd',
            templateUrl: 'app/customer/score/score-batch-add/score-batch-add.html',
            controller: 'scoreBatchAddController',
            breadcrumb: {
                label: '批量录入成绩',
                parent: 'ppts.score'
            },
            dependencies: ['app/customer/score/score-batch-add/score-batch-add.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.student-view.score',
            url: '/score?prev=:page',
            templateUrl: 'app/customer/score/score-view/score-stu-view.tpl.html',
            controller: 'studentScoreViewController',
            breadcrumb: {
                label: '考试成绩',
                parent: 'ppts.student'
            },
            dependencies: ['app/customer/score/score-view/score-stu-view.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.student-view.follows',
            url: '/follows?prev=:page',
            templateUrl: 'app/customer/follow/follow-view/follow-view.html',
            controller: 'followListController',
            breadcrumb: {
                label: '我的跟进记录',
            },
            dependencies: ['app/customer/follow/follow-view/follow-view.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.customervisit',
            url: '/customervisit',
            templateUrl: 'app/customer/customervisit/customervisit-list/customervisit-list.html',
            controller: 'customerVisitListController',
            breadcrumb: {
                label: '回访记录',
                parent: 'ppts'
            },
            dependencies: ['app/customer/customervisit/customervisit-list/customervisit-list.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.student-view.visits',
            url: '/visits?prev=:page',
            templateUrl: 'app/customer/customervisit/customervisit-view/customervisit-view.html',
            controller: 'customerVisitViewController',
            breadcrumb: {
                label: '我的回访记录'
            },
            dependencies: ['app/customer/customervisit/customervisit-view/customervisit-view.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.student-view.customervisit-add',
            url: '/customervisit-add?prev=:page',
            templateUrl: 'app/customer/customervisit/customervisit-add/customervisit-add.html',
            controller: 'customerVisitSingleListController',
            breadcrumb: {
                label: '录入回访'
            },
            dependencies: ['app/customer/customervisit/customervisit-add/customervisit-add.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.student-view.visit-info',
            url: '/visits/:visitId?prev=:page',
            templateUrl: 'app/customer/customervisit/customervisit-info/customervisit-info.html',
            controller: 'customerVisitInfoController',
            breadcrumb: {
                label: '查看回访'
            },
            dependencies: ['app/customer/customervisit/customervisit-info/customervisit-info.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.student-view.customervisit-edit',
            url: '/customervisit-edit/:visitId?prev=:page',
            templateUrl: 'app/customer/customervisit/customervisit-edit/customervisit-edit.html',
            controller: 'customerVisitEditController',
            breadcrumb: {
                label: '编辑回访'
            },
            dependencies: ['app/customer/customervisit/customervisit-edit/customervisit-edit.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.customervisit-batch',
            url: '/customervisit-batch?prev=:page',
            templateUrl: 'app/customer/customervisit/customervisit-addBatch/customervisit-addBatch.html',
            controller: 'customerVisitAddBatchController',
            breadcrumb: {
                label: '批量录入回访'
            },
            dependencies: ['app/customer/customervisit/customervisit-addBatch/customervisit-addBatch.controller']
        });
    });

    return customer;
});