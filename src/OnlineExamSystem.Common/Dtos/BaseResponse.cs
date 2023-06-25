namespace OnlineExamSystem.Common.Dtos;
public record BaseResponse(
     string Message,
     List<string> Errors,
     bool Success = true
);