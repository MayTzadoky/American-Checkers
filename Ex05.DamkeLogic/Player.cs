using System;
using System.Collections.Generic;

namespace GameBusinessLogic
{
    public class Player
    {
        private char m_RepresentativeCharacter;
        private GameBoardPosition m_BoardPosition;
        private bool m_HaveMoreJumps;
        private List<GameBoardPosition> m_OptionalRegularMoves;
        private List<GameBoardPosition> m_OptionalJumpMoves;

        public Player(int i_X, int i_Y, char i_RepresentativeCharacter)
        {
            m_RepresentativeCharacter = i_RepresentativeCharacter;
            m_BoardPosition = new GameBoardPosition(i_Y, i_X);
            m_HaveMoreJumps = false;
            m_OptionalRegularMoves = new List<GameBoardPosition>();
            m_OptionalJumpMoves = new List<GameBoardPosition>();
        }

        public char RepresentativeCharacter
        {
            get
            {
                return m_RepresentativeCharacter;
            }

            set
            {
                m_RepresentativeCharacter = value;
            }
        }

        public bool HaveMoreJumps
        {
            get
            {
                return m_HaveMoreJumps;
            }

            set
            {
                m_HaveMoreJumps = value;
            }
        }

        public List<GameBoardPosition> OptionalRegularMoves
        {
            get
            {
                return m_OptionalRegularMoves;
            }

            set
            {
                m_OptionalRegularMoves = value;
            }
        }

        public List<GameBoardPosition> OptionalJumpMoves
        {
            get
            {
                return m_OptionalJumpMoves;
            }

            set
            {
                m_OptionalJumpMoves = value;
            }
        }

        public GameBoardPosition BoardPosition
        {
            get
            {
                return m_BoardPosition;
            }
        }

        public static bool IsInSamePlace(Player i_Player1, Player i_Player2)
        {
            return i_Player1.BoardPosition.Column == i_Player2.BoardPosition.Column && i_Player1.BoardPosition.Row == i_Player2.BoardPosition.Row;
        }

        public bool IsMainParticipant()
        {
            return RepresentativeCharacter == 'K' || RepresentativeCharacter == 'X';
        }

        public void CheckOppositeCharacter(char i_RepresentativeCharacter, out char o_OppositeCharacter1, out char o_OppositeCharacter2)
        {
            if (RepresentativeCharacter == 'K' || RepresentativeCharacter == 'X')
            {
                o_OppositeCharacter1 = 'O';
                o_OppositeCharacter2 = 'U';
            }
            else
            {
                o_OppositeCharacter1 = 'X';
                o_OppositeCharacter2 = 'K';
            }
        }

        private bool checkAnotherJumpForwardAndLeft(Player[,] i_GameBoardMatrix, int i_BoarsSize)
        {
            int xCoordinateAfterJump = m_BoardPosition.Row + 2;
            int yCoordinateInLeftSideAfterJump = m_BoardPosition.Column - 2;
            bool validJump = xCoordinateAfterJump < i_BoarsSize && yCoordinateInLeftSideAfterJump >= 0;
            char oppositeCharacter1;
            char oppositeCharacter2;

            CheckOppositeCharacter(m_RepresentativeCharacter, out oppositeCharacter1, out oppositeCharacter2);

            if (validJump)
            {
                int xCoordinateBetweenTheCoordinates = (m_BoardPosition.Row + xCoordinateAfterJump) / 2;
                int yCoordinateBetweenTheCoordinates = (m_BoardPosition.Column + yCoordinateInLeftSideAfterJump) / 2;

                validJump = i_GameBoardMatrix[xCoordinateBetweenTheCoordinates, yCoordinateBetweenTheCoordinates] != null &&
                            i_GameBoardMatrix[xCoordinateAfterJump, yCoordinateInLeftSideAfterJump] == null;
                if (validJump)
                {
                    char representativeCharacter = i_GameBoardMatrix[xCoordinateBetweenTheCoordinates, yCoordinateBetweenTheCoordinates].RepresentativeCharacter;

                    validJump = CheckJumpOverEnemy(representativeCharacter, oppositeCharacter1, oppositeCharacter2);
                    if (validJump)
                    {
                        GameBoardPosition nextPosition = new GameBoardPosition(yCoordinateInLeftSideAfterJump, xCoordinateAfterJump);
                        m_OptionalJumpMoves.Add(nextPosition);
                    }
                }
            }

            return validJump;
        }

