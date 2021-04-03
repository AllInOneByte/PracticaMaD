using System;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Model;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
    public partial class ListCreditCards : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<CreditCard> cards = SessionManager.FindAllCredritCards(Context);

                TableRow row;

                TableCell numberCell;
                TableCell type;
                TableCell expiration;
                TableCell verification;
                TableCell fav;

                HyperLink number;

                CheckBox checkDefault;
            
                foreach (CreditCard card in cards)
                {
                    row = new TableRow();

                    numberCell = new TableCell();
                    number = new HyperLink();
                    number.ID = "cardId";
                    number.Text = card.cardNumber.ToString();
                    number.NavigateUrl = "~/Pages/User/UpdateCreditCard.aspx?card=" + card.cardId.ToString();
                    numberCell.Controls.Add(number);
                    row.Controls.Add(numberCell);

                    type = new TableCell();
                    type.Text = card.cardType;
                    row.Cells.Add(type);

                    expiration = new TableCell();
                    expiration.Text = card.expirationDate.ToString("MM/yy");
                    row.Cells.Add(expiration);

                    verification = new TableCell();
                    verification.Text = card.verificationCode.ToString();
                    row.Cells.Add(verification);

                    fav = new TableCell();
                    checkDefault = new CheckBox();
                    checkDefault.ID = "ChkSelect";
                    checkDefault.Enabled = false;
                    if (card.defaultCard == 1)
                    {
                        checkDefault.Checked = true;
                    } else
                    {
                        checkDefault.Checked = false;
                    }
                    fav.Controls.Add(checkDefault);
                    row.Cells.Add(fav);

                    lclTableCreditCards.Rows.Add(row);
                }

                
            }
            
        }

    }
}