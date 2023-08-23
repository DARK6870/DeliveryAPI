using Azure;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TEST.Data;
using TEST.Models;

namespace TEST.Endpoints.FoodCategory
{
	[Route("foodcategory")]
	public class EditFoodCategory : EndpointBaseAsync
		.WithRequest<int, string>
		.WithActionResult<FoodCategoryModel>
	{

		private readonly DapperContext ctx;

		public EditFoodCategory(DapperContext ctx)
		{
			this.ctx = ctx;
		}

		[HttpPost("edit/{id}", Name = "EditFoodCategory")]
		[SwaggerOperation(
		Summary = "Update info about Food Category",
		Description = "This API will update information about FoodCategory by Id",
		OperationId = "EditFoodCategory",
		Tags = new[] { "FoodCategory Endpoint" })]
		public override async Task<ActionResult<FoodCategoryModel>> HandleAsync(int id, [FromBody] string foodname, CancellationToken cancellationToken)
		{
			string query = $"UPDATE FoodCategory SET food_name = '{foodname}' WHERE category_id = {id}";
			using var connection = ctx.CreateConnection();

			var foodcategory = await connection.QueryAsync<FoodCategoryModel>(query);
			return Ok();
		}
	}
}
