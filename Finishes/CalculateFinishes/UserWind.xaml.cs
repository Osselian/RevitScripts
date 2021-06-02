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
using Revit.GeometryConversion;

using DSSurface = Autodesk.DesignScript.Geometry.Surface;
using DSLine = Autodesk.DesignScript.Geometry.Line;
using DSCurve = Autodesk.DesignScript.Geometry.Curve;
using Curve = Autodesk.Revit.DB.Curve;
using Line = Autodesk.Revit.DB.Line;
using Point = Autodesk.Revit.DB.Point;

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

        Autodesk.Revit.DB.Line RLine;
        public UserWindow(Selection sel, Document document, ICollection<Element> views)
        {
            InitializeComponent();

            _selection = sel;
            _doc = document;

            Views.ItemsSource = views.Select(view => view.Name);
            //MessageBox.Show(views.Count().ToString());
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

        private void DrawBtn_Click(object sender, RoutedEventArgs e)
        {
            Wall wall = _elements[0] as Wall;
            CurtainGrid curtainGrid = wall.CurtainGrid;
            Curve baseVLine = ((LocationCurve)wall.Location).Curve;

            double vLen = baseVLine.Length;
            double uLen = 328.084;
            
            XYZ baseUPoint1 = baseVLine.GetEndPoint(0);
            XYZ baseUPoint2 = new XYZ(baseUPoint1.X, baseUPoint1.Y, uLen);

            Line baseULine = Line.CreateBound(baseUPoint1, baseUPoint2);
            DSSurface surface = DSSurface.BySweep(baseVLine.ToProtoType(), baseULine.ToProtoType());

            List<CurtainCell> curtainCells = curtainGrid.GetCurtainCells().ToList();

            List<Curve> curves = new List<Curve>();
            foreach (var curtainCell in curtainCells)
            {
                try
                {
                    CurveArrArray curveArrArray = curtainCell.CurveLoops;
                    foreach (var curve in curveArrArray)
                    {
                        curves.Add(curve as Curve);
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }

            foreach (var curve in curves)
            {
                XYZ startPoint = curve.GetEndPoint(0);

            }
        }

    }
}
