using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models;

public class Customer
{
    private string name ="";
    private string email = "";
    private string password = "";

    public int Id { get; set; }
    [Required]
    public string Name
    {
        get => name;

        set{
            if(string.IsNullOrWhiteSpace(value))
            {
                throw new ValidationException("Name cannot be empty");
            }
            name = value.Trim();
        }
    }

    [Required]
    public string Email    
    {
        get => email;

        set{
            if(string.IsNullOrWhiteSpace(value))
            {
                throw new ValidationException("Email cannot be empty");
            }
            email = value.Trim();
        }
    }
    public string Password
        {
        get => password;

        set{
            if(string.IsNullOrWhiteSpace(value))
            {
                throw new ValidationException("Password cannot be empty");
            }
            password = value.Trim();
        }
    }
    
    // public List<Receipt> OrderHistory {get; set;} = new List<Receipt>;
}