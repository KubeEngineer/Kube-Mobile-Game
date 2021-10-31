
using UnityEngine;

public class Boyuntlandir : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;
    private const float min = 0.3f;
    private const float max = 2f;
    float velocityDir = 5f;

    [Range(0,5)]
    public float velocityScale = 2f;
    float x, y, volume = 0.5f;

    // Start is callhed before te first frame update
    void Start()
    {
        x = transform.localScale.x;
        y = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {   if(KarakterKontrolcü.OyunAktif)
        {
            if (Input.GetMouseButton(0))
            {
                float x = Input.GetAxis("Mouse X");
                float y = Input.GetAxis("Mouse Y");
                if(x!=0) setPosition(x); 
                if(y!=0) setScale(y);
                source.volume = Mathf.Lerp(source.volume, volume, Time.deltaTime * 10);
            }
        }
    }
    
   

    void setScale(float difference)
    {
        x -= Time.deltaTime * difference * velocityScale;
        y += Time.deltaTime * difference * velocityScale;

        volume = Mathf.Clamp01(y);
        x = Mathf.Clamp(x, min, max);
        y = Mathf.Clamp(y, min, max);
        float y_degisim = transform.localScale.y - y;

        transform.localScale = new Vector3(x, y, 1);
        transform.position += new Vector3(0,- (y_degisim / 2),0);
    }

     void setPosition(float difference){
        transform.position += Vector3.right * Time.deltaTime * difference * velocityDir;
        ClampPosition();
    }

    /// <summary>
    /// nesnelerin x pozisyonunu kamera içerisine sabitleme. 
    /// </summary>
    public void ClampPosition()
    {
        //scale x 0.3:0.8 ,2:0.9
        // sol : 0.3 => -2.6  2 => -1.73 fark = 0.87
        // sag : 0.3 =>  2.6  2 =>  1.73 fark = 0.87
        // sol : 0.3 => 0.11 2 => 0.24 fark = 0.13
        // sag : 0.3 => 0.89 2 => 0.76 fark = 0.13
        //fark(0.11,0.24) = 0.11 + (0.24-0.11)/1.7 * (scale.x - 0.3);
        /// <summary>
        /// karakterin scale x degerinin karsiligi olan kamera icindeki sinirlarina gore
        /// kameranin içinde sinirlama yapar.
        /// </summary>
        Vector3 poss = transform.position;
        float rule = ((0.87f/(max-min))*(transform.localScale.x-min));
        float minX = -2.6f+rule;
        float maxX = 2.6f-rule ;
        poss.x = Mathf.Clamp(poss.x,minX,maxX);

        transform.position = poss;
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
