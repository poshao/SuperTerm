namespace Spoonson.Apps.SuperTerm
{
    partial class BaseSettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseSettingForm));
            this.label1 = new System.Windows.Forms.Label();
            this.txtPrintFilename = new System.Windows.Forms.TextBox();
            this.btnSelectPrintFilename = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "输出文件";
            // 
            // txtPrintFilename
            // 
            this.txtPrintFilename.Location = new System.Drawing.Point(73, 6);
            this.txtPrintFilename.Name = "txtPrintFilename";
            this.txtPrintFilename.Size = new System.Drawing.Size(278, 20);
            this.txtPrintFilename.TabIndex = 1;
            // 
            // btnSelectPrintFilename
            // 
            this.btnSelectPrintFilename.Location = new System.Drawing.Point(357, 4);
            this.btnSelectPrintFilename.Name = "btnSelectPrintFilename";
            this.btnSelectPrintFilename.Size = new System.Drawing.Size(27, 23);
            this.btnSelectPrintFilename.TabIndex = 2;
            this.btnSelectPrintFilename.Text = "...";
            this.btnSelectPrintFilename.UseVisualStyleBackColor = true;
            this.btnSelectPrintFilename.Click += new System.EventHandler(this.btnSelectPrintFilename_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(309, 207);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // BaseSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 249);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnSelectPrintFilename);
            this.Controls.Add(this.txtPrintFilename);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BaseSettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "基本设置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPrintFilename;
        private System.Windows.Forms.Button btnSelectPrintFilename;
        private System.Windows.Forms.Button btnSave;
    }
}