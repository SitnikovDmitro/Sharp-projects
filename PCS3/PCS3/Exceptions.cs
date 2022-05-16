using System;
using System.Collections.Generic;
using System.Text;

namespace PCS3
{
    class EmptyParametersException: Exception
    {
        public EmptyParametersException ()
        {
        }
    }

    class TooLongAgeException : Exception
    {
        public int Age { get; private set; }

        public TooLongAgeException(int age)
        {
            Age = age;
        }
    }

    class InvalidBirthDateException : Exception
    {
        public DateTime Birthdate { get; private set; }

        public InvalidBirthDateException(DateTime birthdate)
        {
            Birthdate = birthdate;
        }
    }


    class InvalidEmailException : Exception
    {
        public string Email { get; private set; }

        public InvalidEmailException(string email)
        {
            Email = email;
        }
    }
}