        public bool CheckJumpOverEnemy(char i_RepresentativeCharacter, char i_OppositeCharacter1, char i_OppositeCharacter2)
        {
            return (i_RepresentativeCharacter == i_OppositeCharacter1) || (i_RepresentativeCharacter == i_OppositeCharacter2);
        }

        private bool checkAnotherJumpForwardAndRight(Player[,] i_GameBoardMatrix, int i_BoarsSize)
        {
            int xCoordinateAfterJump = m_BoardPosition.Row + 2;
            int yCoordinateInRightSideAfterJump = m_BoardPosition.Column + 2;
            bool validJump = xCoordinateAfterJump < i_BoarsSize && yCoordinateInRightSideAfterJump < i_BoarsSize;
            char oppositeCharacter1;
            char oppositeCharacter2;

            CheckOppositeCharacter(m_RepresentativeCharacter, out oppositeCharacter1, out oppositeCharacter2);

            if (validJump)
            {
                int xCoordinateBetweenTheCoordinates = (m_BoardPosition.Row + xCoordinateAfterJump) / 2;
                int yCoordinateBetweenTheCoordinates = (m_BoardPosition.Column + yCoordinateInRightSideAfterJump) / 2;

                validJump = i_GameBoardMatrix[xCoordinateBetweenTheCoordinates, yCoordinateBetweenTheCoordinates] != null 
                            && i_GameBoardMatrix[xCoordinateAfterJump, yCoordinateInRightSideAfterJump] == null;
                if (validJump)
                {
                    char representativeCharacter = i_GameBoardMatrix[xCoordinateBetweenTheCoordinates, yCoordinateBetweenTheCoordinates].RepresentativeCharacter;
                    validJump = CheckJumpOverEnemy(representativeCharacter, oppositeCharacter1, oppositeCharacter2);
                    if (validJump)
                    {
                        GameBoardPosition nextPosition = new GameBoardPosition(yCoordinateInRightSideAfterJump, xCoordinateAfterJump);
                        m_OptionalJumpMoves.Add(nextPosition);
                    }
                }
            }

            return validJump;
        }

        public bool CheckAnotherJumpForward(Player[,] i_GameBoardMatrix, int i_BoarsSize)
        {
            return checkAnotherJumpForwardAndLeft(i_GameBoardMatrix, i_BoarsSize) || checkAnotherJumpForwardAndRight(i_GameBoardMatrix, i_BoarsSize);
        }

        public bool CheckAnotherJumpBackWard(Player[,] i_GameBoardMatrix, int i_BoarsSize)
        {
            return checkAnotherJumpBackAndLeft(i_GameBoardMatrix) || checkAnotherJumpBackAndRight(i_GameBoardMatrix, i_BoarsSize);
        }

        private bool checkAnotherJumpBackAndLeft(Player[,] i_GameBoardMatrix)
        {
            int xCoordinateAfterJump = m_BoardPosition.Row - 2;
            int yCoordinateInLeftSideAfterJump = m_BoardPosition.Column - 2;
            bool validJump = xCoordinateAfterJump >= 0 && yCoordinateInLeftSideAfterJump >= 0;
            char oppositeCharacter1;
            char oppositeCharacter2;

            CheckOppositeCharacter(m_RepresentativeCharacter, out oppositeCharacter1, out oppositeCharacter2);

            if (validJump)
            {
                int xCoordinateBetweenTheCoordinates = (m_BoardPosition.Row + xCoordinateAfterJump) / 2;
                int yCoordinateBetweenTheCoordinates = (m_BoardPosition.Column + yCoordinateInLeftSideAfterJump) / 2;

                validJump = i_GameBoardMatrix[xCoordinateBetweenTheCoordinates, yCoordinateBetweenTheCoordinates] != null &&
                            i_GameBoardMatrix[xCoordinateAfterJump, yCoordinateInLeftSideAfterJump] == null;
                if (validJump)
                {
                    char representativeCharacter = i_GameBoardMatrix[xCoordinateBetweenTheCoordinates, yCoordinateBetweenTheCoordinates].RepresentativeCharacter;
                    validJump = CheckJumpOverEnemy(representativeCharacter, oppositeCharacter1, oppositeCharacter2);
                    if (validJump)
                    {
                        GameBoardPosition nextPosition = new GameBoardPosition(yCoordinateInLeftSideAfterJump, xCoordinateAfterJump);
                        m_OptionalJumpMoves.Add(nextPosition);
                    }
                }
            }

            return validJump;
        }

