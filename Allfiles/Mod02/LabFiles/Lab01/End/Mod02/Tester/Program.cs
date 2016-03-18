using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Tester.Models;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please enter a phone number argument.");
                Console.ReadLine();
                return;
            }
            else
            {
                string phoneNum = args[0];
                RunAsync(phoneNum).Wait();

                Console.WriteLine("Press enter to exit.");
                Console.ReadLine();
            }
        }

        static async Task RunAsync(string phoneNum)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5000/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Contacts contact = new Contacts() { FirstName = "New", LastName = "Contact", Email = "newcontact@contoso.com", MobilePhone = phoneNum };
                HttpResponseMessage response = await client.PostAsJsonAsync("api/Contacts", contact);
                if (response.IsSuccessStatusCode)
                {
                    // Get the URI of the created resource.
                    Uri contactUrl = response.Headers.Location;
                    Console.WriteLine("Contact added at " + contactUrl.ToString());
                    Console.ReadLine();
                }

                response = await client.GetAsync("api/Contacts/" + phoneNum);
                if (response.IsSuccessStatusCode)
                {
                    contact = await response.Content.ReadAsAsync<Contacts>();
                    Console.WriteLine("CONTACT:\nFull name: {0} {1}, email:{2}, company: {3}", contact.FirstName, contact.LastName, contact.Email, contact.Company);
                    Console.ReadLine();
                }

                contact.Company = "Cotnoso";
                response = await client.PutAsJsonAsync("api/Contacts/" + phoneNum, contact);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Contact information updated successfully!");
                }

                //get contact again to make see the update
                response = await client.GetAsync("api/Contacts/" + phoneNum);
                if (response.IsSuccessStatusCode)
                {
                    contact = await response.Content.ReadAsAsync<Contacts>();
                    Console.WriteLine("UPDATED CONTACT:\nFull name: {0} {1}, email:{2}, company: {3}", contact.FirstName, contact.LastName, contact.Email, contact.Company);
                    Console.ReadLine();
                }

                response = await client.DeleteAsync("api/Contacts/" + phoneNum);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Contact information deleted successfully!");
                }
            }
        }
    }
}
