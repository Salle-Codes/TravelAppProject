using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DbModels;
using DbContext;
using System.Linq;
using System.Threading.Tasks;
using System;
using Services;

[ApiController]
[Route("api/[controller]")]
public class AttractionsController : Controller
{
    readonly IAttractionService _service;
    readonly ILogger<AttractionsController> _logger;

    public AttractionsController(IAttractionService service, ILogger<AttractionsController> logger)
    {
        _service = service;
        _logger = logger;
    }

    // 1. Visa alla sevärdheter filtrerade på kategori, rubrik, beskrivning, land, och ort
    [HttpGet("Filter")]
    [ActionName("GetFiltered")]
    public async Task<IActionResult> GetFilteredAttractions([FromQuery] bool seeded = false, [FromQuery] bool flat = false, [FromQuery] string filtered = "")
    {
        try
        {
            var attractions = await _service.GetFilteredAttractionsAsync(seeded, flat, filtered);
            return Ok(attractions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving filtered attractions");
            return BadRequest(ex.Message);
        }
    }

    // 2. Visa alla sevärdheter som inte har någon kommentar
    [HttpGet("No-comments")]
    [ActionName("GetNoComments")]
    public async Task<IActionResult> GetAttractionsWithoutComments([FromQuery] bool flat = false)
    {
        try
        {
            var attractions = await _service.GetAttractionsWithoutCommentsAsync(flat);
            return Ok(attractions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving attractions without comments");
            return BadRequest(ex.Message);
        }
    }


    // 3. Visa en sevärdhets kategori, rubrik, beskrivning, och alla kommentarer
    [HttpGet("comments")]
    [ActionName("GetWithComments")]
    public async Task<IActionResult> GetAttractionWithCommentsAsync([FromQuery] Guid id, [FromQuery] bool flat = false)
    {
        try
        {
            var attraction = await _service.GetAttractionWithCommentsAsync(id, flat);
            if (attraction == null)
            {
                return NotFound($"Attraction with ID {id} not found.");
            }
            return Ok(attraction);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error retrieving attraction with ID {id}");
            return BadRequest(ex.Message);
        }
    }




}