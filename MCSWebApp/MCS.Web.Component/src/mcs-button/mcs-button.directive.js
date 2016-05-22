(function () {
    'use strict';

    mcs.ng.constant('buttonConfig', {
        categories: ['add', 'edit', 'delete', 'search', 'view', 'save', 'cancel', 'ok', 'close', 'print', 'export', 'refresh', 'submit', 'approve', 'reject'],
        defaultTexts: ['新 增', '编 辑', '删 除', '查 询', '查 看', '保 存', '取 消', '确 定', '关 闭', '打 印', '导 出', '刷 新', '提交审批', '同 意', '驳 回'],
        sizes: ['mini', 'medium', 'large'],
        sizeCss: ['btn-xs', 'btn-sm', 'btn-lg'],
        iconClass: ['fa-plus', 'fa-pencil', 'fa-trash-o', 'fa-search', 'fa-eye', 'fa-save', 'fa-undo', 'fa-check', 'fa-times', 'fa-print', 'fa-share', 'fa-refresh', 'fa-arrow-right', 'fa-check-square', 'fa-undo']
    })
    .directive('mcsDropdownButton', function () {
        return {
            restrict: 'E',
            scope: {
                category: '@', //"info", "primary", "warning", "success", "danger"
                css: '@',
                title: '@',
                items: '=',
                icon: '@'
            },
            templateUrl: mcs.app.config.mcsComponentBaseUrl + '/src/tpl/mcs-dropdown-button.tpl.html',
            replace: true,
            link: function ($scope, $elem, $attrs, $ctrl) {
                $scope.category = $scope.category || 'info';
                $scope.title = $scope.title || '新 增';
                if ($scope.icon) {
                    if ($scope.icon.indexOf('fa-') == -1) {
                        $scope.icon = 'fa-' + $scope.icon;
                    }
                } else {
                    $scope.icon = 'fa-plus';
                }
                $elem.find('i').addClass($scope.icon);
            }
        };
    }).directive('mcsButton', function (buttonConfig) {

        return {
            restrict: 'E',
            scope: {
                category: '@',
                type: '@',
                text: '@',
                icon: '@',
                size: '@',
                css: '@',
                title: '&',
                click: '&'
            },

            template: '<button class="btn mcs-width-130" ng-click="click()"><i class="ace-icon fa bigger-110"></i><span></span></button>',
            replace: true,
            link: function ($scope, $elem, $attrs, $ctrl) {
                $elem.attr('type', $scope.type || 'button');
                var index = buttonConfig.categories.indexOf($scope.category);
                var buttonCss = 'btn-info';
                switch ($scope.category) {
                    case 'search':
                    case 'view':
                        buttonCss = 'btn-primary';
                        break;
                    case 'delete':
                        buttonCss = 'btn-danger';
                        break;
                    case 'save':
                    case 'ok':
                    case 'approve':
                        buttonCss = 'btn-success';
                        break;
                    case 'print':
                    case 'export':
                    case 'refresh':
                    case 'reject':
                        buttonCss = 'btn-yellow';
                        break;
                    case 'submit':
                        buttonCss = 'btn-purple';
                        break;
                    case 'cancel':
                    case 'close':
                        buttonCss = 'btn-light';
                        break;
                }
                $elem.addClass(buttonCss);
                if ($scope.size) {
                    var sizeIndex = buttonConfig.sizes.indexOf($scope.size);
                    $elem.addClass(buttonConfig.sizeCss[sizeIndex]);
                }
                if ($scope.icon && index === -1) {
                    if ($scope.icon.indexOf('fa-') == 0) {
                        $elem.find('i').addClass($scope.icon);
                    } else {
                        $elem.find('i').addClass('fa-' + $scope.icon);
                    }
                } else {
                    $elem.find('i').addClass(buttonConfig.iconClass[index]);
                }
                if ($scope.css) {
                    $elem.addClass($scope.css);
                }
                $elem.find('span').text($scope.text || buttonConfig.defaultTexts[index]);
            }
        };
    });
})();