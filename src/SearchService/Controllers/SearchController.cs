using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using SearchService.Models;
using System.Xml.XPath;

namespace SearchService.Controllers;
[ApiController]
[Route("api/search")]
public class SearchController:ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Item>>> SearchItems(string? searchTerm, int pageNum = 1, int pageSize = 4)
    {
        var query = DB.PagedSearch<Item>();

        query.Sort(x => x.Ascending(a => a.Make));

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query.Match(Search.Full, searchTerm).SortByTextScore();
        }
        query.PageNumber(pageNum);
        query.PageSize(pageSize); 

        var result = await query.ExecuteAsync();

        return Ok(new 
        {
            results = result.Results,
            pageCount = result.PageCount,
            totalCount = result.TotalCount
        }
        );
    }
}
