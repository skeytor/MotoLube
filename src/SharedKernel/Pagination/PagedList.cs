namespace SharedKernel.Pagination;

public sealed class PagedList<T> where T : class
{
    private PagedList(IReadOnlyCollection<T> items, int page, int size, int totalCount)
    {
        Items = items;
        TotalCount = totalCount;
        Page = page;
        Size = size;
    }

    public IReadOnlyCollection<T> Items { get; }
    public int TotalCount { get; }
    public int Page { get; }
    public int Size { get; }
    public bool HasNextPage => Page * Size < TotalCount;
    public bool HasPreviousPage => Page > 1;

    public static PagedList<T> Create(IReadOnlyCollection<T> items, int page, int size, int totalCount) =>
        new(items, page, size, totalCount);
}