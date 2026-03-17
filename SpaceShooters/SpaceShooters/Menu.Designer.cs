
namespace SpaceShooters
{
    partial class Menu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            this.titleLbl = new System.Windows.Forms.Label();
            this.playBtn = new System.Windows.Forms.Button();
            this.leaderboardBtn = new System.Windows.Forms.Button();
            this.exitBtn = new System.Windows.Forms.Button();
            this.controlsBtn = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // titleLbl
            // 
            this.titleLbl.AutoSize = true;
            this.titleLbl.BackColor = System.Drawing.Color.Transparent;
            this.titleLbl.Font = new System.Drawing.Font("Algerian", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLbl.ForeColor = System.Drawing.Color.Red;
            this.titleLbl.Location = new System.Drawing.Point(152, 30);
            this.titleLbl.Name = "titleLbl";
            this.titleLbl.Size = new System.Drawing.Size(514, 66);
            this.titleLbl.TabIndex = 5;
            this.titleLbl.Text = "SPACESHOOTERS";
            // 
            // playBtn
            // 
            this.playBtn.BackColor = System.Drawing.Color.Transparent;
            this.playBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.playBtn.Font = new System.Drawing.Font("OCR A Extended", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playBtn.ForeColor = System.Drawing.Color.Red;
            this.playBtn.Location = new System.Drawing.Point(242, 159);
            this.playBtn.Name = "playBtn";
            this.playBtn.Size = new System.Drawing.Size(335, 67);
            this.playBtn.TabIndex = 6;
            this.playBtn.Text = "PLAY";
            this.playBtn.UseVisualStyleBackColor = false;
            this.playBtn.Click += new System.EventHandler(this.playBtn_Click);
            // 
            // leaderboardBtn
            // 
            this.leaderboardBtn.BackColor = System.Drawing.Color.Transparent;
            this.leaderboardBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.leaderboardBtn.Font = new System.Drawing.Font("OCR A Extended", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leaderboardBtn.ForeColor = System.Drawing.Color.Red;
            this.leaderboardBtn.Location = new System.Drawing.Point(242, 232);
            this.leaderboardBtn.Name = "leaderboardBtn";
            this.leaderboardBtn.Size = new System.Drawing.Size(335, 67);
            this.leaderboardBtn.TabIndex = 7;
            this.leaderboardBtn.Text = "LEADERBOARD";
            this.leaderboardBtn.UseVisualStyleBackColor = false;
            this.leaderboardBtn.Click += new System.EventHandler(this.leaderboardBtn_Click);
            // 
            // exitBtn
            // 
            this.exitBtn.BackColor = System.Drawing.Color.Transparent;
            this.exitBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitBtn.Font = new System.Drawing.Font("OCR A Extended", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitBtn.ForeColor = System.Drawing.Color.Red;
            this.exitBtn.Location = new System.Drawing.Point(242, 378);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(335, 67);
            this.exitBtn.TabIndex = 8;
            this.exitBtn.Text = "EXIT";
            this.exitBtn.UseVisualStyleBackColor = false;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // controlsBtn
            // 
            this.controlsBtn.BackColor = System.Drawing.Color.Transparent;
            this.controlsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.controlsBtn.Font = new System.Drawing.Font("OCR A Extended", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.controlsBtn.ForeColor = System.Drawing.Color.Red;
            this.controlsBtn.Location = new System.Drawing.Point(242, 305);
            this.controlsBtn.Name = "controlsBtn";
            this.controlsBtn.Size = new System.Drawing.Size(335, 67);
            this.controlsBtn.TabIndex = 9;
            this.controlsBtn.Text = "CONTROLS";
            this.controlsBtn.UseVisualStyleBackColor = false;
            this.controlsBtn.Click += new System.EventHandler(this.controlsBtn_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(800, 525);
            this.Controls.Add(this.controlsBtn);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.leaderboardBtn);
            this.Controls.Add(this.playBtn);
            this.Controls.Add(this.titleLbl);
            this.Name = "Menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu";
            this.Load += new System.EventHandler(this.Menu_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SpaceShooters_onPaint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titleLbl;
        private System.Windows.Forms.Button playBtn;
        private System.Windows.Forms.Button leaderboardBtn;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.Button controlsBtn;
        private System.Windows.Forms.Timer timer1;
    }
}