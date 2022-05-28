using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;
using System.Linq;
using System.Globalization;
using System;

namespace SH.Dialogs
{
    [System.Serializable]
    public class Sentence : IDialogElement
    {
        public string ID { get; private set; }
        public string Speaker { get; private set; }
        public string Goto { get; private set; }
        public bool? LineBreak { get; private set; }
        public bool? Clear { get; private set; }
        public double? CharDelay { get; private set; }
        public double? SentenceDelay { get; private set; }
        public List<ISentenceElement> Content { get; private set; } = new List<ISentenceElement>();

        public Sentence() { }
        public Sentence(string text)
        {
            Content.Add(new Text(text));
        }
        public Sentence(params ISentenceElement[] Content)
        {
            this.Content = Content.ToList();
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
            tmp = reader.GetAttribute("speaker");
            if (tmp != null)
                Speaker = tmp;
            tmp = reader.GetAttribute("goto");
            if (tmp != null)
                Goto = tmp;
            tmp = reader.GetAttribute("lineBreak");
            if (tmp != null)
                LineBreak = Convert.ToBoolean(tmp, CultureInfo.InvariantCulture);
            tmp = reader.GetAttribute("clear");
            if (tmp != null)
                Clear = Convert.ToBoolean(tmp, CultureInfo.InvariantCulture);
            tmp = reader.GetAttribute("charDelay");
            if (tmp != null)
                CharDelay = Convert.ToDouble(tmp, CultureInfo.InvariantCulture);
            tmp = reader.GetAttribute("sentenceDelay");
            if (tmp != null)
                SentenceDelay = Convert.ToDouble(tmp, CultureInfo.InvariantCulture);
            bool moved = false;
            var inner = reader.ReadSubtree();
            inner.MoveToContent();
            inner.Read();
            do
            {
                moved = false;
                if (inner.NodeType == XmlNodeType.Element)
                {
                    var type = typeof(Sentence).Assembly.FindTypeByFullName("SH.Dialogs." + inner.Name);
                    ISentenceElement elem = (new XmlSerializer(type)).Deserialize(inner.ReadSubtree()) as ISentenceElement;
                    Content.Add(elem);
                    inner.Skip();
                    moved = true;
                }
                else if (inner.NodeType == XmlNodeType.Text)
                {
                    Content.Add(new Text(inner.ReadContentAsString()));
                    moved = true;
                }
            } while (moved || inner.Read());
        }

        public void WriteXml(XmlWriter writer)
        {
            if (!ID.IsNullOrEmpty())
                writer.WriteAttributeString("id", ID);
            if (!Goto.IsNullOrEmpty())
                writer.WriteAttributeString("goto", Goto);
            if (LineBreak.HasValue)
                writer.WriteAttributeString("lineBreak", LineBreak.Value.ToString(CultureInfo.InvariantCulture));
            if (Clear.HasValue)
                writer.WriteAttributeString("clear", Clear.Value.ToString(CultureInfo.InvariantCulture));
            if (CharDelay.HasValue)
                writer.WriteAttributeString("charDelay", CharDelay.Value.ToString(CultureInfo.InvariantCulture));
            if (SentenceDelay.HasValue)
                writer.WriteAttributeString("sentenceDelay", SentenceDelay.Value.ToString(CultureInfo.InvariantCulture));
            foreach (ISentenceElement element in Content)
            {
                if (element is Text)
                    writer.WriteString(element.ToString());
                else
                {
                    var xml = new XmlSerializer(element.GetType());
                    xml.Serialize(writer, element, new XmlSerializerNamespaces());
                }
            }
        }
    }
}