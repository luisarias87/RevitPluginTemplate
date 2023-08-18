using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Electrical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RevitPluginTemplate.Views
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ClearanceWindow : Window
    {
        Document doc;

        public string SelectedFamilyName { get; private set; }
        public ClearanceWindow(Document doc)
        {
            this.doc = doc;
            InitializeComponent();
        }
        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            SelectedFamilyName = CTComboBox.SelectedItem?.ToString();
            this.Close();
        }
        private void On_Loaded(object sender, RoutedEventArgs e)
        {
            IList<string> clearanceFamilyName = new List<string>();

            FamilySymbol cableTrayFittingSymbol = null;

            FilteredElementCollector familySymbolCollector = new FilteredElementCollector(this.doc)
                .OfClass(typeof(FamilySymbol))
                .OfCategory(BuiltInCategory.OST_GenericModel)
                .WhereElementIsElementType();

            foreach (FamilySymbol symbol in familySymbolCollector)
            {
                if (symbol.Name == "CableTrayClearance_2022")
                {
                    cableTrayFittingSymbol = symbol;

                }
                if (cableTrayFittingSymbol != null)
                {
                    clearanceFamilyName.Add(cableTrayFittingSymbol.Name);
                    CTComboBox.ItemsSource = clearanceFamilyName;
                }
                
            }
        }



       
    }
}
