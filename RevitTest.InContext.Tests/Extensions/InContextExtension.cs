using Autodesk.Revit.UI;

public static class InContextExtension
{
    public static bool InContext(this UIApplication application)
    {
        try
        {
            //void Application_Idling(object sender, Autodesk.Revit.UI.Events.IdlingEventArgs e) { }
            application.Idling += Application_Idling;
            application.Idling -= Application_Idling;
            return true;
        }
        catch { } // Invalid call to Revit API! Revit is currently not within an API context.
        return false;
    }
    public static bool InContext(this UIControlledApplication application)
    {
        try
        {
            //void Application_Idling(object sender, Autodesk.Revit.UI.Events.IdlingEventArgs e) { }
            application.Idling += Application_Idling;
            application.Idling -= Application_Idling;
            return true;
        }
        catch { } // Invalid call to Revit API! Revit is currently not within an API context.
        return false;
    }
    static void Application_Idling(object sender, Autodesk.Revit.UI.Events.IdlingEventArgs e) { }
}