﻿using System;
using System.Collections.Generic;
using MicrosoftDynamics.Abstractions;

using static System.Diagnostics.Debug;

namespace MicrosoftDynamics.Models
{
    /// <summary>
    ///     Class for holding <see cref="DynamicsAxTimeZone" /> objects for version <see cref="DynamicsAxVersion.Ax2012" />
    /// </summary>
    /// <remarks>
    ///     https://msdn.microsoft.com/en-us/library/gg848856.aspx
    /// </remarks>
    public class Dynamics2012 : IAxTimeZoneMapper
    {
        private readonly IDictionary<string, TimeZoneInfo> mapDynamics;

        private readonly IDictionary<string, DynamicsAxTimeZone> mapTimeZones;

        /// <summary>
        ///     List of <see cref="DynamicsAxTimeZone" /> objects
        /// </summary>
        private readonly IReadOnlyCollection<DynamicsAxTimeZone> timeZones;

        static Dynamics2012()
        {
            Current = new Dynamics2012();
        }

        /// <summary>
        ///     Create a new instance of <see cref="Dynamics2012" />
        /// </summary>
        private Dynamics2012()
        {
            timeZones = CreateTimeZones();
            WriteLine($"Registered {timeZones.Count} time zones for '{AxVersion}'");
            mapDynamics = new Dictionary<string, TimeZoneInfo>(timeZones.Count);
            mapTimeZones = new Dictionary<string, DynamicsAxTimeZone>(timeZones.Count);
            // Build mapping
            BuildMapping();
        }

        /// <summary>
        ///     Singleton instance of <see cref="Dynamics2012" />
        /// </summary>
        public static Dynamics2012 Current { get; private set; }

        public string AxVersion { get; } = DynamicsAxVersion.Ax2012;

        public DynamicsAxTimeZone ConvertToAx(string standardName)
        {
            if (!mapTimeZones.ContainsKey(standardName))
            {
                throw new InvalidOperationException($"Could not find any value for '{standardName}'");
            }
            return mapTimeZones[standardName];
        }

        public TimeZoneInfo ConvertFromAx(string name)
        {
            if (!mapDynamics.ContainsKey(name))
            {
                throw new InvalidOperationException($"Could not find any value for '{name}'");
            }
            return mapDynamics[name];
        }

        private void BuildMapping()
        {
            foreach (var dynamicsAxTimeZone in timeZones)
            {
                foreach (var systemTimeZone in TimeZoneInfo.GetSystemTimeZones())
                {
                    // Patch name to establish relation
                    var patch = PatchDisplayName(dynamicsAxTimeZone.Description, true);
                    if (patch.Equals(systemTimeZone.DisplayName))
                    {
                        mapDynamics[dynamicsAxTimeZone.Name] = systemTimeZone;
                        mapTimeZones[systemTimeZone.StandardName] = dynamicsAxTimeZone;
                        break;
                    }
                }
            }
            WriteLine($"Established mapping to TimeZoneInfo objects for {mapDynamics.Count} time zones");
        }

