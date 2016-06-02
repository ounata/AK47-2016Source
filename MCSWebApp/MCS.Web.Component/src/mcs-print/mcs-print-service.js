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

            printService.print = function() {



                var content = document.getElementById("printArea").cloneNode(true);

                var newwin = window.open(mcs.app.config.mcsComponentBaseUrl + '/src/mcs-print/mcs-print.html', '', '');
                newwin.opener = null;



                newwin.onload = function() {
                    var container = newwin.document.getElementById('printContainer');
                    container.appendChild(content);
                }



            }

            return printService;
        });
    }
)();
