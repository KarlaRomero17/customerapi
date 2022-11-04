using Microsoft.AspNetCore.Mvc;
using customerapi.Dtos;
using customerapi.Repositories;
using customerapi.CasosDeUso;

namespace customerapi.Controllers;

[ApiController] //atributo para la api
[Route("api/[controller]/[action]")]
public class CustomerController : Controller
{
    private readonly CustomerDatabaseContext _customerDatabaseContext;
    private readonly IUpdateCustomerUseCase _updateCustomerUseCase;

    public CustomerController(CustomerDatabaseContext customerDatabaseContext,IUpdateCustomerUseCase updateCustomerUseCase)
    {
        _customerDatabaseContext = customerDatabaseContext;
        _updateCustomerUseCase = updateCustomerUseCase;
    }

    //api/customer
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(List<CustomerDto>))]
    public async Task<IActionResult>GetCustomers()
    {
       var result = _customerDatabaseContext.Customer.Select(c=>c.ToDto()).ToList();
       return new OkObjectResult(result);
        //throw new NotImplementedException();

    }
    //api/customer/{id}
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(CustomerDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult>GetCustomer(long id)
    {
        CustomerEntity result = await _customerDatabaseContext.Get(id);
        return new OkObjectResult(result.ToDto());

        //throw new NotImplementedException();

    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(bool))]
    public async Task<IActionResult>DeleteCustomer(long id)
    {
        var result = await _customerDatabaseContext.Delete(id);
        return new OkObjectResult(result);
        //throw new NotImplementedException();

    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type=typeof(CustomerDto))]
    public async Task<IActionResult>CreateCustomer(CreateCustomerDto customer)
    {
        CustomerEntity result = await _customerDatabaseContext.Add(customer);
        return new CreatedResult($"https://localhost:7142/api/customer/getcustomer/{result.Id}", null);
        // throw new NotImplementedException();

    }
    [HttpPut] //actualizar
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(bool))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult>UpdateCustomer(CustomerDto customer)
    {
        var result = await _updateCustomerUseCase.Execute(customer);
        if(result == null )
            return new NotFoundResult();
    
        return new OkObjectResult(result);
       // throw new NotImplementedException();

    }

}
