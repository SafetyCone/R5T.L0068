using System;


namespace R5T.L0068.Extensions
{
    public static class StringExtensions
    {
        /// <inheritdoc cref="IStringOperator.ToRuntimeName(string)"/>
        public static IRuntimeName ToRuntimeName(this string value)
        {
            return Instances.StringOperator_Extensions.ToRuntimeName(value);
        }
    }
}
