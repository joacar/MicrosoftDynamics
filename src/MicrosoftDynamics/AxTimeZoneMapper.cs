using System;
using System.Collections.Generic;
using System.Linq;
using MicrosoftDynamics.Abstractions;
using MicrosoftDynamics.Models;

namespace MicrosoftDynamics
{
    public class AxTimeZoneMapper : IAxTimeZoneMapper
    {
        public AxTimeZoneMapper(string axVersion, IReadOnlyCollection<DynamicsAxTimeZone> timeZones)
        {
            AxVersion = axVersion;
            TimeZones = timeZones;
        }

        public virtual IReadOnlyCollection<DynamicsAxTimeZone> TimeZones { get; }

        public string AxVersion { get; }

        public virtual DynamicsAxTimeZone ConvertToAx(string timeZoneDisplayName)
        {
            // The value returned from TimeZoneInfo.DisplayName begings with (UTC..) and Dyanmics objects has (GMT..)
            timeZoneDisplayName = timeZoneDisplayName.Replace("(UTC", "(GMT");
            var timeZone = TimeZones.FirstOrDefault(t => t.Description.Equals(timeZoneDisplayName));
            if (timeZone.Equals(DynamicsAxTimeZone.Null))
            {
                throw new InvalidOperationException($"No time zone found for '{timeZoneDisplayName}'");
            }
            return timeZone;
        }
    }
}