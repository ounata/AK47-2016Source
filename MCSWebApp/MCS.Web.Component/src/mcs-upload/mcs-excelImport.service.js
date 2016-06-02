(
    function() {
        mcs.ng.service('excelImportService', function() {
            var excelImportService = this;

            excelImportService.import = function(param) {
                var win = window.open('../src/tpl/mcs-excelImport.tpl.html', '_blank', "height=600, width=700, toolbar =no, menubar=no, scrollbars=no, resizable=no, location=no, status=no");
                win.onload = function() {
                    win.setParam(param);
                };

            };



            return excelImportService;
        });
    }
)();
