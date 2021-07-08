using System;
using System.Reflection;

namespace Util
{
    public class EntityHelper
    {
        #region 1.属性
        /// <summary>
        /// 获取实体属性
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static object GetFieldValue(object entity, string fieldName)
        {
            object fieldValue = string.Empty;
            Type type = entity.GetType();

            object innerEntity = entity;

            PropertyInfo prop = null;
            try
            {
                if (entity is Newtonsoft.Json.Linq.JObject)
                    return (entity as Newtonsoft.Json.Linq.JObject).GetValue(fieldName);

                string[] sNames = fieldName.Split('.');
                for (int i = 0; i < sNames.Length; i++)
                {
                    if (sNames.Length <= 1)
                        break;
                    if (string.IsNullOrEmpty(sNames[i]))
                        continue;
                    prop = type.GetProperty(sNames[i]);
                    type = prop.PropertyType;
                    innerEntity = prop.GetValue(entity, null);
                    if (i == sNames.Length - 2)
                    {
                        fieldName = sNames[sNames.Length - 1];
                        break;
                    }
                }
                prop = type.GetProperty(fieldName);
                fieldValue = prop.GetValue(innerEntity, null);
            }
            catch { }

            return fieldValue;
        }

        /// <summary>
        /// 获取静态变量/属性的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static object GetStaticFieldValue<T>(string fieldName)
        {
            object value = null;
            Type type = typeof(T);

            try
            {
                BindingFlags flag = BindingFlags.Static | BindingFlags.NonPublic;
                FieldInfo fieldInfo = type.GetField(fieldName, flag);
                if (fieldInfo != null)
                    value = fieldInfo.GetValue(null);
                else
                {
                    var propertyInfo = type.GetProperty(fieldName, flag);
                    if (propertyInfo != null)
                        value = propertyInfo.GetValue(null, null);
                }
            }
            catch { }

            return value;
        }

        /// <summary>
        /// 给实体字段赋值
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static object SetFieldValue(object entity, string fieldName, object fieldValue)
        {
            Type type = entity.GetType();
            var propertyInfo = type.GetProperty(fieldName);
            bool flag = false;
            //将string转换成值类型成功后也可以赋值
            if (fieldValue != null && fieldValue is string && propertyInfo.PropertyType.Name.ToLower() != "string")
            {
                MethodInfo tryParseMethod = null;
                Type parseType = propertyInfo.PropertyType;
                if (propertyInfo.PropertyType.Name == "Nullable`1")
                    parseType = parseType.GetGenericArguments()[0];

                foreach (var method in parseType.GetMethods())
                {
                    if (method.Name == "TryParse" && method.GetParameters().Length == 2)
                    {
                        tryParseMethod = method;
                        break;
                    }
                }
                if (tryParseMethod != null)
                {
                    try
                    {
                        var val = Convert.ChangeType(fieldValue.ToString().Trim(), parseType);
                        propertyInfo.SetValue(entity, val, null);

                        flag = true;
                    }
                    catch
                    {
                        string fieldDisplayName = fieldName;
                        throw new InvalidCastException(string.Format("为“{0}”所指定的值“{1}”数据类型不正确，必须是“{2}”类型的，请检查！"
                            , fieldDisplayName
                            , (fieldValue ?? "").ToString()
                            , propertyInfo.PropertyType.Name));
                    }
                }
            }

            if (!flag) propertyInfo.SetValue(entity, fieldValue, null);

            return entity;
        }

        /// <summary>
        /// 获取字段属性信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static PropertyInfo GetFieldProperty(Type entityType, string fieldName)
        {
            PropertyInfo prop = null;
            try
            {
                Type type = entityType;
                string[] sNames = fieldName.Split('.');
                for (int i = 0; i < sNames.Length; i++)
                {
                    if (sNames.Length <= 1)
                        break;
                    if (string.IsNullOrEmpty(sNames[i]))
                        continue;
                    type = type.GetProperty(sNames[i]).PropertyType;
                    if (i == sNames.Length - 2)
                    {
                        fieldName = sNames[sNames.Length - 1];
                        break;
                    }
                }
                prop = type.GetProperty(fieldName);
            }
            catch { }
            return prop;
        }

        /// <summary>
        /// 获取类所属字段特性
        /// </summary>
        /// <param name="entityType">类</param>
        /// <param name="fieldName">字段</param>
        /// <param name="attrType">特性类型</param>
        /// <returns></returns>
        public static Attribute GetFieldAttribute(Type entityType, string fieldName, Type attrType)
        {
            Attribute attr = null;
            PropertyInfo prop = GetFieldProperty(entityType, fieldName);
            if (prop != null)
            {
                attr = Attribute.GetCustomAttribute(prop, attrType, false);
            }
            else
            {
                var field = entityType.GetField(fieldName);
                attr = Attribute.GetCustomAttribute(field, attrType, false);
            }
            return attr;
        }

        /// <summary>
        /// GetPropertyDefaultFormat
        /// </summary>
        /// <param name="type"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string GetPropertyDefaultFormat(Type type, ref string format)
        {
            switch (type.Name)
            {
                case "Decimal":
                case "Single":
                case "Double":
                    format = "{0:N2}";
                    break;
                case "DateTime":
                    format = "{0:yyyy-MM-dd}";
                    break;
                case "Nullable`1":
                    GetPropertyDefaultFormat(type.GetGenericArguments()[0], ref format);
                    break;
            }
            return format;
        }
        #endregion

        #region 2.特性
        /// <summary>
        /// 获取类所属字段特性
        /// </summary>
        /// <param name="entityType">类</param>
        /// <param name="fieldName">字段</param>
        /// <param name="attrType">特性类型</param>
        /// <returns></returns>
        public static Attribute GetFieldAttribute(PropertyInfo prop, Type attrType)
        {
            Attribute attr = null;
            if (prop != null)
            {
                attr = Attribute.GetCustomAttribute(prop, attrType, false);
            }
            return attr;
        }

        public static string GetEnumDescription(Enum enumValue)
        {
            string str = enumValue.ToString();
            System.Reflection.FieldInfo field = enumValue.GetType().GetField(str);
            object[] objs = field.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            if (objs == null || objs.Length == 0) return str;
            System.ComponentModel.DescriptionAttribute da = (System.ComponentModel.DescriptionAttribute)objs[0];
            return da.Description;
        }

        /// <summary>
        /// 获取类所属字段特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityType"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static T GetFieldAttribute<T>(Type entityType, string fieldName) where T : Attribute
        {
            return GetFieldAttribute(entityType, fieldName, typeof(T)) as T;
        }

        /// <summary>
        /// 获取类所属字段特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="prop"></param>
        /// <returns></returns>
        public static T GetFieldAttribute<T>(PropertyInfo prop) where T : Attribute
        {
            return GetFieldAttribute(prop, typeof(T)) as T;
        }
        #endregion

        #region 3.操作
        /// <summary>
        /// 复制对象到新实体(深度复制)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static T DeepClone<T>(T entity) where T : class
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(Newtonsoft.Json.JsonConvert.SerializeObject(entity));
            }
            catch
            {
                throw new Exception("复制对象失败!");
            }
        }
        #endregion
    }
}
