using Microsoft.Data.SqlClient;
using System.Data;

namespace DL;

public class DBRepository : IRepository
{

    private readonly string _connectionString;

    public DBRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public StoreFront CreateStore(StoreFront newStore)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        SqlCommand cmd = new SqlCommand("Insert into StoreFront(Name, City, State) OUTPUT Inserted.Id Values (@name, @city, @state)", connection);

        cmd.Parameters.AddWithValue("@name", newStore.Name);
        cmd.Parameters.AddWithValue("@city", newStore.City);
        cmd.Parameters.AddWithValue("@state", newStore.State);

        try
        {
            newStore.StoreID = (int) cmd.ExecuteScalar();
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
        connection.Close();
        return newStore;
    }

    public Customer CreateCustomer(Customer newCustomer)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        SqlCommand cmd = new SqlCommand("INSERT INTO  Users(Email, CPassword, CName) OUTPUT INSERTED.Id VALUES (@email, @password, @name)", connection);

        cmd.Parameters.AddWithValue("@email", newCustomer.Email);
        cmd.Parameters.AddWithValue("@password", newCustomer.Password);
        cmd.Parameters.AddWithValue("@name", newCustomer.Name);

        try
        {
            newCustomer.Id = (int) cmd.ExecuteScalar();
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }

        connection.Close();
        return newCustomer;
    }

    public Admin CreateAdmin(Admin newAdmin, int storeId)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        SqlCommand cmd = new SqlCommand("Insert into Admin(Email, APassword, AName, StoreId) OUTPUT Inserted.Id Values (@email, @password, @name, @storeId)", connection);

        cmd.Parameters.AddWithValue("@email", newAdmin.Email);
        cmd.Parameters.AddWithValue("@password", newAdmin.Password);
        cmd.Parameters.AddWithValue("@name", newAdmin.Name);
        cmd.Parameters.AddWithValue("@storeId", storeId);

        try
        {
            newAdmin.Id = (int) cmd.ExecuteScalar();
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }

        connection.Close();
        return newAdmin;
    }

    public Product AddProduct(Product newProduct, int quantity, int storeId)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        SqlCommand cmd = new SqlCommand("Insert into Product(Name, Description, Price, Quantity, storeId) output inserted.Id Values(@name, @description, @price, @quantity, @storeId)", connection);

        cmd.Parameters.AddWithValue("@name", newProduct.Name);
        cmd.Parameters.AddWithValue("@description", newProduct.Description);
        cmd.Parameters.AddWithValue("@price", newProduct.Price);
        cmd.Parameters.AddWithValue("@quantity", quantity);
        cmd.Parameters.AddWithValue("@storeId", storeId);

        try
        {
            newProduct.ProductID = (int) cmd.ExecuteScalar();
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
        connection.Close();
        return newProduct;
    }

    public Receipt CreateReceipt(int storeId, int customerId, int productId)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        SqlCommand cmd = new SqlCommand("Insert into Receipt(ProductName, TotalCost, StoreID, CustomerID) Values ((select Name from Product where Id = @prodId), (select Price from Product where Id = @prodId), @storeId, @customerId)", connection);
        SqlCommand cmd2 = new SqlCommand("UPDATE Product SET Quantity = Quantity -1 Where Id = @prodId  AND StoreID = @storeId", connection);
        cmd.Parameters.AddWithValue("@storeId", storeId);
        cmd.Parameters.AddWithValue("@customerId", customerId);
        cmd.Parameters.AddWithValue("@prodId", productId);
        cmd2.Parameters.AddWithValue("@prodId", productId);
        cmd2.Parameters.AddWithValue("@storeId", storeId);
        cmd.ExecuteNonQuery();
        cmd2.ExecuteNonQuery();
        connection.Close();
        Console.WriteLine("Terrible code, rework later please...\n");
        Receipt receipt = new Receipt();
        receipt.CustomerID = customerId;
        receipt.StoreID = storeId;
        return receipt;
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        List<Product> allProducts = new List<Product>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        using SqlCommand cmd = new("Select * from Product", connection);
        using SqlDataReader reader = cmd.ExecuteReader();
        while (await reader.ReadAsync())
        {
            int id = reader.GetInt32(0);
            string name = reader.GetString(1);
            string description = reader.GetString(2);
            decimal price = reader.GetDecimal(3);
            int quantity = reader.GetInt32(4);
            int sid = reader.GetInt32(5);
            Product prod = new()
            {
                ProductID = id,
                Price = price,
                Name = name,
                Description = description,
                Quantity = quantity,
                StoreID = sid,
            };
            allProducts.Add(prod);
        }
        reader.Close();
        connection.Close();
        return allProducts;
    }

    public async Task<List<Product>> GetStoreProductsAsync(int storeId)
    {
        List<Product> storeProduct = new List<Product>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        using SqlCommand cmd = new SqlCommand("Select * from Product where StoreID = @storeId", connection);
        cmd.Parameters.AddWithValue("@storeId", storeId);
        using SqlDataReader reader = cmd.ExecuteReader();

        while(await reader.ReadAsync())
        {
            int id = reader.GetInt32(0);
            string name = reader.GetString(1);
            string description = reader.GetString(2);
            decimal price = reader.GetDecimal(3);
            int quantity = reader.GetInt32(4);
            int sid = storeId;
            Product prod = new Product
            {
                ProductID = id,
                Price = price,
                Name = name,
                Description = description,
                Quantity = quantity,
                StoreID = sid,
            };
            storeProduct.Add(prod);
        }
        reader.Close();
        connection.Close();
        return storeProduct;
    }

    public async Task<List<Customer>> GetAllCustomersAsync()
    {
        List<Customer> customerFromStore = new List<Customer>();

        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand cmd = new SqlCommand("Select * from Users", connection);
        using SqlDataReader reader = cmd.ExecuteReader();

        while(await reader.ReadAsync())
        {
            int Id = reader.GetInt32(0);
            string email = reader.GetString(1);
            string password = reader.GetString(2);
            string cname = reader.GetString(3);

            Customer cus = new Customer
            {
                Id = Id,
                Email = email,
                Name = cname,
                Password = password,
            };
            customerFromStore.Add(cus);
        }

        reader.Close();
        connection.Close();

        return customerFromStore;
    }
    public void UpdateCustomer(Customer customerToUpdate)
    {
        DataSet customer = new DataSet();
        throw new NotImplementedException();
    }


    public async Task<List<StoreFront>> GetAllStoreFrontsAsync()
    {
        List<StoreFront> allStores = new List<StoreFront>();

        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand cmd = new SqlCommand("Select * from StoreFront", connection);
        using SqlDataReader reader = cmd.ExecuteReader();

        while(await reader.ReadAsync())
        {
            int id = reader.GetInt32(0);
            string name = reader.GetString(1);
            string city = reader.GetString(2);
            string state = reader.GetString(3);

            StoreFront store = new StoreFront
            {
                StoreID = id,
                Name = name,
                City = city,
                State = state,
            };
            allStores.Add(store);
        }
        reader.Close();
        connection.Close();

        return allStores;
    }

    public async Task<StoreFront?> GetStoreByIDAsync(int storeId)
    {
        StoreFront storeToReturn = new StoreFront();
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        SqlCommand cmd = new SqlCommand("Select * from StoreFront where StoreId = @storeId", connection);
        cmd.Parameters.AddWithValue("@storeId", storeId);
        SqlDataReader reader = cmd.ExecuteReader();
        if (await reader.ReadAsync())
        {
            storeToReturn.StoreID = reader.GetInt32(0);
            storeToReturn.Name = reader.GetString(1);
            storeToReturn.City = reader.GetString(2);
            storeToReturn.State = reader.GetString(3);
            reader.Close();
            return storeToReturn;
        }
        return null;
    }

    public async Task<Customer> FindCustomerAsync(string email, string password)
    {
        Customer customerToReturn = new Customer();
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        SqlCommand cmd = new SqlCommand("Select * from Users where Email = @email AND CPassword = @password", connection);
        cmd.Parameters.AddWithValue("@email", email.Trim());
        cmd.Parameters.AddWithValue("@password", password.Trim());
        SqlDataReader reader = cmd.ExecuteReader();

        if(await reader.ReadAsync())
        {
            int id = reader.GetInt32(0);
            string emailRet = reader.GetString(1);
            string passw = reader.GetString(2);
            string name = reader.GetString(3);
            customerToReturn.Id = id;
            customerToReturn.Email = emailRet;
            customerToReturn.Password = passw;
            customerToReturn.Name = name;
        }
        reader.Close();
        connection.Close();
        /* Have something to return null like STORE*/
        return customerToReturn;
    }

    public async Task<Customer> FindCustomerByEmailAsync(string email)
    {
        Customer customerToTest = new Customer();
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        SqlCommand cmd = new SqlCommand("Select * from Users where Email = @email", connection);
        cmd.Parameters.AddWithValue("@email", email.Trim());
        SqlDataReader reader = cmd.ExecuteReader();

        if (await reader.ReadAsync())
        {
            int id = reader.GetInt32(0);
            string emailRet = reader.GetString(1);
            string passw = reader.GetString(2);
            string name = reader.GetString(3);
            customerToTest.Id = id;
            customerToTest.Email = emailRet;
            customerToTest.Password = passw;
            customerToTest.Name = name;
        }
        reader.Close();
        connection.Close();
        /* Have something to return null like STORE*/
        return customerToTest;
    }

    public int ValidateEmailPass(string email, string password)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        SqlCommand cmd = new SqlCommand("SELECT COUNT(*) from users where Email like @email and CPassword like @password", connection);
        cmd.Parameters.AddWithValue("@email", email.Trim());
        cmd.Parameters.AddWithValue("@password", password.Trim());
        int userCount = (int) cmd.ExecuteScalar();
        connection.Close();
        return userCount;
    }

    public void DeleteCustomer(Customer customerToDelete)
    {
        throw new NotImplementedException();
    }

    public void DeleteStore(StoreFront storeToDelete)
    {
        throw new NotImplementedException();
    }
    public void DeleteProduct(Product productToDelete)
    {
        throw new NotImplementedException();
    }
}