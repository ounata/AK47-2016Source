(function () {
    angular.module('app.issue').controller('MCSDateSerializeController', [
        '$scope', '$filter', function ($scope, $filter) {
            var vm = this;

            vm.dt1 = new Date();

            //控制器中使用
            vm.dt2 = $filter("date")(vm.dt1, "yyyy-MM-dd HH:mm:ss");

            //dt3: /Date(1448864369815)/ 
            //前台输出dt3结果为 2015-11-30 14:19:29

            vm.dt3 = $filter("date")(1448864369815, "yyyy-MM-dd HH:mm:ss");


            vm.jsondt = "/Date(1448864369815)/";

            //控制器中使用
            vm.dt4 = $filter("jsonDate")(vm.jsondt, "yyyy-MM-dd HH:mm:ss");

            vm.dt5 = jsonDateFormat(vm.jsondt, "yyyy-MM-dd hh:mm:ss");
        }
    ]).filter("jsonDate", function ($filter) {
        return function (input, format) {

            //从字符串 /Date(1448864369815)/ 得到时间戳 1448864369815
            var timestamp = Number(input.replace(/\/Date\((\d+)\)\//, "$1"));

            //转成指定格式
            return $filter("date")(timestamp, format);
        };
    });
})();


function jsonDateFormat(jsonDt, format) {

    var date, timestamp, dtObj;

    timestamp = jsonDt.replace(/\/Date\((\d+)\)\//, "$1");

    date = new Date(Number(timestamp));

    dtObj = {
        "M+": date.getMonth() + 1,   //月
        "d+": date.getDate(),        //日
        "h+": date.getHours(),       //时
        "m+": date.getMinutes(),     //分
        "s+": date.getSeconds(),     //秒
    };

    //因为年份是4位数，所以单独拿出来处理
    if (/(y+)/.test(format)) {
        format = format.replace(RegExp.$1, (date.getFullYear() + "").substr(4 - RegExp.$1.length));
    }

    //遍历dtObj
    for (var k in dtObj) {

        //dtObj的属性名作为正则进行匹配
        if (new RegExp("(" + k + ")").test(format)) {

            //月，日，时，分，秒 小于10时前面补 0
            format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? dtObj[k] : ("00" + dtObj[k]).substr(("" + dtObj[k]).length));
        }
    }

    return format;
}