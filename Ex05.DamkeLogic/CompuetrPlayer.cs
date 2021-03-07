using System;
using System.Collections.Generic;

namespace GameBusinessLogic
{
    public class ComputerPlayer
    {
        public void ComputerRandomMove(List<Player> i_CompPlayers, Player[,] i_GameBoardMatrix, int i_BoardSize, ref GameBoardPosition io_FromWhereSoldierMoved, ref GameBoardPosition io_ToWhereSoldierMoved, ref bool io_IsJump, ref eWinningStatus io_eWinningStatus)
        {
            GameBoardPosition movedPosition = null;
            Player playerMove = null;
            List<Player> soldierCanMoveEmpty = new List<Player>();
            List<Player> soldierCanMoveJump = new List<Player>();

            for (int i = 0; i < i_CompPlayers.Count; ++i)
            {
                if (i_CompPlayers[i].HavePotentialMove(i_GameBoardMatrix, i_BoardSize))
                {
                    if (i_CompPlayers[i].OptionalJumpMoves.Count > 0)
                    {
                        soldierCanMoveJump.Add(i_CompPlayers[i]);
                    }
                    else
                    {
                        soldierCanMoveEmpty.Add(i_CompPlayers[i]);
                    }
                }
            }

            if (i_CompPlayers.Count > 0)
            {
                Random randPlay = new Random();

                if (soldierCanMoveJump.Count > 0)
                {
                    playerMove = randomMove(soldierCanMoveJump, ref io_FromWhereSoldierMoved);
                    int randomMovePos = randPlay.Next(0, playerMove.OptionalJumpMoves.Count - 1);
                    movedPosition = playerMove.OptionalJumpMoves[randomMovePos];
                    io_IsJump = true;
                }
                else if (soldierCanMoveEmpty.Count > 0)
                {
                    playerMove = randomMove(soldierCanMoveEmpty, ref io_FromWhereSoldierMoved);
                    int randomMovePos = randPlay.Next(0, playerMove.OptionalRegularMoves.Count - 1);
                    movedPosition = playerMove.OptionalRegularMoves[randomMovePos];
                }
                else if(soldierCanMoveJump.Count == 0 && soldierCanMoveEmpty.Count == 0)
                {
                    io_eWinningStatus = eWinningStatus.MainParticipantUser;
                }
            }

            io_ToWhereSoldierMoved = movedPosition;
        }

        private Player randomMove(List<Player> i_PlayersToMove, ref GameBoardPosition io_FromWhereSoldierMoved)
        {
            Random randPlay = new Random();
            int randomSoldier = randPlay.Next(0, i_PlayersToMove.Count - 1);

            io_FromWhereSoldierMoved = i_PlayersToMove[randomSoldier].BoardPosition;

            return i_PlayersToMove[randomSoldier];
        }

        public GameBoardPosition DoubleJumpRandom(Player i_PlayerToDoubleJump)
        {
            Random randPlay = new Random();
            int randomMove = randPlay.Next(0, i_PlayerToDoubleJump.OptionalJumpMoves.Count - 1);
            GameBoardPosition movedPosition = i_PlayerToDoubleJump.OptionalJumpMoves[randomMove];

            return movedPosition;
        }
    }
}
