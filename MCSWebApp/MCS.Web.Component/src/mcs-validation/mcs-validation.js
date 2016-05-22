/* 验证框架 */
(function () {
    'use strict';

    mcs.ng.service('mcsValidationService', ['mcsValidationRules', 'mcsValidationMessageConfig', function (rules, config) {
        var service = this;

        var checkValidationResult = function (elem, options) {
            var opts = $.extend({
                errorMessage: config.general,
                validate: true
            }, options);

            var parent = elem.closest('.form-group');
            // 如果验证通过，则跳转到下一规则继续验证
            var message = parent.find('.help-block');
            if (opts.validate) {
                parent.removeClass('validate has-error');
                message.text('').css('visibility', 'hidden');
            } else {
                parent.addClass('validate has-error');
                message.text(opts.errorMessage).css('visibility', 'visible');
            }

            return opts.validate;
        };

        service.validate = function (elem) {
            var validationResult = true;
            // 如不包含任何验证规则，则不参与验证
            if (!mcs.util.hasAttrs(elem, rules)) return validationResult;
            // required
            if (validationResult && mcs.util.hasAttr(elem, 'required')) {
                validationResult = checkValidationResult(elem, {
                    errorMessage: elem.attr('data-validation-required-message') || config.required,
                    validate: $.trim(elem.val()).length > 0
                });
            }
            // minlength
            if (validationResult && mcs.util.hasAttr(elem, 'minlength')) {
                var minlength = parseInt(elem.attr('minlength'));
                if (minlength > 0) {
                    validationResult = checkValidationResult(elem, {
                        errorMessage: elem.attr('data-validation-minlength-message') || mcs.util.format(config.minlength, minlength),
                        validate: $.trim(elem.val()).length >= minlength
                    });
                }
            }
            //maxlength
            if (validationResult && mcs.util.hasAttr(elem, 'maxlength')) {
                var maxlength = parseInt(elem.attr('maxlength'));
                if (maxlength > 0) {
                    validationResult = checkValidationResult({
                        errorMessage: elem.attr('data-validation-maxlength-message') || mcs.util.format(config.maxlength, maxlength),
                        validate: $.trim(elem.val()).length <= maxlength
                    });
                }
            }
            //min
            if (validationResult && mcs.util.hasAttr(elem, 'min')) {
                var min = parseFloat(elem.attr('min'));
                validationResult = checkValidationResult({
                    errorMessage: elem.attr('data-validation-min-message') || mcs.util.format(config.min, min),
                    validate: parseFloat($.trim(elem.val())) >= min
                });
            }
            //max
            if (validationResult && mcs.util.hasAttr(elem, 'max')) {
                var max = parseFloat(elem.attr('max'));
                validationResult = checkValidationResult({
                    errorMessage: elem.attr('data-validation-max-message') || mcs.util.format(config.max, max),
                    validate: parseFloat($.trim(elem.val())) <= max
                });
            }
            //positive
            if (validationResult && mcs.util.hasAttr(elem, 'positive')) {
                validationResult = checkValidationResult({
                    errorMessage: elem.attr('data-validation-positive-message') || config.positive,
                    validate: (/^[1-9]+[0-9]*$/).test($.trim(elem.val()))
                });
            }
            //negtive
            if (validationResult && mcs.util.hasAttr(elem, 'negtive')) {
                validationResult = checkValidationResult({
                    errorMessage: elem.attr('data-validation-negtive-message') || config.negative,
                    validate: (/^-[1-9]+[0-9]*$/).test($.elem(elem.val()))
                });
            }
            //currency
            if (validationResult && mcs.util.hasAttr(elem, 'currency')) {
                validationResult = checkValidationResult({
                    errorMessage: elem.attr('data-validation-currency-message') || config.currency,
                    validate: (/^(([1-9]\d*)|0)(\.\d{1,2})?$/).test($.trim(elem.val()))
                });
            }
            //between
            if (validationResult && mcs.util.hasAttr(elem, 'between')) {
                var between = elem.attr('between').split(',');
                if (between.length == 2) {
                    var min = parseFloat(between[0]);
                    var max = parseFloat(between[1]);
                    if (!isNaN(min) && !isNaN(max)) {
                        validationResult = checkValidationResult({
                            errorMessage: elem.attr('data-validation-between-message') || mcs.util.format(config.between, min, max),
                            validate: parseFloat($.trim(elem.val())) >= min && parseFloat($.trim(elem.val())) <= max
                        });
                    }
                }
            }
            // 设置已经过验证
            elem.attr('data-validate-result', validationResult);
            return validationResult;
        };

        return service;
    }]).constant('mcsValidationRules', [
        'required',
        'minlength',
        'maxlength',
        'min',
        'max',
        'positive',
        'negtive',
        'currency',
        'between'
    ]).constant('mcsValidationMessageConfig', {
        general: '输入数据项不正确，请重新输入!',
        required: '输入数据项为必填！',
        minlength: '应至少输入{0}个字符！',
        maxlength: '最多只能输入{0}个字符！',
        min: '输入数据项应大于{0}！',
        max: '输入数据项应小于{0}！',
        positive: '输入数据项应为正整数！',
        negative: '输入数据项应为负整数！',
        currency: '输入数据项应为小数或货币值！',
        between: '输入数据项应在{0}和{1}之间！'
    });
})();