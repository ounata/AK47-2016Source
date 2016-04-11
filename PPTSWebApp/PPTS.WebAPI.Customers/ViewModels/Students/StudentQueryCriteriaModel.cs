using System;

namespace PPTS.WebAPI.Customers.ViewModels.Students
{
    [Serializable]
    public class StudentQueryCriteriaModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string TeacherZX { get; set; }
        public string TeacherXG { get; set; }
        public string Contact { get; set; }
    }
}
