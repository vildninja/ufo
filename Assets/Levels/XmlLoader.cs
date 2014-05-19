using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.IO;
using System.Xml;

[ExecuteInEditMode]
public class XmlLoader : MonoBehaviour {

    public bool save;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (save)
        {
            save = false;
            Export(name);
        }
	}

    public void Export(string levelName)
    {
        XmlDocument doc = new XmlDocument();
        var root = doc.AppendChild(doc.CreateElement("body"));

        var environment = GameObject.Find("Static").transform;
        var e = doc.CreateElement("static");
        RecursiveAddStatic(environment, e);

        doc.Save(Application.dataPath + "/Levels/" + levelName + ".xml");
    }

    private void RecursiveAddStatic(Transform s, XmlElement element)
    {
        print(s);

        foreach (var c in s.gameObject.GetComponents<Component>())
        {
            print(c + " " + c.GetType());
            //element.SetAttribute
        }

        foreach (Transform t in s)
        {
            var e = element.OwnerDocument.CreateElement("transform");
            e.SetAttribute("name", t.name);
            RecursiveAddStatic(t, e);
        }
    }
}
