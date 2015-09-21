namespace AMS.Utilities
{

    public class JsonMessage
    {
        public override string ToString()
        {
            return this.ToJson();
        }

        public string Code { get; set; }

        public string Message { get; set; }

        public bool Success { get; set; }
    }
}

