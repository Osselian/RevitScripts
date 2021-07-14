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
            Selection sel = uidoc.Selection;

            BuiltInParameter builtInParameter = BuiltInParameter.VIEW_FAMILY;

            ElementParameterFilter EP_Filter = CreateElementStringParameterFilter(builtInParameter, "Чертежные виды");

            FilteredElementCollector col = new FilteredElementCollector(doc);

            ICollection<Element> draftingViews = col.WhereElementIsNotElementType().WherePasses(EP_Filter)
                                                    .OfClass(typeof(View)).ToElements();

            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Transaction Name");
                UserWindow userWindow = new UserWindow(sel, doc, draftingViews);

                userWindow.Show();
                tx.Commit();
            }
            return Result.Succeeded;
        }

        private ElementParameterFilter CreateElementStringParameterFilter(BuiltInParameter parameter, string ruleValue)
        {
            ParameterValueProvider pvp = new ParameterValueProvider(new ElementId((int)parameter));
            FilterStringContains fsc = new FilterStringContains();
            FilterStringRule fRule = new FilterStringRule(pvp, fsc, ruleValue, true);
            ElementParameterFilter EP_Filter = new ElementParameterFilter(fRule);

            return EP_Filter;
        }
    }
}
