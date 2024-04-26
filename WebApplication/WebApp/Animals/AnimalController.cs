using Microsoft.AspNetCore.Mvc;

namespace WebApp.Animals;

[Route("api/animals")]
public class AnimalController : ControllerBase
{
    private readonly IAnimalService _service;


    public AnimalController(IAnimalService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAnimals(string orderBy)
    {
        var animals = _service.GetAnimal(orderBy);
        if (animals.Count() > 0)
        {
            return Ok(animals);
        }

        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpPost]
    public IActionResult AddAnimal([FromBody] AnimalDTO animal)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var created = _service.AddAnimal(animal);
        if (created)
        {
            return Created();
        }

        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateAnimal(int id, [FromBody] AnimalDTO animal)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updated = _service.UpdateAnimal(id, animal);
        if (updated)
        {
            return NoContent();
        }

        return NotFound();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteAnimal(int id)
    {
        var deleted = _service.DeleteAnimal(id);
        if (deleted)
        {
            return Ok();
        }

        return NotFound();
    }
}