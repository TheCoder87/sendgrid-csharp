using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SendGrid.Resources.MarketingCampaigns.Contacts
{
    public class Recipients
    {
        private string _endpoint;
        private SendGrid.Client _client;

        /// <summary>
        /// Constructs the SendGrid Suppressions object.
        /// See https://sendgrid.com/docs/API_Reference/Web_API_v3/Marketing_Campaigns/contactdb.html
        /// </summary>
        /// <param name="client">SendGrid Web API v3 client</param>
        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        /// 
        /// https://api.sendgrid.com/v3/contactdb/recipients?page_size=100&page=1 HTTP/1.1
        public Recipients(Client client, string endpoint = "v3/contactdb/recipients")
        {
            _endpoint = endpoint;
            _client = client;
        }

     
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> Get(int pageSize=100,int page=1)
        {
            HttpResponseMessage zz = await _client.Get(_endpoint + "?" + "page_size=" + pageSize + "&page=" + page);

            // reason: "TOO MANY REQUESTS" status 429

            return zz;
        }

       
        public async Task<HttpResponseMessage> Post(List<RecipientData> recipients)
        {
            JArray jArray = new JArray();
            foreach (var r in recipients)
            {
                var jsonObject = JObject.FromObject(r);
                jArray.Add(jsonObject);
            }
            return await _client.Post(_endpoint, jArray);
        }

        public async Task<HttpResponseMessage> Patch(List<RecipientData> recipients)
        {
            JArray jArray = new JArray();
            foreach (var r in recipients)
            {
                var jsonObject = JObject.FromObject(r);
                jArray.Add(jsonObject);
            }
            return await _client.Patch(_endpoint, jArray);
        }

        public async Task<HttpResponseMessage> Delete(string recipientId)
        {
            return await _client.Delete(_endpoint + "/" + recipientId);
        }

        //public Task<HttpResponseMessage> Delete(List<string> recipientIds)
        //{
        //    JArray jArray = new JArray(recipientIds.Select(s => JToken.FromObject(s)));
        //    return await _client.Delete(_endpoint, jArray);
        //}
    }

    public class RecipientData
    {
        public string email { get; set; }
        public string last_name { get; set; }
        public string first_name { get; set; }
        public string customer_id { get; set; }
        public string SqlCriteriaArray { get; set; }
        public bool Criteria1 { get; set; }
        public bool Criteria2 { get; set; }
        public bool Criteria3 { get; set; }
        public bool Criteria4 { get; set; }
        public bool Criteria5 { get; set; }

    }
}

//[
//  {
//    "email": "jones@example.com",
//    "last_name": "Jones",
//    "pet": "Indiana",
//    "age": 25
//  },
//  {
//    "email": "miller@example.com",
//    "last_name": "Miller",
//    "pet": "FrouFrou",
//    "age": 32
//  },
//  {
//    "email": "invalid_email",
//    "last_name": "Smith",
//    "pet": "Spot",
//    "age": 17
//  }
//]
