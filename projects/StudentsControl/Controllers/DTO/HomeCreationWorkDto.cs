namespace Controllers
{
    using System;
    public class HomeWorkCreationDto
    {
        public int Mark { get; set; }
        public Guid AssignedStudent { get; set; }
        public Guid RelevantLecture { get; set; }
    }
}