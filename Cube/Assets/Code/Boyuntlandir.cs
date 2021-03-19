
using UnityEngine;

public class Boyuntlandir : MonoBehaviour
{
    public AudioSource source;
    const float min = 0.3f;
    const float max = 2f;
    [SerializeField]
    private float sag_limit = 2.5f;
    [SerializeField]
    private float sol_limit = -2.5f;
    float hiz = 5f;
    float hizBoyut = 0.2f;
    float x, y, onceki_x, x_degisim,volume=0.5f;
    Vector3 son_konum;
    bool basma_aktif_mi = false;


    // Start is called before the first frame update
    void Start()
    {
        x = transform.localScale.x;
        y = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (KarakterKontrolcü.OyunAktif)
            Ayarla();
        source.volume = Mathf.Lerp(source.volume, volume, Time.deltaTime * 10);
    }
    void Ayarla()
    {
        if (Input.GetMouseButtonDown(0))
        {
            basma_aktif_mi = true;
        }
        if (Input.GetMouseButton(0))
        {


            if (basma_aktif_mi)
            {
                son_konum = Input.mousePosition;
                basma_aktif_mi = false;
                return;

            }
            ///yonlendirme//////
            float degisim = Input.GetAxis("Mouse X");
            degisim *= Time.deltaTime * hiz;
            degisim = transform.position.x + degisim;
            if (degisim < (sag_limit - x_degisim) && degisim > (sol_limit - x_degisim))
                transform.position = new Vector3(degisim, transform.position.y, transform.position.z);
            ////////////////////
            float fark = Input.mousePosition.y - son_konum.y;
            if (fark > 0)
            {
                x -= Time.deltaTime * fark * hizBoyut;
                y += Time.deltaTime * fark * hizBoyut;
            }
            else
            {
                x -= Time.deltaTime * fark * hizBoyut;
                y += Time.deltaTime * fark * hizBoyut;
            }
            son_konum = Input.mousePosition;
            volume = Mathf.Clamp01(y);
            x = Mathf.Clamp(x, min, max);
            y = Mathf.Clamp(y, min, max);
            float y_degisim = transform.localScale.y - y;

            transform.localScale = new Vector3(x, y, 1);

            if (transform.position.x < 0)
                x_degisim = -(transform.localScale.x - 0.3f) * 0.45f;
            else
                x_degisim = (transform.localScale.x - 0.3f) * 0.45f;

            if (degisim >= (sag_limit - x_degisim))
                transform.position = new Vector3(transform.position.x - Mathf.Abs(y_degisim / 2), transform.position.y - (y_degisim / 2), transform.position.z);
            else if (degisim <= (sol_limit - x_degisim))
                transform.position = new Vector3(transform.position.x + Mathf.Abs(y_degisim / 2), transform.position.y - (y_degisim / 2), transform.position.z);
            else
                transform.position = new Vector3(transform.position.x, transform.position.y - (y_degisim / 2), transform.position.z);



        }

    }

    public void setX(float x)
    {
        this.x = x;
    }

    public void setY(float y)
    {
        this.y = y;
    }
}
