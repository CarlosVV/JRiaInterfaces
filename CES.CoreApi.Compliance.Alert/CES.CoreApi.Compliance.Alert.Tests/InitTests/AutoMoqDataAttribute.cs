using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Xunit2;
using System;

namespace CES.CoreApi.Compliance.Alert.Tests
{
	public class AutoMoqDataAttribute: AutoDataAttribute
	{
		public AutoMoqDataAttribute() : this(null, null, null)
		{ }

		public AutoMoqDataAttribute(Type customization) : this(customization, null, null)
		{
		}

		public AutoMoqDataAttribute(Type customization1, Type customization2, Type customization3) : base()
		{
			AddDefaultCustommizations();
			AddCustomization(customization1);
			AddCustomization(customization2);
			AddCustomization(customization3);
		}

		private void AddDefaultCustommizations()
		{
			Fixture.Customize(new AutoMoqCustomization());
		}

		private void AddCustomization(Type customization)
		{
			if (customization != null)
			{
				var customizationInstance = (ICustomization)Activator.CreateInstance(customization);
				Fixture.Customize(customizationInstance);
			}
		}

	}
}
