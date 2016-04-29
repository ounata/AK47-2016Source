(function () { 
    /** * 获取本周、本季度、本月、上月的开端日期、停止日期 */
    //当前日期 
    var now = new Date();
    //今天本周的第几天 
    var nowDayOfWeek = now.getDay();
    //当前日 
    var nowDay = now.getDate();
    //当前月
    var nowMonth = now.getMonth();
    //当前年
    var nowYear = now.getYear();
    nowYear += (nowYear < 2000) ? 1900 : 0;
    //上月日期
    var lastMonthDate = new Date();
    lastMonthDate.setDate(1);
    lastMonthDate.setMonth(lastMonthDate.getMonth() - 1);
    //上年
    var lastYear = lastMonthDate.getYear();
    var lastMonth = lastMonthDate.getMonth();
    //格局化日期：yyyy-MM-dd 
    mcs.date.format = function(date) {
        var myyear = date.getFullYear();
        var mymonth = date.getMonth() + 1;
        var myweekday = date.getDate();
        if (mymonth < 10) {
            mymonth = "0" + mymonth;
        } if (myweekday < 10) {
            myweekday = "0" + myweekday;
        }
        return (myyear + "-" + mymonth + "-" + myweekday);
    }
    //比较两个时间的大小
    mcs.date.compare = function (beginTime, endTime) {
        //将字符串转换为日期
        var begin = new Date(beginTime.replace(/-/g, "/"));
        var end = new Date(endTime.replace(/-/g, "/"));
        return begin <= end;
    };
    //获得某月的天数 
    mcs.date.getMonthDays = function(month) {
        var monthStartDate = new Date(nowYear, month, 1);
        var monthEndDate = new Date(nowYear, month + 1, 1);
        var days = (monthEndDate - monthStartDate) / (1000 * 60 * 60 * 24);
        return days;
    }
    //获得本季度的开端月份 
    mcs.date.getQuarterStartMonth = function() {
        var quarterStartMonth = 0;
        if (nowMonth < 3) {
            quarterStartMonth = 0;
        } if (2 < nowMonth && nowMonth < 6) {
            quarterStartMonth = 3;
        } if (5 < nowMonth && nowMonth < 9) {
            quarterStartMonth = 6;
        } if (nowMonth > 8) {
            quarterStartMonth = 9;
        }
        return quarterStartMonth;
    }
    // 获取今天
    mcs.date.today = function () {
        var todayDate = new Date(nowYear, nowMonth, nowDay);
        return todayDate;
    };
    //获得本周的开始日期 
    mcs.date.getWeekStartDate = function() {
        var weekStartDate = new Date(nowYear, nowMonth, nowDay - nowDayOfWeek);
        return mcs.date.format(weekStartDate);
    }
    //获得本周的停止日期 
    mcs.date.getWeekEndDate = function() {
        var weekEndDate = new Date(nowYear, nowMonth, nowDay + (6 - nowDayOfWeek));
        return mcs.date.format(weekEndDate);
    }
    //获得本月的开始日期 
    mcs.date.getMonthStartDate = function() {
        var monthStartDate = new Date(nowYear, nowMonth, 1);
        return mcs.date.format(monthStartDate);
    }
    //获得本月的停止日期 
    mcs.date.getMonthEndDate = function() {
        var monthEndDate = new Date(nowYear, nowMonth, getMonthDays(nowMonth));
        return mcs.date.format(monthEndDate);
    }
    //获得上月开始日期 
    mcs.date.getLastMonthStartDate = function() {
        var lastMonthStartDate = new Date(nowYear, lastMonth, 1);
        return mcs.date.format(lastMonthStartDate);
    }
    //获得上月停止日期 
    mcs.date.getLastMonthEndDate = function() {
        var lastMonthEndDate = new Date(nowYear, lastMonth, getMonthDays(lastMonth));
        return mcs.date.format(lastMonthEndDate);
    }
    //获得本季度的开始日期 
    mcs.date.getQuarterStartDate = function() {
        var quarterStartDate = new Date(nowYear, getQuarterStartMonth(), 1);
        return mcs.date.format(quarterStartDate);
    }
    //获得本季度的停止日期 
    mcs.date.getQuarterEndDate = function() {
        var quarterEndMonth = getQuarterStartMonth() + 2;
        var quarterStartDate = new Date(nowYear, quarterEndMonth, getMonthDays(quarterEndMonth));
        return mcs.date.format(quarterStartDate);
    }

    return mcs.date;
})();