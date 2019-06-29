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
        private XMLHandler sessionree;

        private string spreadsheetID = "13nGkfQ9tjwW3Ah6qQZIYQQh4_65118uqZv3gOZegYrc";
        private string xmlReportPath = @"D:\Source\repos\reee-port\reee_port_01\bin\Debug";
        private string xmlDocName = DateTime.Now.ToString("YYYT-MM-DD-HH_mm");

        public MainWindow()
        {

            InitializeComponent();
            
        }

        private void Application_Startup(object sender, EventArgs e)
        {

            //temporaty - populate combobox with data from txt
            string[] NoteTypeComboBox = File.ReadAllLines("NoteType.txt");

            NoteType.ItemsSource = NoteTypeComboBox;
            NoteType.SelectedItem = "Bug";

            NoteField.Focus();

            Settings settings = new Settings(Settings.GetSpreadsheetID(spreadsheetID), xmlReportPath);

            XMLHandler ree = new XMLHandler();
            ree.XmlReportPath = Path.Combine(settings.XmlReportPath, xmlDocName);
            ree.CreateXmlReport();

            sessionree = ree;

            

        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NoteFiled_KeyDown(Object sender, KeyEventArgs e)
        {
 
            if (e.Key == Key.Enter && e.KeyboardDevice.Modifiers == ModifierKeys.Control)
            {

                Note note = new Note(NoteType.Text, NoteField.Text);
                sessionree.WriteToXmlReport(note, sessionree);

                GoogleSheetHandler.AppendToSheet(NoteType.Text, NoteField.Text, spreadsheetID);

                NoteField.Clear();

            }       

        }

    }
}
