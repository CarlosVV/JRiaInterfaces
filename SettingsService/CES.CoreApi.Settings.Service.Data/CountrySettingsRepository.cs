using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Tools;
using CES.CoreApi.Foundation.Data;
using CES.CoreApi.Foundation.Data.Base;
using CES.CoreApi.Foundation.Data.Utility;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Settings.Service.Business.Contract.Attributes;
using CES.CoreApi.Settings.Service.Business.Contract.Interfaces;
using CES.CoreApi.Settings.Service.Business.Contract.Models;

namespace CES.CoreApi.Settings.Service.Data
{
    public class CountrySettingsRepository: BaseGenericRepository, ICountrySettingsRepository
    {
        #region Core

        public CountrySettingsRepository(ICacheProvider cacheProvider, ILogMonitorFactory monitorFactory, IIdentityManager identityManager) 
            : base(cacheProvider, monitorFactory, identityManager, DatabaseType.ReadOnly)
        {
        }

        #endregion

        #region Public methods

        public CountrySettingsModel GetCountyrSettings(int countryId)
        {
            var request = new DatabaseRequest<CountrySettingsModel>
            {
                ProcedureName = "ol_sp_systblSetting_Country_GetByCountryID",
                IsCacheable = true,
                Parameters = new Collection<SqlParameter>()
                    .Add("@fCountryID", countryId),
                Shaper = reader => GetCountrySettingsDetails(reader)
            };

            return Get(request);
        }

        #endregion

        #region Private methods

        private static CountrySettingsModel GetCountrySettingsDetails(IDataReader reader)
        {
            var model = new CountrySettingsModel();

            var propertyList = new Dictionary<string, PropertyItem>();
            PopulatePropertyListByAttribute(model, ref propertyList);

            while (reader.Read())
            {
                var name = reader.ReadValue<string>("fName");
                PropertyItem propertyItem;
                
                if (!propertyList.TryGetValue(name, out propertyItem)) 
                    continue;
                
                var value = reader.ReadValue<object>("fVal");
                SetValue(propertyItem, value);
            }

            return model;
        }

        private static void PopulatePropertyListByAttribute(object inputObject, ref Dictionary<string, PropertyItem> propertyList)
        {
            var type = inputObject is PropertyInfo
                ? ((PropertyInfo) inputObject).PropertyType
                : inputObject.GetType();

            if (!type.IsClass)
                return;

            var fullList = type.GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();
            
            var items = from property in fullList
                from attr in property.GetCustomAttributes(typeof (CountrySettingCodeAttribute), true)
                let custAttr = (CountrySettingCodeAttribute) attr
                where !string.IsNullOrEmpty(custAttr.Code)
                select new PropertyItem
                {
                    OwnerObject = inputObject,
                    OwnerType = type,
                    PropertyInfo = property,
                    Code =  custAttr.Code
                };

            foreach (var item in items)
            {
                propertyList.Add(item.Code, item);
            }

            foreach (var item in fullList.Where(p=> !p.PropertyType.IsPrimitive))
            {
                PopulatePropertyListByAttribute(item, ref propertyList);
            }
        }

        private static void SetValue(PropertyItem propertyItem, object value)
        {
            //Convert.ChangeType does not handle conversion to nullable types
            //if the property type is nullable, we need to get the underlying type of the property
            var targetType = propertyItem.OwnerType.IsGenericType && propertyItem.OwnerType.GetGenericTypeDefinition() == typeof(Nullable<>)
                ? Nullable.GetUnderlyingType(propertyItem.PropertyInfo.PropertyType)
                : propertyItem.PropertyInfo.PropertyType;

            //Returns an System.Object with the specified System.Type and whose value is
            //equivalent to the specified object.
            //value = Convert.ChangeType(value, targetType);
            value = value.ConvertValue(targetType);

            //Set the value of the property
            propertyItem.PropertyInfo.SetValue(propertyItem.OwnerObject, value, null);

        }

        #endregion
    }

    public class PropertyItem
    {
        public object OwnerObject { get; set; }
        public PropertyInfo PropertyInfo { get; set; }
        public Type OwnerType { get; set; }
        public string Code { get; set; }
    }

}

