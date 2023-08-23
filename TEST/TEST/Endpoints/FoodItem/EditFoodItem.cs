using Azure;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TEST.Data;
using TEST.Models;

namespace TEST.Endpoints.FoodItem
{
	[Route("fooditems")]
	public class EditFoodItem : EndpointBaseAsync
		.WithRequest<int, FoodItemData>
		.WithActionResult<FoodItemData>
	{

		private readonly DapperContext ctx;

		public EditFoodItem(DapperContext ctx)
		{
			this.ctx = ctx;
		}

		[HttpPut("edit/{id}", Name = "EditFoodItem")]
		[SwaggerOperation(
		Summary = "Update Food Item By Id",
		Description = "This API will update information about FoodItem by Id",
		OperationId = "EditFoodItem",
		Tags = new[] { "FoodItem Endpoint" })]
		public override async Task<ActionResult<FoodItemData>> HandleAsync(int id, [FromBody] FoodItemData foodItem, CancellationToken cancellationToken)
		{
			string query = $"SELECT * FROM Fooditem WHERE item_id = {id}";
			using var connection = ctx.CreateConnection();

			FoodItemData ex = connection.QueryFirstOrDefault<FoodItemData>(query);

			

			ex.item_name = foodItem.item_name;
			ex.item_price = foodItem.item_price;
			ex.category_id = foodItem.category_id;

			query = $@"UPDATE Fooditem SET item_name = @item_name, item_price = @item_price, category_id = @category_id
              WHERE item_id = {id}";

			var parameters = new DynamicParameters();
			parameters.Add("@item_name", ex.item_name);
			parameters.Add("@item_price", ex.item_price);
			parameters.Add("@category_id", ex.category_id);

			var food = await connection.QueryAsync<FoodItemData>(query, parameters);
			return Ok();
		}

	}
}
