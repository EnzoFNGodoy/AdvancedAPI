using AdvancedApi.Domain.Enums;

namespace AdvancedApi.WebApi.Helpers;

public class UserQueryParameters : QueryParameters
{
    public string? Like { get; set; }
    public DateTime? MinCreatedAtDate { get; set; }
    public DateTime? MaxCreatedAtDate { get; set; }
    public DateTime? MinUpdatedAtDate { get; set; }
    public DateTime? MaxUpdatedAtDate { get; set; }
    public EStatus? Status { get; set; }
}