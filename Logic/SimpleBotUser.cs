using SimpleBot.Interface;

namespace SimpleBot.Logic
{
    public  class SimpleBotUser
    {
        private readonly IUserProfileRepository _repo;

        public SimpleBotUser(IUserProfileRepository repo)
        {
            _repo = repo;
        }

        private string Popula_UserProfile(Message message)
        {
            var prof = _repo.GetProfile(message.Id);

            prof.Visitas += 1;

            _repo.SetProfile(message.Id, prof);        

            return $"{message.User} disse '{message.Text} e mandou {prof.Visitas} '";
        }
        
        public string Reply(Message message)
        {
           return Popula_UserProfile(message);
        }
    }
}