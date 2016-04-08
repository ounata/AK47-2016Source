(function() {
        'use strict';

        mcs.ng.service('mcsDialogService', ['dialogs', function(dialogs) {
            var mcsDialogService = this;
            mcsDialogService.messageConfig = {
                wait: {
                    title: '操作中',
                    message: '操作进行中，请稍后！',
                },
                error: {
                    title: '错误',
                    message: '操作发生错误，请重试或联系系统管理员！',
                },
                confirm: {
                    title: '请确认',
                    message: '确认进行此操作吗？'
                }

            };
            this.wait = function(title, msg, opts) {
                dialogs.wait(title || mcsDialogService.messageConfig.wait.title, msg || mcsDialogService.messageConfig.wait.message);
            }

            this.error = function(title, msg, opts) {
                dialogs.error(title || mcsDialogService.messageConfig.error.title, msg || mcsDialogService.messageConfig.error.message);
            }


            this.confirm = function(title, msg, opts) {
                dialogs.confirm(title || mcsDialogService.messageConfig.confirm.title, msg || mcsDialogService.messageConfig.confirm.message);

            }


        }]);
    }

)();
