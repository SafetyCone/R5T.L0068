using System;


namespace R5T.L0068
{
    public class DotnetPackPathsOperator : IDotnetPackPathsOperator
    {
        #region Infrastructure

        public static IDotnetPackPathsOperator Instance { get; } = new DotnetPackPathsOperator();


        private DotnetPackPathsOperator()
        {
        }

        #endregion
    }
}
