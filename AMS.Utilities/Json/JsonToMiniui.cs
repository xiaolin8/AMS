namespace DotNet.Utilities
{
    public partial class JsonHelper2
    {
        /// <summary>
        /// 将objcet转换为Grid使用的Json格式
        /// </summary>
        /// <param name="as_jsonstring"></param>
        /// <returns></returns>
        public string GetJsonStringByObject2grid(object obj, int iCnt,bool isBox=false)
        {
            /*
            {total:"2",
            data:[
               {A:"A1",B:"B1"},
               {A:"A2",B:"B2"}
            ]}
            */
            string _Json = string.Empty;
            if (iCnt == 0)
            {
                _Json = "{}";
                return _Json;
            }
            string _strValue = this.GetJsonStringByObject(obj);
            _Json += "{\"total\":\"" + iCnt + "\",\"data\":";
            _Json += _strValue;
            _Json += "}";
            if (isBox)
            {
                _Json = "[" + _Json + "]";
            }
            return _Json;

        }
    }
}