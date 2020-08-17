namespace FacebookUI
{
    public partial class FormBirthdayFeature
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBirthdayFeature));
            this.m_PictureBoxFriend = new System.Windows.Forms.PictureBox();
            this.m_ButtonNext = new System.Windows.Forms.Button();
            this.m_GroupBoxQuizz = new System.Windows.Forms.GroupBox();
            this.m_RadioButtonQ4 = new System.Windows.Forms.RadioButton();
            this.m_RadioButtonQ3 = new System.Windows.Forms.RadioButton();
            this.m_RadioButtonQ2 = new System.Windows.Forms.RadioButton();
            this.m_RadioButtonQ1 = new System.Windows.Forms.RadioButton();
            this.m_ButtonMinimize = new System.Windows.Forms.Button();
            this.m_ButtonExit = new System.Windows.Forms.Button();
            this.m_LabelWelcomeFriendBirthday = new System.Windows.Forms.Label();
            this.m_LabelNoFriends = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.m_PictureBoxFriend)).BeginInit();
            this.m_GroupBoxQuizz.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_PictureBoxFriend
            // 
            this.m_PictureBoxFriend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(105)))), ((int)(((byte)(152)))));
            this.m_PictureBoxFriend.Location = new System.Drawing.Point(280, 25);
            this.m_PictureBoxFriend.Name = "m_PictureBoxFriend";
            this.m_PictureBoxFriend.Size = new System.Drawing.Size(134, 126);
            this.m_PictureBoxFriend.TabIndex = 0;
            this.m_PictureBoxFriend.TabStop = false;
            // 
            // m_ButtonNext
            // 
            this.m_ButtonNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(105)))), ((int)(((byte)(152)))));
            this.m_ButtonNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_ButtonNext.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.m_ButtonNext.Location = new System.Drawing.Point(45, 184);
            this.m_ButtonNext.Name = "m_ButtonNext";
            this.m_ButtonNext.Size = new System.Drawing.Size(134, 37);
            this.m_ButtonNext.TabIndex = 7;
            this.m_ButtonNext.Text = "Next";
            this.m_ButtonNext.UseVisualStyleBackColor = false;
            this.m_ButtonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // m_GroupBoxQuizz
            // 
            this.m_GroupBoxQuizz.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(105)))), ((int)(((byte)(152)))));
            this.m_GroupBoxQuizz.Controls.Add(this.m_RadioButtonQ4);
            this.m_GroupBoxQuizz.Controls.Add(this.m_RadioButtonQ3);
            this.m_GroupBoxQuizz.Controls.Add(this.m_RadioButtonQ2);
            this.m_GroupBoxQuizz.Controls.Add(this.m_ButtonNext);
            this.m_GroupBoxQuizz.Controls.Add(this.m_RadioButtonQ1);
            this.m_GroupBoxQuizz.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_GroupBoxQuizz.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_GroupBoxQuizz.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.m_GroupBoxQuizz.Location = new System.Drawing.Point(175, 224);
            this.m_GroupBoxQuizz.Name = "m_GroupBoxQuizz";
            this.m_GroupBoxQuizz.Size = new System.Drawing.Size(340, 243);
            this.m_GroupBoxQuizz.TabIndex = 13;
            this.m_GroupBoxQuizz.TabStop = false;
            this.m_GroupBoxQuizz.Text = "Changing Question";
            // 
            // m_RadioButtonQ4
            // 
            this.m_RadioButtonQ4.AutoSize = true;
            this.m_RadioButtonQ4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(105)))), ((int)(((byte)(152)))));
            this.m_RadioButtonQ4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.m_RadioButtonQ4.Location = new System.Drawing.Point(11, 132);
            this.m_RadioButtonQ4.Name = "m_RadioButtonQ4";
            this.m_RadioButtonQ4.Size = new System.Drawing.Size(197, 27);
            this.m_RadioButtonQ4.TabIndex = 3;
            this.m_RadioButtonQ4.TabStop = true;
            this.m_RadioButtonQ4.Text = "m_RadioButtonQ4";
            this.m_RadioButtonQ4.UseVisualStyleBackColor = false;
            // 
            // m_RadioButtonQ3
            // 
            this.m_RadioButtonQ3.AutoSize = true;
            this.m_RadioButtonQ3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(105)))), ((int)(((byte)(152)))));
            this.m_RadioButtonQ3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.m_RadioButtonQ3.Location = new System.Drawing.Point(11, 100);
            this.m_RadioButtonQ3.Name = "m_RadioButtonQ3";
            this.m_RadioButtonQ3.Size = new System.Drawing.Size(197, 27);
            this.m_RadioButtonQ3.TabIndex = 2;
            this.m_RadioButtonQ3.TabStop = true;
            this.m_RadioButtonQ3.Text = "m_RadioButtonQ3";
            this.m_RadioButtonQ3.UseVisualStyleBackColor = false;
            // 
            // m_RadioButtonQ2
            // 
            this.m_RadioButtonQ2.AutoSize = true;
            this.m_RadioButtonQ2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(105)))), ((int)(((byte)(152)))));
            this.m_RadioButtonQ2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.m_RadioButtonQ2.Location = new System.Drawing.Point(11, 68);
            this.m_RadioButtonQ2.Name = "m_RadioButtonQ2";
            this.m_RadioButtonQ2.Size = new System.Drawing.Size(197, 27);
            this.m_RadioButtonQ2.TabIndex = 1;
            this.m_RadioButtonQ2.TabStop = true;
            this.m_RadioButtonQ2.Text = "m_RadioButtonQ2";
            this.m_RadioButtonQ2.UseVisualStyleBackColor = false;
            // 
            // m_RadioButtonQ1
            // 
            this.m_RadioButtonQ1.AutoSize = true;
            this.m_RadioButtonQ1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(105)))), ((int)(((byte)(152)))));
            this.m_RadioButtonQ1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.m_RadioButtonQ1.Location = new System.Drawing.Point(11, 36);
            this.m_RadioButtonQ1.Name = "m_RadioButtonQ1";
            this.m_RadioButtonQ1.Size = new System.Drawing.Size(197, 27);
            this.m_RadioButtonQ1.TabIndex = 0;
            this.m_RadioButtonQ1.TabStop = true;
            this.m_RadioButtonQ1.Text = "m_RadioButtonQ1";
            this.m_RadioButtonQ1.UseVisualStyleBackColor = false;
            // 
            // m_ButtonMinimize
            // 
            this.m_ButtonMinimize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(105)))), ((int)(((byte)(152)))));
            this.m_ButtonMinimize.FlatAppearance.BorderSize = 0;
            this.m_ButtonMinimize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(157)))), ((int)(((byte)(195)))));
            this.m_ButtonMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_ButtonMinimize.Image = ((System.Drawing.Image)(resources.GetObject("m_ButtonMinimize.Image")));
            this.m_ButtonMinimize.Location = new System.Drawing.Point(654, 12);
            this.m_ButtonMinimize.Name = "m_ButtonMinimize";
            this.m_ButtonMinimize.Size = new System.Drawing.Size(20, 20);
            this.m_ButtonMinimize.TabIndex = 15;
            this.m_ButtonMinimize.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.m_ButtonMinimize.UseVisualStyleBackColor = false;
            this.m_ButtonMinimize.Click += new System.EventHandler(this.buttonMinimize_Click);
            // 
            // m_ButtonExit
            // 
            this.m_ButtonExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(105)))), ((int)(((byte)(152)))));
            this.m_ButtonExit.FlatAppearance.BorderSize = 0;
            this.m_ButtonExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(157)))), ((int)(((byte)(195)))));
            this.m_ButtonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_ButtonExit.Image = ((System.Drawing.Image)(resources.GetObject("m_ButtonExit.Image")));
            this.m_ButtonExit.Location = new System.Drawing.Point(677, 12);
            this.m_ButtonExit.Name = "m_ButtonExit";
            this.m_ButtonExit.Size = new System.Drawing.Size(20, 20);
            this.m_ButtonExit.TabIndex = 14;
            this.m_ButtonExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.m_ButtonExit.UseVisualStyleBackColor = false;
            this.m_ButtonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // m_LabelWelcomeFriendBirthday
            // 
            this.m_LabelWelcomeFriendBirthday.AutoSize = true;
            this.m_LabelWelcomeFriendBirthday.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(105)))), ((int)(((byte)(152)))));
            this.m_LabelWelcomeFriendBirthday.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_LabelWelcomeFriendBirthday.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.m_LabelWelcomeFriendBirthday.Location = new System.Drawing.Point(198, 166);
            this.m_LabelWelcomeFriendBirthday.Name = "m_LabelWelcomeFriendBirthday";
            this.m_LabelWelcomeFriendBirthday.Size = new System.Drawing.Size(295, 46);
            this.m_LabelWelcomeFriendBirthday.TabIndex = 1;
            this.m_LabelWelcomeFriendBirthday.Text = "This Friend has Birthday Today! \r\n What do you know about him?";
            // 
            // m_LabelNoFriends
            // 
            this.m_LabelNoFriends.AutoSize = true;
            this.m_LabelNoFriends.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(105)))), ((int)(((byte)(152)))));
            this.m_LabelNoFriends.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_LabelNoFriends.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.m_LabelNoFriends.Location = new System.Drawing.Point(125, 198);
            this.m_LabelNoFriends.Name = "m_LabelNoFriends";
            this.m_LabelNoFriends.Size = new System.Drawing.Size(439, 23);
            this.m_LabelNoFriends.TabIndex = 16;
            this.m_LabelNoFriends.Text = "You Don\'t have any Friend with Birthday Today!";
            this.m_LabelNoFriends.Visible = false;
            // 
            // FormBirthdayFeature
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(105)))), ((int)(((byte)(152)))));
            this.ClientSize = new System.Drawing.Size(709, 484);
            this.Controls.Add(this.m_LabelNoFriends);
            this.Controls.Add(this.m_ButtonMinimize);
            this.Controls.Add(this.m_ButtonExit);
            this.Controls.Add(this.m_GroupBoxQuizz);
            this.Controls.Add(this.m_LabelWelcomeFriendBirthday);
            this.Controls.Add(this.m_PictureBoxFriend);
            this.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormBirthdayFeature";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormBirthdayFeature";
            ((System.ComponentModel.ISupportInitialize)(this.m_PictureBoxFriend)).EndInit();
            this.m_GroupBoxQuizz.ResumeLayout(false);
            this.m_GroupBoxQuizz.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox m_PictureBoxFriend;
        private System.Windows.Forms.Button m_ButtonNext;
        private System.Windows.Forms.GroupBox m_GroupBoxQuizz;
        private System.Windows.Forms.RadioButton m_RadioButtonQ4;
        private System.Windows.Forms.RadioButton m_RadioButtonQ3;
        private System.Windows.Forms.RadioButton m_RadioButtonQ2;
        private System.Windows.Forms.RadioButton m_RadioButtonQ1;
        private System.Windows.Forms.Button m_ButtonMinimize;
        private System.Windows.Forms.Button m_ButtonExit;
        private System.Windows.Forms.Label m_LabelWelcomeFriendBirthday;
        private System.Windows.Forms.Label m_LabelNoFriends;
    }
}