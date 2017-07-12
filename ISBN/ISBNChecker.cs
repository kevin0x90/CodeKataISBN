using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISBN
{
    public class ISBNChecker
    {
        private const int ISBN_13_LENGTH = 13;

        public bool IsValidISBN(string isbn)
        {
            if (ContainsMixedSeperators(isbn)) return false;

            string normalizedISBN = NormalizeISBN(isbn);

            if (!ISBNHasValidLength(normalizedISBN)) return false;

            var weightedSum = 0;
            int checkSum = -1;

            for (int i = 0; i < ISBN_13_LENGTH; i++)
            {
                if (!int.TryParse(normalizedISBN[i].ToString(), out int parsedDigit))
                {
                    return false;
                }

                if (IsCheckSumDigit(i))
                {
                    checkSum = parsedDigit;
                }
                else if (IsEven(i))
                {
                    weightedSum += parsedDigit * 3;
                }
                else
                {
                    weightedSum += parsedDigit * 1;
                }
            }

            var calculatedChecksum = GetCalculatedChecksum(weightedSum);

            return calculatedChecksum == checkSum;
        }

        private static bool ContainsMixedSeperators(string isbn)
        {
            return isbn.Contains(" ") && isbn.Contains("-");
        }

        private static bool IsEven(int i)
        {
            return (i + 1) % 2 == 0;
        }

        private static bool IsCheckSumDigit(int i)
        {
            return i == ISBN_13_LENGTH - 1;
        }

        private static int GetCalculatedChecksum(int weightedSum)
        {
            return (10 - (weightedSum % 10)) % 10;
        }

        private static bool ISBNHasValidLength(string normalizedISBN)
        {
            return normalizedISBN.Length == ISBN_13_LENGTH;
        }

        private static string NormalizeISBN(string isbn)
        {
            return isbn.Replace(" ", string.Empty).Replace("-", string.Empty);
        }
    }
}
