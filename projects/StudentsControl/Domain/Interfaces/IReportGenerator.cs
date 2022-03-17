namespace Domain
{
    public interface IReportGenerator
    {
        public string GenerateJsonByLecture(LectureModel lecture);
        public string GenerateXmlByLecture(LectureModel lecture);
        public string GenerateJsonByStudent(StudentModel student);
        public string GenerateXmlByStudent(StudentModel student);
    }
}