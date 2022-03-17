namespace Domain
{
    using System;

    public class HomeWorkEntity
    {
        public Guid Id { get; init; }
        public int Mark { get; init; }
        public StudentEntity AssignedStudent { get; init; }
        public LectureEntity RelevantLecture { get; init; }
    }
}