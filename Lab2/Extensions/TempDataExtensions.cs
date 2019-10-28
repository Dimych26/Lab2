using Lab2.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Extensions
{
    public static class TempDataExtensions
    {
        public static void Set<T>(this ITempDataDictionary tempData, string KeyName, T value)
        {
            tempData[KeyName] = JsonConvert.SerializeObject(value);
        }

        public static T Get<T>(this ITempDataDictionary tempData, string KeyName)
        {
            var value = JsonConvert.DeserializeObject<T>(KeyName);
            return value;
        }
    }
    
}
