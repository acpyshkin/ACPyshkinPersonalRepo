namespace Domain
{
    using System;

    public class LecturerEntity
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
        public Guid CourseId { get; init; }
        public CourseEntity Course { get; init; }
    }
}