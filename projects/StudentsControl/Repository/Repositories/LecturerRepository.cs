namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    internal class LecturerRepository : ILecturerRepository
    {
        private readonly ILogger<LecturerRepository> _logger;
        private RepositoryContext _context;

        public LecturerRepository(RepositoryContext context, ILogger<LecturerRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public bool Delete(Guid id)
        {
            var lecturerToDelete = _context.Lectures.Find(id);

            // TODO добавить TryCatch с исключениями
            if (lecturerToDelete == null)
            {
                _logger.LogError($"Lecturer repository throws exception.Lecture by {id} is not found");
                throw new EntityNotFoundException($"Lecturer by {id} is not found");
            }
            else
            {
                _context.Lectures.Remove(lecturerToDelete);
                _context.SaveChanges();
                return true;
            }
        }

        public LecturerEntity Edit(LecturerInputEntity lecturerToEdit)
        {
            if (lecturerToEdit == null)
            {
                _logger.LogError($"Lecturer repository throws exception.Lecturer you are sending to edit is null");
                throw new EntityIsNullException("Lecturer you are sending to edit is null");
            }

            var lecturerInDB = _context.Lecturers.Find(lecturerToEdit.Id);
            if (lecturerInDB == null)
            {
                _logger.LogError($"Lecturer repository throws exception.\nLecturer by ID: {lecturerToEdit.Id}" +
                    "\nyou are sending to edit does not exist at current DB context");
                throw new EntityNotFoundException($"Lecturer by ID: {lecturerToEdit.Id}" +
                    "\nyou are sending to edit does not exist at current DB context");
            }

            var lecturerToSave = MapToEntity(lecturerToEdit.Id, lecturerToEdit);
/*            _context.Lecturers.Remove(lecturerInDB);
            _context.Lecturers.Add(lecturerToSave);*/
            _context.Entry(lecturerInDB).State = EntityState.Detached;
            _context.Entry(lecturerToSave).State = EntityState.Modified;
            _context.SaveChanges();
            return lecturerToSave;
        }

        public LecturerEntity Get(Guid id)
        {
            if (_context.Lecturers.Find(id) == null)
            {
                _logger.LogError($"Lecturer repository throws exception.Lecturer by ID {id} does not exist at current DB context");
                throw new EntityNotFoundException($"Lecturer by ID {id} does not exist at current DB context");
            }

            return _context.Lecturers.Find(id);
        }

        public IReadOnlyCollection<LecturerEntity> GetAll()
        {
            if (_context.Lecturers == null)
            {
                _logger.LogError($"Lecturer repository throws exception.Not a single lecturer exist within current DB context");
                throw new EntityNotFoundException("Not a single lecturer exist within current DB context");
            }

            return _context.Lecturers.ToList();
        }

        public LecturerEntity Create(LecturerInputEntity lecturerInput)
        {
            if (lecturerInput == null)
            {
                _logger.LogError($"Lecturer repository throws exception.Lecturer you are sending to create is null");

                throw new EntityIsNullException("Lecturer you are sending to create is null");
            }

            Guid newId = Guid.NewGuid();
            var entity = MapToEntity(newId, lecturerInput);
            var result = _context.Lecturers.Add(entity);
            _context.SaveChanges();
            return result.Entity;
        }

        private LecturerEntity MapToEntity(Guid id, LecturerInputEntity lecturerToMap)
        {
            var course = _context.Courses.Find(lecturerToMap.Course);
            var entity = new LecturerEntity { Id = id, Name = lecturerToMap.Name, Email = lecturerToMap.Email, Course = course };
            return entity;
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