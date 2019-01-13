using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Spoonson.Common;

namespace Spoonson.Apps.SuperTerm
{
    public partial class BaseSettingForm : Form
    {
        public BaseSettingForm()
        {
            InitializeComponent();
            LoadData();
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            txtPrintFilename.Text = Config.GetValue("baseSetting")["printfilename"].ToString();
        }

        private void btnSelectPrintFilename_Click(object sender, EventArgs e)
        {
            using (var ofn=new OpenFileDialog())
            {
                if (ofn.ShowDialog() == DialogResult.OK)
                {
                    ofn.InitialDirectory = Environment.CurrentDirectory;
                    txtPrintFilename.Text = ofn.FileName;
                    Config.GetValue("baseSetting")["printfilename"] = txtPrintFilename.Text;
                    Config.Save();
                }
            }
        }
    }
}
