namespace DA.Kochbuch.WebAPI.Exceptions
{
	/// <ChangeLog>
	/// <Create Datum="29.07.2024" Entwickler="DA" />
	/// </ChangeLog>
	/// <summary>
	/// Will be thrown if an Object cannot be found
	/// </summary>
	public class ObjectNotFoundException : Exception
	{
		private string? _entityName;
		private object? _id;
		public ObjectNotFoundException(string entityName, object? id)
		{
			_entityName = entityName;
			_id = id;
		}

		public override string ToString()
		{
			return $"Entity {_entityName} with ID {_id} not found.";
		}
	}
}