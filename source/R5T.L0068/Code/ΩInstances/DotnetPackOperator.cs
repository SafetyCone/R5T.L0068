using System;


namespace R5T.L0068
{
    public class DotnetPackOperator : IDotnetPackOperator
    {
        #region Infrastructure

        public static IDotnetPackOperator Instance { get; } = new DotnetPackOperator();


        private DotnetPackOperator()
        {
        }

        #endregion
    }
}
