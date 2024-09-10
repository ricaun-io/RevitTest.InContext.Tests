using Autodesk.Revit.UI;

public static class InAddInContextExtension
{
    public static bool InAddInContext(this UIApplication application)
    {
        return application.ActiveAddInId is not null;
    }
    public static bool InAddInContext(this UIControlledApplication application)
    {
        return application.ActiveAddInId is not null;
    }
}