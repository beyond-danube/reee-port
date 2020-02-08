using System;
using System.Collections.Generic;
using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using System.Threading;
using Google.Apis.Util.Store;
using Google.Apis.Services;

using Data = Google.Apis.Sheets.v4.Data;

namespace reee_port_01
{
    public class GoogleSheetHandler
    {

        static string[] Scopes = { SheetsService.Scope.Spreadsheets };
        
        public void AppendToSheet(Note note, string spreadsheetID, string sheetRange)
        {
            string ApplicationName = "reeeport";

            UserCredential credential;

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

            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });


            string spreadsheetId = spreadsheetID;

            string range = sheetRange;

            SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum valueInputOption = (SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.RAW);  // TODO: Update placeholder value.

            SpreadsheetsResource.ValuesResource.AppendRequest.InsertDataOptionEnum insertDataOption = (SpreadsheetsResource.ValuesResource.AppendRequest.InsertDataOptionEnum.INSERTROWS);  // TODO: Update placeholder value.

            Data.ValueRange requestBody = new Data.ValueRange();

            var arr = new string[] { note.NoteType, note.NoteContent, note.NoteRecordtime};

            requestBody.Values = new List<IList<object>> { arr };

            SpreadsheetsResource.ValuesResource.AppendRequest request = service.Spreadsheets.Values.Append(requestBody, spreadsheetId, range);
            request.ValueInputOption = valueInputOption;
            request.InsertDataOption = insertDataOption;

            request.Execute();

        }
    }
}
