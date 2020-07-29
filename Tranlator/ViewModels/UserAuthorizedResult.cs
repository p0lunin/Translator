namespace Tranlator.ViewModels
{
    public class UserAuthorizedResult
    {
        public bool IsNewUser { get; }
        public string Username { get; }
        public UserAuthorizedResult(string username, bool isNewUser = false)
        {
            IsNewUser = isNewUser;
            Username = username;
        }
    }
}