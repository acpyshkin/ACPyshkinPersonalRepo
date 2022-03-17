namespace Domain
{
    using System;
    using System.Collections.Generic;

    public class StudentEntity
    {
        public StudentEntity()
        {
        }

        public StudentEntity(Guid id, string name, string email, int phoneNumber, CourseEntity course, IReadOnlyCollection<HomeWorkEntity> appointedHomeWorksList, List<LectureEntity> lectureAttandance)
        {
            Id = id;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Course = course;
            AppointedHomeWorksList = appointedHomeWorksList;
            LectureAttandance = lectureAttandance;
        }

        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
        public int PhoneNumber { get; init; }
        public Guid CourseID { get; init; }
        public CourseEntity Course { get; init; }
        public IReadOnlyCollection<HomeWorkEntity> AppointedHomeWorksList { get; init; }
        public List<LectureEntity> LectureAttandance { get; set; }
    }
}