namespace Controllers
{
    using System;
    using System.Collections.Generic;
    using Domain;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Repository;

    public static class HostExtention
    {
        public static IHost AddTestData(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            using var context = scope.ServiceProvider.GetService<RepositoryContext>();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            LecturerEntity onotoliy = new LecturerEntity { Id = Guid.NewGuid(), Name = "Onotole", Email = "onotole@ololo.up4k" };

            CourseEntity cSharp = new CourseEntity { Id = Guid.NewGuid(), Name = "C#", Lecturer = onotoliy, AppointedLecturesList = new List<LectureEntity>(), AppointedStudentsList = new List<StudentEntity>() };

            StudentEntity tom = new StudentEntity { Id = Guid.NewGuid(), Course = cSharp, Name = "Tom", Email = "tom@email.com", PhoneNumber = 1112223340 ,LectureAttandance = new List<LectureEntity>() };
            StudentEntity alice = new StudentEntity { Id = Guid.NewGuid(), Course = cSharp, Name = "Alice", Email = "alice@email.com", PhoneNumber = 1112223341, LectureAttandance = new List<LectureEntity>() };
            StudentEntity bob = new StudentEntity { Id = Guid.NewGuid(), Course = cSharp, Name = "Bob", Email = "bob@email.com", PhoneNumber = 1112223342, LectureAttandance = new List<LectureEntity>() };
            StudentEntity sam = new StudentEntity { Id = Guid.NewGuid(), Course = cSharp, Name = "Sam", Email = "sam@email.com", PhoneNumber = 1112223343, LectureAttandance = new List<LectureEntity>() };
            StudentEntity alfred = new StudentEntity { Id = Guid.NewGuid(), Course = cSharp, Name = "Alfred", Email = "alfred@email.com", PhoneNumber = 1112223344, LectureAttandance = new List<LectureEntity>() };
            StudentEntity samantha = new StudentEntity { Id = Guid.NewGuid(), Course = cSharp, Name = "Samantha", Email = "samantha@email.com", PhoneNumber = 111222334, LectureAttandance = new List<LectureEntity>() };

            LectureEntity algorithms = new LectureEntity { Id = Guid.NewGuid(), Topic = "Algorithms", Course = cSharp, DateOfLecture = new DateTime(2020, 5, 10), StudentsThatAttend = new List<StudentEntity>() };
            LectureEntity dotnet = new LectureEntity { Id = Guid.NewGuid(), Topic = "Dotnet", Course = cSharp, DateOfLecture = new DateTime(2020, 6, 20), StudentsThatAttend = new List<StudentEntity>() };
            LectureEntity math = new LectureEntity { Id = Guid.NewGuid(), Topic = "Math", Course = cSharp, DateOfLecture = new DateTime(2020, 7, 21), StudentsThatAttend = new List<StudentEntity>() };

            HomeWorkEntity homeWork1_1 = new HomeWorkEntity { Id = Guid.NewGuid(), Mark = 5, AssignedStudent = tom, RelevantLecture = algorithms };
            HomeWorkEntity homeWork1_2 = new HomeWorkEntity { Id = Guid.NewGuid(), Mark = 5, AssignedStudent = tom, RelevantLecture = dotnet };
            HomeWorkEntity homeWork1_3 = new HomeWorkEntity { Id = Guid.NewGuid(), Mark = 0, AssignedStudent = tom, RelevantLecture = math };
            HomeWorkEntity homeWork2_1 = new HomeWorkEntity { Id = Guid.NewGuid(), Mark = 4, AssignedStudent = alice, RelevantLecture = algorithms };
            HomeWorkEntity homeWork2_2 = new HomeWorkEntity { Id = Guid.NewGuid(), Mark = 0, AssignedStudent = alice, RelevantLecture = dotnet };
            HomeWorkEntity homeWork2_3 = new HomeWorkEntity { Id = Guid.NewGuid(), Mark = 5, AssignedStudent = alice, RelevantLecture = math };
            HomeWorkEntity homeWork3_1 = new HomeWorkEntity { Id = Guid.NewGuid(), Mark = 0, AssignedStudent = bob, RelevantLecture = algorithms };
            HomeWorkEntity homeWork3_2 = new HomeWorkEntity { Id = Guid.NewGuid(), Mark = 3, AssignedStudent = bob, RelevantLecture = dotnet };
            HomeWorkEntity homeWork3_3 = new HomeWorkEntity { Id = Guid.NewGuid(), Mark = 4, AssignedStudent = bob, RelevantLecture = math };
            HomeWorkEntity homeWork4_1 = new HomeWorkEntity { Id = Guid.NewGuid(), Mark = 5, AssignedStudent = sam, RelevantLecture = algorithms };
            HomeWorkEntity homeWork4_2 = new HomeWorkEntity { Id = Guid.NewGuid(), Mark = 0, AssignedStudent = sam, RelevantLecture = dotnet };
            HomeWorkEntity homeWork4_3 = new HomeWorkEntity { Id = Guid.NewGuid(), Mark = 0, AssignedStudent = sam, RelevantLecture = math };
            HomeWorkEntity homeWork5_1 = new HomeWorkEntity { Id = Guid.NewGuid(), Mark = 0, AssignedStudent = alfred, RelevantLecture = algorithms };
            HomeWorkEntity homeWork5_2 = new HomeWorkEntity { Id = Guid.NewGuid(), Mark = 5, AssignedStudent = alfred, RelevantLecture = dotnet };
            HomeWorkEntity homeWork5_3 = new HomeWorkEntity { Id = Guid.NewGuid(), Mark = 0, AssignedStudent = alfred, RelevantLecture = math };
            HomeWorkEntity homeWork6_1 = new HomeWorkEntity { Id = Guid.NewGuid(), Mark = 5, AssignedStudent = samantha, RelevantLecture = algorithms };
            HomeWorkEntity homeWork6_2 = new HomeWorkEntity { Id = Guid.NewGuid(), Mark = 5, AssignedStudent = samantha, RelevantLecture = dotnet };
            HomeWorkEntity homeWork6_3 = new HomeWorkEntity { Id = Guid.NewGuid(), Mark = 5, AssignedStudent = samantha, RelevantLecture = math };



            tom.LectureAttandance.Add(algorithms);
            tom.LectureAttandance.Add(dotnet);
            alice.LectureAttandance.Add(algorithms);
            alice.LectureAttandance.Add(math);
            bob.LectureAttandance.Add(dotnet);
            bob.LectureAttandance.Add(math);
            sam.LectureAttandance.Add(algorithms);
            alfred.LectureAttandance.Add(dotnet);
            samantha.LectureAttandance.Add(dotnet);
            samantha.LectureAttandance.Add(algorithms);
            samantha.LectureAttandance.Add(math);
            cSharp.AppointedStudentsList.Add(tom);
            cSharp.AppointedStudentsList.Add(alice);
            cSharp.AppointedStudentsList.Add(bob);
            cSharp.AppointedStudentsList.Add(sam);
            cSharp.AppointedStudentsList.Add(alfred);
            cSharp.AppointedStudentsList.Add(samantha);
            cSharp.AppointedLecturesList.Add(algorithms);
            cSharp.AppointedLecturesList.Add(dotnet);
            cSharp.AppointedLecturesList.Add(math);

            context.Students.AddRange(tom, alice, bob, sam, samantha, alfred);
            context.Lectures.AddRange(algorithms, dotnet, math);
            context.Lecturers.Add(onotoliy);
            context.Courses.Add(cSharp);
            context.HomeWorks.AddRange(homeWork1_1, homeWork1_2, homeWork1_3, homeWork2_1, homeWork2_2, homeWork2_3, homeWork3_1, homeWork3_2, homeWork3_3, homeWork4_1, homeWork4_2, homeWork4_3, homeWork5_1, homeWork5_2, homeWork5_3, homeWork6_1, homeWork6_2, homeWork6_3);

            context.SaveChanges();

            return host;
        }
    }
}