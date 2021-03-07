using System;

namespace GameBusinessLogic
{
    public class GameBoardPosition
    {
        private int m_Column;
        private int m_Row;

        public GameBoardPosition(int i_Column, int i_Row)
        {
            m_Column = i_Column;
            m_Row = i_Row;
        }

        public int Column
        {
            get
            {
                return m_Column;
            }

            set
            {
                m_Column = value;
            }
        }

        public int Row
        {
            get
            {
                return m_Row;
            }

            set
            {
                m_Row = value;
            }
        }
    }
}
