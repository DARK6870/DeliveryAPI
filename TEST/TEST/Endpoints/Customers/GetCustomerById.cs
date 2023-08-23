using Microsoft.AspNetCore.Mvc;
using TEST.Models;
using TEST.Data;
using Dapper;
using Azure;
using Swashbuckle.AspNetCore.Annotations;

namespace TEST.Endpoints.Address
{
    [Route("customers")]
    public class GetById : EndpointBaseAsync
        .WithRequest<int>
        .WithActionResult<CustomersModel>
    {
        private readonly DapperContext ctx;

        public GetById(DapperContext ctx)
        {
            this.ctx = ctx;
        }

        [HttpGet("{id}", Name = "GetCustomerById")]
		[SwaggerOperation(
		Summary = "Get Customer By Id",
		Description = "This API will return information about Customer with a specific Id",
		OperationId = "GetCustomerById",
		Tags = new[] { "Customer Endpoint" })]
		public override async Task<ActionResult<CustomersModel>> HandleAsync(int id, CancellationToken cancellationToken)
        {
            string query = $"SELECT * FROM Customer Where customer_id = {id}";

            using var connection = ctx.CreateConnection();

            var res = await connection.QueryFirstOrDefaultAsync<CustomersModel>(query);

            if (res is null) return NotFound();

            return res;
        }
    }
}
