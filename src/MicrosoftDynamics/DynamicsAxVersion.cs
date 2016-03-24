using System.Linq;

namespace MicrosoftDynamics
{
    /// <summary>
    ///     Microsoft Dynamics AX version
    /// </summary>
    /// <remarks>
    ///     Data from https://msdn.microsoft.com/en-us/library/hh881897.aspx
    /// </remarks>
    public static class DynamicsAxVersion
    {
        internal static readonly string[] SupportedVersions;

        public static readonly string AxLatest = "latest";
        public static readonly string Ax2012 = "2012";
        public static readonly string Ax2009 = "2009";
        public static readonly string Ax40 = "4.0";

        static DynamicsAxVersion()
        {
            // TODO: Add support for more version
            SupportedVersions = new[]
            {
                //AxLatest,
                Ax2012,
                //Ax2009,
                //Ax40
            };
        }

        public static bool IsVersionSupported(string version)
        {
            return SupportedVersions.Contains(version);
        }
    }
}