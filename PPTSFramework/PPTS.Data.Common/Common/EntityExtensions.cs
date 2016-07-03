//===================================================================================
// 实体扩展，主要在实体与实体之间实现转换
//=================================================================================== 
//
// 例如实体A需要转为实体B才可以调用数据访问组件，这时就使用该扩展进行转换
//
//===================================================================================
// 2013-01-01 李栋
//===================================================================================

namespace PPTS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;
    using System.Linq;
    using System.Text;
    using System.IO;

    /// <summary>
    /// 实体扩展类
    /// </summary>
    public static class EntityExtensions
    {
        /// <summary>
        /// 把原始实体映射为目标实体。
        /// </summary>
        /// <typeparam name="TTarget">目标实体类型</typeparam>
        /// <param name="entity">原实体类型</param>
        /// <returns>目标实体</returns>
        public static TTarget ProjectedAs<TTarget>(this object item)
            where TTarget : class, new()
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var mapper = new AutoMapper.MapperConfiguration(c =>
            {
                c.CreateMap(item.GetType(), typeof(TTarget));
            }).CreateMapper();

            return (TTarget)mapper.Map<TTarget>(item);
        }

        /// <summary>
        /// 把原实体内容映射到目标实体。
        /// </summary>
        /// <typeparam name="TTarget">目标实体</typeparam>
        /// <param name="entity">原实体类型</param>
        /// <param name="target">目标实体</param>
        public static void ProjectedTo<TTarget>(this object item, TTarget target)
            where TTarget : class
        {
            if (item == null)
                throw new ArgumentNullException("item");
            if (target == null)
                throw new ArgumentNullException("target");

            var mapper = new AutoMapper.MapperConfiguration(c =>
            {
                c.CreateMap(item.GetType(), typeof(TTarget));
            }).CreateMapper();

            mapper.Map(item, target, item.GetType(), target.GetType());
        }

        /// <summary>
        /// 把原始实体集合映射为目标实体集合。
        /// </summary>
        /// <typeparam name="TTarget">目标实体类型</typeparam>
        /// <param name="items">原实体类型</param>
        /// <returns>目标实体集合</returns>
        public static TTarget[] ProjectedAsArray<TTarget>(this IEnumerable<object> items)
            where TTarget : class, new()
        {
            if (items == null)
                throw new ArgumentNullException("items");

            List<TTarget> list = new List<TTarget>();
            if (items.Count() != 0)
            {
                var mapper = new AutoMapper.MapperConfiguration(c =>
                {
                    c.CreateMap(items.First().GetType(), typeof(TTarget));
                }).CreateMapper();

                foreach (object item in items)
                    list.Add((TTarget)mapper.Map<TTarget>(item));
            }
            return list.ToArray();
        }

        /// <summary>
        /// 把原始实体集合映射为目标实体集合。
        /// </summary>
        /// <typeparam name="TTarget">目标实体类型</typeparam>
        /// <param name="items">原实体类型</param>
        /// <returns>目标实体集合</returns>
        public static List<TTarget> ProjectedAsList<TTarget>(this IEnumerable<object> items)
            where TTarget : class, new()
        {
            if (items == null)
                throw new ArgumentNullException("items");

            List<TTarget> list = new List<TTarget>();
            if (items.Count() != 0)
            {
                var mapper = new AutoMapper.MapperConfiguration(c =>
                {
                    c.CreateMap(items.First().GetType(), typeof(TTarget));
                }).CreateMapper();

                foreach (object item in items)
                    list.Add((TTarget)mapper.Map<TTarget>(item));
            }
            return list;
        }
    }
}
