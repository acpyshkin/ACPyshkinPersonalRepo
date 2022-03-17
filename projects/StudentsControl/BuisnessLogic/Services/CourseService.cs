namespace BuisnessLogic
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using Domain;

    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAttendadanceSupervisingServise _attendadanceSupervising;
        private readonly IStudentPerformanceSupervision _performanceSupervision;
        public CourseService(ICourseRepository repository, IMapper mapper, IAttendadanceSupervisingServise attendadanceSupervising, IStudentPerformanceSupervision performanceSupervision)
        {
            _repository = repository;
            _mapper = mapper;
            _attendadanceSupervising = attendadanceSupervising;
            _performanceSupervision = performanceSupervision;
        }

        public bool Delete(Guid id)
        {
            var courseToDelete = _repository.Get(id);
            var studentsToCheck =_mapper.Map<IReadOnlyCollection<StudentModel>>(courseToDelete.AppointedStudentsList);
            var result = _repository.Delete(id);
            _attendadanceSupervising.NotifyIfEligible(studentsToCheck);
            _performanceSupervision.NotifyIfNecessary(studentsToCheck);
            return result;
        }

        public CourseModel Edit(CourseInputModel lectureToEdit)
        {
            var entity = _mapper.Map<CourseInputEntity>(lectureToEdit);
            var repository = _repository.Edit(entity);
            var studentsToCheck = _mapper.Map<IReadOnlyCollection<StudentModel>>(repository.AppointedStudentsList);
            _attendadanceSupervising.NotifyIfEligible(studentsToCheck);
            _performanceSupervision.NotifyIfNecessary(studentsToCheck);
            return _mapper.Map<CourseModel>(repository);
        }

        public CourseModel Get(Guid id)
        {
            var entity = _repository.Get(id);
            return _mapper.Map<CourseModel>(entity);
        }

        public IReadOnlyCollection<CourseModel> GetAll()
        {
            var Entities = _repository.GetAll();
            return _mapper.Map<IReadOnlyCollection<CourseModel>>(Entities);
        }

        public CourseModel Create(CourseInputModel lectureToCreate)
        {
            var entity = _mapper.Map<CourseInputEntity>(lectureToCreate);
            var repository = _repository.Create(entity);
            var studentsToCheck = _mapper.Map<IReadOnlyCollection<StudentModel>>(repository.AppointedStudentsList);
            _attendadanceSupervising.NotifyIfEligible(studentsToCheck);
            _performanceSupervision.NotifyIfNecessary(studentsToCheck);
            return _mapper.Map<CourseModel>(repository);
        }
    }
}