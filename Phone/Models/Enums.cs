using System;

namespace Phone.Models
{
    [Flags]
    public enum SimOperators
    {
        Irancell = 2,
        Mci = 4,
        Rightel = 8,
        Taliya = 16,
        Mobin=32
    }
    class program
    {
        static void main(string[] args)
        {
            int op = 14;
            Console.WriteLine((SimOperators)op);
        }
    }
}