using System;
using System.Drawing;
using System.Windows.Forms;
using tesseract;

namespace Tesseract301
{
    public enum TesseractEngineMode : int
    {
        /// <summary>
        /// Run Tesseract only - fastest
        /// </summary>
        TESSERACT_ONLY = 0,

        /// <summary>
        /// Run Cube only - better accuracy, but slower
        /// </summary>
        CUBE_ONLY = 1,

        /// <summary>
        /// Run both and combine results - best accuracy
        /// </summary>
        TESSERACT_CUBE_COMBINED = 2,

        /// <summary>
        /// Specify this mode when calling init_*(),
        /// to indicate that any of the above modes
        /// should be automatically inferred from the
        /// variables in the language-specific config,
        /// command-line configs, or if not specified
        /// in any of the above should be set to the
        /// default OEM_TESSERACT_ONLY.
        /// </summary>
        DEFAULT = 3
    }

    public enum TesseractPageSegMode : int
    {
        /// <summary>
        /// Fully automatic page segmentation
        /// </summary>
        PSM_AUTO = 0,

        /// <summary>
        /// Assume a single column of text of variable sizes
        /// </summary>
        PSM_SINGLE_COLUMN = 1,

        /// <summary>
        /// Assume a single uniform block of text (Default)
        /// </summary>
        PSM_SINGLE_BLOCK = 2,

        /// <summary>
        /// Treat the image as a single text line
        /// </summary>
        PSM_SINGLE_LINE = 3,

        /// <summary>
        /// Treat the image as a single word
        /// </summary>
        PSM_SINGLE_WORD = 4,

        /// <summary>
        /// Treat the image as a single character
        /// </summary>
        PSM_SINGLE_CHAR = 5
    }

    public partial class FormMain : Form
    {
        private TesseractProcessor m_tesseract = null;

        // 此处制定目录的时候，一定要添加结尾的 \
        // FK
        private const string m_path = @"..\..\..\..\data\tessdata\3.0.1\";
        //private const string m_lang = "test";  // 对应的训练文件为 test.traineddata
        private const string m_lang = "eng";  // 使用  eng.traineddata文件

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            m_tesseract = new TesseractProcessor();
            labelStatus.Text = "Initializing tesseract...";
            bool succeed = m_tesseract.Init(m_path, m_lang, (int)TesseractEngineMode.DEFAULT);

            if (!succeed)
            {
                MessageBox.Show("Tesseract initialization failed. The application will exit.");
                Application.Exit();
            }

            labelStatus.Text = m_path + m_lang + ".traineddata loaded";

            // 此处应该传递的是枚举的字符串值 还是 枚举的整型字符串表示  ???
            //m_tesseract.SetVariable("tessedit_pageseg_mode", ((int)TesseractPageSegMode.PSM_SINGLE_LINE).ToString());
      
            m_tesseract.SetVariable("tessedit_pageseg_mode", TesseractPageSegMode.PSM_SINGLE_LINE.ToString());
            m_tesseract.SetVariable("tessedit_char_whitelist", "0123456789"); // 设置字符白名单

            System.Environment.CurrentDirectory = System.IO.Path.GetFullPath(m_path);
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = System.Environment.CurrentDirectory;
            dlg.RestoreDirectory = false;
            dlg.Filter = "All Files|*.*|Common Images|*.tif;*.tiff;*.bmp;*.jpg;*.jpeg;*.png;*.gif";
            dlg.FilterIndex = 2;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Image image = Image.FromFile(dlg.FileName);
                this.pictureBox1.Image = image;
                string bt = this.Ocr(image);

                textBoxResult.Text = bt.Replace("\n", "\r\n");//不做此转换则无换行效果  

            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private string Ocr(Image image)
        {
            m_tesseract.Clear();
            m_tesseract.ClearAdaptiveClassifier();
            return m_tesseract.Apply(image);
        }
    }
}
