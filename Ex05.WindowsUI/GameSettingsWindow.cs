using System;
using System.Linq;
using System.Windows.Forms;
using GameBusinessLogic;

namespace Ex05.WindowsUI
{
    public partial class GameSettingsWindow : Form
    {
        public event DamkaGameLogic.StartGameEventHandler StartGameEventHandler;

        public GameSettingsWindow()
        {
            this.InitializeComponent();
        }

        private void checkBoxPlayer2_CheckedChanged(object i_Sender, EventArgs i_Event)
        {
            this.textBoxPlayer2.Enabled = this.checkBoxPlayer2.Checked;
            this.textBoxPlayer2.Text = this.textBoxPlayer2.Enabled ? string.Empty : GameSettingsGlobal.sr_DefaultNamePlayer2;
        }

        private void button_startGame_Click(object i_Sender, EventArgs i_Event)
        {
            RadioButton selectedRadioButton = null;

            foreach(object control in this.panelRbBoardSize.Controls)
            {
                RadioButton radioButton = control as RadioButton;

                if(radioButton != null)
                {
                    if(radioButton.Checked)
                    {
                        selectedRadioButton = radioButton;
                        break;
                    }
                }
            }

            if (selectedRadioButton != null)
            {
                int boardSize = 0;

                switch (selectedRadioButton.Name)
                {
                    case "rb_6x6":
                        boardSize = 6;
                        break;
                    case "rb_8x8":
                        boardSize = 8;
                        break;
                    case "rb_10x10":
                        boardSize = 10;
                        break;
                }

                string player1Name = this.textBoxPlayer1.Text;

                if (this.checkUserNameValidity(player1Name))
                {
                    if (this.checkBoxPlayer2.Checked)
                    {
                        string player2Name = this.textBoxPlayer2.Text;
                        if (this.checkUserNameValidity(player2Name))
                        {
                            UserPreferences userPreferences = new UserPreferences(player1Name, boardSize, true, player2Name);
                            this.StartGameEventHandler.Invoke(userPreferences);
                            this.Hide();
                        }
                    }
                    else
                    {
                        UserPreferences userPreferences = new UserPreferences(player1Name, boardSize, false, GameSettingsGlobal.sr_DefaultNamePlayer2);
                        this.StartGameEventHandler.Invoke(userPreferences);
                        this.Hide();
                    }
                }
            }
            else
            {
                MessageBox.Show("Board size was not selected!", "Error");
            }
        }

        private bool checkUserNameValidity(string i_UserName)
        {
            bool isValidName = i_UserName.Length <= 20 && i_UserName.Length > 0 && !i_UserName.Contains(" ");

            if (!isValidName)
            {
                if (i_UserName.Length > 20)
                {
                    MessageBox.Show("The name that entered is longer than maximum length", "Error");
                }
                else if(i_UserName.Length == 0)
                {
                    MessageBox.Show("Please fill in player name!", "Error");
                }
                else
                {
                    MessageBox.Show("The name you enter contains spaces", "Error");
                }
            }

            return isValidName;
        }
    }
}
