﻿using System;
using System.Diagnostics;
using LibAtem.Common;
using LibAtem.Util;

namespace LibAtem.DeviceProfile
{
    public static class AvailabilityChecker
    {
        public static object GetMaxForProperty(DeviceProfile profile, string propName)
        {
            switch (propName)
            {
                case "MediaPlayerSourceClipIndex.Index":
                    return profile.MediaPoolClips - 1;
                case "MediaPlayerSourceStillIndex.Index":
                    return profile.MediaPoolStills - 1;
                case "MacroSleep.Frames":
                    return (uint) 500; // TODO
                default:
                    return null;
            }
        }

        public static bool IsAvailable(DeviceProfile profile, object val)
        {
            if (val is VideoSource)
                return IsAvailable((VideoSource)val, profile);
            if (val is AudioSource)
                return IsAvailable((AudioSource)val, profile);
            if (val is MediaPlayerId)
                return IsAvailable((MediaPlayerId)val, profile);
            if (val is MixEffectBlockId)
                return IsAvailable((MixEffectBlockId)val, profile);
            if (val is UpstreamKeyId)
                return IsAvailable((UpstreamKeyId)val, profile);
            if (val is DownstreamKeyId)
                return IsAvailable((DownstreamKeyId)val, profile);

            // Assume it is available as many types do not need implementing
            return true;
        }

        public static bool IsAvailable(this VideoSource src, DeviceProfile profile)
        {
            if (!src.IsValid())
                return false;

            VideoSourceTypeAttribute props = src.GetAttribute<VideoSource, VideoSourceTypeAttribute>();
            switch (props.PortType)
            {
                case InternalPortType.Auxilary:
                    return props.Index <= profile.Auxiliaries;
                case InternalPortType.Black:
                case InternalPortType.ColorBars:
                    return true;
                case InternalPortType.ColorGenerator:
                    return props.Index <= profile.ColorGenerators;
                case InternalPortType.External:
                    return props.Index <= profile.Sources.Count;
                case InternalPortType.MEOutput:
                    return props.Index <= profile.MixEffectBlocks;
                case InternalPortType.Mask:
                    return props.Index <= profile.UpstreamKeys;
                case InternalPortType.MediaPlayerFill:
                case InternalPortType.MediaPlayerKey:
                    return props.Index <= profile.MediaPlayers;
                case InternalPortType.SuperSource:
                    return props.Index <= profile.SuperSource;
                default:
                    Debug.Fail(String.Format("Invalid source type:{0}", props.PortType));
                    return false;
            }
        }

        public static bool IsAvailable(this AudioSource src, DeviceProfile profile)
        {
            if (!src.IsValid())
                return false;

            VideoSource? videoSrc = src.GetVideoSource();
            if (videoSrc == null)
                return false;

            return videoSrc.Value.IsAvailable(profile);
        }

        public static bool IsAvailable(this MediaPlayerId id, DeviceProfile profile)
        {
            return id.IsValid() && (int) id < profile.MediaPlayers;
        }

        public static bool IsAvailable(this MixEffectBlockId id, DeviceProfile profile)
        {
            return id.IsValid() && (int)id < profile.MixEffectBlocks;
        }

        public static bool IsAvailable(this UpstreamKeyId id, DeviceProfile profile)
        {
            return id.IsValid() && (int)id < profile.UpstreamKeys;
        }

        public static bool IsAvailable(this DownstreamKeyId id, DeviceProfile profile)
        {
            return id.IsValid() && (int)id < profile.DownstreamKeys;
        }
    }
}