﻿using System;
using MicrosoftDynamics.Abstractions;
using MicrosoftDynamics.Models;

namespace MicrosoftDynamics
{
    public class DynamicsAxTimeZoneManager
    {
        static DynamicsAxTimeZoneManager()
        {
            Current = new DynamicsAxTimeZoneManager();
        }

        private DynamicsAxTimeZoneManager()
        {
        }

        public static DynamicsAxTimeZoneManager Current { get; private set; }

        public IAxTimeZoneMapper Create(string version)
        {
            if (DynamicsAxVersion.Ax2012.Equals(version))
            {
                return new AxTimeZoneMapper(version, Dynamics2012.TimeZones);
            }
            // TODO: Add more
            throw new InvalidOperationException($"'{version}' is not supported. See DynamicsAxVersion for valid values");
        }
    }
}