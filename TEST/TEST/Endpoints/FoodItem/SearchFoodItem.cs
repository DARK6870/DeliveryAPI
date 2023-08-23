using Azure;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TEST.Data;
using TEST.Models;

namespace TEST.Endpoints.FoodItem
{
	[Route("fooditems/SearchFoodItem")]
	public class SearchFoodItem : EndpointBaseAsync
		.WithRequest<string>
		.WithActionResult<IEnumerable<FoodItemModel>>
	{
		private readonly DapperContext ctx;

		public SearchFoodItem(DapperContext ctx)
		{
			this.ctx = ctx;
		}

		[HttpGet("{Query}", Name = "SearchFoodItem")]
		[SwaggerOperation(
		Summary = "Search Food Item By Name",
		Description = "This API куегкт information about Food Items whose names contain the query",
		OperationId = "SearchFoodItem",
		Tags = new[] { "FoodItem Endpoint" })]
		public override async Task<ActionResult<IEnumerable<FoodItemModel>>> HandleAsync(string query, CancellationToken cancellationToken)
		{
			string sql = "SELECT * FROM Fooditem WHERE item_name LIKE '%' + @Query + '%'";

			using var connection = ctx.CreateConnection();

			IEnumerable<FoodItemModel> res = await connection.QueryAsync<FoodItemModel>(sql, new { Query = query });

			if (res == null || !res.Any())
			{
				return NotFound();
			}

			return Ok(res);
		}

	}
}
