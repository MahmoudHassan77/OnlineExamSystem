namespace OnlineExamSystem.Common.Dtos;
public record BaseResponse(
     string Id,
     string Message,
     List<string> Errors,
     bool Success = true
);