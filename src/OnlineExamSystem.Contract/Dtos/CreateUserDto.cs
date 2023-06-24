namespace OnlineExamSystem.Common.Dtos;
public record CreateUserDto(
    string Name,
    int Age,
    string Username,
    string Email,
    string Password,
    string PhoneNumber
    );
