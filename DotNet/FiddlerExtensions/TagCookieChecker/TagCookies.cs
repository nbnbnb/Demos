using Fiddler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TagCookieChecker
{
    public class TagCookies : IAutoTamper2
    {
        private bool bEnabled = false;
        private bool bEnforceP3PValidity = false;
        private bool bCreatedColumn = false;
        private MenuItem miEnabled;
        private MenuItem miEnforceP3PValidity;
        private MenuItem mnuCookieTag;

        private enum P3PState
        {
            NoCookies,
            NoP3PAndSetsCookies,
            P3POk,
            P3PUnsatisfactory,
            P3PMalformed
        }

        public void AutoTamperRequestAfter(Session oSession)
        {

        }

        public void AutoTamperRequestBefore(Session oSession)
        {

        }

        public void AutoTamperResponseAfter(Session oSession)
        {

        }

        public void AutoTamperResponseBefore(Session oSession)
        {

        }

        public void OnBeforeReturningError(Session oSession)
        {

        }

        public void OnBeforeUnload()
        {

        }

        public void OnLoad()
        {
            FiddlerApplication.UI.mnuMain.MenuItems.Add(mnuCookieTag);
        }

        public void OnPeekAtResponseHeaders(Session oSession)
        {
            if (!bEnabled)
            {
                return;
            }

            P3PState oP3PState = P3PState.NoCookies;

            if (!oSession.oResponse.headers.Exists("Set-Cookie"))
            {
                return;
            }

            oP3PState = P3PState.NoP3PAndSetsCookies;

            if (oSession.oResponse.headers.Exists("P3P"))
            {
                SetP3PStateFromHeader(oSession.oResponse.headers["P3P"], ref oP3PState);
            }

            switch (oP3PState)
            {
                case P3PState.NoCookies:
                    oSession["ui-backcolor"] = "#ACDC85";
                    oSession["X-Privacy"] = "Sets cookies & P3P";
                    break;
                case P3PState.NoP3PAndSetsCookies:
                    oSession["ui-backcolor"] = "#FAFDA4";
                    oSession["X-Privacy"] = "Sets cookies without P3P";
                    break;
                case P3PState.P3POk:
                    oSession["ui-backcolor"] = "#EC921A";
                    oSession["X-Privacy"] = "Sets cookies; P3P unsat. for 3rd-party use";
                    break;
                case P3PState.P3PUnsatisfactory:
                    oSession["ui-backcolor"] = "#E90A05";
                    if (bEnforceP3PValidity)
                    {
                        oSession.oResponse.headers["MALFORMAT-P3P"] = oSession.oResponse.headers["P3P"];
                        oSession["X-Privacy"] = "MALFORMED P3P: " + oSession.oResponse.headers["P3P"];
                        oSession.oResponse.headers.Remove("P3P");
                    }
                    break;
                case P3PState.P3PMalformed:
                    break;
                default:
                    break;
            }
        }

        private void InitializeMenu()
        {
            this.miEnabled = new MenuItem("&Enabled");
            this.miEnforceP3PValidity = new MenuItem("&Rename P3P header if invalid");

            this.miEnabled.Index = 0;
            this.miEnforceP3PValidity.Index = 1;

            this.mnuCookieTag = new MenuItem("Privacy");
            this.mnuCookieTag.MenuItems.AddRange(new MenuItem[] {
                this.miEnabled,
                this.miEnforceP3PValidity
            });

            this.miEnabled.Click += MiEnabled_Click;
            this.miEnabled.Checked = bEnabled;

            this.miEnforceP3PValidity.Click += MiEnforceP3PValidity_Click;
            this.miEnforceP3PValidity.Checked = bEnforceP3PValidity;
        }

        private void MiEnabled_Click(object sender, EventArgs e)
        {
            miEnabled.Checked = !miEnabled.Checked;
            bEnabled = miEnabled.Checked;
            this.miEnforceP3PValidity.Enabled = bEnabled;
            if (bEnabled)
            {
                EnsureColumn();
            }

            FiddlerApplication.Prefs.SetBoolPref("extensions.tagcookies.enabled", bEnabled);
        }

        private void MiEnforceP3PValidity_Click(object sender, EventArgs e)
        {
            miEnforceP3PValidity.Checked = !miEnforceP3PValidity.Checked;
            bEnforceP3PValidity = miEnforceP3PValidity.Checked;
            FiddlerApplication.Prefs.SetBoolPref("extensions.tagcookies.EnforceP3PValidity", bEnforceP3PValidity);
        }

        private void EnsureColumn()
        {
            if (bCreatedColumn)
            {
                return;
            }

            FiddlerApplication.UI.lvSessions.AddBoundColumn("Privacy Info", 1, 120, "X-Privacy");
            bCreatedColumn = true;
        }

        public TagCookies()
        {
            this.bEnabled = FiddlerApplication.Prefs.GetBoolPref("extensions.tagcookies.enabled", false);
            this.bEnforceP3PValidity = FiddlerApplication.Prefs.GetBoolPref("extensions.tagcookies.EnforceP3PValidity", true);

            InitializeMenu();

            if (bEnabled)
            {
                EnsureColumn();
            }
            else
            {
                this.miEnforceP3PValidity.Enabled = false;
            }
        }

        private void SetP3PStateFromHeader(string sValue, ref P3PState oP3PState)
        {
            if (String.IsNullOrEmpty(sValue))
            {
                return;
            }

            string sUnsatCat = String.Empty;
            string sUnsatPurpose = String.Empty;
            sValue = sValue.Replace('\'', '"');

            string sCP = null;

            Regex r = new Regex("CP\\s?=\\s?[\"]?[\"]?(?<TokenValue>[^\";]*");
            Match m = r.Match(sValue);

            if (m.Success && (null != m.Groups["TokenValue"]))
            {
                sCP = m.Groups["TokenValue"].Value;
            }

            if (String.IsNullOrEmpty(sCP))
            {
                return;
            }

            oP3PState = P3PState.P3POk;

            string[] sTokens = sCP.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string sToken in sTokens)
            {
                if ((sTokens.Length < 3) || (sToken.Length) > 4)
                {
                    oP3PState = P3PState.P3PMalformed;
                    return;
                }

                if (",PHY,ONL,GOV,FIN,".IndexOf("," + sToken + ",", StringComparison.OrdinalIgnoreCase) > -1)
                {
                    sUnsatCat += (sTokens + " ");
                    continue;
                }
            }

            if ((sUnsatCat.Length) > 0 && (sUnsatPurpose.Length > 0))
            {
                if (oP3PState == P3PState.P3POk)
                {
                    oP3PState = P3PState.P3PUnsatisfactory;
                }
            }
        }
    }
}
