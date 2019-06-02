using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MessageTester
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Program.SendMessage();
            Console.Read();
        }
        public static void SendMessage()
        {
            using (HttpClient client = new HttpClient())
            {
                MessageParameter param = new MessageParameter();
                param.PhoneNumber = "08177585827";
                param.Message = "This you Sharabyte OTP - ";
                HttpRequestMessage request = new HttpRequestMessage();
                Uri uri = new Uri(param.ToString());
                request.RequestUri = uri;
                request.Method = HttpMethod.Get;

                Task<HttpResponseMessage> rsp =  client.SendAsync(request);
                Console.WriteLine("Status Code " + rsp.Result.StatusCode);

            }
        }
    }

    public class MessageParameter
    {
        public string Url = "http://www.jweb2sms.com.ng/components/com_spc/smsapi.php";
        public string UserName = "soladejo18@yahoo.co.uk";
        public string Password = "sola123";
        public string Message { set; get; }
        public string PhoneNumber { set; get; }
        public string Sender = "sharabyte";

        public override string ToString()
        {
            string EndPoint = $"{this.Url}?username={this.UserName}&password={this.Password}&message={this.Message}&sender={this.Sender}&recipient={this.PhoneNumber}";
            return EndPoint;
        }
    }
}
