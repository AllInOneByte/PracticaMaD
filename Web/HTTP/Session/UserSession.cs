using System;

namespace Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session
{
    public class UserSession
    {

        private long userProfileId;
        private String firstName;
        private byte rol;

        private long cardDefaultId;
        private long cardDefaultNumber;

        public long UserProfileId
        {
            get { return userProfileId; }
            set { userProfileId = value; }
        }

        public String FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public byte Rol
        {
            get { return rol;  }
            set { rol = value; }
        }

        public long CardDefaultId
        {
            get { return cardDefaultId; }
            set { cardDefaultId = value; }
        }

        public long CardDefaultNumber
        {
            get { return cardDefaultNumber; }
            set { cardDefaultNumber = value; }
        }
    }
}


