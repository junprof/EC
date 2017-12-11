using System;
using System.Data;
using System.Reflection;

namespace DTcms.Common
{ 
    public class DBRowConvertor
    {
        private DataRow _dr; 
        private string predix;
        public DBRowConvertor(string ppredix, DataRow dr)
        {
            this._dr = dr;
            this.predix = ppredix;
        }
        public DBRowConvertor(DataRow dr)
        {
            this._dr=dr;
        }

        public T ConvertToEntity<T>() where T : new()
        {
            T t = new T();
            PropertyInfo[] pis = t.GetType().GetProperties();
            for (int i = 0; i < pis.Length; i++)
            {
                var property = pis[i];
                if (this.Contains(property.Name))
                {
                    bool isNullable = false;
                    Type pt = property.PropertyType;
                    if (pt.IsGenericType && pt.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        pt = pt.GetGenericArguments()[0];
                        isNullable = true;
                    }
                    switch (pt.Name)
                    {
                        case "String":
                            property.SetValue(t, GetString(property.Name), null);
                            break;
                        case "Byte":
                            property.SetValue(t, isNullable ? GetNullableByte(property.Name) : GetByte(property.Name), null);
                            break;
                        case "Boolean":
                            property.SetValue(t, isNullable ? GetNullableBoolean(property.Name) : GetBoolean(property.Name), null);
                            break;
                        case "Int16":
                            property.SetValue(t, isNullable ? GetNullableInt16(property.Name) : GetInt16(property.Name), null);
                            break;
                        case "Int32":
                            property.SetValue(t, isNullable ? GetNullableInt32(property.Name) : GetInt32(property.Name), null);
                            break;
                        case "Int64":
                            property.SetValue(t, isNullable ? GetNullableInt64(property.Name) : GetInt64(property.Name), null);
                            break;
                        case "Double":
                            property.SetValue(t, isNullable ? GetNullableDouble(property.Name) : GetDouble(property.Name), null);
                            break;
                        case "DateTime":
                            property.SetValue(t, isNullable ? GetNullableDateTime(property.Name) : GetDateTime(property.Name), null);
                            break;
                        case "Decimal":
                            property.SetValue(t, isNullable ? GetNullableDecimal(property.Name) : GetDecimal(property.Name), null);
                            break;
                        case "Guid":
                            property.SetValue(t, isNullable ? GetNullableGuid(property.Name) :GetGuid(property.Name), null);
                            break;
                    }

                }
            }
            return t;
        }

        private void setColName(ref string colName)
        {
            if (predix != null)
            {
                colName = predix + colName;
            }
        }
        #region Double
        /// <summary>
        /// 获取Double型数据(null:0)
        /// </summary>
        /// <param name="colName">列名</param>
        public double GetDouble(string colName)
        {
            setColName(ref colName);
            if (this._dr.IsNull(colName))
            {
                return 0;
            }
            return Convert.ToDouble(this._dr[colName].ToString());
        }

        /// <summary>
        /// 获取Double型数据(null:0)
        /// </summary>
        /// <param name="colIndex">列索引</param>
        public double GetDouble(int colIndex)
        {
            if (this._dr.IsNull(colIndex))
            {
                return 0;
            }
            return Convert.ToDouble(this._dr[colIndex]);
        }

        /// <summary>
        /// 获取可空Double型数据
        /// </summary>
        /// <param name="colName">列名</param>
        public Nullable<double> GetNullableDouble(string colName)
        {
            setColName(ref colName);
            if (this._dr.IsNull(colName))
            {
                return null;
            }
            return Convert.ToDouble(this._dr[colName].ToString());
        }

        /// <summary>
        /// 获取可空Double型数据
        /// </summary>
        /// <param name="colIndex">列索引</param>
        public Nullable<double> GetNullableDouble(int colIndex)
        {
            if (this._dr.IsNull(colIndex))
            {
                return null;
            }
            return Convert.ToDouble(this._dr[colIndex]);
        }

        #endregion

