namespace Domain
{
    using System;
    using System.Collections.Generic;

    public class StudentModel
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
        public int PhoneNumber { get; init; }
        public CourseModel Course { get; init; }
        public IReadOnlyCollection<HomeWorkModel> AppointedHomeWorksList { get; init; }
        public IReadOnlyCollection<LectureModel> LectureAttandance { get; init; }
    }
}