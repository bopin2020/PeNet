﻿using System;

namespace PeNet.Structures
{
    /// <summary>
    /// COM+ 2.0 (CLI) Header
    /// https://www.codeproject.com/Articles/12585/The-NET-File-Format
    /// </summary>
    public class IMAGE_COR20_HEADER : AbstractStructure
    {
        private IMAGE_DATA_DIRECTORY? _metaData;
        private IMAGE_DATA_DIRECTORY? _resources;
        private IMAGE_DATA_DIRECTORY? _strongSignatureNames;
        private IMAGE_DATA_DIRECTORY? _codeManagerTable;
        private IMAGE_DATA_DIRECTORY? _vTableFixups;
        private IMAGE_DATA_DIRECTORY? _exportAddressTableJumps;
        private IMAGE_DATA_DIRECTORY? _managedNativeHeader;

        /// <summary>
        /// Create a new instance of an COM+ 2 (CLI) header.
        /// </summary>
        /// <param name="peFile">A PE file.</param>
        /// <param name="offset">Offset to the COM+ 2 (CLI) header in the byte array.</param>
        public IMAGE_COR20_HEADER(IRawFile peFile, long offset) 
            : base(peFile, offset)
        {
        }

        /// <summary>
        /// Size of the structure.
        /// </summary>
        public uint cb
        {
            get => PeFile.ReadUInt(Offset);
            set => PeFile.WriteUInt(Offset, value);
        }

        /// <summary>
        /// Major runtime version of the CRL.
        /// </summary>
        public ushort MajorRuntimeVersion
        {
            get => PeFile.ReadUShort(Offset + 0x4);
            set => PeFile.WriteUShort(Offset + 0x4, value);
        }

        /// <summary>
        /// Minor runtime version of the CRL.
        /// </summary>
        public ushort MinorRuntimeVersion
        {
            get => PeFile.ReadUShort(Offset + 0x6);
            set => PeFile.WriteUShort(Offset + 0x6, value);
        }

        /// <summary>
        /// Meta data directory.
        /// </summary>
        public IMAGE_DATA_DIRECTORY? MetaData
        {
            get
            {
                if (_metaData != null)
                    return _metaData;

                _metaData = SetImageDataDirectory(PeFile, Offset + 0x8);
                return _metaData;
            }
        }
        
        /// <summary>
        /// COM image flags.
        /// </summary>
        public uint Flags
        {
            get => PeFile.ReadUInt(Offset + 0x10);
            set => PeFile.WriteUInt(Offset + 0x10, value);
        }

        /// <summary>
        /// Represents the managed entry point if COMIMAGE_FLAGS_NATIVE_ENTRYPOINT is not set.
        /// Union with EntryPointRVA.
        /// </summary>
        public uint EntryPointToken
        {
            get => PeFile.ReadUInt(Offset + 0x14);
            set => PeFile.WriteUInt(Offset + 0x14, value);
        }

        /// <summary>
        /// Represents an RVA to an native entry point if the COMIMAGE_FLAGS_NATIVE_ENTRYPOINT is set.
        /// Union with EntryPointToken.
        /// </summary>
        public uint EntryPointRVA
        {
            get => EntryPointToken;
            set => EntryPointToken = value;
        }

        /// <summary>
        /// Resource data directory.
        /// </summary>
        public IMAGE_DATA_DIRECTORY? Resources
        {
            get
            {
                _resources ??= SetImageDataDirectory(PeFile, Offset + 0x18);
                return _resources;
            }
        }

        /// <summary>
        /// Strong names signature directory.
        /// </summary>
        public IMAGE_DATA_DIRECTORY? StrongNameSignature
        {
            get
            {
                _strongSignatureNames ??= SetImageDataDirectory(PeFile, Offset + 0x20);
                return _strongSignatureNames;
            }
        }

        /// <summary>
        /// Code manager table directory.
        /// </summary>
        public IMAGE_DATA_DIRECTORY? CodeManagerTable
        {
            get
            {
                _codeManagerTable ??= SetImageDataDirectory(PeFile, Offset + 0x28);
                return _codeManagerTable;
            }
        }

        /// <summary>
        /// Virtual table fix up directory.
        /// </summary>
        public IMAGE_DATA_DIRECTORY? VTableFixups
        {
            get
            {
                _vTableFixups ??= SetImageDataDirectory(PeFile, Offset + 0x30);
                return _vTableFixups;
            }
        }

        /// <summary>
        /// Export address table jump directory.
        /// </summary>
        public IMAGE_DATA_DIRECTORY? ExportAddressTableJumps
        {
            get
            {
                _exportAddressTableJumps ??= SetImageDataDirectory(PeFile, Offset + 0x38);
                return _exportAddressTableJumps;
            }
        }

        /// <summary>
        /// Managed native header directory.
        /// </summary>
        public IMAGE_DATA_DIRECTORY? ManagedNativeHeader
        {
            get
            {
                _managedNativeHeader ??= SetImageDataDirectory(PeFile, Offset + 0x40);
                return _managedNativeHeader;
            }
        }

        private IMAGE_DATA_DIRECTORY? SetImageDataDirectory(IRawFile peFile, long offset)
        {
            try
            {
                return new IMAGE_DATA_DIRECTORY(peFile, offset);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}