        #region Int32
        /// <summary>
        /// 获取Int型数据
        /// </summary>
        /// <param name="colName">列名</param>
        public int GetInt32(string colName)
        {
            setColName(ref colName);
            if (this._dr.IsNull(colName))
            {
                return 0;
            }
            return Convert.ToInt32(this._dr[colName]);
        }

        /// <summary>
        /// 获取Int型数据
        /// </summary>
        /// <param name="colIndex">列索引</param>
        public int GetInt32(int colIndex)
        {
            if (this._dr.IsNull(colIndex))
            {
                return 0;
            }
            return Convert.ToInt32(this._dr[colIndex]);
        }

        /// <summary>
        /// 获取可空Int型数据
        /// </summary>
        /// <param name="colName">列名</param>
        public Nullable<int> GetNullableInt32(string colName)
        {
            setColName(ref colName);
            if (this._dr.IsNull(colName))
            {
                return null;
            }
            return Convert.ToInt32(this._dr[colName]);
        }


        /// <summary>
        /// 获取可空Int型数据
        /// </summary>
        /// <param name="colIndex">列索引</param>
        public Nullable<int> GetNullableInt32(int colIndex)
        {
            if (this._dr.IsNull(colIndex))
            {
                return null;
            }
            return Convert.ToInt32(this._dr[colIndex]);
        }
        #endregion

        public bool Contains(string colName)
        {
            setColName(ref colName);
            return this._dr.Table.Columns.Contains(colName);
            
        }

        #region String
        /// <summary>
        /// 获取String型数据
        /// </summary>
        /// <param name="colName">列名</param>
        public string GetString(string colName)
        {
            setColName(ref colName);
            if (this._dr.IsNull(colName))
            {
                return null;
            }
            return Convert.ToString(this._dr[colName]);
        }

        /// <summary>
        /// 获取String型数据
        /// </summary>
        /// <param name="colIndex">列索引</param>
        public string GetString(int colIndex)
        {
            if (this._dr.IsNull(colIndex))
            {
                return null;
            }
            return Convert.ToString(this._dr[colIndex]);
        }

        #endregion

        #region DateTime

        /// <summary>
        /// 获取DateTime型数据
        /// </summary>
        /// <param name="colName">列名</param>
        public DateTime GetDateTime(string colName)
        {
            setColName(ref colName);
            if (this._dr.IsNull(colName))
            {
                return DateTime.MinValue;
            }
            return Convert.ToDateTime(this._dr[colName]);
        }


        /// <summary>
        /// 获取DateTime型数据
        /// </summary>
        /// <param name="colIndex">列索引</param>
        public DateTime GetDateTime(int colIndex)
        {
            if (this._dr.IsNull(colIndex))
            {
                return DateTime.MinValue;
            }
            return Convert.ToDateTime(this._dr[colIndex]);
        }

        /// <summary>
        /// 获取可空DateTime型数据
        /// </summary>
        /// <param name="colName">列名</param>
        public Nullable<DateTime> GetNullableDateTime(string colName)
        {
            setColName(ref colName);
            if (this._dr.IsNull(colName))
            {
                return null;
            }
            return Convert.ToDateTime(this._dr[colName]);
        }

        /// <summary>
        /// 获取可空DateTime型数据
        /// </summary>
        /// <param name="colIndex">列索引</param>
        public Nullable<DateTime> GetNullableDateTime(int colIndex)
        {
            if (this._dr.IsNull(colIndex))
            {
                return null;
            }
            return Convert.ToDateTime(this._dr[colIndex]);
        }

        #endregion 

        #region Boolean
        /// <summary>
        /// 获取Boolean型数据
        /// </summary>
        /// <param name="colName">列名</param>
        public bool GetBoolean(string colName)
        {
            setColName(ref colName);
            if (this._dr.IsNull(colName))
            {
                return false;
            }
            return Convert.ToBoolean(this._dr[colName]);
        }

