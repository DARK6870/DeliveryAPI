using Azure;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TEST.Data;
using TEST.Models;

namespace TEST.Endpoints.Restaurant
{
	[Route("restaurants")]
	public class AddRestaurant : EndpointBaseAsync
		.WithRequest<RestaurantModel>
		.WithActionResult<RestaurantModel>
	{

		private readonly DapperContext ctx;

		public AddRestaurant(DapperContext ctx)
		{
			this.ctx = ctx;
		}

		[HttpPost("add", Name = "AddRestaurant")]
		[SwaggerOperation(
		Summary = "Add new Restaurant",
		Description = "This API will add information about Restaurant in DB",
		OperationId = "AddRestaurant",
		Tags = new[] { "Restaurant Endpoint" })]
		public override async Task<ActionResult<RestaurantModel>> HandleAsync([FromBody] RestaurantModel restaurant, CancellationToken cancellationToken)
		{
			string query = "INSERT INTO Restaurant (restaurant_id, restaurant_name, adress_id) VALUES (@restaurant_id, @restaurant_name, @adress_id)";
			using var connection = ctx.CreateConnection();

			await connection.ExecuteAsync(query, restaurant);

			query = "SELECT * FROM Restaurant WHERE restaurant_id = @restaurant_id";

			var res = await connection.QueryAsync<RestaurantModel>(query, restaurant);
			return Ok();
		}
	}
}
