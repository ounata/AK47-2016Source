(function() {
    angular.module('app.component')
        .controller('MCSDropdownButtonController', ['$scope', function($scope) {
            var vm = this;
            vm.purchase = function (item) {
                alert(item.text + ',' + item.route);
            };

            vm.products = [{
                text: '常规订购', route: 'ppts.products({type:"cgdg"})', click: vm.purchase 
            }, {
                text: '插班订购', route: 'ppts.products({type:"cbdg"})', click: vm.purchase
            }, {
                text: '买赠订购', route: 'ppts.products({type:"mzdg"})', click: vm.purchase
            }];

            vm.products_2 = [{
                text: '常规订购', route: 'ppts.products({type:"cgdg"})', click: vm.purchase
            }, {
                text: '插班订购', route: 'ppts.products({type:"cbdg"})', click: vm.purchase
            }, {
                text: '买赠订购', children: [
                    { text: '买赠订购1', route: 'ppts.products({type:"mzdg1"})', click: vm.purchase },
                    { text: '买赠订购2', route: 'ppts.products({type:"mzdg2"})', click: vm.purchase },
                    { text: '买赠订购3', route: 'ppts.products({type:"mzdg3"})', click: vm.purchase }
                ]
            }];
        }]);
})();