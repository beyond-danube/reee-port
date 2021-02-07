using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;
using System.Collections.Generic;

namespace reeeport
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ReeeportSettings settings;
  
        private GoogleHandlerSheet sheetHandler;
        private GoogleHandlerDrive driveHandler;

        private string settingsPath = Environment.CurrentDirectory + @"\Resources\reeeportsettings.xml";

        PreferencesWindow pw = new PreferencesWindow();

        private List<string> draggedFiles = new List<string>();

        public MainWindow()
        {

            InitializeComponent();
            DataContext = settings;
            pw.DataContext = DataContext;
            
        }

        private void Application_Startup(object sender, EventArgs e)
        {

            settings = ReeeportSettings.SettingsReader(settingsPath);
            sheetHandler = null;
            driveHandler = null;

            NoteType.ItemsSource = settings.NoteTypesArr;
            NoteType.SelectedIndex = 0;

            MainWindow mw = this;
            mw.Topmost = settings.AlwaysOnTop;
        }


        private void NoteFiled_KeyDown(object sender, KeyEventArgs e)
        {
 
            if (e.Key == Key.Enter && e.KeyboardDevice.Modifiers == ModifierKeys.Control)
            {

                Note note = new Note(NoteType.Text, NoteField.Text, draggedFiles.ToArray());

                sheetHandler = sheetHandler == null ? new GoogleHandlerSheet() : sheetHandler;
                driveHandler = driveHandler == null ? new GoogleHandlerDrive() : driveHandler;

                try
                {


                    if (note.AttachedFiles != null && note.AttachedFiles.Length > 0)
                    {
                        string driveSubFolderId = driveHandler.CreateFolder(note.Id, settings.DriveFolderID);

                        sheetHandler.AppendToSheet(note, settings.SpreadsheetID, settings.SheetRange, "=HYPERLINK(\"https://drive.google.com/drive/u/0/folders/" + driveSubFolderId + "\"" + ", \"Attachments\")");
                        
                        foreach (var file in note.AttachedFiles)
                        {
                            driveHandler.UploadFile(file, driveSubFolderId);
                        }
                    }

                    else
                    {
                        sheetHandler.AppendToSheet(note, settings.SpreadsheetID, settings.SheetRange, "");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Something went wrong, and unfortunately we have no idea what exactly, since this this message handles every possible wrong scenario.\n\nPlease, check following:\n • Google Spreadsheet URL and Sheet Name are correct.\n • Internet connection is fine.\n", "Cannot Write to Google Drive");                     
                }

                draggedFiles.Clear();
                NoteField.Clear();
            }       

        }

        private void OnExit(object sender, EventArgs e)
        {
            ReeeportSettings.SettingsSaver(settingsPath, settings);
        }

        public void Application_Deactivated(object sender, EventArgs e)
        {
            MainWindow mw = this;
            mw.Topmost = settings.AlwaysOnTop;
        }

        private void OpenPrefs(object sender, RoutedEventArgs e)
        {
            pw.Show();
        }

        private void NoteField_DragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }

        private void NoteField_Drop(object sender, DragEventArgs e)
        {
            e.Handled = true;
            e.Effects = DragDropEffects.Copy;
            draggedFiles.AddRange(e.Data.GetData(DataFormats.FileDrop) as string[]);          
        }
    }
}
