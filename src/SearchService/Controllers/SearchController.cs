using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using SearchService.Data;
using SearchService.Models;
using SearchService.RequestHelpers;
using System.Xml.XPath;

namespace SearchService.Controllers;
[ApiController]
[Route("api/search")]
public class SearchController:ControllerBase
{
    private readonly ISearchRepo _searchRepo;
    public SearchController(ISearchRepo searchRepo)
    {
        _searchRepo = searchRepo;
    }
    [HttpGet]
    public async Task<ActionResult<List<Item>>> SearchItems([FromQuery] SearchParams searchParams)
    {

        if(!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _searchRepo.SearchItemsAsync(searchParams);

        return Ok(new 
        {
            results = result.Results,
            pageCount = result.PageCount,
            totalCount = result.TotalCount
        }
        );
    }
}
