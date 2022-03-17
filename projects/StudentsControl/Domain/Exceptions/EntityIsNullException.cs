namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    [Serializable]
    public class EntityIsNullException : Exception
    {
        public EntityIsNullException() { }
        public EntityIsNullException(string message) : base(message) { }
        public EntityIsNullException(string message, Exception inner) : base(message, inner) { }
        protected EntityIsNullException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
