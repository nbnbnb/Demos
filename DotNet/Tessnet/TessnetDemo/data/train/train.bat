rem ����ͨ�� jTessBoxEditor �ϲ��ļ��󱣴�Ϊ zhang.btking.exp0.tif
rem �������� box �ļ�
rem tesseract.exe zhang.btking.exp0.tif zhang.btking.exp0 batch.nochop makebox 
rem ͨ�� jTessBoxEditor У�� TIFF �ļ��󱣴漴��
rem ִ�и�������ǰ��Ҫ��Ŀ¼�´��� font_properties�ļ�
rem �������� box��У���󱣴�
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