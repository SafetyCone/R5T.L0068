using System;
using System.Collections.Generic;
using System.Linq;

using R5T.T0132;
using R5T.T0159;
using R5T.T0180;
using R5T.T0180.Extensions;
using R5T.T0215;
using R5T.T0216;
using R5T.T0216.Extensions;
using R5T.T0218;
using R5T.T0227;
using R5T.T0227.Extensions;


namespace R5T.L0068
{
    [FunctionalityMarker]
    public partial interface IRuntimeDirectoryPathOperator : IFunctionalityMarker
    {
        public IDirectoryPath Get_RuntimeRootDirectoryPath(IRuntimeName runtimeName)
        {
            var runtimeRootDirectoryName = this.Get_RuntimeDirectoryName(runtimeName);

            var output = Instances.PathOperator.Get_DirectoryPath(
                Instances.Paths.DotnetRuntimesDirectoryPath,
                runtimeRootDirectoryName.Value)
                .ToDirectoryPath();

            return output;
        }

        public IDirectoryName Get_RuntimeDirectoryName(IRuntimeName runtimeName)
        {
            // The directory name is just the runtime name.
            var output = runtimeName.Value.ToDirectoryName();
            return output;
        }

        public IDirectoryPath[] Get_VersionedDotnetRuntimeDirectoryPaths(IDirectoryPath dotnetRuntimeRootDirectoryPath)
        {
            var output = Instances.FileSystemOperator.Enumerate_ChildDirectoryPaths(
                dotnetRuntimeRootDirectoryPath)
                .Now();

            return output;
        }

        public Dictionary<Version, IVersionedDirectoryPath> Get_VersionedDotnetRuntimeDirectoryPathsByVersion(IDirectoryPath dotnetRuntimeRootDirectoryPath)
        {
            var versionedDotnetRuntimeDirectoryPaths = this.Get_VersionedDotnetRuntimeDirectoryPaths(dotnetRuntimeRootDirectoryPath)
                .Where(versionedDirectoryPath =>
                {
                    var versionedDirectoryName = Instances.VersionedDirectoryPathOperator._Platform.Get_VersionedDirectoryName(versionedDirectoryPath.Value);

                    var versionName = Instances.VersionedDirectoryNameOperator._Platform.Get_VersionName(versionedDirectoryName);

                    var isSuffxed = Instances.VersionOperator.Is_Suffixed(versionName);

                    var output = !isSuffxed;
                    return output;
                })
                .Select(x => x.Value.ToVersionedDirectoryPath());

            var output = Instances.VersionedDirectoryPathOperator.Get_VersionedDirectoryPathsByVersion(versionedDotnetRuntimeDirectoryPaths);
            return output;
        }

        public Dictionary<Version, IVersionedDirectoryPath> Get_VersionedDotnetRuntimeDirectoryPathsByVersion(IRuntimeName runtimeName)
        {
            var dotnetRuntimeRootDirectoryPath = this.Get_RuntimeRootDirectoryPath(runtimeName);

            var output = this.Get_VersionedDotnetRuntimeDirectoryPathsByVersion(
                dotnetRuntimeRootDirectoryPath);

            return output;
        }

        public IRuntimeDirectoryPath Get_RuntimeDirectoryPath(
            IRuntimeName runtimeName,
            Version version)
        {
            var runtimeDirectoryName = this.Get_RuntimeDirectoryName(runtimeName);

            var versionDirectoryName = Instances.VersionedDirectoryNameOperator.Get_VersionedDirectoryName(version);

            var output = Instances.PathOperator.Get_DirectoryPath(
                Instances.Paths.DotnetRuntimesDirectoryPath,
                runtimeDirectoryName.Value,
                versionDirectoryName.Value)
                .ToRuntimeDirectoryPath();

            return output;
        }

        public IRuntimeDirectoryPath Get_RuntimeDirectoryPath(
            IRuntimeName runtimeName,
            ITargetFrameworkMoniker targetFrameworkMoniker)
        {
            var versionedDirectoryPathsByVersion = this.Get_VersionedDotnetRuntimeDirectoryPathsByVersion(runtimeName);

            var dotnetMajorVersion = Instances.TargetFrameworkMonikerOperator.Get_DotnetMajorVersion(targetFrameworkMoniker);

            var highestSubVersion = Instances.VersionOperator.Choose_HighestSubVersionOf(
                versionedDirectoryPathsByVersion.Keys,
                dotnetMajorVersion)
                ?? throw new Exception($"No subversions found for dotnet runtime '{runtimeName}', major version {dotnetMajorVersion}.");

            var output = this.Get_RuntimeDirectoryPath(
                runtimeName,
                highestSubVersion);

            return output;
        }
    }
}
