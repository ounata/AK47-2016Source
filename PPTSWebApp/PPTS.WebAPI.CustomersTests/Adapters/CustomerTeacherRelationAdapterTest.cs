using MCS.Library.Core;
using MCS.Library.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.CustomersTests.Adapters
{
    [TestClass()]
    public class CustomerTeacherRelationAdapterTest
    {
        [TestMethod()]
        public void Test() {
            CustomerTeacherRelation ctr = CustomerTeacherRelationAdapter.Instance.Load(builder => builder.AppendItem("CustomerID", "3990032").AppendItem("TeacherJobID", "93276-Group"), DateTime.MinValue).FirstOrDefault();//查询
            if(ctr==null)
                ctr = new CustomerTeacherRelation() {ID= UuidHelper.NewUuidString(), CustomerID = "1358641" , TeacherJobID ="0"};//insert
            //ctr.TeacherID = DateTime.Now.ToString();//update
            
            //CustomerTeacherRelationAdapter.Instance.Update(ctr);
            CustomerTeacherRelationAdapter.Instance.Delete(ctr);//delete
        }

        [TestMethod()]
        public void InsertCollection() {
            CustomerTeacherRelationCollection ctrc = new CustomerTeacherRelationCollection();
            ctrc.Add(new CustomerTeacherRelation() {  CustomerID = "1358641", TeacherJobID = "0" });
            ctrc.Add(new CustomerTeacherRelation() {  CustomerID = "1358642", TeacherJobID = "0" });
            ctrc.Add(new CustomerTeacherRelation() {  CustomerID = "1358643", TeacherJobID = "0" });
            ctrc.Add(new CustomerTeacherRelation() {  CustomerID = "1358644", TeacherJobID = "0" });

            CustomerTeacherRelationAdapter.Instance.InsertCollection(ctrc);
        }
    }
}
