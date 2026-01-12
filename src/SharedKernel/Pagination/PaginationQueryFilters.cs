namespace SharedKernel.Pagination;

public sealed record PaginationQueryFilters(
    string? SearchTerm = null,
    string? SortColumn = null,
    SortOrder SortOrder = PaginationDefaults.DefaultSortOrder);
