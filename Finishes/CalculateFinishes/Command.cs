#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#endregion

namespace CurtainWallScan
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]

    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            // Access current selection

            Selection sel = uidoc.Selection;

            // Retrieve elements from database

            ElementParameterFilter EP_Filter = CreateElementParameterFilter();

            FilteredElementCollector col = new FilteredElementCollector(doc);
            ICollection<Element> draftingViews = col.WhereElementIsNotElementType().WherePasses(EP_Filter)
                                               .OfClass(typeof(View)).ToElements();

            // Modify document within a transaction

            //using (Transaction tx = new Transaction(doc))
            //{
            //    tx.Start("Transaction Name");
            //    tx.Commit();
            //}

            UserWindow userWindow = new UserWindow(sel, doc); ;

            userWindow.Show();

            return Result.Succeeded;
        }

        private static ElementParameterFilter CreateElementParameterFilter()
        {
            BuiltInParameter builtInParameter = BuiltInParameter.VIEW_TYPE;
            ParameterValueProvider pvp = new ParameterValueProvider(new ElementId((int)builtInParameter));
            FilterStringContains fsc = new FilterStringContains();
            string ruleValue = "��������� ����(Detail)";
            FilterStringRule fRule = new FilterStringRule(pvp, fsc, ruleValue, true);
            ElementParameterFilter EP_Filter = new ElementParameterFilter(fRule);
            return EP_Filter;
        }
    }
}
