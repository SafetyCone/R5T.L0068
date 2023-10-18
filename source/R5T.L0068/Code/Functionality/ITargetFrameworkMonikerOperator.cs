using System;

using R5T.T0132;
using R5T.T0218;


namespace R5T.L0068
{
    [FunctionalityMarker]
    public partial interface ITargetFrameworkMonikerOperator : IFunctionalityMarker,
        F0139.ITargetFrameworkMonikerOperator
    {
        public bool Is_WindowsSpecific(ITargetFrameworkMoniker targetFrameworkMoniker)
        {
            var output = Instances.StringOperator.Contains(
                targetFrameworkMoniker.Value,
                Instances.TargetFrameworkMonikerTokens.Windows);

            return output;
        }
    }
}
