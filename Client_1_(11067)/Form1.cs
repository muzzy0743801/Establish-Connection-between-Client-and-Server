using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client_1__11067_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Form2 obj = new Form2();
            obj.Show();
        }
        TcpClient client = new TcpClient();


        public void Readmessage()
        {
            while (true)
            {
                NetworkStream stream = client.GetStream();
                StreamReader sdr = new StreamReader(stream);
                string msg = sdr.ReadLine();

                textBox2.AppendText(Environment.NewLine);
                textBox2.AppendText("Server: " + msg);

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            NetworkStream stream = client.GetStream();
            StreamWriter sdr = new StreamWriter(stream);
            sdr.WriteLine(textBox3.Text);
            sdr.Flush();
            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText("C1: " + textBox3.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            IPEndPoint point = new IPEndPoint(IPAddress.Loopback, 8004);
            client = new TcpClient(point);
            client.Connect(IPAddress.Loopback, 8001);

            Thread t = new Thread(Readmessage);
            t.Start();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
