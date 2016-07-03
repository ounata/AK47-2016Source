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
            text += '';
            return !mcs.util.bool(text) || text.indexOf('0001-01-01') > -1 || text.indexOf('9999-09-09') > -1 || (text.indexOf('￥') > -1 && parseFloat(text.replace('￥', '')) == 0) ? '' : text;
        };
    });

    mcs.ng.filter('truncate', function () {
        return function (text, length) {
            if (!text) return '';
            if (!length) {
                // 按照数组进行拦截
                text = text.replace('，', ',');
                var array = mcs.util.toArray(text);
                if (array.length > 1) {
                    return array[0] + '...';
                }
            }
            if (length > 0 && length < text.length) {
                return text.substr(0, length) + '...';
            }
            return text;
        }
    });

    mcs.ng.filter('tooltip', function () {
        return function (text, length) {
            if (!text) return '';
            length = length || 10;
            return text && mcs.util.isString(text) && text.length > length ? text : '';
        };
    });

    mcs.ng.filter('rmb', function () {
        return function (input) {
            if (!/^(0|[1-9]\d*)(\.\d+)?$/.test(input))
                return '';
            if (parseFloat(input) == 0) return '零元整';
            var unit = '仟佰拾亿仟佰拾万仟佰拾元角分', str = '';
            input += '00';
            var p = input.indexOf('.');
            if (p >= 0)
                input = input.substring(0, p) + input.substr(p + 1, 2);
            unit = unit.substr(unit.length - input.length);
            for (var i = 0; i < input.length; i++)
                str += '零壹贰叁肆伍陆柒捌玖'.charAt(input.charAt(i)) + unit.charAt(i);
            return str.replace(/零(仟|佰|拾|角)/g, '零').replace(/(零)+/g, '零').replace(/零(万|亿|元)/g, '$1').replace(/(亿)万|壹(拾)/g, '$1$2').replace(/^元零?|零分/g, "").replace(/元$/g, '元整');
        };
    });
})();
