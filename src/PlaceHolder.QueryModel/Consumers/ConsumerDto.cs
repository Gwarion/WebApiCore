namespace PlaceHolder.QueryModel.Consumers;

public sealed record ConsumerDto(
    Guid Guid,
    string FirstName,
    string LastName,
    string PhoneNumber, 
    string Email,
    string Address) { }
