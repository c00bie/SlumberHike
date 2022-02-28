using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SH.Dialogs
{
    public class Pause : ISentenceElement, IDialogElement
    {
        public double? Delay { get; set; }

        public string ID { get; set; }

        public Pause() { }
        public Pause(double delay)
        {
            Delay = delay;
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            var tmp = reader.GetAttribute("delay");
            if (tmp != null)
                Delay = Convert.ToDouble(tmp, CultureInfo.InvariantCulture);
            tmp = reader.GetAttribute("id");
            if (tmp != null && !tmp.IsNullOrEmpty())
                ID = tmp;
        }

        public void WriteXml(XmlWriter writer)
        {
            if (Delay.HasValue)
                writer.WriteAttributeString("delay", Delay.Value.ToString(CultureInfo.InvariantCulture));
            if (!ID.IsNullOrEmpty())
                writer.WriteAttributeString("id", ID);
        }
    }
}
