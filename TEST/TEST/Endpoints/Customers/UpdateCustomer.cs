using Azure;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TEST.Data;
using TEST.Models;

namespace TEST.Endpoints.Customers
{
	[Route("customers")]
	public class UpdateCustomer : EndpointBaseAsync
		.WithRequest<int, CustomersData>
		.WithActionResult<CustomersData>
	{
		private readonly DapperContext ctx;

		public UpdateCustomer(DapperContext ctx)
		{
			this.ctx = ctx;
		}

		[HttpPut("edit/{id}", Name = "UpdateCustomer")]
		[SwaggerOperation(
		Summary = "Update Customer",
		Description = "This API will update Customer info by Customer Id",
		OperationId = "UpdateCustomer",
		Tags = new[] { "Customer Endpoint" })]
		public override async Task<ActionResult<CustomersData>> HandleAsync(int id, [FromBody] CustomersData customersData, CancellationToken cancellationToken)
		{
			using var connection = ctx.CreateConnection();

			var parameters = new DynamicParameters();
			parameters.Add("@c_firstname", customersData.c_firstname);
			parameters.Add("@c_lastname", customersData.c_lastname);
			parameters.Add("@id", id);

			string query = @"UPDATE Customer SET
                             c_firstname = @c_firstname,
                             c_lastname = @c_lastname
                             WHERE customer_id = @id";

			int affectedRows = await connection.ExecuteAsync(query, parameters);

			if (affectedRows > 0)
			{
				return Ok();
			}
			else
			{
				return NotFound();
			}
		}
	}
}
