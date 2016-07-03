var ppts = ppts || mcs.app;
ppts.config = {
    datePickerFormat: 'yyyy-mm-dd',
    datetimePickerFormat: 'yyyy-mm-dd hh:ii:00',
    datePickerLang: 'zh-CN',
    dictMappingConfig: {
        'stage': 'c_codE_ABBR_CUSTOMER_STAGE',
        'grade': 'c_codE_ABBR_CUSTOMER_GRADE',
        'grade_async': 'c_codE_ABBR_CUSTOMER_GRADE_Async'
    },
    mcsComponentBaseUrl: 'http://' + location.host + '/MCSWebApp/MCS.Web.Component/',
    mcsApiBaseUrl: 'http://' + location.host + '/MCSWebApp/MCS.Web.API/',
};