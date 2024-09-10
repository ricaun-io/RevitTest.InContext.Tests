using System;
using System.Linq;
using System.Reflection;
public static class RevitApi
{
    /// <summary>
    /// Based: https://github.com/Nice3point/RevitToolkit/blob/main/source/Nice3point.Revit.Toolkit/Context.cs
    /// </summary>
    static RevitApi()
    {
        var assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(assembly => assembly.GetName().Name == "APIUIAPI");
        var assemblyMethods = assembly?.ManifestModule.GetMethods(BindingFlags.NonPublic | BindingFlags.Static);
        apiCallDepthManagerMethod = assemblyMethods?.FirstOrDefault(info => info.Name == "APICallDepthManager.singletonfactory");
        isRevitInApiModeMethod = assemblyMethods?.FirstOrDefault(info => info.Name == "APICallDepthManager.isRevitInAPIMode");
    }

    private static readonly MethodInfo apiCallDepthManagerMethod;
    private static readonly MethodInfo isRevitInApiModeMethod;
    public static bool IsRevitInApiMode
    {
        get
        {
            if (apiCallDepthManagerMethod is null | isRevitInApiModeMethod is null)
                return false;

            var apiCallDepthManager = apiCallDepthManagerMethod.Invoke(null, null);
            return (bool)isRevitInApiModeMethod.Invoke(null, [apiCallDepthManager]);
        }
    }
}