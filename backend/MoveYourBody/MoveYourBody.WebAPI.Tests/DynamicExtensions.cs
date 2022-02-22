using System;
using System.Collections.Generic;
using System.Text;

namespace MoveYourBody.WebAPI.Tests
{
    public static class DynamicExtensions
    {
        public static T GetPropertyValue<T>(this object value, string propertyName)
        {
            var propertyInfo = value.GetType().GetProperty(propertyName);
            return (T)propertyInfo.GetValue(value, null);
        }
    }
}