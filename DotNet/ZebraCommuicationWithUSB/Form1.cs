using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using WindowApp.Zebra.PrintHelper;

namespace WindowApp.Zebra
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button_Load_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = File.ReadAllText(openFileDialog1.FileName);
            }
        }

        private void button_Print_Click(object sender, EventArgs e)
        {
            string printName = "";
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printName = printDialog1.PrinterSettings.PrinterName;
            }

            if (!string.IsNullOrEmpty(printName))
            {
                DrvPrinterHelper.SendStringToPrinter(printName, "Hello World!");
            }
        }
    }
}
