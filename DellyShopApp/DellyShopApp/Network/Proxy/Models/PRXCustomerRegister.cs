using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DellyShopApp.Network.Proxy.Models
{
    public class PRXRequestCustomerRegister
    {
        [JsonProperty("customerName")]
        public string CustomerName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("street1")]
        public string Street1 { get; set; }

        [JsonProperty("street2")]
        public string Street2 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("province")]
        public string Province { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("contacts")]
        public IList<PRXCustomerRegisterContact> Contacts { get; set; }
    }

    public class PRXCustomerRegisterContact
    {
        [JsonProperty("jobTitle")]
        public string JobTitle { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("middleName")]
        public string MiddleName { get; set; }

        [JsonProperty("nickName")]
        public string NickName { get; set; }

        [JsonProperty("gender")]
        public int Gender { get; set; }

        [JsonProperty("salutation")]
        public int Salutation { get; set; }

        [JsonProperty("mobilePhone")]
        public string MobilePhone { get; set; }

        [JsonProperty("officePhone")]
        public string OfficePhone { get; set; }

        [JsonProperty("fax")]
        public string Fax { get; set; }

        [JsonProperty("personalEmail")]
        public string PersonalEmail { get; set; }

        [JsonProperty("workEmail")]
        public string WorkEmail { get; set; }
    }

    public class PRXResponseCustomerRegister
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("customerId")]
        public string CustomerId { get; set; }
    }


}
