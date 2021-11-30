﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Web;
/*
 * Copyright ©2021 Harshal Kudale
 * SPDX-License-Identifier: MIT
 * License-Filename: LICENSE
 */

namespace NoMoreEdge
{
    static class NoMoreEdge
    {

        [STAThread]
        static Boolean check_bang(string url)
        {
            if (url.Contains("%2521"))
                return true;
            else
                return false;
        }

        static string defEngine(string engine)
        {
            string engineurl = "";
            switch (engine)
            {
                case "google":
                    engineurl = "https://www.google.com/search?q=";
                    break;
                case "duckduckgo":
                    engineurl = "https://duckduckgo.com/?q=";
                    break;
                case "ecosia":
                    engineurl = "https://www.ecosia.org/search?q=";
                    break;
                case "sogou":
                    engineurl = "https://www.sogou.com/web?query=";
                    break;
                case "yahoo":
                    engineurl = "https://search.yahoo.com/search?p=";
                    break;
                case "yandex":
                    engineurl = "https://yandex.com/search/?text=";
                    break;
                case "ask":
                    engineurl = "https://www.ask.com/web?q=";
                    break;
                case "brave":
                    engineurl = "https://search.brave.com/search?q=";
                    break;
                default:
                    MessageBox.Show("wrong engine");
                    break;
            }
            return engineurl;
        }

        private static string DecodeUrlString(string url)
        {
            string newUrl;
            while ((newUrl = Uri.UnescapeDataString(url)) != url)
                url = newUrl;
            return newUrl;
        }
        static void Main(string[] args)
        {
            //string url = "microsoft-edge:?launchContext1=Microsoft.Windows.Search_cw5n1h2txyewy&url=https%3A%2F%2Fwww.bing.com%2Fsearch%3Fq%3D%2521reddit%2Bwebsite%26filters%3Dufn%253a%2522Reddit%2522%2Bsid%253a%252220757754-e543-a49f-c338-3463e22655c3%2522%26form%3DWSBEDG%26qs%3DMB%26cvid%3Daafc035c9d924cd493477b7f4854107d%26pq%3Dreddit%26cc%3DIN%26setlang%3Den-US%26nclid%3DA7CDEE0718C7A2E6219EAA07F2209F3B%26ts%3D1637862572948%26nclidts%3D1637862572%26tsms%3D948%26wsso%3DModerate";
            //string url = "microsoft-edge://https://www.google.com";
            //string url = "microsoft-edge:?upn=abc%40gmail.com&cid=8208f3b1a83e496b&source=Windows.Widgets&timestamp=1637894205027&url=https%3A%2F%2Fwww.msn.com%2Fen-in%2Fmoney%2Fnews%2Fblack-friday-sale-here-are-the-top-deals-on-iphones-oneplus-and-other-phones-you-can-t-miss%2Far-AAR82bF%3Focid%3Dwinp2octtaskbar";
            //string url = "microsoft-edge:?launchContext1=Microsoft.Windows.Search_cw5n1h2txyewy&url=http%3A%2F%2Fwww.amazon.in%2F";
            //string url = "microsoft-edge:?launchContext1=Microsoft.Windows.Search_cw5n1h2txyewy&url=https%3A%2F%2Fwww.bing.com%2FWS%2Fredirect%2F%3Fq%3Damazon.in%26url%3DaHR0cHM6Ly93d3cuYW1hem9uLmluL2luZGlhL3M%2Faz1pbmRpYQ%3D%3D%26form%3DWSBSTK%26cvid%3D862ffe62e8aa4be1b7b103c5ac4d5d08%26rtk%3DRShhlsdPnBoyLzMkHlFi3uiMwVjRyHBlh%252FV1sy1YmaiOwrtpcAVI%252FIXH0ospojDn";
            //string url = "microsoft-edge:https://www.bing.com/images/search?q=walker+bay+south+africa+whales&filters=IsConversation:%22True%22+BTWLKey:%22WalkerBaySouthAfrica%22+BTWLType:%22Trivia%22&trivia=1&qft=+filterui:photo-photo&FORM=EMSDS0";


            if (args[args.Length - 1].Contains("microsoft-edge"))
            //if(true)
            {
                string url = args[args.Length - 1];
                string engine = "google";
                if (args.Length == 4)
                {
                    engine = args[0];
                }
                url = url.Substring(url.IndexOf("http"), url.Length - url.IndexOf("http"));
                if (url.Contains("%26") && !url.Contains("redirect"))
                    url = url.Substring(url.IndexOf("http"), url.IndexOf("%26"));
                if (check_bang(url))
                    engine = "duckduckgo";
                url = DecodeUrlString(url);
                MessageBox.Show(url);
                url = url.Replace("https://www.bing.com/search?q=", defEngine(engine));
                Uri uri = new(url.ToString());
                Uri.UnescapeDataString(url);
                ProcessStartInfo launcher = new ProcessStartInfo(uri.ToString())
                {
                    UseShellExecute = true
                };
                Process.Start(launcher);
            }
            else
            {
                string varient = "";
                if (args[1].Contains("Beta"))
                {
                    varient = " Beta";
                }
                else if (args[1].Contains("Dev"))
                {
                    varient = " Dev";
                }
                string edgepath = "\"C:\\Program Files (x86)\\Microsoft\\Edge" + varient + "\\Application\\msedge_backup.exe\"";
                string argument = "";
                for (int i = 2; i < args.Length; i++)
                {
                    argument = argument + args[i] + " ";
                }

                Process p = new Process
                {
                    StartInfo = { UseShellExecute = false, RedirectStandardOutput = true, FileName = edgepath, Arguments = argument }
                };
                p.Start();
            }

        }
    }
}
