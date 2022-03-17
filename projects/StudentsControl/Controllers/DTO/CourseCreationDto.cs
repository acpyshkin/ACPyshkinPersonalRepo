namespace Controllers
{
    using System;
    using System.Collections.Generic;
    public class CourseCreationDto
    {
        public string Name { get; init; }
        public Guid LecturerId { get; init; }
        public ICollection<Guid> AppointedStudentsIdsList { get; init; }
        public ICollection<Guid> AppointedLecturesIdsList { get; init; }
    }
}