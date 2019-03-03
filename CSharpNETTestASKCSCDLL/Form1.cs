using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CSharpNETTestASKCSCDLL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            AskReaderLib.CSC.sCARD_SearchExtTag SearchExtender;
            int Status;
            byte[] ATR;
            ATR = new byte[200];
            int lgATR;
            lgATR = 200;
            int Com=0;
            int SearchMask;

            txtCom.Text = "";
            txtCard.Text = "";

            try
            {
                AskReaderLib.CSC.SearchCSC();
                // user can also use line below to speed up coupler connection
                //AskReaderLib.CSC.Open ("COM2");

                // Define type of card to be detected: number of occurence for each loop
                SearchExtender.CONT = 0;
                SearchExtender.ISOB = 2;
                SearchExtender.ISOA = 2;
                SearchExtender.TICK = 1;
                SearchExtender.INNO = 2;
                SearchExtender.MIFARE = 0;
                SearchExtender.MV4k = 0;
                SearchExtender.MV5k = 0;
                SearchExtender.MONO = 0;


                if (AskReaderLib.CSC.CSC_EHP_PARAMS_EXT(1, 1, 0, 0, 0, 0, 0, 0, null, 0, 0) != AskReaderLib.CSC.RCSC_Ok)
                {
                    Console.WriteLine("Impossible de définir la sélection sur 0");
                    return;
                }

                // Define type of card to be detected
                SearchMask = AskReaderLib.CSC.SEARCH_MASK_INNO | AskReaderLib.CSC.SEARCH_MASK_ISOB | AskReaderLib.CSC.SEARCH_MASK_ISOA | AskReaderLib.CSC.SEARCH_MASK_TICK;
                Status = AskReaderLib.CSC.SearchCardExt(ref SearchExtender, SearchMask, 1, 20, ref Com, ref lgATR, ATR);

                if (Status != AskReaderLib.CSC.RCSC_Ok)
                    txtCom.Text =  "Error :" + Status.ToString ("X");
                else
                    txtCom.Text = Com.ToString("X");

                // type de carte
                CardType(Com);
                // SELECT APPLICATION 
                byte[] select_app_cmd = { 0x00, 0xA4, 0x04, 0x00, 0x07, 0xD2, 0x76, 0x00, 0x00, 0x85, 0x01, 0x01, 0x00 };
                byte[] buff_response = new byte[300];
                int buff_response_length = 0;
                int res;
                if ((res = AskReaderLib.CSC.CSC_ISOCommand(select_app_cmd, select_app_cmd.Length, buff_response, ref buff_response_length)) != AskReaderLib.CSC.RCSC_Ok)// la reponse de la commande select application
                {
                    Console.WriteLine("Erreur, SELECT APPLICATION n'a pas fonctionnée : " + res);
                    return;
                }
                Console.WriteLine("SELECT APPLICATION");
                Console.WriteLine("LENGTH RESPONSE :" + buff_response_length);
                Console.WriteLine("RESPONSE : " + AskReaderLib.CSC.ToStringN(buff_response).Substring(0,buff_response_length));
                // 9000h Status bytes (SW1, SW2), command completed 
                if ((buff_response_length > 2) && (buff_response[buff_response_length - 2] == 0x90) && (buff_response[buff_response_length - 1] == 0x00))
                {
                    Console.WriteLine("SELECT APPLICATION COMMAND COMPLETED");
                }
                byte[] select_file_cmd = { 0x00, 0xA4, 0x00, 0x0C, 0x02, 0xE1, 0x03 }; // select CC (Capability Container) file 
                buff_response_length = 0;
                if ((res = AskReaderLib.CSC.CSC_ISOCommand(select_file_cmd, select_file_cmd.Length, buff_response, ref buff_response_length)) != AskReaderLib.CSC.RCSC_Ok)// la reponse de la commande select file
                {
                    Console.WriteLine("Erreur, SELECT FILE n'a pas fonctionnée : " + res);
                    return;
                }
                Console.WriteLine("SELECT FILE");
                Console.WriteLine("LENGTH RESPONSE :" + buff_response_length);
                Console.WriteLine("RESPONSE : " + AskReaderLib.CSC.ToStringN(buff_response).Substring(0, buff_response_length));
                // 9000h Status bytes (SW1, SW2), command completed 
                if ((buff_response_length > 2) && (buff_response[buff_response_length - 2] == 0x90) && (buff_response[buff_response_length - 1] == 0x00))
                {
                    Console.WriteLine("SELECT FILE COMMAND COMPLETED");
                    // Read File (READBenary Command: 00 B0 00 00 0F) 
                    // 00h Class byte (CLA) 
                    // B0h Instruction byte (INS) for ReadBinary command 
                    // 0000h Parameter byte (P1, P2), offset inside the CC file 
                    // 0Fh Le field 
                    byte[] readBinary_cmd = { 0x00, 0xB0, 0x00, 0x00, 0x0F };
                    buff_response_length = 0;
                    res = AskReaderLib.CSC.CSC_ISOCommand(readBinary_cmd, readBinary_cmd.Length, buff_response, ref buff_response_length);
                    if ((res == AskReaderLib.CSC.RCSC_Ok) && (buff_response_length > 2) && (buff_response[buff_response_length - 2] == 0x90) && (buff_response[buff_response_length - 1] == 0x00))
                    {
                        Console.WriteLine("READ BINARY (read cc file) COMMAND COMPLETED");
                        Console.WriteLine("LENGTH RESPONSE :" + buff_response_length);
                        Console.WriteLine("RESPONSE : " + AskReaderLib.CSC.ToStringN(buff_response).Substring(0, buff_response_length));
                        CCF ccfile = new CCF(buff_response);
                        Console.WriteLine("CCLEN : " + BitConverter.ToString(ccfile.cclen));
                        Console.WriteLine("Mapping Version : " + ccfile.mappingVersion.ToString("X2"));
                        Console.WriteLine("Maximum R-APDU data size : " + BitConverter.ToString(ccfile.mle)); 
                        Console.WriteLine("Maximum C-APDU data size : " + BitConverter.ToString(ccfile.mlc)); 
                        Console.WriteLine("T field of the NDEF File Control TLV : " + ccfile.tlv.t.ToString("X2")); 
                        Console.WriteLine("L field of the NDEF File Control TLV : " + ccfile.tlv.l.ToString("X2")); 
                        Console.WriteLine("V field of the NDEF File Control TLV : "); 
                        Console.WriteLine("\tFile Identifier : " + BitConverter.ToString(ccfile.tlv.fileID)); 
                        Console.WriteLine("\tMaximum NDEF size : " + BitConverter.ToString(ccfile.tlv.maxNDEFFileSize)); 
                        Console.WriteLine("\tNDEF file read access condition : " + ccfile.tlv.readAccessCond.ToString("X2")); 
                        Console.WriteLine("\tNDEF file read access condition : " + ccfile.tlv.writeAccessCond.ToString("X2"));
                        //00h Class byte (CLA) 
                        //A4h Instruction byte (INS) for Select command
                        //00h Parameter byte (P1), select by identifier
                        //0Ch Parameter byte (P2), first or only occurrence  
                        //02h Lc field 
                        //file identifier of the NDEF file retrieved from the CC file 
                        byte[] ndefSelect_cmd = { 0x00, 0xA4, 0x00, 0x0C, 0x02, ccfile.tlv.fileID[0], ccfile.tlv.fileID[1]}; //NDEF Select command
                        buff_response_length = 0;
                        res = AskReaderLib.CSC.CSC_ISOCommand(ndefSelect_cmd, ndefSelect_cmd.Length, buff_response, ref buff_response_length);
                        if ((res == AskReaderLib.CSC.RCSC_Ok) && (buff_response_length > 2) && (buff_response[buff_response_length - 2] == 0x90) && (buff_response[buff_response_length - 1] == 0x00))
                        {
                            Console.WriteLine("NDEF SELECT COMMAND COMPLETED");
                            Console.WriteLine("LENGTH RESPONSE :" + buff_response_length);
                            Console.WriteLine("RESPONSE : " + AskReaderLib.CSC.ToStringN(buff_response).Substring(0, buff_response_length));

                            byte[] ndef_file_buff = new byte[(ccfile.tlv.maxNDEFFileSize[0] << 8) | ccfile.tlv.maxNDEFFileSize[1]];
                            int offset = 0;
                            while (offset < ndef_file_buff.Length)
                            {
                                // reads the NLEN field of the NDEF file
                                byte[] readBinary_NLEN_cmd = new byte[] { 0x00, 0xB0, (byte)(offset >> 8), (byte)(offset & 0x00FF), ((ndef_file_buff.Length - offset) > (Int16)((ccfile.mle[0]<<8) | ccfile.mle[1])) ? ccfile.mle[1] : (byte)((ndef_file_buff.Length - offset) & 0x00FF) };
                                buff_response_length = 0;
                                res = AskReaderLib.CSC.CSC_ISOCommand(readBinary_NLEN_cmd, readBinary_NLEN_cmd.Length, buff_response, ref buff_response_length);
                                if ((res == AskReaderLib.CSC.RCSC_Ok) && (buff_response_length > 2) && (buff_response[buff_response_length - 2] == 0x90) && (buff_response[buff_response_length - 1] == 0x00))
                                {
                                    Console.WriteLine("ReadBinary NLEN COMMAND COMPLETED");
                                    Console.WriteLine("LENGTH RESPONSE :" + buff_response_length);
                                    Console.WriteLine("RESPONSE : " + BitConverter.ToString(buff_response).Substring(0, buff_response_length));
                                    Console.WriteLine("offset : " + offset + " buff_response_length " + buff_response_length);
                                    Array.Copy(buff_response, 1, ndef_file_buff, offset, buff_response_length - 3); // à la fin 9000 => -2 & first byte
                                    offset += buff_response_length - 3; // 9000 => -2 - firstbyte = -3
                                }
                                else
                                {
                                    Console.WriteLine("Erreur, la cmd reads the NLEN field n'a pas fonctionnée : " + res);
                                    return;
                                }


                            }
                      

                            Console.WriteLine("NDEF : " + BitConverter.ToString(ndef_file_buff));
                            offset = 0;
                            while(offset < ndef_file_buff.Length - 2)
                            {
                                byte sr = (byte)(ndef_file_buff[offset] & 0x10);
                                byte typeLen = ndef_file_buff[offset + 3];
                                byte payloadLen = ndef_file_buff[offset + 4];
                                Console.WriteLine("Type size : " + typeLen);
                                Console.WriteLine("Payload size : " + payloadLen);
                             
                                byte[] type = new byte[typeLen];
                                Array.Copy(ndef_file_buff, 5, type, 0, typeLen);
                                byte[] payload = new byte[payloadLen];
                                Array.Copy(ndef_file_buff, 5 + typeLen, payload, 0, payloadLen);
                                string re;
                                re = BitConverter.ToString(type);
                                Console.WriteLine("TYPE : " + re);
                                if(re == "55")
                                {
                                    Console.WriteLine("TYPE : 55 URI");
                                    data.Text += "URI : http://www." + System.Text.Encoding.ASCII.GetString(payload).Trim();
                                }
                                if(re == "54")
                                {
                                    Console.WriteLine("TYPE : 54 Text");
                                    data.Text += "Textrecord : " + System.Text.Encoding.ASCII.GetString(payload).Trim();
                                }
                                if (re == "00")
                                {
                                    Console.WriteLine("TYPE : 00 Binary");
                                    data.Text += "Binary : " + System.Text.Encoding.ASCII.GetString(payload).Trim();
                                }
                                re = BitConverter.ToString(payload);
                                Console.WriteLine("payload : " + System.Text.Encoding.ASCII.GetString(payload).Trim());
                                Console.WriteLine(sr);
                                if (sr == 0x10)
                                {
                                    // payload type = 0x01
                                    offset += typeLen + payloadLen + 0x05;
                                } else
                                {
                                    // payload type = 0x04
                                    offset += typeLen + payloadLen + 0x08;
                                }
                               
                            }

                        }
                        else
                        {
                            Console.WriteLine("Erreur, la cmd NDEF Select command n'a pas fonctionnée : " + res);
                            return;
                        }

                    }
                    else
                    {
                        Console.WriteLine("Erreur, la cmd ReadBinary n'a pas fonctionnée : " + res);
                        return;
                    }

                }


            }
            catch
            {
                MessageBox.Show("Error on trying do deal with reader");
            }
            AskReaderLib.CSC.Close();
        }

        public void CardType(int Com)
        {
            switch (Com)
            {
                case 2: txtCard.Text = "ISO14443A-4 no Calypso"; break;
                case 3: txtCard.Text = "INNOVATRON"; break;
                case 4: txtCard.Text = "ISOB14443B-4 Calypso"; break;
                case 5: txtCard.Text = "Mifare"; break;
                case 6: txtCard.Text = "CTS or CTM"; break;
                case 8: txtCard.Text = "ISO14443A-3 "; break;
                case 9: txtCard.Text = "ISOB14443B-4 Calypso"; break;
                case 12: txtCard.Text = "ISO14443A-4 Calypso"; break;
                case 0x6F: txtCard.Text = "Card not found"; break;
                default: txtCard.Text = ""; break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Writing !");
            code.Text += BitConverter.ToString(System.Text.Encoding.ASCII.GetBytes(URI.Text));
            code.Text += BitConverter.ToString(System.Text.Encoding.ASCII.GetBytes(binary.Text));
            code.Text += BitConverter.ToString(System.Text.Encoding.ASCII.GetBytes(text.Text));
           
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void URI_TextChanged(object sender, EventArgs e)
        {

        }
    }
}