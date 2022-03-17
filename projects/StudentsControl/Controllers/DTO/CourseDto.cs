namespace Controllers
{
    using System;
    using System.Collections.Generic;

    public class CourseDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public Guid Lecturer { get; init; }
        public IEnumerable<Guid> AppointedStudentsIdsList { get; init; }
        public IEnumerable<Guid> AppointedLecturesIdsList { get; init; }
    }
}