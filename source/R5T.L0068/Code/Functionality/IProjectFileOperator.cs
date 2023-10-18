using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using R5T.L0032.T000;
using R5T.N0000;
using R5T.T0132;
using R5T.T0172;
using R5T.T0172.Extensions;
using R5T.T0179.Extensions;
using R5T.T0227;

using ITargetFrameworkMoniker = R5T.T0218.ITargetFrameworkMoniker;


namespace R5T.L0068
{
    [FunctionalityMarker]
    public partial interface IProjectFileOperator : IFunctionalityMarker,
        L0067.IProjectFileOperator
    {
        private static F0112.IFilePathOperator F0112_FilePathOperator => F0112.FilePathOperator.Instance;
        private static F0115.IFilePathOperator F0115_FilePathOperator => F0115.FilePathOperator.Instance;


        public Task<(IProjectSdkName sdkName, ITargetFrameworkMoniker targetFrameworkMoniker)> Get_RuntimeTargetInformation(IProjectFilePath projectFilePath)
        {
            return this.In_QueryProjectFileContext(
                projectFilePath,
                Instances.ProjectXElementOperator.Get_RuntimeTargetInformation);
        }

        public (IProjectSdkName sdkName, ITargetFrameworkMoniker targetFrameworkMoniker) Get_RuntimeTargetInformation_Synchronous(IProjectFilePath projectFilePath)
        {
            var output = this.In_QueryProjectFileContext_Synchronous(
                projectFilePath,
                Instances.ProjectXElementOperator.Get_RuntimeTargetInformation);

            return output;
        }

        public IRuntimeName Get_RuntimeName_Synchronous(IProjectFilePath projectFilePath)
        {
            var (projectSdkName, targetFrameworkMoniker) = this.Get_RuntimeTargetInformation_Synchronous(projectFilePath);

            var output = Instances.DotnetRuntimeNameOperator.Get_RuntimeName(
                projectSdkName,
                targetFrameworkMoniker);

            return output;
        }

        public IRuntimeDirectoryPath Get_RuntimeDirectoryPath_Synchronous(
            IProjectFilePath projectFilePath,
            out (IProjectSdkName ProjectSdkName, ITargetFrameworkMoniker TargetFrameworkMoniker, IRuntimeName RuntimeName) context)
        {
            var (projectSdkName, targetFrameworkMoniker) = this.Get_RuntimeTargetInformation_Synchronous(projectFilePath);

            var runtimeName = Instances.DotnetRuntimeNameOperator.Get_RuntimeName(
                projectSdkName,
                targetFrameworkMoniker);

            var output = Instances.RuntimeDirectoryPathOperator.Get_RuntimeDirectoryPath(
                runtimeName,
                targetFrameworkMoniker);

            context = (projectSdkName, targetFrameworkMoniker, runtimeName);

            return output;
        }

        public IRuntimeDirectoryPath Get_RuntimeDirectoryPath_Synchronous(IProjectFilePath projectFilePath)
        {
            var output = this.Get_RuntimeDirectoryPath_Synchronous(
                projectFilePath,
                out _);

            return output;
        }

        /// <summary>
        /// <main>Using the publish convention (that output assemblies will be in the /bin/publish/ directory), get the corresponding output assembly file path for a project file path.</main>
        /// <para>Note: the SDK value of the project is required since the publish output can be in different locations based on the SDK
        /// (for example, Blazor WebAssembly projects output to the wwwroot directory).</para>
        /// </summary>
        public IAssemblyFilePath Get_OutputAssemblyFilePath_PublishConvention(
            IProjectFilePath projectFilePath,
            IProjectSdkName projectSdkName)
        {
            var assemblyFilePath = projectSdkName.Value switch
            {
                F0020.IProjectSdkStrings.BlazorWebAssembly_Constant => F0112_FilePathOperator.Get_PublishWwwRootFrameworkDirectoryOutputAssemblyFilePath(projectFilePath.Value),
                // Else
                _ => F0112_FilePathOperator.Get_PublishDirectoryOutputAssemblyFilePath(projectFilePath.Value),
            };

            var output = assemblyFilePath.ToAssemblyFilePath();
            return output;
        }

        public IDocumentationXmlFilePath Get_DocumentationXmlFilePath(
            IProjectFilePath projectFilePath,
            IProjectSdkName projectSdkName,
            IAssemblyFilePath assemblyFilePath)
        {
            var documentationFilePath = projectSdkName.Value switch
            {
                F0020.IProjectSdkStrings.BlazorWebAssembly_Constant => F0115_FilePathOperator.Get_DocumentationFilePath_ReleaseDirectory(projectFilePath),
                _ => Instances.DocumentationXmlFilePathOperator.Get_DocumentationFilePath_ForAssemblyFilePath(assemblyFilePath),
            };

            return documentationFilePath;
        }

        /// <summary>
        /// <inheritdoc cref="Get_OutputAssemblyFilePath_PublishConvention(IProjectFilePath, IProjectSdkName)" path="/summary/main"/>
        /// <para>Note: because the SDK value of the project is require, the file must exist so that it's SDK value can be determined.</para>
        /// </summary>
        public IAssemblyFilePath Get_OutputAssemblyFilePath_PublishConvention(FilePathExists<IProjectFilePath> projectFilePath)
        {
            var sdkName = this.Get_SdkName(projectFilePath.FilePath);

            var output = this.Get_OutputAssemblyFilePath_PublishConvention(
                projectFilePath.FilePath,
                sdkName);

            return output;
        }

        public void In_OutputAssemblyContext_PublishConvention(
            IProjectFilePath projectFilePath,
            IAssemblyFilePath outputAssemblyFilePath,
            IDocumentationXmlFilePath documentationXmlFilePath,
            IRuntimeDirectoryPath runtimeDirectoryPath,
            IProjectSdkName projectSdkName,
            IRuntimeName runtimeName,
            ITargetFrameworkMoniker targetFrameworkMoniker,
            Action<Assembly, ProjectOutputAssemblyContext> action)
        {
            var outputAssemblyDirectoryAssemblyFilePaths = Instances.AssemblyFilePathOperator.Get_AssemblyDirectoryAssemblyFilePaths(outputAssemblyFilePath);

            var runtimeDirectoryAssemblyFilePaths = Instances.AssemblyFilePathOperator.Get_AssemblyFilePaths(runtimeDirectoryPath);

            // If the runtime name is not the base core runtime, add those.
            var isCoreRuntime = runtimeName.Equals(Instances.DotnetRuntimeNames.Microsoft_NETCore_App);

            IAssemblyFilePath[] coreRuntimeAssemblyFilePaths;
            if (isCoreRuntime)
            {
                coreRuntimeAssemblyFilePaths = Instances.ArrayOperator.Empty<IAssemblyFilePath>();
            }
            else
            {
                var coreRuntimeDirectoryPath = Instances.RuntimeDirectoryPathOperator.Get_RuntimeDirectoryPath(
                    Instances.DotnetRuntimeNames.Microsoft_NETCore_App,
                    targetFrameworkMoniker);

                coreRuntimeAssemblyFilePaths = Instances.AssemblyFilePathOperator.Get_AssemblyFilePaths(coreRuntimeDirectoryPath);
            }

            // If in any dependency there is a Sdk="Microsoft.NET.Sdk.Razor" or <FrameworkReference Include="Microsoft.AspNetCore.App" />,
            // then the publish result will be missing many assemblies since publish is counting on those assemblies being available from the runtime.
            var isAspNetRuntimeDependent = true; // TODO

            IAssemblyFilePath[] aspNetRuntimeAssemblyFilePaths;
            if(isAspNetRuntimeDependent)
            {
                var aspNetRuntimeDirectoryPath = Instances.RuntimeDirectoryPathOperator.Get_RuntimeDirectoryPath(
                    Instances.DotnetRuntimeNames.Microsoft_AspNetCore_App,
                    targetFrameworkMoniker);

                aspNetRuntimeAssemblyFilePaths = Instances.AssemblyFilePathOperator.Get_AssemblyFilePaths(aspNetRuntimeDirectoryPath);
            }
            else
            {
                aspNetRuntimeAssemblyFilePaths = Instances.ArrayOperator.Empty<IAssemblyFilePath>();
            }

            // May not be distinct.
            var allDependencyAssemblyFilePaths = 
                // Always start with the core runtime assembly file paths.
                coreRuntimeAssemblyFilePaths
                .Append(aspNetRuntimeAssemblyFilePaths)
                .Append(outputAssemblyDirectoryAssemblyFilePaths)
                // Put the runtime directory assembly file paths after the output assembly directory assembly file paths in the hopes that a distinct operation will use the first
                // encountered value for a given file name.
                .Append(runtimeDirectoryAssemblyFilePaths)
                .ToArray();

            // Need to make the file paths distinct.
            // For now, make distinct by filename.
            var dependencyAssemblyFilePathsByFileName = new Dictionary<string, IAssemblyFilePath>();
            foreach (var dependencyAssemblyFilePath in allDependencyAssemblyFilePaths)
            {
                var fileName = Instances.PathOperator.Get_FileName(dependencyAssemblyFilePath.Value);

                dependencyAssemblyFilePathsByFileName.TryAdd(
                    fileName,
                    dependencyAssemblyFilePath);
            }

            var dependencyAssemblyFilePaths = dependencyAssemblyFilePathsByFileName.Values.ToArray();

            var projectOutputAssemblyContext = new ProjectOutputAssemblyContext
            {
                DependencyAssemblyFilePaths = dependencyAssemblyFilePaths,
                DocumentationXmlFilePath = documentationXmlFilePath,
                ProjectFilePath = projectFilePath,
                ProjectSdkName = projectSdkName,
                RuntimeName = runtimeName,
                TargetFrameworkMoniker = targetFrameworkMoniker,
            };

            Instances.ReflectionOperator.In_AssemblyContext(
                outputAssemblyFilePath,
                dependencyAssemblyFilePaths,
                assembly =>
                {
                    action(assembly, projectOutputAssemblyContext);
                });
        }

        public void In_OutputAssemblyContext_PublishConvention(
            IProjectFilePath projectFilePath,
            IAssemblyFilePath outputAssemblyFilePath,
            IDocumentationXmlFilePath documentationXmlFilePath,
            Action<Assembly, ProjectOutputAssemblyContext> action)
        {
            var runtimeDirectoryPath = this.Get_RuntimeDirectoryPath_Synchronous(
                projectFilePath,
                out var projectRuntimeContext);

            this.In_OutputAssemblyContext_PublishConvention(
                projectFilePath,
                outputAssemblyFilePath,
                documentationXmlFilePath,
                runtimeDirectoryPath,
                projectRuntimeContext.ProjectSdkName,
                projectRuntimeContext.RuntimeName,
                projectRuntimeContext.TargetFrameworkMoniker,
                action);
        }

        public void In_OutputAssemblyContext_PublishConvention(
            IProjectFilePath projectFilePath,
            Action<Assembly, ProjectOutputAssemblyContext> action)
        {
            var runtimeDirectoryPath = this.Get_RuntimeDirectoryPath_Synchronous(
                projectFilePath,
                out var projectRuntimeContext);

            var outputAssemblyFilePath = this.Get_OutputAssemblyFilePath_PublishConvention(
                projectFilePath,
                projectRuntimeContext.ProjectSdkName);

            var documentationXmlFilePath = this.Get_DocumentationXmlFilePath(
                projectFilePath,
                projectRuntimeContext.ProjectSdkName,
                outputAssemblyFilePath);

            this.In_OutputAssemblyContext_PublishConvention(
                projectFilePath,
                outputAssemblyFilePath,
                documentationXmlFilePath,
                runtimeDirectoryPath,
                projectRuntimeContext.ProjectSdkName,
                projectRuntimeContext.RuntimeName,
                projectRuntimeContext.TargetFrameworkMoniker,
                action);
        }
    }
}
