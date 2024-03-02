namespace PizzaMaster_GADProject_
{
    partial class Map
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
            this.components = new System.ComponentModel.Container();
            this.timepanel = new System.Windows.Forms.Panel();
            this.lblToday = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pictureBoxLogOut = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.webBrowserPanel = new System.Windows.Forms.Panel();
            this.timepanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogOut)).BeginInit();
            this.SuspendLayout();
            // 
            // timepanel
            // 
            this.timepanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.timepanel.Controls.Add(this.lblToday);
            this.timepanel.Controls.Add(this.lblTime);
            this.timepanel.Controls.Add(this.pictureBoxLogOut);
            this.timepanel.Controls.Add(this.lblDate);
            this.timepanel.Controls.Add(this.panel4);
            this.timepanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.timepanel.Location = new System.Drawing.Point(0, 0);
            this.timepanel.Name = "timepanel";
            this.timepanel.Size = new System.Drawing.Size(1345, 75);
            this.timepanel.TabIndex = 28;
            // 
            // lblToday
            // 
            this.lblToday.AutoSize = true;
            this.lblToday.BackColor = System.Drawing.Color.Transparent;
            this.lblToday.Font = new System.Drawing.Font("Microsoft YaHei", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToday.ForeColor = System.Drawing.Color.White;
            this.lblToday.Location = new System.Drawing.Point(827, 23);
            this.lblToday.Name = "lblToday";
            this.lblToday.Size = new System.Drawing.Size(97, 31);
            this.lblToday.TabIndex = 26;
            this.lblToday.Text = "TODAY";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.BackColor = System.Drawing.Color.Transparent;
            this.lblTime.Font = new System.Drawing.Font("Microsoft YaHei", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.Color.White;
            this.lblTime.Location = new System.Drawing.Point(623, 23);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(158, 31);
            this.lblTime.TabIndex = 26;
            this.lblTime.Text = "00:00:00 PM";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.BackColor = System.Drawing.Color.Transparent;
            this.lblDate.Font = new System.Drawing.Font("Microsoft YaHei", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.White;
            this.lblDate.Location = new System.Drawing.Point(405, 23);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(172, 31);
            this.lblDate.TabIndex = 26;
            this.lblDate.Text = "MM DD YYYY";
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(13, 75);
            this.panel4.TabIndex = 25;
            // 
            // pictureBoxLogOut
            // 
            this.pictureBoxLogOut.BackgroundImage = global::PizzaMaster_GADProject_.Properties.Resources.previous;
            this.pictureBoxLogOut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxLogOut.Location = new System.Drawing.Point(19, 3);
            this.pictureBoxLogOut.Name = "pictureBoxLogOut";
            this.pictureBoxLogOut.Size = new System.Drawing.Size(70, 70);
            this.pictureBoxLogOut.TabIndex = 21;
            this.pictureBoxLogOut.TabStop = false;
            this.pictureBoxLogOut.Click += new System.EventHandler(this.pictureBoxLogOut_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // webBrowserPanel
            // 
            this.webBrowserPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserPanel.Location = new System.Drawing.Point(0, 75);
            this.webBrowserPanel.Name = "webBrowserPanel";
            this.webBrowserPanel.Size = new System.Drawing.Size(1345, 815);
            this.webBrowserPanel.TabIndex = 29;
            // 
            // Map
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(1345, 890);
            this.Controls.Add(this.webBrowserPanel);
            this.Controls.Add(this.timepanel);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Map";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Map";
            this.Load += new System.EventHandler(this.Map_Load);
            this.timepanel.ResumeLayout(false);
            this.timepanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogOut)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel timepanel;
        private System.Windows.Forms.Label lblToday;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox pictureBoxLogOut;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel webBrowserPanel;
    }
}