using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;

namespace reee_port_01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ReeeportSettings settings;
  
        private GoogleSheetHandler sheetHandler;
        private GoogleDriveHandler driveHandler;

        private string settingsPath = Environment.CurrentDirectory + @"\Resources\reeeportsettings.xml";

        PreferencesWindow pw = new PreferencesWindow();

        private string[] draggedFiles;

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

                Note note = new Note(NoteType.Text, NoteField.Text, draggedFiles);

                sheetHandler = sheetHandler == null ? new GoogleSheetHandler() : sheetHandler;
                driveHandler = driveHandler == null ? new GoogleDriveHandler() : driveHandler;

                try
                {
                    sheetHandler.AppendToSheet(note, settings.SpreadsheetID, settings.SheetRange);
                    driveHandler.UploadFile(note.AttachedFiles[0], settings.DriveFolderID);
                }
                catch (Exception)
                {
                    MessageBox.Show("Cannot write note to spreadsheet.\n\nPlease, check following:\n • Google Spreadsheet URL and Sheet Name are correct.\n • Internet connection is fine.\n", "Cannot Write to Spreadsheet");                     
                }

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
            draggedFiles = e.Data.GetData(DataFormats.FileDrop) as string[];
        }
    }
}
