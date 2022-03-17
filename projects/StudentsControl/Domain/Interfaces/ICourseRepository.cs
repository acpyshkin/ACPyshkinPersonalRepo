namespace Domain
{
    using System;
    using System.Collections.Generic;

    public interface ICourseRepository : IDisposable
    {
        IReadOnlyCollection<CourseEntity> GetAll();
        CourseEntity Get(Guid id);
        CourseEntity Create(CourseInputEntity lecturer);
        CourseEntity Edit(CourseInputEntity lecturer);
        public bool Delete(Guid id);
    }
}