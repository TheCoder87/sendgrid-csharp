using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SendGrid.Resources.MarketingCampaigns.Contacts
{
    public class Lists
    {
        private string _endpoint;
        private Client _client;

        /// <summary>
        /// Constructs the SendGrid Suppressions object.
        /// See https://sendgrid.com/docs/API_Reference/Web_API_v3/Marketing_Campaigns/contactdb.html
        /// </summary>
        /// <param name="client">SendGrid Web API v3 client</param>
        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        /// 
        /// https://api.sendgrid.com/v3/contactdb/lists HTTP/1.1
        public Lists(Client client, string endpoint = "v3/contactdb/lists")
        {
            _endpoint = endpoint;
            _client = client;
        }

        /// <summary>
        /// Get suppressed addresses for a given group.
        /// </summary>
        /// <param name="groupId">ID of the suppression group</param>
        /// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/Suppression_Management/suppressions.html</returns>
        public async Task<HttpResponseMessage> Get()
        {
            return await _client.Get(_endpoint);
        }

        /// <summary>
        /// Add recipient addresses to the suppressions list for a given group.
        /// 
        /// If the group has been deleted, this request will add the address to the global suppression.
        /// </summary>
        /// <param name="groupId">ID of the suppression group</param>
        /// <param name="recipient_emails">Array of email addresses to add to the suppression group</param>
        /// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/Suppression_Management/suppressions.html</returns>
        public async Task<HttpResponseMessage> Post(int groupId, string[] emails)
        {
            JArray receipient_emails = new JArray();
            foreach (string email in emails) { receipient_emails.Add(email); }
            var data = new JObject(new JProperty("recipient_emails", receipient_emails));
            return await _client.Post(_endpoint + "/" + groupId.ToString() + "/suppressions", data);
        }

        /// <summary>
        /// Delete a suppression group.
        /// </summary>
        /// <param name="groupId">ID of the suppression group to delete</param>
        /// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/Suppression_Management/suppressions.html</returns>
        public async Task<HttpResponseMessage> Delete(int groupId, string email)
        {
            return await _client.Delete(_endpoint + "/" + groupId.ToString() + "/suppressions/" + email);
        }
    }
}
