using System;


namespace R5T.L0068
{
    public class DotnetPackNames : IDotnetPackNames
    {
        #region Infrastructure

        public static IDotnetPackNames Instance { get; } = new DotnetPackNames();


        private DotnetPackNames()
        {
        }

        #endregion
    }
}
