using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using MCS.Library.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace PPTS.Data.Common.Executors
{
    /// <summary>
    /// 编辑实体用到的Executor的基类
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public abstract class PPTSEditEntityExecutorBase<TModel> : DataExecutorBase<UserOperationLogCollection>
    {
        private TModel _Model;
        private bool _NeedValidation = true;
        private Action<TModel> _DataAction = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dataAction"></param>
        public PPTSEditEntityExecutorBase(TModel model, Action<TModel> dataAction)
            : base()
        {
            model.NullCheck("model");

            this._Model = model;
            this._DataAction = dataAction;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dataAction"></param>
        /// <param name="opType"></param>
        public PPTSEditEntityExecutorBase(TModel model, Action<TModel> dataAction, string opType)
            : base(opType)
        {
            model.NullCheck("model");

            this._Model = model;
            this._DataAction = dataAction;
        }

        /// <summary>
        /// 数据实体
        /// </summary>
        public TModel Model
        {
            get
            {
                return this._Model;
            }
        }

        public bool NeedValidation
        {
            get
            {
                return this._NeedValidation;
            }
            set
            {
                this._NeedValidation = value;
            }
        }

        public Action<TModel> DataAction
        {
            get
            {
                return this._DataAction;
            }
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);

            this.Validate();
        }

        /// <summary>
        /// 验证当前数类型
        /// </summary>
        protected virtual void Validate()
        {
            if (this._NeedValidation)
            {
                ValidationResults validationResults = new ValidationResults();

                DoValidate(validationResults);

                ExceptionHelper.TrueThrow(validationResults.ResultCount > 0, validationResults.ToString());
            }
        }

        /// <summary>
        /// 通常重载此方法来进行校验工作
        /// </summary>
        /// <param name="validationResults"></param>
        protected virtual void DoValidate(ValidationResults validationResults)
        {
            Validator validator = ValidationFactory.CreateValidator(typeof(TModel));

            ValidationResults innerResults = validator.Validate(this._Model);

            foreach (ValidationResult innerResult in innerResults)
                validationResults.AddResult(innerResult);
        }

        protected override object DoOperation(DataExecutionContext<UserOperationLogCollection> context)
        {
            if (this._DataAction != null)
                this._DataAction(this._Model);

            return this._Model;
        }

        protected override void PrepareOperationLog(DataExecutionContext<UserOperationLogCollection> context)
        {
            UserOperationLog log = UserOperationLog.FromEnvironment();

            log.Subject = this.GetOperationDescription();

            context.Logs.Add(log);
        }
    }
}
