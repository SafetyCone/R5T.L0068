using System;

using R5T.L0032.T000;
using R5T.T0132;

using IProjectSdkNameStrings = R5T.L0032.Z000.Raw.Platform.IProjectSdkNames;
using ITargetFrameworkMoniker = R5T.T0218.ITargetFrameworkMoniker;


namespace R5T.L0068
{
    [FunctionalityMarker]
    public partial interface IProjectSdkNameOperator : IFunctionalityMarker
    {
        //public IRuntimeName Get_CorrespondingRuntimeName(IProjectSdkName projectSdkName)
        //{
        //    var output = projectSdkName.Value switch
        //    {
        //        IProjectSdkNameStrings.Microsoft_NET_Sdk_Constant => Instances.DotnetRuntimeNames.Microsoft_NETCore_App,

        //        _ => throw new Exception($"{projectSdkName}: Unrecognized project SDK name.")
        //    };
        //}

        //public ITargetFrameworkMoniker Get_TargetFrameworkMoniker(IProjectSdkName projectSdkName)
        //{

        //}
    }
}
