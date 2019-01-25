using System;
using Realms;

namespace Courier.Model
{
	/// <summary>
	/// Компания.
	/// </summary>
	public class Company : RealmObject
	{
		[PrimaryKey]
		public string Id { get; set; } = Guid.NewGuid().ToString();

		public string UserLogin { get; set; }

		public string UserPassword { get; set; }

		public string CompanyName { get; set; }

		public string Inn { get; set; }

		public string Kpp { get; set; }

		/// <summary>
		/// ИдЭДО компании. Используется, если пользователь заведен в нескольких филиалах.
		/// </summary>
		public string CompanyIdEdo { get; set; }

	}
}