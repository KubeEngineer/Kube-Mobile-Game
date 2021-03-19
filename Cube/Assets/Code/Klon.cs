
using UnityEngine;

public class Klon : MonoBehaviour
{
    private static GameObject[] tokenler, engeller;
    private GameObject karakter;
    float A_onceki_z;
    float onceki_z;
    int _maxEngel = 20;
    public static int _engelSayisi;
    float _x_sag_max = 2f;
    float _x_sol_max = -2f;
    int olusan_altin_sayisi = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Education") == 1)
        {
            _engelSayisi = 0;
            if (areObjectsEmpty())
                nesneleriGetir();

            karakter = GameObject.FindGameObjectWithTag("Player");
            onceki_z = karakter.transform.position.z;
            while (_engelSayisi < _maxEngel)
            {
                int rast_seviye = Random.Range(0, 100);
                int rast_sayi;
                if (rast_seviye < 30)
                    rast_sayi = Random.Range(engeller.Length / 2, engeller.Length);
                else
                    rast_sayi = Random.Range(0, engeller.Length / 2);

                GameObject yeni_klon = Instantiate(engeller[rast_sayi]);
                yeni_klon.transform.position = new Vector3(yeni_klon.transform.position.x, yeni_klon.transform.position.y, onceki_z + 35f);
                altin_klonla(onceki_z + 5, onceki_z + 30f);
                onceki_z += 35f;
                _engelSayisi++;
            }


            InvokeRepeating("nesne_klonla", 1f, 1f);
        }
        else
            enabled = false;
       
        
        
    }

    // Update is called once per frame
    void nesne_klonla()
    {
        if (KarakterKontrolcü.OyunAktif && _engelSayisi < _maxEngel)
        {
            int rast_seviye = Random.Range(0, 100);
            int rast_sayi;
            if (rast_seviye < 30)
                rast_sayi = Random.Range(engeller.Length / 2, engeller.Length);
            else
                rast_sayi = Random.Range(0, engeller.Length / 2);
            
            GameObject yeni_klon = Instantiate(engeller[rast_sayi]);
            yeni_klon.transform.position = new Vector3(yeni_klon.transform.position.x, yeni_klon.transform.position.y, onceki_z + 35f);
            altin_klonla(onceki_z + 5, onceki_z + 30f);
            onceki_z += 35f;
            _engelSayisi++;
            
        }
        else if (KarakterKontrolcü.OyunAktif)
        {
            StopAllCoroutines();
        }
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
    public static void  nesneleriGetir()
    {
        //Engellerin dosyadan cekildigi bolum
        int seviyeEngelSayisi = 14;
        string[] seviyeler = { "Kolay", "Orta" };
        engeller = new GameObject[seviyeEngelSayisi * seviyeler.Length];//toplam seviye sayisi ile her seviyedeki engel sayisi carpiliyor
        int k = 0;
        for (int i = 0; i < seviyeler.Length; i++)
        {
            for (int j = 0; j < seviyeEngelSayisi; j++)
            {
                GameObject obje = Resources.Load("Engel Prefabları/" + "engel_" + (j + 1) + "_" + seviyeler[i], typeof(GameObject)) as GameObject;

                engeller[k] = obje;
                k++;
            }
        }
        //Butun altin tokenlerinin dosyadan alındıgı bolum
        string[] altin = { "mavi", "sari", "yesil", "Renklendir" };
        tokenler = new GameObject[altin.Length];
        for (int i = 0; i < altin.Length; i++)
        {
            GameObject altin_obje = Resources.Load("Engel Prefabları/" + altin[i] + "Altin", typeof(GameObject)) as GameObject;
            tokenler[i] = altin_obje;
        }
    }

    public static bool areObjectsEmpty()
    { 
        if(engeller == null || tokenler == null)
        {
            return true;
        }
        return false;
    }


   
}
