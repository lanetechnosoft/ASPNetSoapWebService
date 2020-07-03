using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDLService
{
    public class CustomerInfo
    {
        public bool MESSAGE_STAT { get; set; }
        public string ERROR_CODE { get; set; }
        public string ERROR_DESC { get; set; }
        public string CUSTOMER_NO { get; set; }
        public string ACCOUNT_NO { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string CUSTOEMR_TYPE { get; set; }
        public string COMPANY_NAME { get; set; }
        public string OLD_ACC_NO { get; set; }
        public string ACC_STAT { get; set; }
        public string ADDRESS { get; set; }
        public string BRANCH_CODE { get; set; }
        public string BRANCH_DESC_LA { get; set; }
        public string BRANCH_DESC_EN { get; set; }
        public string DISTRICT_CODE { get; set; }
        private string DISTRICT_DESC_LA { get; set; }
        public string DISTRICT_DESC_EN { get; set; }
        public string OUTSTANDING_BAL { get; set; }

        

        public CustomerInfo()
        {
        }

        public CustomerInfo GetCustomerInfo(int account_no) {
                CustomerInfo customer = null; 
            try
            {
                                    
                string sql = "SELECT * FROM VW_EDL_CUST_INFO WHERE 1=1 AND ACCOUNT_NO = TRIM("+ account_no + ")";
                OracleDataReader dr = Dbconfig.ExecuteReader(sql);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        
                        customer = new CustomerInfo();
                        customer.MESSAGE_STAT = true;
                        customer.ERROR_CODE = "00";
                        customer.ERROR_DESC = "Query Customer Successfully";
                        customer.CUSTOMER_NO = dr.GetInt32(0).ToString("D8");
                        customer.ACCOUNT_NO = dr.GetInt32(1).ToString("D8");
                        customer.FIRST_NAME = dr.IsDBNull(2) ? "N/A" : dr.GetString(2);
                        customer.LAST_NAME = dr.IsDBNull(3) ? "N/A" : dr.GetString(3);
                        customer.CUSTOEMR_TYPE = dr.GetString(4);
                        customer.COMPANY_NAME = dr.IsDBNull(5) ? "N/A" : dr.GetString(5);
                        customer.OLD_ACC_NO = dr.GetString(6);
                        customer.ACC_STAT = dr.GetString(7);
                        customer.ADDRESS = dr.IsDBNull(8) ? "N/A" : dr.GetString(8);
                        customer.BRANCH_CODE = dr.GetString(9);
                        customer.BRANCH_DESC_LA = dr.GetString(10);
                        customer.BRANCH_DESC_EN = dr.GetString(11);
                        customer.DISTRICT_CODE = dr.GetString(12);
                        customer.DISTRICT_DESC_LA = dr.GetString(13);
                        customer.DISTRICT_DESC_EN = dr.GetString(14);
                        customer.OUTSTANDING_BAL = string.Format("{0:n}", dr.GetDouble(15).ToString());
                    }
                    dr.Dispose();
                }
                else {
                    customer = new CustomerInfo();
                    customer.MESSAGE_STAT =false;
                    customer.ERROR_CODE = "01";
                    customer.ERROR_DESC = "Account Dose Not Exist !";
                }
                
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Show Error " + ex.Message);
                //throw;
                customer = new CustomerInfo();
                customer.MESSAGE_STAT = false;
                customer.ERROR_CODE = "99";
                customer.ERROR_DESC = "SYSTEM ERROR "+ex.Message;
            }


            return customer;
        }
    }
}