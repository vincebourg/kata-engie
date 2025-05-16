using Microsoft.AspNetCore.Mvc;

namespace back;

[Route("api")]
[ApiController]
public class BuildingsController : Controller
{

    [HttpGet("buildings.geojson")]
    public IActionResult GetBuildings()
    {
        throw new NotImplementedException();
    }
}