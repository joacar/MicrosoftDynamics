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
        ///     Get the <see cref="DynamicsAxTimeZone" /> object for the <paramref name="standardName" />
        /// </summary>
        /// <param name="standardName">Time zone standard name</param>
        /// <returns>A <see cref="DynamicsAxTimeZone" /></returns>
        /// <exception cref="InvalidOperationException">If no object is found</exception>
        DynamicsAxTimeZone ConvertToAx(string standardName);

        /// <summary>
        ///     Get the <see cref="TimeZoneInfo" /> object from <paramref name="name" />
        /// </summary>
        /// <remarks>
        ///     The parameter <paramref name="name" /> should be the format of <see cref="DynamicsAxTimeZone.Name" />
        /// </remarks>
        /// <param name="name">The name for the Dynamics AX time zone</param>
        /// <returns>A <see cref="TimeZoneInfo" /></returns>
        /// <exception cref="InvalidOperationException">If no object is found</exception>
        TimeZoneInfo ConvertFromAx(string name);
    }
}