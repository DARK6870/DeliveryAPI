using Microsoft.AspNetCore.Mvc;
using TEST.Models;
using TEST.Data;
using Dapper;
using Azure;
using Swashbuckle.AspNetCore.Annotations;

namespace TEST.Endpoints.Address
{
    [Route("adresses")]
    public class GetAllAddresses : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult<IEnumerable<AdressModel>>
    {
        private readonly DapperContext ctx;

        public GetAllAddresses(DapperContext ctx)
        {
            this.ctx = ctx;
        }

        [HttpGet(Name = "GetAllAddresses")]
		[SwaggerOperation(
		Summary = "Get Addresses",
		Description = "This API returns full list of Addresses",
		OperationId = "GetAllAddresses",
		Tags = new[] { "Address Endpoint" })]
		public override async Task<ActionResult<IEnumerable<AdressModel>>> HandleAsync(CancellationToken cancellationToken)
        {
            string query = "SELECT * FROM Adres";

            using var connection = ctx.CreateConnection();

            IEnumerable<AdressModel> addresses = await connection.QueryAsync<AdressModel>(query);

            if (addresses == null || !addresses.Any())
            {
                return NotFound();
            }

            return Ok(addresses);
        }
    }
}
