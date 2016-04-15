using CES.CoreApi.Foundation.Contract.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;



namespace CES.CoreApi.Foundation.Contract.Models
{
	public class Application : IApplication
	{
		public Application(int id, string name, bool isActive)
		{
			if (id <= 0)
				throw new ArgumentOutOfRangeException("id", string.Format(CultureInfo.InvariantCulture, "Invalid application ID = '{0}'", id));
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name");

			Id = id;
			Name = name;
			IsActive = isActive;

			Configuration = new List<ApplicationConfiguration>();
			Operations = new Collection<ServiceOperation>();
		}

		public int Id { get; private set; }

		public string Name { get; private set; }

		public bool IsActive { get; private set; }

		public ICollection<ApplicationConfiguration> Configuration { get; protected set; }

		public ICollection<ServiceOperation> Operations { get; protected set; }

	}
}
