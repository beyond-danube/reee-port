using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Sheets.v4;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading;

namespace reee_port_01
{
    public class GoogleHandler
    {
        protected string ApplicationName = "reeeport";
        protected UserCredential credential;

        private readonly string[] Scopes = { DriveService.Scope.DriveFile, SheetsService.Scope.Spreadsheets };

        public GoogleHandler()
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

    }
}
