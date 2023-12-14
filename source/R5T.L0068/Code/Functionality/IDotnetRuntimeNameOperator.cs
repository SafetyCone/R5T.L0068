using System;
using System.Linq;

using R5T.L0032.T000;
using R5T.T0132;

using IProjectSdkNameStrings = R5T.L0032.Z001.Raw.IProjectSdkNames;
using ITargetFrameworkMoniker = R5T.T0218.ITargetFrameworkMoniker;


namespace R5T.L0068
{
    [FunctionalityMarker]
    public partial interface IDotnetRuntimeNameOperator : IFunctionalityMarker
    {
        /// <summary>
        /// <para>Chooses <see cref="Get_RuntimeNames_InOrder(ITargetFrameworkMoniker)"/> as the default.</para>
        /// <inheritdoc cref="Get_RuntimeNames_InOrder(ITargetFrameworkMoniker)" path="/summary"/>
        /// </summary>
        public IRuntimeName[] Get_RuntimeNames(ITargetFrameworkMoniker targetFrameworkMoniker)
        {
            var output = this.Get_RuntimeNames_InOrder(targetFrameworkMoniker);
            return output;
        }

        /// <summary>
        /// For a target framework moniker (for example, from a project file), get the runtime names, in order from most specific to least specific assembly,
        /// containing assemblies that should be used.
        /// </summary>
        public IRuntimeName[] Get_RuntimeNames_InOrder(ITargetFrameworkMoniker targetFrameworkMoniker)
        {
            // Basically, ignore the project SDK name and always add the ASP.NET Core assemblies.
            var runtimeNames = Instances.EnumerableOperator.From(
                // Everything starts with the core runtime.
                Instances.DotnetRuntimeNames.Microsoft_NETCore_App,
                // Always include the ASP.NET assemblies, since you can never be sure if they will be needed.
                // ...App should be used over ...All
                Instances.DotnetRuntimeNames.Microsoft_AspNetCore_App);

            // If it's a Windows-specific target framework moniker, then return windows.
            var isWindowsSpecific = Instances.TargetFrameworkMonikerOperator.Is_WindowsSpecific(targetFrameworkMoniker);
            if (isWindowsSpecific)
            {
                runtimeNames = runtimeNames.Append(Instances.DotnetRuntimeNames.Microsoft_WindowsDesktop_App);
            }

            var output = runtimeNames
                // Reverse the order (so that more specific assemblies are used before less specific assemblies).
                .Reverse()
                .Now();

            return output;
        }

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
