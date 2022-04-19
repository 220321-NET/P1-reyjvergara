using Models;
using System.Text;
using System.Text.Json;
//using System.Text.RegularExpressions;

namespace FumoAlgo;

public class HttpSerivce
{
    private readonly string _apiBaseURL = "https://localhost:7183/api/";
    private HttpClient client = new HttpClient();

    public HttpSerivce()
    {
        client.BaseAddress = new Uri(_apiBaseURL);
    }

    public async Task<List<Customer>> GetAllCustomersAsync()
    {
        List<Customer> customers = new List<Customer>();
        /*  First we will assemble our URL for the response
            Then we will send a GET request to the API endpoint
            After that, we are going to deserialize the response and 
            return it to our caller as List<StoreFront>     */
        try
        {
            //HttpResponseMessage response = await client.GetAsync(url);
            //response.EnsureSuccessStatusCode();
            //string responseString = await response.Content.ReadAsStringAsync();
            
            customers = await JsonSerializer.DeserializeAsync<List<Customer>>(await client.GetStreamAsync("Customer/GetAll")) ?? new List<Customer>();
            
            //customers = await JsonSerializer.DeserializeAsync<List<Customer>>(await response.Content.ReadAsStreamAsync())?? new List<Customer>();
            //customers = await JsonSerializer.DeserializeAsync<List<Customer>>(await client.GetStreamAsync("Customer/GetAll")) ?? new List<Customer>();
        }
        catch(HttpRequestException)
        {
            Console.WriteLine("Should write this to a log *IDIOT*");
        }
        return customers;
    }

    public Customer CreateCustomer()
    {
        return new Customer();
    }
}
