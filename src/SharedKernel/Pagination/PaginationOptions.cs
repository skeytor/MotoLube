namespace SharedKernel.Pagination;

public sealed record PaginationOptions(
    int Page = PaginationDefaults.DefaultPageNumber,
    int Size = PaginationDefaults.DefaultPageSize)
{
    public int Page { get; init; } = Page < 1
        ? PaginationDefaults.DefaultPageNumber 
        : Page;
    public int Size { get; init; } = Size < 1
        ? PaginationDefaults.DefaultPageSize
        : (Size > PaginationDefaults.MaxPageSize 
            ? PaginationDefaults.MaxPageSize 
            : Size);
}