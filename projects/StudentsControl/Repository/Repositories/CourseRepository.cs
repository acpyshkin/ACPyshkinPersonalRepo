namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    internal class CourseRepository : ICourseRepository
    {
        private readonly ILogger<CourseRepository> _logger;
        private RepositoryContext _context;
        public CourseRepository(RepositoryContext context, ILogger<CourseRepository> logger)
        {
            _logger = logger;
            _context = context;
        }

        public bool Delete(Guid id)
        {
            var courseToDelete = _context.Courses.Find(id);

            // TODO добавить TryCatch с исключениями
            if (courseToDelete == null)
            {
                _logger.LogError($"Course repository throws exception.Course by {id} is not found");
                throw new EntityNotFoundException($"Course by {id} is not found");
            }
            else
            {
                _context.Courses.Remove(courseToDelete);
                _context.SaveChanges();
                return true;
            }
        }

        public CourseEntity Edit(CourseInputEntity courseInput)
        {
            if (courseInput == null)
            {
                _logger.LogError($"Course repository throws exception.Course you are sending to edit is null");
                throw new EntityIsNullException("Course you are sending to edit is null");
            }

            var courseInDB = _context.Courses.Find(courseInput.Id);
            if (courseInDB == null)
            {
                _logger.LogError($"Course repository throws exception.\nCourse by ID: {courseInput.Id}" +
                    "\nyou are sending to edit does not exist at current DB context");
                throw new EntityNotFoundException($"Course by ID: {courseInput.Id}" +
                    "\nyou are sending to edit does not exist at current DB context");
            }

            CourseEntity courseToSave = MapToEntity(courseInput.Id, courseInput);
            /*            _context.Courses.Remove(courseInDB);
                        _context.SaveChanges();
                        _context.Courses.Add(courseToSave);
                        _context.SaveChanges();*/
            _context.Entry(courseInDB).State = EntityState.Detached;
            _context.Entry(courseToSave).State = EntityState.Modified;
            _context.SaveChanges();
            return courseToSave;
        }

        public CourseEntity Get(Guid id)
        {
            if (_context.Courses.Find(id) == null)
            {
                _logger.LogError($"Course repository throws exception.Course by ID {id} does not exist at current DB context");
                throw new EntityNotFoundException($"Course by ID {id} does not exist at current DB context");
            }

            return _context.Courses
                .Include(c => c.AppointedLecturesList)
                .Include(c => c.AppointedStudentsList)
                .Where(x => x.Id == id)
                .SingleOrDefault();
        }

        public IReadOnlyCollection<CourseEntity> GetAll()
        {
            if (_context.Courses == null)
            {
                _logger.LogError($"Lecture repository throws exception.Not a single lecture exist within current DB context");
                throw new EntityNotFoundException("Not a single lecture exist within current DB context");
            }

            return _context.Courses
                .Include(c => c.AppointedLecturesList)
                .Include(c => c.AppointedStudentsList)
                .Include(c => c.Lecturer)
                .ToList();
        }

        public CourseEntity Create(CourseInputEntity courseToCreate)
        {
            if (courseToCreate == null)
            {
                _logger.LogError($"Course repository throws exception.Course you are sending to create is null");

                throw new EntityIsNullException("Course you are sending to create is null");
            }

            Guid newId = Guid.NewGuid();
            CourseEntity courseEntity = MapToEntity(newId, courseToCreate);
            var result = _context.Courses.Add(courseEntity);
            _context.SaveChanges();
            return result.Entity;
        }

        private CourseEntity MapToEntity(Guid id, CourseInputEntity courseToMap)
        {
            var studentEntities = new List<StudentEntity>();

            foreach (Guid guid in courseToMap.AppointedStudentsIdsList)
            {
                studentEntities.Add(_context.Students.Find(guid));
            }

            var lectureEntities = new List<LectureEntity>();
            foreach (Guid guid in courseToMap.AppointedLecturesIdsList)
            {
                lectureEntities.Add(_context.Lectures
                                    .Include(l => l.StudentsThatAttend)
                                    .Include(l => l.SubmitedHomeWorks)
                                    .Where(l => l.Id == guid)
                                    .SingleOrDefault());
            }

            var lecturer = _context.Lecturers.Find(courseToMap.Lecturer);
            return new CourseEntity(id, courseToMap.Name, lecturer, studentEntities, lectureEntities);
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}