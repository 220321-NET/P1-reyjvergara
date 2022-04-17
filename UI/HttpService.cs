using System.Net.Http;
using Models;
namespace UI;

public class HttpSerivce
{
    private readonly string _apiBaseUrl = "https://localhost:7223/api";

    public List<StoreFront> GetStoreFronts()
    {
        return new List<StoreFront>();
    }

    public Customer CreateCustomer()
    {
        return new Customer();
    }

    public List<Customer> GetAllCustomers()
    {
        return new List<Customer>();
    }
}
