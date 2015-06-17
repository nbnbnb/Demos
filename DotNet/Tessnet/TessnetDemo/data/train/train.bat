rem 首先通过 jTessBoxEditor 合并文件后保存为 zhang.btking.exp0.tif
rem 首先生成 box 文件
rem tesseract.exe zhang.btking.exp0.tif zhang.btking.exp0 batch.nochop makebox 
rem 通过 jTessBoxEditor 校正 TIFF 文件后保存即可
rem 执行该批处理前先要在目录下创建 font_properties文件
rem 首先生成 box，校正后保存
rem tesseract [lang].[fontname].exp[num].tif [lang].[fontname].exp[num] batch.nochop makebox  
echo Run Tesseract for Training..
tesseract.exe zhang.btking.exp0.tif zhang.btking.exp0 nobatch box.train

echo Compute the Character Set..
unicharset_extractor.exe zhang.btking.exp0.box
mftraining -F font_properties -U unicharset -O zhang.unicharset zhang.btking.exp0.tr

echo Clustering..
cntraining.exe zhang.btking.exp0.tr

echo Rename Files..
rename normproto zhang.normproto
rename inttemp zhang.inttemp
rename pffmtable zhang.pffmtable
rename shapetable zhang.shapetable 

echo Create Tessdata..
combine_tessdata.exe zhang.