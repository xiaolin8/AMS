using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace AMS.Utilities
{


    public static class JsonHelper2
    {
        public static List<T> JonsToList<T>(this string Json)
        {
            return JsonConvert.DeserializeObject<List<T>>(Json);
        }

        public static DataTable JsonToDataTable(this string strJson)
        {
            DataTable table = null;
            MatchCollection matchs = new Regex("(?<={)[^}]+(?=})").Matches(strJson);
            for (int i = 0; i < matchs.Count; i++)
            {
                string[] strArray = matchs[i].Value.Split(new char[] { ',' });
                if (table == null)
                {
                    table = new DataTable {
                        TableName = "Table"
                    };
                    foreach (string str2 in strArray)
                    {
                        DataColumn column = new DataColumn();
                        string[] strArray2 = str2.Split(new char[] { ':' });
                        column.DataType = typeof(string);
                        column.ColumnName = strArray2[0].ToString().Replace("\"", "").Trim();
                        table.Columns.Add(column);
                    }
                    table.AcceptChanges();
                }
                DataRow row = table.NewRow();
                for (int j = 0; j < strArray.Length; j++)
                {
                    object obj2 = strArray[j].Split(new char[] { ':' })[1].Trim().Replace("，", ",").Replace("：", ":").Replace("/", "").Replace("\"", "").Trim();
                    if ((obj2.ToString().Length >= 5) && (obj2.ToString().Substring(0, 5) == "Date("))
                    {
                        obj2 = CommonHelper.JsonToDateTime(obj2.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    row[j] = obj2;
                }
                table.Rows.Add(row);
                table.AcceptChanges();
            }
            return table;
        }

        public static T JsonToEntity<T>(this string Json)
        {
            return JsonConvert.DeserializeObject<T>(Json);
        }

        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static object ToJson(this string Json)
        {
            return JsonConvert.DeserializeObject(Json);
        }
    }
}

