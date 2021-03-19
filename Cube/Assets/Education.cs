using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Education : MonoBehaviour
{
    public GameObject touchUp, touchDown, arrowDown,arrowUp, touchRight, touchLeft, arrowRight, arrowLeft, karakter, tamamlandi;
    public SahnelereYollama sahne;
    public bool downObjective = false, upObjective = false, rightObjective = false, leftObjective = false, tamamlandiBasildi = true;
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("EducationFirst") == 1)
        {
            enabled = false;
        }else
        {
            karakter.GetComponent<Hareket>().enabled = false;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Hareket>().enabled = false;
            tamamlandiBasildi = true;
            touchDown.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (tamamlandiBasildi)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // right ve left için ekleme
                if(!downObjective)
                {
                    pos = Input.mousePosition;
                    pos.Set(pos.x, pos.y -170F, pos.z);
                }
                else if(!upObjective)
                {
                    pos = Input.mousePosition;
                    pos.Set(pos.x, pos.y + 170, pos.z);
                }
                else if(!rightObjective)
                {
                    pos = Input.mousePosition;
                    pos.Set(pos.x + 170f, pos.y, pos.z);
                }
                else if(!leftObjective)
                {
                    pos = Input.mousePosition;
                    pos.Set(pos.x-170f, pos.y, pos.z);
                }
                

            }
            if (Input.GetMouseButton(0))
            {
                if (!downObjective)
                {
                    touchDown.SetActive(false);
                    arrowDown.transform.position = pos;
                    arrowDown.SetActive(true);
                }
                else if (!upObjective)
                {
                    touchUp.SetActive(false);
                    arrowUp.transform.position = pos;
                    arrowUp.SetActive(true);
                }
                else if (!rightObjective)
                {
                    touchRight.SetActive(false);
                    arrowRight.transform.position = pos;
                    arrowRight.SetActive(true);
                }
                else if (!leftObjective)
                {
                    touchLeft.SetActive(false);
                    arrowLeft.transform.position = pos;
                    arrowLeft.SetActive(true);
                }

            }
            else
            {
                if (!downObjective)
                {
                    touchDown.SetActive(true);
                    arrowDown.SetActive(false);
                }
                else if (!upObjective)
                {
                    touchUp.SetActive(true);
                    arrowUp.SetActive(false);
                }
                else if (!rightObjective)
                {
                    touchRight.SetActive(true);
                    arrowRight.SetActive(false);
                }
                else if (!leftObjective)
                {
                    touchLeft.SetActive(true);
                    arrowLeft.SetActive(false);
                }
            }
            // her gorev tamamlandığında karakter konumunu sıfırla ve boyutlandırının aktifliğini kapat ve buton basılınca aç
            if (!downObjective && karakter.transform.localScale.x.Equals(2f) && karakter.transform.localScale.y.Equals(0.3f) &&
                    karakter.transform.localScale.z.Equals(1f))
            {
                downObjective = true;
                tamamlandiBasildi = false;
                tamamlandi.GetComponentInChildren<Text>().text = "(1 / 4)";
                tamamlandi.SetActive(true);
                tamamlandi.GetComponent<Animator>().Play("tickAnimation");
                KonumSifirla();
                arrowDown.SetActive(false);
                touchDown.SetActive(false);
            }
            else if (downObjective && !upObjective && karakter.transform.localScale.x.Equals(0.3f) &&
                    karakter.transform.localScale.y.Equals(2f) &&
                    karakter.transform.localScale.z.Equals(1f))
            {
                tamamlandiBasildi = false;
                tamamlandi.GetComponentInChildren<Text>().text = "(2 / 4)";
                tamamlandi.SetActive(true);
                tamamlandi.GetComponent<Animator>().Play("tickAnimation");
                KonumSifirla();
                touchUp.SetActive(false);
                arrowUp.SetActive(false);
                upObjective = true;
            }
            else if (upObjective && !rightObjective && karakter.transform.position.x > 1.7f)
            {
                tamamlandiBasildi = false;
                tamamlandi.GetComponentInChildren<Text>().text = "(3 / 4)";
                tamamlandi.SetActive(true);
                tamamlandi.GetComponent<Animator>().Play("tickAnimation");
                KonumSifirla();
                touchRight.SetActive(false);
                arrowRight.SetActive(false);
                rightObjective = true;
            }

            else if (rightObjective && !leftObjective && karakter.transform.position.x < -1.7f )
            {
                tamamlandiBasildi = false;
                tamamlandi.GetComponentInChildren<Text>().text = "(4 / 4)";
                tamamlandi.SetActive(true);
                tamamlandi.GetComponent<Animator>().Play("tickAnimation");
                KonumSifirla();
                touchLeft.SetActive(false);
                arrowLeft.SetActive(false);
                leftObjective = true;
            }
            
        }
    }
    public void bas()
    {
        if(upObjective && rightObjective && downObjective && leftObjective)
        {
            PlayerPrefs.SetInt("EducationFirst",1);
            sahne.loadLevel(1);
        }else
        {
            tamamlandiBasildi = true;
            baslat();
            tamamlandi.SetActive(false);
        }
    }
    void baslat()
    {
        karakter.GetComponent<Boyuntlandir>().setX(karakter.transform.localScale.x);
        karakter.GetComponent<Boyuntlandir>().setY(karakter.transform.localScale.y);
        karakter.GetComponent<Boyuntlandir>().enabled = true;
    }

    void KonumSifirla()
    {
        karakter.GetComponent<Boyuntlandir>().enabled = false;
        karakter.transform.Translate(new Vector3(0-karakter.transform.position.x, 0.87f- karakter.transform.position.y, -45.42f- karakter.transform.position.z));
        karakter.transform.localScale = new Vector3(1, 1, 1);
    }
}
