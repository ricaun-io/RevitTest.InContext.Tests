using Autodesk.Revit.UI;
using NUnit.Framework;

namespace RevitTest.InContext.Tests
{
    public class RevitInContextTests : ContextBenchamarkTests
    {
        public override bool Run(UIApplication uiapp)
        {
            return uiapp.InContext();
        }
    }

    public class RevitInAddInContextTests : ContextBenchamarkTests
    {
        public override bool Run(UIApplication uiapp)
        {
            return uiapp.InAddInContext();
        }
    }

    public class RevitInApiModeTests : ContextBenchamarkTests
    {
        public override bool Run(UIApplication uiapp)
        {
            return RevitApi.IsRevitInApiMode;
        }
    }

}
