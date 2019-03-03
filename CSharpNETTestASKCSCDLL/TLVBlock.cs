using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpNETTestASKCSCDLL
{
    public class TLVBlock
    {
        public byte t; // 04h
        public byte l; // 06h
        /*V is composed of 6 bytes */
        public byte[] fileID; //  Indicates a valid NDEF file. The valid ranges are 0001h to E101h, E104h to 3EFFh, 3F01h to 3FFEh and 4000h to FFFEh
        public byte[] maxNDEFFileSize; // The valid range is 0005h to FFFEh
        public byte readAccessCond; // 00h indicates read access granted without any security 
        public byte writeAccessCond; // 00h indicates read access granted without any security. FFh indicates no write access granted at all (read-only)

        public TLVBlock(byte[] tlvBlockBuff)
        {
            // 8 bytes TLV block
            this.t = tlvBlockBuff[0];
            this.l = tlvBlockBuff[1];
            this.fileID = new byte[] { tlvBlockBuff[2], tlvBlockBuff[3] };
            this.maxNDEFFileSize = new byte[]{tlvBlockBuff[4], tlvBlockBuff[5]};
            this.readAccessCond = tlvBlockBuff[6];
            this.writeAccessCond = tlvBlockBuff[7];
        }
    }
}
