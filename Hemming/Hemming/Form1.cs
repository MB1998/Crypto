using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hemming
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<int> sequence = new List<int>();
            foreach (char character in textBox1.Text)
                sequence.Add((int)character - 48);
            int[] result = new int[] { };
            try
            {
                result = Hemming.Encoder(sequence.ToArray());
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Incorrect length");
            }
            string str = "";
            foreach (int digit in result)
                str += digit;
            resultTextBox.Text = str;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<int> sequence = new List<int>();
            foreach (char character in textBox1.Text)
                sequence.Add((int)character - 48);
            int[] result = new int[] { };
            try
            {
                result = Hemming.Decoder(sequence.ToArray());
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Incorrect length");
            }
            string str = "";
            foreach (int digit in result)
                str += digit;
            resultTextBox.Text = str;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 11)
            {
                encodeButton.Enabled = true;
                decodeButton.Enabled = false;
            }
            else if (textBox1.Text.Length == 15)
            {
                encodeButton.Enabled = false;
                decodeButton.Enabled = true;
            }
            else
            {
                encodeButton.Enabled = false;
                decodeButton.Enabled = false;
            }
        }
    }
}
