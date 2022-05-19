namespace PinService.Domain.Core.Interfaces
{
    public interface ICacheService
    {
        Task<T> CreateOrGetValueAsync<T>(string key, Func<Task<T>> action);
        Task<T> SetValueAsync<T>(string key, Func<Task<T>> action);
        bool TryGetValue<T>(string key, out T value);
    }
}
