using System;
using System.Net;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter.Xml;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleProject.Application.Customers;
using SampleProject.Application.Customers.GetCustomerDetails;
using SampleProject.Application.Customers.RegisterCustomer;

namespace SampleProject.API.Customers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(IMediator mediator, ILogger<CustomersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Register customer.
        /// </summary>
        [Route("")]
        [HttpPost]
        [ProducesResponseType(typeof(CustomerDto), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> RegisterCustomer([FromBody]RegisterCustomerRequest request)
        {
           var customer = await _mediator.Send(new RegisterCustomerCommand(request.Email, request.Name));

            _logger.LogTrace($"Logging Trace: {request.Name}");
            _logger.LogDebug($"Logging Debug: {request.Name}");
            _logger.LogInformation($"Logging Information: {request.Name}");
            _logger.LogWarning($"Logging Warning: {request.Name}");
            _logger.LogError($"Logging Error: {request.Name}");
            _logger.LogCritical($"Logging Critical: {request.Name}");

            throw new System.Exception("whyyr?");


           return Created(string.Empty, customer);
        }

        /// <summary>
        /// Get customer by ID
        /// </summary>
        [Route("{customerId}")]
        [HttpGet]
        [ProducesResponseType(typeof(CustomerDto),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetCustomer(Guid customerId)
        {
            var query = new GetCustomerDetailsQuery(customerId);
            var customerDto = await _mediator.Send(query);

            if (customerDto == null)
            {
                return NotFound();
            }
            return Ok(customerDto);
        }


    }




}
