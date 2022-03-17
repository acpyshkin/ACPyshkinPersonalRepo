namespace BuisnessLogic
{
    using System;
    using System.Collections.Generic;

    public class StudentForReport
    {
        public Guid ID { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
        public int PhoneNumber { get; init; }
        public Guid CourseID { get; init; }
        public string CourseName { get; init; }
        public List<LectureForReport> LecturesThatStudentAttend { get; init; }
    }
}