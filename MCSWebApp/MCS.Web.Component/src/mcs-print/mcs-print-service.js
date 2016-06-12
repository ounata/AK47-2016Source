(
    function() {
        mcs.ng.service('printService', function() {
            var printService = this;

            function isIE() {
                if (!!window.ActiveXObject || "ActiveXObject" in window)
                    return true;
                else
                    return false;
            }



            printService.checkTargetPageLoaded = function(newwin, nopreview, content) {
                printService.timeoutObj = setTimeout(function() {
                    var container = newwin.document.getElementById('printDiv');
                    if (container) {
                        container.innerHTML = content.innerHTML;

                        if (nopreview) {
                            newwin.printWindow(nopreview);
                        }
                    } else {
                        clearTimeout(printService.timeoutObj);
                        printService.checkTargetPageLoaded();
                    }
                }, 100);
            };



            printService.print = function(nopreview) {



                var content = document.getElementById("printArea").cloneNode(true);


                var newwin = window.open(mcs.app.config.mcsComponentBaseUrl + '/src/mcs-print/mcs-print.html', '', '');


                if (mcs.browser.s.msie) {
                    printService.checkTargetPageLoaded(newwin, nopreview, content);
                    return;
                }


                newwin[newwin.addEventListener ? 'addEventListener' : 'attachEvent'](
                    (newwin.attachEvent ? 'on' : '') + 'load',
                    function() {
                        var container = newwin.document.getElementById('printDiv');
                        container.innerHTML = content.innerHTML;

                        if (nopreview) {
                            newwin.printWindow(nopreview);
                        }


                    }, false);

            };

            return printService;
        });
    }
)();
