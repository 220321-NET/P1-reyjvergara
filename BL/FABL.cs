using DL;
using Models;
namespace BL;

public class FABL : IFABL
{
    private readonly DBRepository _repo;
    public FABL(DBRepository repo)
    {
        _repo = repo;
    }
    ///<summary>
    ///  CreateStore recieves information of a StoreFront (address, city, name) to make a store. StoreID is automatically made.
    ///</summary>
    public StoreFront CreateStore(StoreFront storeToCreate)
    {
        return _repo.CreateStore(storeToCreate);
    }

    // public void CreateProduct(Product productToCreate)
    // {
    //     _repo.CreateProduct(productToCreate);
    // }

    ///<summary>
    ///  CreateCustomer recieves information of Customer to make one
    ///</summary>
    public Customer CreateCustomer(Customer customerToCreate)
    {
        return _repo.CreateCustomer(customerToCreate);
    }

    public Admin CreateAdmin(Admin adminToCreate, int storeId)
    {
        return _repo.CreateAdmin(adminToCreate, storeId);
    }
    ///<summary>
    ///  CreateReceipt will be adjusted to take in total price and date, no plan for updating this project to have all product names in an order
    ///</summary>
    public Receipt CreateReceipt(int storeId, int customerId, int productId)
    {
        return _repo.CreateReceipt(storeId, customerId, productId);
    }

    public async Task<Customer> FindCustomerAsync(string email, string password)
    {
        return await _repo.FindCustomerAsync(email, password);
    }


    public async Task<List<StoreFront>> GetAllStoreFrontsAsync()
    {
        return await _repo.GetAllStoreFrontsAsync();
    }

    public async Task<List<Customer>> GetAllCustomersAsync()
    {
        return await _repo.GetAllCustomersAsync();
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _repo.GetAllProductsAsync();
    }

    ///<summary>
    ///  AddProduct recieves the product, wanted quantity, and desired storeID to place the product
    ///</summary>
    public Product AddProduct(Product productToAdd, int quantity, int storeId)
    {
        return _repo.AddProduct(productToAdd, quantity, storeId);
    }

    public async Task<StoreFront?> GetStoreByIDAsync(int storeId)
    {
        return  await _repo.GetStoreByIDAsync(storeId);
    }

    ///<summary>
    ///  GetStoreProducts shows a list of all the products given storeId
    ///</summary>
    public async Task<List<Product>> GetStoreProductsAsync(int storeId)
    {
        return await _repo.GetStoreProductsAsync(storeId);
    }

    public void DeleteCustomer(Customer customerToDelete)
    {
        throw new NotImplementedException();
        //return _repo.DeleteCustomer(customerToDelete);
    }

    public void DeleteStore(StoreFront storeToDelete)
    {
        throw new NotImplementedException();
        //return _repo.DeleteStore(storeToDelete);
    }
    public void DeleteProduct(Product productToDelete)
    {
        throw new NotImplementedException();
        //return _repo.DeleteProduct(productToDelete);
    }

    ///<summary>
    ///  ValidateEmail is used to see if email exists in database already
    ///</summary>
    public int ValidateEmail(string email)
    {
        return _repo.ValidateEmail(email);
    }

    public int ValidateEmailPass(string email, string password)
    {
        return _repo.ValidateEmailPass(email, password);
    }
}