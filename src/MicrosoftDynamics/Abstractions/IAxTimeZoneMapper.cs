using System;
using MicrosoftDynamics.Models;

namespace MicrosoftDynamics.Abstractions
{
    /// <summary>
    ///     Interface for mapping <see cref="DynamicsAxTimeZone" /> objects
    /// </summary>
    public interface IAxTimeZoneMapper
    {
        /// <summary>
        ///     The version the mapper is valid for
        /// </summary>
        string AxVersion { get; }

        /// <summary>
        ///     Get the <see cref="DynamicsAxTimeZone" /> object for the <paramref name="timeZoneDisplayName" />
        /// </summary>
        /// <remarks>
        ///     The <see cref="TimeZoneInfo.DisplayName" /> contains UTC and the <see cref="DynamicsAxTimeZone.Description" /> has
        ///     GMT. Prior to lookup
        ///     a string replace is done for UTC -> GMT
        /// </remarks>
        /// <param name="timeZoneDisplayName">Time zone display name</param>
        /// <returns>A <see cref="DynamicsAxTimeZone" /></returns>
        /// <exception cref="InvalidOperationException">If no object is found</exception>
        DynamicsAxTimeZone ConvertToAx(string timeZoneDisplayName);
    }
}