﻿using System.Linq;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.GPU.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct PrivateFanCoolersStatusV1 : IInitializable
    {
        internal const int MaxNumberOfFanCoolerStatusEntries = 3;

        internal StructureVersion _Version;
        internal readonly uint _FanCoolersStatusCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxNumberOfFanCoolerStatusEntries)]
        internal readonly FanCoolersStatusEntry[] _FanCoolersStatusEntries;

        internal readonly uint _Unknown;

        public FanCoolersStatusEntry[] FanCoolersStatusEntries
        {
            get => _FanCoolersStatusEntries.Take((int) _FanCoolersStatusCount).ToArray();
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct FanCoolersStatusEntry
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.U4)]
            internal readonly uint[] _UnknownBinary1;

            internal readonly uint _UnknownUInt1;
            internal readonly uint _CurrentRPM;
            internal readonly uint _CurrentMinimumLevel;
            internal readonly uint _CurrentMaximumLevel;
            internal readonly uint _CurrentLevel;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = UnmanagedType.U4)]
            internal readonly uint[] _UnknownBinary2;

            public uint UnknownUInt1
            {
                get => _UnknownUInt1;
            }

            public uint CurrentRPM
            {
                get => _CurrentRPM;
            }

            public uint CurrentMinimumLevel
            {
                get => _CurrentMinimumLevel;
            }

            public uint CurrentMaximumLevel
            {
                get => _CurrentMaximumLevel;
            }

            public uint CurrentLevel
            {
                get => _CurrentLevel;
            }
        }
    }
}