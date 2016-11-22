﻿/***********************************************************************
Copyright 2016 Stefan Hausotte

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

*************************************************************************/

using System.Text;

namespace PeNet.Structures
{
    /// <summary>
    /// The Meta Data Tables Header contains information about all present
    /// data tables in the .Net assembly.
    /// </summary>
    public class METADATATABLESHDR : AbstractStructure
    {
        /// <summary>
        /// Create a new Meta Data Tables Header instance from a byte array.
        /// </summary>
        /// <param name="buff">Buffer which contains a METADATATABLESHDR structure.</param>
        /// <param name="offset">Offset in the buffer, where the header starts.</param>
        public METADATATABLESHDR(byte[] buff, uint offset) 
            : base(buff, offset)
        {
        }

        /// <summary>
        /// Reserved1, always 0.
        /// </summary>
        public uint Reserved1
        {
            get { return Buff.BytesToUInt32(Offset); }
            set { Buff.SetUInt32(Offset, value); }
        }

        /// <summary>
        /// Major Version.
        /// </summary>
        public byte MajorVersion
        {
            get { return Buff[Offset + 0x4]; }
            set { Buff[Offset + 0x4] = value; }
        }

        /// <summary>
        /// Minor Version.
        /// </summary>
        public byte MinorVersion
        {
            get { return Buff[Offset + 0x5]; }
            set { Buff[Offset + 0x5] = value; }
        }

        /// <summary>
        /// The size the indexes into the streams have. 
        /// Bit 0 (0x01) set: Indexes into #String are 4 bytes wide.
        /// Bit 1 (0x02) set: Indexes into #GUID heap are 4 bytes wide.
        /// Bit 2 (0x04) set: Indexes into #Blob heap are 4 bytes wide.
        /// If bit not set: indexes into heap is 2 bytes wide.
        /// </summary>
        public byte HeapOffsetSizes
        {
            get { return Buff[Offset + 0x6]; }
            set { Buff[Offset + 0x6] = value; }
        }

        /// <summary>
        /// Reserved2, always 1.
        /// </summary>
        public byte Reserved2
        {
            get { return Buff[Offset + 0x7]; }
            set { Buff[Offset + 0x7] = value; }
        }

        /// <summary>
        /// Bit mask which shows, which tables are present in the .Net assembly. 
        /// Maximal 64 tables can be present, but most tables are not defined such that
        /// the high bits of the mask are always 0.
        /// </summary>
        public ulong MaskValid
        {
            get { return Buff.BytesToUInt64(Offset + 0x8); }
            set { Buff.SetUInt64(Offset + 0x8, value); }
        }

        /// <summary>
        /// Bit mask which shows, which tables are sorted. 
        /// </summary>
        public ulong MaskSorted
        {
            get { return Buff.BytesToUInt64(Offset + 0x10); }
            set { Buff.SetUInt64(Offset + 0x10, value); }
        }

        /// <summary>
        ///     Create a string representation of the objects
        ///     properties.
        /// </summary>
        /// <returns>The METADATATABLESHDR properties as a string.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder("METADATATABLESHDR\n");
            sb.Append(Utility.PropertiesToString(this, "{0,-10}:\t{1,10:X}\n"));

            return sb.ToString();
        }
    }
}