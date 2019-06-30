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
        private XMLHandler xmlHandler;
        private Settings settings;
        private GoogleSheetHandler sheetHandler;
        private string settingsPath = Environment.CurrentDirectory + @"\Resources\reeeportsettings.xml";
        private string xmlDocName = DateTime.Now.ToString("MM-dd-yyyy_HH_mm_ss") + ".xml";
        private string xmlReportPath;

        public MainWindow()
        {

            InitializeComponent();
            
        }

        private void Application_Startup(object sender, EventArgs e)
        {

            settings = Settings.SettingsReader(settingsPath);

            xmlHandler = new XMLHandler();
            xmlReportPath = Path.Combine(settings.XmlReportPath, xmlDocName);
            xmlHandler.CreateXmlReport(xmlReportPath);

            sheetHandler = new GoogleSheetHandler();
    
            NoteType.ItemsSource = settings.NoteTypes;
            NoteType.SelectedIndex = 0;

        }


        private void NoteFiled_KeyDown(Object sender, KeyEventArgs e)
        {
 
            if (e.Key == Key.Enter && e.KeyboardDevice.Modifiers == ModifierKeys.Control)
            {

                Note note = new Note(NoteType.Text, NoteField.Text);

                if (settings.GenerateXml == true)
                {
                    xmlHandler.WriteToXmlReport(note, xmlReportPath);
                }

                if (settings.GenarateGoogleSheet == true)
                {
                    sheetHandler.AppendToSheet(note, settings.SpreadsheetID, settings.SheetRange);
                }            

                NoteField.Clear();

            }       

        }

        private void button_Open_Pref(object sender, RoutedEventArgs e)
        {
            //var preferences = new Views.Preferences();
                        
            //preferences.Show();
        }
    }
}
