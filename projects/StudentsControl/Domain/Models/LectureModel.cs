namespace Domain
{
    using System;
    using System.Collections.Generic;
    public class LectureModel
    {
        public Guid Id { get; init; }
        public string Topic { get; init; }
        public CourseModel Course { get; init; }
        public IReadOnlyCollection<StudentModel> StudentsThatAttend { get; init; }
        public IReadOnlyCollection<HomeWorkModel> SubmitedHomeWorks { get; init; }
        public DateTime DateOfLecture { get; init; }
    }
}