using MCS.Library.Data;
using MCS.Web.MVC.Library.Filters;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Products.Adapters;
using PPTS.WebAPI.Products.DataSources;
using PPTS.WebAPI.Products.Executors;
using PPTS.WebAPI.Products.ViewModels.ServiceFees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PPTS.WebAPI.Customers.Controllers
{
    [ApiPassportAuthentication]
    public class ServiceFeesController : ApiController
    {
        #region api/servicefees/getallservicefees

        /// <summary>
        /// 只获取数据词典(特殊需要)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ExpensesQueryResult LoadDictionaries()
        {
            return new ExpensesQueryResult
            {
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(ExpensesQueryModel))
            };
        }

        /// <summary>
        /// 综合服务费馈默认查询
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        [HttpPost]
        public ExpensesQueryResult GetAllServiceFees(ExpensesCriteriaModel criteria)
        {
            return new ExpensesQueryResult
            {
                QueryResult = ExpensesDataSource.Instance.GetServiceFeesList(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(ExpensesQueryModel))
            };
        }
        #endregion

        #region api/servicefees/getpagedservicefeelist
        /// <summary>
        ///综合服务费分页查询
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        [HttpPost]
        public PagedQueryResult<ExpensesQueryModel, ExpensesQueryCollection> GetPagedServiceFeeList(ExpensesCriteriaModel criteria)
        {
            return ExpensesDataSource.Instance.GetServiceFeesList(criteria.PageParams, criteria, criteria.OrderBy);
        }
        #endregion

        #region api/servicefees/createexpenses
        /// <summary>
        /// 编辑或者创建
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        public void CreateExpenses(EditExpensesModel expense)
        {
            ExpensesExecutor crExecutor = new ExpensesExecutor(expense);
            crExecutor.Execute();
        }
        #endregion

        #region api/servicefees/getExpense
        /// <summary>
        /// 根据ID获取综合服务费
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        [HttpGet]
        public EditExpensesModel GetExpense(string id)
        {
            return new EditExpensesModel
            {
                expense = ExpenseAdapter.Instance.Load(builder => builder.AppendItem("ExpenseID", id)).SingleOrDefault()
            };
        }
        #endregion

        [HttpPost]
        public void DelExpenses(string[] ids)
        {
            DeleteExpensesExecutor dExpenseExcutor = new DeleteExpensesExecutor(new EditExpensesModel() { ExpenseIds=ids});
            dExpenseExcutor.Execute();
        }

    }
}
