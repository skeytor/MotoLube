using System.Text.Json.Serialization;

namespace SharedKernel.Pagination;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SortOrder
{
    [JsonStringEnumMemberName("asc")]
    Ascending,

    [JsonStringEnumMemberName("desc")]
    Descending
}