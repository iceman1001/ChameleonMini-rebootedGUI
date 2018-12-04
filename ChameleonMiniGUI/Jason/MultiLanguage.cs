using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ChameleonMiniGUI
{
    public class MultiLanguage
    {
        public string DirectoryPath { get; set; }

        public MultiLanguage ()
        {
            DirectoryPath = Path.Combine(Application.StartupPath, "Languages");
        }

        private static IEnumerable<T> FindToolStrips<T>(ICollection ctrls) where T : ToolStrip
        {
            var list = new List<T>();

            // make sure we have controls to search for.
            if (ctrls == null || ctrls.Count == 0) return list;

            foreach (Control c in ctrls)
            {
                if (c.HasChildren)
                    list.AddRange(FindToolStrips<T>(c.Controls));

                if ( c.ContextMenuStrip != null )
                    list.Add(c.ContextMenuStrip as T);
            }

            return list;
        }

        private static IEnumerable<T> FindControls<T>(ICollection ctrls) where T : Control
        {
            var list = new List<T>();

            // make sure we have controls to search for.
            if (ctrls == null || ctrls.Count == 0) return list;

            foreach (Control c in ctrls)
            {
                if (c.HasChildren)
                {
                    list.AddRange(FindControls<T>(c.Controls));
                }

                if ( !string.IsNullOrWhiteSpace(c.Text))
                    list.Add(c as T);
            }

            return list;
        }

        private static IEnumerable<T> FindControls<T>(ICollection ctrls, string searchname) where T : Control
        {
            var list = new List<T>();

            // make sure we have controls to search for.
            if (ctrls == null || ctrls.Count == 0) return list;
            if (string.IsNullOrWhiteSpace(searchname)) return list;


            foreach (Control cb in ctrls)
            {
                if (cb.HasChildren)
                {
                    list.AddRange(FindControls<T>(cb.Controls, searchname));
                }

                if (cb.Name.ToLowerInvariant().StartsWith(searchname))
                    list.Add(cb as T);
            }

            return list;
        }

        public void GetText( ICollection ctrls)
        {
            var list = FindControls<Control>(ctrls);
            foreach (var i in list)
            {
                Console.WriteLine( $"{i.Name}={i.Text}" );
            }
        }
        
        public void LoadLanguage(ICollection ctrls, string lang)
        {
            var fn = Path.Combine(DirectoryPath, lang);
            if (!File.Exists(fn))
                return;

            var strings = File.ReadAllLines(fn);

            foreach (var line in strings)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                // skip comments
                if (line.StartsWith("#")) continue;

                var property = line.Split('.');
                var values = line.Split('=');
                var txt = values[1];
                var propertyname = property[0].ToLowerInvariant();

                var list_ctrls = FindControls<Control>(ctrls, propertyname);
                foreach (var i in list_ctrls)
                {
                    i.Text = txt;
                }

                var list_items = FindToolStrips<ToolStrip>(ctrls);
                foreach (var i in list_items)
                {
                    foreach(ToolStripMenuItem j in i.Items)
                    {
                        if (j.Name.ToLowerInvariant().StartsWith(propertyname)) 
                            j.Text = txt;
                    }
                }
            }
        }

        public Dictionary<string, string> GetLanguages()
        {
            var dic = new Dictionary<string, string>();
            var di = new DirectoryInfo( DirectoryPath );
            if (!di.Exists)
                return dic;

            foreach (var f in di.GetFiles("*.txt").OrderBy( i =>i.Name))
            {                 
                dic.Add( f.Name.Replace(f.Extension,"") , f.Name);
            }
            return dic;
        }
    }
}