using SearchService.Models;
using SearchService.RequestHelpers;

namespace SearchService.Data;

public interface ISearchRepo
{
    Task<SearchResult<Item>> SearchItemsAsync(SearchParams searchParams);
}