        /// <summary>
        /// 获取Boolean型数据
        /// </summary>
        /// <param name="colIndex">列索引</param>
        public bool GetBoolean(int colIndex)
        {
            if (this._dr.IsNull(colIndex))
            {
                return false;
            }
            return Convert.ToBoolean(this._dr[colIndex]);
        }

        /// <summary>
        /// 获取可空Boolean型数据
        /// </summary>
        /// <param name="colName">列名</param>
        public Nullable<bool> GetNullableBoolean(string colName)
        {
            setColName(ref colName);
            if (this._dr.IsNull(colName))
            {
                return null;
            }
            return Convert.ToBoolean(this._dr[colName]);
        }


        /// <summary>
        /// 获取可空Boolean型数据
        /// </summary>
        /// <param name="colIndex">列索引</param>
        public Nullable<bool> GetNullableBoolean(int colIndex)
        {
            if (this._dr.IsNull(colIndex))
            {
                return null;
            }
            return Convert.ToBoolean(this._dr[colIndex]);
        }
        #endregion 
    
        #region Int64

        public long GetInt64(string colName)
        {
            setColName(ref colName);
            if (this._dr.IsNull(colName))
            {
                return 0;
            }
            return Convert.ToInt64(this._dr[colName]);
        }
        public long? GetNullableInt64(string colName)
        {
            setColName(ref colName);
            if (this._dr.IsNull(colName))
            {
                return null;
            }
            return Convert.ToInt64(this._dr[colName]);
        } 
        #endregion

        #region Int16 
        public Int16 GetInt16(string colName)
        {
            setColName(ref colName);
            if (this._dr.IsNull(colName))
            {
                return 0;
            }
            return Convert.ToInt16(this._dr[colName]);
        }
        public Int16? GetNullableInt16(string colName)
        {
            setColName(ref colName);
            if (this._dr.IsNull(colName))
            {
                return null;
            }
            return Convert.ToInt16(this._dr[colName]);
        }

        #endregion

        #region Byte
        public byte GetByte(string colName)
        {
            setColName(ref colName);
            if (this._dr.IsNull(colName))
            {
                return 0;
            }
            return Convert.ToByte(this._dr[colName]);
        }
        public byte[] GetBytes(string colName)
        {
            setColName(ref colName);
            if (this._dr.IsNull(colName))
            {
                return null;
            }
            return (byte[])this._dr[colName];
        }
        public byte? GetNullableByte(string colName)
        {
            setColName(ref colName);
            if (this._dr.IsNull(colName))
            {
                return null;
            }
            return Convert.ToByte(this._dr[colName]);
        }
        #endregion

        internal uint? GetNullableUInt32(string colName)
        {
            setColName(ref colName);
            if (this._dr.IsNull(colName))
            {
                return null;
            }
            return Convert.ToUInt32(this._dr[colName]);

        }

        internal decimal? GetNullableDecimal(string colName)
        {
            setColName(ref colName);

            if (this._dr.IsNull(colName))
            {
                return null;
            }
            return Convert.ToDecimal(this._dr[colName]);
        }
        public decimal GetDecimal(string colName)
        {
            setColName(ref colName);
            if (this._dr.IsNull(colName))
            {
                return 0;
            }
            return Convert.ToDecimal(this._dr[colName]);
        }
        //internal T GetValue<T>(string colName) where T : struct
        //{
        //    Type tp = typeof(T);
        //    switch (tp.Name)
        //    {
        //    }
        //    return new T();
        //    //Convert.
        //}

        internal Guid GetGuid(string p)
        {
            //return GetString(p);
            setColName(ref p);
            return Guid.Parse(_dr[p].ToString());
        }

        internal Guid? GetNullableGuid(string p)
        {
            //return GetString(p);
            try
            {
                setColName(ref p);
                return Guid.Parse(_dr[p].ToString());
            }
            catch
            {
                return null;
            }


        }

        internal DateTime? GetStringDate(string p)
        {
            setColName(ref p);
            DateTime dt;
            if (DateTime.TryParse(_dr[p].ToString(), out dt)) return dt;
            return null;
        }
    }
}
