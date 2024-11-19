namespace StoneDocuments_r24
{
    [Transaction(TransactionMode.Manual)]
    public class cmdReset : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // this is a variable for the Revit application
            UIApplication uiapp = commandData.Application;

            // this is a variable for the current Revit model
            Document doc = uiapp.ActiveUIDocument.Document;

            string userName = uiapp.Application.Username;

            // set current view to 3D view
            View curView;

            if (doc.IsWorkshared == true)
                curView = Utils.GetViewByName(doc, "{3D - " + userName + "}");
            else
                curView = Utils.GetViewByName(doc, "{3D}");

            // get all elements in view
            List<Element> viewElements = Utils.GetElementsFromView(doc, curView);

            // set override settings
            OverrideGraphicSettings colSet = new OverrideGraphicSettings();

            // update element overrides in view
            using (Transaction t = new Transaction(doc))
            {
                t.Start("Reset elements");

                foreach (Element curElem in viewElements)
                {
                    doc.ActiveView.SetElementOverrides(curElem.Id, colSet);
                }

                t.Commit();
            }

            return Result.Succeeded;
        }
        internal static PushButtonData GetButtonData()
        {
            // use this method to define the properties for this command in the Revit ribbon
            string buttonInternalName = "btnCommand1";
            string buttonTitle = "Button 1";
            string? methodBase = MethodBase.GetCurrentMethod().DeclaringType?.FullName;

            if (methodBase == null)
            {
                throw new InvalidOperationException("MethodBase.GetCurrentMethod().DeclaringType?.FullName is null");
            }
            else
            {
                Common.clsButtonData myButtonData1 = new Common.clsButtonData(
                    buttonInternalName,
                    buttonTitle,
                    methodBase,
                    Properties.Resources.Blue_32,
                    Properties.Resources.Blue_16,
                    "This is a tooltip for Button 1");

                return myButtonData1.Data;
            }
        }
    }
}
