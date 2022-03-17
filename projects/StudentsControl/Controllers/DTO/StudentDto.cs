namespace Controllers
{
    using System;
    using System.Collections.Generic;

    public class StudentDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
        public int PhoneNumber { get; init; }
        public Guid Course { get; init; }
        public List<Guid> AppointedHomeWorksList { get; init; }
        public List<Guid> LectureAttandance { get; init; }
    }
}