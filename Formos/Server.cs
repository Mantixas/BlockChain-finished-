using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Formos {
    public partial class Server : Form {
      
        

            public Blockchain blockChain;
            private string Port;
            private string IP;
            private List<string> IPs;
            private List<string> Ports;
            private bool BlockChainsIsValid = true;

            //For locking threads.
            private object Locks = new object();

            public Server(string IP, string Port, List<string> IPs, List<string> Ports)
            {
                InitializeComponent();
                this.IP = IP;
                this.Port = Port;
                this.ServerPort.Text = Port;
                this.IPs = IPs;
                this.Ports = Ports;
                blockChain = new Blockchain();
                button1.PerformClick();
            }

            private void button1_Click(object sender, EventArgs e)
            {
                Thread tpcListenerThread = new Thread(new ThreadStart(tpcListener));
                tpcListenerThread.Start();
                button1.Enabled = false;
            }

            private void tpcListener()
            {
                TcpListener tcpListener = new TcpListener(IPAddress.Any, int.Parse(Port));
                tcpListener.Start();

                ServerConsole("Started");

                while (true)
                {
                    TcpClient client = tcpListener.AcceptTcpClient();

                    if (client.Connected)
                    {
                        ServerConsole("User connected");
                    }

                    Thread tcpHandlerThread = new Thread(new ParameterizedThreadStart(tcpHandler));
                    tcpHandlerThread.Start(client);
                }
            }

            private void tcpHandler(object client)
            {
                lock (Locks)
                {
                    TcpClient Client = (TcpClient)client;
                    NetworkStream stream = Client.GetStream();
                    blockChain.origin = "Server";
                    ServerConsole("Client taking main blockchain");

                    //Sending blockchain to client
                    stream.Write(ObjectToByteArray(blockChain), 0, ObjectToByteArray(blockChain).Length);

                    //Reading blockchain from client
                    var userChain = new byte[2042];
                    //Read bytes to userChain and return readed bytes number.
                    int bytes = stream.Read(userChain, 0, userChain.Length);

                    //Write to memory stream from 0 to readed number bytes.
                    var memStream = new MemoryStream();
                    memStream.Write(userChain, 0, bytes);

                    byte[] BlockChainBytes = new byte[2042];
                    //Convert to byte array.
                    BlockChainBytes = memStream.ToArray();

                    Blockchain tempBlockChain = (Blockchain)ByteArrayToObject(BlockChainBytes);

                    ServerConsole("Got blockchain");
                    ServerConsole("Checking if blockchain is valid");


                    if (CheckIfValid(tempBlockChain))
                    {
                        ServerConsole("Blockchain is valid");
                        blockChain = tempBlockChain;
                        if (tempBlockChain.origin.ToString() == "Client")
                        {
                            Servers(tempBlockChain);
                        }
                    }
                    else
                    {
                        ServerConsole("Blockchain is not valid");
                    }
                }
                if (!BlockChainsIsValid)
                {
                    ServerConsole("Not valid block chain from server synchronization cancel");
                }
                SetGrid(blockChain);
            }

            private void Servers(Blockchain ClientBlockChain)
            {
                //Serveris atsiuncia tuscia blockchain
                ServerConsole("Cheking chain blocks with other servers");
                for (int i = 0; i < Ports.Count; i++)
                {
                    string serverip = IPs[i];
                    string serverport = Ports[i];
                    if (serverip == IP && serverport == Port)
                    {
                        continue;
                    }

                    TcpClient client = new TcpClient();
                    client.Connect(IPAddress.Parse(IPs[i]), int.Parse(Ports[i]));
                    if (client.Connected)
                    {
                        NetworkStream stream = client.GetStream();

                        //Reading blockchain from server
                        var response = new byte[2042];
                        //Read bytes to userChain and return readed bytes number.
                        int bytes = stream.Read(response, 0, response.Length);

                        var memoryStream = new MemoryStream();
                        //Write to memory stream from 0 to readed number bytes.
                        memoryStream.Write(response, 0, bytes);

                        byte[] BlockChainBytes = new byte[2042];
                        //Convert to byte array.
                        BlockChainBytes = memoryStream.ToArray();

                        Blockchain OtherServerBlockChain = (Blockchain)ByteArrayToObject(BlockChainBytes);

                        if (CheckIfValid(OtherServerBlockChain))
                        {
                            //Chech blocks from other servers
                            for (int a = 0; a < OtherServerBlockChain.Chain.Count; a++)
                            {
                                if (OtherServerBlockChain.Chain[a].Hash != ClientBlockChain.Chain[a].Hash)
                                {
                                    ServerConsole("Not valid block chain from: " + serverip.ToString() + " " + serverport.ToString());
                                    BlockChainsIsValid = false;
                                }
                                if (!BlockChainsIsValid)
                                {
                                    continue;
                                }
                            }
                            try
                            {
                                // Sending blockchain to server.
                                if (BlockChainsIsValid)
                                {
                                    ServerConsole("Blockchain from server: " + serverip.ToString() + " " + serverport.ToString() + " is valid");
                                    ClientBlockChain.origin = "Server";
                                    stream.Write(ObjectToByteArray(ClientBlockChain), 0, ObjectToByteArray(ClientBlockChain).Length);
                                    ServerConsole("Blockchain exchange with server: " + serverip.ToString() + " " + serverport.ToString() + " successfull");
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                        else
                        {
                            ServerConsole("Blockchain from server: " + serverip.ToString() + " " + serverport.ToString() + " not valid");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Not connected to server. Try next time.");
                    }
                }
            }

            private void SetGrid(Blockchain blockchain)
            {
                int kandidatas1Rez = 0;
                int kandidatas2Rez = 0;
                int kandidatas3Rez = 0;

                foreach (Block blck in blockchain.Chain)
                {
                    if (blck.Data == "Ingrida Šimonytė")
                    {
                        kandidatas1Rez++;
                    }
                    else if (blck.Data == "Gitanas Nausėda")
                    {
                        kandidatas2Rez++;
                    }
                    else if (blck.Data == "Saulius Skvernelis")
                    {
                        kandidatas3Rez++;
                    }
                }

                DataTable dt = new DataTable();
                dt.Columns.Add("Kandidatai");
                dt.Columns.Add("Rezultatai");
                dt.Rows.Add("Ingrida Šimonytė", kandidatas1Rez);
                dt.Rows.Add("Gitanas Nausėda", kandidatas2Rez);
                dt.Rows.Add("Saulius Skvernelis", kandidatas3Rez);

                this.BeginInvoke((MethodInvoker)delegate {
                    dataGridView1.DataSource = dt;

                    dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Descending);
                });
            }


            public bool CheckIfValid(Blockchain blockChain)
            {
                for (int i = 1; i < blockChain.Chain.Count; i++)
                {
                    Block currentBlock = blockChain.Chain[i];
                    Block previousBlock = blockChain.Chain[i - 1];

                    if (currentBlock.Hash != currentBlock.CalculateHash())
                    {
                        return false;
                    }

                    if (currentBlock.PreviousHash != previousBlock.Hash)
                    {
                        return false;
                    }
                }
                return true;
            }
            public static byte[] ObjectToByteArray(Object obj)
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (var ms = new MemoryStream())
                {
                    bf.Serialize(ms, obj);
                    return ms.ToArray();
                }
            }
            public static Object ByteArrayToObject(byte[] arrBytes)
            {
                using (var memStream = new MemoryStream())
                {
                    var binForm = new BinaryFormatter();
                    memStream.Write(arrBytes, 0, arrBytes.Length);
                    memStream.Seek(0, SeekOrigin.Begin);
                    var obj = binForm.Deserialize(memStream);
                    return obj;
                }
            }
            private void ServerConsole(string Messages)
            {
                this.BeginInvoke((MethodInvoker)delegate {
                    Console.Text += Messages + "\r\n";
                    Console.SelectionStart = Console.Text.Length;
                    // scroll it automatically
                    Console.ScrollToCaret();
                });
            }
        

        private void Console_TextChanged(object sender, EventArgs e)
        {

        }

        private void ServerPort_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
