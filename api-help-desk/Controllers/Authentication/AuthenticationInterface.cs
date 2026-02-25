namespace api_help_desk.Controllers.Security.Authentication
{
    public interface AuthenticationInterface
    {
        public Task<List<Config>> PostToken(User user, LoginContext context);

        public Task<object> PostLogOut(Guid user_id);

        public Task<string> GetJWT(User user, Guid id, string country, LoginContext context);
        public Task<List<MenuModel>> GetMenu(Guid user);
        public Task<List<MenuModel>> PostMenu(List<UserAccess> menu);
    }
}
