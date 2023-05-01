namespace AdventureWorks.Business
{
    /// <summary>
    /// Userに対するリポジトリー
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// ユーザーを取得する。
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns></returns>
        Task<User?> GetUserAsync(LoginId loginId);
    }
}