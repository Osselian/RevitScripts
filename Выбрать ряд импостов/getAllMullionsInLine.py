import clr

clr.AddReference('RevitAPI')
from Autodesk.Revit.DB import*

clr.AddReference('RevitServices')
import RevitServices
from RevitServices.Persistence import DocumentManager
from RevitServices.Transactions import TransactionManager

clr.AddReference('ProtoGeometry')
from Autodesk.DesignScript.Geometry import Surface as DS_Surface, PolyCurve, Geometry

clr.AddReference('RevitNodes')
import Revit
from Revit.Elements import *
clr.ImportExtensions(Revit.GeometryConversion)

clr.AddReference('RevitAPIUI')
from  Autodesk.Revit.UI import Selection 

from System.Collections.Generic import List

doc = DocumentManager.Instance.CurrentDBDocument
uidoc=DocumentManager.Instance.CurrentUIApplication.ActiveUIDocument

def sortByName(viewTemp):
    return viewTemp.Name

def module(a):
    if a <0:
        a = a*-1
    return a

selectedMull = doc.GetElement(uidoc.Selection.GetElementIds()[0])
#categoryId = selectedMull.Symbol.Name

curtainSys = selectedMull.Host

pvp = ParameterValueProvider(ElementId(BuiltInParameter.ELEM_CATEGORY_PARAM))
fsc = FilterStringContains()
ruleValue = 'Импосты'
fRule = FilterStringRule(pvp, fsc, ruleValue, True)
EP_Filter = ElementParameterFilter(fRule)

allmullsIds = curtainSys.GetDependentElements(EP_Filter)
allmulls = []
for i in allmullsIds:
    allmulls.append(doc.GetElement(i))

codirectMulls = []
direct = module(selectedMull.LocationCurve.Direction[2])
err_list = []

for mull in allmulls:
    try:
        mull_direct = module(mull.LocationCurve.Direction[2])
        if mull_direct == direct:
            codirectMulls.append(mull)
    except AttributeError:
        err_list.append(mull)

inLineMulls = []

for c_mull in codirectMulls:
    if direct == 1:
        if c_mull.LocationCurve.GetEndPoint(0).X == selectedMull.LocationCurve.GetEndPoint(0).X and c_mull.LocationCurve.GetEndPoint(0).Y == selectedMull.LocationCurve.GetEndPoint(0).Y:
            inLineMulls.append(c_mull)
    elif direct == 0:
        if c_mull.LocationCurve.GetEndPoint(0).Z == selectedMull.LocationCurve.GetEndPoint(0).Z:
            inLineMulls.append(c_mull)


mullsList = List[ElementId]([i.Id for i in inLineMulls])
uidoc.Selection.SetElementIds(mullsList)


OUT = err_list