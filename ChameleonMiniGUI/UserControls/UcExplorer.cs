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
using ChameleonMiniGUI.Dump;

namespace ChameleonMiniGUI
{
    public partial class UcExplorer : UserControl
    {
        public UcExplorer()
        {
            InitializeComponent();
            SetTreeView();
        }

        private void SetTreeView()
        {
            treeView1.ImageList = TreeImages;
            GetLogicalDrives(treeView1);
            SetContextMenu();
        }

        private static ImageList _treeImagelist;
        private static ImageList TreeImages
        {
            get
            {
                if (_treeImagelist == null)
                {
                    _treeImagelist = new ImageList();
                    _treeImagelist.Images.Add("Harddrive", Properties.Resources.hardDrive);
                    _treeImagelist.Images.Add("CDrom", Properties.Resources.cdrom);
                    _treeImagelist.Images.Add("Networkdrive", Properties.Resources.networkDrive);
                    _treeImagelist.Images.Add("Folderopen", Properties.Resources.folderOpen);
                    _treeImagelist.Images.Add("Folderclose", Properties.Resources.folderClose);
                    _treeImagelist.Images.Add("Warning", Properties.Resources.warning);
                }
                return _treeImagelist;
            }
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
                    ImageIndex = 3,
                    SelectedImageIndex = 4
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
                    node.ImageIndex = 5;
                    node.SelectedImageIndex = 5;
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
            var node = new TreeNode(di.Name, 3, 4) { Tag = di };

            //
            if (di.GetDirectories().Any())
                node.Nodes.Add(null, "...", 5, 5);

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
                int selectectimage;
                switch (di.DriveType)
                {
                    case DriveType.CDRom:
                        image = 1;
                        selectectimage = 1;
                        break;
                    case DriveType.Network:
                        image = 2;
                        selectectimage = 2;
                        break;
                    case DriveType.NoRootDirectory:
                        image = 4;
                        selectectimage = 3;
                        break;
                    case DriveType.Unknown:
                        image = 5;
                        selectectimage = 5;
                        break;
                    default:
                        image = 0;
                        selectectimage = 0;
                        break;
                }
                var node = new TreeNode(drive.Substring(0, 1), image, selectectimage) { Tag = di };

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

        private void UpdateListView( TreeNode n)
        {
            listView1.Items.Clear();

            DirectoryInfo di;

            var tag = n.Tag as DriveInfo;
            if (tag != null)
            {
                di = tag.RootDirectory;
            }
            else
            {
                di = n.Tag as DirectoryInfo;
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
                item = new ListViewItem(file.Name, 1) { Tag = file };
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
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            UpdateListView(e.Node);
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
                        var node = new TreeNode(di.Name, 4, 3);

                        try
                        {
                            //keep the directory's full path in the tag for use later
                            node.Tag = di;

                            //if the directory has sub directories add the place holder
                            if (di.GetDirectories().Any())
                                node.Nodes.Add(null, "...", 4, 3);
                        }
                        catch (UnauthorizedAccessException)
                        {
                            //display a locked folder icon
                            node.ImageIndex = 4;
                            node.SelectedImageIndex = 4;
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
            var type = (DumpType)e.ClickedItem.Tag;
            ConvertFile((FileInfo)listView1.FocusedItem.Tag, type);

            UpdateListView( treeView1.SelectedNode);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            // directories can't be converted :)
            if (listView1.FocusedItem.SubItems[1].Text == "Directory")
                return;


            var binLabel = new ToolStripMenuItem { Text = "To BIN", Tag = DumpType.Bin };
            var jsonLabel = new ToolStripMenuItem { Text = "To JSON", Tag = DumpType.Json };
            var emlLabel = new ToolStripMenuItem { Text = "To EML", Tag = DumpType.Eml };
            var mctLabel = new ToolStripMenuItem { Text = "To MCT", Tag = DumpType.Mct };

            contextMenuStrip1.Items.Clear();
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { binLabel, jsonLabel, emlLabel, mctLabel });

            var tmp = listView1.FocusedItem.Text.ToLowerInvariant();

            if (tmp.Contains(".bin"))
            {
                contextMenuStrip1.Items.Remove(binLabel);
            }
            else if (tmp.Contains(".json"))
            {
                contextMenuStrip1.Items.Remove(jsonLabel);
            }
            else if (tmp.Contains(".eml"))
            {
                contextMenuStrip1.Items.Remove(emlLabel);
            }
            else if (tmp.Contains(".mct"))
            {
                contextMenuStrip1.Items.Remove(mctLabel);
            }
        }

        private void ConvertFile(FileInfo fi, DumpType to)
        {
            if (fi == null)
                return;
            var fromDumpStrategy = DumpStrategyFactory.Create(fi.FullName);
            var toDumpStrategy = DumpStrategyFactory.Create(to);
            toDumpStrategy.FileName = Path.ChangeExtension(fi.FullName, toDumpStrategy.Extension);
            var data = fromDumpStrategy.Read();
            toDumpStrategy.Save(data);
        }
    }
}
