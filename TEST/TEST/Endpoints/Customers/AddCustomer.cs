using Azure;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TEST.Data;
using TEST.Models;

namespace TEST.Endpoints.Customers
{
	[Route("customers")]
	public class AddCustomer : EndpointBaseAsync
		.WithRequest<CustomersModel>
		.WithActionResult<CustomersModel>
	{

		private readonly DapperContext ctx;

		public AddCustomer(DapperContext ctx)
		{
			this.ctx = ctx;
		}

		[HttpPost("add", Name = "AddNewCustomer")]
		[SwaggerOperation(
		Summary = "Add Customer",
		Description = "This API will add a new customer in DB",
		OperationId = "AddNewCustomer",
		Tags = new[] { "Customer Endpoint" })]
		public override async Task<ActionResult<CustomersModel>> HandleAsync(CustomersModel customers, CancellationToken cancellationToken)
		{
			string query = "INSERT INTO Customer VALUES (@customer_id, @c_firstname, @c_lastname)";
			using var connection = ctx.CreateConnection();

			var customer = await connection.QueryAsync<CustomersModel>(query, customers);
			return Ok();
		}
	}
}
