using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.FinTech.Domain.ViewModels
{
  public class Log
  {
    public long start_time { get; set; }
    public long time_spent { get; set; }
    public long attempts { get; set; }
    public string authentication { get; set; }
    public long errors { get; set; }
    public bool success { get; set; }
    public bool mobile { get; set; }
    public List<object> input { get; set; }
    public List<History> history { get; set; }
  }

  public class History
  {
    public string type { get; set; }
    public string message { get; set; }
    public long time { get; set; }
  }

  public class Metadata
  {
    public string referrer { get; set; }
  }

  public class Customer
  {
    public long id { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string email { get; set; }
    public string customer_code { get; set; }
    public string phone { get; set; }
    public object metadata { get; set; }
    public string risk_action { get; set; }
    public object longernational_format_phone { get; set; }
  }

  public class Data
  {
    public long id { get; set; }
    public string domain { get; set; }
    public string status { get; set; }
    public string reference { get; set; }
    public object receipt_number { get; set; }
    public long amount { get; set; }
    public string message { get; set; }
    public string gateway_response { get; set; }
    public DateTime paid_at { get; set; }
    public DateTime created_at { get; set; }
    public string channel { get; set; }
    public string currency { get; set; }
    public string ip_address { get; set; }
    public Metadata metadata { get; set; }
    public Log log { get; set; }
    public long fees { get; set; }
    public object fees_split { get; set; }
    public Authorization authorization { get; set; }
    public Customer customer { get; set; }
    public object plan { get; set; }
    public object split { get; set; }
    public object order_id { get; set; }
    public DateTime paidAt { get; set; }
    public DateTime createdAt { get; set; }
    public long requested_amount { get; set; }
    public object pos_transaction_data { get; set; }
    public object source { get; set; }
    public object fees_breakdown { get; set; }
    public DateTime transaction_date { get; set; }
    public object plan_object { get; set; }
    public object subaccount { get; set; }
  }

  public class PaystackVerification
  {
    public bool status { get; set; }
    public string message { get; set; }
    public Data data { get; set; }
  }
}