        private static List<DynamicsAxTimeZone> CreateTimeZones()
        {
            return new List<DynamicsAxTimeZone>
            {
                new DynamicsAxTimeZone("GMTMINUS1200INTERNATIONALDATELINEWEST", 24,
                    "(GMT-12:00) International Date Line West"),
                new DynamicsAxTimeZone("GMTMINUS1100COORDINATEDUNIVERSALTIME", 99,
                    "(GMT-11:00) Coordinated Universal Time-11"),
                new DynamicsAxTimeZone("GMTMINUS1000HAWAII", 39, "(GMT-10:00) Hawaii"),
                new DynamicsAxTimeZone("GMTMINUS0900ALASKA", 2, "(GMT-09:00) Alaska"),
                new DynamicsAxTimeZone("GMTMINUS0800PACIFICTIME", 58, "(GMT-08:00) Pacific Time (US & Canada)"),
                new DynamicsAxTimeZone("GMTMINUS0800TIJUANA_BAJACALIFORNIA", 59, "(GMT-08:00) Tijuana, Baja California"),
                new DynamicsAxTimeZone("GMTMINUS0700ARIZONA", 75, "(GMT-07:00) Arizona"),
                new DynamicsAxTimeZone("GMTMINUS0700MOUNTAINTIME", 47, "(GMT-07:00) Mountain Time (US & Canada)"),
                new DynamicsAxTimeZone("GMTMINUS0700CHIHUAHUA_LAPAZ_MAZATLAN", 48,
                    "(GMT-07:00) Chihuahua, La Paz, Mazatlan"),
                new DynamicsAxTimeZone("GMTMINUS0600CENTRALAMERICA", 15, "(GMT-06:00) Central America"),
                new DynamicsAxTimeZone("GMTMINUS0600CENTRALTIME", 21, "(GMT-06:00) Central Time (US & Canada)"),
                new DynamicsAxTimeZone("GMTMINUS0600GUADALAJARA_MEXICOCITY", 22,
                    "(GMT-06:00) Guadalajara, Mexico City, Monterrey"),
                new DynamicsAxTimeZone("GMTMINUS0600SASKATCHEWAN", 11, "(GMT-06:00) Saskatchewan"),
                new DynamicsAxTimeZone("GMTMINUS0500BOGOTA_LIMA_QUITO_RIOBRANCO", 63,
                    "(GMT-05:00) Bogota, Lima, Quito, Rio Branco"),
                new DynamicsAxTimeZone("GMTMINUS0500EASTERNTIME", 29, "(GMT-05:00) Eastern Time (US & Canada)"),
                new DynamicsAxTimeZone("GMTMINUS0500INDIANA", 74, "(GMT-05:00) Indiana (East)"),
                new DynamicsAxTimeZone("GMTMINUS0430CARACAS", 85, "(GMT-04:30) Caracas"),
                new DynamicsAxTimeZone("GMTMINUS0400ASUNCION", 95, "(GMT-04:00) Asuncion"),
                new DynamicsAxTimeZone("GMTMINUS0400ATLANTICTIME", 6, "(GMT-04:00) Atlantic Time (Canada)"),
                new DynamicsAxTimeZone("GMTMINUS0400LAPAZ", 64, "(GMT-04:00) La Paz"),
                new DynamicsAxTimeZone("GMTMINUS0400MANAUS", 17, "(GMT-04:00) Manaus"),
                new DynamicsAxTimeZone("GMTMINUS0400SANTIAGO", 57, "(GMT-04:00) Santiago"),
                new DynamicsAxTimeZone("GMTMINUS0330NEWFOUNDLAND", 54, "(GMT-03:30) Newfoundland"),
                new DynamicsAxTimeZone("GMTMINUS0300BRASILIA", 28, "(GMT-03:00) Brasilia"),
                new DynamicsAxTimeZone("GMTMINUS0300BUENOSAIRES", 86, "(GMT-03:00) Buenos Aires"),
                new DynamicsAxTimeZone("GMTMINUS0300BUENOSAIRES_GEORGETOWN", 62, "(GMT-03:00) Buenos Aires, Georgetown"),
                new DynamicsAxTimeZone("GMTMINUS0300GREENLAND", 36, "(GMT-03:00) Greenland"),
                new DynamicsAxTimeZone("GMTMINUS0300MONTEVIDEO", 83, "(GMT-03:00) Montevideo"),
                new DynamicsAxTimeZone("GMTMINUS0300_SALVADOR", 87, "(GMT-03:00) Salvador"),
                new DynamicsAxTimeZone("GMTMINUS0200MIDATLANTIC", 45, "(GMT-02:00) Mid-Atlantic"),
                new DynamicsAxTimeZone("GMTMINUS0100AZORES", 10, "(GMT-01:00) Azores"),
                new DynamicsAxTimeZone("GMTMINUS0100CAPEVERDIS", 12, "(GMT-01:00) Cape Verde Is."),
                new DynamicsAxTimeZone("GMT_CASABLANCA", 93, "(GMT) Casablanca"),
                new DynamicsAxTimeZone("GMT_COORDINATEDUNIVERSALTIME", 89, "(GMT) Coordinated Universal Time"),
                new DynamicsAxTimeZone("GMT_CASABLANCA_MONTROVIA_REYKJAVIK", 37, "(GMT) Casablanca, Monrovia, Reykjavik"),
                new DynamicsAxTimeZone("GMT_DUBLIN_EDINBURGH_LISBON_LONDON", 35,
                    "(GMT) Greenwich Mean Time : Dublin, Edinburgh, Lisbon, London"),
                new DynamicsAxTimeZone("GMTPLUS0100_AMSTERDAM_BERLIN_BERN_ROME", 79,
                    "(GMT+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna"),
                new DynamicsAxTimeZone("GMTPLUS0100BELGRADE_BRATISLAVA_BUDAPEST", 18,
                    "(GMT+01:00) Belgrade, Bratislava, Budapest, Ljubljana, Prague"),
                new DynamicsAxTimeZone("GMTPLUS0100BRUSSELS_COPENHAGEN_MADRID", 60,
                    "(GMT+01:00) Brussels, Copenhagen, Madrid, Paris"),
                new DynamicsAxTimeZone("GMTPLUS0100SARAJEVO_SKOPJE_WARSAW_ZAGREB", 19,
                    "(GMT+01:00) Sarajevo, Skopje, Warsaw, Zagreb"),
                new DynamicsAxTimeZone("GMTPLUS0100TRIPOLI", 101, "(GMT+01:00) Tripoli"),
                new DynamicsAxTimeZone("GMTPLUS0100WESTCENTRALAFRICA", 78, "(GMT+01:00) West Central Africa"),
                new DynamicsAxTimeZone("GMTPLUS0200AMMAN", 43, "(GMT+02:00) Amman"),
                new DynamicsAxTimeZone("GMTPLUS0200ATHENS_BUCHAREST_ISTANBUL", 38,
                    "(GMT+02:00) Athens, Bucharest, Istanbul"),
                new DynamicsAxTimeZone("GMTPLUS0200BEIRUT", 46, "(GMT+02:00) Beirut"),
                new DynamicsAxTimeZone("GMTPLUS0200MINSK", 27, "(GMT+02:00) Minsk"),
                new DynamicsAxTimeZone("GMTPLUS0200CAIRO", 30, "(GMT+02:00) Cairo"),
                new DynamicsAxTimeZone("GMTPLUS0200_DAMASCUS", 96, "(GMT+02:00) Damascus"),
                new DynamicsAxTimeZone("GMTPLUS0200HARARE_PRETORIA", 68, "(GMT+02:00) Harare, Pretoria"),
                new DynamicsAxTimeZone("GMTPLUS0200HELSINKI_KYIV_RIGA_VILNIUS", 33,
                    "(GMT+02:00) Helsinki, Kyiv, Riga, Sofia, Tallinn, Vilnius"),
                new DynamicsAxTimeZone("GMTPLUS0200ISTANBUL", 98, "(GMT+02:00) Istanbul"),
                new DynamicsAxTimeZone("GMTPLUS0200JERUSALEM", 42, "(GMT+02:00) Jerusalem"),
                new DynamicsAxTimeZone("GMTPLUS0200WINDHOEK", 51, "(GMT+02:00) Windhoek"),
                new DynamicsAxTimeZone("GMTPLUS0300BAGHDAD", 5, "(GMT+03:00) Baghdad"),
                new DynamicsAxTimeZone("GMT_PLUS0300KALININGRAD_MINSK", 90, "(GMT+03:00) Kaliningrad, Minsk"),
                new DynamicsAxTimeZone("GMTPLUS0300KUWAIT_RIYADH", 3, "(GMT+03:00) Kuwait, Riyadh"),
                new DynamicsAxTimeZone("GMTPLUS0300MOSCOW_STPETERSBURG_VOLGOGRAD", 61,
                    "(GMT+04:00) Moscow, St. Petersburg, Volgograd"),
                new DynamicsAxTimeZone("GMTPLUS0400PORTLOUIS", 92, "(GMT+04:00) Port Louis"),
                new DynamicsAxTimeZone("GMTPLUS0300NAIROBI", 25, "(GMT+03:00) Nairobi"),
                new DynamicsAxTimeZone("GMTPLUS0300TBILISI", 34, "(GMT+03:00) Tbilisi"),
                new DynamicsAxTimeZone("GMTPLUS0330TEHRAN", 41, "(GMT+03:30) Tehran"),
                new DynamicsAxTimeZone("GMTPLUS0400ABUDHABI_MUSCAT", 4, "(GMT+04:00) Abu Dhabi, Muscat"),
                new DynamicsAxTimeZone("GMTPLUS0400BAKU", 9, "(GMT+04:00) Baku"),
                new DynamicsAxTimeZone("GMTPLUS0400CAUCASUSSTANDARDTIME", 84, "(GMT+04:00) Yerevan"),
                new DynamicsAxTimeZone("GMTPLUS0400YEREVAN", 13, "(GMT+04:00) Yerevan (Do not use)"),
                new DynamicsAxTimeZone("GMTPLUS0430KABUL", 1, "(GMT+04:30) Kabul"),
                new DynamicsAxTimeZone("GMTPLUS0500EKATERINBURG", 31, "(GMT+05:00) Ekaterinburg"),
                new DynamicsAxTimeZone("GMTPLUS0500ISLAMABAD_KARACHI", 94, "(GMT+05:00) Islamabad, Karachi"),
                new DynamicsAxTimeZone("GMTPLUS0500ISLAMABAD_KARACHI_TASHKENT", 80, "(GMT+05:00) Tashkent"),
                new DynamicsAxTimeZone("GMTPLUS0530CHENNAI_KOLKATA_MUMBAI", 40,
                    "(GMT+05:30) Chennai, Kolkata, Mumbai, New Delhi"),
                new DynamicsAxTimeZone("GMTPLUS0530SRIJAYAWARDENEPURA", 69, "(GMT+05:30) Sri Jayawardenepura"),
                new DynamicsAxTimeZone("GMTPLUS0545KATHMANDU", 52, "(GMT+05:45) Kathmandu"),
                new DynamicsAxTimeZone("GMTPLUS0600ALMATY_NOVOSIBIRSK", 50, "(GMT+06:00) Almaty, Novosibirsk"),
                new DynamicsAxTimeZone("GMTPLUS0600ASTANA_DHAKA", 16, "(GMT+06:00) Astana, Dhaka"),
                new DynamicsAxTimeZone("GMTPLUS0600DHAKA", 88, "(GMT+06:00) Dhaka"),
                new DynamicsAxTimeZone("GMTPLUS0630_YANGON", 49, "(GMT+06:30) Yangon (Rangoon)"),
                new DynamicsAxTimeZone("GMTPLUS0700_BANGKOK_HANOI_JAKARTA", 66, "(GMT+07:00) Bangkok, Hanoi, Jakarta"),
                new DynamicsAxTimeZone("GMTPLUS0700KRASNOYARSK", 56, "(GMT+07:00) Krasnoyarsk"),
                new DynamicsAxTimeZone("GMTPLUS0800BEIJING_CHONGQING_HONGKONG", 23,
                    "(GMT+08:00) Beijing, Chongqing, Hong Kong, Urumqi"),
                new DynamicsAxTimeZone("GMTPLUS0800IRKUTSK_ULAANBATAAR", 55, "(GMT+08:00) Irkutsk, Ulaan Bataar"),
                new DynamicsAxTimeZone("GMTPLUS0800_ULAANBAATAR", 97, "(GMT+08:00) Ulaanbaatar"),
                new DynamicsAxTimeZone("GMTPLUS0800KUALALUMPUR_SINGAPORE", 67, "(GMT+08:00) Kuala Lumpur, Singapore"),
                new DynamicsAxTimeZone("GMTPLUS0800PERTH", 77, "(GMT+08:00) Perth"),
                new DynamicsAxTimeZone("GMTPLUS0800TAIPEI", 70, "(GMT+08:00) Taipei"),
                new DynamicsAxTimeZone("GMTPLUS0900OSAKA_SAPPORO_TOKYO", 72, "(GMT+09:00) Osaka, Sapporo, Tokyo"),
                new DynamicsAxTimeZone("GMTPLUS0900SEOUL", 44, "(GMT+09:00) Seoul"),
                new DynamicsAxTimeZone("GMTPLUS0900YAKUTSK", 82, "(GMT+09:00) Yakutsk"),
                new DynamicsAxTimeZone("GMTPLUS0930ADELAIDE", 14, "(GMT+09:30) Adelaide"),
                new DynamicsAxTimeZone("GMTPLUS0930DARWIN", 7, "(GMT+09:30) Darwin"),
                new DynamicsAxTimeZone("GMTPLUS1000BRISBANE", 26, "(GMT+10:00) Brisbane"),
                new DynamicsAxTimeZone("GMTPLUS1000CANBERRA_MELBOURNE_SYDNEY", 8,
                    "(GMT+10:00) Canberra, Melbourne, Sydney"),
                new DynamicsAxTimeZone("GMTPLUS1000GUAM_PORTMORESBY", 81, "(GMT+10:00) Guam, Port Moresby"),
                new DynamicsAxTimeZone("GMTPLUS1000HOBART", 71, "(GMT+10:00) Hobart"),
                new DynamicsAxTimeZone("GMTPLUS1000VLADIVOSTOK", 76, "(GMT+10:00) Vladivostok"),
                new DynamicsAxTimeZone("GMTPLUS0600MAGADAN", 91, "(GMT+12:00) Magadan"),
                new DynamicsAxTimeZone("GMTPLUS1100MAGADAN_SOLOMONIS", 20,
                    "(GMT+11:00) Magadan, Solomon Is., New Caledonia"),
                new DynamicsAxTimeZone("GMTPLUS1200AUCKLAND_WELLINGTON", 53, "(GMT+12:00) Auckland, Wellington"),
                new DynamicsAxTimeZone("GMTPLUS1200COORDINATEDUNIVERSALTIME", 100,
                    "(GMT+12:00) Coordinated Universal Time+12"),
                new DynamicsAxTimeZone("GMTPLUS1200FIJI_KAMCHATKA_MARSHALLIS", 32,
                    "(GMT+12:00) Fiji, Kamchatka, Marshall Is."),
                new DynamicsAxTimeZone("GMTMINUS1100MIDWAYISLAND_SAMOA", 65, "(GMT+13:00) Samoa"),
                new DynamicsAxTimeZone("GMTPLUS1300NUKU_ALOFA", 73, "(GMT+13:00) Nuku'alofa")
            };
        }

        private static string PatchDisplayName(string displayName, bool toZimeZone)
        {
            return toZimeZone ? displayName.Replace("(GMT", "(UTC") : displayName.Replace("(UTC", "(GMT");
        }
    }
}