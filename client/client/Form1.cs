using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Windows.Forms;

namespace client
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
        byte[] b = new byte[1024];
        TcpClient cl = new TcpClient();

        private void button1_Click(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            cl.Connect(IPAddress.Loopback, 11000);
            NetworkStream ns = cl.GetStream();
            StreamWriter sw = new StreamWriter(ns);
            sw.WriteLine("@name@" + textBox1.Text);
            sw.Flush();
            ns.BeginRead(b, 0, b.Length, ReadMsg, ns);

        }
        private void ReadMsg(IAsyncResult ar)
        {
            NetworkStream ns = (NetworkStream)ar.AsyncState;
            int count = ns.EndRead(ar);
            textBox2.Text += ASCIIEncoding.ASCII.GetString(b, 0, count);
            ns.BeginRead(b, 0, b.Length, ReadMsg, ns);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NetworkStream ns = cl.GetStream();
            StreamWriter sw = new StreamWriter(ns);
            sw.WriteLine(textBox1.Text + "Says: " + textBox3.Text);
            sw.Flush();
        }
    }
}
