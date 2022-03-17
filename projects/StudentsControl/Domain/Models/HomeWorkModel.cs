namespace Domain
{
    using System;

    public class HomeWorkModel
    {
        public Guid Id { get; init; }
        public int Mark { get; init; }
        public StudentModel AssignedStudent { get; init; }
        public LectureModel RelevantLecture { get; init; }
    }
}