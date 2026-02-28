namespace SharedKernel.Pagination;

public sealed record PagingFilters(
    string? SearchTerm = null,
    string? SortColumn = null,
    SortOrder SortOrder = PagingOptions.DefaultSortOrder);