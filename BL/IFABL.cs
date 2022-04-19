namespace BL;

/// <summary> 
/// Interface <c>IFABL</c> is the interface for the Fumo Algorithms Business Logic
/// </summary>
public interface IFABL
{
    StoreFront CreateStore(StoreFront storeToCreate);
    Customer CreateCustomer(Customer customerToCreate);
    Admin CreateAdmin(Admin adminToCreate, int storeId);

    ///<summary>
    ///  GetStoreFronts shows a list of all the storefronts 
    ///</summary>
    Task<List<StoreFront>> GetAllStoreFrontsAsync();

    ///<summary>
    ///  GetAllCustomers shows a list of all the customers
    ///</summary>
    Task<List<Customer>> GetAllCustomersAsync();

    ///<summary>
    ///  GetStoreByID gives a StoreFront given an id
    ///</summary>
    Task<StoreFront?> GetStoreByIDAsync(int storeId);
    Task<List<Product>> GetStoreProductsAsync(int storeId);

    ///<summary>
    ///  GetProducts shows a list of all the products
    ///</summary>
    Task<List<Product>> GetAllProductsAsync();
    //List<Receipt> GetReceipts();
    Product AddProduct(Product productToAdd, int quantity, int storeId); 


    ///<summary>
    ///  FindCustomer is used for the login feature, checks database if email and password combination exists
    ///</summary>
    Task<Customer> FindCustomerAsync(string email, string password);
    Task<Customer> FindCustomerByEmailAsync(string email);
    int ValidateEmailPass(string email, string password);
    
    void DeleteCustomer(Customer customerToDelete);
    void DeleteProduct(Product productToDelete);
    void DeleteStore(StoreFront storeToDelete);
}
