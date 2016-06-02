using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace PPTS.ExtServices.UnionPay.Validation
{
    public class ValidateExecutor
    {
        /// <summary>
        /// 获取验证结果信息
        /// </summary>
        /// <param name="obj">验证实体对象</param>
        /// <param name="msg">预制的错误信息</param>
        /// <returns></returns>
        public bool GetValidateResult(object obj, out string msg)
        {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo property in properties)
            {
                object[] validateContent = property.GetCustomAttributes(typeof(ValidateAttribute), true);
                if (validateContent != null)
                {
                    var value = property.GetValue(obj);

                    foreach (ValidateAttribute validateAttribute in validateContent)
                    {
                        IList<ValidateModel> condition = new List<ValidateModel>();

                        condition.Add(new ValidateModel() { Type = ValidateType.IsDecimal, CheckFunc = () => { Regex regex = new Regex(validateAttribute.RegexContent); Match match = regex.Match(value.ToString()); return !match.Success; }, ErrorMessage = validateAttribute.ErrorMessage });
                        condition.Add(new ValidateModel() { Type = ValidateType.IsMiniDate, CheckFunc = () => { return value == null || (DateTime)value == DateTime.MinValue; }, ErrorMessage = validateAttribute.ErrorMessage });
                        condition.Add(new ValidateModel() { Type = ValidateType.IsLessEqualZero, CheckFunc = () => { return value == null || (decimal)value <= 0; }, ErrorMessage = validateAttribute.ErrorMessage });
                        condition.Add(new ValidateModel() { Type = ValidateType.NullOrEmpty, CheckFunc = () => { return value == null || value.ToString() == string.Empty; }, ErrorMessage = validateAttribute.ErrorMessage });

                        string checkResult = string.Empty;
                        foreach (ValidateModel model in condition)
                        {
                            checkResult = CheckValidate(
                                validateAttribute.ValidateType,
                                model.Type,
                                model.CheckFunc,
                                model.ErrorMessage
                                );
                            if (!string.IsNullOrEmpty(checkResult))
                            {
                                msg = checkResult;
                                return false;
                            }
                        }

                    }
                }
            }
            msg = string.Empty;
            return true;
        }

        private string CheckValidate(ValidateType checkType, ValidateType matchType, Func<bool> func, string errorMessage)
        {
            if (checkType.HasFlag(matchType))
            {
                if (func())
                {
                    return errorMessage;
                }
            }
            return string.Empty;
        }
    }
}