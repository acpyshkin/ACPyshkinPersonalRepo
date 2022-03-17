namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    internal class LectureRepository : ILectureRepository
    {
        private RepositoryContext _context;
        private readonly ILogger<LectureRepository> _logger;
        public LectureRepository(RepositoryContext context, ILogger<LectureRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public bool Delete(Guid id)
        {
            var lectureToDelete = _context.Lectures.Find(id);

            // TODO добавить TryCatch с исключениями
            if (lectureToDelete == null)
            {
                _logger.LogError($"Lecture repository throws exception.Lecture by {id} is not found");
                throw new EntityNotFoundException($"Lecture by {id} is not found");
            }
            else
            {
                _context.Lectures.Remove(lectureToDelete);
                _context.SaveChanges();
                return true;
            }
        }

        public LectureEntity Edit(LectureInputEntity lectureInput)
        {
            if (lectureInput == null)
            {
                _logger.LogError($"Lecture repository throws exception.Lecture you are sending to edit is null");
                throw new EntityIsNullException("Lecture you are sending to edit is null");
            }

            var lectureInDB = _context.Lectures.Find(lectureInput.Id);
            if (lectureInDB == null)
            {
                _logger.LogError($"Lecture repository throws exception.\nLecture by ID: {lectureInput.Id}" +
                    "\nyou are sending to edit does not exist at current DB context");
                throw new EntityNotFoundException($"Lecture by ID: {lectureInput.Id}" +
                    "\nyou are sending to edit does not exist at current DB context");
            }

            LectureEntity lectureToSave = MapToEntity(lectureInput.Id, lectureInput);
            _context.Lectures.Remove(lectureInDB);
            _context.SaveChanges();
            _context.Lectures.Add(lectureToSave);
            /*            _context.Entry(lectureInDB).State = EntityState.Detached;
                        _context.Entry(lectureToSave).State = EntityState.Modified;*/
            _context.SaveChanges();
            return lectureToSave;
        }

        public LectureEntity Get(Guid id)
        {
            if (_context.Lectures.Find(id) == null)
            {
                _logger.LogError($"Lecture repository throws exception.Lecture by ID {id} does not exist at current DB context");
                throw new EntityNotFoundException($"Lecture by ID {id} does not exist at current DB context");
            }

            return _context.Lectures
               .Include(l => l.StudentsThatAttend)
               .Include(l => l.SubmitedHomeWorks)
               .Include(l => l.Course)
               .Where(l => l.Id == id)
               .SingleOrDefault();
        }

        public IReadOnlyCollection<LectureEntity> GetAll()
        {
            if (_context.Lectures == null)
            {
                _logger.LogError($"Lecture repository throws exception.Not a single lecture exist within current DB context"); 
                throw new EntityNotFoundException("Not a single lecture exist within current DB context");
            }

            var result = _context.Lectures
                .Include(l => l.StudentsThatAttend)
                .Include(l => l.Course)
                .Include(h => h.SubmitedHomeWorks)
                .ToList();

            return result;
        }

        public LectureEntity Create(LectureInputEntity lectureToCreate)
        {
            if (lectureToCreate == null)
            {
                _logger.LogError($"Lecture repository throws exception.Lecture you are sending to create is null");

                throw new EntityIsNullException("Lecture you are sending to create is null");
            }

            Guid newId = Guid.NewGuid();
            LectureEntity lectureEntity = MapToEntity(newId,lectureToCreate);
            var result = _context.Lectures.Add(lectureEntity);
            _context.SaveChanges();
            return result.Entity;
        }

        private LectureEntity MapToEntity(Guid id, LectureInputEntity lectureToMap)
        {
            var studentEntities = new List<StudentEntity>();

            // TODO Make LINQ query istead foreach
            foreach (Guid guid in lectureToMap.StudentsThatAttend)
            {
                studentEntities.Add(_context.Students.Find(guid));
            }

            var homeWorksEntities = new List<HomeWorkEntity>();
            foreach (Guid guid in lectureToMap.SubmitedHomeWorks)
            {
                homeWorksEntities.Add(_context.HomeWorks
                    .Include(h => h.RelevantLecture)
                    .Include(h => h.AssignedStudent)
                    .Where(h => h.Id == guid)
                    .FirstOrDefault());
            }

            var course = _context.Courses.Find(lectureToMap.Course);
            return new LectureEntity(id, lectureToMap.Topic, course, studentEntities, homeWorksEntities, lectureToMap.DateOfLecture);
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