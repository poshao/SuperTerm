using System;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace Spoonson.Apps.SuperTerm
{
    public partial class HostListForm : Form
    {
        public HostListForm()
        {
            InitializeComponent();
            //加载数据
            loadData();
        }

        /// <summary>
        /// 加载地址库
        /// </summary>
        public void loadData()
        {
            var list = Spoonson.Common.Config.GetValue("hostlist");
            lvDetail.Items.Clear();
            foreach(Newtonsoft.Json.Linq.JProperty item in list)
            {
                var lvItem=lvDetail.Items.Add(item.Name,item.Name);
                lvItem.SubItems.Add(item.Value["url"].ToString());
                lvItem.SubItems.Add(item.Value["port"].ToString());
            }

        }

        private void lvDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvDetail.SelectedItems.Count > 0)
            {
                var item = lvDetail.SelectedItems[0];
                txtName.Text = item.Text;
                txtURL.Text = item.SubItems[1].Text;
                txtPort.Text = item.SubItems[2].Text;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //更新文件
            var jo = Spoonson.Common.Config.GetValue("hostlist") as JObject;
            JObject jhost = jo[txtName.Text] as JObject;
            if (jhost == null)
            {
                jhost = new JObject
                {
                    { "url", txtURL.Text },
                    { "port", txtPort.Text }
                };
                jo.Add(txtName.Text,jhost);
            }
            else
            {
                jhost["url"]  = txtURL.Text;
                jhost["port"]  = txtPort.Text;
            }
            Spoonson.Common.Config.Save();

            //更新界面
            foreach (ListViewItem item in lvDetail.Items)
            {
                if (item.Text == txtName.Text)
                {
                    item.SubItems[1].Text = txtURL.Text;
                    item.SubItems[2].Text = txtPort.Text;
                    return;
                }
            }
            var lvItem = lvDetail.Items.Add(txtName.Text, txtName.Text);
            lvItem.SubItems.Add(txtURL.Text);
            lvItem.SubItems.Add(txtPort.Text);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ((JObject)Spoonson.Common.Config.GetValue("hostlist")).Remove(txtName.Text);
            foreach(ListViewItem item in lvDetail.Items)
            {
                if (item.Text == txtName.Text) item.Remove();
            }
            Spoonson.Common.Config.Save();
            txtName.Text = "";
            txtURL.Text = "";
            txtPort.Text = "";
        }
    }
}
