namespace Domain
{
    using System;
    using System.Collections.Generic;

    public interface ILectureService
    {
        IReadOnlyCollection<LectureModel> GetAll();
        LectureModel Get(Guid id);
        LectureModel Create(LectureInputModel lecture);
        LectureModel Edit(LectureInputModel lecture);
        bool Delete(Guid id);
        public string GetJsonReportByLectureID(Guid lectureID);
        public string GetXmlReportByLectureID(Guid lectureID);

    }
}