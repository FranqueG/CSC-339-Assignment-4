
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMethods
{
    public class Exceptions
    {
        
        public class OutOfGameBoardBoundsException : Exception
        {
            public OutOfGameBoardBoundsException()
            {

            }
            public OutOfGameBoardBoundsException(string Message) : base(Message)
            { }
        }

        public class BoardFullException:Exception
        {
            public BoardFullException()
            {

            }
            public BoardFullException(string Message) : base(Message)
            { }
        }

        public class InvalidBoardDimensionsException : Exception
        {
            public InvalidBoardDimensionsException()
            {

            }
            public InvalidBoardDimensionsException(string Message) : base(Message)
            { }
        }

        public class WrongPlayerMoveException:Exception
        {
            public WrongPlayerMoveException()
            {

            }
            public WrongPlayerMoveException(string Message) : base(Message)
            { }
        }
    }
}
