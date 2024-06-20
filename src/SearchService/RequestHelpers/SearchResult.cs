﻿namespace SearchService.RequestHelpers;

public class SearchResult<T>
{
    public List<T> Results { get; set; }
    public int PageCount { get; set; }
    public long TotalCount { get; set; }
}