        private bool checkAnotherJumpBackAndRight(Player[,] i_GameBoardMatrix, int i_BoarsSize)
        {
            int xCoordinateAfterJump = m_BoardPosition.Row - 2;
            int yCoordinateInRightSideAfterJump = m_BoardPosition.Column + 2;
            bool validJump = xCoordinateAfterJump >= 0 && yCoordinateInRightSideAfterJump < i_BoarsSize;
            char oppositeCharacter1;
            char oppositeCharacter2;

            CheckOppositeCharacter(m_RepresentativeCharacter, out oppositeCharacter1, out oppositeCharacter2);

            if (validJump)
            {
                int xCoordinateBetweenTheCoordinates = (m_BoardPosition.Row + xCoordinateAfterJump) / 2;
                int yCoordinateBetweenTheCoordinates = (m_BoardPosition.Column + yCoordinateInRightSideAfterJump) / 2;

                validJump = i_GameBoardMatrix[xCoordinateBetweenTheCoordinates, yCoordinateBetweenTheCoordinates] != null && 
                            i_GameBoardMatrix[xCoordinateAfterJump, yCoordinateInRightSideAfterJump] == null;
                if (validJump)
                {
                    char representativeCharacter = i_GameBoardMatrix[xCoordinateBetweenTheCoordinates, yCoordinateBetweenTheCoordinates].RepresentativeCharacter;
                    validJump = CheckJumpOverEnemy(representativeCharacter, oppositeCharacter1, oppositeCharacter2);
                    if(validJump)
                    {
                        GameBoardPosition nextPosition = new GameBoardPosition(yCoordinateInRightSideAfterJump, xCoordinateAfterJump);
                        m_OptionalJumpMoves.Add(nextPosition);
                    }
                }
            }

            return validJump;
        }

        public bool HavePotentialMove(Player[,] i_GameBoardMatrix, int i_BoardSize)
        {
            bool canMoveForward = false;
            bool canMoveBackward = false;

            OptionalRegularMoves.Clear();
            OptionalJumpMoves.Clear();

            if (RepresentativeCharacter == 'O')
            {
                canMoveForward = isCanMoveForward(i_GameBoardMatrix, i_BoardSize);
            }
            else if (RepresentativeCharacter == 'X')
            {
                canMoveBackward = isCanMoveBackward(i_GameBoardMatrix, i_BoardSize);
            }
            else
            {
                canMoveForward = isCanMoveForward(i_GameBoardMatrix, i_BoardSize);
                canMoveBackward = isCanMoveBackward(i_GameBoardMatrix, i_BoardSize);
            }

            return canMoveForward || canMoveBackward;
        }

        private bool isCanMoveForward(Player[,] i_GameBoardMatrix, int i_SizeOfBoard)
        {
            bool isCanJump = false;
            bool isCanMoveToEmpty = false;

            if (BoardPosition.Row == i_SizeOfBoard - 2)
            {
                isCanMoveToEmpty = isNextMovePlaceEmpty(i_GameBoardMatrix, eDirectionOnBoard.ForwardOneStep, i_SizeOfBoard);
            }
            else if (BoardPosition.Row < i_SizeOfBoard - 2)
            {
                isCanMoveToEmpty = isNextMovePlaceEmpty(i_GameBoardMatrix, eDirectionOnBoard.ForwardOneStep, i_SizeOfBoard);
                isCanJump = CanEatEnemySoldier(i_GameBoardMatrix, eDirectionOnBoard.ForwardJumpStep, i_SizeOfBoard);
            }

            return isCanMoveToEmpty || isCanJump;
        }

