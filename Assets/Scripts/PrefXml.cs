using System;
using System.Linq;
using System.Collections.Generic;

[Serializable]
public class PrefXml
{
    public string filePath { private set; get; }
    public XML doc;

    public PrefXml(string path)
    {
        doc = XML.GetInstance(path);
    }

    public List<System.Xml.Linq.XElement> GetStages()
    {
        var stages = from s in doc.ToCollection("stages")
                     select s;
        return stages.ToList();
    }
}
