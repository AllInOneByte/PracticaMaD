using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects
{
    public class CreditType
    {

        /* 
         * In a more realistic application, these values could be read from a 
         * database in the "static" constructor.
         */
        private static readonly ArrayList creditType_es = new ArrayList();
        private static readonly ArrayList creditType_en = new ArrayList();
        private static readonly ArrayList creditType_gl = new ArrayList();
        private static readonly Hashtable creditType = new Hashtable();


        /* Access modifiers are not allowed on static constructors
         * so if we want to prevent that anybody creates instances 
         * of this class we must do the following ...
         */
        private CreditType() { }

        static CreditType()
        {
            #region set the countries

            creditType_es.Add(new ListItem("Crédito", "Crédito"));
            creditType_es.Add(new ListItem("Débito", "Débito"));

            creditType_en.Add(new ListItem("Credit", "Credit"));
            creditType_en.Add(new ListItem("Debit", "Debit"));

            creditType_gl.Add(new ListItem("Crédito", "Crédito"));
            creditType_gl.Add(new ListItem("Débito", "Débito"));

            creditType.Add("es", creditType_es);
            creditType.Add("en", creditType_en);
            creditType.Add("gl", creditType_gl);

            #endregion

        }

        public static ICollection GetCreditTypeCodes()
        {
            return creditType.Keys;
        }

        public static ArrayList GetCreditType(String languageCode)
        {
            ArrayList lang = (ArrayList)creditType[languageCode];

            if (lang != null)
            {
                return lang;
            }
            else
            {
                return (ArrayList)creditType["en"];
            }
        }
    }
}