using System;
using System.Linq;
using R5T.T0132;
using R5T.T0172;

using ITargetFrameworkMoniker = R5T.T0218.ITargetFrameworkMoniker;


namespace R5T.L0068
{
    [FunctionalityMarker]
    public partial interface IDotnetRuntimePathsOperator : IFunctionalityMarker
    {
        /// <summary>
        /// <para>Chooses <see cref="Get_RuntimeAssemblyFilePaths_InOrder(ITargetFrameworkMoniker)"/> as the default.</para>
        /// <inheritdoc cref="Get_RuntimeAssemblyFilePaths_InOrder(ITargetFrameworkMoniker)" path="/summary"/>
        /// <para>Note: there may be duplicates in the output depending on how Microsoft thought to include assemblies in runtimes.
        /// There shouldn't be, but there might be. To de-duplicate, prefix the runtime assembly file paths with the target project's assembly file paths,
        /// and keep the first file path with the same file name.</para>
        /// </summary>
        public IAssemblyFilePath[] Get_RuntimeAssemblyFilePaths(ITargetFrameworkMoniker targetFrameworkMoniker)
        {
            var output = this.Get_RuntimeAssemblyFilePaths_InOrder(targetFrameworkMoniker);
            return output;
        }

        /// <summary>
        /// Gets runtime assembly file paths in order from most specific to least specific,
        /// with order provided by the order of runtimes from <see cref="IDotnetRuntimeNameOperator.Get_RuntimeNames_InOrder(ITargetFrameworkMoniker)"/>,
        /// and then alphabetically within each runtime.
        /// </summary>
        public IAssemblyFilePath[] Get_RuntimeAssemblyFilePaths_InOrder(ITargetFrameworkMoniker targetFrameworkMoniker)
        {
            // Order of assemblies is provided by order of runtimes (and then alphabetical within each runtime).
            var runtimeNames = Instances.DotnetRuntimeNameOperator.Get_RuntimeNames_InOrder(targetFrameworkMoniker);

            var output = runtimeNames
                .SelectMany(runtimeName =>
                {
                    var runtimeDirectoryPath = Instances.RuntimeDirectoryPathOperator.Get_RuntimeDirectoryPath(
                        runtimeName,
                        targetFrameworkMoniker);

                    var output = Instances.AssemblyFilePathOperator.Get_AssemblyFilePaths(runtimeDirectoryPath);
                    return output;
                })
                .Now();

            return output;
        }
    }
}
