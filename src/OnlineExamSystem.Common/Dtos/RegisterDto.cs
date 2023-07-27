namespace OnlineExamSystem.Common.Dtos;
public record RegisterDto(
        string Email, 
        string Password,
        string PhoneNumber,
        string Username,
        string Name,
        int Age
    );
