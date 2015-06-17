using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace Tesseract302
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://github.com/charlesw/tesseract
            // A .NET wrapper for tesseract-ocr
            // 使用 Install-Package Tesseract 进行安装

            // 3.0.2 版本的训练文件必须放在 tessdata 目录中
            // 例如此处的 完整物理路径为 E:\Sample\Tessnet\TessnetDemo\data\tessdata\3.0.2\tessdata
            // 训练文件放在 3.0.2 的 tessdata 目录中
            using (var engine = new TesseractEngine(@"..\..\..\..\data\tessdata\3.0.2\", "zhang", EngineMode.Default))
            {
                using (var img = Pix.LoadFromFile(@"..\..\..\..\data\images\A-03.png"))
                {
                    using (var page = engine.Process(img))
                    {
                        var text = page.GetText();
                        Console.WriteLine(text);
                    }
                }
            }
        }
    }
}