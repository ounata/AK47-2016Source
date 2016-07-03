using PPTS.Data.Common.Executors;
using System.Linq;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Orders.Adapters;
using PPTS.Data.Orders.Entities;
using PPTS.Data.Common;

namespace PPTS.Data.Orders.Executors
{
    public class PPTSyncOrderExecutor : PPTSExecutorBase
    {
        public PPTSyncOrderExecutor(string opType) : base(opType)
        {

        }

        public string OrderID { set; get; }
        public ProcessStatusDefine ProcessStatus { set; get; }

        public Order Order { set; get; }

        public OrderItemCollection Items { set; get; }

        public AssetCollection Assets { set; get; }

        /// <summary>
        /// 插班 排课信息
        /// </summary>
        public AssignCollection Assigns { set; get; }

        /// <summary>
        /// 插班 排课信息
        /// </summary>
        public ClassLessonItemCollection ClassLessonItems { set; get; }

        protected override object DoOperation(DataExecutionContext<UserOperationLogCollection> context)
        {
            OrdersAdapter.Instance.ModifyProcessStatusInContext(OrderID, (int)ProcessStatus);

            if (Order != null && Items != null && Assets != null )
            {
                var whereSqlBuilder = new MCS.Library.Data.Builder.InSqlClauseBuilder("AssetID");
                whereSqlBuilder.AppendItem(Assets.Select(i => i.AssetID).ToArray());
                GenericAssetAdapter<Asset, AssetCollection>.Instance.UpdateCollectionInContext(whereSqlBuilder, Assets);
            }

            if (Assigns != null && ClassLessonItems!=null)
            {
                ClassLessonItemsAdapter.Instance.UpdateCollectionInContext(ClassLessonItems);
                AssignsAdapter.Instance.UpdateCollectionInContext(Assigns);
            }

            ConnectionDefine.GetDbContext().DoAction(db => db.ExecuteTimePointSqlInContext());

            return null;

        }

        protected override string GetOperationDescription()
        {
            string description = base.GetOperationDescription();
            if (string.IsNullOrWhiteSpace(description))
            {
                return OperationType;
            }
            return description;
        }


    }
}
