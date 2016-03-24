using System;

namespace MicrosoftDynamics.Models
{
    public struct DynamicsAxTimeZone
    {
        internal DynamicsAxTimeZone(string name, int value, string description)
        {
            Description = description;
            Name = name;
            Value = value;
        }

        /// <summary>
        /// Time zone name as defined in <see cref="TimeZoneInfo.DisplayName"/> expect it has GMT instead of UTC
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Time zone name as definied in AX
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Value
        /// </summary>
        public int Value { get; }
    }
}