namespace OnlineExamSystem.Common.Contracts.Services;
public interface IValidationService
{
    public Task EnsureValid<T>(T model);
}
