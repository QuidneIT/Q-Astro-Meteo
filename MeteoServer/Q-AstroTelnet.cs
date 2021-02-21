// minimalistic telnet implementation
// conceived by Tom Janssens on 2007/06/06  for codeproject
//
// http://www.corebvba.be

using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace ASCOM.QAstroMeteo
{
    enum Verbs {
        WILL = 251,
        WONT = 252,
        DO = 253,
        DONT = 254,
        IAC = 255
    }

    enum Options
    {
        SGA = 3
    }

    class Telnet
    {
        private TcpClient tcpSocket;
        private int TimeOutMs = 100;
        private bool m_Connected = false;

        public string HostName = "";
        public int PortNumber = 0;

        public Telnet()
        {
        }

        public bool Connected
        {
            get { return CheckConnected(); }
            set 
            {
                tcpSocket = new TcpClient(HostName, PortNumber);
                CheckConnected(); 
            }
        }
            
        private bool CheckConnected()
        {
            try
            {
                string sreturn = "";
                int noDataCount = 0;
                m_Connected = false;

                while ((noDataCount<3) && (sreturn.Length == 0))
                {
                    WriteLine("i#");
                    Thread.Sleep(1000);
                    sreturn = Read();
                    noDataCount++;
                }

                if (sreturn.Contains("Q-Astro Meteo"))
                    m_Connected = true;

                return m_Connected;
            }
            catch (Exception e)
            {
                return m_Connected;
            }
        }

        public void WriteLine(string cmd)
        {
            Write(cmd + "\n");
        }

        public void Write(string cmd)
        {
            if (!tcpSocket.Connected)
            {
                m_Connected = false;
                return;
            }

            byte[] buf = System.Text.ASCIIEncoding.ASCII.GetBytes(cmd.Replace("\0xFF", "\0xFF\0xFF"));
            tcpSocket.GetStream().Write(buf, 0, buf.Length);

        }

        public string Read()
        {

            if (!tcpSocket.Connected)
            {
                m_Connected = false;
                return String.Empty;
            }

            StringBuilder sb = new StringBuilder();
            do
            {
                ParseTelnet(sb);
                Thread.Sleep(TimeOutMs);
            }
            while (tcpSocket.Available > 0);

            return sb.ToString();

        }

        void ParseTelnet(StringBuilder sb)
        {
            while (tcpSocket.Available > 0)
            {
                int input = tcpSocket.GetStream().ReadByte();
                switch (input)
                {
                    case -1 :
                        break;
                    case (int)Verbs.IAC:
                        // interpret as command
                        int inputverb = tcpSocket.GetStream().ReadByte();
                        if (inputverb == -1) break;
                        switch (inputverb)
                        {
                            case (int)Verbs.IAC: 
                                //literal IAC = 255 escaped, so append char 255 to string
                                sb.Append(inputverb);
                                break;
                            case (int)Verbs.DO: 
                            case (int)Verbs.DONT:
                            case (int)Verbs.WILL:
                            case (int)Verbs.WONT:
                                // reply to all commands with "WONT", unless it is SGA (suppres go ahead)
                                int inputoption = tcpSocket.GetStream().ReadByte();
                                if (inputoption == -1) break;
                                tcpSocket.GetStream().WriteByte((byte)Verbs.IAC);
                                if (inputoption == (int)Options.SGA )
                                    tcpSocket.GetStream().WriteByte(inputverb == (int)Verbs.DO ? (byte)Verbs.WILL:(byte)Verbs.DO); 
                                else
                                    tcpSocket.GetStream().WriteByte(inputverb == (int)Verbs.DO ? (byte)Verbs.WONT : (byte)Verbs.DONT); 
                                tcpSocket.GetStream().WriteByte((byte)inputoption);
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        sb.Append( (char)input );
                        break;
                }
            }
        }
    }
}
