// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Controllers.Controllers
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using Domain;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using NLog;

    [Route("api/[controller]")]
    [ApiController]
    public class LectureController : ControllerBase
    {
        private readonly ILectureService _lectureServise;
        private readonly IMapper _mapper;
        private readonly ILogger<LectureController> _logger;
        public LectureController(ILectureService lectureServise, IMapper mapper, ILogger<LectureController> logger )
        {
            _lectureServise = lectureServise;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/<LectureController>
        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogDebug("Collecting all lectures");
            var models = _lectureServise.GetAll();
            var lectures = _mapper.Map<ICollection<LectureDto>>(models);
            if (lectures == null)
            {
                _logger.LogError("No lectures available");
                return NotFound();
            }

            return Ok(lectures);
        }

        // GET api/<LectureController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            _logger.LogDebug("Collecting lecture by ID");
            var model = _lectureServise.Get(id);
            var lecture = _mapper.Map<LectureDto>(model);
            if (lecture == null)
            {
                _logger.LogError("Lecture by input ID is not found");
                return NotFound(lecture);
            }

            return Ok(lecture);
        }

        // POST api/<LectureController>
        [HttpPost]
        public IActionResult Post([FromBody] LectureCreationDto lectureToPost)
        {
            if (lectureToPost.Topic == null)
            {
                _logger.LogError("ERROR! Input lecture's topic is null");
                return BadRequest("Topic could not be null");
            }

            _logger.LogDebug("Creating lecture from user input");
            var model = _mapper.Map<LectureInputModel>(lectureToPost);
            var service = _lectureServise.Create(model);
            var dto = _mapper.Map<LectureDto>(service);
            return Ok(dto);
        }

        // PUT api/<LectureController>/5
        [HttpPut]
        public IActionResult Edit([FromBody] LectureDto lectureToEdit)
        {
            if (lectureToEdit.Topic == null)
            {
                _logger.LogError("ERROR! Input lecture's topic is null");
                return BadRequest("Topic could not be null");
            }

            _logger.LogDebug("Editing lecture from user input");
            var model = _mapper.Map<LectureInputModel>(lectureToEdit);
            var service = _lectureServise.Edit(model);
            var dto = _mapper.Map<LectureDto>(service);

            return Ok(dto);
        }

        // DELETE api/<LectureController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _logger.LogDebug("Deliting lecture by ID");
            var lectureToRemove = _lectureServise.Get(id);
            if (lectureToRemove == null)
            {
                _logger.LogError("Lecture by ID is null");
                return NoContent();
            }

            if (_lectureServise.Delete(id))
            {
                return Ok();
            }

            _logger.LogError("Lecture by ID is not found");
            return NotFound();
        }

        [HttpGet("jsonreport/{lectureID}")]
        public IActionResult GenerateJsonReportByLectureId(Guid lectureID)
        {
            var report = _lectureServise.GetJsonReportByLectureID(lectureID);
            if (report == null)
            {
                _logger.LogError($"Lecture by ID {lectureID} is nor found. Report building failed");
                return NotFound();
            }

            return Ok(report);
        }

        [HttpGet("xmlreport/{lectureID}")]
        public IActionResult GenerateXMLReportByStudentId(Guid lectureID)
        {
            var report = _lectureServise.GetXmlReportByLectureID(lectureID);
            if (report == null)
            {
                _logger.LogError($"Lecture by ID {lectureID} is nor found. Report building failed");
                return NotFound();
            }

            return Ok(report);
        }
    }
}