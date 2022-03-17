namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    internal class HomeWorkRepository : IHomeWorkRepository
    {
        private readonly ILogger<HomeWorkRepository> _logger;
        private RepositoryContext _context;
        public HomeWorkRepository(RepositoryContext context, ILogger<HomeWorkRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public bool Delete(Guid id)
        {
            var homeWorkToDelete = _context.HomeWorks.Find(id);

            // TODO добавить TryCatch с исключениями
            if (homeWorkToDelete == null)
            {
                _logger.LogError($"Homework repository throws exception. Homework by {id} is not found");
                throw new EntityNotFoundException($"Homework by {id} is not found");
            }
            else
            {
                _context.HomeWorks.Remove(homeWorkToDelete);
                _context.SaveChanges();
                return true;
            }
        }

        public HomeWorkEntity Edit(HomeWorkInputEntity homeWorkToEdit)
        {
            if (homeWorkToEdit == null)
            {
                _logger.LogError($"Homework repository throws exception. Homework you are sending to edit is null");
                throw new EntityIsNullException("Homework you are sending to edit is null");
            }

            var homeworkInDB = _context.HomeWorks.Find(homeWorkToEdit.Id);
            if (homeworkInDB == null)
            {
                _logger.LogError($"Homework repository throws exception. Homework by ID: {homeWorkToEdit.Id}" +
                    "\nyou are sending to edit does not exist at current DB context");
                throw new EntityNotFoundException($"Homework by ID: {homeWorkToEdit.Id}" +
                    "\nyou are sending to edit does not exist at current DB context");
            }

            var entity = MapToEntity(homeWorkToEdit.Id, homeWorkToEdit);
            /*            _context.HomeWorks.Remove(homeworkInDB);
                        _context.HomeWorks.Add(entity);
                        _context.SaveChanges();*/
            _context.Entry(homeworkInDB).State = EntityState.Detached;
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return entity;
        }

        public HomeWorkEntity Get(Guid id)
        {
            if (_context.HomeWorks.Find(id) == null)
            {
                _logger.LogError($"Homework repository throws exception.Homework by ID {id} does not exist at current DB context");
                throw new EntityNotFoundException($"Homework by ID {id} does not exist at current DB context");
            }

            return _context.HomeWorks
                .Include(h => h.AssignedStudent)
                .Include(h => h.RelevantLecture)
                .Where(x => x.Id == id).SingleOrDefault();
        }

        public IReadOnlyCollection<HomeWorkEntity> GetAll()
        {
            if (_context.HomeWorks == null)
            {
                _logger.LogError($"Homework repository throws exception.Not a single Homework exist within current DB context");
                throw new EntityNotFoundException("Not a single Homework exist within current DB context");
            }

            return _context.HomeWorks
                .Include(h => h.AssignedStudent)
                .Include(h => h.RelevantLecture)
                .ToList();
        }

        public HomeWorkEntity Create(HomeWorkInputEntity homeWorkToCreate)
        {
            if (homeWorkToCreate == null)
            {
                _logger.LogError($"Homework repository throws exception.Homework you are sending to create is null");

                throw new EntityIsNullException("Homework you are sending to create is null");
            }

            Guid newId = Guid.NewGuid();
            var entity = MapToEntity(newId, homeWorkToCreate);
            var result = _context.HomeWorks.Add(entity);
            _context.SaveChanges();
            return result.Entity;
        }

        private HomeWorkEntity MapToEntity(Guid id, HomeWorkInputEntity homeworkToMap)
        {
            var student = _context.Students.Find(homeworkToMap.AssignedStudent);
            var lecture = _context.Lectures.Find(homeworkToMap.RelevantLecture);
            var entity = new HomeWorkEntity { Id = id, Mark = homeworkToMap.Mark, AssignedStudent = student, RelevantLecture = lecture };
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