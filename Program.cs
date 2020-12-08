using System;

namespace Struktury
{
    class Program
    {
        static void Main(string[] args)
        {



            
            TimePeriod tp1 = new TimePeriod(22, 50, 50);
            TimePeriod tp2 = new TimePeriod(5, 11, 5);
            Time timeExpected = new Time(18, 00, 00);


            Console.WriteLine(tp1.Plus(tp2));


        }
    }
}
