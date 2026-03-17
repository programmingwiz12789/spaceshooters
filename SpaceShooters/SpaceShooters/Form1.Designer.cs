
namespace SpaceShooters
{
    partial class SpaceShooters
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpaceShooters));
            this.playerLifeBar = new System.Windows.Forms.ProgressBar();
            this.playerScoreLbl = new System.Windows.Forms.Label();
            this.bossLifeBar = new System.Windows.Forms.ProgressBar();
            this.bossLifeLbl = new System.Windows.Forms.Label();
            this.playerLifeLbl = new System.Windows.Forms.Label();
            this.backBtn = new System.Windows.Forms.Button();
            this.pauseLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // playerLifeBar
            // 
            this.playerLifeBar.Location = new System.Drawing.Point(991, 12);
            this.playerLifeBar.Name = "playerLifeBar";
            this.playerLifeBar.Size = new System.Drawing.Size(171, 23);
            this.playerLifeBar.TabIndex = 0;
            this.playerLifeBar.Value = 100;
            // 
            // playerScoreLbl
            // 
            this.playerScoreLbl.AutoSize = true;
            this.playerScoreLbl.BackColor = System.Drawing.Color.Black;
            this.playerScoreLbl.ForeColor = System.Drawing.Color.White;
            this.playerScoreLbl.Location = new System.Drawing.Point(1046, 67);
            this.playerScoreLbl.Name = "playerScoreLbl";
            this.playerScoreLbl.Size = new System.Drawing.Size(68, 20);
            this.playerScoreLbl.TabIndex = 1;
            this.playerScoreLbl.Text = "Score: 0";
            // 
            // bossLifeBar
            // 
            this.bossLifeBar.Location = new System.Drawing.Point(339, 12);
            this.bossLifeBar.Name = "bossLifeBar";
            this.bossLifeBar.Size = new System.Drawing.Size(530, 23);
            this.bossLifeBar.TabIndex = 2;
            this.bossLifeBar.Value = 100;
            // 
            // bossLifeLbl
            // 
            this.bossLifeLbl.AutoSize = true;
            this.bossLifeLbl.BackColor = System.Drawing.Color.Black;
            this.bossLifeLbl.ForeColor = System.Drawing.Color.White;
            this.bossLifeLbl.Location = new System.Drawing.Point(578, 38);
            this.bossLifeLbl.Name = "bossLifeLbl";
            this.bossLifeLbl.Size = new System.Drawing.Size(45, 20);
            this.bossLifeLbl.TabIndex = 3;
            this.bossLifeLbl.Text = "Boss";
            // 
            // playerLifeLbl
            // 
            this.playerLifeLbl.AutoSize = true;
            this.playerLifeLbl.BackColor = System.Drawing.Color.Black;
            this.playerLifeLbl.ForeColor = System.Drawing.Color.White;
            this.playerLifeLbl.Location = new System.Drawing.Point(1061, 38);
            this.playerLifeLbl.Name = "playerLifeLbl";
            this.playerLifeLbl.Size = new System.Drawing.Size(35, 20);
            this.playerLifeLbl.TabIndex = 6;
            this.playerLifeLbl.Text = "Life";
            // 
            // backBtn
            // 
            this.backBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backBtn.Font = new System.Drawing.Font("OCR A Extended", 20F, System.Drawing.FontStyle.Bold);
            this.backBtn.ForeColor = System.Drawing.Color.Red;
            this.backBtn.Location = new System.Drawing.Point(12, 12);
            this.backBtn.Name = "backBtn";
            this.backBtn.Size = new System.Drawing.Size(75, 75);
            this.backBtn.TabIndex = 7;
            this.backBtn.Text = "<";
            this.backBtn.UseVisualStyleBackColor = true;
            this.backBtn.Click += new System.EventHandler(this.backBtn_Click);
            // 
            // pauseLbl
            // 
            this.pauseLbl.AutoSize = true;
            this.pauseLbl.BackColor = System.Drawing.Color.Transparent;
            this.pauseLbl.Font = new System.Drawing.Font("Algerian", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pauseLbl.ForeColor = System.Drawing.Color.Red;
            this.pauseLbl.Location = new System.Drawing.Point(332, 339);
            this.pauseLbl.Name = "pauseLbl";
            this.pauseLbl.Size = new System.Drawing.Size(556, 66);
            this.pauseLbl.TabIndex = 8;
            this.pauseLbl.Text = "PRESS S TO START";
            // 
            // SpaceShooters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1178, 744);
            this.Controls.Add(this.pauseLbl);
            this.Controls.Add(this.backBtn);
            this.Controls.Add(this.playerLifeLbl);
            this.Controls.Add(this.bossLifeLbl);
            this.Controls.Add(this.bossLifeBar);
            this.Controls.Add(this.playerScoreLbl);
            this.Controls.Add(this.playerLifeBar);
            this.KeyPreview = true;
            this.Name = "SpaceShooters";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SpaceShooters";
            this.Load += new System.EventHandler(this.SpaceShooters_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SpaceShooters_onPaint);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SpaceShooters_KeyUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SpaceShooters_mouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SpaceShooters_mouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SpaceShooters_mouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ProgressBar playerLifeBar;
        private System.Windows.Forms.Label playerScoreLbl;
        private System.Windows.Forms.ProgressBar bossLifeBar;
        private System.Windows.Forms.Label bossLifeLbl;
        private System.Windows.Forms.Label playerLifeLbl;
        private System.Windows.Forms.Button backBtn;
        private System.Windows.Forms.Label pauseLbl;
    }
}

