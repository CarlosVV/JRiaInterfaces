using System;
using System.Collections;
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
using CES.CoreApi.Settings.Service.Data.Models;

namespace CES.CoreApi.Settings.Service.Data.Repositories
{
    public class CountrySettingsRepository : BaseGenericRepository, ICountrySettingsRepository
    {
        #region Core

        public CountrySettingsRepository(ICacheProvider cacheProvider, ILogMonitorFactory monitorFactory,
            IIdentityManager identityManager)
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
                Shaper = reader => GetCountrySettingsDetails(reader, countryId)
            };

            return Get(request);
        }

        #endregion

        #region Private methods

        private static CountrySettingsModel GetCountrySettingsDetails(IDataReader reader, int countryId)
        {
            var model = new CountrySettingsModel {CountryId = countryId};

            var propertyList = new Dictionary<string, PropertyItem>();
            PopulatePropertyListByAttribute(model, ref propertyList);

            while (reader.Read())
            {
                var name = reader.ReadValue<string>("fName");
                if (name == null)
                    continue;

                PropertyItem propertyItem;

                if (!propertyList.TryGetValue(name, out propertyItem))
                    continue;

                var value = reader.ReadValue<object>("fVal");
                if (value == null)
                    continue;

                SetValue(propertyItem, value);
            }

            return model;
        }

        private static void PopulatePropertyListByAttribute(object inputObject,
            ref Dictionary<string, PropertyItem> propertyList)
        {
            var type = inputObject.GetType();

            if (!type.IsClass)
                return;

            var fullList = type.GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();

            var items = from property in fullList
                from attribute in property.GetCustomAttributes(typeof (CountrySettingAttribute), true)
                let settingAttribute = (CountrySettingAttribute) attribute
                where !string.IsNullOrEmpty(settingAttribute.Code)
                select new PropertyItem
                {
                    OwnerObject = inputObject,
                    OwnerType = type,
                    PropertyInfo = property,
                    Code = settingAttribute.Code,
                    IsList = settingAttribute.IsList,
                    ListDelimiter = settingAttribute.ListDelimiter
                };

            foreach (var item in items)
            {
                propertyList.Add(item.Code, item);
            }

            var possibleSettingProperties = fullList.Where(item => IsPossibleSettingProperty(item, inputObject));

            foreach (var item in possibleSettingProperties)
            {
                PopulatePropertyListByAttribute(item.GetValue(inputObject, null), ref propertyList);
            }
        }

        private static void SetValue(PropertyItem propertyItem, object value)
        {
            //Convert.ChangeType does not handle conversion to nullable types
            //if the property type is nullable, we need to get the underlying type of the property
            var targetType = propertyItem.OwnerType.IsGenericType &&
                             propertyItem.OwnerType.GetGenericTypeDefinition() == typeof (Nullable<>)
                ? Nullable.GetUnderlyingType(propertyItem.PropertyInfo.PropertyType)
                : propertyItem.PropertyInfo.PropertyType;

            if (targetType == typeof(string) || !targetType.IsList())
            {
                value = value.ConvertValue(targetType);
                propertyItem.PropertyInfo.SetValue(propertyItem.OwnerObject, value, null);
            }
            else
            {
                var valueList = value.ToString().Split(propertyItem.ListDelimiter.ToCharArray());
                var itemType = targetType.GetGenericArguments()[0];
                //propertyItem.PropertyInfo.SetValue(propertyItem.OwnerObject,
                //    valueList.Select(p => Convert.ChangeType(p, itemType)).ToList(), null);

                var list = (IList) Activator.CreateInstance(typeof (List<>).MakeGenericType(itemType));

                propertyItem.PropertyInfo.SetValue(propertyItem.OwnerObject, list, null);

                for (var i = 0; i < valueList.Length; i++)
                {
                    list.Add(Convert.ChangeType(valueList[i], itemType));
                }

                //object obj = Activator.CreateInstance(type);
                //type.GetProperty("Bar").SetValue(obj, "abc", null);
                //list.Add(obj);

            }

            //Set the value of the property
            

        }

        public static bool IsPossibleSettingProperty(PropertyInfo info, object owner)
        {
            var type = info.PropertyType;

            return !type.IsEnum &&
                   !type.IsPrimitive &&
                   type != typeof (string) &&
                   type != typeof (decimal) &&
                   type != typeof (DateTime) &&
                   type != typeof (DateTimeOffset) &&
                   type != typeof (TimeSpan) &&
                   !type.IsList();
        }
        
        #endregion
    }
}