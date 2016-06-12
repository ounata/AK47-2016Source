(function () {
    'use strict';

    mcs.ng.service('mcsValidationService', ['mcsValidationRules', 'mcsValidationMessageConfig', '$parse', function (rules, config, $parse) {
        var service = this;

        var checkValidationResult = function (elem, options) {
            var opts = $.extend({
                errorMessage: config.general,
                validate: true
            }, options);

            var parent = elem.closest('.form-group');
            var validateRow = elem.closest('.row');
            if (!parent || !parent.length) {
                var message = elem.parent().find('.help-block');
                if (!message || !message.length) {
                    elem.parent().append('<div class="help-block"></div>');
                    message = elem.parent().find('.help-block');
                }
                if (opts.validate) {
                    elem.removeClass('has-error');
                    elem.parent().find('.control-label').removeClass('has-error');
                    message.removeClass('has-error').text('').css('visibility', 'hidden');
                } else {
                    elem.addClass('has-error');
                    elem.parent().find('.control-label').addClass('has-error');
                    message.text(opts.errorMessage).css('visibility', 'visible');
                }
            } else {
                var message = parent.find('.help-block');
                var validationItems = validateRow.find('.form-group');
                if (!message || !message.length) {
                    // 对于单行中只有一个验证项则附加水平消息框
                    if (validationItems.length == 1) {
                        parent.append('<div class="help-block horizontal"></div>');
                    } else {
                        elem.parent().append('<div class="help-block"></div>');
                    }
                    message = parent.find('.help-block');
                }

                if (opts.validate) {
                    parent.removeClass('validate has-error');
                    message.text('').css('visibility', 'hidden');
                } else {
                    parent.addClass('validate has-error');
                    message.text(opts.errorMessage).css('visibility', 'visible');
                }
            }

            // 设置已经过验证（如果启用提交时验证则不需要设置验证结果）
            if (!mcs.util.hasAttr(elem, 'submit-validate')) {
                elem.attr('data-validate-result', opts.validate);
            }

            return opts.validate;
        };

        var filterValidationElems = function (form, callback) {
            // 查找页面中需要进行验证的元素
            var elems = !form ? $('input, select, textarea') : $('input, select, textarea', form);
            elems.each(function () {
                var $this = $(this);
                // 如果是隐藏域或文件输入或父级隐藏则不参与验证
                if ($this.is(':hidden') && !($this.is('select') && $this.next().hasClass('select2-container'))
                    || $this.is(':file')
                    || $this.closest('.form-group').is(':hidden')
                    || !mcs.util.hasAttrs($this, rules)) {
                    return true;
                }
                //如果当前元素从未验证过则参与验证，否则将维持原状
                if (!mcs.util.hasAttr($this, 'data-validate-result')) {
                    if (typeof callback === 'function') {
                        callback($this);
                    };
                }
            });
        };

        service.validate = function (elem, scope) {
            var validationResult = true;

            // 如不包含任何验证规则，则不参与验证
            if (!mcs.util.hasAttrs(elem, rules)) return validationResult;
            // 如果验证通过，则跳转到下一规则继续验证
            // required
            if (validationResult && mcs.util.hasAttr(elem, 'required')) {
                var cascading = elem.is('select') && elem.parent().siblings().not(':hidden').length > 0;
                validationResult = checkValidationResult(elem, {
                    errorMessage: elem.attr('data-validation-required-message') || function () {
                        if (elem.is(':checkbox') || elem.is(':radio')) {
                            return config.selected;
                        } else if (elem.is('select')) {
                            return cascading ? config.requiredAll : config.selected;
                        } else {
                            return config.required;
                        }
                    }(),
                    validate: function () {
                        if (elem.is(':checkbox')) {
                            return scope.model == undefined ? elem.parent().parent().find(':checkbox:checked').length > 0 : scope.model.length > 0;
                        } else if (elem.is(':radio')) {
                            return scope.model == undefined ? elem.parent().parent().find(':radio:checked').length > 0 : !!scope.model;
                        } else if (elem.is('select')) {
                            if (cascading) {
                                // 如果是级联框
                                var selected = true;
                                elem.parent().not(':hidden').find('select[required]').each(function () {
                                    var $this = $(this);
                                    if ($.trim($this.find('option:selected').val()).length == 0) {
                                        selected = false;
                                        return false; // 跳出循环
                                    }
                                });
                                return selected;
                            } else {
                                // 如果是单选框
                                return scope.model == undefined ? $.trim(elem.find('option:selected').val()) > 0 : !!scope.model;
                            }
                        } else if (mcs.util.hasClasses(elem, 'date-picker data-range')) {
                            // 对日期范围，数据范围控件单独处理
                            var valid = true;
                            elem.each(function () {
                                var $this = $(this);
                                if ($.trim($this.val()).length == 0) {
                                    valid = false;
                                    return false; // 跳出循环
                                }
                            });
                            return valid;
                        } else {
                            return $.trim(elem.val()).length > 0;
                        }
                    }()
                });
            }
            // required-level(only for mcs-cascading-select)
            if (validationResult && mcs.util.hasAttr(elem, 'required-level')) {
                // 如果当前元素是级联控件才参与验证
                var parent = elem.closest('.mcs-cascading-select-container');
                if (parent) {
                    var requiredLevel = parseInt(parent.attr('required-level'));
                    if (requiredLevel > 0) {
                        validationResult = checkValidationResult(elem, {
                            errorMessage: elem.attr('data-validation-required-message') || mcs.util.format(config.requiredLevel, requiredLevel),
                            validate: function () {
                                var selected = true;
                                elem.parent().not(':hidden').find('select[required-level]').each(function (index) {
                                    var $this = $(this);
                                    if ($.trim($this.find('option:selected').val()).length == 0) {
                                        selected = false;
                                        return false; // 跳出循环
                                    }
                                });

                                return selected;
                            }()
                        });
                    }
                }
            }
            // 如果不参与required验证，则不需要再进行其他规则验证
            var skipRequiredValidation = validationResult && !mcs.util.hasAttrs(elem, 'required required-level') && $.trim(elem.val()).length == 0;
            // minlength
            if (validationResult && mcs.util.hasAttr(elem, 'minlength')) {
                var minlength = parseInt(elem.attr('minlength'));
                if (minlength > 0) {
                    validationResult = checkValidationResult(elem, {
                        errorMessage: elem.attr('data-validation-minlength-message') || mcs.util.format(config.minlength, minlength),
                        validate: skipRequiredValidation || $.trim(elem.val()).length >= minlength
                    });
                }
            }
            //maxlength
            if (validationResult && mcs.util.hasAttr(elem, 'maxlength')) {
                var maxlength = parseInt(elem.attr('maxlength'));
                if (maxlength > 0) {
                    validationResult = checkValidationResult(elem, {
                        errorMessage: elem.attr('data-validation-maxlength-message') || mcs.util.format(config.maxlength, maxlength),
                        validate: skipRequiredValidation || $.trim(elem.val()).length <= maxlength
                    });
                }
            }
            //min
            if (validationResult && mcs.util.hasAttr(elem, 'min')) {
                var min = parseFloat(elem.attr('min'));
                validationResult = checkValidationResult(elem, {
                    errorMessage: elem.attr('data-validation-min-message') || mcs.util.format(config.min, min),
                    validate: skipRequiredValidation || parseFloat($.trim(elem.val())) >= min
                });
            }
            //max
            if (validationResult && mcs.util.hasAttr(elem, 'max')) {
                var max = parseFloat(elem.attr('max'));
                validationResult = checkValidationResult(elem, {
                    errorMessage: elem.attr('data-validation-max-message') || mcs.util.format(config.max, max),
                    validate: skipRequiredValidation || parseFloat($.trim(elem.val())) <= max
                });
            }
            //positive
            if (validationResult && mcs.util.hasAttr(elem, 'positive')) {
                validationResult = checkValidationResult(elem, {
                    errorMessage: elem.attr('data-validation-positive-message') || config.positive,
                    validate: skipRequiredValidation || ($.trim(elem.val()) == '0' || (/^[1-9]+[0-9]*$/).test($.trim(elem.val())))
                });
            }
            //negative
            if (validationResult && mcs.util.hasAttr(elem, 'negative')) {
                validationResult = checkValidationResult(elem, {
                    errorMessage: elem.attr('data-validation-negative-message') || config.negative,
                    validate: skipRequiredValidation || ($.trim(elem.val()) == '0' || (/^-[1-9]+[0-9]*$/).test($.trim(elem.val())))
                });
            }
            //number
            if (validationResult && mcs.util.hasAttr(elem, 'number')) {
                validationResult = checkValidationResult(elem, {
                    errorMessage: elem.attr('data-validation-number-message') || config.number,
                    validate: skipRequiredValidation || (/^(([1-9]\d*)|0)(\.\d+)?$/).test($.trim(elem.val()))
                });
            }
            //currency
            if (validationResult && mcs.util.hasAttr(elem, 'currency')) {
                validationResult = checkValidationResult(elem, {
                    errorMessage: elem.attr('data-validation-currency-message') || config.currency,
                    validate: skipRequiredValidation || (/^(([1-9]\d*)|0)(\.\d{1,2})?$/).test($.trim(elem.val()))
                });
            }
            //less
            if (validationResult && mcs.util.hasAttr(elem, 'less')) {
                var less = parseFloat(elem.attr('less'));
                validationResult = checkValidationResult(elem, {
                    errorMessage: elem.attr('data-validation-less-message') || mcs.util.format(config.less, less),
                    validate: skipRequiredValidation || parseFloat($.trim(elem.val())) > less
                });
            }
            //great
            if (validationResult && mcs.util.hasAttr(elem, 'great')) {
                var great = parseFloat(elem.attr('great'));
                validationResult = checkValidationResult(elem, {
                    errorMessage: elem.attr('data-validation-great-message') || mcs.util.format(config.great, great),
                    validate: skipRequiredValidation || parseFloat($.trim(elem.val())) < great
                });
            }
            //between
            if (validationResult && mcs.util.hasAttr(elem, 'between')) {
                var between = elem.attr('between').split(',');
                if (between.length == 2) {
                    var min = parseFloat(between[0]);
                    var max = parseFloat(between[1]);
                    if (!isNaN(min) && !isNaN(max)) {
                        validationResult = checkValidationResult(elem, {
                            errorMessage: elem.attr('data-validation-between-message') || mcs.util.format(config.between, min, max),
                            validate: skipRequiredValidation || (parseFloat($.trim(elem.val())) >= min && parseFloat($.trim(elem.val())) <= max)
                        });
                    }
                }
            }
            //phone
            if (validationResult && mcs.util.hasAttr(elem, 'phone')) {
                validationResult = checkValidationResult(elem, {
                    errorMessage: elem.attr('data-validation-phone-message') || config.phone,
                    validate: skipRequiredValidation || (/(^1[34578]\d{9}$)|(^\d{3,4}-\d{7,8}-\d{1,5}$)|(^\d{3,4}-\d{7,8}$)/).test($.trim(elem.val()))
                });
            }
            //email
            if (validationResult && mcs.util.hasAttr(elem, 'email')) {
                validationResult = checkValidationResult(elem, {
                    errorMessage: elem.attr('data-validation-email-message') || config.email,
                    validate: skipRequiredValidation || (/^[0-9a-z][_.0-9a-z-]{0,31}@([0-9a-z][0-9a-z-]{0,30}[0-9a-z]\.){1,4}[a-z]{2,4}$/).test($.trim(elem.val()))
                });
            }
            //idcard
            if (validationResult && mcs.util.hasAttr(elem, 'idcard')) {
                validationResult = checkValidationResult(elem, {
                    errorMessage: elem.attr('data-validation-idcard-message') || config.idcard,
                    validate: skipRequiredValidation || (/^(\d{18,18}|\d{15,15}|\d{17,17}x|\d{17,17}X)$/).test($.trim(elem.val()))
                });
            }
            //validate
            if (validationResult && mcs.util.hasAttr(elem, 'validate')) {
                return $parse(elem.attr('validate'))(scope);
            }

            return validationResult;
        };

        service.init = function (scope, form) {
            // 查找页面中需要进行验证的元素
            scope.$on('$viewContentLoaded', function (event) {
                filterValidationElems(form, function (elem) {
                    if (elem.is(':input[type=text]') || elem.is('textarea')) {
                        elem.blur(function () {
                            service.validate(elem, scope);
                        });
                    }
                });
            });
        };
        // 提交整个页面时开始触发
        service.run = function (scope, form) {
            // 查找页面中需要进行验证的元素
            filterValidationElems(form, function (elem) {
                if ((elem.is(':input[type=text]') || elem.is('textarea')) && !mcs.util.hasClasses(elem, 'date-picker date-timepicker data-range')) {
                    elem.blur();
                } else {
                    service.validate(elem, scope);
                }
            });
            return !$('.has-error').length;
        };

        return service;
    }]).constant('mcsValidationRules', [
        'required',
        'required-level', // 用于指定级联下拉规则
        'minlength',
        'maxlength',
        'min',
        'max',
        'positive',
        'negative',
        'number',
        'currency',
        'great',
        'less',
        'between',
        'phone',
        'email',
        'idcard',
        'validate'
    ]).constant('mcsValidationMessageConfig', {
        general: '输入数据项不正确，请重新输入!',
        required: '输入数据项为必填！',
        requiredLevel: '请选择到第{0}级数据！',
        requiredAll: '请选择所有级别数据！',
        selected: '请至少选择一项！',
        minlength: '应至少输入{0}个字符！',
        maxlength: '最多只能输入{0}个字符！',
        min: '输入数据项应大于或等于{0}！',
        max: '输入数据项应小于或等于{0}！',
        positive: '输入数据项应为正整数！',
        negative: '输入数据项应为负整数！',
        number: '输入数据项应为小数！',
        currency: '输入数据项应为货币值(最多保留两位小数)！',
        great: '输入数据项应小于{0}！',
        less: '输入数据项应大于{0}！',
        between: '输入数据项应在{0}和{1}之间！',
        phone: '输入数据项不是合法电话！',
        email: '输入数据项不是合法电子邮件！',
        idcard: '输入数据项不是合法身份证号！'
    });
})();