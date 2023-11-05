using System;


namespace R5T.L0068
{
    public class DotnetRuntimePathsOperator : IDotnetRuntimePathsOperator
    {
        #region Infrastructure

        public static IDotnetRuntimePathsOperator Instance { get; } = new DotnetRuntimePathsOperator();


        private DotnetRuntimePathsOperator()
        {
        }

        #endregion
    }
}
