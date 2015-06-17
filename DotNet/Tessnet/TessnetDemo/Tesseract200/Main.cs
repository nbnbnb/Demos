using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Tesseract200
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        Bitmap m_image;
        List<tessnet2.Word> m_words;

        private void Main_Load(object sender, EventArgs e)
        {
            // ָ��Ŀ¼
            // �������Ŀ¼�е� eng.unicharset �ļ�

            // ָֻ���ļ�Ŀ¼�� �� tessdataĿ¼ ������ .exe �ļ�ͬһĿ¼��
            //ocr.Init(@"tessdata", "eng", true);

            // ָ������Ŀ¼
            // ocr.Init("h:\tessdata", "eng", true);

            // ָ�����Ŀ¼
            //ocr.Init(@"..\..\..\..\data", "eng", true);

            /*  ע�⣺
             *  �����װ�� Tesseract�����ᴴ�����������е� TESSDATA_PREFIX
             *  ��ʱ������ʹ����Ժ;���·����ʾ��
             * */
            txtPath.Text = @"..\..\..\..\data\tessdata\2.0.0\";
            txtLang.Text = "eng";
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                m_image = new Bitmap(openFileDialog1.FileName);
                m_image.SetResolution(96, 96);
                lstResult.Items.Clear();
                m_words = null;
                panel2.AutoScrollMinSize = m_image.Size;
                panel2.Refresh();
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            if (m_image != null)
                e.Graphics.DrawImage(m_image, panel2.AutoScrollPosition.X, panel2.AutoScrollPosition.Y);
            if (m_words != null)
            {
                foreach (tessnet2.Word word in m_words)
                {
                    Pen pen = null;
                    if (word == lstResult.SelectedItem)
                        pen = new Pen(Color.FromArgb((int)word.Confidence, 0, 0));
                    else
                        pen = new Pen(Color.FromArgb(255, 128, (int)word.Confidence));
                    e.Graphics.DrawRectangle(pen, word.Left + panel2.AutoScrollPosition.X, word.Top + panel2.AutoScrollPosition.Y, word.Right - word.Left, word.Bottom - word.Top);
                    foreach (tessnet2.Character c in word.CharList)
                        e.Graphics.DrawRectangle(Pens.BlueViolet, c.Left + panel2.AutoScrollPosition.X, c.Top + panel2.AutoScrollPosition.Y, c.Right - c.Left, c.Bottom - c.Top);
                }
            }
        }

        private void btnDoOCR_Click(object sender, EventArgs e)
        {
            if (m_image != null && !string.IsNullOrEmpty(txtLang.Text))
            {
                progressBar1.Value = 0;
                lstResult.Items.Clear();

                tessnet2.Tesseract ocr = new tessnet2.Tesseract();
                ocr.Init(txtPath.Text, txtLang.Text, false);

                // ���ò�����������
                // ocr.SetVariable("tessedit_char_whitelist", "0123456789");

                ocr.ProgressEvent += new tessnet2.Tesseract.ProgressHandler(ocr_ProgressEvent);
                ocr.OcrDone = new tessnet2.Tesseract.OcrDoneHandler(Done);
                ocr.DoOCR(m_image, Rectangle.Empty);
            }
        }

        void Done(List<tessnet2.Word> words)
        {
            m_words = words;
            this.Invoke(new FillResult(FillResultMethod));
        }

        delegate void SetPercent(int percent);

        void ocr_ProgressEvent(int percent)
        {
            progressBar1.Invoke(new SetPercent(SetPercentMethod), new object[] { percent });
        }

        void SetPercentMethod(int percent)
        {
            progressBar1.Value = percent;
        }

        private void lstResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel2.Refresh();
        }

        delegate void FillResult();
        private void FillResultMethod()
        {
            progressBar1.Value = 0;
            lstResult.Items.AddRange(m_words.ToArray());
            panel2.Refresh();
        }

        private void btnSetPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = txtPath.Text;
            if (fbd.ShowDialog() == DialogResult.OK)
                txtPath.Text = fbd.SelectedPath;
        }
    }
}