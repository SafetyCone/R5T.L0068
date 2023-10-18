using System;

using R5T.T0132;


namespace R5T.L0068.Extensions
{
    [FunctionalityMarker]
    public partial interface IStringOperator : IFunctionalityMarker
    {
        /// <inheritdoc cref="IRuntimeName"/>
        public IRuntimeName ToRuntimeName(string value)
        {
            var output = new RuntimeName(value);
            return output;
        }
    }
}
