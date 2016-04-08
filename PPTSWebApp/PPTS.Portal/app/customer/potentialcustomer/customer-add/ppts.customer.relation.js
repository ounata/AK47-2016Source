define([ppts.config.modules.customer], function (customer) {
    customer.registerService('customerRelationService', function () {
        var service = this;
        var spr = [
            { sid: "1", sr: "儿子", ss: "1", r: [{ pid: "1", pr: "父亲", ps: "1" }, { pid: "2", pr: "母亲", ps: "2" }] },
            { sid: "2", sr: "女儿", ss: "2", r: [{ pid: "1", pr: "父亲", ps: "1" }, { pid: "2", pr: "母亲", ps: "2" }] },
            { sid: "3", sr: "孙子", ss: "1", r: [{ pid: "3", pr: "祖父", ps: "1" }, { pid: "4", pr: "祖母", ps: "2" }] },
            { sid: "4", sr: "孙女", ss: "2", r: [{ pid: "3", pr: "祖父", ps: "1" }, { pid: "4", pr: "祖母", ps: "2" }] },
            { sid: "5", sr: "外孙子", ss: "1", r: [{ pid: "5", pr: "外袓父", ps: "1" }, { pid: "6", pr: "外袓母", ps: "2" }] },
            { sid: "6", sr: "外孙女", ss: "2", r: [{ pid: "5", pr: "外袓父", ps: "1" }, { pid: "6", pr: "外袓母", ps: "2" }] },
            { sid: "7", sr: "曾孙", ss: "1", r: [{ pid: "7", pr: "曾祖父", ps: "1" }, { pid: "8", pr: "曾祖母", ps: "2" }] },
            { sid: "8", sr: "曾孙女", ss: "2", r: [{ pid: "7", pr: "曾祖父", ps: "1" }, { pid: "8", pr: "曾祖母", ps: "2" }] },
            { sid: "9", sr: "侄女", ss: "2", r: [{ pid: "9", pr: "姑姑", ps: "2" }, { pid: "10", pr: "姑父", ps: "1" }, { pid: "11", pr: "叔叔", ps: "1" }, { pid: "12", pr: "婶婶", ps: "2" }, { pid: "25", pr: "伯父", ps: "1" }, { pid: "26", pr: "伯母", ps: "2" }] },
            { sid: "10", sr: "侄子", ss: "1", r: [{ pid: "9", pr: "姑姑", ps: "2" }, { pid: "10", pr: "姑父", ps: "1" }, { pid: "11", pr: "叔叔", ps: "1" }, { pid: "12", pr: "婶婶", ps: "2" }, { pid: "25", pr: "伯父", ps: "1" }, { pid: "26", pr: "伯母", ps: "2" }] },
            { sid: "11", sr: "外甥", ss: "1", r: [{ pid: "13", pr: "舅舅", ps: "1" }, { pid: "14", pr: "舅母", ps: "2" }, { pid: "15", pr: "姨", ps: "2" }, { pid: "16", pr: "姨夫", ps: "1" }] },
            { sid: "12", sr: "外甥女", ss: "2", r: [{ pid: "13", pr: "舅舅", ps: "1" }, { pid: "14", pr: "舅母", ps: "2" }, { pid: "15", pr: "姨", ps: "2" }, { pid: "16", pr: "姨夫", ps: "1" }] },
            { sid: "13", sr: "养子", ss: "1", r: [{ pid: "17", pr: "养父", ps: "1" }, { pid: "18", pr: "养母", ps: "2" }] },
            { sid: "14", sr: "养女", ss: "2", r: [{ pid: "17", pr: "养父", ps: "1" }, { pid: "18", pr: "养母", ps: "2" }] },
            { sid: "15", sr: "继子", ss: "1", r: [{ pid: "19", pr: "继父", ps: "1" }, { pid: "20", pr: "继母", ps: "2" }] },
            { sid: "16", sr: "继女", ss: "2", r: [{ pid: "19", pr: "继父", ps: "1" }, { pid: "20", pr: "继母", ps: "2" }] },
            { sid: "17", sr: "弟弟", ss: "1", r: [{ pid: "21", pr: "哥哥", ps: "1" }, { pid: "22", pr: "嫂子", ps: "2" }, { pid: "23", pr: "姐姐", ps: "2" }, { pid: "24", pr: "姐夫", ps: "1" }] },
            { sid: "18", sr: "妹妹", ss: "2", r: [{ pid: "21", pr: "哥哥", ps: "1" }, { pid: "22", pr: "嫂子", ps: "2" }, { pid: "23", pr: "姐姐", ps: "2" }, { pid: "24", pr: "姐夫", ps: "1" }] }
        ];
        var psr = [
            { pid: "1", pr: "父亲", ps: "1", r: [{ sid: "1", sr: "儿子", ss: "1" }, { sid: "2", sr: "女儿", ss: "2" }] },
            { pid: "2", pr: "母亲", ps: "2", r: [{ sid: "1", sr: "儿子", ss: "1" }, { sid: "2", sr: "女儿", ss: "2" }] },
            { pid: "3", pr: "祖父", ps: "1", r: [{ sid: "3", sr: "孙子", ss: "1" }, { sid: "4", sr: "孙女", ss: "2" }] },
            { pid: "4", pr: "祖母", ps: "2", r: [{ sid: "3", sr: "孙子", ss: "1" }, { sid: "4", sr: "孙女", ss: "2" }] },
            { pid: "5", pr: "外袓父", ps: "1", r: [{ sid: "5", sr: "外孙子", ss: "1" }, { sid: "6", sr: "外孙女", ss: "2" }] },
            { pid: "6", pr: "外袓母", ps: "2", r: [{ sid: "5", sr: "外孙子", ss: "1" }, { sid: "6", sr: "外孙女", ss: "2" }] },
            { pid: "7", pr: "曾祖父", ps: "1", r: [{ sid: "7", sr: "曾孙", ss: "1" }, { sid: "8", sr: "曾孙女", ss: "2" }] },
            { pid: "8", pr: "曾祖母", ps: "2", r: [{ sid: "7", sr: "曾孙", ss: "1" }, { sid: "8", sr: "曾孙女", ss: "2" }] },
            { pid: "9", pr: "姑姑", ps: "2", r: [{ sid: "9", sr: "侄女", ss: "2" }, { sid: "10", sr: "侄子", ss: "1" }] },
            { pid: "10", pr: "姑父", ps: "1", r: [{ sid: "9", sr: "侄女", ss: "2" }, { sid: "10", sr: "侄子", ss: "1" }] },
            { pid: "11", pr: "叔叔", ps: "1", r: [{ sid: "9", sr: "侄女", ss: "2" }, { sid: "10", sr: "侄子", ss: "1" }] },
            { pid: "12", pr: "婶婶", ps: "2", r: [{ sid: "9", sr: "侄女", ss: "2" }, { sid: "10", sr: "侄子", ss: "1" }] },
            { pid: "13", pr: "舅舅", ps: "1", r: [{ sid: "11", sr: "外甥", ss: "1" }, { sid: "12", sr: "外甥女", ss: "2" }] },
            { pid: "14", pr: "舅母", ps: "2", r: [{ sid: "11", sr: "外甥", ss: "1" }, { sid: "12", sr: "外甥女", ss: "2" }] },
            { pid: "15", pr: "姨", ps: "2", r: [{ sid: "11", sr: "外甥", ss: "1" }, { sid: "12", sr: "外甥女", ss: "2" }] },
            { pid: "16", pr: "姨夫", ps: "1", r: [{ sid: "11", sr: "外甥", ss: "1" }, { sid: "12", sr: "外甥女", ss: "2" }] },
            { pid: "17", pr: "养父", ps: "1", r: [{ sid: "13", sr: "养子", ss: "1" }, { sid: "14", sr: "养女", ss: "2" }] },
            { pid: "18", pr: "养母", ps: "2", r: [{ sid: "13", sr: "养子", ss: "1" }, { sid: "14", sr: "养女", ss: "2" }] },
            { pid: "19", pr: "继父", ps: "1", r: [{ sid: "15", sr: "继子", ss: "1" }, { sid: "16", sr: "继女", ss: "2" }] },
            { pid: "20", pr: "继母", ps: "2", r: [{ sid: "15", sr: "继子", ss: "1" }, { sid: "16", sr: "继女", ss: "2" }] },
            { pid: "21", pr: "哥哥", ps: "1", r: [{ sid: "17", sr: "弟弟", ss: "1" }, { sid: "18", sr: "妹妹", ss: "2" }] },
            { pid: "22", pr: "嫂子", ps: "2", r: [{ sid: "17", sr: "弟弟", ss: "1" }, { sid: "18", sr: "妹妹", ss: "2" }] },
            { pid: "23", pr: "姐姐", ps: "2", r: [{ sid: "17", sr: "弟弟", ss: "1" }, { sid: "18", sr: "妹妹", ss: "2" }] },
            { pid: "24", pr: "姐夫", ps: "1", r: [{ sid: "17", sr: "弟弟", ss: "1" }, { sid: "18", sr: "妹妹", ss: "2" }] },
            { pid: "25", pr: "伯父", ps: "1", r: [{ sid: "9", sr: "侄女", ss: "2" }, { sid: "10", sr: "侄子", ss: "1" }] },
            { pid: "26", pr: "伯母", ps: "2", r: [{ sid: "9", sr: "侄女", ss: "2" }, { sid: "10", sr: "侄子", ss: "1" }] }
        ];
        /*
            说明:
                性别：
                0：未知、所有
                1：男
                2：女
        */

        //通过性别获取家长列表
        service.parents = function(ps) {
            var ret = [];
            for (var i = 0; i < psr.length; i++) {
                if (psr[i].ps == ps) {
                    ret.push(psr[i]);
                }
            }
            return ret;
        }
        //通过家长ID及孩子性别获取孩子列表
        service.myChildren = function (pid, ss) {
            var ret = [];
            for (var i = 0; i < psr.length; i++) {
                if (psr[i].pid == pid) {
                    if (!ss)
                        return psr[i].r;
                    else {
                        for (var j = 0; j < psr[i].r.length; j++) {
                            if (ss == psr[i].r[j].ss) {
                                ret.push(psr[i].r[j]);
                            }
                        }
                    }
                }
            }
            return ret;
        }
        //通过孩子性别获取孩子列表
        service.children = function (ss) {
            var ret = [];
            for (var i = 0; i < spr.length; i++) {
                if (spr[i].ss == ss) {
                    ret.push(spr[i]);
                }
            }
            return ret;
        }
        //通过孩子ID及家长性别获取家长列表
        service.myParents = function (sid, ps) {
            var ret = [];
            for (var i = 0; i < spr.length; i++) {
                if (spr[i].sid == sid) {
                    if (!ps)
                        return spr[i].r;
                    else {
                        for (var j = 0; j < spr[i].r.length; j++) {
                            if (ps == spr[i].r[j].ps) {
                                ret.push(spr[i].r[j]);
                            }
                        }
                    }
                }
            }
            return ret;
        }

        return service;
    });
});