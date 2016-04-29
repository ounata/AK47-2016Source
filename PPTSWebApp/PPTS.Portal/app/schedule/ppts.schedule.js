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
                label: '学员排课列表查询',
                parent: 'ppts'
            },
            dependencies: ['app/schedule/studentassignment/stuasgmt-list/stuasgmt-list.controller']
        })/*
            .loadRoute($stateProvider, {
            name: 'ppts.schedule-add',
            url: '/schedule/add/:cID',
            templateUrl: 'app/schedule/studentassignment/stuasgmt-add/stuasgmt-add.html',
            controller: 'stuAsgmtAddController',
            breadcrumb: {s
                label: '新增排课',
                parent: 'ppts'
            },
            dependencies: ['app/schedule/studentassignment/stuasgmt-add/stuasgmt-add.controller']
        })*/.loadRoute($stateProvider, {
            name: 'ppts.stuasgmt-course',
            url: '/course/:cID',
            templateUrl: 'app/schedule/studentassignment/stuasgmt-course/stuasgmt-course.html',
            controller: 'stuAsgmtCourseController',
            breadcrumb: {
                label: '学生课表',
                parent: 'ppts'
            },
            dependencies: ['app/schedule/studentassignment/stuasgmt-course/stuasgmt-course.controller'
                , 'app/schedule/studentassignment/stuasgmt-add/stuasgmt-add.controller'
                , 'app/schedule/studentassignment/stuasgmt-course/stuasgmt-course-copy.controller'
            ]
        }).loadRoute($stateProvider, {
            name: 'ppts.classgroup',
            url: '/class/list',
            templateUrl: 'app/schedule/classgroup/class-list/class-list.html',
            controller: 'classListController',
            dependencies: ['app/schedule/classgroup/class-list/class-list.controller'],
            breadcrumb: {
                label: '新增班级',
                parent: 'ppts'
            }
        });
    });

    return schedule;
});