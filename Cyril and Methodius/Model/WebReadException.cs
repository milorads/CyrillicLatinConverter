using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyril_and_Methodius.Model
{
    public class WebReadException : System.Exception
    {
        public WebReadException() : base() { }
        public WebReadException(string message) : base(message) { }
        public WebReadException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client.
        protected WebReadException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
        { }
    }
}
