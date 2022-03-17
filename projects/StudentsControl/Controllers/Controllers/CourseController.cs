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
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _lecturerServise;
        private readonly IMapper _mapper;
        private readonly ILogger<CourseController> _logger;
        public CourseController(ICourseService lecturerServise, IMapper mapper, ILogger<CourseController> logger)
        {
            _lecturerServise = lecturerServise;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/<CourseController>
        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogDebug("Collecting all courses");
            var courses = _lecturerServise.GetAll();
            if (courses == null)
            {
                _logger.LogError("No courses available");
                return NotFound();
            }

            return Ok(_mapper.Map<IReadOnlyCollection<CourseDto>>(courses));
        }

        // GET api/<CourseController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            _logger.LogDebug("Collecting course by ID");
            var course = _lecturerServise.Get(id);
            if (course == null)
            {
                _logger.LogError("Course is not found");
                return NotFound(course);
            }

            return Ok(_mapper.Map<CourseDto>(course));
        }

        // POST api/<CourseController>
        [HttpPost]
        public IActionResult Post([FromBody] CourseCreationDto courseToPost)
        {
            if (courseToPost.Name == null)
            {
                _logger.LogError("ERROR! Input course name is null");
                return BadRequest("Name could not be null");
            }

            _logger.LogDebug("Creating course from user input");
            var model = _mapper.Map<CourseInputModel>(courseToPost);
            var service = _lecturerServise.Create(model);
            var dto = _mapper.Map<CourseDto>(service);
            return Ok(dto);
        }

        // PUT api/<CourseController>/5
        [HttpPut]
        public IActionResult Edit([FromBody] CourseDto courseToEdit)
        {
            if (courseToEdit.Name == null)
            {
                _logger.LogError("ERROR! Input course name is null");
                return BadRequest("Name could not be null");
            }

            _logger.LogDebug("Editing course from user input");
            var model = _mapper.Map<CourseInputModel>(courseToEdit);
            var service = _lecturerServise.Edit(model);
            var dto = _mapper.Map<CourseDto>(service);

            return Ok(dto);
        }

        // DELETE api/<CourseController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _logger.LogDebug("Deliting course by ID");
            var lecturerToRemove = _lecturerServise.Get(id);
            if (lecturerToRemove == null)
            {
                _logger.LogError("Course by ID is null");
                return NoContent();
            }

            if (_lecturerServise.Delete(id))
            {
                return Ok();
            }

            _logger.LogError("Course by ID is not found");
            return NotFound();
        }
    }
}