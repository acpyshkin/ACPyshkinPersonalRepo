namespace Controllers
{
    using System;
    using System.Collections.Generic;

    public class StudentCreationDto
    {
        public string Name { get; set; }
        public string Email { get; init; }
        public int PhoneNumber { get; init; }
        public Guid Course { get; set; }
        public List<Guid> AppointedHomeWorksList { get; set; }
        public List<Guid> LectureAttandance { get; set; }
    }
}