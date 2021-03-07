namespace Ex05.WindowsUI
{
    public partial class GameSettingsWindow
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
            this.rb_6x6 = new System.Windows.Forms.RadioButton();
            this.boardSizeLable = new System.Windows.Forms.Label();
            this.rb_8x8 = new System.Windows.Forms.RadioButton();
            this.rb_10x10 = new System.Windows.Forms.RadioButton();
            this.lablePlayers = new System.Windows.Forms.Label();
            this.lablePlayer1 = new System.Windows.Forms.Label();
            this.labelPlayer2 = new System.Windows.Forms.Label();
            this.checkBoxPlayer2 = new System.Windows.Forms.CheckBox();
            this.textBoxPlayer1 = new System.Windows.Forms.TextBox();
            this.textBoxPlayer2 = new System.Windows.Forms.TextBox();
            this.buttonStartGame = new System.Windows.Forms.Button();
            this.panelRbBoardSize = new System.Windows.Forms.Panel();
            this.panelRbBoardSize.SuspendLayout();
            this.SuspendLayout();
            // 
            // rb_6x6
            // 
            this.rb_6x6.AutoSize = true;
            this.rb_6x6.Location = new System.Drawing.Point(34, 7);
            this.rb_6x6.Margin = new System.Windows.Forms.Padding(2);
            this.rb_6x6.Name = "rb_6x6";
            this.rb_6x6.Size = new System.Drawing.Size(62, 21);
            this.rb_6x6.TabIndex = 0;
            this.rb_6x6.Text = "6 X 6";
            this.rb_6x6.UseVisualStyleBackColor = true;
            // 
            // boardSizeLable
            // 
            this.boardSizeLable.AutoSize = true;
            this.boardSizeLable.Location = new System.Drawing.Point(18, 35);
            this.boardSizeLable.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.boardSizeLable.Name = "boardSizeLable";
            this.boardSizeLable.Size = new System.Drawing.Size(81, 17);
            this.boardSizeLable.TabIndex = 8;
            this.boardSizeLable.Text = "Board Size:";
            // 
            // rb_8x8
            // 
            this.rb_8x8.AutoSize = true;
            this.rb_8x8.Location = new System.Drawing.Point(134, 7);
            this.rb_8x8.Margin = new System.Windows.Forms.Padding(2);
            this.rb_8x8.Name = "rb_8x8";
            this.rb_8x8.Size = new System.Drawing.Size(62, 21);
            this.rb_8x8.TabIndex = 1;
            this.rb_8x8.Text = "8 X 8";
            this.rb_8x8.UseVisualStyleBackColor = true;
            // 
            // rb_10x10
            // 
            this.rb_10x10.AutoSize = true;
            this.rb_10x10.Location = new System.Drawing.Point(224, 7);
            this.rb_10x10.Margin = new System.Windows.Forms.Padding(2);
            this.rb_10x10.Name = "rb_10x10";
            this.rb_10x10.Size = new System.Drawing.Size(78, 21);
            this.rb_10x10.TabIndex = 2;
            this.rb_10x10.Text = "10 X 10";
            this.rb_10x10.UseVisualStyleBackColor = true;
            // 
            // lablePlayers
            // 
            this.lablePlayers.AutoSize = true;
            this.lablePlayers.Location = new System.Drawing.Point(18, 106);
            this.lablePlayers.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lablePlayers.Name = "lablePlayers";
            this.lablePlayers.Size = new System.Drawing.Size(59, 17);
            this.lablePlayers.TabIndex = 1;
            this.lablePlayers.Text = "Players:";
            // 
            // lablePlayer1
            // 
            this.lablePlayer1.AutoSize = true;
            this.lablePlayer1.Location = new System.Drawing.Point(44, 143);
            this.lablePlayer1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lablePlayer1.Name = "lablePlayer1";
            this.lablePlayer1.Size = new System.Drawing.Size(64, 17);
            this.lablePlayer1.TabIndex = 2;
            this.lablePlayer1.Text = "Player 1:";
            // 
            // labelPlayer2
            // 
            this.labelPlayer2.AutoSize = true;
            this.labelPlayer2.Location = new System.Drawing.Point(44, 182);
            this.labelPlayer2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelPlayer2.Name = "labelPlayer2";
            this.labelPlayer2.Size = new System.Drawing.Size(64, 17);
            this.labelPlayer2.TabIndex = 5;
            this.labelPlayer2.Text = "Player 2:";
            // 
            // checkBoxPlayer2
            // 
            this.checkBoxPlayer2.AutoSize = true;
            this.checkBoxPlayer2.Location = new System.Drawing.Point(22, 182);
            this.checkBoxPlayer2.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxPlayer2.Name = "checkBoxPlayer2";
            this.checkBoxPlayer2.Size = new System.Drawing.Size(18, 17);
            this.checkBoxPlayer2.TabIndex = 4;
            this.checkBoxPlayer2.UseVisualStyleBackColor = true;
            this.checkBoxPlayer2.CheckedChanged += new System.EventHandler(this.checkBoxPlayer2_CheckedChanged);
            // 
            // textBoxPlayer1
            // 
            this.textBoxPlayer1.Location = new System.Drawing.Point(125, 143);
            this.textBoxPlayer1.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxPlayer1.Name = "textBoxPlayer1";
            this.textBoxPlayer1.Size = new System.Drawing.Size(212, 22);
            this.textBoxPlayer1.TabIndex = 3;
            // 
            // textBoxPlayer2
            // 
            this.textBoxPlayer2.Enabled = false;
            this.textBoxPlayer2.Location = new System.Drawing.Point(125, 179);
            this.textBoxPlayer2.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxPlayer2.Name = "textBoxPlayer2";
            this.textBoxPlayer2.Size = new System.Drawing.Size(212, 22);
            this.textBoxPlayer2.TabIndex = 6;
            this.textBoxPlayer2.Text = "[Computer]";
            // 
            // buttonStartGame
            // 
            this.buttonStartGame.Location = new System.Drawing.Point(221, 222);
            this.buttonStartGame.Margin = new System.Windows.Forms.Padding(2);
            this.buttonStartGame.Name = "buttonStartGame";
            this.buttonStartGame.Size = new System.Drawing.Size(116, 27);
            this.buttonStartGame.TabIndex = 7;
            this.buttonStartGame.Text = "Done";
            this.buttonStartGame.UseVisualStyleBackColor = true;
            this.buttonStartGame.Click += new System.EventHandler(this.button_startGame_Click);
            // 
            // panelRbBoardSize
            // 
            this.panelRbBoardSize.Controls.Add(this.rb_10x10);
            this.panelRbBoardSize.Controls.Add(this.rb_6x6);
            this.panelRbBoardSize.Controls.Add(this.rb_8x8);
            this.panelRbBoardSize.Location = new System.Drawing.Point(22, 54);
            this.panelRbBoardSize.Margin = new System.Windows.Forms.Padding(2);
            this.panelRbBoardSize.Name = "panelRbBoardSize";
            this.panelRbBoardSize.Size = new System.Drawing.Size(315, 45);
            this.panelRbBoardSize.TabIndex = 0;
            // 
            // GameSettingsWindow
            // 
            this.AcceptButton = this.buttonStartGame;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 260);
            this.Controls.Add(this.panelRbBoardSize);
            this.Controls.Add(this.buttonStartGame);
            this.Controls.Add(this.textBoxPlayer2);
            this.Controls.Add(this.textBoxPlayer1);
            this.Controls.Add(this.checkBoxPlayer2);
            this.Controls.Add(this.labelPlayer2);
            this.Controls.Add(this.lablePlayer1);
            this.Controls.Add(this.lablePlayers);
            this.Controls.Add(this.boardSizeLable);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(416, 307);
            this.Name = "GameSettingsWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Setting";
            this.panelRbBoardSize.ResumeLayout(false);
            this.panelRbBoardSize.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rb_6x6;
        private System.Windows.Forms.Label boardSizeLable;
        private System.Windows.Forms.RadioButton rb_8x8;
        private System.Windows.Forms.RadioButton rb_10x10;
        private System.Windows.Forms.Label lablePlayers;
        private System.Windows.Forms.Label lablePlayer1;
        private System.Windows.Forms.Label labelPlayer2;
        private System.Windows.Forms.CheckBox checkBoxPlayer2;
        private System.Windows.Forms.TextBox textBoxPlayer1;
        private System.Windows.Forms.TextBox textBoxPlayer2;
        private System.Windows.Forms.Button buttonStartGame;
        private System.Windows.Forms.Panel panelRbBoardSize;
    }
}
