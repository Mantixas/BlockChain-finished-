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

namespace Formos {
    public partial class Client : Form
    {

        public Client()
        {
            InitializeComponent();
            IP.Text = "127.0.0.1";
            List<string> IPs = new List<string>();
            List<string> Ports = new List<string>();
            IPs.Add("127.0.0.1");
            IPs.Add("127.0.0.1");
            Ports.Add("7777");
            Ports.Add("7778");
            for (int i = 0; i < Ports.Count; i++)
            {
                string port = Ports[i].ToString();
                string ip = IPs[i].ToString();
                new Thread(() => new Server(ip, port, IPs, Ports).ShowDialog()).Start();
            }
        }

        private void btnVote_Click(object sender, EventArgs e)
        {
            Vote form = new Vote(IP.Text, Port.Text);
            this.Hide();
            form.ShowDialog();
            this.Show();
        }

        private void Port_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void IP_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
