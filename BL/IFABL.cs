namespace BL;

/// <summary> 
/// Interface <c>IFABL</c> is the interface for the Fumo Algorithms Business Logic
/// </summary>
public interface IFABL
{
    StoreFront CreateStore(StoreFront storeToCreate);
    Customer CreateCustomer(Customer customerToCreate);
    Admin CreateAdmin(Admin adminToCreate, int storeId);
    List<StoreFront> GetStoreFronts();
    List<Customer> GetAllCustomers();
    StoreFront? GetStoreByID(int storeId);
    List<Product> GetStoreProducts(int storeId);
    List<Product> GetAllProducts();
      //List<Receipt> GetReceipts();
    Product AddProduct(Product productToAdd, int quantity, int storeId);
    Customer FindCustomer(string email, string password);
    void DeleteCustomer(Customer customerToDelete);
    void DeleteProduct(Product productToDelete);
    void DeleteStore(StoreFront storeToDelete);
}
