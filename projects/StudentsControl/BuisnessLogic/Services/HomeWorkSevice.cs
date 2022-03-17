namespace BuisnessLogic
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using Domain;

    internal class HomeWorkSevice : IHomeWorkService
    {
        private readonly IHomeWorkRepository _repository;
        private readonly IMapper _mapper;
        private readonly IStudentPerformanceSupervision _studentPerformanceSupervision;
        public HomeWorkSevice(IHomeWorkRepository repository, IMapper mapper, IStudentPerformanceSupervision studentPerformanceSupervision)
        {
            _repository = repository;
            _mapper = mapper;
            _studentPerformanceSupervision = studentPerformanceSupervision;
        }

        public bool Delete(Guid id)
        {
            var homeWorkToDelete = _repository.Get(id);
            var studentToCheck = _mapper.Map<StudentModel>(homeWorkToDelete.AssignedStudent);
            var result = _repository.Delete(id);
            _studentPerformanceSupervision.NotifyIfNecessary(studentToCheck);
            return result;
        }

        public HomeWorkModel Edit(HomeWorkInputModel homeWorkToEdit)
        {
            var entity = _mapper.Map<HomeWorkInputEntity>(homeWorkToEdit);
            var repository = _repository.Edit(entity);
            var studentToCheck = _mapper.Map<StudentModel>(repository.AssignedStudent);
            _studentPerformanceSupervision.NotifyIfNecessary(studentToCheck);
            return _mapper.Map<HomeWorkModel>(repository);
        }

        public HomeWorkModel Get(Guid id)
        {
            var entity = _repository.Get(id);
            return _mapper.Map<HomeWorkModel>(entity);
        }

        public IReadOnlyCollection<HomeWorkModel> GetAll()
        {
            var entities = _repository.GetAll();
            return _mapper.Map<IReadOnlyCollection<HomeWorkModel>>(entities);
        }

        public HomeWorkModel Create(HomeWorkInputModel homeWorkToCreate)
        {
            var entity = _mapper.Map<HomeWorkInputEntity>(homeWorkToCreate);
            var repository = _repository.Create(entity);
            var studentToCheck = _mapper.Map<StudentModel>(repository.AssignedStudent);
            _studentPerformanceSupervision.NotifyIfNecessary(studentToCheck);
            return _mapper.Map<HomeWorkModel>(repository);
        }
    }
}