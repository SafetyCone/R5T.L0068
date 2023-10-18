using System;


namespace R5T.L0068
{
    public class DotnetRuntimeNameOperator : IDotnetRuntimeNameOperator
    {
        #region Infrastructure

        public static IDotnetRuntimeNameOperator Instance { get; } = new DotnetRuntimeNameOperator();


        private DotnetRuntimeNameOperator()
        {
        }

        #endregion
    }
}
