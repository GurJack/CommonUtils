using System;
using System.Text;

namespace CommonUtils.Expressions
{
    internal static class NumberToWordsHelper
    {
        private static readonly string[] Hunds =
        {
            "", "сто ", "двести ", "триста ", "четыреста ",
            "пятьсот ", "шестьсот ", "семьсот ", "восемьсот ", "девятьсот "
        };

        private static readonly string[] Tens =
        {
            "", "десять ", "двадцать ", "тридцать ", "сорок ", "пятьдесят ",
            "шестьдесят ", "семьдесят ", "восемьдесят ", "девяносто "
        };

        internal static string Str(int val, bool male, string one, string two, string five)
        {
            string[] frac20 =
            {
                "", "один ", "два ", "три ", "четыре ", "пять ", "шесть ",
                "семь ", "восемь ", "девять ", "десять ", "одиннадцать ",
                "двенадцать ", "тринадцать ", "четырнадцать ", "пятнадцать ",
                "шестнадцать ", "семнадцать ", "восемнадцать ", "девятнадцать "
            };

            int num = val%1000;
            if (0 == num)
                return "";

            if (num < 0)
                throw new ArgumentOutOfRangeException("val", "Параметр не может быть отрицательным");

            if (!male)
            {
                frac20[1] = "одна ";
                frac20[2] = "две ";
            }

            StringBuilder r = new StringBuilder(Hunds[num/100]);

            if (num%100 < 20)
            {
                r.Append(frac20[num%100]);
            }
            else
            {
                r.Append(Tens[num%100/10]);
                r.Append(frac20[num%10]);
            }

            r.Append(Case(num, one, two, five));

            if (r.Length != 0 && r[r.Length - 1] != ' ')
                r.Append(" ");

            return r.ToString();
        }

        internal static string Case(int val, string one, string two, string five)
        {
            int t = (val%100 > 20) ? val%10 : val%20;

            switch (t)
            {
                case 1:
                    return one;
                case 2:
                case 3:
                case 4:
                    return two;
                default:
                    return five;
            }
        }

        private static readonly string[] Dek1 =
        {
            "", " одно", " двух", " трех", " четырех", " пяти", " шести", " семи", " восеми", " девяти", " десяти", " одиннадцати",
            " двенадцати", " тринадцати", " четырнадцати", " пятнадцати", " шестнадцати", " семнадцати", " восемнадцати", " девятнадцати"
        };

        private static readonly string[] Dek2 = {"", "", " двадцати", " тридцати", " сорока", " пятидесяти", " шестидесяти", " семидесяти", " восьмидесяти", " девяноста"};
        private static readonly string[] Dek3 = {"", " ста", " двухсот", " трехсот", " четырехсот", " пятисот", " шестисот", " семисот", " восьмисот", " девятисот"};
        private static readonly string[] Th = {"", "", " тысяч", " миллиона", " миллиарда", " триллиона", " квадрилиона", " квинтилиона"};

        internal static string NumPhrase(int number, bool male)
        {
            if (number == 0)
                return "нуля";

            string str = "";
            for (byte th = 1; number > 0; th++)
            {
                short gr = (short) (number % 1000);
                number = (number - gr)/1000;
                if (gr > 0)
                {
                    byte d3 = (byte) ((gr - gr%100)/100);
                    byte d1 = (byte) (gr%10);
                    byte d2 = (byte) ((gr - d3*100 - d1)/10);
                    if (d2 == 1)
                        d1 += (byte) 10;
                    bool ismale = (th > 2) || (th == 1 && male);
                    str = Dek3[d3] + Dek2[d2] + Dek1[d1] + EndDek1(d1, ismale) + Th[th] + EndTh(th, d1) + str;
                }
            }
            return str;
        }

        private static string EndDek1(byte Dek, bool male)
        {
            if (Dek == 1)
                return male ? "го" : "й";

            return "";
        }

        private static string EndTh(byte ThNum, byte Dek)
        {
            if (ThNum == 2 && Dek == 1)
                // одной тысячИ
                return "и";
            else if (ThNum > 2 && Dek > 1)
                // двух миллионОВ, миллиардОВ
                return "ов";
            else
                return "";
        }
    }
}