        private bool isCanMoveBackward(Player[,] i_GameBoardMatrix, int i_BoardSize)
        {
            bool isCanJump = false;
            bool isCanMoveToEmpty = false;
            if (BoardPosition.Row == 1)
            {
                isCanMoveToEmpty = isNextMovePlaceEmpty(i_GameBoardMatrix, eDirectionOnBoard.BackwardOneStep, i_BoardSize);
            }
            else if (BoardPosition.Row > 1)
            {
                isCanMoveToEmpty = isNextMovePlaceEmpty(i_GameBoardMatrix, eDirectionOnBoard.BackwardOneStep, i_BoardSize);
                isCanJump = CanEatEnemySoldier(i_GameBoardMatrix, eDirectionOnBoard.BackwardJumpStep, i_BoardSize);
            }

            return isCanMoveToEmpty || isCanJump;
        }

        private bool isNextMovePlaceEmpty(Player[,] i_GameBoardMatrix, eDirectionOnBoard i_DirectionRow, int i_BoardSize)
        {
            bool isPlaceEmptyRight = false;
            bool isPlaceEmptyLeft = false;

            if ((BoardPosition.Column == 0 && i_DirectionRow == eDirectionOnBoard.ForwardOneStep)
                || (BoardPosition.Column == 0 && i_DirectionRow == eDirectionOnBoard.BackwardOneStep))
            {
                isPlaceEmptyRight = isRightPlaceEmpty(i_GameBoardMatrix, i_DirectionRow);
            }
            else if (((BoardPosition.Column == i_BoardSize - 1) && i_DirectionRow == eDirectionOnBoard.ForwardOneStep)
                || (BoardPosition.Column == i_BoardSize - 1 && i_DirectionRow == eDirectionOnBoard.BackwardOneStep))
            {
                isPlaceEmptyLeft = isLeftPlaceEmpty(i_GameBoardMatrix, i_DirectionRow);
            }
            else
            {
                isPlaceEmptyRight = isRightPlaceEmpty(i_GameBoardMatrix, i_DirectionRow);
                isPlaceEmptyLeft = isLeftPlaceEmpty(i_GameBoardMatrix, i_DirectionRow);
            }

            return isPlaceEmptyRight || isPlaceEmptyLeft;
        }

        private bool isRightPlaceEmpty(Player[,] i_GameBoardMatrix, eDirectionOnBoard i_DirectionRow)
        {
            bool isRightPlaceEmpty = i_GameBoardMatrix[BoardPosition.Row + (int)i_DirectionRow, BoardPosition.Column + 1] == null;

            if (isRightPlaceEmpty)
            {
                GameBoardPosition emptyPlace = new GameBoardPosition(BoardPosition.Column + 1, BoardPosition.Row + (int)i_DirectionRow);
                OptionalRegularMoves.Add(emptyPlace);
            }

            return isRightPlaceEmpty;
        }

        private bool isLeftPlaceEmpty(Player[,] i_GameBoardMatrix, eDirectionOnBoard i_DirectionRow)
        {
            bool isLeftPlaceEmpty = i_GameBoardMatrix[BoardPosition.Row + (int)i_DirectionRow, BoardPosition.Column - 1] == null;

            if (isLeftPlaceEmpty)
            {
                GameBoardPosition emptyPlace = new GameBoardPosition(BoardPosition.Column - 1, BoardPosition.Row + (int)i_DirectionRow);
                OptionalRegularMoves.Add(emptyPlace);
            }

            return isLeftPlaceEmpty;
        }

