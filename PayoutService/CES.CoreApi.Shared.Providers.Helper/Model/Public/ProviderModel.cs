using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using CES.CoreApi.Shared.Providers.Helper.Model.Public.Enums;
using CES.CoreApi.Shared.Providers.Helper.Xsd;


namespace CES.CoreApi.Shared.Providers.Helper.Model.Public
{
    public  class ProviderModel
    {
        private List<KeyValuePair<ConfigurationProviderKeys, object>> configurations = new List<KeyValuePair<ConfigurationProviderKeys, object>>();
        private Requirements configRequiredFields = new Requirements();

        public int ProviderID { get; set; }
        public int ProviderTypeID { get; set; }

        public string Name { get; set; }

        

        public void AddConfiguration<T>(ConfigurationProviderKeys key, object value)
        {
            var convertValue = value.ConvertValue<T>(true);
            this.configurations.Add(new KeyValuePair<ConfigurationProviderKeys, object>(key, convertValue));
        }
        public void LoadConfigurationFromJson(string json)
        {
            try
            {
                configurations = JsonConvert.DeserializeObject<List<KeyValuePair<ConfigurationProviderKeys, object>>>(json);

                //Conversion complex types
                var jsonRequiredFields = GetConfigurationString(ConfigurationProviderKeys.RequiredFields);
                if (!string.IsNullOrEmpty(jsonRequiredFields))
                {
                    try
                    {
                        var requiredFields = JsonConvert.DeserializeObject<Requirements>(jsonRequiredFields);
                        configurations.RemoveAll(p => p.Key == ConfigurationProviderKeys.RequiredFields);
                        AddConfiguration<Requirements>(ConfigurationProviderKeys.RequiredFields, requiredFields);
                    }
                    catch
                    {

                    }
                }

              
            }
            catch
            {
                throw new Exception("Unable to load configuration provider");
            }
        }
        public List<KeyValuePair<ConfigurationProviderKeys, object>> GetAllConfiguration()
        {
            return configurations;
        }
        public T GetConfiguration<T>(ConfigurationProviderKeys key) 
        {
            T result = default(T);
            
            var value = configurations.FindLast(p => p.Key.Equals(key)).Value ;
            try
            {
                result = (T)Convert.ChangeType(value, typeof(T));
            }
            catch
            {
                //Could not convert.  Pass back default value...
                //result = value.ToString();
                result = (T)Convert.ChangeType(value, typeof(T));
            }

            return result;

        }
        public string GetConfigurationString(ConfigurationProviderKeys key)
        {

            var result = string.Empty;
            var value = configurations.FindLast(p => p.Key.Equals(key)).Value;

            try
            {
                result = value.ToString();
            }
            catch
            {

                result = string.Empty;
            }

            return result;

        }
        public void UpdateConfiguration<T>(ConfigurationProviderKeys key, object value)
        {
            configurations.RemoveAll(pair => pair.Key.Equals(key));
            var convertValue = value.ConvertValue<T>(true);
            AddConfiguration<T>(key, convertValue);
        }
        public string GetConfigurationJson()
        {
            return JsonConvert.SerializeObject(configurations);
        }        
        public string GetXmlConfigRequiredFields()
        {
            return GetConfigRequiredFields().Serialize();
        }
        public void AddConfigRequiredField(string fieldName, string requirementType)
        {
            var _configRequiredFields = GetConfigRequiredFields();
            _configRequiredFields.Field.RemoveAll(f => f.FieldName == fieldName);
            _configRequiredFields.Field.Add(new fieldType() {  FieldName = fieldName, RequirementType = requirementType });
            configRequiredFields = _configRequiredFields;

            UpdateConfiguration<Requirements>(ConfigurationProviderKeys.RequiredFields, configRequiredFields);

        }
        private Requirements GetConfigRequiredFields()
        {
            var requiredFields = new Requirements();
            var jsonConfigRequiredFields = GetConfiguration<Requirements>(ConfigurationProviderKeys.RequiredFields);

            if (jsonConfigRequiredFields == null) return requiredFields;
            try
            {
                return jsonConfigRequiredFields;
            }
            catch
            {
                return requiredFields;
            }

        }

    }
}
