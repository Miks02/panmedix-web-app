namespace PanMedix.DTO.Global;

public class PagedResult<T>
{
    public int PageSize { get; set; }
    public int Page { get; set; }
    public int PaginatedCount { get; set; }
    public IReadOnlyList<T> Items { get; set; } 
    
    public PagedResult(int page, int pageSize, IReadOnlyList<T> items)
    {
        Page = page;
        PageSize = pageSize;
        PaginatedCount = items.Count;
        Items = items;
    }
    
}