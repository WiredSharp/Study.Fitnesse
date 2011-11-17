using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Study.Fit.MyProject
{
    [Serializable]
    public class ConflictingProgramException : Exception
    {
        private static readonly long _serialVersionUID = 1L;
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public ConflictingProgramException()
        {}

        public ConflictingProgramException(string message) : base(message)
        {}

        public ConflictingProgramException(string message, Exception inner) : base(message, inner)
        {}

        protected ConflictingProgramException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {}
    }
}
