using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tessnet2;

namespace Tesseract200Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // 此版本只能用 2.0 的训练文件

            Bitmap image = new Bitmap(@"..\..\..\..\data\images\code.png");  // 一个简单的验证码
            Tesseract ocr = new Tesseract();
            // 此处设置识别字符的白名单
            ocr.SetVariable("tessedit_char_whitelist", "0123456789");

            // 指定目录
            // 将会加载目录中的 eng.unicharset 文件

            // 只指定文件目录名 将 tessdata目录 放在与 .exe 文件同一目录下
            //ocr.Init(@"tessdata", "eng", true);

            // 指定绝对目录
            // ocr.Init("h:\tessdata", "eng", true);

            // 指定相对目录
            //ocr.Init(@"..\..\..\..\data", "eng", true);

            /*  注意：
             *  如果安装了 Tesseract，将会创建环境变量中的 TESSDATA_PREFIX
             *  此时将不能使用相对和绝对路径表示法
             * */
            ocr.Init(@"..\..\..\..\data\tessdata\2.0.0\", "eng", true);

            // 生成黑白阈值的图片并保存
            // string fileName = "G:\\" + Guid.NewGuid().ToString() + ".png";
            // ocr.GetThresholdedImage(image, Rectangle.Empty).Save(fileName);

            List<Word> result = ocr.DoOCR(image, Rectangle.Empty);
            foreach (Word word in result)
            {
                // Confidence：可信度
                // 小于 160 的 Confidence 表示还可以接受
                Console.WriteLine("{0} : {1}", word.Confidence, word.Text);
            }
        }
    }
}
