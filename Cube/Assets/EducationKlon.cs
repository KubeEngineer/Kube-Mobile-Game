
using UnityEngine;

public class EducationKlon : MonoBehaviour
{
    private GameObject[] tokenler, engeller;
    private GameObject karakter;
    float A_onceki_z;
    float onceki_z;
    int _maxEngel = 8;
    public int _engelSayisi;
    float _x_sag_max = 2f;
    float _x_sol_max = -2f;
    int olusan_altin_sayisi = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Education") == 0 && PlayerPrefs.GetInt("EducationFirst") == 1)
        {
            _engelSayisi = 0;
            nesneleriGetir();
            karakter = GameObject.FindGameObjectWithTag("Player");
            onceki_z = karakter.transform.position.z;
            for (int i = 0; i < _maxEngel; i++)
            {
                GameObject yeni_klon = Instantiate(engeller[i]);
                yeni_klon.transform.position = new Vector3(yeni_klon.transform.position.x, yeni_klon.transform.position.y, onceki_z + 50f);
                altin_klonla(onceki_z + 5, onceki_z + 45);
                onceki_z += 50;
                _engelSayisi++;
            }
        }
        else
            enabled = false;
    }

    



    void altin_klonla(float z_ilk, float z_son)
    {
        int klon_sayisi = Random.Range(1, 5);
        float aralik = (z_son - z_ilk) / klon_sayisi;
        float temp = z_ilk + aralik;
        float[] x_positions = randomArrayGenerator(_x_sol_max, _x_sag_max, klon_sayisi);



        for (int i = 0; i < klon_sayisi; i++)
        {
            int rand = Random.Range(0, tokenler.Length);
            GameObject yeni_altin = Instantiate(tokenler[rand]);
            yeni_altin.transform.position = new Vector3(x_positions[i], yeni_altin.transform.position.y, temp);
            temp += aralik;
        }
    }

    float[] randomArrayGenerator(float min, float max, int length)
    {
        float[] randoms = new float[length];
        for (int i = 0; i < length; i++)
        {
            randoms[i] = Random.Range(min, max);
        }
        return randoms;
    }

    //###############--Dosyadaki ALTINLARIN VE ENGELLERIN DIZILERE AKTARILDIGI BOLUM--###############################/
    public void nesneleriGetir()
    {
        //Engellerin dosyadan cekildigi bolum
        int seviyeEngelSayisi = 8;
        engeller = new GameObject[seviyeEngelSayisi];//toplam seviye sayisi ile her seviyedeki engel sayisi carpiliyor
        int k = 0;
        for (int j = 0; j < seviyeEngelSayisi; j++)
        {
            GameObject obje = Resources.Load("Engel Prefabları/" + "Cube" + (j + 1), typeof(GameObject)) as GameObject;
            engeller[k] = obje;
            k++;
        }    
        //Butun altin tokenlerinin dosyadan alındıgı bolum
        string[] altin = { "mavi", "sari", "yesil", "Renklendir" };
        tokenler = new GameObject[altin.Length];
        for (int i = 0; i < altin.Length; i++)
        {
            GameObject altin_obje = Resources.Load("Engel Prefabları/" + altin[i] + "AltinEdu", typeof(GameObject)) as GameObject;
            tokenler[i] = altin_obje;
        }
    }
}
