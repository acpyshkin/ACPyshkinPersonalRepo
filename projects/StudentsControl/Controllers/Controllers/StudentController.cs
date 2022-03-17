// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Controllers.Controllers
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using Domain;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentServise;
        private readonly IMapper _mapper;
        private readonly ILogger<StudentController> _logger;
        public StudentController(IStudentService studentServise, IMapper mapper, ILogger<StudentController> logger)
        {
            _studentServise = studentServise;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/<StudentController>
        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogDebug("Collecting all students");
            var students = _studentServise.GetAll();
            if (students == null)
            {
                _logger.LogError("No students available");
                return NotFound();
            }

            return Ok(_mapper.Map<IReadOnlyCollection<StudentDto>>(students));
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            _logger.LogDebug("Collecting student by ID");
            var student = _studentServise.Get(id);
            if (student == null)
            {
                _logger.LogError("Student by input ID is not found");
                return NotFound(student);
            }

            return Ok(_mapper.Map<StudentDto>(student));
        }

        // POST api/<StudentController>
        [HttpPost]
        public IActionResult Post([FromBody] StudentCreationDto studentToPost)
        {
            _logger.LogDebug("Creating student from user input");
            var model = _mapper.Map<StudentInputModel>(studentToPost);
            var service = _studentServise.Create(model);
            var dto = _mapper.Map<StudentDto>(service);

            return Ok(dto);
        }

        // PUT api/<StudentController>/5
        [HttpPut]
        public IActionResult Edit([FromBody] StudentDto studentToEdit)
        {
            _logger.LogDebug("Editing student from user input");
            var model = _mapper.Map<StudentInputModel>(studentToEdit);
            var service = _studentServise.Edit(model);
            var dto = _mapper.Map<StudentDto>(service);

            return Ok(dto);
        }

        // DELETE api/<LectureController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _logger.LogDebug("Deliting student by ID");
            var studentToRemove = _studentServise.Get(id);
            if (studentToRemove == null)
            {
                _logger.LogError("Student by ID is null");
                return NoContent();
            }

            if (_studentServise.Delete(id))
            {
                return Ok();
            }

            _logger.LogError("Student by ID is not found");
            return NotFound();
        }

        [HttpGet("jsonreport/{studentID}")]
        public IActionResult GenerateJsonReportByStudentId(Guid studentID)
        {
            var report = _studentServise.GetJsonReportByStudentID(studentID);
            if (report == null)
            {
                _logger.LogError($"Student by ID {studentID} is nor found. Report building failed");
                return NotFound();
            }

            return Ok(report);
        }

        [HttpGet("xmlreport/{studentID}")]
        public IActionResult GenerateXMLReportByStudentId(Guid studentID)
        {
            var report = _studentServise.GetXmlReportByStudentId(studentID);
            if (report == null)
            {
                _logger.LogError($"Student by ID {studentID} is nor found. Report building failed");
                return NotFound();
            }

            return Ok(report);
        }
    }
}