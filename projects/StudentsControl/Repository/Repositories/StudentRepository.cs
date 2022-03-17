namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    internal class StudentRepository : IStudentRepository
    {
        private readonly ILogger<StudentRepository> _logger;
        private RepositoryContext _context;
        public StudentRepository(RepositoryContext context, ILogger<StudentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public bool Delete(Guid id)
        {
            var studentToDelete = _context.Students.Find(id);

            // TODO добавить TryCatch с исключениями
            if (studentToDelete == null)
            {
                _logger.LogError($"Student repository throws exception.Student by {id} is not found");
                throw new EntityNotFoundException($"Student by {id} is not found");
            }
            else
            {
                _context.Students.Remove(studentToDelete);
                _context.SaveChanges();
                return true;
            }
        }

        public StudentEntity Edit(StudentInputEntity studentInput)
        {
            if (studentInput == null)
            {
                _logger.LogError($"Student repository throws exception.Student you are sending to edit is null");
                throw new EntityIsNullException("Student you are sending to edit is null");
            }

            var studentInDB = _context.Students.Find(studentInput.Id);
            if (studentInDB == null)
            {
                _logger.LogError($"Student repository throws exception.\nStudent by ID: {studentInput.Id}" +
                    "\nyou are sending to edit does not exist at current DB context");
                throw new EntityNotFoundException($"Student by ID: {studentInput.Id}" +
                    "\nyou are sending to edit does not exist at current DB context");
            }

            StudentEntity studentToSave = MapToEntity(studentInput.Id, studentInput);
            _context.Students.Remove(studentInDB);
            _context.SaveChanges();
            _context.Students.Add(studentToSave);
/*            _context.Entry(studentInDB).State = EntityState.Detached;
            _context.Entry(studentToSave).State = EntityState.Modified;*/
            _context.SaveChanges();
            return studentToSave;
        }

        public StudentEntity Get(Guid id)
        {
            if (_context.Students.Find(id) == null)
            {
                _logger.LogError($"Student repository throws exception.Lecture by ID {id} does not exist at current DB context");
                throw new EntityNotFoundException($"Student by ID {id} does not exist at current DB context");
            }

            return _context.Students
                .Include(s => s.LectureAttandance)
                .Include(s => s.AppointedHomeWorksList)
                .Include(s => s.Course)
                .Where(s => s.Id == id)
                .FirstOrDefault();
        }

        public IReadOnlyCollection<StudentEntity> GetAll()
        {
            if (_context.Students == null)
            {
                _logger.LogError($"Student repository throws exception.Not a single student exist within current DB context");
                throw new EntityNotFoundException("Not a single student exist within current DB context");
            }

            return _context.Students
                .Include(s => s.LectureAttandance)
                .Include(s => s.AppointedHomeWorksList)
                .Include(s => s.Course)
                .ToList();
        }

        public StudentEntity Create(StudentInputEntity studentToCreate)
        {
            if (studentToCreate == null)
            {
                _logger.LogError($"Student repository throws exception.Student you are sending to create is null");

                throw new EntityIsNullException("Student you are sending to create is null");
            }

            Guid newId = Guid.NewGuid();
            StudentEntity studentEntity = MapToEntity(newId, studentToCreate);
            var result = _context.Students.Add(studentEntity);
            _context.SaveChanges();
            return result.Entity;
        }

        private StudentEntity MapToEntity(Guid id, StudentInputEntity studentToMap)
        {
            var lectureEntities = new List<LectureEntity>();

            foreach (Guid guid in studentToMap.LectureAttandance)
            {
                lectureEntities.Add(_context.Lectures.Find(guid));
/*                lectureEntities.Add(_context.Lectures
                    .Include(l => l.StudentsThatAttend)
                    .Include(l => l.SubmitedHomeWorks)
                    .Include(l => l.Course)
                    .Where(l => l.Id == guid)
                    .FirstOrDefault());*/
            }

            var homeWorksEntities = new List<HomeWorkEntity>();
            foreach (Guid guid in studentToMap.AppointedHomeWorksList)
            {
                homeWorksEntities.Add(_context.HomeWorks.Find(guid));
            }

            var course = _context.Courses.Find(studentToMap.Course);
            return new StudentEntity(id, studentToMap.Name, studentToMap.Email, studentToMap.PhoneNumber, course, homeWorksEntities, lectureEntities);
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