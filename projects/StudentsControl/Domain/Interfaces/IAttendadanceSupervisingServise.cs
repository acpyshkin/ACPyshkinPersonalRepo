namespace Domain
{
    using System;
    using System.Collections.Generic;

    public interface IAttendadanceSupervisingServise
    {
        public void NotifyIfEligible(StudentModel student);
        public void NotifyIfEligible(IReadOnlyCollection<StudentModel> students);
        public void NotifyIfEligible(ICollection<StudentModel> students);
    }
}