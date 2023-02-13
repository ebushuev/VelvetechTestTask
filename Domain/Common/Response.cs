using Domain.Enums;

namespace Domain.Common
{
	public class Response
	{
		public ItemState State { get; set; }

		public Response(ItemState state)
		{
			State = state;
		}
	}

	public class Response<T> : Response
	{
		public T Result { get; set; }

		public Response(T result, ItemState state) : base(state)
		{
			Result = result;
		}
	}
	
}