namespace BuisnessLogic
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using Domain;

    internal class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAttendadanceSupervisingServise _attendadanceSupervising;
        private readonly IStudentPerformanceSupervision _studentPerformanceSupervision;
        private readonly IReportGenerator _reportGenerator;
        public StudentService(
            IStudentRepository repository,
            IMapper mapper,
            IAttendadanceSupervisingServise attendadanceSupervising,
            IStudentPerformanceSupervision studentPerformanceSupervision,
            IReportGenerator reportGenerator)
        {
            _repository = repository;
            _mapper = mapper;
            _attendadanceSupervising = attendadanceSupervising;
            _studentPerformanceSupervision = studentPerformanceSupervision;
            _reportGenerator = reportGenerator;
        }

        public bool Delete(Guid id)
        {
            var student = _mapper.Map<StudentModel>(_repository.Get(id));
            var result = _repository.Delete(id);
            return result;
        }

        public StudentModel Edit(StudentInputModel StudentToEdit)
        {
            var entity = _mapper.Map<StudentInputEntity>(StudentToEdit);
            var repository = _repository.Edit(entity);
            var student = _mapper.Map<StudentModel>(repository);
            _attendadanceSupervising.NotifyIfEligible(student);
            _studentPerformanceSupervision.NotifyIfNecessary(student);
            return student;
        }

        public StudentModel Get(Guid id)
        {
            var entity = _repository.Get(id);
            return _mapper.Map<StudentModel>(entity);
        }

        public IReadOnlyCollection<StudentModel> GetAll()
        {
            var entities = _repository.GetAll();
            return _mapper.Map<IReadOnlyCollection<StudentModel>>(entities);
        }

        public StudentModel Create(StudentInputModel studentToCreate)
        {
            var entity = _mapper.Map<StudentInputEntity>(studentToCreate);
            var repository = _repository.Create(entity);
            var student = _mapper.Map<StudentModel>(repository);
            _attendadanceSupervising.NotifyIfEligible(student);
            _studentPerformanceSupervision.NotifyIfNecessary(student);
            return student;
        }

        public string GetJsonReportByStudentID(Guid studentID)
        {
            var entity = _repository.Get(studentID);
            var student = _mapper.Map<StudentModel>(entity);
            return _reportGenerator.GenerateJsonByStudent(student);
        }

        public string GetXmlReportByStudentId(Guid studentID)
        {
            var entity = _repository.Get(studentID);
            var student = _mapper.Map<StudentModel>(entity);
            return _reportGenerator.GenerateXmlByStudent(student);
        }
    }
}