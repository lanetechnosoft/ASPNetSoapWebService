using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace EDLService
{
    /// <summary>
    /// Summary description for EDLService
    /// </summary>
    [WebService(Namespace = "http://jdb.local.net/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class EDLService : System.Web.Services.WebService
    {

        [WebMethod]
        public CustomerInfo CustomerQuery(int ACCOUNT_NO)
        {
            return new CustomerInfo().GetCustomerInfo(ACCOUNT_NO);
        }

        [WebMethod]
        public PaymentInfo CustomerPayment(int ACCOUNT_NO,double PAYMENT_AMT)
        {
            return new PaymentInfo().Payment(ACCOUNT_NO,PAYMENT_AMT);
        }

        [WebMethod]
        public ReverseInfo ReversePayment(int ACCOUNT_NO, double PAYMENT_AMT, int RECEIPT_NO, int PAYMENT_ID)
        {
            return new ReverseInfo().revert(ACCOUNT_NO, PAYMENT_AMT, RECEIPT_NO, PAYMENT_ID);
        }

    }
}
