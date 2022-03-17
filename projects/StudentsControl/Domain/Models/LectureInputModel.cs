namespace Domain
{
    using System;
    using System.Collections.Generic;
    public class LectureInputModel
    {
        public Guid Id { get; init; }
        public string Topic { get; init; }
        public Guid Course { get; init; }
        public IReadOnlyCollection<Guid> StudentsThatAttend { get; init; }
        public IReadOnlyCollection<Guid> SubmitedHomeWorks { get; init; }
        public DateTime DateOfLecture { get; init; }
    }
}