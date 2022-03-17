namespace Domain
{
    using System;
    using System.Collections.Generic;

    public class CourseInputEntity
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public Guid Lecturer { get; init; }
        public IReadOnlyCollection<Guid> AppointedStudentsIdsList { get; init; }
        public IReadOnlyCollection<Guid> AppointedLecturesIdsList { get; init; }
    }
}