        public bool CanEatEnemySoldier(Player[,] i_GameBoardMatrix, eDirectionOnBoard i_DirectionRow, int i_BoardSize)
        {
            bool canEatFromRight = false;
            bool canEatFromLeft = false;
            eDirectionOnBoard checkIfEnemyIn;

            if (i_DirectionRow == eDirectionOnBoard.BackwardJumpStep)
            {
                checkIfEnemyIn = eDirectionOnBoard.BackwardOneStep;
            }
            else
            {
                checkIfEnemyIn = eDirectionOnBoard.ForwardOneStep;
            }

            if (BoardPosition.Column == 0)
            {
                if (haveSoldierToEatIRight(i_GameBoardMatrix, checkIfEnemyIn))
                {
                    canEatFromRight = isCanEatInDirection(i_GameBoardMatrix, eDirectionOnBoard.RightJump, i_DirectionRow, i_BoardSize);
                }
            }
            else if (BoardPosition.Column == i_BoardSize - 1)
            {
                if (haveSoldierToEatInLeft(i_GameBoardMatrix, checkIfEnemyIn))
                {
                    canEatFromLeft = isCanEatInDirection(i_GameBoardMatrix, eDirectionOnBoard.LeftJump, i_DirectionRow, i_BoardSize);
                }
            }
            else
            {
                if (haveSoldierToEatInLeft(i_GameBoardMatrix, checkIfEnemyIn))
                {
                    canEatFromLeft = isCanEatInDirection(i_GameBoardMatrix, eDirectionOnBoard.LeftJump, i_DirectionRow, i_BoardSize);
                }

                if (haveSoldierToEatIRight(i_GameBoardMatrix, checkIfEnemyIn))
                {
                    canEatFromRight = isCanEatInDirection(i_GameBoardMatrix, eDirectionOnBoard.RightJump, i_DirectionRow, i_BoardSize);
                }
            }

            return canEatFromRight || canEatFromLeft;
        }

        private bool haveSoldierToEatIRight(Player[,] i_GameBoardMatrix, eDirectionOnBoard i_DirectionRow)
        {
            bool isCanEat = false;
            char EnemySoldier;
            char EnemySoldierKing;

            CheckOppositeCharacter(RepresentativeCharacter, out EnemySoldier, out EnemySoldierKing);
            if (i_GameBoardMatrix[BoardPosition.Row + (int)i_DirectionRow, BoardPosition.Column + 1] != null)
            {
                isCanEat = i_GameBoardMatrix[BoardPosition.Row + (int)i_DirectionRow, BoardPosition.Column + 1].RepresentativeCharacter == EnemySoldier
                    || i_GameBoardMatrix[BoardPosition.Row + (int)i_DirectionRow, BoardPosition.Column + 1].RepresentativeCharacter == EnemySoldierKing;
            }

            return isCanEat;
        }

        private bool haveSoldierToEatInLeft(Player[,] i_GameBoardMatrix, eDirectionOnBoard i_DirectionRow)
        {
            bool isCanEat = false;
            char enemySoldier;
            char enemySoldierKing;

            CheckOppositeCharacter(RepresentativeCharacter, out enemySoldier, out enemySoldierKing);
            if (i_GameBoardMatrix[BoardPosition.Row + (int)i_DirectionRow, BoardPosition.Column - 1] != null)
            {
                isCanEat = i_GameBoardMatrix[BoardPosition.Row + (int)i_DirectionRow, BoardPosition.Column - 1].RepresentativeCharacter == enemySoldier
                    || i_GameBoardMatrix[BoardPosition.Row + (int)i_DirectionRow, BoardPosition.Column - 1].RepresentativeCharacter == enemySoldierKing;
            }

            return isCanEat;
        }

        private bool isCanEatInDirection(Player[,] i_GameBoardMatrix, eDirectionOnBoard i_DirectionColumn, eDirectionOnBoard i_DirectionRow, int i_BoardSize)
        {
            bool canEatForward = false;
            if (BoardPosition.Column + i_DirectionColumn >= 0 && BoardPosition.Row + i_DirectionRow >= 0
                && BoardPosition.Column + (int)i_DirectionColumn < i_BoardSize && BoardPosition.Row + (int)i_DirectionRow < i_BoardSize)
            {
                if (i_GameBoardMatrix[BoardPosition.Row + (int)i_DirectionRow, BoardPosition.Column + (int)i_DirectionColumn] == null)
                {
                    GameBoardPosition emptyPlace = new GameBoardPosition(BoardPosition.Column + (int)i_DirectionColumn, BoardPosition.Row + (int)i_DirectionRow);
                    OptionalJumpMoves.Add(emptyPlace);
                    canEatForward = true;
                }
            }

            return canEatForward;
        }
    }
}