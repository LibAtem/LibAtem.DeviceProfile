﻿using System.Collections.Generic;
using System.Xml.Serialization;
using LibAtem.Common;

namespace LibAtem.DeviceProfile
{
    [XmlRoot("DeviceProfile", IsNullable = false)]
    public class DeviceProfile
    {
        public string Product { get; set; }

        public List<MixEffect> MixEffectBlocks { get; set; }

        public List<DevicePort> Sources { get; set; }

        public uint ColorGenerators { get; set; }

        public uint Auxiliaries { get; set; }

        public uint DownstreamKeys { get; set; }

        public uint UpstreamKeys { get; set; }

        public bool RoutableKeyMasks { get; set; }

        public uint HyperDecks { get; set; }

        public uint Stingers { get; set; }

        public MultiView MultiView { get; set; }

        public uint DVE { get; set; }

        public uint SuperSource { get; set; }

        public uint MediaPlayers { get; set; }

        public uint MediaPoolClips { get; set; }

        public uint MediaPoolStills { get; set; }

        public uint MacroCount { get; set; }

        public uint SerialPort { get; set; }

        public bool AudioMonitor { get; set; }

        public VideoModeStandard DownConvertFrom { get; set; }
    }

    public class MixEffect
    {
        public MixEffect()
        {
        }

        public MixEffect(int i)
        {
            i++;

            ProgramLong = "Program " + i;
            ProgramShort = "Pgm" + i;
            PreviewLong = "Preview " + i;
            PreviewShort = "Pvw" + i;
        }

        [XmlAttribute("programLong")]
        public string ProgramLong { get; set; }
        [XmlAttribute("programShort")]
        public string ProgramShort { get; set; }

        [XmlAttribute("previewLong")]
        public string PreviewLong { get; set; }
        [XmlAttribute("previewShort")]
        public string PreviewShort { get; set; }
    }

    public class DevicePort
    {
        [XmlAttribute("id")]
        public VideoSource Id { get; set; }

        [XmlElement("Port")]
        public List<ExternalPortType> Port { get; set; }
    }

    public class MultiView
    {
        public uint Count { get; set; }

        public bool VuMeters { get; set; }

        public bool CanSwapPreviewProgram { get; set; }
    }
}