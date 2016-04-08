define([], function () {
    var states = {
        'ppts.customer': {
            url: '/customer',
            templateUrl: 'app/customer/potentialcustomer/customer-list/customer-list.html',
            controller: 'customerListController',
            dependencies: ['app/customer/potentialcustomer/customer-list/customer-list.controller']
        },
        'ppts.customerAdd': {
            url: '/customer/add',
            templateUrl: 'app/customer/potentialcustomer/customer-add/customer-add.html',
            controller: 'customerAddController',
            dependencies: ['app/customer/potentialcustomer/customer-add/customer-add.controller']
        },
        'ppts.customerView': {
            url: '/customer/view/:id/:page',
            templateUrl: 'app/customer/potentialcustomer/customer-view/customer-view.html',
            controller: 'customerViewController',
            dependencies: ['app/customer/potentialcustomer/customer-view/customer-view.controller']
        }
    };

    return states;
});