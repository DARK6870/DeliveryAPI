using Azure;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TEST.Data;
using TEST.Models;

namespace TEST.Endpoints.Restaurant
{
	[Route("restaurants")]
	public class GetRestaurantsById : EndpointBaseAsync
		.WithRequest<int>
		.WithActionResult<RestaurantModel>
	{
		private readonly DapperContext ctx;

		public GetRestaurantsById(DapperContext ctx)
		{
			this.ctx = ctx;
		}

		[HttpGet("{id}", Name = "GetRestaurantById")]
		[SwaggerOperation(
		Summary = "Get Restaurant By Id",
		Description = "This API return information about Restaurant with a specific Id",
		OperationId = "GetRestaurantById",
		Tags = new[] { "Restaurant Endpoint" })]
		public override async Task<ActionResult<RestaurantModel>> HandleAsync(int id, CancellationToken cancellationToken)
		{
			string query = $"SELECT * FROM Restaurant Where restaurant_id = {id}";

			using var connection = ctx.CreateConnection();

			var res = await connection.QueryAsync<RestaurantModel>(query);

			if (res == null || !res.Any())
			{
				return NotFound();
			}

			return Ok(res);
		}
	}
}
