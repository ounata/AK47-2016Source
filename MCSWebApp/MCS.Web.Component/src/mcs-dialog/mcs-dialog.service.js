(function() {
    'use strict';

    mcs.ng.service('mcsDialogService', ['dialogs', function(dialogs) {
        var mcsDialogService = this;
        mcsDialogService.messageConfig = {
            wait: {
                title: '操作中',
                message: '操作进行中，请稍后！',
            },
            info: {
                title: '消息',
                message: '至少选择一条数据！'
            },
            error: {
                title: '错误',
                message: '操作发生错误，请重试或联系系统管理员！'
            },
            confirm: {
                title: '请确认',
                message: '确认进行此操作吗？'
            }

        };

        this.info = function(options) {
            options = jQuery.extend({
                title: mcsDialogService.messageConfig.info.title,
                message: mcsDialogService.messageConfig.info.message
            }, options);

            return dialogs.notify(options.title, options.message);
        };

        this.confirm = function(options) {
            options = jQuery.extend({
                title: mcsDialogService.messageConfig.confirm.title,
                message: mcsDialogService.messageConfig.confirm.message
            }, options);

            return dialogs.confirm(options.title, options.message);
        };

        this.error = function(options) {
            options = jQuery.extend({
                title: mcsDialogService.messageConfig.error.title,
                message: mcsDialogService.messageConfig.error.message
            }, options);

            return dialogs.error(options.title, options.message);
        };

        this.wait = function(options) {
            options = jQuery.extend({
                title: mcsDialogService.messageConfig.wait.title,
                message: mcsDialogService.messageConfig.wait.message
            }, options);

            return dialogs.wait(options.title, options.message);
        };

        this.create = function(url, options) {
            if (!url) return;
            options = jQuery.extend({
                controller: '',
                params: {},
                settings: {
                    backdrop: 'static',
                    size: 'md'
                },
                controllerAs: 'vm'
            }, options);

            return dialogs.create(url, options.controller, options.params, options.settings, options.controllerAs);
        };
    }]);
})();
