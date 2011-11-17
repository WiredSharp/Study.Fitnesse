using System;
using System.Text;

namespace Study.Fit.PageGenerator.Test
{
    public class AnotherTestFixture
    {
        public AnotherTestFixture()
        {}

        public AnotherTestFixture(string aStringParameter)
        { }

        public AnotherTestFixture(string aStringParameter, int andAnIntParameter)
        { }

        public string AnotherStringProperty { get; set; }

        public int? AnotherIntProperty { get; set; }

        public DateTime? AnotherDatetimeProperty { get; set; }

        private bool? AnotherPrivateProperty { get; set; }

        public bool? AnotherPrivateGetterProperty { private get; set; }

        public bool? AnotherPrivateSetterProperty { get; private set; }

        public string AnotherLog { get; private set; }

        public void Execute()
        {
            var log = new StringBuilder();
            if (AnotherStringProperty != null)
                log.Append("AnotherStringProperty :" + AnotherStringProperty + Environment.NewLine);
            if (AnotherIntProperty.HasValue)
                log.Append("AnotherIntProperty :" + AnotherIntProperty.Value + Environment.NewLine);
            if (AnotherDatetimeProperty.HasValue)
                log.Append("AnotherDatetimeProperty :" + AnotherDatetimeProperty.Value + Environment.NewLine);
            if (AnotherPrivateProperty.HasValue)
                log.Append("AnotherPrivateProperty :" + AnotherPrivateProperty.Value + Environment.NewLine);
            if (AnotherPrivateGetterProperty.HasValue)
                log.Append("AnotherPrivateGetterProperty :" + AnotherPrivateGetterProperty.Value + Environment.NewLine);
            if (AnotherPrivateSetterProperty.HasValue)
                log.Append("AnotherPrivateSetterProperty :" + AnotherPrivateSetterProperty.Value + Environment.NewLine);
            AnotherLog = log.ToString();
        }         
    }
}