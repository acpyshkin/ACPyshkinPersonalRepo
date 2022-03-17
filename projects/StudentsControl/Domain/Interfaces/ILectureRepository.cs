namespace Domain
{
    using System;
    using System.Collections.Generic;
    public interface ILectureRepository : IDisposable
    {
        IReadOnlyCollection<LectureEntity> GetAll();
        LectureEntity Get(Guid id);
        LectureEntity Create(LectureInputEntity lecture);
        LectureEntity Edit(LectureInputEntity lecture);
        public bool Delete(Guid id);
    }
}