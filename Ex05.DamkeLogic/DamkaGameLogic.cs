using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GameBusinessLogic
{
    public class DamkaGameLogic
    {
        private Player[,] m_GameBoardMatrix;
        private List<Player> m_PlayersOfXAndK;
        private List<Player> m_PlayersOfOAndU;
        private ComputerPlayer m_ComputerPlayers;
        private int m_BoarsSize;
        private bool m_IsMainParticipantTurn;
        private UserPreferences m_UserPreferences;
        private int m_NumberOfO;
        private int m_NumberOfX;
        private int m_NumberOfU;
        private int m_NumberOfK;
        private int m_VictoryPointsOfMainParticipant;
        private int m_VictoryPointsOfSecondParticipant;
        private int m_MainParticipantPoints;
        private int m_SecondParticipantPoints;

        public delegate void GameIsOverEventHandler(eWinningStatus i_WinningStatus);

        public delegate void StartGameEventHandler(UserPreferences userPreferences);

        public delegate void NoArgsEventHandler();

        public event GameIsOverEventHandler GameOverEventHandler;

        public event NoArgsEventHandler BoardChangedEventHandler;

        public event NoArgsEventHandler GamePointsChangedEventHandler;

        public DamkaGameLogic(UserPreferences i_UserPreferences)
        {
            m_VictoryPointsOfMainParticipant = 0;
            m_VictoryPointsOfSecondParticipant = 0;
            m_NumberOfO = 0;
            m_NumberOfX = 0;
            m_NumberOfU = 0;
            m_NumberOfK = 0;
            m_IsMainParticipantTurn = true;
            m_UserPreferences = i_UserPreferences;
            GameBoardSize = m_UserPreferences.BoardSize;
            m_GameBoardMatrix = new Player[GameBoardSize, GameBoardSize];
            m_ComputerPlayers = new ComputerPlayer();
            m_PlayersOfOAndU = new List<Player>(GameBoardSize);
            m_PlayersOfXAndK = new List<Player>(GameBoardSize);
            m_MainParticipantPoints = 0;
            m_SecondParticipantPoints = 0;
            initializeGameBoard();
        }

        public UserPreferences UserPreferences
        {
            get
            {
                return m_UserPreferences;
            }
        }

        public Player[,] GameMatrix
        {
            get
            {
                return m_GameBoardMatrix;
            }
        }

        public int GameBoardSize
        {
            get
            {
                return m_BoarsSize;
            }

            set
            {
                m_BoarsSize = value;
            }
        }

        public bool IsMainParticipantTurn
        {
            get
            {
                return m_IsMainParticipantTurn;
            }

            set
            {
                m_IsMainParticipantTurn = value;
            }
        }

        public int NumberOfO
        {
            get
            {
                return m_NumberOfO;
            }
        }

        public int NumberOfX
        {
            get
            {
                return m_NumberOfX;
            }
        }

        public int NumberOfU
        {
            get
            {
                return m_NumberOfU;
            }
        }

        public int NumberOfK
        {
            get
            {
                return m_NumberOfK;
            }
        }

        public ComputerPlayer ComputerPlayers
        {
            get
            {
                return m_ComputerPlayers;
            }
        }

        public int VictoryPointsOfMainParticipant
        {
            get
            {
                return m_VictoryPointsOfMainParticipant;
            }

            set
            {
                m_VictoryPointsOfMainParticipant = value;
            }
        }

        public int VictoryPointsOfSecondParticipant
        {
            get
            {
                return m_VictoryPointsOfSecondParticipant;
            }

            set
            {
                m_VictoryPointsOfSecondParticipant = value;
            }
        }

        public int MainPlayerPoints
        {
            get
            {
                return m_MainParticipantPoints;
            }
        }

        public int SecondPlayerPoints
        {
            get
            {
                return m_SecondParticipantPoints;
            }
        }

        public void StartAnotherRound()
        {
            m_NumberOfO = 0;
            m_NumberOfX = 0;
            m_NumberOfU = 0;
            m_NumberOfK = 0;
            m_IsMainParticipantTurn = true;

            m_PlayersOfOAndU.Clear();
            m_PlayersOfXAndK.Clear();
            initializeGameBoard();
            BoardChangedEventHandler?.Invoke();
        }

        private void initializeGameBoard()
        {
            bool isEvenLine = false;
            Player player;

            for (int i = 0; i < m_BoarsSize; ++i)
            {
                for (int j = 0; j < m_BoarsSize; ++j)
                {
                    isEvenLine = i % 2 == 0;

                    if (i < (m_BoarsSize / 2) - 1)
                    {
                        if ((j % 2 == 1 && isEvenLine) || (j % 2 == 0 && !isEvenLine))
                        {
                            player = new Player(i, j, 'O');
                            m_GameBoardMatrix[i, j] = player;
                            m_NumberOfO++;
                            m_PlayersOfOAndU.Add(player);
                        }
                        else
                        {
                            m_GameBoardMatrix[i, j] = null;
                        }
                    }
                    else if (i >= (m_BoarsSize / 2) + 1)
                    {
                        if ((j % 2 == 1 && isEvenLine) || (j % 2 == 0 && !isEvenLine))
                        {
                            player = new Player(i, j, 'X');
                            m_GameBoardMatrix[i, j] = player;
                            m_NumberOfX++;
                            m_PlayersOfXAndK.Add(player);
                        }
                        else
                        {
                            m_GameBoardMatrix[i, j] = null;
                        }
                    }
                    else
                    {
                        m_GameBoardMatrix[i, j] = null;
                    }
                }
            }
        }

        private eError isValidMove(GameBoardPosition i_CurrentSoldierPosition, GameBoardPosition i_NextSoldierPositionMove)
        {
            eError errorMove = eError.ValidMoveAndInput;
            bool haveJump = false;
            Player playerToMove = null;
            bool isValidMove = false;

            if ((m_IsMainParticipantTurn && (isSoldierExistInCurrentPosition('X', i_CurrentSoldierPosition) || isSoldierExistInCurrentPosition('K', i_CurrentSoldierPosition))) ||
                (!m_IsMainParticipantTurn && (isSoldierExistInCurrentPosition('O', i_CurrentSoldierPosition) || isSoldierExistInCurrentPosition('U', i_CurrentSoldierPosition))))
            {
                playerToMove = m_GameBoardMatrix[i_CurrentSoldierPosition.Row, i_CurrentSoldierPosition.Column];
                bool isValidMoveOrJump = CollectSoldiersMoves(playerToMove, ref haveJump, ref errorMove);
                bool isPossibleToMove = isPossibleMove(playerToMove, i_NextSoldierPositionMove, haveJump, ref errorMove);
                isValidMove = isValidMoveOrJump && isPossibleToMove;
            }
            else
            {
                isValidMove = false;
                errorMove = eError.InValidMove;
            }

            return errorMove;
        }

        private void updateBoard(Player i_MovedPlayer, GameBoardPosition i_MoveTo)
        {
            bool i_IsJump = i_MovedPlayer.OptionalJumpMoves.Count > 0;
            if (i_IsJump)
            {
                int xCoordinate = (i_MovedPlayer.BoardPosition.Row + i_MoveTo.Row) / 2;
                int yCoordinate = (i_MovedPlayer.BoardPosition.Column + i_MoveTo.Column) / 2;

                if (isSoldierExistInCurrentPosition('X', i_MovedPlayer.BoardPosition) || isSoldierExistInCurrentPosition('K', i_MovedPlayer.BoardPosition))
                {
                    deleteEnemySoldierOfX(i_MovedPlayer.BoardPosition, i_MoveTo, xCoordinate, yCoordinate);
                }
                else
                {
                    deleteEnemySoldierOfO(i_MovedPlayer.BoardPosition, i_MoveTo, xCoordinate, yCoordinate);
                }
            }

            ChangeSoldierPlace(i_MovedPlayer.BoardPosition, i_MoveTo);
            BoardChangedEventHandler?.Invoke();
            CalculatePoints();
        }

        public bool CollectSoldiersMoves(Player i_PlayerToMove, ref bool io_HaveJump, ref eError io_ErrorMove)
        {
            int countJumpers = 0;
            bool isPlayerHaveToJumpIsCurrent = true;

            if (m_IsMainParticipantTurn)
            {
                foreach (Player soldier in m_PlayersOfXAndK)
                {
                    soldier.HavePotentialMove(m_GameBoardMatrix, m_BoarsSize);
                    if (soldier.OptionalJumpMoves.Count > 0)
                    {
                        countJumpers++;
                        if (soldier == i_PlayerToMove)
                        {
                            io_HaveJump = true;
                        }
                    }

                    if (soldier.HaveMoreJumps)
                    {
                        if (i_PlayerToMove == soldier)
                        {
                            isPlayerHaveToJumpIsCurrent = true;
                        }
                        else
                        {
                            io_ErrorMove = eError.JumpInContinuity;
                        }

                        break;
                    }
                }
            }
            else
            {
                foreach (Player soldier in m_PlayersOfOAndU)
                {
                    soldier.HavePotentialMove(m_GameBoardMatrix, m_BoarsSize);
                    if (soldier.OptionalJumpMoves.Count > 0)
                    {
                        countJumpers++;
                        if (soldier == i_PlayerToMove)
                        {
                            io_HaveJump = true;
                        }
                    }

                    if (soldier.HaveMoreJumps)
                    {
                        if(i_PlayerToMove == soldier)
                        {
                            isPlayerHaveToJumpIsCurrent = true;
                        }
                        else
                        {
                            io_ErrorMove = eError.JumpInContinuity;
                        }

                        break;
                    }
                }
            }

            if(!io_HaveJump && countJumpers > 0 && io_ErrorMove != eError.JumpInContinuity)
            {
                io_ErrorMove = eError.RegularJump;
            }

            return ((!io_HaveJump && countJumpers == 0) || io_HaveJump) && isPlayerHaveToJumpIsCurrent;
        }

        private bool isPossibleMove(Player i_CurrentPlayerToMove, GameBoardPosition i_WhereToMove, bool i_HaveJump, ref eError io_ErrorMove)
        {
            bool legalMove = false;

            if (i_HaveJump)
            {
                foreach (GameBoardPosition move in i_CurrentPlayerToMove.OptionalJumpMoves)
                {
                    if (move != null)
                    {
                        if (move.Column == i_WhereToMove.Column && move.Row == i_WhereToMove.Row)
                        {
                            legalMove = true;
                        }
                    }
                }
            }
            else
            {
                foreach (GameBoardPosition move in i_CurrentPlayerToMove.OptionalRegularMoves)
                {
                    if (move != null)
                    {
                        if (move.Column == i_WhereToMove.Column && move.Row == i_WhereToMove.Row)
                        {
                            legalMove = true;
                        }
                    }
                }
            }

            if(io_ErrorMove != eError.JumpInContinuity && !legalMove && i_HaveJump)
            {
                io_ErrorMove = eError.RegularJump;
            }
            else if(!legalMove)
            {
                io_ErrorMove = eError.InvaidInput;
            }

            return legalMove;
        }

        private bool isSoldierExistInCurrentPosition(char i_Soldier, GameBoardPosition i_BoardLocation)
        {
            bool isExistInPlace = false;

            if(m_GameBoardMatrix[i_BoardLocation.Row, i_BoardLocation.Column] != null)
            {
                isExistInPlace = m_GameBoardMatrix[i_BoardLocation.Row, i_BoardLocation.Column].RepresentativeCharacter == i_Soldier;
            }

            return isExistInPlace;
        }

        private void deleteEnemySoldierOfO(GameBoardPosition i_From, GameBoardPosition i_To, int i_ToDeleteX, int i_ToDeleteY)
        {
            if (m_GameBoardMatrix[i_ToDeleteX, i_ToDeleteY].RepresentativeCharacter == 'X')
            {
                m_NumberOfX--;
            }
            else
            {
                m_NumberOfK--;
            }

            DeleteSoldierFromList(m_PlayersOfXAndK, m_GameBoardMatrix[i_ToDeleteX, i_ToDeleteY]);
            m_GameBoardMatrix[i_ToDeleteX, i_ToDeleteY] = null;
            Player movedPlayer = ChangeSoldierPlace(i_From, i_To);
            bool haveMoreJumpForward = false;
            bool haveMoreJumpBackward = false;

            movedPlayer.OptionalJumpMoves.Clear();

            if (m_GameBoardMatrix[i_To.Row, i_To.Column].RepresentativeCharacter == 'U')
            {
                haveMoreJumpBackward = m_GameBoardMatrix[i_To.Row, i_To.Column].CheckAnotherJumpBackWard(m_GameBoardMatrix, m_BoarsSize);
            }

            haveMoreJumpForward = m_GameBoardMatrix[i_To.Row, i_To.Column].CheckAnotherJumpForward(m_GameBoardMatrix, m_BoarsSize);
            m_GameBoardMatrix[i_To.Row, i_To.Column].HaveMoreJumps = haveMoreJumpForward || haveMoreJumpBackward;
        }

        private void deleteEnemySoldierOfX(GameBoardPosition i_From, GameBoardPosition i_To, int i_ToDeleteX, int i_ToDeleteY)
        {
            if (m_GameBoardMatrix[i_ToDeleteX, i_ToDeleteY].RepresentativeCharacter == 'O')
            {
                m_NumberOfO--;
            }
            else
            {
                m_NumberOfU--;
            }

            DeleteSoldierFromList(m_PlayersOfOAndU, m_GameBoardMatrix[i_ToDeleteX, i_ToDeleteY]);
            bool haveMoreJumpsForward = false;
            bool haveMoreJumpBackward = false;
            m_GameBoardMatrix[i_ToDeleteX, i_ToDeleteY] = null;
            Player movedPlayer = ChangeSoldierPlace(i_From, i_To);

            movedPlayer.OptionalJumpMoves.Clear();

            if (m_GameBoardMatrix[i_To.Row, i_To.Column].RepresentativeCharacter == 'K')
            {
                haveMoreJumpsForward = m_GameBoardMatrix[i_To.Row, i_To.Column].CheckAnotherJumpForward(m_GameBoardMatrix, m_BoarsSize);
            }

            haveMoreJumpBackward = m_GameBoardMatrix[i_To.Row, i_To.Column].CheckAnotherJumpBackWard(m_GameBoardMatrix, m_BoarsSize);
            m_GameBoardMatrix[i_To.Row, i_To.Column].HaveMoreJumps = haveMoreJumpsForward || haveMoreJumpBackward;
        }

        public Player ChangeSoldierPlace(GameBoardPosition i_From, GameBoardPosition i_To)
        {
            Player soldier = m_GameBoardMatrix[i_From.Row, i_From.Column];
            m_GameBoardMatrix[i_From.Row, i_From.Column] = null;
            soldier.BoardPosition.Row = i_To.Row;
            soldier.BoardPosition.Column = i_To.Column;
            m_GameBoardMatrix[i_To.Row, i_To.Column] = soldier;

            if (m_GameBoardMatrix[i_To.Row, i_To.Column].RepresentativeCharacter == 'X')
            {
                CheckAndChangeToK(i_To);
            }
            else if (m_GameBoardMatrix[i_To.Row, i_To.Column].RepresentativeCharacter == 'O')
            {
                CheckAndChangeToU(i_To);
            }

            return soldier;
        }

        public eWinningStatus GameIsOver()
        {
            eWinningStatus winningStatus = eWinningStatus.NoWin;
            bool isOutOfMovesXAndK = true;
            bool isOutOfMovesOAndU = true;

            checkOutOfMoves(ref isOutOfMovesXAndK, ref isOutOfMovesOAndU);

            if (m_NumberOfO == 0 && m_NumberOfU == 0)
            {
                winningStatus = eWinningStatus.MainParticipantUser;
                m_VictoryPointsOfMainParticipant += m_MainParticipantPoints;
            }
            else if (m_NumberOfX == 0 && m_NumberOfK == 0)
            {
                winningStatus = eWinningStatus.SecondParticipantUser;
                m_VictoryPointsOfSecondParticipant += m_SecondParticipantPoints;
            }
            else if (isOutOfMovesOAndU == true && isOutOfMovesXAndK == true)
            {
                winningStatus = eWinningStatus.ThereIsDraw;
            }
            else if (isOutOfMovesOAndU == true && isOutOfMovesXAndK == false && !m_IsMainParticipantTurn)
            {
                winningStatus = eWinningStatus.MainParticipantUser;
                m_VictoryPointsOfMainParticipant += m_MainParticipantPoints;
            }
            else if (isOutOfMovesOAndU == false && isOutOfMovesXAndK == true && m_IsMainParticipantTurn)
            {
                winningStatus = eWinningStatus.SecondParticipantUser;
                m_VictoryPointsOfSecondParticipant += m_SecondParticipantPoints;
            }

            return winningStatus;
        }

        public eError MakeMove(GameBoardPosition i_SourceGameBoardPosition, GameBoardPosition i_DestGameBoardPosition)
        {
            eError error = isValidMove(i_SourceGameBoardPosition, i_DestGameBoardPosition);
            if (error == eError.ValidMoveAndInput)
            {
                updateBoard(GameMatrix[i_SourceGameBoardPosition.Row, i_SourceGameBoardPosition.Column], i_DestGameBoardPosition);
                GamePointsChangedEventHandler?.Invoke();
                NextTurn(i_DestGameBoardPosition);

                if (!IsMainParticipantTurn && !m_UserPreferences.IsTwoPlayersGame)
                {
                    GameBoardPosition toWhereSoldierMoved = ComputerRandomLogic();
                    NextTurn(toWhereSoldierMoved);
                }
            }

            return error;
        }

        private void checkOutOfMoves(ref bool io_IsOutOfMovesXAndK, ref bool io_IsOutOfMovesOAndU)
        {
            foreach (Player currentPlayer in m_PlayersOfXAndK)
            {
                currentPlayer.HavePotentialMove(m_GameBoardMatrix, m_BoarsSize);
                if (currentPlayer.OptionalRegularMoves.Count != 0 || currentPlayer.OptionalJumpMoves.Count != 0)
                {
                    io_IsOutOfMovesXAndK = false;
                }
            }

            foreach (Player currentPlayer in m_PlayersOfOAndU)
            {
                currentPlayer.HavePotentialMove(m_GameBoardMatrix, m_BoarsSize);
                if (currentPlayer.OptionalRegularMoves.Count != 0 || currentPlayer.OptionalJumpMoves.Count != 0)
                {
                    io_IsOutOfMovesOAndU = false;
                }
            }
        }

        public void CheckAndChangeToK(GameBoardPosition i_SoldierPosition)
        {
            bool isK = i_SoldierPosition.Row == 0;

            if (isK)
            {
                m_NumberOfK++;
                m_GameBoardMatrix[i_SoldierPosition.Row, i_SoldierPosition.Column].RepresentativeCharacter = 'K';
                m_NumberOfX--;
            }
        }

        public bool CheckAndChangeToU(GameBoardPosition i_SoldierPosition)
        {
            bool isU = i_SoldierPosition.Row == (m_BoarsSize - 1);

            if(isU)
            {
                m_NumberOfU++;
                m_GameBoardMatrix[i_SoldierPosition.Row, i_SoldierPosition.Column].RepresentativeCharacter = 'U';
                m_NumberOfO--;
            }

            return isU;
        }

        public GameBoardPosition ComputerRandomLogic()
        {
            bool isJump = false;
            GameBoardPosition fromWhereSoldierMoved = null;
            GameBoardPosition toWhereSoldierMoved = null;
            eWinningStatus winningStatus = eWinningStatus.NoWin;

            ComputerPlayers.ComputerRandomMove(m_PlayersOfOAndU, GameMatrix, m_UserPreferences.BoardSize, ref fromWhereSoldierMoved, ref toWhereSoldierMoved, ref isJump, ref winningStatus);
            if(fromWhereSoldierMoved != null)
            {
                if(isJump)
                {
                    int xCoordinate = (fromWhereSoldierMoved.Row + toWhereSoldierMoved.Row) / 2;
                    int yCoordinate = (fromWhereSoldierMoved.Column + toWhereSoldierMoved.Column) / 2;

                    deleteEnemySoldierOfO(fromWhereSoldierMoved, toWhereSoldierMoved, xCoordinate, yCoordinate);
                    Player movedPlayer = m_GameBoardMatrix[toWhereSoldierMoved.Row, toWhereSoldierMoved.Column];

                    while(movedPlayer.OptionalJumpMoves.Count > 0)
                    {
                        toWhereSoldierMoved = m_ComputerPlayers.DoubleJumpRandom(movedPlayer);
                        xCoordinate = (movedPlayer.BoardPosition.Row + toWhereSoldierMoved.Row) / 2;
                        yCoordinate = (movedPlayer.BoardPosition.Column + toWhereSoldierMoved.Column) / 2;
                        deleteEnemySoldierOfO(movedPlayer.BoardPosition, toWhereSoldierMoved, xCoordinate, yCoordinate);
                        movedPlayer = m_GameBoardMatrix[toWhereSoldierMoved.Row, toWhereSoldierMoved.Column];
                    }
                }

                ChangeSoldierPlace(fromWhereSoldierMoved, toWhereSoldierMoved);
            }
            else if(winningStatus == eWinningStatus.MainParticipantUser)
            {
                GameOverEventHandler?.Invoke(winningStatus);
            }

            return toWhereSoldierMoved;
        }

        public void DeleteSoldierFromList(List<Player> i_ListOfPlayers, Player i_PlayerThatWasEated)
        {
            for (int i = 0; i < i_ListOfPlayers.Count; ++i)
            {
                if (Player.IsInSamePlace(i_ListOfPlayers[i], i_PlayerThatWasEated))
                {
                    i_ListOfPlayers.Remove(i_ListOfPlayers[i]);
                }
            }
        }
        
        public void CalculatePoints()
        {
            if (m_MainParticipantPoints != m_VictoryPointsOfMainParticipant + ((4 * NumberOfK) + NumberOfX) - ((4 * NumberOfU) + NumberOfO) ||
                m_SecondParticipantPoints != m_VictoryPointsOfSecondParticipant + ((4 * NumberOfU) + NumberOfO) - ((4 * NumberOfK) + NumberOfX))
            {
                m_MainParticipantPoints = m_VictoryPointsOfMainParticipant + ((4 * NumberOfK) + NumberOfX) - ((4 * NumberOfU) + NumberOfO);
                m_SecondParticipantPoints = m_VictoryPointsOfSecondParticipant + ((4 * NumberOfU) + NumberOfO) - ((4 * NumberOfK) + NumberOfX);
                GamePointsChangedEventHandler?.Invoke();
            }
        }

        public string[,] GetMatrix()
        {
            string[,] stringMatrix = new string[m_BoarsSize, m_BoarsSize];
            for (int i = 0; i < m_BoarsSize; i++)
            {
                for (int j = 0; j < m_BoarsSize; j++)
                {
                    if (GameMatrix[i, j] == null)
                    {
                        stringMatrix[i, j] = string.Empty;
                    }
                    else
                    {
                        stringMatrix[i, j] = GameMatrix[i, j].RepresentativeCharacter.ToString();
                    }
                }
            }

            return stringMatrix;
        }

        public bool IsCurrentParticipatePlayerByString(string i_Str)
        {
            return IsMainParticipantTurn ? i_Str == "X" || i_Str == "K" : i_Str == "O" || i_Str == "U";
        }

        public void NextTurn(GameBoardPosition i_CurrentSoldierPosition)
        {
            eWinningStatus winningStatus = GameIsOver();
            if (winningStatus != eWinningStatus.NoWin)
            {
                GameOverEventHandler?.Invoke(winningStatus);
            }
            else
            {
                UpdateTurn(i_CurrentSoldierPosition);
                BoardChangedEventHandler?.Invoke();
            }
        }

        public void UpdateTurn(GameBoardPosition i_CurrentSoldierPosition)
        {
            char whoPlayed = m_GameBoardMatrix[i_CurrentSoldierPosition.Row, i_CurrentSoldierPosition.Column].RepresentativeCharacter;

            if (whoPlayed == 'X' || whoPlayed == 'K')
            {
                m_IsMainParticipantTurn = m_GameBoardMatrix[i_CurrentSoldierPosition.Row, i_CurrentSoldierPosition.Column].HaveMoreJumps;
            }
            else
            {
                m_IsMainParticipantTurn = !m_GameBoardMatrix[i_CurrentSoldierPosition.Row, i_CurrentSoldierPosition.Column].HaveMoreJumps;
            }
        }
    }
}
