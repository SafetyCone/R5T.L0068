using System;


namespace R5T.L0068
{
    public class RuntimeDirectoryPathOperator : IRuntimeDirectoryPathOperator
    {
        #region Infrastructure

        public static IRuntimeDirectoryPathOperator Instance { get; } = new RuntimeDirectoryPathOperator();


        private RuntimeDirectoryPathOperator()
        {
        }

        #endregion
    }
}
