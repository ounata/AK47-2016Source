//注册过滤器
for (var prop in ppts.enum) {
    if (!ppts.config.dictMappingConfig[prop]) {
        ppts.config.dictMappingConfig[prop] = 'c_codE_ABBR_' + prop;
    }
}

for (var mappingConfig in ppts.config.dictMappingConfig) {
    (function() {
        var filterName = mappingConfig;
        ppts.ng.filter(filterName, function() {
            var config = ppts.config.dictMappingConfig[filterName];
            return function(current, separator) {
                if (filterName == 'ifElse') {
                    current = mcs.util.bool(current) ? '1' : '0';
                }
                current += '';
                if (!mcs.util.bool(current, true)) return '';
                separator = separator || ',';
                if ((current).indexOf(separator) == -1) {
                    return mcs.util.getDictionaryItemValue(ppts.dict[config], current);
                } else {
                    var array = mcs.util.toArray(current, separator);
                    if (!array.length) return '';
                    var result = mcs.util.getDictionaryItemValue(ppts.dict[config], array[0]);
                    return result ? result + '...' : '';
                }
            };
        });

        ppts.ng.filter(filterName + '_full', function() {
            var config = ppts.config.dictMappingConfig[filterName];
            return function(current, separator) {
                if (!mcs.util.bool(current)) return '';
                separator = separator || ',';
                var array = mcs.util.toArray(current, separator);
                if (!array.length) return '';
                var result = [];
                for (var item in array) {
                    if (!mcs.util.bool(array[item], true)) continue;
                    if (array[item] == -1) continue;
                    result.push(mcs.util.getDictionaryItemValue(ppts.dict[config], array[item]));
                }
                return result.join('，');
            };
        });
    })();
}
