namespace SharedKernel.Pagination;

public sealed record PagingParameters(
    int Page = PagingOptions.DefaultPageNumber,
    int Size = PagingOptions.DefaultPageSize)
{
    public int Page { get; } = Page < 1
        ? PagingOptions.DefaultPageNumber
        : Page;

    public int Size { get; } = Size < 1
        ? PagingOptions.DefaultPageSize
        : (Size > PagingOptions.MaxPageSize
            ? PagingOptions.MaxPageSize
            : Size);
}