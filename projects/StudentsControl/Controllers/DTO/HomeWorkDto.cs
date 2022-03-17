namespace Controllers
{
    using System;
    public class HomeWorkDto
    {
        public Guid Id { get; set; }
        public int Mark { get; set; }
        public Guid AssignedStudent { get; set; }
        public Guid RelevantLecture { get; set; }
    }
}