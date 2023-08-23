using Azure;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TEST.Data;
using TEST.Models;

namespace TEST.Endpoints.Address
{
	[Route("adresses")]
	public class UpdateAddress : EndpointBaseAsync
		.WithRequest<int, AdressModel>
		.WithActionResult<AdressModel>
	{

		private readonly DapperContext ctx;

		public UpdateAddress(DapperContext ctx)
		{
			this.ctx = ctx;
		}

		[HttpPut("edit/{id}", Name = "UpdateAddressById")]
		[SwaggerOperation(
		Summary = "Update Address",
		Description = "This API will update Address info by Address Id",
		OperationId = "UpdateAddressById",
		Tags = new[] { "Address Endpoint" })]
		public override async Task<ActionResult<AdressModel>> HandleAsync(int id, [FromBody] AdressModel addressData, CancellationToken cancellationToken)
		{
			string query = $"SELECT * FROM Adres WHERE adress_id = {id}";
			using var connection = ctx.CreateConnection();

			AdressModel existingAddress = connection.QueryFirstOrDefault<AdressModel>(query);
			if (existingAddress is null) return NotFound();

			existingAddress.house = addressData.house;
			existingAddress.street = addressData.street;
			existingAddress.city = addressData.city;
			existingAddress.postalcode = addressData.postalcode;

			query = $@"UPDATE Adres SET
                  house = @house, street = @street, city = @city, postalcode = @postalcode
                  WHERE adress_id = {id}";

			var parameters = new DynamicParameters();
			parameters.Add("@house", existingAddress.house);
			parameters.Add("@street", existingAddress.street);
			parameters.Add("@city", existingAddress.city);
			parameters.Add("@postalcode", existingAddress.postalcode);

			var updatedAddress = await connection.QueryAsync<AdressModel>(query, parameters);
			return Ok();
		}
	}
}