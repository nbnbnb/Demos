using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fiddler;
using System.IO;
using System.Windows.Forms;

namespace CVSTranscoder
{

    [ProfferFormat("TAB-Separated Values", "Session List in Tab-Delimited Format")]
    [ProfferFormat("Comma-Separated Values", "Session List in Comma-Delimited Format; import into Excel or other Tools")]
    public class CVSTranscoderExporter : ISessionExporter
    {
        public void Dispose()
        {

        }

        public bool ExportSessions(string sExportFormat, Session[] oSessions,
            Dictionary<string, object> dictOptions,
            EventHandler<ProgressCallbackEventArgs> evtProgressNotifications)
        {
            bool bResult = false;
            string chSplit;

            string sFilename = null;
            if (null != dictOptions && dictOptions.ContainsKey("Filename"))
            {
                sFilename = dictOptions["Filename"] as string;
            }

            if (sExportFormat == "Comma-Spearated Values")
            {
                chSplit = ",";
                if (String.IsNullOrEmpty(sFilename))
                {
                    sFilename = Fiddler.Utilities.ObtainSaveFilename("Export As " + sExportFormat, "CSV Files (*.csv)|*.csv");
                }
            }
            else
            {
                if (sExportFormat != "TAB-Separated Values")
                {
                    return false;
                }
                chSplit = "\t";
                if (String.IsNullOrEmpty(sFilename))
                {
                    sFilename = Fiddler.Utilities.ObtainSaveFilename("Export As " + sExportFormat, "TSV Files (*.tsv)|*.tsv");
                }
            }


            if (String.IsNullOrEmpty(sFilename))
            {
                return false;
            }

            try
            {
                StreamWriter swOutput = new StreamWriter(sFilename, false, Encoding.UTF8);
                int iCount = 0;
                int iMax = oSessions.Length;

                #region WriteColHeaders
                bool bFirstCol = true;
                foreach (ColumnHeader oLVCol in FiddlerApplication.UI.lvSessions.Columns)
                {
                    if (!bFirstCol)
                    {
                        swOutput.Write(chSplit);
                    }
                    else
                    {
                        bFirstCol = false;
                    }

                    swOutput.Write(oLVCol.Text.Replace(chSplit, ""));
                }
                swOutput.WriteLine();
                #endregion WriteColHeaders

                #region WriteEachSession
                foreach (Session oS in oSessions)
                {
                    iCount++;

                    if (null != oS.ViewItem)
                    {
                        bFirstCol = true;
                        ListViewItem oLVI = (oS.ViewItem as ListViewItem);
                        if (null == oLVI)
                        {
                            continue;
                        }
                        foreach (ListViewItem.ListViewSubItem oLVC in oLVI.SubItems)
                        {
                            if (!bFirstCol)
                            {
                                swOutput.Write(chSplit);
                            }
                            else
                            {
                                bFirstCol = false;
                            }

                            swOutput.Write(oLVC.Text.Replace(chSplit, ""));
                        }

                        swOutput.WriteLine();
                    }

                    if (null != evtProgressNotifications)
                    {
                        ProgressCallbackEventArgs PEAC = new ProgressCallbackEventArgs((iCount / (float)iMax), "wrote " + iCount.ToString() + " records.");
                        evtProgressNotifications(null, PEAC);

                        if (PEAC.Cancel)
                        {
                            swOutput.Close();
                            return false;
                        }
                    }
                }
                #endregion WriteEachSession

                swOutput.Close();
                bResult = true;
            }
            catch (Exception ex)
            {
                FiddlerApplication.Log.LogFormat("_Failed to export Error:{0}", ex.StackTrace);
                MessageBox.Show(ex.Message, "Failed to export");
                bResult = false;
            }

            return bResult;
        }
    }
}
