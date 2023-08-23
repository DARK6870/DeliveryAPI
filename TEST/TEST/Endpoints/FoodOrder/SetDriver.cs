using Azure;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TEST.Data;
using TEST.Models;

namespace TEST.Endpoints.FoodOrder
{
	[Route("foodorders")]
	public class SetDriver : EndpointBaseAsync
		.WithRequest<int, int>
		.WithActionResult<FoodOrderModel>
	{

		private readonly DapperContext ctx;

		public SetDriver(DapperContext ctx)
		{
			this.ctx = ctx;
		}

		[HttpPut("setdriver/{id}", Name = "SetDriver")]
		[SwaggerOperation(
		Summary = "Set Driver By Order Id",
		Description = "This API will set driver to Order by order_id",
		OperationId = "SetDriver",
		Tags = new[] { "FoodOrder Endpoint" })]
		public override async Task<ActionResult<FoodOrderModel>> HandleAsync(int id, int driver_id, CancellationToken cancellationToken)
		{
			string query = $"UPDATE Food_order SET driver_id = {driver_id}  WHERE Food_order_id = @OrderId";
			var parameters = new { OrderId = id };

			using var connection = ctx.CreateConnection();

			var res = await connection.QueryAsync<FoodOrderModel>(query, parameters);
			return Ok();
		}
	}
}
