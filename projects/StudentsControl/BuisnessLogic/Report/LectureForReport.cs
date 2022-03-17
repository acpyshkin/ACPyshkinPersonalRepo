namespace BuisnessLogic
{
    using System;
    using System.Collections.Generic;

    public class LectureForReport
    {
        public Guid ID { get; init; }
        public string Topic { get; init; }
        public DateTime DateOfLecture { get; init; }
        public Guid CourseID { get; init; }
        public string CourseName { get; init; }
        public List<StudentForReport> StudentsThatAttentLecture { get; init; }
    }
}