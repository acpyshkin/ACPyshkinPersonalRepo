namespace Domain
{
    using System;
    using System.Collections.Generic;
    public class LectureEntity
    {
        public LectureEntity()
        {
        }

        public LectureEntity(Guid id, string topic, CourseEntity course, IReadOnlyCollection<StudentEntity> studentThatAttend, IReadOnlyCollection<HomeWorkEntity> submitedHomeWorks, DateTime dateOfLecture)
        {
            Id = id;
            Topic = topic;
            Course = course;
            StudentsThatAttend = studentThatAttend;
            SubmitedHomeWorks = submitedHomeWorks;
            DateOfLecture = dateOfLecture;
        }

        public Guid Id { get; init; }
        public string Topic { get; init; }
        public Guid CourseID { get; init; }
        public CourseEntity Course { get; init; }
        public IReadOnlyCollection<StudentEntity> StudentsThatAttend { get; init; }
        public IReadOnlyCollection<HomeWorkEntity> SubmitedHomeWorks { get; init; }
        public DateTime DateOfLecture { get; init; }
    }
}