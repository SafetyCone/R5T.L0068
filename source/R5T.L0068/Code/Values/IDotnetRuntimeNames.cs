using System;

using R5T.T0131;

using R5T.L0068.Extensions;


namespace R5T.L0068
{
    [ValuesMarker]
    public partial interface IDotnetRuntimeNames : IValuesMarker
    {
#pragma warning disable IDE1006 // Naming Styles
        public L0053.IRuntimeNames _Platform => L0053.RuntimeNames.Instance;
#pragma warning restore IDE1006 // Naming Styles


        /// <inheritdoc cref="L0053.IRuntimeNames.Microsoft_AspNetCore_All"/>
        [Obsolete("Use .App instead.")]
        public IRuntimeName Microsoft_AspNetCore_All => _Platform.Microsoft_AspNetCore_All.ToRuntimeName();

        /// <inheritdoc cref="L0053.IRuntimeNames.Microsoft_AspNetCore_App"/>
        public IRuntimeName Microsoft_AspNetCore_App => _Platform.Microsoft_AspNetCore_App.ToRuntimeName();

        /// <inheritdoc cref="L0053.IRuntimeNames.Microsoft_NETCore_App"/>
        public IRuntimeName Microsoft_NETCore_App => _Platform.Microsoft_NETCore_App.ToRuntimeName();

        /// <inheritdoc cref="L0053.IRuntimeNames.Microsoft_WindowsDesktop_App"/>
        public IRuntimeName Microsoft_WindowsDesktop_App => _Platform.Microsoft_WindowsDesktop_App.ToRuntimeName();
    }
}
