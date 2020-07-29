using System;

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

        protected bool Equals(UserAuthorizedResult other)
        {
            return IsNewUser == other.IsNewUser && Username == other.Username;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((UserAuthorizedResult) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IsNewUser, Username);
        }
        public override string ToString()
        {
            return $"{nameof(IsNewUser)}: {IsNewUser}, {nameof(Username)}: {Username}";
        }
    }
}