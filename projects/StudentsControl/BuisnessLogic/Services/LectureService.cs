namespace BuisnessLogic
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using Domain;

    public class LectureService : ILectureService
    {
        private readonly ILectureRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAttendadanceSupervisingServise _attendanceSupervising;
        private readonly IStudentPerformanceSupervision _studentPerformanceSupervision;
        private readonly IReportGenerator _reportGenerator;
        public LectureService(
            ILectureRepository repository,
            IMapper mapper,
            IAttendadanceSupervisingServise attendadanceSupervising,
            IStudentPerformanceSupervision studentPerformanceSupervision,
            IReportGenerator reportGenerator)
        {
            _repository = repository;
            _mapper = mapper;
            _attendanceSupervising = attendadanceSupervising;
            _studentPerformanceSupervision = studentPerformanceSupervision;
            _reportGenerator = reportGenerator;
        }

        public bool Delete(Guid id)
        {
            var lectureToDelete = _repository.Get(id);
            var studentsToCheck = _mapper.Map<IReadOnlyCollection<StudentModel>>(lectureToDelete.StudentsThatAttend);
            var result = _repository.Delete(id);
            _attendanceSupervising.NotifyIfEligible(studentsToCheck);
            _studentPerformanceSupervision.NotifyIfNecessary(studentsToCheck);
            return result;
        }

        public LectureModel Edit(LectureInputModel lectureToEdit)
        {
            var entity = _mapper.Map<LectureInputEntity>(lectureToEdit);
            var repository = _repository.Edit(entity);
            var studentsToCheck = _mapper.Map<IReadOnlyCollection<StudentModel>>(repository.StudentsThatAttend);
            _attendanceSupervising.NotifyIfEligible(studentsToCheck);
            _studentPerformanceSupervision.NotifyIfNecessary(studentsToCheck);
            return _mapper.Map<LectureModel>(repository);
        }

        public LectureModel Get(Guid id)
        {
            var entity = _repository.Get(id);
            return _mapper.Map<LectureModel>(entity);
        }

        public IReadOnlyCollection<LectureModel> GetAll()
        {
            var entities = _repository.GetAll();
            return _mapper.Map<IReadOnlyCollection<LectureModel>>(entities);
        }

        public LectureModel Create(LectureInputModel lectureToCreate)
        {
            var entity = _mapper.Map<LectureInputEntity>(lectureToCreate);
            var repository = _repository.Create(entity);
            var studentsToCheck = _mapper.Map<IReadOnlyCollection<StudentModel>>(repository.StudentsThatAttend);
            _attendanceSupervising.NotifyIfEligible(studentsToCheck);
            _studentPerformanceSupervision.NotifyIfNecessary(studentsToCheck);
            return _mapper.Map<LectureModel>(repository);
        }

        public string GetJsonReportByLectureID(Guid lectureID)
        {
            var entity = _repository.Get(lectureID);
            var lecture = _mapper.Map<LectureModel>(entity);
            return _reportGenerator.GenerateJsonByLecture(lecture);
        }

        public string GetXmlReportByLectureID(Guid lectureID)
        {
            var entity = _repository.Get(lectureID);
            var lecture = _mapper.Map<LectureModel>(entity);
            var result = _reportGenerator.GenerateXmlByLecture(lecture);
            return result;
        }
    }
}