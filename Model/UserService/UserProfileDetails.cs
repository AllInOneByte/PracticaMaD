using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService
{
    public class UserProfileDetails
    {
        #region Properties Region
        
        public string firstName { get; private set; }

        public string lastName { get; private set; }

        public string email { get; private set; }

        public string language { get; private set; }

        public string country { get; private set; }

        public byte role { get; private set; }

        public string address { get; private set; }

        #endregion
        public UserProfileDetails(string firstName, string lastName, string email, string language, string country, byte role, string address)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.language = language;
            this.country = country;
            this.role = role;
            this.address = address;
        }

        public override bool Equals(object obj)
        { 
           UserProfileDetails target = (UserProfileDetails)obj;

                return (this.firstName == target.firstName)
                      && (this.lastName == target.lastName)
                      && (this.email == target.email)
                      && (this.language == target.language)
                      && (this.country == target.country)
                      && (this.role == target.role)
                      && (this.address == target.address);
        }

            // The GetHashCode method is used in hashing algorithms and data 
            // structures such as a hash table. In order to ensure that it works 
            // properly, we suppose that the FirstName does not change.        
            public override int GetHashCode()
            {
                return this.firstName.GetHashCode();
            }

            /// <summary>
            /// Returns a <see cref="T:System.String"></see> that represents the 
            /// current <see cref="T:System.Object"></see>.
            public override String ToString()
            {
                String strUserProfileDetails;

                strUserProfileDetails =
                    "[ firstName = " + firstName + " | " +
                    "lastName = " + lastName + " | " +
                    "email = " + email + " | " +
                    "language = " + language + " | " +
                    "country = " + country + " |" +
                    "role = " + role + "|" +
                    "address = " + address + " ]";


                return strUserProfileDetails;
            }
    }
}
