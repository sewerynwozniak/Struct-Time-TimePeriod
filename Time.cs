using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Struktury
{

    /// <summary>
    ///  This struct provides time between 00:00:00 - 23:59:59
    /// </summary>
    public struct Time : IEquatable<Time>, IComparable<Time>
    {

        public byte Hours { get; }
        public byte Minutes { get; }
        public byte Seconds { get; }

        /// <summary>
        ///  Constructor with 3 parameters: hours, minutes and seconds
        /// </summary>
        public Time( byte h, byte m, byte s)
        {
            Hours = h;
            Minutes = m;
            Seconds = s;


            checkRange(h,m,s);

        }


        /// <summary>
        ///  Constructor with 2 parameters: hours and minutes, default seconds value is 0
        /// </summary>
        public Time(byte h, byte m )
        {
            Hours = h;
            Minutes = m;
            Seconds = 0;


            checkRange(h, m);

        }

        /// <summary>
        ///  Constructor with 1 parameter: hour, minutes and seconds have default  value  0
        /// </summary>
        public Time(byte h)
        {
            Hours = h;
            Minutes = 0;
            Seconds = 0;


            checkRange(h);

        }


        /// <summary>
        ///  Constructor with 1 parameter: full time in string, format 00:00:00
        /// </summary>
        public Time(string timeStr)
        {
            string[] timeArr = timeStr.Split(':');

            Hours = Byte.Parse(timeArr[0]);
            Minutes = Byte.Parse(timeArr[1]);
            Seconds = Byte.Parse(timeArr[2]);


            checkRange(Hours, Minutes, Seconds);



        }

        /// <summary>
        ///  function check if hours, minutes and seconds don't exceed its values, if so, ArgumentOutOfRangeException is provided
        /// </summary>
        void checkRange(byte h, byte m=1, byte s=1)
        {
            if (h < 0 || m < 0 || s < 0)
            {
                throw new ArgumentOutOfRangeException("value have to be greater than 0");
            }
            if (h >= 24 || m >= 60 || s >= 60)
            {
                throw new ArgumentOutOfRangeException("");
            }
        }


        /// <summary>
        ///  overriding toString() function to format hh:mm:ss
        /// </summary>
        public override string ToString()
        {

            return $"{Hours.ToString("D2")}:{Minutes.ToString("D2")}:{Seconds.ToString("D2")}";

        }
        /// <summary>
        ///  implementing IEquatable<Time> interface
        /// </summary>
        public override bool Equals(object obj)
        {

            if (obj is Time)
            {
                return this.Equals((Time)obj);
            }
            return false;
        }
        /// <summary>
        ///  implementing IEquatable<Time> interface
        /// </summary>
        public bool Equals(Time a)
        {

            return (Hours == a.Hours) && (Minutes == a.Minutes) && (Seconds == a.Seconds);
        }

        /// <summary>
        ///  overloading == operator
        /// </summary>
        public static bool operator ==(Time t1, Time t2)
        {
            return t1.Equals(t2);
        }
        /// <summary>
        ///  overloading != operator
        /// </summary>
        public static bool operator !=(Time t1, Time t2)
        {
            return !(t1.Equals(t2));
        }


        /// <summary>
        ///  implementing IComparable<Time> interface
        /// </summary>
        public int CompareTo(Time other)
        {
            if (other == null) return 1;

            var by_hour = this.Hours - other.Hours;
            if (by_hour != 0)
                return by_hour;

            var by_minute = this.Minutes - other.Minutes;
            if (by_minute != 0)
                return by_minute;

            return this.Seconds - other.Seconds;
        }

        /// <summary>
        ///  overloading > operator
        /// </summary>
        public static bool operator >(Time t1, Time t2)
        {
            return t1.CompareTo(t2) >= 1;
        }
        /// <summary>
        ///  overloading < operator
        /// </summary>
        public static bool operator <(Time t1, Time t2)
        {
            return t1.CompareTo(t2) <= -1;
        }
        /// <summary>
        ///  overloading >= operator
        /// </summary>
        public static bool operator >=(Time t1, Time t2)
        {
            return t1.CompareTo(t2) >= 0;
        }
        /// <summary>
        ///  overloading <= operator
        /// </summary>
        public static bool operator <=(Time t1, Time t2)
        {
            return t1.CompareTo(t2) <= 0;
        }


        /// <summary>
        ///  overloading + operator
        /// </summary>
        public static Time operator +(Time t1, Time t2)
        {


           var s = (t1.Seconds + t2.Seconds)%60;
           var m = (((t1.Seconds + t2.Seconds) / 60) + (t1.Minutes + t2.Minutes)) % 60;
           var h = (((t1.Minutes + t2.Minutes) / 60) + (t1.Hours + t2.Hours)) % 24;


            return  new Time((byte)h,(byte)m,(byte)s);
        }

        /// <summary>
        ///  overloading - operator
        /// </summary>
        public static Time operator -(Time t1, Time t2)
        {

            var s = t1.Seconds - t2.Seconds;
            if (s < 0)
            {
                s = 60 + s;
            }
            var restS = 0;
            if(t1.Seconds - t2.Seconds < 0)
            {
                restS = 1;
            }
            var m = (t1.Minutes - restS) - t2.Minutes;
            if(m < 0)
            {
                m = 60 + m;
            }
            var restM = 0;
            if (t1.Minutes - t2.Minutes < 0 || restS > 0) 
            {
                restM = 1;
            }
            var h = (t1.Hours - restM) - t2.Hours;
            if (h < 0)
            {
                h = 24 + h;
            }



            return new Time((byte)h, (byte)m, (byte)s);
        }


        /// <summary>
        ///  providing function that adds TimePeriod to Time
        /// </summary>
        public Time Plus(TimePeriod tp)
        {

            var s = (tp.tp_Seconds + Seconds) % 60;
            var m = (((tp.tp_Seconds + Seconds) / 60) + (tp.tp_Minutes + Minutes)) % 60;
            var h = (((tp.tp_Minutes + Minutes) / 60) + (tp.tp_Hours + Hours)) % 24;


            return new Time((byte)h, (byte)m, (byte)s);


        }

        /// <summary>
        ///  providing static function that adds TimePeriod to Time
        /// </summary>
        public static Time Plus(Time t, TimePeriod tp)
        {

            var s = (tp.tp_Seconds + t.Seconds) % 60;
            var m = (((tp.tp_Seconds + t.Seconds) / 60) + (tp.tp_Minutes + t.Minutes)) % 60;
            var h = (((tp.tp_Minutes + t.Minutes) / 60) + (tp.tp_Hours + t.Hours)) % 24;


            return new Time((byte)h, (byte)m, (byte)s);

        }


    }
}
