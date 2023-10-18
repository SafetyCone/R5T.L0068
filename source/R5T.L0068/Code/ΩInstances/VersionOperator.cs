using System;


namespace R5T.L0068
{
    public class VersionOperator : IVersionOperator
    {
        #region Infrastructure

        public static IVersionOperator Instance { get; } = new VersionOperator();


        private VersionOperator()
        {
        }

        #endregion
    }
}
