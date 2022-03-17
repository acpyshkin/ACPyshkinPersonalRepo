namespace Domain
{
    using System;
    using System.Collections.Generic;

    public interface IStudentPerformanceSupervision
    {
        public void NotifyIfNecessary(StudentModel student);
        public void NotifyIfNecessary(IReadOnlyCollection<StudentModel> students);
        public void NotifyIfNecessary(ICollection<StudentModel> students);

    }
}