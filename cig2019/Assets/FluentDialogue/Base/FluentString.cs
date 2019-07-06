using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Fluent
{
    /// <summary>
    /// All text that will be displayed in FluentDialogue should use FluentString.
    /// FluentString allows us to use implicit conversion when working with text.
    /// This allows us to specify multilingual as well as execution time evaluated text
    /// </summary>
    public partial class FluentString
    {
        /// <summary>
        /// Each language has a string associated to it
        /// </summary>
        public Dictionary<Language, string> LanguageString = new Dictionary<Language, string>();

        /// <summary>
        /// Sometimes we want to evaluate a string at runtime instead of creation time
        /// </summary>
        public Dictionary<Language, StringDelegate> LanguageStringDelegate = new Dictionary<Language, StringDelegate>();

        /// <summary>
        /// Get the value of this string based on the current active language
        /// </summary>
        public string Value
        {
            get
            {

                // Check to see if there is a language delegate, evaluate it if there is
                if (LanguageStringDelegate.ContainsKey(LanguageManager.CurrentLanguage))
                    return LanguageStringDelegate[LanguageManager.CurrentLanguage]();

                // Check if there is a string, return it if there is
                if (LanguageString.ContainsKey(LanguageManager.CurrentLanguage))
                    return LanguageString[LanguageManager.CurrentLanguage];

                // 
                return "No string for current language";
            }
            set
            {
                Debug.Log("Don't set FluentString string values");
            }
        }

        /// <summary>
        /// Conversion from string to FluentString
        /// </summary>
        /// <param name="s">String, this will set just the english language string</param>
        public static implicit operator FluentString(string s)
        {
            FluentString fluentString = new FluentString();
            fluentString.LanguageString[Language.English] = s;
            return fluentString;
        }

        /// <summary>
        /// Conversion from a languageString to a FluentString
        /// </summary>
        /// <param name="languageString"></param>
        public static implicit operator FluentString(object[] languageString)
        {
            FluentString ret = new FluentString();

            string errorString = "You are trying to specify multilingual text. There needs to be a Language enum for each and every string or StringDelegate, eg: (Language.English,\"SomeText\", Language.Spanish, \"Spanish Text\"";
            if (languageString.Length % 2 != 0)
            {
                Debug.LogError(errorString);
                return ret; ;
            }

            for (int i = 0; i < languageString.Length; i += 2)
            {
                // It needs to be Language/String or Language/StringDelegate pairs
                if (languageString[i].GetType() != typeof(Language) || !(languageString[i + 1] is string || languageString[i + 1] is StringDelegate))
                {
                    Debug.LogError(errorString);
                    return ret;
                }

                if (languageString[i + 1] is string)
                    ret.LanguageString.Add((Language)languageString[i], languageString[i + 1] as string);

                if (languageString[i + 1] is StringDelegate)
                    ret.LanguageStringDelegate.Add((Language)languageString[i], languageString[i + 1] as StringDelegate);
            }

            return ret;
        }

        public static implicit operator string(FluentString fluentString)
        {
            return fluentString.Value;
        }

        private FluentString()
        {
        }

        public FluentString(StringDelegate stringDelegate)
        {
            LanguageStringDelegate[Language.English] = stringDelegate;
        }

        public static string Join(string separator, FluentString[] strings)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < strings.Length; i++)
            {
                sb.Append(strings[i]);
                if (i < strings.Length - 1)
                    sb.Append(separator);
            }
            return sb.ToString();
        }

        public static FluentString[] FromStringArray(string[] strings)
        {
            return Array.ConvertAll<string, FluentString>(strings, b => b);
        }

    }

    public partial class FluentScript : MonoBehaviour
    {
        /// <summary>
        /// Having to evaluate a string at execute time is so frequent that there is a method to do this 
        /// </summary>
        /// <param name="stringDelegate"></param>
        /// <returns></returns>
        public static FluentString Eval(StringDelegate stringDelegate)
        {
            return new FluentString(stringDelegate);
        }

    }
}
