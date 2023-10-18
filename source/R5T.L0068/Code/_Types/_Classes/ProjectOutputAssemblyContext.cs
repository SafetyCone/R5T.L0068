using System;

using R5T.L0032.T000;
using R5T.T0172;

using ITargetFrameworkMoniker = R5T.T0218.ITargetFrameworkMoniker;


namespace R5T.L0068
{
    public class ProjectOutputAssemblyContext
    {
        public IProjectFilePath ProjectFilePath { get; set; }
        public IDocumentationXmlFilePath DocumentationXmlFilePath { get; set; }
        public IProjectSdkName ProjectSdkName { get; set; }
        public ITargetFrameworkMoniker TargetFrameworkMoniker { get; set; }
        public IRuntimeName RuntimeName { get; set; }
        public IAssemblyFilePath[] DependencyAssemblyFilePaths { get; set; }
    }
}
