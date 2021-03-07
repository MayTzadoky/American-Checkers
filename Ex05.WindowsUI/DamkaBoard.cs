using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using GameBusinessLogic;

namespace Ex05.WindowsUI
{
    public partial class DamkaBoard : Form
    {
        private const char k_ButtonIndexDelimiterIdentifier = '_';
        private readonly GameSettingsWindow r_GameSettingsWindow;
        private DamkaGameLogic m_DamkaGameLogic;
        private Button[,] m_ButtonMatrix;
        private string[,] m_CachedMatrixAsStrings;

        public DamkaBoard()
        {
            this.Hide();
            this.InitializeComponent();
            this.r_GameSettingsWindow = new GameSettingsWindow();
            this.r_GameSettingsWindow.StartGameEventHandler += this.StartGame;
            this.r_GameSettingsWindow.FormClosing += this.Close;
            Application.Run(this.r_GameSettingsWindow);
        }

        public void Close(object i_Sender, EventArgs i_Event)
        {
            Application.Exit();
        }

        public void StartGame(UserPreferences i_UserPreferences)
        {
            this.Show();
            this.m_DamkaGameLogic = new DamkaGameLogic(i_UserPreferences);
            this.m_DamkaGameLogic.BoardChangedEventHandler += this.updateBoard;
            this.m_DamkaGameLogic.GamePointsChangedEventHandler += this.UpdateGameNameAndPoints;
            this.m_DamkaGameLogic.GameOverEventHandler += this.gameOver;
            this.UpdateGameNameAndPoints();
            this.initBoardOnStart();
        }

        private void initBoardOnStart()
        {
            this.UpdateGameNameAndPoints();
            this.drawBoard();
            this.updateBoard();
        }

        public void UpdateGameNameAndPoints()
        {
            string mainPlayerPoints = this.m_DamkaGameLogic.MainPlayerPoints > 0 ? this.m_DamkaGameLogic.MainPlayerPoints.ToString() : "0";
            string secondPlayerPoints = this.m_DamkaGameLogic.SecondPlayerPoints > 0 ? this.m_DamkaGameLogic.SecondPlayerPoints.ToString() : "0";

            this.labelPlayer1.Text = string.Format("{0}: {1}", this.m_DamkaGameLogic.UserPreferences.MainParticipantUserName, mainPlayerPoints);
            this.labelPlayer2.Text = string.Format("{0}: {1}", this.m_DamkaGameLogic.UserPreferences.SecondParticipantUserName, secondPlayerPoints);
        }

        private void drawBoard()
        {
            int count = this.m_DamkaGameLogic.GameBoardSize;
            this.m_ButtonMatrix = new Button[count, count];
            int height = this.panelBoard.Height / 6;
            int width = this.panelBoard.Width / 6;

            this.panelBoard.Height = count * height;
            this.panelBoard.Width = count * width;
            this.Height = this.panelBoard.Height + 150;
            this.Width = this.panelBoard.Width + 100;
            this.Size = new System.Drawing.Size(this.Width, this.Height);
            bool fillOdds = true;

            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    Button button = new Button();
                    button.Text = string.Empty;
                    button.Name = string.Format("{0}{1}{2}", i, k_ButtonIndexDelimiterIdentifier, j);
                    button.Height = height;
                    button.Width = width;
                    button.FlatStyle = FlatStyle.Flat;
                    button.Padding = new Padding(0);
                    button.Margin = new Padding(0);
                    button.BackColor = fillOdds ? GameSettingsGlobal.sr_DefaultColorBoardBackroundDark : GameSettingsGlobal.sr_DefaultColorBoardBackroundLight;
                    button.Enabled = button.BackColor == GameSettingsGlobal.sr_DefaultColorBoardBackroundLight;
                    button.Click += this.soldierButton_SoldierClicked;
                    this.m_ButtonMatrix[i, j] = button;
                    this.panelBoard.Controls.Add(button);
                    fillOdds = !fillOdds;
                }

