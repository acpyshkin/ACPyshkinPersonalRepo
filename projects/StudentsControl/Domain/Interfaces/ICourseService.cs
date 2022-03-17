namespace Domain
{
    using System;
    using System.Collections.Generic;

    public interface ICourseService
    {
        IReadOnlyCollection<CourseModel> GetAll();
        CourseModel Get(Guid id);
        CourseModel Create(CourseInputModel lecturer);
        CourseModel Edit(CourseInputModel lecturer);
        bool Delete(Guid id);
    }
}