using System.Collections.Generic;
using System.Drawing;
using GameBusinessLogic;

namespace Ex05.WindowsUI
{
    public class GameSettingsGlobal
    {
        public static readonly Color sr_DefaultColorSelectedSoldier = Color.LightBlue;
        public static readonly Color sr_DefaultColorBoardBackroundDark = Color.LightGray;
        public static readonly Color sr_DefaultColorBoardBackroundLight = Color.White;
        public static readonly string sr_DefaultNamePlayer2 = "[Computer]";

        public static readonly Dictionary<eError, string> sr_ErrorToStringDict = new Dictionary<eError, string>
        {
            { eError.InvaidInput, "Player cannot move to this location" },
            { eError.InValidMove, "Player cannot move to this location" },
            { eError.JumpInContinuity, "player must complete the continuous jump!" },
            { eError.RegularJump, "Player must jump!" },
        };

        public static readonly Dictionary<eWinningStatus, string> sr_WinToStringDict = new Dictionary<eWinningStatus, string>
        {
            { eWinningStatus.MainParticipantUser, "Player 1 won!" },
            { eWinningStatus.SecondParticipantUser, "Player  2 won!" },
            { eWinningStatus.ThereIsDraw, "Tie!" },
        };
    }
}
