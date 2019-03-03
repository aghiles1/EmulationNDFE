using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpNETTestASKCSCDLL
{
    public class CCF
    {
        // The meanings of the bytes are:
        public byte[] cclen; // 00 0Fh CCLEN length of the CC file 
        public byte mappingVersion;// 20h  Mapping Version 2.0 
        public byte[] mle; // 003Bh  MLe maximum 59 bytes R-APDU data size 
        public byte[] mlc; // 0034h  MLc maximum 52 bytes C-APDU data size 
        public TLVBlock tlv; // NDEF File Control TLV 

        public CCF(byte[] ccFileBuffer)
        {
            this.cclen = new byte[]{ccFileBuffer[1], ccFileBuffer[2]};
            this.mappingVersion = (byte)ccFileBuffer[3];
            this.mle = new byte[] { ccFileBuffer[4], ccFileBuffer[5] };
            this.mlc = new byte[] { ccFileBuffer[6], ccFileBuffer[7] };
            byte[] tmp= new byte[ccFileBuffer.Length - 8];
            Array.Copy(ccFileBuffer, 8, tmp, 0, ccFileBuffer.Length - 8); // copy ccfilebuffer into tmp from index 7 to the end to get the TLV buffer
            this.tlv = new TLVBlock(tmp); // parse the TLV buffer
        }
    }
}
