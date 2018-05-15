using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChameleonMiniGUI
{
    public enum ConvertFileEnum
    {
        Eml,
        Bin,
        Mfd
    }
    public partial class UcExplorer : UserControl
    {
        public UcExplorer()
        {
            InitializeComponent();
            SetTreeView();
        }

        private void SetTreeView()
        {
            GetLogicalDrives(treeView1);
            SetContextMenu();
        }

        private void PopulateDirectories_recursive(DirectoryInfo[] dirs, TreeNode parent)
        {
            TreeNode node;
            DirectoryInfo[] childDirs;
            foreach (var dir in dirs)
            {
                node = new TreeNode(dir.Name, 0, 0)
                {
                    Tag = dir,
                    ImageKey = "folder"
                };
                try
                {
                    childDirs = dir.GetDirectories();
                    if (childDirs.Length != 0)
                    {
                        PopulateDirectories_recursive(childDirs, node);
                    }
                }
                catch (UnauthorizedAccessException uae)
                {
                    node.ImageIndex = 12;
                    node.SelectedImageIndex = 12;
                }
                finally
                {
                    parent.Nodes.Add(node);
                }
            }
        }

        private void AddSpecialFolder(string name, TreeView parent)
        {
            if (string.IsNullOrWhiteSpace(name))
                return; 

            var di = new DirectoryInfo(name);
            var node = new TreeNode(di.Name, 2, 2) { Tag = di };

            //
            if ( di.GetDirectories().Any() )
                node.Nodes.Add(null, "...", 0, 0);

            parent.Nodes.Add(node);
        }

        private void GetLogicalDrives(TreeView parent)
        {
            // Add Chameleon Mini application folder 
            AddSpecialFolder(@"../..", parent);
            
            // Some special folders
            AddSpecialFolder(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), parent);
            AddSpecialFolder(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), parent);

            // DRIVES
            var drives = Environment.GetLogicalDrives();
            foreach (var drive in drives)
            {
                var di = new DriveInfo(drive);
                int image;
                switch (di.DriveType)
                {
                    case DriveType.CDRom:
                        continue;
                    case DriveType.Network:
                        image = 6;
                        break;
                    case DriveType.NoRootDirectory:
                        image = 8;
                        break;
                    case DriveType.Unknown:
                        image = 8;
                        break;
                    default:
                        image = 2;
                        break;
                }
                var node = new TreeNode(drive.Substring(0, 1), image, image) {Tag = di};

                if (di.IsReady)
                {
                    node.Nodes.Add("...");
                }

                parent.Nodes.Add(node);
            }
        }

        private void SetContextMenu()
        {
            listView1.ContextMenuStrip = contextMenuStrip1;
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var newSelected = e.Node;
            listView1.Items.Clear();

            DirectoryInfo di;

            var tag = newSelected.Tag as DriveInfo;
            if (tag != null)
            {
                di = tag.RootDirectory;
            }
            else
            {
                di = newSelected.Tag as DirectoryInfo;
            }


            ListViewItem.ListViewSubItem[] subItems;
            ListViewItem item;

            foreach (var dir in di.GetDirectories())
            {
                item = new ListViewItem(dir.Name, 0);
                subItems = new[]
                {
                    new ListViewItem.ListViewSubItem(item, "Directory"),
                    new ListViewItem.ListViewSubItem(item, ""),
                    new ListViewItem.ListViewSubItem(item, dir.LastAccessTime.ToShortDateString()),
                };

                item.SubItems.AddRange(subItems);
                listView1.Items.Add(item);
            }

            foreach (var file in di.GetFiles())
            {
                var len = $"{ (file.Length / 1024)} kb";
                item = new ListViewItem(file.Name, 1) {Tag = file};
                subItems = new[]
                {
                    new ListViewItem.ListViewSubItem(item, "File"),
                    new ListViewItem.ListViewSubItem(item, len),
                    new ListViewItem.ListViewSubItem(item, file.LastAccessTime.ToShortDateString()),
                };

                item.SubItems.AddRange(subItems);
                listView1.Items.Add(item);
            }

            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes.Count > 0)
            {
                if (e.Node.Nodes[0].Text == "..." && e.Node.Nodes[0].Tag == null)
                {
                    e.Node.Nodes.Clear();

                    //get the list of sub direcotires
                    string[] dirs = Directory.GetDirectories(e.Node.Tag.ToString());

                    foreach (string dir in dirs)
                    {
                        var di = new DirectoryInfo(dir);
                        var node = new TreeNode(di.Name, 0, 1);

                        try
                        {
                            //keep the directory's full path in the tag for use later
                            node.Tag = di;

                            //if the directory has sub directories add the place holder
                            if (di.GetDirectories().Any())
                                node.Nodes.Add(null, "...", 0, 0);
                        }
                        catch (UnauthorizedAccessException)
                        {
                            //display a locked folder icon
                            node.ImageIndex = 12;
                            node.SelectedImageIndex = 12;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "DirectoryLister", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            e.Node.Nodes.Add(node);
                        }
                    }
                }
            }
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // something
            var tmp = e.ClickedItem.Text.ToLowerInvariant().Replace("to ", "");
            switch (tmp)
            {
                case "eml":
                    ConvertFile( (FileInfo)listView1.FocusedItem.Tag, ConvertFileEnum.Eml);
                    break;
                case "bin":
                    ConvertFile((FileInfo)listView1.FocusedItem.Tag, ConvertFileEnum.Bin);
                    break;
                case "mfd":
                    ConvertFile((FileInfo)listView1.FocusedItem.Tag, ConvertFileEnum.Mfd);
                    break;

            }
        }
    
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            // directories can't be converted :)
            if (listView1.FocusedItem.SubItems[1].Text == "Directory")
                return;


            var emllabel = new ToolStripMenuItem { Text = "To EML" };
            var binlabel = new ToolStripMenuItem { Text = "To BIN" };
            var mfdlabel = new ToolStripMenuItem { Text = "To MFD" };

            contextMenuStrip1.Items.Clear();
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { emllabel, binlabel, mfdlabel });

            var tmp = listView1.FocusedItem.Text.ToLowerInvariant();

            if (tmp.Contains(".eml"))
            {
                contextMenuStrip1.Items.Remove( emllabel);
            } else if (tmp.Contains(".bin"))
            {
                contextMenuStrip1.Items.Remove(binlabel);

            } else if (tmp.Contains(".mfd"))
            {
                contextMenuStrip1.Items.Remove(mfdlabel);
            }
        }

        private void ConvertFile( FileInfo fi, ConvertFileEnum to)
        {
            if ( fi == null)
                return;

            switch (to)
            {
                case ConvertFileEnum.Eml:
                {
                    var bytes = File.ReadAllBytes( fi.FullName);

                    // read bytes & convert to ascii
                    var bytesleft = bytes.Length;
                    var pos = 0;
                    var rows = new List<string>();
                    while ( bytesleft > 0 )
                    {
                        var len = Math.Min(bytesleft, 16);
                        rows.Add( BitConverter.ToString( bytes, pos, len ).Replace("-", " "));
                        pos += len;
                        bytesleft -= len;
                    }

                    // save text file
                    var sfilename = fi.FullName.Replace(fi.Extension, ".eml");
                    File.WriteAllLines(sfilename, rows, Encoding.ASCII);
                    break;
                }
                case ConvertFileEnum.Bin:
                {
                    var rows = File.ReadAllLines( fi.FullName);

                    var sfilename = fi.FullName.Replace(fi.Extension, ".bin");

                    using ( var w = new BinaryWriter( new FileStream(sfilename, FileMode.Create)))
                    {
                        foreach (var row in rows)
                        {
                            var clean = row.Replace(":", "").Replace(" ", "");

                            if (string.IsNullOrWhiteSpace(clean))
                                continue;


                            int pos = 0;
                            var bytes = new byte[clean.Length >> 1];
                            for (int i = 0; i < clean.Length; i += 2)
                            {
                                var b = Convert.ToByte(clean.Substring(i, 2), 16);
                                bytes[pos++] = b;
                            }

                            w.Write(bytes);
                        }
                    }

                    break;
                }
            }           
        }
    }
}
