using Azure;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TEST.Data;
using TEST.Models;

namespace TEST.Endpoints.Customers
{
    [Route("customers")]
    public class GetAllCustomers : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult<IEnumerable<CustomersModel>>
    {
        private readonly DapperContext ctx;

        public GetAllCustomers(DapperContext ctx)
        {
            this.ctx = ctx;
        }

        [HttpGet(Name = "GetAllCustomers")]
		[SwaggerOperation(
		Summary = "Get All Customers",
		Description = "This API return full list of Customers",
		OperationId = "GetAllCustomers",
		Tags = new[] { "Customer Endpoint" })]
		public override async Task<ActionResult<IEnumerable<CustomersModel>>> HandleAsync(CancellationToken cancellationToken)
        {
            string query = "SELECT * FROM Customer";

            using var connection = ctx.CreateConnection();

            IEnumerable<CustomersModel> res = await connection.QueryAsync<CustomersModel>(query);

            if (res == null || !res.Any())
            {
                return NotFound();
            }

            return Ok(res);
        }
    }
}
