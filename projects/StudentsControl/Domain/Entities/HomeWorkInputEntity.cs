namespace Domain
{
    using System;

    public class HomeWorkInputEntity
    {
        public Guid Id { get; init; }
        public int Mark { get; init; }
        public Guid AssignedStudent { get; init; }
        public Guid RelevantLecture { get; init; }
    }
}