namespace AdventureWorks.Business
{
    public interface IUserRepository
    {
        Task<bool> TryGetUserByIdAsync(LoginId loginId, out User user);
    }
}