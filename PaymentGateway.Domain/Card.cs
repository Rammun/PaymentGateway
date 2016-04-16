using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Domain
{
    public class Card
    {
        string card_number;
        byte expiry_month;
        short expiry_year;
        string cvv;
        string cardholder_name;
        decimal? cash_limit;

        public Card(string card_number,
                    byte expiry_month,
                    short expiry_year,
                    string cvv,
                    string cardholder_name = "",
                    decimal? cash_limit = null)
        {
            this.Card_number = card_number;
            this.Expiry_month = expiry_month;
            this.Expiry_year = expiry_year;
            this.Cvv = cvv;
            this.Cardholder_name = cardholder_name;
            this.Cash_limit = cash_limit;
        }

        public string Card_number
        {
            get { return card_number; }
            set
            {
                if (value.Length != 16 || !IsNumbers(value) || !IsLunaVerify(value))
                    throw new Exception(string.Format("Неверный номер карты {0}", value));
                card_number = value;
            }
        }
        
        public byte Expiry_month
        {
            get { return expiry_month; }
            set
            {
                if(value < 1 || value > 12)
                    throw new Exception(string.Format("Несуществующий номер месяца {0}", value));
                expiry_month = value;
            }
        }

        public short Expiry_year
        {
            get { return expiry_year; }
            set
            {
                var year = (short)DateTime.Now.Year;
                if (value < year)
                    throw new Exception(string.Format("Год истечения срока не может быть ниже {0}", year));
                expiry_year = value;
            }
        }

        public string Cvv
        {
            get { return cvv; }
            set
            {
                if (cvv == null || cvv.Length != 3 || !IsNumbers(value))
                    throw new Exception(string.Format("Недопустимое значение cvv {0}", cvv));
                cvv = value;
            }
        }

        public string Cardholder_name
        {
            get { return cardholder_name; }
            set
            {
                if(value == null || value.Length > 50)
                    throw new Exception(string.Format("Недопустимое имя или длина имени владельца карты {0}", value));
                cardholder_name = value;
            }
        }

        public decimal? Cash_limit
        { 
            get { return cash_limit; }
            set { cash_limit = value; } 
        }

        private bool IsNumbers(string value)
        {
            return value.All(v => "0123456789".Contains(v));
        }

        private bool IsLunaVerify(string value)
        {
            try
            {
                return value.Select(v => int.Parse(v.ToString()))
                            .Select((n,i) => i%2 != 0? n : (n *= 2 > 9? n*2 - 9 : n*= 2))
                            .Sum() % 10 == 0;
            }
            catch
            {
                throw new Exception("Невозможно преобразовать последовательность к int");
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Card c = obj as Card;
            if ((System.Object)c == null)
                return false;

            return IsIdenticalCard(this, c);
        }

        public bool Equals(Card c)
        {
            if ((object)c == null)
                return false;

            return IsIdenticalCard(this, c);
        }

        public override int GetHashCode()
        {
            return Card_number.GetHashCode() ^ Cvv.GetHashCode();
        }

        public static bool operator ==(Card a, Card b)
        {
            return IsIdenticalCard(a, b);
        }

        public static bool operator !=(Card a, Card b)
        {
            return a.Card_number != b.Card_number ||
                   a.Cvv != b.Cvv ||
                   a.Expiry_month != b.Expiry_month ||
                   a.Expiry_year != b.Expiry_year ||
                   a.Cardholder_name != b.Cardholder_name;
        }

        private static bool IsIdenticalCard(Card a, Card b)
        {
            return a.Card_number == b.Card_number &&
                   a.Cvv == b.Cvv &&
                   a.Expiry_month == b.Expiry_month &&
                   a.Expiry_year == b.Expiry_year &&
                   a.Cardholder_name == b.Cardholder_name;
        }

    }
}