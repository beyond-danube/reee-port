using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading;

namespace reee_port_01
{
    public abstract class GoogleHandler
    {
        protected string ApplicationName = "reeeport";
        protected UserCredential credential;

        protected string[] Scopes;

        public void InitCredentialsWithScopes()
        {
            using (var stream = new FileStream(@"Resources\credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = @"Resources\token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }
        }

        public void SetScopes(string[] scopes)
        {
            Scopes = scopes;
        }
    }
}
