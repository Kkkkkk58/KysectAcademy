using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7
{
    class Date
    {
        public int day;
        public int month;
        public int year;

        public Date(int d, int m, int y)
        {
            day = d-1;
            month = m;
            year = y;

            AddOneDay();
        }

        public Date(Date date)
        {
            day = date.day;
            month = date.month;
            year = date.year;
        }

        public void AddOneDay()
        {
            day++;
            if(day >= 32)
            { day = 1; month++; }

            if(month >= 13)
            { month = 1; year++; }

            if(day == 31 && (month == 4 || month == 6 || month == 9 || month == 11))
            { day = 1; month++; }

            if(day == 30 && month == 2)
            { day = 1; month++; }

            if(day == 29 && month == 2 && (year % 4) != 0)
            { day = 1; month++; }
        }


        public static bool operator >(Date D1, Date D2)
        {
            if (D1.year == D2.year)
            {
                if (D1.month == D2.month)
                    return D1.day > D2.day;
                else
                    return D1.month > D2.month;
            }
            else
                return D1.year > D2.year;
        }

        public static bool operator <(Date D1, Date D2)
        {
            if (D1.year == D2.year)
            {
                if (D1.month == D2.month)
                    return D1.day < D2.day;
                else
                    return D1.month < D2.month;
            }
            else
                return D1.year < D2.year;
        }

        public void Show()
        {
            Console.WriteLine(day + "." + month + "." + year);
        }
    }
}
