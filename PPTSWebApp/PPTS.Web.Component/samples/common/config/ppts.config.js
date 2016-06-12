//全局配置文件(基于具体项目,如:PPTS)
var ppts = ppts || mcs.app;

(function () {
    ppts.name = 'ppts';
    ppts.version = '1.0';
    ppts.user = ppts.user || {};

    ppts.config = {
        pageSizeItem: 20,
        datePickerFormat: 'yyyy-mm-dd',
        datetimePickerFormat: 'yyyy-mm-dd hh:ii:ss',
        datePickerLang: 'zh-CN',
        modules: {
            dashboard: 'samples/dashboard/ppts.dashboard',
            component: 'samples/dashboard/ppts.component',
        }
    }
    // 读取配置文件
    var initConfigSection = function (section) {
        if (section) {
            for (var item in section) {
                ppts.config[item] = section[item];
            }
        }
    }

    var initConfig = function () {
        window.onload = function () {
            var configData = sessionStorage.getItem('configData');
            if (!configData) {
                var parameters = document.getElementById('configData');
                sessionStorage.setItem('configData', parameters.value);
                configData = sessionStorage.getItem('configData');
                parameters.value = '';
            }

            var config = JSON.parse(configData);

            initConfigSection(config.pptsWebAPIs);

            mcs.app.config.mcsComponentBaseUrl = ppts.config.mcsComponentBaseUrl;
            mcs.app.config.pptsComponentBaseUrl = ppts.config.pptsComponentBaseUrl;

            loadAssets();
        };
    };

    var loadAssets = function () {
        mcs.g.loadCss({
            cssFiles: [
                //<!--#bootstrap基础样式-->
                'libs/bootstrap-3.3.5/css/bootstrap',
                //<!--#datetime组件样式-->
                'libs/date-time-3.0.0/css/datepicker',
                'libs/date-time-3.0.0/css/bootstrap-timepicker',
                'libs/date-time-3.0.0/css/daterangepicker',
                'libs/date-time-3.0.0/css/bootstrap-datetimepicker',
                //<!--#ztree组件样式-->
                'libs/zTree-3.5.22/css/metroStyle/metroStyle',
                //<!--#autocomplete组件样式-->
                'libs/ng-tags-input-3.0.0/ng-tags-input',
                //<!--#font awesome字体样式-->
                'libs/font-awesome-4.5.0/css/font-awesome.min',
                //<!--#ace admin基础样式-->
                'libs/ace-1.3.1/css/ace.min',
                'libs/ace-1.3.1/css/ace-fonts',
                //<!--#blockUI样式-->
                'libs/angular-block-ui-0.2.2/dist/angular-block-ui',
                 //<!--#日程插件的样式-->
                'libs/fullcalendar-2.6.1/fullcalendar',
                //<!--#全局组件样式-->
                'libs/mcs-jslib-1.0.0/component/mcs.component',
                //<!--下拉框样式-->
                'libs/angular-ui-select-0.13.2/dist/select2',
                'libs/angular-dialog-service-5.3.0/dist/dialogs.min'
            ], localCssFiles: [
                //<!--#网站主样式-->
                'assets/styles/site'
            ]
        });

        mcs.g.loadRequireJs(
            'libs/requirejs-2.1.22/require',
            mcs.app.config.pptsComponentBaseUrl + 'samples/common/config/require.config');
    };

    initConfig();

    return ppts;

})();
