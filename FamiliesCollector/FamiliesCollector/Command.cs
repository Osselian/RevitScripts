#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

#endregion

namespace FamiliesCollector
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            

            string dir = @"\\ABTB-SERVER\Exchange\BIM_Admin\06_TEMP\Проекты для анализа";
            string[] files = Directory.GetFiles(dir);

            //foreach (var file in files)
            //{
            //    uiapp.OpenAndActivateDocument(dir +"\\" + file);
            //}

            uiapp.OpenAndActivateDocument(@"\\ABTB -SERVER\Exchange\BIM_Admin\06_TEMP\Проекты для анализа\ТПУ_Петровско-Разумовская.rvt");

            Document doc = uidoc.Document;

            // Access current selection

            FilteredElementCollector col
              = new FilteredElementCollector(doc)               
                .OfCategory(BuiltInCategory.OST_Walls)
                .WhereElementIsNotElementType();

            //Selection sel = uidoc.Selection;

            // Retrieve elements from database





            //foreach (Element e in col)
            //{
            //    Debug.Print(e.Name);
            //}

            //// Modify document within a transaction

            //using (Transaction tx = new Transaction(doc))
            //{
            //    tx.Start("Transaction Name");
            //    tx.Commit();
            //}

            return Result.Succeeded;
        }
    }
}
