// 应用程序的启动文件, 整个系统唯一的全局变量, 此文件主要管理命名空间
var mcs = mcs || {};
(function () {
    'use strict';

    mcs.g = mcs.g || {};
    mcs.util = mcs.util || {};
    mcs.date = mcs.date || {};
    mcs.app = mcs.app || { name: "app", version: "1.0" };
    mcs.app.dict = mcs.app.dict || {};
    mcs.app.config = mcs.app.config || {};
    mcs.config = mcs.config || {};

    return mcs;
})();