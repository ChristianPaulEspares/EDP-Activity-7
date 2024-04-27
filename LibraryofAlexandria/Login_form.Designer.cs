
namespace LibraryofAlexandria
{
    partial class Login_form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login_form));
            this.panel1 = new ReaLTaiizor.Controls.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.register_showPass = new System.Windows.Forms.CheckBox();
            this.password_box = new System.Windows.Forms.TextBox();
            this.user_box = new System.Windows.Forms.TextBox();
            this.new_userbutton = new ReaLTaiizor.Controls.AirButton();
            this.forgot_pass = new System.Windows.Forms.Label();
            this.log_in_button = new ReaLTaiizor.Controls.AirButton();
            this.foxLabel2 = new ReaLTaiizor.Controls.FoxLabel();
            this.foxLabel1 = new ReaLTaiizor.Controls.FoxLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.icon = new ReaLTaiizor.Controls.HopePictureBox();
            this.panel3 = new ReaLTaiizor.Controls.Panel();
            this.bigLabel2 = new ReaLTaiizor.Controls.BigLabel();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.bigLabel3 = new ReaLTaiizor.Controls.BigLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.nightControlBox1 = new ReaLTaiizor.Controls.NightControlBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.bigLabel1 = new ReaLTaiizor.Controls.BigLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icon)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Bisque;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.icon);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.EdgeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(41)))), ((int)(((byte)(50)))));
            this.panel1.Location = new System.Drawing.Point(0, 37);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(327, 607);
            this.panel1.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.panel1.TabIndex = 2;
            this.panel1.Text = "panel1";
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.register_showPass);
            this.groupBox1.Controls.Add(this.password_box);
            this.groupBox1.Controls.Add(this.user_box);
            this.groupBox1.Controls.Add(this.new_userbutton);
            this.groupBox1.Controls.Add(this.forgot_pass);
            this.groupBox1.Controls.Add(this.log_in_button);
            this.groupBox1.Controls.Add(this.foxLabel2);
            this.groupBox1.Controls.Add(this.foxLabel1);
            this.groupBox1.Location = new System.Drawing.Point(1, 313);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(328, 256);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "V";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // register_showPass
            // 
            this.register_showPass.AutoSize = true;
            this.register_showPass.Font = new System.Drawing.Font("Comic Sans MS", 9.75F);
            this.register_showPass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(88)))), ((int)(((byte)(100)))));
            this.register_showPass.Location = new System.Drawing.Point(157, 151);
            this.register_showPass.Margin = new System.Windows.Forms.Padding(4);
            this.register_showPass.Name = "register_showPass";
            this.register_showPass.Size = new System.Drawing.Size(156, 28);
            this.register_showPass.TabIndex = 23;
            this.register_showPass.Text = "Show Password";
            this.register_showPass.UseVisualStyleBackColor = true;
            this.register_showPass.CheckedChanged += new System.EventHandler(this.register_showPass_CheckedChanged);
            // 
            // password_box
            // 
            this.password_box.Font = new System.Drawing.Font("Comic Sans MS", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.password_box.Location = new System.Drawing.Point(6, 114);
            this.password_box.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.password_box.Name = "password_box";
            this.password_box.PasswordChar = '*';
            this.password_box.Size = new System.Drawing.Size(305, 31);
            this.password_box.TabIndex = 15;
            this.password_box.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // user_box
            // 
            this.user_box.Font = new System.Drawing.Font("Comic Sans MS", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.user_box.Location = new System.Drawing.Point(6, 50);
            this.user_box.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.user_box.Name = "user_box";
            this.user_box.Size = new System.Drawing.Size(302, 31);
            this.user_box.TabIndex = 12;
            this.user_box.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // new_userbutton
            // 
            this.new_userbutton.BackColor = System.Drawing.Color.Coral;
            this.new_userbutton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.new_userbutton.Customization = "7e3t//Ly8v/r6+v/5ubm/+vr6//f39//p6en/zw8PP8UFBT/gICA/w==";
            this.new_userbutton.Font = new System.Drawing.Font("Bauhaus 93", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.new_userbutton.Image = null;
            this.new_userbutton.Location = new System.Drawing.Point(8, 201);
            this.new_userbutton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.new_userbutton.Name = "new_userbutton";
            this.new_userbutton.NoRounding = false;
            this.new_userbutton.Size = new System.Drawing.Size(113, 33);
            this.new_userbutton.TabIndex = 14;
            this.new_userbutton.Text = "New User?";
            this.new_userbutton.Transparent = false;
            this.new_userbutton.Click += new System.EventHandler(this.airButton2_Click);
            // 
            // forgot_pass
            // 
            this.forgot_pass.AutoSize = true;
            this.forgot_pass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.forgot_pass.ForeColor = System.Drawing.Color.Blue;
            this.forgot_pass.Location = new System.Drawing.Point(192, 85);
            this.forgot_pass.Name = "forgot_pass";
            this.forgot_pass.Size = new System.Drawing.Size(121, 17);
            this.forgot_pass.TabIndex = 12;
            this.forgot_pass.Text = "Forgot password?";
            this.forgot_pass.UseMnemonic = false;
            this.forgot_pass.Click += new System.EventHandler(this.label3_Click);
            // 
            // log_in_button
            // 
            this.log_in_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.log_in_button.Customization = "7e3t//Ly8v/r6+v/5ubm/+vr6//f39//p6en/zw8PP8UFBT/gICA/w==";
            this.log_in_button.Font = new System.Drawing.Font("Bauhaus 93", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.log_in_button.Image = null;
            this.log_in_button.Location = new System.Drawing.Point(210, 201);
            this.log_in_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.log_in_button.Name = "log_in_button";
            this.log_in_button.NoRounding = false;
            this.log_in_button.Size = new System.Drawing.Size(100, 33);
            this.log_in_button.TabIndex = 13;
            this.log_in_button.Text = "Log in";
            this.log_in_button.Transparent = false;
            this.log_in_button.Click += new System.EventHandler(this.airButton1_Click);
            // 
            // foxLabel2
            // 
            this.foxLabel2.Font = new System.Drawing.Font("Comic Sans MS", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.foxLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(88)))), ((int)(((byte)(100)))));
            this.foxLabel2.Location = new System.Drawing.Point(7, 85);
            this.foxLabel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.foxLabel2.Name = "foxLabel2";
            this.foxLabel2.Size = new System.Drawing.Size(91, 25);
            this.foxLabel2.TabIndex = 11;
            this.foxLabel2.Text = "Password";
            this.foxLabel2.Click += new System.EventHandler(this.foxLabel2_Click);
            // 
            // foxLabel1
            // 
            this.foxLabel1.Font = new System.Drawing.Font("Comic Sans MS", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.foxLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(88)))), ((int)(((byte)(100)))));
            this.foxLabel1.Location = new System.Drawing.Point(8, 19);
            this.foxLabel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.foxLabel1.Name = "foxLabel1";
            this.foxLabel1.Size = new System.Drawing.Size(110, 25);
            this.foxLabel1.TabIndex = 8;
            this.foxLabel1.Text = "Username";
            this.foxLabel1.Click += new System.EventHandler(this.foxLabel1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.SandyBrown;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Bauhaus 93", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(115, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 36);
            this.label1.TabIndex = 5;
            this.label1.Text = "Log in";
            this.label1.Click += new System.EventHandler(this.label1_Click_2);
            // 
            // icon
            // 
            this.icon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(196)))), ((int)(((byte)(204)))));
            this.icon.Image = ((System.Drawing.Image)(resources.GetObject("icon.Image")));
            this.icon.Location = new System.Drawing.Point(102, 178);
            this.icon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.icon.Name = "icon";
            this.icon.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            this.icon.Size = new System.Drawing.Size(107, 112);
            this.icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.icon.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.icon.TabIndex = 7;
            this.icon.TabStop = false;
            this.icon.TextRenderingType = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.icon.Click += new System.EventHandler(this.hopePictureBox1_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel3.Controls.Add(this.bigLabel2);
            this.panel3.Controls.Add(this.pictureBox4);
            this.panel3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.panel3.EdgeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(41)))), ((int)(((byte)(50)))));
            this.panel3.Location = new System.Drawing.Point(1, -4);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(5);
            this.panel3.Size = new System.Drawing.Size(328, 130);
            this.panel3.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.panel3.TabIndex = 6;
            this.panel3.Text = "panel3";
            this.panel3.Click += new System.EventHandler(this.panel3_Click);
            // 
            // bigLabel2
            // 
            this.bigLabel2.AutoSize = true;
            this.bigLabel2.BackColor = System.Drawing.Color.Transparent;
            this.bigLabel2.Font = new System.Drawing.Font("Bauhaus 93", 25.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bigLabel2.ForeColor = System.Drawing.Color.Black;
            this.bigLabel2.Location = new System.Drawing.Point(52, 66);
            this.bigLabel2.Name = "bigLabel2";
            this.bigLabel2.Size = new System.Drawing.Size(217, 49);
            this.bigLabel2.TabIndex = 0;
            this.bigLabel2.Text = "Welcome!";
            this.bigLabel2.Click += new System.EventHandler(this.bigLabel2_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(-167, -71);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(655, 450);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 4;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Click += new System.EventHandler(this.pictureBox4_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.BackgroundImage")));
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(320, 37);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(808, 607);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 3;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // bigLabel3
            // 
            this.bigLabel3.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.bigLabel3.AutoSize = true;
            this.bigLabel3.BackColor = System.Drawing.Color.White;
            this.bigLabel3.Font = new System.Drawing.Font("Segoe UI", 25F);
            this.bigLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.bigLabel3.Location = new System.Drawing.Point(518, 440);
            this.bigLabel3.Name = "bigLabel3";
            this.bigLabel3.Size = new System.Drawing.Size(409, 57);
            this.bigLabel3.TabIndex = 4;
            this.bigLabel3.Text = "library of Alexandrea";
            this.bigLabel3.Click += new System.EventHandler(this.bigLabel3_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::LibraryofAlexandria.Properties.Resources.library_icon;
            this.pictureBox1.Location = new System.Drawing.Point(541, 71);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(362, 362);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click_1);
            // 
            // nightControlBox1
            // 
            this.nightControlBox1.BackColor = System.Drawing.Color.Transparent;
            this.nightControlBox1.CloseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.nightControlBox1.CloseHoverForeColor = System.Drawing.Color.White;
            this.nightControlBox1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.nightControlBox1.DefaultLocation = true;
            this.nightControlBox1.DisableMaximizeColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.nightControlBox1.DisableMinimizeColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.nightControlBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.nightControlBox1.EnableCloseColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.nightControlBox1.EnableMaximizeButton = true;
            this.nightControlBox1.EnableMaximizeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.nightControlBox1.EnableMinimizeButton = true;
            this.nightControlBox1.EnableMinimizeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.nightControlBox1.Location = new System.Drawing.Point(989, 0);
            this.nightControlBox1.MaximizeHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.nightControlBox1.MaximizeHoverForeColor = System.Drawing.Color.White;
            this.nightControlBox1.MinimizeHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.nightControlBox1.MinimizeHoverForeColor = System.Drawing.Color.White;
            this.nightControlBox1.Name = "nightControlBox1";
            this.nightControlBox1.Size = new System.Drawing.Size(139, 31);
            this.nightControlBox1.TabIndex = 1;
            this.nightControlBox1.Click += new System.EventHandler(this.nightControlBox1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox2.Image = global::LibraryofAlexandria.Properties.Resources.library_icon;
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(37, 37);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // bigLabel1
            // 
            this.bigLabel1.AutoSize = true;
            this.bigLabel1.BackColor = System.Drawing.Color.Transparent;
            this.bigLabel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.bigLabel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.bigLabel1.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.bigLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.bigLabel1.Location = new System.Drawing.Point(37, 0);
            this.bigLabel1.Name = "bigLabel1";
            this.bigLabel1.Size = new System.Drawing.Size(235, 32);
            this.bigLabel1.TabIndex = 2;
            this.bigLabel1.Text = "library of Alexandrea";
            this.bigLabel1.Click += new System.EventHandler(this.bigLabel1_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.bigLabel1);
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Controls.Add(this.nightControlBox1);
            this.panel2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1128, 37);
            this.panel2.TabIndex = 1;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            this.panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouse_down);
            this.panel2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mouse_move);
            // 
            // Login_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1128, 644);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.bigLabel3);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximumSize = new System.Drawing.Size(1128, 644);
            this.MinimumSize = new System.Drawing.Size(1128, 644);
            this.Name = "Login_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login_form";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Load += new System.EventHandler(this.Login_form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icon)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ReaLTaiizor.Controls.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label1;
        private ReaLTaiizor.Controls.HopePictureBox icon;
        private ReaLTaiizor.Controls.FoxLabel foxLabel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private ReaLTaiizor.Controls.FoxLabel foxLabel2;
        private ReaLTaiizor.Controls.AirButton log_in_button;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label forgot_pass;
        private ReaLTaiizor.Controls.AirButton new_userbutton;
        private ReaLTaiizor.Controls.Panel panel3;
        private ReaLTaiizor.Controls.BigLabel bigLabel2;
        private System.Windows.Forms.TextBox password_box;
        private System.Windows.Forms.TextBox user_box;
        private ReaLTaiizor.Controls.BigLabel bigLabel3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private ReaLTaiizor.Controls.NightControlBox nightControlBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private ReaLTaiizor.Controls.BigLabel bigLabel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox register_showPass;
    }
}