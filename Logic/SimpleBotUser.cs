using SimpleBot.Model;

namespace SimpleBot.Logic
{
    public class SimpleBotUser
    {
        IRepository _repo;

        public SimpleBotUser(IRepository repo)
        {
            this._repo = repo;
        }

        public string Popula_UserProfile(Message message)
        {                                     
            var prof = this._repo.GetProfile(message.Id);

            prof.Visitas += 1;

            this._repo.SetProfile(message.Id, prof);           

            return $"{message.User} disse '{message.Text} e mandou {prof.Visitas} '";
        }
        
        public string Reply(Message message)
        {
           return Popula_UserProfile(message);
        }
    }
}