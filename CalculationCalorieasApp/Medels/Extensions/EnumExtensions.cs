using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CalculationCalorieasApp.Medels.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum enm)
        {
            FieldInfo fi = enm.GetType().GetField(enm.ToString());

            DescriptionAttribute attribute =
                fi.GetCustomAttribute(typeof(DescriptionAttribute), false)
                as DescriptionAttribute;

            return attribute?.Description ?? enm.ToString();
        }

        public static Enum ToEnum(this string enumDescriptionValue, Type targetType)
        {
            var fieldInfos = targetType.GetFields();
            var fieldsDescriptionsDictionary = fieldInfos.Select(fi =>
                (fi, (fi.GetCustomAttribute(typeof(DescriptionAttribute), false)
                as DescriptionAttribute)?.Description))
                .Where(fieldInfoDescriptionPair =>
                    fieldInfoDescriptionPair.Description != null)
                .ToDictionary(keySelector: x => x.Description,
                    elementSelector: x => (Enum.GetValues(targetType).Cast<Enum>())
                    .First(enmVal => enmVal.ToString() == x.fi.Name));

            if (fieldsDescriptionsDictionary.TryGetValue(enumDescriptionValue, out var enumValue))
                return enumValue;

            return default;
        }

        public static TEnum ToEnum<TEnum>(this string enumDescriptionValue) where TEnum : Enum
        {
            return (TEnum)enumDescriptionValue.ToEnum(typeof(TEnum));
        }
    }
}
