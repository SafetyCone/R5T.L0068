using System;


namespace R5T.L0068
{
    public class ProjectSdkNameOperator : IProjectSdkNameOperator
    {
        #region Infrastructure

        public static IProjectSdkNameOperator Instance { get; } = new ProjectSdkNameOperator();


        private ProjectSdkNameOperator()
        {
        }

        #endregion
    }
}
