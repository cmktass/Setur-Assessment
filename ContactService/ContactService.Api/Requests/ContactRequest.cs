namespace ContactService.Api.Requests
{
    public record CreateContactRequest(string FirstName, string LastName, string Company);
    public record CreateContactInfoRequest(Guid PersonId, int ContactTypeId, string Value);
}
