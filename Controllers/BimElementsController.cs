using Microsoft.AspNetCore.Mvc;
using BimApi.Models;
using BimApi.Repositories;
using System.ComponentModel.DataAnnotations;

namespace BimApi.Controllers
{
    [ApiController]
    [Route("api/elements")]
    public class BimElementsController : ControllerBase
    {
        private readonly BimElementRepository _repository;

        public BimElementsController(BimElementRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_repository.GetAll());

        [HttpGet("{ifcGuid}")]
        public IActionResult Get(string ifcGuid)
        {
            var element = _repository.Get(ifcGuid);
            return element == null ? NotFound() : Ok(element);
        }

        [HttpPost]
        public IActionResult Create([FromBody] BimElement element)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_repository.Add(element))
                return Conflict("Element with the same IfcGuid already exists.");

            return CreatedAtAction(nameof(Get), new { ifcGuid = element.IfcGuid }, element);
        }

        public class UpdateProgressDto
        {
            [Range(0, 100)]
            public int ProgressPercent { get; set; }
        }

        [HttpPut("{ifcGuid}/progress")]
        public IActionResult UpdateProgress(string ifcGuid, [FromBody] UpdateProgressDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = _repository.UpdateProgress(ifcGuid, dto.ProgressPercent);
            return updated ? NoContent() : NotFound();
        }
    }
}
