using System.Collections.Generic;

namespace DotNet.Utilities
{
    public class SqlTextAndParameter
    {
        public string SqlString { get; set; }
        public string TableName { get; set; }
        public List<Parameter> Parameters { get; set; }
    }
}