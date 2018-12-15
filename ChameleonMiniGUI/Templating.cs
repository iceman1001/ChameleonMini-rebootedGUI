using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Be.Windows.Forms;
using DynamicExpresso;

namespace ChameleonMiniGUI
{
    public class Templating
    {
        public string DirectoryPath { get; set; }

        public Templating()
        {
            DirectoryPath = Path.Combine(Application.StartupPath, "Templates");
        }

        public void LoadTemplate(HexBox hb, string template, List<IlegendItem> legendItems )
        {
            if (hb.ByteProvider == null) return;

            hb.ClearHighlights();

            var fn = Path.Combine(DirectoryPath, template);
            if (!File.Exists(fn))
                return;

            if (legendItems == null)
                legendItems = new List<IlegendItem>();

            var strings = File.ReadAllLines(fn);

            var interpreter = new Interpreter();

            foreach (var line in strings)
            {

                if (string.IsNullOrWhiteSpace(line)) continue;
                if (line.StartsWith("#")) continue;

                var props = line.Split(';');
                if (props.Length < 4)
                {
                    Console.WriteLine($"[-] Error, bad line in template file. {props}");
                    continue;
                }

                try
                {

                    var startpos_eval = interpreter.Eval(props[0].Trim());
                    int startpos;
                    if (!int.TryParse(startpos_eval.ToString(), out startpos)) continue;

                    var length_eval = interpreter.Eval(props[1].Trim());
                    int length;
                    if (!int.TryParse(length_eval.ToString(), out length)) continue;

                    var fg = props[2].Trim();
                    var bg = props[3].Trim();
                    var desc = (props.Length == 5) ? props[4].Trim() : string.Empty;

                    var fgc = Color.FromName(fg);
                    var bgc = Color.FromName(bg);


                    // length check.
                    if (hb.ByteProvider.Length < (startpos + length))
                        continue;

                    hb.AddHighlight(startpos, length, fgc, bgc);

                    var item = new LegendItem
                    {
                        BackGroundColor = bg,
                        ForeGroundColor = fg,
                        Description = desc
                    };

                    var exists = legendItems.Any(a => a.BackGroundColor == bg && a.ForeGroundColor == fg && a.Description == desc);
                    if (!exists )                    
                        legendItems.Add(item);                  
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Template has errors{Environment.NewLine}{line}{Environment.NewLine}{ex.Message}");
                }
            }
            hb.Invalidate();
        }

        public Dictionary<string, string> GetTemplates()
        {
            var dic = new Dictionary<string, string>();
            var di = new DirectoryInfo(DirectoryPath);
            if (!di.Exists)
                return dic;

            var files = di.GetFiles("*.txt").OrderBy(i =>i.Name);
            if (!files.Any()) return dic;

            dic.Add("Not selected", "----------");
            foreach (var f in files)
            {
                dic.Add(f.Name.Replace(f.Extension, ""), f.Name);
            }
            return dic;
        }

    }
}