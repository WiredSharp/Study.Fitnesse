using System;
using System.Text;

namespace Study.Fit.PageGenerator.Test
{
    public class ThisIsATestFixture
    {
        public string AStringProperty { get; set; }

        public int? AnIntProperty { get; set; }

        public DateTime? ADateTimeProperty { get; set; }

        private bool? APrivateProperty { get; set; }

        public bool? APrivateGetterProperty { private get; set; }

        public bool? APrivateSetterProperty { get; private set; }

        public string Log { get; private set; }

        public void Execute()
        {
            var log = new StringBuilder();
            if (AStringProperty != null)
                log.Append("AStringProperty :" + AStringProperty + Environment.NewLine);
            if (AnIntProperty.HasValue)
                log.Append("AnIntProperty :" + AnIntProperty.Value + Environment.NewLine);
            if (ADateTimeProperty.HasValue)
                log.Append("AnDateTimeProperty :" + ADateTimeProperty.Value + Environment.NewLine);
            if (APrivateProperty.HasValue)
                log.Append("AnPrivateProperty :" + APrivateProperty.Value + Environment.NewLine);
            if (APrivateGetterProperty.HasValue)
                log.Append("APrivateGetterProperty :" + APrivateGetterProperty.Value + Environment.NewLine);
            if (APrivateSetterProperty.HasValue)
                log.Append("APrivateSetterProperty :" + APrivateSetterProperty.Value + Environment.NewLine);
            Log = log.ToString();
        }
    }
}