namespace Es.Udc.DotNet.PracticaMaD.Model.UserService
{
    public class CreditCardDetails
    {

        #region Properties Region

        public String CardType { get; private set; }

        public int cardNumber { get; private set; }

        public int VerificationCode { get; private set; }

        public date ExpirationDate { get; private set; }

        public int DefaultCard { get; private set; }

        public long userId { get; private set; }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="CreditCardDetails"/>
        /// class.
        /// </summary>
        /// <param name="cardType">The card's type.</param>
        /// <param name="cardNumber">The card's number.</param>
        /// <param name="VerificationCode">The card's verification code.</param>
        /// <param name="ExpirationDate">The card's ExpirationDate.</param>
        /// <param name="DefaultCard">The user's default card.</param>
        /// <param name="userId">The user's kid</param>
        public CreditCardDetails(String cardType, int cardNumber,
            int verificationCode, date expirationDate, int defaultCard, long userId)
        {
            this.CardType = cardType;
            this.cardNumber = cardNumber;
            this.VerificationCode = verificationCode;
            this.ExpirationDate = expirationDate;
            this.DefaultCard = defaultCard;
            this.userId = userId;
        }

        public override bool Equals(object obj)
        {

            CreditCardDetails target = (CreditCardDetails)obj;

            return (this.CardType == cardType)
                && (this.cardNumber == cardNumber)
                && (this.VerificationCode == verificationCode)
                && (this.ExpirationDate == expirationDate)
                && (this.DefaultCard == defaultCard)
                && (this.userId == userId);
        }

        // The GetHashCode method is used in hashing algorithms and data 
        // structures such as a hash table. In order to ensure that it works 
        // properly, we suppose that the FirstName does not change.        
        public override int GetHashCode()
        {
            return this.cardNumber.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the 
        /// current <see cref="T:System.Object"></see>.
        public override String ToString()
        {
            String strCreditCardDetails;

            strCreditCardDetails =
                "[ cardType = " + CardType + " | " +
                "cardNumber = " + cardNumber + " | " +
                "verificationCode = " + VerificationCode + " | " +
                "expirationDate = " + ExpirationDate + " | " +
                "defaultCard = " + DefaultCard + " |" +
                "userId = "`+ userId + " ]";

            return strCreditCardDetails;
        }
}
  