
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnvanterManger : MonoBehaviour
{
    [SerializeField]
    private GameObject _ReferansObje;
    [SerializeField]
    private Transform _ParentTransform;
    List<GameObject> Objeler;

    // Start is called before the first frame update
    void Start()
    {
        Objeler = new List<GameObject>();
        Objeler.Add(_ReferansObje);
        //Envarterdeki materiallar getirildi 
        string[] Envanter = PlayerPrefs.GetString("Envanter").Split(' ');
        for(int i = 1; i < Envanter.Length; i++)
        {
            //getirilen materialler kullanilarak yeni urunler listeye eklendi ve ekranda
            //gösterildi.
            GameObject obj = Instantiate(_ReferansObje,_ParentTransform);
            Material mat = Resources.Load("Material/" + Envanter[i], typeof(Material)) as Material;
            obj.GetComponentInChildren<MeshRenderer>().material = mat;
            
            Objeler.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var obje in Objeler)
        {
            string[] Kontrol = obje.GetComponentInChildren<MeshRenderer>().material.name.Split(' ');
            if (Kontrol[0].Equals(PlayerPrefs.GetString("GecerliKarakter")))
            {
                obje.GetComponentInChildren<Text>().text = "Used";
                obje.GetComponentInChildren<Text>().fontSize = 24;
                obje.GetComponentInChildren<Button>().interactable = false;
            }else
            {
                obje.GetComponentInChildren<Text>().text = "Use";
                obje.GetComponentInChildren<Text>().fontSize = 39;
                obje.GetComponentInChildren<Button>().interactable = true;
            }
        }
        
    }

    public void kullan(MeshRenderer renderer)
    {
        string[] gecerliKarakter = renderer.material.name.Split(' ');
        PlayerPrefs.SetString("GecerliKarakter", gecerliKarakter[0]);
    }

    
}
