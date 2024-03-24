using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BuggyController : BaseApiController
{
    private readonly StoreContext _context;

    public BuggyController(StoreContext context)
    {
        _context = context;
    }

    [HttpGet("notfound")]
    public ActionResult NotFound()
    {
        var thing = _context.Products.Find(3456);

        if (thing is null) return NotFound(new ApiResponse(404));

        return Ok();
    }

    [HttpGet("servererror")]
    public ActionResult ServerError()
    {
        var thing = _context.Products.Find(3456);

        var thingToReturn = thing.ToString();

        return Ok();
    }

    [HttpGet("badrequest")]
    public ActionResult GetBadRequest()
    {
        return BadRequest(new ApiResponse(400));
    }

    [HttpGet("badrequest/{id}")]
    public ActionResult GetBadRequest(int id)
    {
        return BadRequest();
    }
}
