namespace Domain
{
    using System;
    using System.Collections.Generic;

    public interface IStudentRepository : IDisposable
    {
        IReadOnlyCollection<StudentEntity> GetAll();
        StudentEntity Get(Guid id);
        StudentEntity Create(StudentInputEntity student);
        StudentEntity Edit(StudentInputEntity student);
        public bool Delete(Guid id);
    }
}