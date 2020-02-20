using System;
using System.Collections.Generic;
using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using System.Threading;
using Google.Apis.Util.Store;
using Google.Apis.Services;
using Google.Apis.Sheets.v4.Data;
using static Google.Apis.Sheets.v4.SpreadsheetsResource.ValuesResource;

namespace reee_port_01
{
    public class GoogleSheetHandler
    {
        private readonly string ApplicationName;
        private readonly UserCredential credential;
        private SheetsService service;

        private ValueRange RequestBody { get; set; }
        private AppendRequest Request { get; set; }

        static string[] Scopes = { SheetsService.Scope.Spreadsheets };

        public GoogleSheetHandler()
        {
            ApplicationName = "reeeport";

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

            RequestBody = new ValueRange();
        }

        public void AppendToSheet(Note note, string spreadsheetID, string sheetRange)
        {
            if (service == null)
            {
                service = new SheetsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });
            }

 
            string spreadsheetId = spreadsheetID;
            string range = sheetRange;

            AppendRequest.ValueInputOptionEnum valueInputOption = AppendRequest.ValueInputOptionEnum.RAW;
            AppendRequest.InsertDataOptionEnum insertDataOption = AppendRequest.InsertDataOptionEnum.INSERTROWS;

            var arr = new string[] { note.NoteType, note.NoteContent, note.NoteRecordtime};

            RequestBody.Values = new List<IList<object>> { arr };

            Request = service.Spreadsheets.Values.Append(RequestBody, spreadsheetId, range);
            Request.ValueInputOption = valueInputOption;
            Request.InsertDataOption = insertDataOption;

            Request.Execute();

        }
    }
}
