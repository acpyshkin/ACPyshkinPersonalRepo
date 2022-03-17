namespace Domain
{
    using System;
    using System.Collections.Generic;

    public class CourseEntity
    {
        public CourseEntity()
        {
        }

        public CourseEntity(Guid id, string name, LecturerEntity lecturer, List<StudentEntity> appointedStudentsList, List<LectureEntity> appointedLecturesList)
        {
            Id = id;
            Name = name;
            Lecturer = lecturer;
            AppointedStudentsList = appointedStudentsList;
            AppointedLecturesList = appointedLecturesList;
        }

        public Guid Id { get; init; }
        public string Name { get; init; }
        public LecturerEntity Lecturer { get; init; }
        public List<StudentEntity> AppointedStudentsList { get; set; }
        public List<LectureEntity> AppointedLecturesList { get; set; }
    }
}