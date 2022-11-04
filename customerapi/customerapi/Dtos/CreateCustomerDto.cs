
using System.ComponentModel.DataAnnotations;

namespace customerapi.Dtos;
public class CreateCustomerDto
{
    [Required (ErrorMessage =  "El nombre propio tiene que estar especificado")]
    public string FirstName {get; set;}
    [Required (ErrorMessage =  "El apellido tiene que estar especificado")]
    public string LastName {get; set;} ///^[-\w.%+]{1,64}@(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,63}$/i
    //[RegularExpression("^[a-ZA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage =  "El mail no es correcto")]
    public string Email {get; set;}
    public string Phone {get; set;}
    public string Address{get; set;}
}