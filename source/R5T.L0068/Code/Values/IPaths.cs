using System;

using R5T.T0131;


namespace R5T.L0068
{
    [ValuesMarker]
    public partial interface IPaths : IValuesMarker
    {
        public string DotnetRuntimesDirectoryPath => @"C:\Program Files\dotnet\shared";
    }
}
