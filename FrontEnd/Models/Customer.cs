using System.ComponentModel.DataAnnotations;

namespace Models;

public class Customer
{
    private string name ="";
    private string email = "";
    private string password = "";

    [Required]
    public int Id{get;set;}
    public string Name{get;set;}
    public string Email{get;set;}
    public string Password{get;set;}
    /*public string Name
    {
        get => name;

        set{
            if(string.IsNullOrWhiteSpace(value))
            {
                throw new ValidationException("Name cannot be empty");
            }
            name = value;
        }
    }

    public string Email    
    {
        get => email;

        set{
            if(string.IsNullOrWhiteSpace(value))
            {
                throw new ValidationException("Email cannot be empty");
            }
            email = value;
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
            password = value;
        }
    }*/

    public override string ToString()
    {
        string qString = $"Id:{Id} \nName:{Name} \nEmail:{Email}";
        return qString;
    }
    
    // public List<Receipt> OrderHistory {get; set;} = new List<Receipt>;
}