using System;
using System.Text;
using System.Windows.Forms;
namespace UserSpace
{
    public partial class UserCode
    {
        /// <summary>
        /// 用户代码逻辑
        /// </summary>
        public void UserScript()
        {
            System.Windows.Forms.Application.Run(new ReleaseFrom(this));
        }

        /// <summary>
        /// 解HOLD动作
        /// </summary>
        /// <param name="strOrder"></param>
        /// <returns></returns>
        public bool ReleaseOrder(string strOrder)
        {
            Sleep(1000);
            return false;
        }
    }

    public class ReleaseFrom : Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dvDatalist = new System.Windows.Forms.DataGridView();
            this.btnDo = new System.Windows.Forms.Button();
            this.colDNEI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colResult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dvDatalist)).BeginInit();
            this.SuspendLayout();
            // 
            // dvDatalist
            // 
            this.dvDatalist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvDatalist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDNEI,
            this.colResult});
            this.dvDatalist.Dock = System.Windows.Forms.DockStyle.Left;
            this.dvDatalist.Location = new System.Drawing.Point(0, 0);
            this.dvDatalist.Name = "dvDatalist";
            this.dvDatalist.Size = new System.Drawing.Size(268, 307);
            this.dvDatalist.TabIndex = 0;
            // 
            // btnDo
            // 
            this.btnDo.Location = new System.Drawing.Point(274, 12);
            this.btnDo.Name = "btnDo";
            this.btnDo.Size = new System.Drawing.Size(75, 35);
            this.btnDo.TabIndex = 1;
            this.btnDo.Text = "Do!!";
            this.btnDo.UseVisualStyleBackColor = true;
            this.btnDo.Click += new System.EventHandler(this.btnDo_Click);
            // 
            // colDNEI
            // 
            this.colDNEI.HeaderText = "单号";
            this.colDNEI.Name = "colDNEI";
            // 
            // colResult
            // 
            this.colResult.HeaderText = "结果";
            this.colResult.Name = "colResult";
            // 
            // ReleaseFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 307);
            this.Controls.Add(this.btnDo);
            this.Controls.Add(this.dvDatalist);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ReleaseFrom";
            this.Text = "ReleaseFrom";
            ((System.ComponentModel.ISupportInitialize)(this.dvDatalist)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dvDatalist;
        private System.Windows.Forms.Button btnDo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDNEI;
        private System.Windows.Forms.DataGridViewTextBoxColumn colResult;

        private UserCode m_script = null;
        public ReleaseFrom(UserCode script)
        {
            InitializeComponent();
            m_script = script;
        }

        private void btnDo_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dvDatalist.Rows.Count-1; i++)
            {
                //单元格空的value为null,引发错误

                string strOrder = dvDatalist.Rows[i].Cells[0].Value.ToString();
                Console.WriteLine(strOrder);
                //Console.WriteLine(string.Format("row {0} ,{1} ,{2}", i,
                //dvDatalist.Rows[i].Cells[0].Value.ToString(),
                //dvDatalist.Rows[i].Cells[1].Value.ToString()));

                if (m_script == null)
                {
                    Console.WriteLine("null");
                }

                if (m_script != null && m_script.ReleaseOrder(strOrder))
                {
                    Console.WriteLine("成功");
                }
                else
                {
                    Console.WriteLine("失败");
                }
            }
        }
    }
}