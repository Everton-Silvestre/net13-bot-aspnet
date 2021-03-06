using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using SimpleBot.Logic;
using SimpleBot.Model;

namespace SimpleBot.Controllers
{
    //Vers�o Inicial 
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        [ResponseType(typeof(void))]
        public virtual async Task<HttpResponseMessage> Post([FromBody] Activity activity)
        {
            if ( activity != null && activity.Type == ActivityTypes.Message)
            {
                await HandleActivityAsync(activity);
            }

            // HTTP 202
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }

        // Estabelece comunica��o entre o usu�rio e o SimpleBotUser
        async Task HandleActivityAsync(Activity activity)
        {
            var text = activity.Text;
            var userFromId = activity.From.Id;
            var userFromName = activity.From.Name;

            var message = new Message(userFromId, userFromName, text);

            var response = new SimpleBotUser(new UserProfileSqlRepository()).Reply(message);

            await ReplyUserAsync(activity, response);
        }

        // Responde mensagens usando o Bot Framework Connector
        async Task ReplyUserAsync(Activity message, string text)
        {
            var connector = new ConnectorClient(new Uri(message.ServiceUrl));
            var reply = message.CreateReply(text);

            await connector.Conversations.ReplyToActivityAsync(reply);
        }
    }
}