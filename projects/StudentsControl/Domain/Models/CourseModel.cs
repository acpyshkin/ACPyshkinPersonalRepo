namespace Domain
{
    using System;
    using System.Collections.Generic;

    public class CourseModel
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public LecturerModel Lecturer { get; init; }
        public IReadOnlyCollection<StudentModel> AppointedStudentsList { get; init; }
        public IReadOnlyCollection<LectureModel> AppointedLecturesList { get; init; }
    }
}