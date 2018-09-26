using SimpleBot.Logic;

namespace SimpleBot.Interface
{
    public interface IUserProfileRepository
    {
        UserProfile GetProfile(string id);

        void SetProfile(string id, UserProfile profile); 
    }
}
