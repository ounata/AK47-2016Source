(function () {
    'use strict';

    mcs.ng.filter('props', function () {
        return function (items, props) {
            var out = [];
            if (angular.isArray(items)) {
                var keys = Object.keys(props);
                items.forEach(function (item) {
                    var itemMatches = false;
                    for (var i = 0; i < keys.length; i++) {
                        var prop = keys[i];
                        var text = props[prop].toLowerCase();
                        if (item[prop].toString().toLowerCase().indexOf(text) !== -1) {
                            itemMatches = true;
                            break;
                        }
                    }
                    if (itemMatches) {
                        out.push(item);
                    }
                });
            } else {
                // Let the output be the input untouched
                out = items;
            }
            return out;
        };
    });

    mcs.ng.filter('trusted', ['$sce', function ($sce) {
        return function (text) {
            return $sce.trustAsHtml(text);
        };
    }]);

    mcs.ng.filter('normalize', function () {
        return function (text) {
            return !text || text == '0001-01-01' ? '' : text;
        };
    });

    mcs.ng.filter('truncate', function () {
        return function (text, length) {
            var array = mcs.util.toArray(text);
            if (array.length > 1) {
                return array[0] + '...';
            }
            if (length > 0 && length < text.length) {
                return text.substr(0, length) + '...';
            }
            return text;
        }
    });
})();