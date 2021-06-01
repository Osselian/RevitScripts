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
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;


namespace CurtainWallScan
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        private Selection _selection;
        private Document _doc;
        private List<Element> _elements = new List<Element>();
        private List<string> _names;

        public UserWindow(Selection sel, Document document)
        {
            InitializeComponent();
            _selection = sel;
            _doc = document;
        }

        private void ShowSelectedWalls(List<Element> elements)
        {
            AllWallsView.ItemsSource = elements.Select(elem => elem.Name);
        }

        private void SelectWallBtn_Click(object sender, RoutedEventArgs e)
        {
            Reference reference = _selection.PickObject(ObjectType.Element);
            Element element = _doc.GetElement(reference);
            
            _elements.Add(element);
            ShowSelectedWalls(_elements);
        }
    }
}
