namespace DL;
public interface IRepository
{
   // Console.WriteLine("Welcome to Fumo and Algorithms!");

   Task<List<Product>> GetAllProductsAsync();
   Task<List<StoreFront>> GetAllStoreFrontsAsync();
   Task<List<Customer>> GetAllCustomersAsync();

   StoreFront CreateStore(StoreFront storeToAdd);
   //void CreateProduct(Product productToAdd);
   Customer CreateCustomer(Customer customerToAdd);
}
