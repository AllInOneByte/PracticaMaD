using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService.Exceptions
{
    /// <summary>
    /// This exception is raised if the card provided doesn't belong to the user.
    /// </summary>
    public class UnmatchingUserAndCardException : Exception
    {
        private long UserIdentifier { get; }
        private long CardIdentifier { get; }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="UnmatchingUserAndCardException"/> class.
        /// </summary>
        /// <param name="userIdentifier"> The user identifier. </param>
        /// <param name="cardIdentifier"> The credit card identifier. </param>
        public UnmatchingUserAndCardException(long userIdentifier, long cardIdentifier)
            : base("Unmatching user and card exception => " +
                  "UserIdentifier = " + userIdentifier + " | " +
                  "CardIdentifier = " + cardIdentifier)
        {
            UserIdentifier = userIdentifier;
            CardIdentifier = cardIdentifier;
        }
    }
}
