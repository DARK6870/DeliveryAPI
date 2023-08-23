using Microsoft.AspNetCore.Mvc;
using TEST.Models;
using TEST.Data;
using Dapper;
using Azure;
using Swashbuckle.AspNetCore.Annotations;

namespace TEST.Endpoints.Address
{
    [Route("adresses")]
    public class GetAddressById : EndpointBaseAsync
        .WithRequest<int>
        .WithActionResult<AdressModel>
    {
        private readonly DapperContext ctx;

        public GetAddressById(DapperContext ctx)
        {
            this.ctx = ctx;
        }

        [HttpGet("{id}", Name = "GetAddressById")]
		[SwaggerOperation(
	    Summary = "Get Address info by Id",
	    Description = "This API returns information about an address with a specific Id",
	    OperationId = "GetAddressById",
    	Tags = new[] { "Address Endpoint" })]
		public override async Task<ActionResult<AdressModel>> HandleAsync(int id, CancellationToken cancellationToken)
        {
            string query = $"SELECT * FROM Adres Where adress_id = {id}";

            using var connection = ctx.CreateConnection();

            var address = await connection.QueryFirstOrDefaultAsync<AdressModel>(query);

            if (address is null) return NotFound();

            return address;
        }
    }
}
