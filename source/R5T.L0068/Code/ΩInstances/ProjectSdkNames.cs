using System;


namespace R5T.L0068
{
    public class ProjectSdkNames : IProjectSdkNames
    {
        #region Infrastructure

        public static IProjectSdkNames Instance { get; } = new ProjectSdkNames();


        private ProjectSdkNames()
        {
        }

        #endregion
    }
}
