namespace SharedKernel.Pagination;

public sealed record PaginationOptions(
    int Page = PaginationDefaults.DefaultPageNumber,
    int Size = PaginationDefaults.DefaultPageSize)
{
    public int Page { get; } = Page < 1
        ? PaginationDefaults.DefaultPageNumber 
        : Page;
    public int Size { get; } = Size < 1
        ? PaginationDefaults.DefaultPageSize
        : (Size > PaginationDefaults.MaxPageSize 
            ? PaginationDefaults.MaxPageSize 
            : Size);
}