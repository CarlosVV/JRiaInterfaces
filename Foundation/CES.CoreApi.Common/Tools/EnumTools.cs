using System;
using System.Collections.Generic;
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

    }
}
