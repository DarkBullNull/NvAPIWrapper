﻿using System;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Helpers;
using NvAPIWrapper.Native.Interfaces;

namespace NvAPIWrapper.Native.Display.Structures
{
    /// <summary>
    ///     Contains information regarding the scan-out warping data
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct ScanOutWarpingV1 : IDisposable, IInitializable
    {
        internal StructureVersion _Version;
        internal IntPtr _Vertices;
        internal WarpingVerticeFormat _VertexFormat;
        internal uint _NumberOfVertices;
        [MarshalAs(UnmanagedType.LPStruct)] internal Rectangle _TextureRectangle;

        /// <summary>
        ///     Creates a new instance of <see cref="ScanOutWarpingV1" />.
        /// </summary>
        /// <param name="vertexFormat">The format of the input vertices.</param>
        /// <param name="vertices">The array of floating values containing the warping vertices.</param>
        /// <param name="textureRectangle">The rectangle in desktop coordinates describing the source area for the warping.</param>
        public ScanOutWarpingV1(WarpingVerticeFormat vertexFormat, float[] vertices, Rectangle textureRectangle)
        {
            if (vertices.Length % 6 != 0)
            {
                throw new ArgumentOutOfRangeException(nameof(vertices));
            }

            this = typeof(ScanOutWarpingV1).Instantiate<ScanOutWarpingV1>();
            _TextureRectangle = textureRectangle;
            _VertexFormat = vertexFormat;
            _NumberOfVertices = (uint) (vertices.Length / 6);
            _Vertices = Marshal.AllocHGlobal(vertices.Length * sizeof(float));
            Marshal.Copy(vertices, 0, _Vertices, vertices.Length);
        }

        /// <summary>
        ///     Gets the format of the input vertices
        /// </summary>
        public WarpingVerticeFormat VertexFormat
        {
            get => _VertexFormat;
        }

        /// <summary>
        ///     Gets the rectangle in desktop coordinates describing the source area for the warping
        /// </summary>
        public Rectangle TextureRectangle
        {
            get => _TextureRectangle;
        }

        /// <summary>
        ///     Gets the array of floating values containing the warping vertices
        /// </summary>
        public float[] Vertices
        {
            get
            {
                var floats = new float[_NumberOfVertices * 6];
                Marshal.Copy(_Vertices, floats, 0, floats.Length);

                return floats;
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Marshal.FreeHGlobal(_Vertices);
        }
    }
}