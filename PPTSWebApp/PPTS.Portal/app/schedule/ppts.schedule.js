define(['angular', 'momentLocale', 'fullCalendar', 'fullCalendarLang', 'ngCalendar'], function (ng) {
    var schedule = ng.module('ppts.schedule', ['ui.calendar']);

    // 配置provider
    schedule.config(function ($controllerProvider, $compileProvider, $filterProvider, $provide) {
        mcs.util.configProvider(schedule, $controllerProvider, $compileProvider, $filterProvider, $provide);
    });


    // 配置路由
    schedule.config(function ($stateProvider) {
        mcs.util.loadRoute($stateProvider, {
            name: 'ppts.schedule',
            url: '/schedule',
            templateUrl: 'app/schedule/studentassignment/stuasgmt-list/stuasgmt-list.html',
            controller: 'stuAsgmtListController',
            breadcrumb: {
                label: '按学员排课',
                parent: 'ppts'
            },
            dependencies: ['app/schedule/studentassignment/stuasgmt-list/stuasgmt-list.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.stuasgmt-course',
            url: '/course/:id?prev=:page',
            templateUrl: 'app/schedule/studentassignment/stuasgmt-course/stuasgmt-course.html',
            controller: 'stuAsgmtCourseController',
            breadcrumb: {
                label: '周视图',
                //parent: 'ppts.schedule'
            },
            dependencies: ['app/schedule/studentassignment/stuasgmt-course/stuasgmt-course.controller'
                , 'app/schedule/studentassignment/stuasgmt-add/stuasgmt-add.controller'
                , 'app/schedule/studentassignment/stuasgmt-course/stuasgmt-course-copy.controller'
                , 'app/schedule/studentassignment/stuasgmt-course/stuasgmt-course-reset.controller'
            ]
        }).loadRoute($stateProvider, {
            name: 'ppts.stuasgmt-course-list',
            url: '/courselist/:id?prev=:page',
            templateUrl: 'app/schedule/studentassignment/stuasgmt-course/stuasgmt-course-list.html',
            controller: 'stuAsgmtCourseListController',
            breadcrumb: {
                label: '列表视图',
                //parent: 'ppts.schedule'
            },
            dependencies: ['app/schedule/studentassignment/stuasgmt-course/stuasgmt-course-list.controller'
                , 'app/schedule/studentassignment/stuasgmt-add/stuasgmt-add.controller'
                , 'app/schedule/studentassignment/stuasgmt-course/stuasgmt-course-copy.controller'
                , 'app/schedule/studentassignment/stuasgmt-course/stuasgmt-course-reset.controller'
            ]
        }).loadRoute($stateProvider, {
            name: 'ppts.schedule-tchasgmt',
            url: '/schedule/tchasgmt/:cID',
            templateUrl: 'app/schedule/teacherassignment/tchasgmt-list/tchasgmt-list.html',
            controller: 'tchAsgmtListController',
            breadcrumb: {
                label: '按教师排课',
                parent: 'ppts'
            },
            dependencies: ['app/schedule/teacherassignment/tchasgmt-list/tchasgmt-list.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.tchasgmt-course',
            url: '/tchcourse/:cID/:tn/:tji',
            templateUrl: 'app/schedule/teacherassignment/tchasgmt-course/tchasgmt-course.html',
            controller: 'tchAsgmtCourseController',
            breadcrumb: {
                label: '周视图',
                parent: 'ppts.schedule-tchasgmt'
            },
            dependencies: ['app/schedule/teacherassignment/tchasgmt-course/tchasgmt-course.controller'
                , 'app/schedule/teacherassignment/tchasgmt-add/tchasgmt-add.controller'
                , 'app/schedule/teacherassignment/tchasgmt-course/tchasgmt-course-copy.controller'
                , 'app/schedule/teacherassignment/tchasgmt-course/tchasgmt-course-reset.controller'
            ]
        }).loadRoute($stateProvider, {
            name: 'ppts.tchasgmt-course-list',
            url: '/tchcourselist/:cID/:tn/:tji',
            templateUrl: 'app/schedule/teacherassignment/tchasgmt-course/tchasgmt-course-list.html',
            controller: 'tchAsgmtCourseListController',
            breadcrumb: {
                label: '列表视图',
                parent: 'ppts.schedule-tchasgmt'
            },
            dependencies: ['app/schedule/teacherassignment/tchasgmt-course/tchasgmt-course-list.controller'
                , 'app/schedule/teacherassignment/tchasgmt-add/tchasgmt-add.controller'
                , 'app/schedule/teacherassignment/tchasgmt-course/tchasgmt-course-copy.controller'
                , 'app/schedule/teacherassignment/tchasgmt-course/tchasgmt-course-reset.controller'
            ]
        }).loadRoute($stateProvider, {
            name: 'ppts.studentcourse',
            url: '/stucourse',
            templateUrl: 'app/schedule/studentcourse/stucourse-list/stucourse-list.html',
            controller: 'stuCourseListController',
            breadcrumb: {
                label: '客户课表',
                parent: 'ppts'
            },
            dependencies: ['app/schedule/studentcourse/stucourse-list/stucourse-list.controller'
                , 'app/schedule/studentassignment/stuasgmt-course/stuasgmt-course-reset.controller'
                , 'app/schedule/studentcourse/stucourse-list/stucourse-confirm.controller'
            ]
        }).loadRoute($stateProvider, {
            name: 'ppts.teachercourse',
            url: '/teachercourse',
            templateUrl: 'app/schedule/teachercourse/teacher-course/teacher-course.html',
            controller: 'tchCourseListController',
            breadcrumb: {
                label: '教师课表',
                parent: 'ppts'
            },
            dependencies: ['app/schedule/teachercourse/teacher-course/teacher-course.controller'
            ]
        }).loadRoute($stateProvider, {
            name: 'ppts.teachercourserecord',
            url: '/tchcrsrecord',
            templateUrl: 'app/schedule/teachercourse/tchrecord-list.html',
            controller: 'tchcrsListController',
            breadcrumb: {
                label: '教师上课记录',
                parent: 'ppts'
            },
            dependencies: ['app/schedule/teachercourse/tchrecord-list.controller'
                , 'app/schedule/teachercourse/tchrecord-list-markup.controller'
                , 'app/schedule/studentcourse/stucourse-list/stucourse-confirm.controller'
                , 'app/schedule/teachercourse/tchrecord-list-companion.controller'
            ]
        }).loadRoute($stateProvider, {
            name: 'ppts.accedit',
            url: '/accedit/:accid/:cid',
            templateUrl: 'app/schedule/studentassignment/stuasgmt-condition/stuasgmt-condition-edit.html',
            controller: 'asgmtConditionEditController',
            breadcrumb: {
                label: '排课条件',
                parent: 'ppts'
            },
            dependencies: ['app/schedule/studentassignment/stuasgmt-condition/stuasgmt-condition-edit.controller'
            ]
        }).loadRoute($stateProvider, {
            name: 'ppts.tchcoursepsn',
            url: '/tchpsncourse',
            templateUrl: 'app/schedule/teachercourse/teacher-course-psn/teacher-course-psn.html',
            controller: 'tchCoursePsnController',
            breadcrumb: {
                label: '教师个人课表',
                parent: 'ppts'
            },
            dependencies: ['app/schedule/teachercourse/teacher-course-psn/teacher-course-psn.controller'
            ]
        }).loadRoute($stateProvider, {
            name: 'ppts.tchcoursepsn-list',
            url: '/tchpsncourse/lst',
            templateUrl: 'app/schedule/teachercourse/teacher-course-psn/teacher-course-psn-list.html',
            controller: 'tchCoursePsnListController',
            breadcrumb: {
                label: '列表视图',
                parent: 'ppts.tchcoursepsn'
            },
            dependencies: ['app/schedule/teachercourse/teacher-course-psn/teacher-course-psn-list.controller'
            ]
        }).loadRoute($stateProvider, {
            name: 'ppts.classgroup',
            url: '/class/list:productCode',
            templateUrl: 'app/schedule/classgroup/class-list/class-list.html',
            controller: 'classListController',
            dependencies: ['app/schedule/classgroup/class-list/class-list.controller',
                            'app/schedule/classgroup/customer-list/customer-list.controller',
                            'app/schedule/classgroup/class-detail/class-detail.controller'
            ],
            breadcrumb: {
                label: '班级列表',
                parent: 'ppts'
            }
        }).loadRoute($stateProvider, {
            name: 'ppts.classgroup.customerlist',
            url: '/class/customer/list',
            templateUrl: 'app/schedule/classgroup/customer-list/customer-list.html',
            controller: 'customerListController',
            dependencies: ['app/schedule/classgroup/customer-list/customer-list.controller'],
            breadcrumb: {
                label: '查看学员',
                parent: 'ppts.classgroup'
            }
        }).loadRoute($stateProvider, {
            name: 'ppts.classgroup-detail',
            url: '/class/detail/:classID',
            templateUrl: 'app/schedule/classgroup/class-detail/class-detail.html',
            controller: 'classDetailController',
            dependencies: ['app/schedule/classgroup/class-detail/class-detail.controller',
                'app/schedule/classgroup/lesson-edit/lesson-edit.controller',
                'app/schedule/classgroup/teacher-edit/teacher-edit.controller',
                'app/schedule/classgroup/teacher-add/teacher-add.controller',
                'app/schedule/classgroup/customer-add/customer-add.controller',
                'app/schedule/classgroup/customer-list/customer-list.controller'
            ],
            breadcrumb: {
                label: '班级详情',
                parent: 'ppts.classgroup'
            }
        }).loadRoute($stateProvider, {
            name: 'ppts.classgroup-lessonEdit',
            url: '/class/lesson/edit',
            templateUrl: 'app/schedule/classgroup/lesson-edit/lesson-edit.html',
            controller: 'lessonEditController',
            dependencies: ['app/schedule/classgroup/lesson-edit/lesson-edit.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.classgroup-teacherEdit',
            url: '/class/teacher/edit',
            templateUrl: 'app/schedule/classgroup/teacher-edit/teacher-edit.html',
            controller: 'teacherEditController',
            dependencies: ['app/schedule/classgroup/teacher-edit/teacher-edit.controller']
        });
    });

    return schedule;
});