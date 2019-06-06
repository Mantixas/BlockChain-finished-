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
    public partial class Vote : Form {
        public Blockchain blockChain = new Blockchain();
        private string IP;
        private string Port;

        public Vote(string IP, string Port) {
            this.IP = IP;
            this.Port = Port;
            InitializeComponent();
        }

        private void Kandidatas1RadioButton_CheckedChanged(object sender, EventArgs e) { }
        private void Kandidatas2RadioButton_CheckedChanged(object sender, EventArgs e) { }

        private void StartButton_Click(object sender, EventArgs e) {
            Thread ClientThread = new Thread(new ThreadStart(Client));
            ClientThread.Start();
            //StartButton.Enabled = false;
        }

        private void Client() {
            TcpClient client = new TcpClient();
            client.Connect(IPAddress.Parse(IP), int.Parse(Port));
            if (client.Connected) {
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

                blockChain = (Blockchain)ByteArrayToObject(BlockChainBytes);
                blockChain.origin = "Client";

                //Write data to client network stream
                try {
                    if (kanditatas1.Checked) {
                        blockChain.AddBlock(new Block(null, kanditatas1.Text));
                    }
                    if (kanditatas2.Checked) {
                        blockChain.AddBlock(new Block(null, kanditatas2.Text));
                    }
                    if (kanditatas3.Checked) {
                        blockChain.AddBlock(new Block(null, kanditatas3.Text));
                    }
                    // Sending blockchain to server.
                    blockChain.origin = "Client";
                    stream.Write(ObjectToByteArray(blockChain), 0, ObjectToByteArray(blockChain).Length);

                    //MessageBox.Show("Sekmingai balsuota");

                    this.BeginInvoke((MethodInvoker)delegate {
                        this.Close();
                    });
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            } else {
                MessageBox.Show("Not connected to server. Try next time.");
            }
        }



        public static Object ByteArrayToObject(byte[] arrBytes) {
            using (var memStream = new MemoryStream()) {
                var binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = binForm.Deserialize(memStream);
                return obj;
            }
        }
        public static byte[] ObjectToByteArray(Object obj) {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream()) {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
