using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Xml.Schema;
using System.Xml;
using System.Globalization;
using System;
using System.Linq;

namespace SH.Dialogs
{
    [Serializable]
    public class Dialog : IXmlSerializable
    {
        public List<IDialogElement> Content { get; private set; } = new List<IDialogElement>();
        public bool? LineBreak { get; private set; } = false;
        public double? CharDelay { get; private set; } = 0.5;
        public double? SentenceDelay { get; private set; } = 1.0;
        public double? PauseDelay { get; private set; } = 1.0;
        public string ID { get; private set; }

        public string Serialize()
        {
            using (var str = new StringWriter())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Dialog));
                serializer.Serialize(str, this);
                return str.ToString();
            }
        }

        public static Dialog Deserialize(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Dialog));
            return serializer.Deserialize(new StringReader(xml)) as Dialog;
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            string tmp = null;
            tmp = reader.GetAttribute("id");
            if (tmp != null)
                ID = tmp;
            tmp = reader.GetAttribute("lineBreak");
            if (tmp != null)
                LineBreak = Convert.ToBoolean(tmp, CultureInfo.InvariantCulture);
            tmp = reader.GetAttribute("charDelay");
            if (tmp != null)
                CharDelay = Convert.ToDouble(tmp, CultureInfo.InvariantCulture);
            tmp = reader.GetAttribute("sentenceDelay");
            if (tmp != null)
                SentenceDelay = Convert.ToDouble(tmp, CultureInfo.InvariantCulture);
            tmp = reader.GetAttribute("pauseDelay");
            if (tmp != null)
                PauseDelay = Convert.ToDouble(tmp, CultureInfo.InvariantCulture);
            var inner = reader.ReadSubtree();
            inner.MoveToContent();
            inner.Read();
            do
            {
                if (inner.NodeType == XmlNodeType.Element)
                {
                    var type = typeof(Sentence).Assembly.FindTypeByFullName("SH.Dialogs." + inner.Name);
                    IDialogElement elem = (new XmlSerializer(type)).Deserialize(inner.ReadSubtree()) as IDialogElement;
                    Content.Add(elem);
                    inner.Skip();
                }
            } while (!(inner.NodeType == XmlNodeType.EndElement && inner.Name == "Dialog"));
            inner.Close();
        }

        public void WriteXml(XmlWriter writer)
        {
            if (LineBreak.HasValue)
                writer.WriteAttributeString("lineBreak", LineBreak.Value.ToString(CultureInfo.InvariantCulture));
            if (CharDelay.HasValue)
                writer.WriteAttributeString("charDelay", CharDelay.Value.ToString(CultureInfo.InvariantCulture));
            if (SentenceDelay.HasValue)
                writer.WriteAttributeString("sentenceDelay", SentenceDelay.Value.ToString(CultureInfo.InvariantCulture));
            if (PauseDelay.HasValue)
                writer.WriteAttributeString("pauseDelay", PauseDelay.Value.ToString(CultureInfo.InvariantCulture));
            foreach (IDialogElement element in Content)
            {
                var xml = new XmlSerializer(element.GetType());
                xml.Serialize(writer, element, new XmlSerializerNamespaces());
            }
            //XmlSerializer serializer = new XmlSerializer(Content.GetType());
            //serializer.Serialize(writer, Content);
        }
    }
}
