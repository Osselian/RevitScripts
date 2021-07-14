using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using Revit.GeometryConversion;

using DSSurface = Autodesk.DesignScript.Geometry.Surface;
using DSPoint = Autodesk.DesignScript.Geometry.Point;
using DSUV = Autodesk.DesignScript.Geometry.UV;
using Curve = Autodesk.Revit.DB.Curve;
using Line = Autodesk.Revit.DB.Line;
using Point = Autodesk.Revit.DB.Point;

namespace CurtainWallScan
{
    public partial class UserWindow : Window
    {
        private Selection _selection;
        private Document _doc;
        private List<Element> _elements = new List<Element>();
        private ICollection<Element> _views;

        public UserWindow(Selection sel, Document document, ICollection<Element> views)
        {
            InitializeComponent();
            _selection = sel;
            _doc = document;
            _views = views;

            Views.ItemsSource = _views.Select(view => view.Name);
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

        private void DrawBtn_Click(object sender, EventArgs e)
        {
            Wall wall = _elements[0] as Wall;
            CurtainGrid curtainGrid = wall.CurtainGrid;
            Curve baseVLine = ((LocationCurve)wall.Location).Curve;
            var views = _views.Where(view => view.Name == Views.SelectedItem.ToString()).Select(view => view as View).ToList();
            View selView = views[0];

            double vLen = baseVLine.Length;
            double uLen = 328.084;

            XYZ baseUPoint1 = baseVLine.GetEndPoint(0);
            XYZ baseUPoint2 = new XYZ(baseUPoint1.X, baseUPoint1.Y, uLen);

            Line baseULine = Line.CreateBound(baseUPoint1, baseUPoint2);
            DSSurface surface = DSSurface.BySweep(baseVLine.ToProtoType(), baseULine.ToProtoType());

            List<CurtainCell> curtainCells = curtainGrid.GetCurtainCells().ToList();
            List<Curve> curves = GetCurtainCellsCurves(curtainCells);
            List<DSUV[]> curvesNormParams = GetCurvesNormParams(surface, curves);

            foreach (var coord in curvesNormParams)
            {
                double u1 = coord[0].U;
                double u2 = coord[1].U;
                double v1 = coord[0].V;
                double v2 = coord[1].V;

                XYZ point1 = new XYZ(vLen * v1, uLen * u1, 0);
                XYZ point2 = new XYZ(vLen * v2, uLen * u2, 0);

                Line baseLine = Line.CreateBound(point1, point2);

                _doc.Create.NewDetailCurve(selView, baseLine);
            }
        }

        private List<Curve> GetCurtainCellsCurves(List<CurtainCell> curtainCells)
        {
            List<Curve> curves = new List<Curve>();
            foreach (var curtainCell in curtainCells)
            {
                try
                {
                    CurveArrArray curveArrArray = curtainCell.CurveLoops;
                    foreach (CurveArray curveArray in curveArrArray)
                    {
                        foreach (var curve in curveArray)
                        {
                            curves.Add(curve as Curve);
                        }
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
            return curves;
        }

        private List<DSUV[]> GetCurvesNormParams(DSSurface surface, List<Curve> curves)
        {
            List<DSUV[]> curvesNormParams = new List<DSUV[]>();
            foreach (var curve in curves)
            {
                try
                {
                    DSUV[] coords = new DSUV[2];
                    XYZ xyz = curve.GetEndPoint(0);
                    Point startPoint = Point.Create(xyz);
                    DSPoint dsStartPoint = startPoint.ToProtoType();
                    DSPoint endPoint = Point.Create(curve.GetEndPoint(1)).ToProtoType();
                    DSUV uV1 = surface.UVParameterAtPoint(dsStartPoint);
                    DSUV uV2 = surface.UVParameterAtPoint(endPoint);
                    coords[0] = uV1;
                    coords[1] = uV2;
                    curvesNormParams.Add(coords);
                }
                catch (Exception)
                {
                    continue;
                }
            }

            return curvesNormParams;
        }
    }
}
