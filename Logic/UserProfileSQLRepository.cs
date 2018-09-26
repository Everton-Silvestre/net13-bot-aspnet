using System.Data.Entity;
using System.Linq;
using SimpleBot.Interface;
using SimpleBot.Model; 

namespace SimpleBot.Logic
{
    public class UserProfileSqlRepository : IUserProfileRepository
    {
        private readonly Contexto _context;
        
        public UserProfileSqlRepository()
        {
            _context = new Contexto();           
        }
        
        public UserProfile GetProfile(string id)
        {
            
            var profile = _context.Profile.FirstOrDefault(x => x.MessageId==id) ?? new Profile();

            return new UserProfile
            {
                Id = id,                
                Visitas = profile.Visitas
            };
            
        }

        public void SetProfile(string id, UserProfile profile)
        {
            var prof = _context.Profile.FirstOrDefault(x => x.MessageId == id);

            if (prof != null)
            {
                _context.Profile.Attach(prof);
                _context.Entry(prof).State = EntityState.Modified;
            }
            else
            {
                _context.Profile.Add( new Profile
                {
                    MessageId = profile.Id,
                    Visitas = profile.Visitas,
                    Nome = profile.Id
                });
            }

            _context.SaveChanges();
                        
        }
    }
}