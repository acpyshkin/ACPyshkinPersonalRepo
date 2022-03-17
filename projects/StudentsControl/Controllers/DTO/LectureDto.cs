namespace Controllers
{
    using System;
    using System.Collections.Generic;
    public class LectureDto
    {
        public Guid Id { get; init; }
        public string Topic { get; init; }
        public IEnumerable<Guid> StudentsThatAttend { get; init; }
        public Guid Course { get; init; }

        public IEnumerable<Guid> SubmitedHomeWorks { get; init; }
        public DateTime DateOfLecture { get; init; }
    }
}