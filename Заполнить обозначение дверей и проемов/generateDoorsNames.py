import clr

clr.AddReference('RevitNodes')
import Revit
from Revit.Elements import *
clr.ImportExtensions(Revit.GeometryConversion)

clr.AddReference('RevitAPI')
from Autodesk.Revit.DB import*

clr.AddReference('RevitServices')
import RevitServices
from RevitServices.Persistence import DocumentManager
from RevitServices.Transactions import TransactionManager

doc = DocumentManager.Instance.CurrentDBDocument

doors = [i for i in FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Doors).WhereElementIsNotElementType().ToElements()]

a = []

TransactionManager.Instance.EnsureInTransaction(doc)
for door in doors:
    if 'ABTB_' in  door.Symbol.FamilyName:
        if 'Витраж' in door.Symbol.FamilyName:
            door.LookupParameter('Примечание').Set('Витраж')
        else:
            pass
        if 'Двупольная' in door.Symbol.FamilyName:
            doorName = 'Дверь двупольная '
            doorSectionSign = 'x'
        elif 'Однопольная' in door.Symbol.FamilyName:
            doorName = 'Дверь однопольная '
            doorSectionSign = ''
        else:
            pass
        doorOpeningSign = door.LookupParameter('Обозначение открывания').AsString()
        doorLocationSign = door.Symbol.LookupParameter('Обозначение расположения').AsString()
        doorFillerSign = door.Symbol.LookupParameter('Обозначение заполнения').AsString()
        
        if 'Витраж' in door.Symbol.FamilyName:
            doorSize = str(door.LookupParameter('Ширина в чистоте').AsValueString()) + "x" + str(door.LookupParameter('Высота проема').AsValueString())
            doorSizeSign = str(int(door.LookupParameter('Высота проема').AsDouble() * 3.048)) + '-' + str(int(door.LookupParameter('Ширина в чистоте').AsDouble() * 3.048))
        else:
            doorSize = str(door.Symbol.LookupParameter('Ширина').AsValueString()) + "x" + str(door.Symbol.LookupParameter('Высота').AsValueString())
            doorSizeSign = str(int(door.Symbol.LookupParameter('Высота').AsDouble() * 3.048)) + '-' + str(int(door.Symbol.LookupParameter('Ширина').AsDouble() * 3.048))

        doorMaterialSign = door.Symbol.LookupParameter('Обозначение материала').AsString()
        try:
            doorConstructSign = door.Symbol.LookupParameter('Обозначение конструкции').AsString()
        except AttributeError:
            pass
        # doorDescription = door.LookupParameter('ADSK_Обозначение').AsString()
        doorFireresist = door.Symbol.LookupParameter('Огнестойкость').AsString()
        a.append(doorFireresist)

        if doorMaterialSign == 'ПС':
            doorMaterial = 'противопожарная стальная'
        elif doorMaterialSign == 'С':
            doorMaterial = 'стальная'
        else:
            doorMaterial = ''

        if doorFillerSign == 'Г':
            doorDescription = 'глухая'
        else: 
            doorDescription = ''

        if doorOpeningSign == 'Нет':
            doorOpeningSign = ""
            doorOpening = ""
        elif doorOpeningSign == '*':
            doorOpening = "левая "
        else: 
            doorOpening = "правая "

        if doorLocationSign == "Н":
            doorLocation = "наружняя "
        else:
            doorLocation = "внутренняя "

        if doorFillerSign == "О":
            doorFiller = " остекленная."
        else:
            doorFiller = "."
        
        if doorConstructSign == "К":
            doorConstruction = " маятниковая"
        else:
            doorConstruction = ""

        openingSize = str(door.LookupParameter('Ширина проема').AsValueString()) + "x" + str(door.LookupParameter('Высота проема').AsValueString())

        openingSizeSign = str(int(round(door.LookupParameter('Высота проема').AsDouble() * 3.048)))+ "x" + str(int(round(door.LookupParameter('Ширина проема').AsDouble() * 3.048))) 

        doorNameSign = doorSectionSign + doorOpeningSign + 'Д' + doorLocationSign + doorMaterialSign + doorConstructSign + doorFillerSign + " " + openingSizeSign + ', ' + doorSize 
        
        try:
            doorComment = door.LookupParameter('ABTB_Многострочное примечание').AsString()
            if doorComment == None:
                doorComment = ''
            else:
                pass
        except AttributeError:
            doorComment = ''
        if doorFireresist != None:
            if doorFireresist not in doorComment:
                newValue = doorComment + '\n' + doorFireresist
                door.LookupParameter('ABTB_Многострочное примечание').Set(newValue)
            else:
                pass
        else:
            pass

        

        doorName += doorOpening + doorLocation + doorDescription + doorMaterial + doorConstruction + doorFiller

        door.LookupParameter('ADSK_Наименование').Set(doorNameSign + ' ' + doorName)

        door.LookupParameter('ABTB_Обозначение проемов').Set(openingSize)

    else:
        continue

TransactionManager.Instance.TransactionTaskDone()

OUT = a 