using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateFinishes
{
    class ExtEventHandler : IExternalEventHandler
    {
        public void Execute(UIApplication app, Document _doc, View selView, Line baseLine)
        {
            _doc.Create.NewDetailCurve(selView as View, baseLine);
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }
    }
}
