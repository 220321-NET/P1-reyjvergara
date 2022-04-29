using Models;
using System.Text;
using System.Text.Json;

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

    public async Task<Customer> FindEmail(string email)
    {
        Customer customer = new Customer();
        try
        {
            customer = await JsonSerializer.DeserializeAsync<Customer>(await client.GetStreamAsync("Customer/FindEmail/{email}")) ?? new Customer();
        }
        catch(HttpRequestException)
        {
            Console.WriteLine("Cant' find email right");
        }
        return customer;
    }

    public async Task<Customer> FindEmailPass(string email, string password)
    {
        Customer customer = new();
        customer.Name = "temp";
        try
        {
            customer = await JsonSerializer.DeserializeAsync<Customer>(await client.GetStreamAsync("Customer/Find/{email}/{password}")) ?? new Customer();
        }
        catch(HttpRequestException)
        {
            Console.WriteLine("Cant' find email/password right");
        }
        return customer;
    }

    public async void CreateCustomer(string name, string email, string password)
    {
        Customer newCust = new();
        newCust.Name = name;
        newCust.Email = email;
        newCust.Password = password;
        string customerToCreate = JsonSerializer.Serialize(newCust);
        StringContent content = new StringContent(customerToCreate, UnicodeEncoding.UTF8, "application/json");
        try
        {
            HttpResponseMessage responseMessage = await client.PostAsync("Customer/Create{customerToCreate}", content);
        }
        catch(HttpRequestException)
        {
            Console.WriteLine("Terrible execution on create customer");
        }
    }

    public int ValidateEmail(string email)
    {
        return ValidateEmail(email);
    }
}
