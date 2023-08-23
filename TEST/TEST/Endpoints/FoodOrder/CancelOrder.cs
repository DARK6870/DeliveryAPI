using Azure;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TEST.Data;
using TEST.Models;

namespace TEST.Endpoints.FoodOrder
{
	[Route("foodorders")]
	public class CancelOrder : EndpointBaseAsync
		.WithRequest<int>
		.WithActionResult<FoodOrderModel>
	{

		private readonly DapperContext ctx;

		public CancelOrder(DapperContext ctx)
		{
			this.ctx = ctx;
		}

		[HttpPut("cancel/{id}", Name = "CancelOrder")]
		[SwaggerOperation(
		Summary = "Cancel Order By Id",
		Description = "This API will cancel order by Id",
		OperationId = "CancelOrder",
		Tags = new[] { "FoodOrder Endpoint" })]
		public override async Task<ActionResult<FoodOrderModel>> HandleAsync(int id, CancellationToken cancellationToken)
		{
			string query = "UPDATE Food_order SET status_id = 4 WHERE Food_order_id = @OrderId";
			var parameters = new { OrderId = id };

			using var connection = ctx.CreateConnection();

			var res = await connection.QueryAsync<FoodOrderModel>(query, parameters);
			return Ok();
		}
	}
}
