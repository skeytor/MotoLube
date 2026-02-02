using System.Text.Json.Serialization;

namespace SharedKernel.Pagination;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SortOrder
{
    [JsonStringEnumMemberName("asc")]
    Asc,

    [JsonStringEnumMemberName("desc")]
    Desc
}