namespace DL;
public interface IRepository
{
   // Console.WriteLine("Welcome to Fumo and Algorithms!");

   List<Product> GetAllProducts();
   List<StoreFront> GetAllStoreFronts();
   List<Customer> GetAllCustomers();

   StoreFront CreateStore(StoreFront storeToAdd);
   //void CreateProduct(Product productToAdd);
   Customer CreateCustomer(Customer customerToAdd);
}
