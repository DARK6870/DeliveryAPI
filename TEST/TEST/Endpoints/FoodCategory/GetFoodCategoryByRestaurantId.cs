using Azure;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TEST.Data;
using TEST.Models;

namespace TEST.Endpoints.FoodCategory
{
	[Route("foodcategory/GetByRestaurant")]
	public class GetById : EndpointBaseAsync
		.WithRequest<int>
		.WithActionResult<FoodCategoryModel>
	{
		private readonly DapperContext ctx;

		public GetById(DapperContext ctx)
		{
			this.ctx = ctx;
		}

		[HttpGet("{id}", Name = "GetFoodCategoryById")]
		[SwaggerOperation(
		Summary = "Get Food Category By Id",
		Description = "This API return information about FoodCategory with a specific Id",
		OperationId = "GetFoodCategoryById",
		Tags = new[] { "FoodCategory Endpoint" })]
		public override async Task<ActionResult<FoodCategoryModel>> HandleAsync(int id, CancellationToken cancellationToken)
		{
			string query = $"SELECT * FROM FoodCategory Where restaurant_id = {id}";

			using var connection = ctx.CreateConnection();

			IEnumerable<FoodCategoryModel> res = await connection.QueryAsync<FoodCategoryModel>(query);

			if (res == null || !res.Any())
			{
				return NotFound();
			}

			return Ok(res);
		}
	}
}
