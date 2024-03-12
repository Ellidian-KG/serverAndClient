using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _123123Client
{
    public partial class Form1 : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        public Form1()
        {
            InitializeComponent();

            client = new TcpClient("192.168.0.105", 12345);
            stream = client.GetStream();
        }

        private void button2_Click(object sender, EventArgs e)
        {
         
         if (!string.IsNullOrEmpty(textBox1.Text))
            {
                string message = $"{textBox3.Text}: {textBox1.Text}\n";
                SendMessage(message);
                textBox1.Clear(); 
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.Text = textBox3.Text.Trim();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox3.Text))
            {
                string userNameMessage = textBox3.Text; 
                SendMessage(userNameMessage);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void SendMessage(string message)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                await stream.WriteAsync(data, 0, data.Length);

                string formattedMessage = $"{message}\n";

                textBox2.AppendText(formattedMessage);
                textBox2.AppendText(Environment.NewLine);
                textBox2.ScrollToCaret();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending message: " + ex.Message);
            }
        }


    
    }
}
