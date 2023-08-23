using Azure;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TEST.Data;
using TEST.Models;

namespace TEST.Endpoints.FoodItem
{
	[Route("fooditems")]
	public class GetAllFodItems : EndpointBaseAsync
		.WithoutRequest
		.WithActionResult<IEnumerable<FoodItemModel>>
	{
		private readonly DapperContext ctx;

		public GetAllFodItems(DapperContext ctx)
		{
			this.ctx = ctx;
		}

		[HttpGet(Name = "GetAllFoodItems")]
		[SwaggerOperation(
		Summary = "Get All Food Items",
		Description = "This API return full list of FoodItems",
		OperationId = "GetAllFoodItems",
		Tags = new[] { "FoodItem Endpoint" })]
		public override async Task<ActionResult<IEnumerable<FoodItemModel>>> HandleAsync(CancellationToken cancellationToken)
		{
			string query = "SELECT * FROM Fooditem";

			using var connection = ctx.CreateConnection();

			IEnumerable<FoodItemModel> res = await connection.QueryAsync<FoodItemModel>(query);

			if (res == null || !res.Any())
			{
				return NotFound();
			}

			return Ok(res);
		}
	}
}
