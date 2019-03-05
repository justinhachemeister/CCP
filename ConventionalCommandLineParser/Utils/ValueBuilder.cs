﻿using ConventionalCommandLineParser.Exceptions;
using ConventionalCommandLineParser.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ConventionalCommandLineParser.Utils
{
    internal class ValueBuilder
    {
        private CultureInfo locale;
        private string? dateFormat;

        public ValueBuilder(CultureInfo locale, string? dateFormat)
        {
            this.locale = locale ?? throw new ArgumentNullException(nameof(locale));
            this.dateFormat = dateFormat;
        }

        private Dictionary<Type, Func<string, object>> GetDefaultParsers()
        {
            return new Dictionary<Type, Func<string, object>>
            {
                { typeof(string), s => s },
                { typeof(sbyte), s => sbyte.Parse(s, locale)},
                { typeof(sbyte?), s => sbyte.Parse(s, locale)},
                { typeof(short), s => short.Parse(s, locale)},
                { typeof(short?), s => short.Parse(s, locale)},
                { typeof(int), s => int.Parse(s, locale)},
                { typeof(int?), s => int.Parse(s, locale)},
                { typeof(long), s => long.Parse(s, locale) },
                { typeof(long?), s => long.Parse(s, locale) },
                { typeof(byte), s => byte.Parse(s, locale)},
                { typeof(byte?), s => byte.Parse(s, locale)},
                { typeof(ushort), s => ushort.Parse(s, locale)},
                { typeof(ushort?), s => ushort.Parse(s, locale)},
                { typeof(uint), s => uint.Parse(s, locale)},
                { typeof(uint?), s => uint.Parse(s, locale)},
                { typeof(ulong), s => ulong.Parse(s, locale) },
                { typeof(ulong?), s => ulong.Parse(s, locale) },
                { typeof(decimal), s => decimal.Parse(s, locale) },
                { typeof(decimal?), s => decimal.Parse(s, locale) },
                { typeof(float), s => float.Parse(s, locale) },
                { typeof(float?), s => float.Parse(s, locale) },
                { typeof(double), s => double.Parse(s, locale) },
                { typeof(double?), s => double.Parse(s, locale) },
                { typeof(char), s => s[0] },
                { typeof(char?), s => s[0] },
                { typeof(bool), s => bool.Parse(s) },
                { typeof(bool?), s => bool.Parse(s) },
                {
                    typeof(DateTime),
                    s => dateFormat != null ? DateTime.ParseExact(s, dateFormat, locale) : DateTime.Parse(s, locale)
                }
            };
        }

        public object GetValue(Type type, Argument argument)
        {
            try
            {
                var parsers = GetDefaultParsers();
                if (parsers.ContainsKey(type))
                {
                    return parsers[type](argument.Value);
                }

                if (argument.IsPotentialJson)
                {
                    return JsonConvert.DeserializeObject(argument.Value, type);
                }
            }
            catch(Exception ex)
            {
                throw new ValueParsingException($"Could not parse value \"{argument.Value}\" as {type}. See inner exception for the actual exception thrown.", ex);
            }

            throw new TypeNotSupportedException(message: "Not supported", type: type);            
        }
    }
}