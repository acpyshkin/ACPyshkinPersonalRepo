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
    public class LecturerController : ControllerBase
    {
        private readonly ILecturerService _lecturerServise;
        private readonly IMapper _mapper;
        private readonly ILogger<LectureController> _logger;
        public LecturerController(ILecturerService lecturerServise, IMapper mapper, ILogger<LectureController> logger)
        {
            _lecturerServise = lecturerServise;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/<LecturerController>
        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogDebug("Collecting all lecturers");
            var lecturers = _lecturerServise.GetAll();
            if (lecturers == null)
            {
                _logger.LogError("No lecturers available");
                return NotFound();
            }

            return Ok(_mapper.Map<IReadOnlyCollection<LecturerDto>>(lecturers));
        }

        // GET api/<LecturerController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            _logger.LogDebug("Collecting lecturer by ID");
            var lecturer = _lecturerServise.Get(id);
            if (lecturer == null)
            {
                _logger.LogError("Lecturer by input ID is not found");
                return NotFound(lecturer);
            }

            return Ok(_mapper.Map<LecturerDto>(lecturer));
        }

        // POST api/<LecturerController>
        [HttpPost]
        public IActionResult Post([FromBody] LecturerCreationDto lectureToPost)
        {
            _logger.LogDebug("Creating lecturer from user input");
            var model = _mapper.Map<LecturerInputModel>(lectureToPost);
            var service = _lecturerServise.Create(model);
            var dto = _mapper.Map<LecturerDto>(service);
            return Ok(dto);
        }

        // PUT api/<LecturerController>/5
        [HttpPut]
        public IActionResult Edit([FromBody] LecturerDto lectureToEdit)
        {
            _logger.LogDebug("Editing lecturer from user input");
            var model = _mapper.Map<LecturerInputModel>(lectureToEdit);
            var service = _lecturerServise.Edit(model);
            var dto = _mapper.Map<LecturerDto>(service);

            return Ok(dto);
        }

        // DELETE api/<LecturerController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _logger.LogDebug("Deliting lecturer by ID");
            var lectureToRemove = _lecturerServise.Get(id);
            if (lectureToRemove == null)
            {
                _logger.LogError("Lecturer by ID is null");
                return NoContent();
            }

            if (_lecturerServise.Delete(id))
            {
                return Ok();
            }

            _logger.LogError("Lecturer by ID is not found");
            return NotFound();
        }
    }
}