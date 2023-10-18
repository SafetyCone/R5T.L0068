using System;

using R5T.T0178;
using R5T.T0179;


namespace R5T.L0068
{
    /// <inheritdoc cref="IRuntimeName"/>
    [StrongTypeImplementationMarker]
    public class RuntimeName : TypedBase<string>, IStrongTypeMarker,
        IRuntimeName
    {
        public RuntimeName(string value)
            : base(value)
        {
        }
    }
}