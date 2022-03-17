namespace Controllers
{
    using System;
    using System.Collections.Generic;
    public class LectureCreationDto
    {
        public string Topic { get; init; }
        public Guid Course { get; init; }

        public IEnumerable<Guid> SubmitedHomeWorks { get; init; }
        public IEnumerable<Guid> StudentsThatAttend { get; init; }
        DateTime DateOfLecture { get; init; }
    }
}