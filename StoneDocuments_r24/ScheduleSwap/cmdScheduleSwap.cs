﻿namespace StoneDocuments_r24
{
    [Transaction(TransactionMode.Manual)]
    public class cmdScheduleSwap : IExternalCommand
    {
        public ViewSheet curSheet;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // this is a variable for the Revit application
            UIApplication uiapp = commandData.Application;

            // this is a variable for the current Revit model
            Document curDoc = uiapp.ActiveUIDocument.Document;

            // check current view - make sure it's a sheet

            if (!(curDoc.ActiveView is ViewSheet))
            {
                TaskDialog.Show("Error", "Please make the active view a sheet");
                return Result.Failed;
            }

            curSheet = curDoc.ActiveView as ViewSheet;

            if (Utils.SheetHasSchedule(curDoc, curSheet) == false)
            {
                TaskDialog.Show("Error", "The current sheet does not have a schedule. Please select another sheet.");
                return Result.Failed;
            }

            // open form
            frmScheduleSwap curForm = new frmScheduleSwap(uiapp)
            {
                Width = 450,
                Height = 150,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen,
                Topmost = true,
            };

            curForm.ShowDialog();


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
                    Properties.Resources.ScheduleSwap_32,
                    Properties.Resources.ScheduleSwap_16,
                    "This is a tooltip for Button 1");

                return myButtonData1.Data;
            }
        }
    }

}
