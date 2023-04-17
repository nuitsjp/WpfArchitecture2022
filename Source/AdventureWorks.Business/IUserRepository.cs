namespace AdventureWorks.Business
{
    public interface IUserRepository
    {
        Task<User?> GetUserAsync(LoginId loginId);
    }
}