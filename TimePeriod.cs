using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Struktury
{
    /// <summary>
    ///  This struct provides time as a period
    /// </summary>
    public struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod>
    {

        public long innerTimePeriod { get; }

        public byte tp_Hours { get; }
        public byte tp_Minutes { get; }
        public byte tp_Seconds { get; }



        /// <summary>
        ///  Constructor with 3 parameters: hours, minutes and seconds
        /// </summary>
        public TimePeriod(byte h, byte m, byte s)
        {
            tp_Hours = h;
            tp_Minutes = m;
            tp_Seconds = s;

            innerTimePeriod = time_period_outer_to_inner(h, m, s);

        }
        /// <summary>
        ///  Constructor with 2 parameters: hours and minutes, default seconds value is 0
        /// </summary>
        public TimePeriod(byte h, byte m)
        {
            tp_Hours = h;
            tp_Minutes = m;
            tp_Seconds = 0;


            innerTimePeriod = time_period_outer_to_inner(h, m, 0);

        }
        /// <summary>
        ///  Constructor with 1 parameter: seconds
        /// </summary>
        public TimePeriod(long s)
        {

            tp_Hours = time_period_inner_to_outer(s)[0];
            tp_Minutes = time_period_inner_to_outer(s)[1];
            tp_Seconds = time_period_inner_to_outer(s)[2];
            innerTimePeriod = s;


        }

        /// <summary>
        ///  Constructor with 2 parameters: two structs Time
        /// </summary>
        public TimePeriod(Time t1, Time t2)
        {
            tp_Hours=0;
            tp_Minutes = 0;
            tp_Seconds = 0;
            innerTimePeriod = 0;


            if (t1 > t2)
            {

                var timeDiff = t1 - t2;

                tp_Hours = timeDiff.Hours;
                tp_Minutes = timeDiff.Minutes;
                tp_Seconds = timeDiff.Seconds;

                innerTimePeriod = time_period_outer_to_inner(tp_Hours, tp_Minutes, tp_Seconds);

            }
            else
            {
                var timeDiff = t2 - t1;

                tp_Hours = timeDiff.Hours;
                tp_Minutes = timeDiff.Minutes;
                tp_Seconds = timeDiff.Seconds;

                innerTimePeriod = time_period_outer_to_inner(tp_Hours, tp_Minutes, tp_Seconds);
            }

        }

        /// <summary>
        ///  Constructor with 1 parameter: full time in string, format 0:00:00
        /// </summary>
        public TimePeriod(string timeStr)
        {
            string[] timeArr = timeStr.Split(':');

            tp_Hours = Byte.Parse(timeArr[0]);
            tp_Minutes = Byte.Parse(timeArr[1]);
            tp_Seconds = Byte.Parse(timeArr[2]);


            innerTimePeriod = time_period_outer_to_inner(tp_Hours, tp_Minutes, tp_Seconds);

        }


        /// <summary>
        ///  functions converts timePeriod in seconds to hours:minutes:seconds
        /// </summary>
        static byte[] time_period_inner_to_outer(long inner)
        {
            byte[] arrayTime= {0,0,0};
            arrayTime[0] = ((byte)(inner / 3600));
            arrayTime[1] = ((byte)(inner % 3600 / 60));
            arrayTime[2] = ((byte)(inner % 3600 % 60));
       
            return arrayTime;
        }

        /// <summary>
        ///  functions converts timePeriod in  hours:minutes:seconds format to seconds
        /// </summary>
        static long time_period_outer_to_inner(byte tp_Hours, byte tp_Minutes, byte tp_Seconds)
        {
            return (tp_Hours * 3600)+(tp_Minutes * 60)+ tp_Seconds;
          
        }


        /// <summary>
        ///  overriding toString() function to format hh:mm:ss
        /// </summary>
        public override string ToString()
        {

            return $"{tp_Hours}:{tp_Minutes.ToString("D2")}:{tp_Seconds.ToString("D2")}";

        }


        /// <summary>
        ///  implementing IEquatable<Time> interface
        /// </summary>
        public override bool Equals(object obj)
        {

            if (obj is TimePeriod)
            {
                return this.Equals((TimePeriod)obj);
            }
            return false;
        }
        /// <summary>
        ///  implementing IEquatable<Time> interface
        /// </summary>
        public bool Equals(TimePeriod a)
        {

            return (tp_Hours == a.tp_Hours) && (tp_Minutes == a.tp_Minutes) && (tp_Seconds == a.tp_Seconds);
        }

        /// <summary>
        ///  overloading == operator
        /// </summary>
        public static bool operator ==(TimePeriod t1, TimePeriod t2)
        {
            return t1.Equals(t2);
        }
        /// <summary>
        ///  overloading != operator
        /// </summary>
        public static bool operator !=(TimePeriod t1, TimePeriod t2)
        {
            return !(t1.Equals(t2));
        }



        /// <summary>
        ///  implementing IComparable<Time> interface
        /// </summary>
        public int CompareTo(TimePeriod other)
        {
            if (other == null) return 1;

            var by_hour = this.tp_Hours - other.tp_Hours;
            if (by_hour != 0)
                return by_hour;

            var by_minute = this.tp_Minutes - other.tp_Minutes;
            if (by_minute != 0)
                return by_minute;

            return this.tp_Seconds - other.tp_Seconds;
        }

        /// <summary>
        ///  overloading > operator
        /// </summary>
        public static bool operator >(TimePeriod t1, TimePeriod t2)
        {
            return t1.CompareTo(t2) >= 1;
        }
        /// <summary>
        ///  overloading < operator
        /// </summary>
        public static bool operator <(TimePeriod t1, TimePeriod t2)
        {
            return t1.CompareTo(t2) <= -1;
        }
        /// <summary>
        ///  overloading >= operator
        /// </summary>
        public static bool operator >=(TimePeriod t1, TimePeriod t2)
        {
            return t1.CompareTo(t2) >= 0;
        }
        /// <summary>
        ///  overloading <= operator
        /// </summary>
        public static bool operator <=(TimePeriod t1, TimePeriod t2)
        {
            return t1.CompareTo(t2) <= 0;
        }


        /// <summary>
        ///  overloading + operator
        /// </summary>
        public static TimePeriod operator +(TimePeriod t1, TimePeriod t2)
        {

            var tempInner = t1.innerTimePeriod+ t2.innerTimePeriod;

            return new TimePeriod(tempInner);
        }
        /// <summary>
        ///  overloading - operator
        /// </summary>
        public static TimePeriod operator -(TimePeriod t1, TimePeriod t2)
        {
            if (t2 > t1)
            {
                throw new ArgumentOutOfRangeException();
            }
            var tempInner = t1.innerTimePeriod - t2.innerTimePeriod;

            return new TimePeriod(tempInner);
        }

        /// <summary>
        ///  providing function that adds TimePeriod to TimePeriod
        /// </summary>
        public TimePeriod Plus(TimePeriod tp)
        {

            var s = (tp.tp_Seconds + tp_Seconds) % 60;
            var m = (((tp.tp_Seconds + tp_Seconds) / 60) + (tp.tp_Minutes + tp_Minutes)) % 60;
            var h = (((tp.tp_Minutes + tp_Minutes) / 60) + (tp.tp_Hours + tp_Hours));


            return new TimePeriod((byte)h, (byte)m, (byte)s);

        }

        /// <summary>
        ///  providing static function that adds TimePeriod to TimePeriod
        /// </summary>
        public static TimePeriod Plus(TimePeriod tp1, TimePeriod tp2)
        {

            var s = (tp1.tp_Seconds + tp2.tp_Seconds) % 60;
            var m = (((tp1.tp_Seconds + tp2.tp_Seconds) / 60) + (tp1.tp_Minutes + tp2.tp_Minutes)) % 60;
            var h = (((tp1.tp_Minutes + tp2.tp_Minutes) / 60) + (tp1.tp_Hours + tp2.tp_Hours));


            return new TimePeriod((byte)h, (byte)m, (byte)s);

        }


    }
}
