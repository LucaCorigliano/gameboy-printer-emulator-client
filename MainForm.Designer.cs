namespace gameboy_printer_windows
{
    partial class MainForm
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboSerialPort = new System.Windows.Forms.ComboBox();
            this.btnSerialConnect = new System.Windows.Forms.Button();
            this.lblSerialPort = new System.Windows.Forms.Label();
            this.txtSerialLog = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.picPanel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkRemoveBorder = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblMagnify = new System.Windows.Forms.Label();
            this.comboMagnify = new System.Windows.Forms.ComboBox();
            this.lblColorPalette = new System.Windows.Forms.Label();
            this.comboColorPalette = new System.Windows.Forms.ComboBox();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboSerialPort
            // 
            this.comboSerialPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSerialPort.FormattingEnabled = true;
            this.comboSerialPort.Location = new System.Drawing.Point(6, 26);
            this.comboSerialPort.Name = "comboSerialPort";
            this.comboSerialPort.Size = new System.Drawing.Size(70, 21);
            this.comboSerialPort.TabIndex = 0;
            this.comboSerialPort.DropDown += new System.EventHandler(this.onSerialPortDropdown);
            // 
            // btnSerialConnect
            // 
            this.btnSerialConnect.Location = new System.Drawing.Point(82, 27);
            this.btnSerialConnect.Name = "btnSerialConnect";
            this.btnSerialConnect.Size = new System.Drawing.Size(121, 21);
            this.btnSerialConnect.TabIndex = 1;
            this.btnSerialConnect.Text = "Connect";
            this.btnSerialConnect.UseVisualStyleBackColor = true;
            this.btnSerialConnect.Click += new System.EventHandler(this.onConnect);
            // 
            // lblSerialPort
            // 
            this.lblSerialPort.AutoSize = true;
            this.lblSerialPort.Location = new System.Drawing.Point(3, 9);
            this.lblSerialPort.Name = "lblSerialPort";
            this.lblSerialPort.Size = new System.Drawing.Size(57, 13);
            this.lblSerialPort.TabIndex = 2;
            this.lblSerialPort.Text = "Serial port:";
            // 
            // txtSerialLog
            // 
            this.txtSerialLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtSerialLog.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSerialLog.Location = new System.Drawing.Point(0, 458);
            this.txtSerialLog.Multiline = true;
            this.txtSerialLog.Name = "txtSerialLog";
            this.txtSerialLog.ReadOnly = true;
            this.txtSerialLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSerialLog.Size = new System.Drawing.Size(742, 185);
            this.txtSerialLog.TabIndex = 4;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(3, 50);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(139, 13);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Status: NOT_CONNECTED";
            // 
            // picPanel
            // 
            this.picPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picPanel.BackColor = System.Drawing.SystemColors.Window;
            this.picPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picPanel.Location = new System.Drawing.Point(0, 79);
            this.picPanel.Name = "picPanel";
            this.picPanel.Size = new System.Drawing.Size(742, 373);
            this.picPanel.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.checkRemoveBorder);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.lblMagnify);
            this.panel2.Controls.Add(this.comboMagnify);
            this.panel2.Controls.Add(this.lblColorPalette);
            this.panel2.Controls.Add(this.comboColorPalette);
            this.panel2.Controls.Add(this.lblSerialPort);
            this.panel2.Controls.Add(this.comboSerialPort);
            this.panel2.Controls.Add(this.lblStatus);
            this.panel2.Controls.Add(this.btnSerialConnect);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(742, 73);
            this.panel2.TabIndex = 8;
            // 
            // checkRemoveBorder
            // 
            this.checkRemoveBorder.AutoSize = true;
            this.checkRemoveBorder.Location = new System.Drawing.Point(361, 52);
            this.checkRemoveBorder.Name = "checkRemoveBorder";
            this.checkRemoveBorder.Size = new System.Drawing.Size(139, 17);
            this.checkRemoveBorder.TabIndex = 13;
            this.checkRemoveBorder.Text = "Remove Camera Border";
            this.checkRemoveBorder.UseVisualStyleBackColor = true;
            this.checkRemoveBorder.CheckedChanged += new System.EventHandler(this.checkRemoveBorder_CheckedChanged);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(655, 40);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Save...";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblMagnify
            // 
            this.lblMagnify.AutoSize = true;
            this.lblMagnify.Location = new System.Drawing.Point(358, 9);
            this.lblMagnify.Name = "lblMagnify";
            this.lblMagnify.Size = new System.Drawing.Size(47, 13);
            this.lblMagnify.TabIndex = 11;
            this.lblMagnify.Text = "Magnify:";
            // 
            // comboMagnify
            // 
            this.comboMagnify.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboMagnify.FormattingEnabled = true;
            this.comboMagnify.Items.AddRange(new object[] {
            "1x",
            "2x",
            "4x"});
            this.comboMagnify.Location = new System.Drawing.Point(361, 25);
            this.comboMagnify.Name = "comboMagnify";
            this.comboMagnify.Size = new System.Drawing.Size(121, 21);
            this.comboMagnify.TabIndex = 10;
            this.comboMagnify.SelectedIndexChanged += new System.EventHandler(this.comboMagnify_SelectedIndexChanged);
            // 
            // lblColorPalette
            // 
            this.lblColorPalette.AutoSize = true;
            this.lblColorPalette.Location = new System.Drawing.Point(231, 9);
            this.lblColorPalette.Name = "lblColorPalette";
            this.lblColorPalette.Size = new System.Drawing.Size(70, 13);
            this.lblColorPalette.TabIndex = 8;
            this.lblColorPalette.Text = "Color Palette:";
            // 
            // comboColorPalette
            // 
            this.comboColorPalette.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboColorPalette.FormattingEnabled = true;
            this.comboColorPalette.Location = new System.Drawing.Point(234, 25);
            this.comboColorPalette.Name = "comboColorPalette";
            this.comboColorPalette.Size = new System.Drawing.Size(121, 21);
            this.comboColorPalette.TabIndex = 7;
            this.comboColorPalette.SelectedIndexChanged += new System.EventHandler(this.comboColorPalette_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 643);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.picPanel);
            this.Controls.Add(this.txtSerialLog);
            this.Name = "MainForm";
            this.Text = "Game Boy Printer Emulator - Client";
            this.Load += new System.EventHandler(this.onFormOpening);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboSerialPort;
        private System.Windows.Forms.Button btnSerialConnect;
        private System.Windows.Forms.Label lblSerialPort;
        private System.Windows.Forms.TextBox txtSerialLog;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel picPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblColorPalette;
        private System.Windows.Forms.ComboBox comboColorPalette;
        private System.Windows.Forms.Label lblMagnify;
        private System.Windows.Forms.ComboBox comboMagnify;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox checkRemoveBorder;
    }
}

