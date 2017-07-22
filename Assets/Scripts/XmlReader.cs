using System.Xml;
using UnityEngine;

public class XmlReader
{
    public static Dialog GetDialog(string id)
    {
        TextAsset file = Resources.Load("Dialogs") as TextAsset;
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(file.text);
        
        try
        {
            XmlNodeList nodes = doc.GetElementsByTagName("dialog");
            XmlNodeList curNode = GetNode(id, nodes).ChildNodes;
            Dialog dialog = new Dialog();

            for (int i = 0; i < curNode.Count; i++)
            {
                XmlNode frase = curNode[i];
                dialog.AddFrase(new Frase(frase.Attributes["person"].InnerText, frase.InnerText));
            }
            return dialog;
        }
        catch
        {
            throw new System.Exception("There is an error in Dialogs while getting " + id);
        }
    }

    public static Dialog GetWave(string id)
    {
        TextAsset file = (TextAsset)Resources.Load("Waves");
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(file.text);

        try
        {
            XmlNodeList nodes = doc.GetElementsByTagName("wave");
            XmlNodeList curNode = GetNode(id, nodes).ChildNodes;
            
            return null;
        }
        catch
        {
            return null;
        }
    }

    private static XmlNode GetNode(string id, XmlNodeList nodes)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].Attributes["id"].InnerText == id)
            {
                return nodes[i];
            }
        }
        return null;
    }
}
