(function() {
    angular.module('app.component')
        .controller('MCSDatetimePickerController', ['$scope', 'uibDateParser', function($scope, uibDateParser) {
            var vm = this;
            vm.format = 'yyyy/MM/dd hh:mm:ss'



            vm.dt = new Date();



            vm.dateOptions = {
                // dateDisabled: disabled,
                formatYear: 'yy',
                // maxDate: new Date(2020, 5, 22),
                // minDate: new Date(),
                startingDay: 1
            };



            vm.open1 = function() {
                vm.popup1.opened = true;
            };

            vm.popup1 = {
                opened: false
            };

            vm.clear = function() {
                vm.dt = null;
            };

            vm.inlineOptions = {
                customClass: getDayClass,
                minDate: new Date(),
                showWeeks: true
            };

            // Disable weekend selection
            function disabled(data) {
                var date = data.date,
                    mode = data.mode;
                return mode === 'day' && (date.getDay() === 0 || date.getDay() === 6);
            }

            vm.toggleMin = function() {
                vm.inlineOptions.minDate = vm.inlineOptions.minDate ? null : new Date();
                vm.dateOptions.minDate = vm.inlineOptions.minDate;
            };

            vm.toggleMin();



            vm.open2 = function() {
                vm.popup2.opened = true;
            };

            vm.setDate = function(year, month, day) {
                vm.dt = new Date(year, month, day);
            };


            vm.altInputFormats = ['M!/d!/yyyy'];



            vm.popup2 = {
                opened: false
            };

            var tomorrow = new Date();
            tomorrow.setDate(tomorrow.getDate() + 1);
            var afterTomorrow = new Date();
            afterTomorrow.setDate(tomorrow.getDate() + 1);
            vm.events = [{
                date: tomorrow,
                status: 'full'
            }, {
                date: afterTomorrow,
                status: 'partially'
            }];

            function getDayClass(data) {
                var date = data.date,
                    mode = data.mode;
                if (mode === 'day') {
                    var dayToCheck = new Date(date).setHours(0, 0, 0, 0);

                    for (var i = 0; i < vm.events.length; i++) {
                        var currentDay = new Date(vm.events[i].date).setHours(0, 0, 0, 0);

                        if (dayToCheck === currentDay) {
                            return vm.events[i].status;
                        }
                    }
                }

                return '';
            }



        }]);
})();
