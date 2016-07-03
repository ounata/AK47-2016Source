(function() {
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
            }
            if (myweekday < 10) {
                myweekday = "0" + myweekday;
            }
            return (myyear + "-" + mymonth + "-" + myweekday);
        }
        //比较两个时间的大小
    mcs.date.compare = function(beginTime, endTime) {
        //将字符串转换为日期
        var begin = new Date(beginTime.replace(/-/g, "/"));
        var end = new Date(endTime.replace(/-/g, "/"));
        return begin < end ? 1 : (begin == end ? 0 : -1);
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
            }
            if (2 < nowMonth && nowMonth < 6) {
                quarterStartMonth = 3;
            }
            if (5 < nowMonth && nowMonth < 9) {
                quarterStartMonth = 6;
            }
            if (nowMonth > 8) {
                quarterStartMonth = 9;
            }
            return quarterStartMonth;
        }
        // 获取今天
    mcs.date.today = function() {
        var todayDate = new Date(nowYear, nowMonth, nowDay, now.getHours(), now.getMinutes(), now.getSeconds());
        return todayDate;
    };
    //获取今天之前的某一天
    mcs.date.lastDay = function(offsetDay) {
        var lastStartDate = new Date(nowYear, nowMonth, nowDay + offsetDay + 1);
        return lastStartDate;
    };
    //得到左边界时间点
    mcs.date.getLeftBoundDatetime = function(date, offset) {
        if (isNaN(offset)) {
            return null;
        }
        var selectionTime = new Date(date || Date.now());
        var ticks = selectionTime.getTime();
        var times = selectionTime.getHours() * 60 * 60 * 1000 + selectionTime.getMinutes() * 60 * 1000 + selectionTime.getSeconds() * 1000;

        if (offset < 0) {
            return new Date(ticks + (offset + 1) * 24 * 60 * 60 * 1000 - times + 1000);
        } else {
            return new Date(ticks - times);
        }
    };
    //得到右边界时间点
    mcs.date.getRightBoundDatetime = function(date, offset) {
        if (isNaN(offset)) {
            return null;
        }
        var selectionTime = new Date(date || Date.now());
        var ticks = selectionTime.getTime();
        var times = selectionTime.getHours() * 60 * 60 * 1000 + selectionTime.getMinutes() * 60 * 1000 + selectionTime.getSeconds() * 1000;

        if (offset < 0) {
            return new Date(ticks - times + 1 * 24 * 60 * 60 * 1000 - 1000);

        } else {

            return new Date(ticks + (offset + 1) * 24 * 60 * 60 * 1000 - times - 1000);
        }
    };



    // 获取指定日期的前后几天
    // mcs.date.siblingsDay = function(date, offsetDay) {
    //     var currentDay = new Date(date);
    //     if (isNaN(parseInt(offsetDay))) return currentDay;
    //     var currentYear = currentDay.getYear();
    //     currentYear += (currentYear < 2000) ? 1900 : 0;
    //     var siblingsDate = new Date(currentYear, currentDay.getMonth(), currentDay.getDate() + offsetDay, 23, 59, 59);
    //     return siblingsDate;
    // };
    //获得本周的开始日期 
    mcs.date.getWeekStartDate = function() {
        var weekStartDate = new Date(nowYear, nowMonth, nowDay - nowDayOfWeek);
        return weekStartDate;
    };
    //获得本周的停止日期 
    mcs.date.getWeekEndDate = function() {
        var weekEndDate = new Date(nowYear, nowMonth, nowDay + (6 - nowDayOfWeek));
        return weekEndDate;
    };
    //获得本月的开始日期 
    mcs.date.getMonthStartDate = function() {
        var monthStartDate = new Date(nowYear, nowMonth, 1);
        return monthStartDate;
    };
    //获得本月的停止日期 
    mcs.date.getMonthEndDate = function() {
        var monthEndDate = new Date(nowYear, nowMonth, mcs.date.getMonthDays(nowMonth));
        return monthEndDate;
    };
    //获得上月开始日期 
    mcs.date.getLastMonthStartDate = function() {
        var lastMonthStartDate = new Date(nowYear, lastMonth, 1);
        return lastMonthStartDate;
    };
    //获得上月停止日期 
    mcs.date.getLastMonthEndDate = function() {
        var lastMonthEndDate = new Date(nowYear, lastMonth, mcs.date.getMonthDays(lastMonth));
        return lastMonthEndDate;
    };
    //获得本季度的开始日期 
    mcs.date.getQuarterStartDate = function() {
        var quarterStartDate = new Date(nowYear, getQuarterStartMonth(), 1);
        return quarterStartDate;
    };
    //获得本季度的停止日期 
    mcs.date.getQuarterEndDate = function() {
        var quarterEndMonth = getQuarterStartMonth() + 2;
        var quarterStartDate = new Date(nowYear, quarterEndMonth, mcs.date.getMonthDays(quarterEndMonth));
        return quarterStartDate;
    };
    //获取时间差
    mcs.date.datepart = function(startDate, endDate, part) {
        //if (!mcs.util.isDate(startDate) || !mcs.util.isDate(endDate)) return;
        var start = new Date(startDate.replace(/-/g, "/"));
        var end = new Date(endDate.replace(/-/g, "/"));
        var diff = end.getTime() - start.getTime(); //时间差的毫秒数
        var section = {};

        section.year = diff / (12 * 30 * 24 * 3600 * 1000);
        section.month = diff % (12 * 30 * 24 * 3600 * 1000);
        section.day = section.month % (30 * 24 * 3600 * 1000);
        section.hour = section.day % (24 * 3600 * 1000); //计算天数后剩余的毫秒数
        section.minute = section.hour % (3600 * 1000); //计算小时数后剩余的毫秒数
        section.second = section.minute % (60 * 1000); //计算分钟数后剩余的毫秒数

        switch (part) {
            case 'y':
            case 'year':
                return Math.floor(section.year);
            case 'M':
            case 'month':
                return Math.floor(section.month / (30 * 24 * 3600 * 1000));
            case 'd':
            case 'day':
                return Math.floor(section.day / (24 * 3600 * 1000));
            case 'h':
            case 'hour':
                return Math.floor(section.hour / (3600 * 1000)) + (Math.floor(section.day / (24 * 3600 * 1000))) * 24;
            case 'm':
            case 'minute':
                return Math.floor(section.minute / (60 * 1000)) + (Math.floor(section.hour / (3600 * 1000)) + (Math.floor(section.day / (24 * 3600 * 1000))) * 24) * 60;
            case 's':
            case 'second':
                return Math.round(section.second / 1000) + (Math.floor(section.minute / (60 * 1000)) + (Math.floor(section.hour / (3600 * 1000)) + (Math.floor(section.day / (24 * 3600 * 1000))) * 24) * 6) * 60;
            default:
                return;
        }
    };

    return mcs.date;
})();
