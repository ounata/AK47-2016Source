// 提供跨模块的service
define(['angular', ppts.config.modules.customer,
        ppts.config.dataServiceConfig.customerDataService,
        ppts.config.dataServiceConfig.studentDataService,
        ppts.config.dataServiceConfig.followDataService], function (ng, customer) {
            customer.registerFactory('customerService', ['$rootScope', '$state', '$stateParams', 'customerDataService', 'studentDataService', 'followDataService',
                function ($rootScope, $state, $stateParams, customerDataService, studentDataService, followDataService) {
                    var service = this;

                    service.handle = function (operation, data, callback) {
                        var result = { prev: $stateParams.prev };

                        switch (result.prev) {
                            case 'ppts.customer':
                            case 'ppts.market':
                            case 'ppts.follow':
                            case 'ppts.customerverify':
                                switch (operation) {
                                    // 初始化潜客信息
                                    case 'init':
                                        // 如果没有传递data参数，则callback将覆盖data传递
                                        customerDataService.getCustomerInfo($stateParams.id, data || callback);
                                        break;
                                    case 'edit':
                                        result.route = 'ppts.customer-view.profile-edit';
                                        $state.go(result.route, { prev: result.prev });
                                        break;
                                    case 'save':
                                        result.route = 'ppts.customer-view.profiles';
                                        customerDataService.updateCustomer(data, function () {
                                            $state.go(result.route, { prev: result.prev });
                                        });
                                        break;
                                    case 'cancel':
                                        result.route = 'ppts.customer-view.profiles';
                                        $state.go(result.route, { prev: result.prev });
                                        break;
                                    case 'confirm-parent':
                                        result.route = 'ppts.customer-view.parents';
                                        customerDataService.addParent(data, function () {
                                            if (ng.isFunction(callback)) {
                                                callback();
                                            }
                                            $state.go(result.route, { prev: result.prev });
                                        });
                                        break;
                                    case 'get-parents':
                                        customerDataService.getCustomerParents($stateParams.id, data || callback);
                                        break;
                                    case 'get-parent':
                                        result.route = 'ppts.customer-view.parents-edit';
                                        $state.go(result.route, { parentId: data.parentID, prev: result.prev });
                                        break;
                                    case 'init-parent':
                                        customerDataService.getCustomerParent($stateParams.parentId, $stateParams.id, data || callback);
                                        break;
                                    case 'update-parent':
                                        result.route = 'ppts.customer-view.parents';
                                        customerDataService.updateParent(data, function () {
                                            $state.go(result.route, { prev: result.prev });
                                        });
                                        break;
                                    case 'update-parent-cancel':
                                        result.route = 'ppts.customer-view.parents';
                                        $state.go(result.route, { prev: result.prev });
                                        break;
                                    case 'trans-new-parent':
                                        result.route = 'ppts.customer-view.parent-new';
                                        if (ng.isFunction(data || callback)) {
                                            data();
                                        }
                                        $state.go(result.route, { id: $stateParams.id, prev: $stateParams.prev });
                                        break;
                                    case 'init-new-parent':
                                        customerDataService.initParentForCreate($stateParams.id, data || callback);
                                        break;
                                    case 'new-parent':
                                        result.route = 'ppts.customer-view.parents';
                                        customerDataService.createParent(data, function () {
                                            $state.go(result.route, { prev: result.prev });
                                        });
                                        break;
                                    case 'addFollow':
                                        followDataService.getFollowForCreate($stateParams.customerId, true, data || callback);
                                        break;
                                    case 'addFollow-cancel':
                                        $state.go(result.prev);
                                        break;
                                }
                                break;
                            case 'ppts.student':
                            case 'ppts.customermeeting':
                            case 'ppts.score':
                                switch (operation) {
                                    // 初始化学员信息
                                    case 'init':
                                        studentDataService.getStudentInfo($stateParams.id, data || callback);
                                        break;
                                    case 'edit':
                                        result.route = 'ppts.student-view.profile-edit';
                                        $state.go(result.route, { prev: result.prev });
                                        break;
                                    case 'save':
                                        result.route = 'ppts.student-view.profiles';
                                        studentDataService.updateStudent(data, function () {
                                            $state.go(result.route, { prev: result.prev });
                                        });
                                        break;
                                    case 'cancel':
                                        result.route = 'ppts.student-view.profiles';
                                        $state.go(result.route, { prev: result.prev });
                                        break;
                                    case 'confirm-parent':
                                        result.route = 'ppts.student-view.parents';
                                        studentDataService.addParent(data, function () {
                                            if (ng.isFunction(callback)) {
                                                callback();
                                            }
                                            $state.go(result.route, { prev: prev });
                                        });
                                        break;
                                    case 'get-parents':
                                        studentDataService.getStudentParents($stateParams.id, data || callback);
                                        break;
                                    case 'get-parent':
                                        result.route = 'ppts.student-view.parents-edit';
                                        $state.go(result.route, { parentId: data.parentID, prev: result.prev });
                                        break;
                                    case 'init-parent':
                                        studentDataService.getStudentParent($stateParams.parentId, $stateParams.id, data || callback);
                                        break;
                                    case 'update-parent':
                                        result.route = 'ppts.student-view.parents';
                                        studentDataService.updateParent(data, function () {
                                            $state.go(result.route, { prev: result.prev });
                                        });
                                        break;
                                    case 'update-parent-cancel':
                                        result.route = 'ppts.student-view.parents';
                                        $state.go(result.route, { prev: result.prev });
                                        break;
                                    case 'trans-new-parent':
                                        result.route = 'ppts.student-view.parent-new';
                                        if (ng.isFunction(data || callback)) {
                                            data();
                                        }
                                        $state.go(result.route, { id: $stateParams.id, prev: $stateParams.prev });
                                        break;
                                    case 'init-new-parent':
                                        studentDataService.initParentForCreate($stateParams.id, data || callback);
                                        break;
                                    case 'new-parent':
                                        result.route = 'ppts.student-view.parents';
                                        studentDataService.createParent(data, function () {
                                            $state.go(result.route, { prev: result.prev });
                                        });
                                        break;
                                    case 'addFollow':
                                        followDataService.getFollowForCreate($stateParams.customerId, false, data || callback);
                                        break;
                                    case 'addFollow-cancel':
                                        $state.go(result.prev);
                                        break;
                                }
                                break;
                        }
                    }

                    return service;
                }]);
        });