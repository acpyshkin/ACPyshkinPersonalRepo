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
    public class HomeWorkController : ControllerBase
    {
        private readonly IHomeWorkService _homeWorkServise;
        private readonly IMapper _mapper;
        private readonly ILogger<HomeWorkController> _logger;
        public HomeWorkController(IHomeWorkService homeWorkServise, IMapper mapper, ILogger<HomeWorkController> logger)
        {
            _homeWorkServise = homeWorkServise;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/<LecturerController>
        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogDebug("Collecting all homeworks");
            var homeWorks = _homeWorkServise.GetAll();
            if (homeWorks == null)
            {
                _logger.LogError("No homeworks available");
                return NotFound();
            }

            return Ok(_mapper.Map<IReadOnlyCollection<HomeWorkDto>>(homeWorks));
        }

        // GET api/<HomeWorkController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            _logger.LogDebug("Collecting homework by ID");
            var homeWork = _homeWorkServise.Get(id);
            if (homeWork == null)
            {
                _logger.LogError("Homework by input ID is not found");
                return NotFound(homeWork);
            }

            return Ok(_mapper.Map<HomeWorkDto>(homeWork));
        }

        // POST api/<HomeWorkController>
        [HttpPost]
        public IActionResult Post([FromBody] HomeWorkCreationDto homeWorkToPost)
        {
            _logger.LogDebug("Creating homework from user input");
            var model = _mapper.Map<HomeWorkInputModel>(homeWorkToPost);
            var service = _homeWorkServise.Create(model);
            var dto = _mapper.Map<HomeWorkDto>(service);
            return Ok(dto);
        }

        // PUT api/<HomeWorkController>/5
        [HttpPut]
        public IActionResult Edit([FromBody] HomeWorkDto homeWorkToEdit)
        {
            _logger.LogDebug("Editing homework from user input");
            var model = _mapper.Map<HomeWorkInputModel>(homeWorkToEdit);
            var service = _homeWorkServise.Edit(model);
            var dto = _mapper.Map<HomeWorkDto>(service);

            return Ok(dto);
        }

        // DELETE api/<LectureController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _logger.LogDebug("Deliting homework by ID");
            var lecturerToRemove = _homeWorkServise.Get(id);
            if (lecturerToRemove == null)
            {
                _logger.LogError("Homework by ID is null");
                return NoContent();
            }

            if (_homeWorkServise.Delete(id))
            {
                return Ok();
            }

            _logger.LogError("Homework by ID is not found");
            return NotFound();
        }
    }
}