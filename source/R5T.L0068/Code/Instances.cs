using System;


namespace R5T.L0068
{
    public static class Instances
    {
        public static L0053.IArrayOperator ArrayOperator => L0053.ArrayOperator.Instance;
        public static L0057.IAssemblyFilePathOperator AssemblyFilePathOperator => L0057.AssemblyFilePathOperator.Instance;
        public static L0056.IDocumentationXmlFilePathOperator DocumentationXmlFilePathOperator => L0056.DocumentationXmlFilePathOperator.Instance;
        public static IDotnetPackNames DotnetPackNames => L0068.DotnetPackNames.Instance;
        public static IDotnetRuntimeNameOperator DotnetRuntimeNameOperator => L0068.DotnetRuntimeNameOperator.Instance;
        public static IDotnetRuntimeNames DotnetRuntimeNames => L0068.DotnetRuntimeNames.Instance;
        public static L0057.IFileSystemOperator FileSystemOperator => L0057.FileSystemOperator.Instance;
        public static L0057.IPathOperator PathOperator => L0057.PathOperator.Instance;
        public static IPaths Paths => L0068.Paths.Instance;
        public static IProjectSdkNames ProjectSdkNames => L0068.ProjectSdkNames.Instance;
        public static IProjectXElementOperator ProjectXElementOperator => L0068.ProjectXElementOperator.Instance;
        public static F0020.IProjectXmlOperator ProjectXmlOperator => F0020.ProjectXmlOperator.Instance;
        public static L0057.IReflectionOperator ReflectionOperator => L0057.ReflectionOperator.Instance;
        public static IRuntimeDirectoryPathOperator RuntimeDirectoryPathOperator => L0068.RuntimeDirectoryPathOperator.Instance;
        public static L0057.IStringOperator StringOperator => L0057.StringOperator.Instance;
        public static Extensions.IStringOperator StringOperator_Extensions => Extensions.StringOperator.Instance;
        public static ITargetFrameworkMonikerOperator TargetFrameworkMonikerOperator => L0068.TargetFrameworkMonikerOperator.Instance;
        public static ITargetFrameworkMonikerTokens TargetFrameworkMonikerTokens => L0068.TargetFrameworkMonikerTokens.Instance;
        public static T0216.F001.IVersionedDirectoryNameOperator VersionedDirectoryNameOperator => T0216.F001.VersionedDirectoryNameOperator.Instance;
        public static T0216.F001.IVersionedDirectoryPathOperator VersionedDirectoryPathOperator => T0216.F001.VersionedDirectoryPathOperator.Instance;
        public static IVersionOperator VersionOperator => L0068.VersionOperator.Instance;
    }
}