using Microsoft.AspNetCore.Mvc;

[Route("/api/person/")]
[ApiController]

public class PersonController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> GetPeople(IPersonRepository personRepository)
    {
        IEnumerable<Person> people = await personRepository.GetAllPersonsAsync();
        if (people == null)
        {
            return Results.NotFound();
        }
        return Results.Ok(people);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> CreatePerson(IPersonRepository personRepository, [FromBody] Person? person)
    {
        if (person == null)
        {
            return Results.BadRequest("Person is null");
        }
        bool res = await personRepository.CreatePersonAsync(person);
        if (res == false)
        {
            return Results.BadRequest("Person not created");
        }
        return Results.Ok("Person created");
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> DeletePerson(IPersonRepository personRepository, [FromRoute] Guid? id)
    {
        if (id == null)
        {
            return Results.BadRequest("Id is null.");
        }
        bool res = await personRepository.DeletePersonAsync((Guid)id);
        if (res == false)
        {
            return Results.BadRequest("Person was not deleted.");
        }
        return Results.Ok("Person deleted!");
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> GetById(IPersonRepository personRepository, [FromRoute] Guid? id)
    {
        if (id == null)
        {
            return Results.BadRequest("Id is null.");
        }
        Person? person = await personRepository.GetPersonByIdAsync((Guid)id);
        if (person == null)
        {
            return Results.NotFound("Person was not found");
        }
        return Results.Ok(person);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> UpdatePerson(IPersonRepository personRepository, [FromBody]Person? person)
    {
        if(person==null)
        {
            return Results.BadRequest("Person object is null");
        }
        bool res = await personRepository.UpdatePersonAsync(person);
        if(res==false)
        {
            return Results.BadRequest("Person was not updated");
        }
        return Results.Ok("Person Updated"); 
    }
}