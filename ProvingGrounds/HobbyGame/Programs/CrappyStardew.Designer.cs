namespace HobbyGame.Programs {
    partial class CrappyStardew {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.createCharacterButton = new System.Windows.Forms.Button();
            this.saveMapButton = new System.Windows.Forms.Button();
            this.loadMapButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.framesPerSecTextbox = new System.Windows.Forms.TextBox();
            this.statTimer = new System.Windows.Forms.Timer(this.components);
            this.loopsPerSecTextbox = new System.Windows.Forms.TextBox();
            this.keypressTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // createCharacterButton
            // 
            this.createCharacterButton.BackColor = System.Drawing.Color.Tomato;
            this.createCharacterButton.Location = new System.Drawing.Point(133, 12);
            this.createCharacterButton.Name = "createCharacterButton";
            this.createCharacterButton.Size = new System.Drawing.Size(75, 27);
            this.createCharacterButton.TabIndex = 8;
            this.createCharacterButton.Text = "Add Person";
            this.createCharacterButton.UseVisualStyleBackColor = false;
            this.createCharacterButton.Click += new System.EventHandler(this.createCharacterButton_Click);
            // 
            // saveMapButton
            // 
            this.saveMapButton.BackColor = System.Drawing.Color.Tomato;
            this.saveMapButton.Location = new System.Drawing.Point(377, 12);
            this.saveMapButton.Name = "saveMapButton";
            this.saveMapButton.Size = new System.Drawing.Size(75, 27);
            this.saveMapButton.TabIndex = 13;
            this.saveMapButton.Text = "Save Map";
            this.saveMapButton.UseVisualStyleBackColor = false;
            this.saveMapButton.Click += new System.EventHandler(this.saveMapButton_Click);
            // 
            // loadMapButton
            // 
            this.loadMapButton.BackColor = System.Drawing.Color.Tomato;
            this.loadMapButton.Location = new System.Drawing.Point(255, 12);
            this.loadMapButton.Name = "loadMapButton";
            this.loadMapButton.Size = new System.Drawing.Size(75, 27);
            this.loadMapButton.TabIndex = 14;
            this.loadMapButton.Text = "Load Map";
            this.loadMapButton.UseVisualStyleBackColor = false;
            this.loadMapButton.Click += new System.EventHandler(this.loadMapButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.DimGray;
            this.textBox1.Location = new System.Drawing.Point(12, 16);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(95, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // framesPerSecTextbox
            // 
            this.framesPerSecTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.framesPerSecTextbox.BackColor = System.Drawing.Color.DimGray;
            this.framesPerSecTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.framesPerSecTextbox.ForeColor = System.Drawing.Color.Snow;
            this.framesPerSecTextbox.Location = new System.Drawing.Point(1108, 12);
            this.framesPerSecTextbox.Name = "framesPerSecTextbox";
            this.framesPerSecTextbox.ReadOnly = true;
            this.framesPerSecTextbox.Size = new System.Drawing.Size(84, 24);
            this.framesPerSecTextbox.TabIndex = 15;
            this.framesPerSecTextbox.Text = "0";
            this.framesPerSecTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // statTimer
            // 
            this.statTimer.Interval = 1000;
            this.statTimer.Tick += new System.EventHandler(this.statTimer_Tick);
            // 
            // loopsPerSecTextbox
            // 
            this.loopsPerSecTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loopsPerSecTextbox.BackColor = System.Drawing.Color.DimGray;
            this.loopsPerSecTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loopsPerSecTextbox.ForeColor = System.Drawing.Color.Snow;
            this.loopsPerSecTextbox.Location = new System.Drawing.Point(1018, 12);
            this.loopsPerSecTextbox.Name = "loopsPerSecTextbox";
            this.loopsPerSecTextbox.ReadOnly = true;
            this.loopsPerSecTextbox.Size = new System.Drawing.Size(84, 24);
            this.loopsPerSecTextbox.TabIndex = 16;
            this.loopsPerSecTextbox.Text = "0";
            this.loopsPerSecTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // keypressTimer
            // 
            this.keypressTimer.Interval = 25;
            this.keypressTimer.Tick += new System.EventHandler(this.keypressTimer_Tick);
            // 
            // CrappyStardew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1204, 660);
            this.Controls.Add(this.loopsPerSecTextbox);
            this.Controls.Add(this.framesPerSecTextbox);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.loadMapButton);
            this.Controls.Add(this.saveMapButton);
            this.Controls.Add(this.createCharacterButton);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Name = "CrappyStardew";
            this.ShowIcon = false;
            this.Text = "Crappy Stardew";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CrappyStardew_FormClosing);
            this.Load += new System.EventHandler(this.CrappyStardew_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CrappyStardew_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CrappyStardew_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button createCharacterButton;
        private System.Windows.Forms.Button saveMapButton;
        private System.Windows.Forms.Button loadMapButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox framesPerSecTextbox;
        private System.Windows.Forms.Timer statTimer;
        private System.Windows.Forms.TextBox loopsPerSecTextbox;
        private System.Windows.Forms.Timer keypressTimer;
    }
}