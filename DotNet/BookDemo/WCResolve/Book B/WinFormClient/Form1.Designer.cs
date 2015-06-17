namespace WinFormClient
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button_Browse = new System.Windows.Forms.Button();
            this.button_Send = new System.Windows.Forms.Button();
            this.checkBox_rs = new System.Windows.Forms.CheckBox();
            this.checkBox_od = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(623, 410);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // button_Browse
            // 
            this.button_Browse.Location = new System.Drawing.Point(13, 434);
            this.button_Browse.Name = "button_Browse";
            this.button_Browse.Size = new System.Drawing.Size(75, 23);
            this.button_Browse.TabIndex = 1;
            this.button_Browse.Text = "Browse";
            this.button_Browse.UseVisualStyleBackColor = true;
            // 
            // button_Send
            // 
            this.button_Send.Location = new System.Drawing.Point(109, 434);
            this.button_Send.Name = "button_Send";
            this.button_Send.Size = new System.Drawing.Size(75, 23);
            this.button_Send.TabIndex = 2;
            this.button_Send.Text = "Send";
            this.button_Send.UseVisualStyleBackColor = true;
            // 
            // checkBox_rs
            // 
            this.checkBox_rs.AutoSize = true;
            this.checkBox_rs.Location = new System.Drawing.Point(319, 434);
            this.checkBox_rs.Name = "checkBox_rs";
            this.checkBox_rs.Size = new System.Drawing.Size(114, 16);
            this.checkBox_rs.TabIndex = 3;
            this.checkBox_rs.Text = "Reliale Session";
            this.checkBox_rs.UseVisualStyleBackColor = true;
            // 
            // checkBox_od
            // 
            this.checkBox_od.AutoSize = true;
            this.checkBox_od.Location = new System.Drawing.Point(455, 433);
            this.checkBox_od.Name = "checkBox_od";
            this.checkBox_od.Size = new System.Drawing.Size(120, 16);
            this.checkBox_od.TabIndex = 4;
            this.checkBox_od.Text = "Ordered Delivery";
            this.checkBox_od.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 469);
            this.Controls.Add(this.checkBox_od);
            this.Controls.Add(this.checkBox_rs);
            this.Controls.Add(this.button_Send);
            this.Controls.Add(this.button_Browse);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Sender";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button_Browse;
        private System.Windows.Forms.Button button_Send;
        private System.Windows.Forms.CheckBox checkBox_rs;
        private System.Windows.Forms.CheckBox checkBox_od;
    }
}

