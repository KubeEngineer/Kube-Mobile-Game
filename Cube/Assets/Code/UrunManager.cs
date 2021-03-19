
using UnityEngine;
using UnityEngine.UI;

public class UrunManager : MonoBehaviour
{
    [SerializeField]
    private int _fiyat;
    [SerializeField]
    private string _MaterialIsim;
    private Text[] text;
    // Start is called before the first frame update
    void Start()
    {
        _MaterialIsim = GetComponentInChildren<MeshRenderer>().material.name;
        text = GetComponentsInChildren<Text>();
        text[1].text = _fiyat.ToString() + " AE";
       

        if (PlayerPrefs.GetInt(gameObject.name) == 1)
        {
            Debug.Log("alındı  ->" + gameObject.name);
            GetComponentInChildren<Button>().interactable = false;
            text[0].text = "PURCHASED";
        }

       
    }
    private void Update()
    {
        if(PlayerPrefs.GetInt(gameObject.name) == 1)
        {
            GetComponentInChildren<Button>().interactable = false;
            text[0].text = "PURCHASED";
        }
    }

    public int getFiyat()
    {
        return _fiyat;
    }
    public string getMaterialIsım()
    {
        return _MaterialIsim;
    }
}
