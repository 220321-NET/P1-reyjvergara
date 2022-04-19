using System.ComponentModel.DataAnnotations;
namespace Models;

public class StoreFront
{
    private string name = "";
    private string city = "";
    private string state = "";
    [Required]
    public string Name 
    {
        get => name;

        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ValidationException("Name cannot be empty");
            name = value.Trim();
        }
    } 
    [Required]
    public int StoreID {get;set;}
    public string City 
    {
        get=> city;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ValidationException("City cannot be empty");
            city = value.Trim();
        }
    }
    [Required]
    public string State
    {
        get => state; 

        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ValidationException("State cannot be empty");
            state = value.Trim();
        }
    }
    
}
