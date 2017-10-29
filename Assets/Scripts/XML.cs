using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

[Serializable]
public class XML
{
    [NonSerialized]
    private const string template =
@"<?xml version='1.0'?>
<root>
    <settings>
    </settings>
    <stages>
    </stages>
</root>
";
    public string filePath { get; private set; }
    [NonSerialized]
    public XElement doc;
    public bool readability;
    [NonSerialized]
    private string content;

    private XML(string fp)
    {
        readability = File.Exists(fp);
        if (readability)
        {
            using (
                StreamReader strd =
                new StreamReader(new FileStream(fp, FileMode.Open, FileAccess.Read)))
            {
                content = strd.ReadToEnd();
            }
        }
        else
        {
            content = template;
        }
        doc = XElement.Parse(content);
    }

    public static XML GetInstance(string path)
    {
        XML xml = new XML(path);
        return xml;
    }

    public IEnumerable<XElement> ToCollection(string node)
    {
        return doc.Descendants("root").Descendants(node);
    }
}
