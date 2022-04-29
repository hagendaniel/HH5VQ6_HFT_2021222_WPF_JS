using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH5VQ6_HFT_2021221.Logic
{
    class InvalidPlaceException : Exception
    {
        public InvalidPlaceException()
        {
        }

        public InvalidPlaceException(string message) : base(message)
        {
        }
    }

    class SeasonNotFinishedException : Exception
    {
        public SeasonNotFinishedException()
        {

        }

        public SeasonNotFinishedException(string message) : base(message)
        {
        }
    }

    class PlayerAlreadyDeadException : Exception
    {
        public PlayerAlreadyDeadException()
        {

        }

        public PlayerAlreadyDeadException(string message) : base(message)
        {
        }
    }
    class PlayerDoesNotExistException : Exception
    {
        public PlayerDoesNotExistException()
        {

        }

        public PlayerDoesNotExistException(string message) : base(message)
        {
        }
    }
    class PlayerNotDeadException : Exception
    {
        public PlayerNotDeadException()
        {

        }

        public PlayerNotDeadException(string message) : base(message)
        {
        }
    }
}
