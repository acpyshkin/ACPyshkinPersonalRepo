namespace BuisnessLogic
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text.Json;
    using System.Xml.Serialization;
    using AutoMapper;
    using Domain;

    public class ReportGenerator : IReportGenerator
    {
        private readonly IMapper _mapper;

        public ReportGenerator(IMapper mapper)
        {
            _mapper = mapper;
        }

        public string GenerateJsonByLecture(LectureModel lecture)
        {
            var report = MapToLectureForReport(lecture);
            return JsonSerializer.Serialize(report);
        }

        public string GenerateJsonByStudent(StudentModel student)
        {
            var report = MapToStudetForReport(student);

            return JsonSerializer.Serialize(report);
        }

        public string GenerateXmlByLecture(LectureModel lecture)
        {
            var report = MapToLectureForReport(lecture);

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(LectureForReport));
            using (StringWriter writer = new StringWriter())
            {
                xmlSerializer.Serialize(writer, report);
                return writer.ToString();
            }
        }

        public string GenerateXmlByStudent(StudentModel student)
        {
            var report = MapToStudetForReport(student);

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(StudentForReport));
            using (StringWriter writer = new StringWriter())
            {
                xmlSerializer.Serialize(writer, report);
                return writer.ToString();
            }
        }

        private LectureForReport MapToLectureForReport(LectureModel lectureToMap)
        {
            var students = new List<StudentForReport>();
            foreach (StudentModel student in lectureToMap.StudentsThatAttend)
            {
                students.Add(_mapper.Map<StudentForReport>(student));
            }

            var result = new LectureForReport()
            {
                ID = lectureToMap.Id,
                Topic = lectureToMap.Topic,
                DateOfLecture = lectureToMap.DateOfLecture,
                CourseID = lectureToMap.Course.Id,
                CourseName = lectureToMap.Course.Name,
                StudentsThatAttentLecture = students
            };
            return result;
        }

        private StudentForReport MapToStudetForReport(StudentModel studentToMap)
        {
            var lectures = new List<LectureForReport>();

            foreach (LectureModel lecture in studentToMap.LectureAttandance)
            {
                lectures.Add(_mapper.Map<LectureForReport>(lecture));
            }

            var result = new StudentForReport()
            {
                ID = studentToMap.Id,
                Name = studentToMap.Name,
                Email = studentToMap.Email,
                PhoneNumber = studentToMap.PhoneNumber,
                CourseID = studentToMap.Course.Id,
                CourseName = studentToMap.Course.Name,
                LecturesThatStudentAttend = lectures
            };
            return result;
        }
    }
}