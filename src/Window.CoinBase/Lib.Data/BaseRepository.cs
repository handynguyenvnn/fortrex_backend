using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Xml;
using System.ComponentModel;
using System.Collections;
using System.Globalization;

namespace Lib.Data
{
    public abstract class BaseRepository
    {
        protected Database mdb;
        protected internal Database db
        {
            get
            {
                if (mdb == null)
                {
                    DatabaseProviderFactory factory = new DatabaseProviderFactory();
                    mdb = factory.Create("MiningHash");
                }
                return mdb;
            }
        }

        protected readonly Database _db;
        public BaseRepository()
        {
            this._db = db;
        }

        protected T XmlNodeToModel<T>(XmlNode xmlNode, params string[] includePropeties)
            where T : class, new()
        {
            return (T)XmlNodeToModel(xmlNode, typeof(T), includePropeties);
        }


        protected object XmlNodeToModel(XmlNode xmlNode, Type type, params string[] includePropeties)
        {
            if (string.Compare(xmlNode.Name, "root", 0) == 0 && xmlNode.ChildNodes.Count == 1)
                xmlNode = xmlNode.ChildNodes[0];

            if (type.IsPrimitive || type == typeof(string) || type == typeof(DateTime))
            {
                if (xmlNode.Attributes.Count > 0)
                    return GetValue(xmlNode.Attributes[0].Value, type);
                return GetValue(xmlNode.InnerText, type);
            }

            var model = Activator.CreateInstance(type);

            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                if (property.CanWrite && (includePropeties.Length == 0 || includePropeties.Contains(property.Name)))
                {
                    if (xmlNode.Attributes[property.Name] != null)
                    {
                        var value = GetValue(xmlNode.Attributes[property.Name].Value, property.PropertyType);
                        if (value != null)
                        {
                            property.SetValue(model, value, null);
                        }
                    }
                    else
                    {
                        var childNode = xmlNode.SelectSingleNode(property.Name);
                        if (childNode != null)
                        {
                            if (property.PropertyType.IsGenericType && property.PropertyType.GetInterface("IList") != null)
                            {
                                if (childNode.ChildNodes.Count > 0)
                                {
                                    var value = (IList)Activator.CreateInstance(property.PropertyType);
                                    var itemType = property.PropertyType.GetGenericArguments()[0];
                                    foreach (XmlNode item in childNode.ChildNodes)
                                    {
                                        value.Add(XmlNodeToModel(item, itemType));
                                    }
                                    property.SetValue(model, value, null);
                                }
                            }
                            else if (childNode.ChildNodes.Count == 1)
                            {

                                var value = XmlNodeToModel(childNode.FirstChild, property.PropertyType);
                                if (value != null)
                                    property.SetValue(model, value, null);
                            }
                        }
                    }
                }
            }

            return model;
        }

        protected object GetValue(string value, Type type, params string[] properties)
        {
            if (type.IsGenericType && type.GetInterface("IList") != null)
            {
                try
                {
                    var doc = new XmlDocument();
                    doc.LoadXml(value);
                    if (doc.FirstChild.ChildNodes.Count > 0)
                    {
                        var result = (IList)Activator.CreateInstance(type);
                        var itemType = type.GetGenericArguments()[0];
                        foreach (XmlNode item in doc.FirstChild.ChildNodes)
                        {
                            result.Add(XmlNodeToModel(item, itemType, properties));
                        }
                        return result;
                    }
                    else
                        return null;
                }
                catch
                {
#if DEBUG
                    throw;
#endif
                    return null;
                }
            }

            if (TypeDescriptor.GetConverter(type).CanConvertFrom(typeof(string)))
            {
                try
                {
                    if (type.FullName == typeof(bool).FullName
                        || (type.IsGenericType && type.GenericTypeArguments.Count() == 1 && type.GenericTypeArguments[0].FullName == typeof(bool).FullName))
                    {
                        if (value == "1" || string.Compare(value, "True", StringComparison.OrdinalIgnoreCase) == 0)
                            return true;
                        else
                            return false;
                    }

                    return To(value, type);
                }
                catch
                {
#if DEBUG
                    throw;
#endif
                    return null;
                }
            }

            if (type.IsClass)
            {
                var doc = new XmlDocument();
                doc.LoadXml(value);
                return XmlNodeToModel(doc.FirstChild, type, properties);
            }

            return null;
        }

        protected T GetValue<T>(string value, params string[] properties)
            where T : new()
        {
            var type = typeof(T);
            var result = GetValue(value, type, properties);

            if (result != null)
                return (T)result;

            return default(T);
        }

        public object To(object value, Type destinationType)
        {
            return To(value, destinationType, CultureInfo.InvariantCulture);
        }

        public object To(object value, Type destinationType, CultureInfo culture)
        {
            if (value != null)
            {
                Type sourceType = value.GetType();

                TypeConverter destinationConverter = GetFCustomTypeConverter(destinationType);
                TypeConverter sourceConverter = GetFCustomTypeConverter(sourceType);
                if (destinationConverter != null && destinationConverter.CanConvertFrom(value.GetType()))
                    return destinationConverter.ConvertFrom(null, culture, value);
                if (sourceConverter != null && sourceConverter.CanConvertTo(destinationType))
                    return sourceConverter.ConvertTo(null, culture, value, destinationType);
                if (destinationType.IsEnum && value is int)
                    return Enum.ToObject(destinationType, (int)value);
                if (!destinationType.IsAssignableFrom(value.GetType()))
                    return Convert.ChangeType(value, destinationType, culture);
            }
            return value;
        }

        public TypeConverter GetFCustomTypeConverter(Type type)
        {
            if (type == typeof(List<int>))
                return new GenericListTypeConverter<int>();
            if (type == typeof(List<decimal>))
                return new GenericListTypeConverter<decimal>();
            if (type == typeof(List<string>))
                return new GenericListTypeConverter<string>();
            return TypeDescriptor.GetConverter(type);
        }
    }
}
