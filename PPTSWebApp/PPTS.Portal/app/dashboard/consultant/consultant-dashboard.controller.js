define(['angular', ppts.config.modules.dashboard], function (ng, dashboard) {
    dashboard.registerController('consulantDashboardController', function () {
        var vm = this;
        vm.active = true;
        /*
        * oScroll, 需要滚动的区域
        * options, 可配选项
        */
        var autoScroll = function(oScroll, options) {
            options = ng.extend({
                height: 20,
                speed: 20,
                delay: 1000
            }, options);

            var t;
            var p = false;
            var o = document.getElementById(oScroll);
            var preTop = 0;
            o.scrollTop = 0;
            function start() {
                t = setInterval(scrolling, options.speed);
                o.scrollTop += 1;
            }
            function scrolling() {
                if (o.scrollTop % options.height != 0
                        && o.scrollTop % (o.scrollHeight - o.height - 1) != 0) {
                    preTop = o.scrollTop;
                    o.scrollTop += 1;
                    if (preTop >= o.scrollHeight || preTop == o.scrollTop) {
                        o.scrollTop = 0;
                    }
                } else {
                    clearInterval(t);
                    setTimeout(start, options.delay);
                }
            }
            setTimeout(start, options.delay);
        }

        vm.ranks = [{
            active: true,
            text: '本校区咨询师排名'
        }, {
            active: false,
            text: '本分公司咨询师前20名'
        }];

        vm.switchRank = function (scope) {
            angular.forEach(vm.ranks, function (rank) {
                rank.active = false;
            });
            scope.rank.active = true;
        };

        // 页面初始化
        (function () {
            autoScroll('new-dispatches');
        })();
    });
});