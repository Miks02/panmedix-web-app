namespace PanMedix.DTO.Global;

public class PagedResult<T>
{
    public int PageSize { get; set; }
    public int Page { get; set; }
    public int PaginatedCount { get; set; }
    public int TotalCount { get; set; }
    public double TotalPages => Math.Ceiling((double)TotalCount / PageSize);
    public IReadOnlyList<T> Items { get; set; } 
    
    public PagedResult(int page, int pageSize, IReadOnlyList<T> items, int totalCount)
    {
        Page = page;
        PageSize = pageSize;
        PaginatedCount = items.Count;
        TotalCount = totalCount;
        Items = items;
    }
    
}