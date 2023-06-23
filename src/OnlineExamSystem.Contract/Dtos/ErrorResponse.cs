namespace OnlineExamSystem.Common.Dtos;
public record ErrorResponse(
    List<string> ErrorMessage,
    string ErrorType,
    int? code
    );
