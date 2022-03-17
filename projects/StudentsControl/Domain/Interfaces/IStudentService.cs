namespace Domain
{
    using System;
    using System.Collections.Generic;

    public interface IStudentService
    {
        IReadOnlyCollection<StudentModel> GetAll();
        StudentModel Get(Guid id);
        StudentModel Create(StudentInputModel student);
        StudentModel Edit(StudentInputModel student);
        public bool Delete(Guid id);
        public string GetJsonReportByStudentID(Guid studentID);
        public string GetXmlReportByStudentId(Guid studentID);


    }
}