namespace Domain
{
    using System;
    using System.Collections.Generic;

    public class StudentInputEntity
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
        public int PhoneNumber { get; init; }
        public Guid Course { get; init; }
        public IReadOnlyCollection<Guid> AppointedHomeWorksList { get; init; }
        public IReadOnlyCollection<Guid> LectureAttandance { get; init; }
    }
}