using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace CES.CoreApi.Common.Tools
{
    public static class EnumTools
    {
        /// <summary>
        /// Gets attribute from enum field
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TExpected"></typeparam>
        /// <param name="enumeration"></param>
        /// <param name="expression"></param>
        /// Sample of call: string description = targetLevel.GetAttributeValue<DescriptionAttribute, string>(x => x.Description);
        /// <returns></returns>
        public static TExpected GetAttributeValue<T, TExpected>(this Enum enumeration, Func<T, TExpected> expression)
            where T : Attribute
        {
            var attribute = enumeration
                .GetType()
                .GetMember(enumeration.ToString())[0]
                .GetCustomAttributes(typeof (T), false)
                .Cast<T>()
                .SingleOrDefault();

            return attribute == null
                ? default(TExpected)
                : expression(attribute);
        }

        public static IEnumerable<TOut> GetValues<TEnum, TOut>()
        {
            return Enum.GetValues(typeof (TEnum)).Cast<TOut>();
        }

        public static T GetEnumValueFromDescription<T>(this string description)
        {
            var type = typeof(T);
            if (!type.IsEnum)
                throw new ArgumentException();
            
            var fields = type.GetFields();

            var field = fields
                .SelectMany(f => f.GetCustomAttributes(typeof (DescriptionAttribute), false),
                    (f, a) => new {Field = f, Att = a})
                .SingleOrDefault(a => ((DescriptionAttribute) a.Att)
                    .Description == description);

            return field == null
                ? default(T)
                : (T) field.Field.GetRawConstantValue();
        }
    }
}
