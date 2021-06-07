using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace Util
{
    public class ConvertHelper
    {
        #region 数据类型转换
        /// <summary>
        /// 转换成int
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ConverToInt(object obj)
        {
            if (Object.Equals(obj, DBNull.Value) || obj == null || Object.Equals(obj, string.Empty))
                return 0;
            else
                return Convert.ToInt32(obj);
        }

        /// <summary>
        /// 转换成int16
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Int16 ConverToInt16(object obj)
        {
            if (Object.Equals(obj, DBNull.Value) || obj == null || Object.Equals(obj, string.Empty))
                return 0;
            else
                return Convert.ToInt16(obj);
        }

        /// <summary>
        /// 转换成int64
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Int64 ConverToInt64(object obj)
        {
            if (Object.Equals(obj, DBNull.Value) || obj == null || Object.Equals(obj, string.Empty))
                return 0;
            else
                return Convert.ToInt64(obj);
        }

        /// <summary>
        /// 转换成float
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static float ConverToFloat(object obj)
        {
            if (Object.Equals(obj, DBNull.Value) || obj == null || Object.Equals(obj, string.Empty))
                return 0;
            else
                return Convert.ToSingle(obj);
        }

        /// <summary>
        /// 转换成double
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double ConverToDouble(object obj)
        {
            if (Object.Equals(obj, DBNull.Value) || obj == null || Object.Equals(obj, string.Empty))
                return 0;
            else
                return Convert.ToDouble(obj);
        }

        /// <summary>
        /// 转换成decimal
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal ConverToDecimal(object obj)
        {
            if (Object.Equals(obj, DBNull.Value) || obj == null || Object.Equals(obj, string.Empty))
                return 0M;
            else
                return Convert.ToDecimal(obj);
        }

        /// <summary>
        /// 转换成string
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ConverToString(object obj)
        {
            if (Object.Equals(obj, DBNull.Value) || obj == null)
                return string.Empty;
            else
                return obj.ToString();
        }

        /// <summary>
        /// 转换成DataTiem
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime? ConverToDateTime(object obj)
        {
            if (Object.Equals(obj, DBNull.Value) || obj == null || Object.Equals(obj, string.Empty))
                return null;
            else
                return Convert.ToDateTime(obj);
        }

        /// <summary>
        /// 字符串转枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="charValue"></param>
        /// <returns></returns>
        public static T ConvertToEnum<T>(string charValue) where T : struct, IComparable, IConvertible, IFormattable
        {
            if (string.IsNullOrEmpty(charValue) || charValue.Length > 1) throw new Exception("待转换为枚举的字符串格式不正确!");
            return (T)Enum.Parse(typeof(T), ((int)charValue.ToCharArray()[0]).ToString());
        }
        #endregion

        #region 实体与DataTable转换
        public static DataTable ConvertTo<T>(IList<T> list)
        {
            DataTable table = CreateTable<T>();
            Type entityType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (T item in list)
            {
                DataRow row = table.NewRow();

                foreach (PropertyDescriptor prop in properties)
                {
                    var val = prop.GetValue(item);
                    if (val == null) val = DBNull.Value;
                    row[prop.Name] = val;
                }

                table.Rows.Add(row);
            }

            return table;
        }

        public static List<T> ConvertTo<T>(IList<DataRow> rows)
        {
            List<T> list = null;

            if (rows != null)
            {
                list = new List<T>();

                foreach (DataRow row in rows)
                {
                    T item = CreateItem<T>(row);
                    list.Add(item);
                }
            }

            return list;
        }

        public static List<T> ConvertTo<T>(DataTable table)
        {
            if (table == null)
            {
                return null;
            }

            List<DataRow> rows = new List<DataRow>();

            foreach (DataRow row in table.Rows)
            {
                rows.Add(row);
            }

            return ConvertTo<T>(rows);
        }

        public static T CreateItem<T>(DataRow row)
        {
            T entity = default(T);
            if (row != null)
            {
                entity = Activator.CreateInstance<T>();
                string typeName = string.Empty;

                foreach (DataColumn column in row.Table.Columns)
                {
                    PropertyInfo prop = entity.GetType().GetProperty(column.ColumnName);
                    if (prop == null) continue;
                    try
                    {
                        var value = row[column.ColumnName];
                        if (value == DBNull.Value || value == null) continue;
                        if (prop.PropertyType.IsEnum)
                        {
                            string val = value.ToString();
                            if (value.GetType().Equals(typeof(string)) && value.ToString().Length == 1)
                            {
                                val = ((int)Convert.ToChar(value)).ToString();
                            }
                            prop.SetValue(entity, Enum.Parse(prop.PropertyType, val), null);
                        }
                        else if (value.GetType().Equals(prop.PropertyType))//TODO:待简化
                        {
                            prop.SetValue(entity, value, null);
                        }
                        else if (prop.PropertyType == typeof(string))
                        {
                            prop.SetValue(entity, value.ToString(), null);
                        }
                        else if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(int?))
                        {
                            prop.SetValue(entity, int.Parse(value.ToString()), null);
                        }
                        else if (prop.PropertyType == typeof(byte) || prop.PropertyType == typeof(byte?))
                        {
                            prop.SetValue(entity, byte.Parse(value.ToString()), null);
                        }
                        else if (prop.PropertyType == typeof(short) || prop.PropertyType == typeof(short?))
                        {
                            prop.SetValue(entity, short.Parse(value.ToString()), null);
                        }
                        else if (prop.PropertyType == typeof(DateTime?) || prop.PropertyType == typeof(DateTime))
                        {
                            prop.SetValue(entity, DateTime.Parse(value.ToString()), null);
                        }
                        else if (prop.PropertyType == typeof(float) || prop.PropertyType == typeof(float?))
                        {
                            prop.SetValue(entity, float.Parse(value.ToString()), null);
                        }
                        else if (prop.PropertyType == typeof(double) || prop.PropertyType == typeof(double?))
                        {
                            prop.SetValue(entity, double.Parse(value.ToString()), null);
                        }
                        else if (prop.PropertyType == typeof(decimal) || prop.PropertyType == typeof(decimal?))
                        {
                            prop.SetValue(entity, decimal.Parse(value.ToString()), null);
                        }
                        else
                        {
                            prop.SetValue(entity, value, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        // You can log something here

                        // throw ex;
                    }
                }
            }

            return entity;
        }

        public static DataTable CreateTable<T>()
        {
            Type entityType = typeof(T);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (PropertyDescriptor prop in properties)
            {
                var type = prop.PropertyType;
                if (prop.PropertyType.Name.StartsWith("Nullable`"))
                    type = type.GetGenericArguments()[0];
                table.Columns.Add(prop.Name, type);
            }

            return table;
        }
        #endregion
    }
}
