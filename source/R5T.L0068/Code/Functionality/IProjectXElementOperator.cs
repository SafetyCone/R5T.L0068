using System;
using System.Xml.Linq;

using R5T.L0032.T000;
using R5T.L0032.T000.Extensions;
using R5T.T0132;
using R5T.T0218.Extensions;

using ITargetFrameworkMoniker = R5T.T0218.ITargetFrameworkMoniker;


namespace R5T.L0068
{
    [FunctionalityMarker]
    public partial interface IProjectXElementOperator : IFunctionalityMarker
    {
        public ITargetFrameworkMoniker Get_TargetFrameworkMoniker(XElement projectElement)
        {
            var targetFrameworkMoniker = Instances.ProjectXmlOperator.GetTargetFramework(projectElement)
                .ToTargetFrameworkMoniker();

            return targetFrameworkMoniker;
        }

        public (IProjectSdkName sdkName, ITargetFrameworkMoniker targetFrameworkMoniker) Get_RuntimeTargetInformation(XElement projectElement)
        {
            var sdkName = Instances.ProjectXmlOperator.GetSdk(projectElement)
                .ToProjectSdkName();

            var targetFrameworkMoniker = Instances.ProjectXmlOperator.GetTargetFramework(projectElement)
                .ToTargetFrameworkMoniker();

            return (sdkName, targetFrameworkMoniker);
        }
    }
}
