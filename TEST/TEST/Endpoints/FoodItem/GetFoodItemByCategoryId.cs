using Azure;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TEST.Data;
using TEST.Models;

namespace TEST.Endpoints.FoodItem
{
	[Route("fooditems")]
	public class GetFoodItemByCategoryId : EndpointBaseAsync
		.WithRequest<int>
		.WithActionResult<IEnumerable<FoodItemModel>>
	{
		private readonly DapperContext ctx;

		public GetFoodItemByCategoryId(DapperContext ctx)
		{
			this.ctx = ctx;
		}

		[HttpGet("GetByCategory/{id}", Name = "GetFoodItemByCategoryId")]
		[SwaggerOperation(
		Summary = "Get Food Item By Category Id",
		Description = "This API return list of FoodItems with a specific Category Id",
		OperationId = "GetFoodItemByCategoryId",
		Tags = new[] { "FoodItem Endpoint" })]
		public override async Task<ActionResult<IEnumerable<FoodItemModel>>> HandleAsync(int id, CancellationToken cancellationToken)
		{
			string query = $"SELECT * FROM Fooditem Where item_id = {id}";

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
