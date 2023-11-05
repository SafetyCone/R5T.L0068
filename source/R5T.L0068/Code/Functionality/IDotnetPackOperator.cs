using System;
using System.Linq;

using R5T.T0132;
using R5T.T0215;

using ITargetFrameworkMoniker = R5T.T0218.ITargetFrameworkMoniker;


namespace R5T.L0068
{
    [FunctionalityMarker]
    public partial interface IDotnetPackOperator : IFunctionalityMarker
    {
        public IDotnetPackName[] Get_DotnetPackNames(ITargetFrameworkMoniker targetFrameworkMoniker)
        {
            // Basically, ignore the project SDK name and always add the ASP.NET Core pack.
            var runtimeNames = Instances.EnumerableOperator.From(
                // Everything starts with the core runtime.
                Instances.DotnetPackNames.Microsoft_NETCore_App_Ref,
                // Always include the ASP.NET assemblies, since you can never be sure if they will be needed.
                // ...App should be used over ...All
                Instances.DotnetPackNames.Microsoft_AspNetCore_App_Ref);

            // If it's a Windows-specific target framework moniker, then return windows.
            var isWindowsSpecific = Instances.TargetFrameworkMonikerOperator.Is_WindowsSpecific(targetFrameworkMoniker);
            if (isWindowsSpecific)
            {
                runtimeNames = runtimeNames.Append(Instances.DotnetPackNames.Microsoft_WindowsDesktop_App_Ref);
            }

            var output = runtimeNames
                // Reverse the order (so that more specific assemblies are used before less specific assemblies).
                .Reverse()
                .Now();

            return output;
        }
    }
}
