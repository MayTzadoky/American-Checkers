using System;

namespace GameBusinessLogic
{
    public class UserPreferences
    {
        private string m_MainParticipantUserName;
        private string m_SecondParticipantUserName;
        private bool m_IsTwoPlayersGame;
        private int m_BoardSize;

        public UserPreferences(string i_MainParticipantName, int i_BoardDamkaSize, bool i_IsTwoPlayersGame, string i_SecondParticipantUserName)
        {
            m_MainParticipantUserName = i_MainParticipantName;
            m_IsTwoPlayersGame = i_IsTwoPlayersGame;
            m_SecondParticipantUserName = i_SecondParticipantUserName;
            m_BoardSize = i_BoardDamkaSize;
        }

        public string MainParticipantUserName
        {
            get
            {
                return m_MainParticipantUserName;
            }
        }

        public string SecondParticipantUserName
        {
            get
            {
                return m_SecondParticipantUserName;
            }
        }

        public bool IsTwoPlayersGame
        {
            get
            {
                return m_IsTwoPlayersGame;
            }

            set
            {
                m_IsTwoPlayersGame = value;
            }
        }

        public int BoardSize
        {
            get
            {
                return m_BoardSize;
            }
        }
    }
}
