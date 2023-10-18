using System;


namespace R5T.L0068
{
    public class DotnetRuntimeNames : IDotnetRuntimeNames
    {
        #region Infrastructure

        public static IDotnetRuntimeNames Instance { get; } = new DotnetRuntimeNames();


        private DotnetRuntimeNames()
        {
        }

        #endregion
    }
}
