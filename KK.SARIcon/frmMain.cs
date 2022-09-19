using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace KK.SARIcon
{
    public partial class frmMain : Form
    {
        private Desktop m_Desktop = null;
        private String m_BaseTitle = "桌面图标位置保存与恢复";
        public frmMain()
        {
            InitializeComponent();
            Version v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            this.Text = $"{m_BaseTitle} - {v.Major}.{v.Minor}";
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                lblState.Text = String.Empty;
                toolStripStatusLabel1.Alignment = ToolStripItemAlignment.Right;

                WriteConsole("默认xml文件路径：" + Common.DefaultXmlPath);
                if (!System.IO.File.Exists(Common.DefaultXmlPath))
                {
                    WriteConsole("记录文件不存在。点击保存按钮生成新的文件");
                    btnRestoreIcon.Enabled = false;
                }
                m_Desktop = new Desktop();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                this.Dispose();
            }
        }

        private void btnSaveIcon_Click(object sender, EventArgs e)
        {
            try
            {

                // 保存图标数据到xml文件
                Boolean result = SaveIconsToXml(Common.DefaultXmlPath);
                if (result)
                {
                    lblState.Text = "保存完成！";
                }
                else
                {
                    lblState.Text = "保存失败！";
                }
            }
            catch (Exception ex)
            {
                WriteConsole("保存失败：" + ex.Message);
            }
            finally
            {
                btnRestoreIcon.Enabled = true;
            }
        }

        private void btnRestoreIcon_Click(object sender, EventArgs e)
        {
            try
            {

                RestoreIcons(Common.DefaultXmlPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("恢复失败：" + ex.Message);
            }
        }

        private void btnListIcons_Click(object sender, EventArgs e)
        {
            try
            {
                List<IconItem> icons = GetIcons();
                WriteConsole("获取到【" + icons.Count + "】个图标数据！");
                foreach (IconItem icon in icons)
                {
                    WriteConsole(icon.Index + ":" + icon.Text);
                }

            }
            catch (Exception ex)
            {
                WriteConsole("列举图标失败：" + ex.Message);
            }      }

        private void btnClearConsole_Click(object sender, EventArgs e)
        {
            txtConsole.Clear();
        }

        private void RestoreIcons( string xmlPath)
        {
            this.Icon = Properties.Resources.hello_;

            List<IconItem> icons = GetIcons();

            if (!System.IO.File.Exists(xmlPath))
            {
                MessageBox.Show("配置文件[" + xmlPath + "]不存在！先保存图标设置！");
                return;
            }

            // 读取XML文件，转化为集合
            XElement root = XElement.Load(xmlPath);
            IEnumerable<XElement> nodes = root.Elements();
            if (nodes != null && nodes.Count() > 0)
            {
                foreach (var node in nodes)
                {
                    String iconText = node.Attribute("text").Value;
                    Int32 locationX = Int32.Parse(node.Attribute("x").Value);
                    Int32 locationY = Int32.Parse(node.Attribute("y").Value);

                    // 检查图标是否存在，存在则设置位置，不存在就跳过
                    IconItem tmpIcon = icons.FirstOrDefault(x => x.Text == iconText);
                    if (tmpIcon != null)
                    {
                        m_Desktop.SetItemLocation(tmpIcon.Index, new Point(locationX, locationY));
                    }
                }
            }
            WriteConsole("恢复完成！");
            lblState.Text = "恢复完成!";

        }

        #region 公共处理
        private List<IconItem> GetIcons()
        {
            List<IconItem> icons = new List<IconItem>();
            this.Icon = Properties.Resources.friendly_icons;

            // 获取图标数量，遍历获取文本与坐标
            WriteConsole("桌面ListView句柄：" + m_Desktop.DesktopListViewPtr.ToString());
            WriteConsole("桌面进程ID：" + m_Desktop.DesktopProcessID);
            WriteConsole("侦测到桌面图标数：" + m_Desktop.GetItemsCount());
            Int32 iconsCount = m_Desktop.GetItemsCount();
            for (Int32 i = 0; i < iconsCount; i++)
            {
                IconItem tmpIcon = new IconItem();
                tmpIcon.Text = m_Desktop.GetItemText(i);
                tmpIcon.Location = m_Desktop.GetItemLocation(i);
                tmpIcon.Index = i;
                icons.Add(tmpIcon);
            }
            return icons;
        }

        private void WriteConsole(String msg)
        {
            txtConsole.AppendText(msg + "\r\n");
        }
        #endregion

        #region XML处理

        /// <summary>
        /// 保存列表到xml文件
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        private Boolean SaveIconsToXml(String xmlPath)
        {
            this.Icon = Properties.Resources.friendly_icons;
            List<IconItem> items = GetIcons();


            WriteConsole("获取到【" + items.Count + "】个图标数据！");

            if (items == null || items.Count == 0)
                return false;
            // 删除现有文件
            if (System.IO.File.Exists(xmlPath))
            {
                System.IO.File.Delete(xmlPath);
            }

            // 生成xml文件
            XElement root = new XElement("Items");
            for (Int32 i = 0; i < items.Count; i++)
            {
                XElement tmpItem = new XElement("Item");
                tmpItem.SetAttributeValue("text", items[i].Text);
                tmpItem.SetAttributeValue("x", items[i].Location.X.ToString());
                tmpItem.SetAttributeValue("y", items[i].Location.Y.ToString());
                root.Add(tmpItem);
            }
            root.Save(xmlPath);

            WriteConsole("保存完成！");
            return true;
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            // (x^2 + z^2 - 1)^3 - x^2*Z^3 = 0

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // 另存配置
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.InitialDirectory = Common.AppRoot;
                sfd.DefaultExt = "xml";
                sfd.OverwritePrompt = true;
                sfd.RestoreDirectory = true;
                sfd.AddExtension = true;
                sfd.FileName = "桌面图标位置配置" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml";
                sfd.Filter = "xml文件(*.xml)|*.xml";
                sfd.Title = "另存图标配置文件";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    SaveIconsToXml(sfd.FileName);
                    MessageBox.Show("保存成功！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存异常：" + ex.Message);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.InitialDirectory = Common.AppRoot;
                ofd.Multiselect = false;
                ofd.Title = "选择图标配置文件";
                ofd.Filter = "xml文件(*.xml)|*.xml";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    RestoreIcons(ofd.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载异常：" + ex.Message);
            }
        }
    }
}
