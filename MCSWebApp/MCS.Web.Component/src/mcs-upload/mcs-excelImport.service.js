(
    function() {
        mcs.ng.service('excelImportService', function (mcsDialogService, storage) {
            var excelImportService = this;

            excelImportService.import = function(param, apiUrl) {
                var win = window.open('../src/tpl/mcs-excelImport.tpl.html', '_blank', "height=600, width=700, toolbar =no, menubar=no, scrollbars=no, resizable=no, location=no, status=no");
                win.onload = function () {
                    var currentJobId = storage.get('ppts.user.currentJobId_' + ppts.user.id);
                    win.setParam(angular.extend({ pptsCurrentJobID: currentJobId },param));
                    win.setApiUrl(apiUrl);
                };

            };

            excelImportService.importOnDialog = function(tpl, config) {
                mcsDialogService.create(tpl, config);
            };



            return excelImportService;
        });
    }
)();
