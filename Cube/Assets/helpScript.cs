using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helpScript : MonoBehaviour
{
    public GameObject help1, help2;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Education") == 0 && PlayerPrefs.GetInt("EducationFirst") == 1)
        {
            Invoke("showHelp2", 15f);
            Invoke("invisibleHelp2", 20f);
            if (PlayerPrefs.GetInt("tekrar")%2 == 1)
            {
                Invoke("showHelp", 5f);
                Invoke("invisibleHelp", 10f);
            }
        }else
        {
            
            Debug.Log(PlayerPrefs.GetInt("Education"));
            enabled = false;
        }
    }
    void showHelp2()
    {
        Debug.Log("sdasda");
        help2.SetActive(true);
        help2.GetComponent<Animator>().Play("help2");
    }
    void invisibleHelp2()
    {
        help2.SetActive(false);
    }
    void showHelp()
    {
        Debug.Log("sdasda");
        help1.SetActive(true);
        Debug.Log(help1.active);
        help1.GetComponent<Animator>().Play("help1");
    }
    void invisibleHelp()
    {
        help1.SetActive(false);
    }
}