                fillOdds = !fillOdds;
            }
        }

        private void updateBoard()
        {
            this.m_CachedMatrixAsStrings = this.m_DamkaGameLogic.GetMatrix();
            for (int i = 0; i < this.m_CachedMatrixAsStrings.GetLength(0); i++)
            {
                for (int j = 0; j < this.m_CachedMatrixAsStrings.GetLength(0); j++)
                {
                    this.m_ButtonMatrix[i, j].Text = this.m_CachedMatrixAsStrings[i, j];
                    if (this.m_CachedMatrixAsStrings[i, j] == string.Empty ||
                        this.m_ButtonMatrix[i, j].BackColor == GameSettingsGlobal.sr_DefaultColorBoardBackroundDark ||
                        !this.m_DamkaGameLogic.IsCurrentParticipatePlayerByString(this.m_CachedMatrixAsStrings[i, j]))
                    {
                        this.m_ButtonMatrix[i, j].Enabled = false;
                    }
                    else
                    {
                        this.m_ButtonMatrix[i, j].Enabled = true;
                    }
                }
            }
        }

        private bool checkBackColorButton(Button i_Btn)
        {
            return i_Btn.BackColor != GameSettingsGlobal.sr_DefaultColorBoardBackroundDark && i_Btn.Text == string.Empty;
        }

        private void enabledButton(Button i_Btn)
        {
            i_Btn.Enabled = true;
        }

        private void soldierButton_SoldierClicked(object i_Sender, EventArgs i_Event)
        {
            int i, j;
            Button selectedButton = (Button)i_Sender;
            List<Button> buttonList = this.m_ButtonMatrix.Cast<Button>().ToList();
            Button activeButton = null;

            foreach(Button btn in buttonList)
            {
                if(btn.BackColor == GameSettingsGlobal.sr_DefaultColorSelectedSoldier)
                {
                    activeButton = btn;
                    break;
                }
            }

            this.parseButtonNameToIndexes(selectedButton.Name, out i, out j);

            if (activeButton == null)
            {
                if (this.m_CachedMatrixAsStrings[i, j] != string.Empty && 
                    this.m_DamkaGameLogic.IsCurrentParticipatePlayerByString(selectedButton.Text))
                {
                    selectedButton.BackColor = GameSettingsGlobal.sr_DefaultColorSelectedSoldier;
                    foreach(Button button in buttonList)
                    {
                        if(this.checkBackColorButton(button))
                        {
                             this.enabledButton(button);
                        }
                    }
                }
            }
            else
            {
                this.parseButtonNameToIndexes(activeButton.Name, out int soldierI, out int soldierJ);
                activeButton.BackColor = GameSettingsGlobal.sr_DefaultColorBoardBackroundLight;
                if (activeButton != selectedButton)
                {
                    if (this.m_CachedMatrixAsStrings[i, j] != string.Empty && this.m_DamkaGameLogic.IsCurrentParticipatePlayerByString(this.m_CachedMatrixAsStrings[i, j]))
                    {
                        ((Button)i_Sender).BackColor = GameSettingsGlobal.sr_DefaultColorSelectedSoldier;
                    }
                    else
                    {
                        eError error = this.m_DamkaGameLogic.MakeMove(new GameBoardPosition(soldierJ, soldierI), new GameBoardPosition(j, i));
                        if (error != eError.ValidMoveAndInput)
                        {
                            string moveError = "Invalid move";
                            GameSettingsGlobal.sr_ErrorToStringDict.TryGetValue(error, out moveError);
                            MessageBox.Show(moveError);
                        }
                    }
                }
            }
        }

        private void gameOver(eWinningStatus i_WinningStatus)
        {
            this.updateBoard();
            GameSettingsGlobal.sr_WinToStringDict.TryGetValue(i_WinningStatus, out string winBanner);
            DialogResult dialogResult = MessageBox.Show(string.Format("{0}{1}Another Round?", winBanner, Environment.NewLine), "Damka", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                this.newGame();
            }
            else
            {
                this.Close();
            }
        }

        private void newGame()
        {
            this.m_DamkaGameLogic.StartAnotherRound();
        }

        private void parseButtonNameToIndexes(string i_ButtonName, out int i, out int j)
        {
            string[] splitedNameDestCell = i_ButtonName.Split(k_ButtonIndexDelimiterIdentifier);
            i = int.Parse(splitedNameDestCell[0]);
            j = int.Parse(splitedNameDestCell[1]);
        }
    }
}
