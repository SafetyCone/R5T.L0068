using System;
using System.Linq;

using R5T.T0132;
using R5T.T0172;

using ITargetFrameworkMoniker = R5T.T0218.ITargetFrameworkMoniker;


namespace R5T.L0068
{
    [FunctionalityMarker]
    public partial interface IDotnetPackPathsOperator : IFunctionalityMarker,
        F0138.IDotnetPackPathOperator
    {
        public IDocumentationXmlFilePath[] Get_DotnetPackDocumentationFilePaths(ITargetFrameworkMoniker targetFrameworkMoniker)
        {
            var dotnetPackNames = Instances.DotnetPackOperator.Get_DotnetPackNames(targetFrameworkMoniker);

            var output = dotnetPackNames
                .SelectMany(dotnetPackName =>
                {
                    var dotnetPackDirectoryPath = this.Get_DotnetPackDirectoryPath(
                        dotnetPackName,
                        targetFrameworkMoniker);

                    var output = Instances.DocumentationXmlFilePathOperator.Get_DocumentationXmlFilePaths_AssumeAllXmls(dotnetPackDirectoryPath);
                    return output;
                })
                .Now();

            return output;
        }

        public IAssemblyFilePath[] Get_DotnetPackAssemblyFilePaths(ITargetFrameworkMoniker targetFrameworkMoniker)
        {
            var dotnetPackNames = Instances.DotnetPackOperator.Get_DotnetPackNames(targetFrameworkMoniker);

            var output = dotnetPackNames
                .SelectMany(dotnetPackName =>
                {
                    var dotnetPackDirectoryPath = this.Get_DotnetPackDirectoryPath(
                        dotnetPackName,
                        targetFrameworkMoniker);

                    var output = Instances.AssemblyFilePathOperator.Get_AssemblyFilePaths(dotnetPackDirectoryPath);
                    return output;
                })
                .Now();

            return output;
        }
    }
}
