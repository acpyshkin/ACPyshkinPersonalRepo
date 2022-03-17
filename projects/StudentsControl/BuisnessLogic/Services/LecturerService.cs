namespace BuisnessLogic
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using Domain;

    public class LecturerService : ILecturerService
    {
        private readonly ILecturerRepository _repository;
        private readonly IMapper _mapper;
        public LecturerService(ILecturerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public bool Delete(Guid id)
        {
            return _repository.Delete(id);
        }

        public LecturerModel Edit(LecturerInputModel lectureToEdit)
        {
            var entity = _mapper.Map<LecturerInputEntity>(lectureToEdit);
            var repository = _repository.Edit(entity);
            return _mapper.Map<LecturerModel>(repository);
        }

        public LecturerModel Get(Guid id)
        {
            var entity = _repository.Get(id);
            return _mapper.Map<LecturerModel>(entity);
        }

        public IReadOnlyCollection<LecturerModel> GetAll()
        {
            var entities = _repository.GetAll();
            return _mapper.Map<IReadOnlyCollection<LecturerModel>>(entities);
        }

        public LecturerModel Create(LecturerInputModel lectureToCreate)
        {
            var entity = _mapper.Map<LecturerInputEntity>(lectureToCreate);
            var repository = _repository.Create(entity);
            return _mapper.Map<LecturerModel>(repository);
        }
    }
}