using System;

using R5T.L0032.T000;
using R5T.T0132;

using IProjectSdkNameStrings = R5T.L0032.Z000.Raw.Platform.IProjectSdkNames;
using ITargetFrameworkMoniker = R5T.T0218.ITargetFrameworkMoniker;


namespace R5T.L0068
{
    [FunctionalityMarker]
    public partial interface IDotnetRuntimeNameOperator : IFunctionalityMarker
    {
        public IRuntimeName Get_RuntimeName(
            IProjectSdkName projectSdkName,
            ITargetFrameworkMoniker targetFrameworkMoniker)
        {
            // If it's a Windows-specific target framework moniker, then return windows.
            var isWindowsSpecific = Instances.TargetFrameworkMonikerOperator.Is_WindowsSpecific(targetFrameworkMoniker);
            if(isWindowsSpecific)
            {
                return Instances.DotnetRuntimeNames.Microsoft_WindowsDesktop_App;
            }

            // Otherwise, use the project SDK name.
            var output = projectSdkName.Value switch
            {
                // _App should be used over _All
                IProjectSdkNameStrings.Microsoft_NET_Sdk_BlazorWebAssembly_Constant => Instances.DotnetRuntimeNames.Microsoft_AspNetCore_App,
                IProjectSdkNameStrings.Microsoft_NET_Sdk_Razor_Constant => Instances.DotnetRuntimeNames.Microsoft_AspNetCore_App,
                IProjectSdkNameStrings.Microsoft_NET_Sdk_Web_Constant => Instances.DotnetRuntimeNames.Microsoft_AspNetCore_App,
                IProjectSdkNameStrings.Microsoft_NET_Sdk_Constant => Instances.DotnetRuntimeNames.Microsoft_NETCore_App,
                _ => throw new Exception($"{projectSdkName}: Unknown project SDK name.")
            };

            return output;
        }
    }
}
