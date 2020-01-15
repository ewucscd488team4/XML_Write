using SAUSALibrary.Defaults;
using SAUSALibrary.Models;
using System;
using System.Collections.Generic;

namespace SOUSALibrary.DataProcessing
{
    public class GetThemes
    {
        /// <summary>
        /// Returns a List of theme models based on the theme enum in defaults
        /// </summary>
        /// <returns>AAA</returns>
        public static List<ThemeModel> GetThemesList()
        {
            List<ThemeModel> list = new List<ThemeModel>();
            foreach (string name in Enum.GetNames(typeof(ThemeEnums)))
            {
                list.Add(new ThemeModel() { ThemeValue = name });
            }


            return list;
        }
    }
}
