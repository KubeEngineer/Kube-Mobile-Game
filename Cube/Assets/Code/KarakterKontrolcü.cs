
using UnityEngine;


public class KarakterKontrolcü : MonoBehaviour
{
    public static bool OyunAktif;
    public static bool OyunBitti;
    public GameManager manager;
    public EducationManager EduManager;
    //Yeni Bolge Parcalanma icin
    public float cubeSize = 0.5f;
    public float explosionRadius = 2f;
    public float explosionUpward = 0.05f;
    public float explosionForce = 85f;
    private Vector3 explosionPosition;
    private int cubesInrow_z = 3;
    private int cubesInrow_x = 3;
    private int cubesInrow_y = 3;
    private Color yol;
    private Color zemin;
    private Color backGround;
    private Camera cam;
    float cubesPivotDistance_x;
    float cubesPivotDistance_y;
    float cubesPivotDistance_z;
    Vector3 cubesPivot;
    Material material, material1;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().material = Resources.Load("Material/" + PlayerPrefs.GetString("GecerliKarakter"), typeof(Material)) as Material;
        material = Resources.Load("Material/yol", typeof(Material)) as Material;
        material1 = Resources.Load("Material/zemin", typeof(Material)) as Material;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        yol = material.color;
        zemin = material1.color;
        backGround = cam.backgroundColor;

        OyunAktif = true;
        OyunBitti = false;
        material.color = Random.ColorHSV();
        material1.color = Random.ColorHSV();
        cam.backgroundColor = Random.ColorHSV();
    }
    void Update()
    {
        if (material.color != yol && material1.color != zemin && cam.backgroundColor != backGround)
        {
            material.color = Color.Lerp(material.color, yol, Time.deltaTime * 10);
            material1.color = Color.Lerp(material1.color, zemin, Time.deltaTime * 10);
            cam.backgroundColor = Color.Lerp(cam.backgroundColor, backGround, Time.deltaTime * 10);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(PlayerPrefs.GetInt("Education") == 1 && PlayerPrefs.GetInt("EducationFirst") == 1)
        {
            switch (other.gameObject.tag)
            {

                case "Altin":
                    manager.skorHesap(2);
                    Destroy(other.gameObject);
                    //GetComponent<AudioSource>().PlayOneShot(manager.altin,1f);
                    AudioSource.PlayClipAtPoint(manager.altin, transform.position, 2f);
                    // GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().backgroundColor = Random.ColorHSV();
                    break;
                case "Renklendir":
                    Destroy(other.gameObject);

                    AudioSource.PlayClipAtPoint(manager.altin, transform.position, 2f);
                    yol = Random.ColorHSV();
                    zemin = Random.ColorHSV();
                    backGround = Random.ColorHSV();
                    manager.skorHesap(4);
                    AudioSource.PlayClipAtPoint(manager.altin, transform.position, 2f);
                    break;
                case "EngelDuvar":
                    OyunAktif = false;
                    OyunBitti = true;
                    Destroy();
                    explosionPosition = gameObject.GetComponent<Collider>().ClosestPoint(other.gameObject.transform.position);
                    //StartCoroutine(Destroy());
                    break;
            }
        }else
        {
            switch(other.gameObject.tag)
            {
                case "AltinEdu":
                    EduManager.skorHesap(10);
                    Destroy(other.gameObject);
                    //GetComponent<AudioSource>().PlayOneShot(manager.altin,1f);
                    AudioSource.PlayClipAtPoint(manager.altin, transform.position, 2f);
                    // GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().backgroundColor = Random.ColorHSV();
                    break;
                case "RenklendirEdu":
                    Destroy(other.gameObject);

                    AudioSource.PlayClipAtPoint(manager.altin, transform.position, 2f);
                    yol = Random.ColorHSV();
                    zemin = Random.ColorHSV();
                    backGround = Random.ColorHSV();
                    EduManager.skorHesap(20);
                    AudioSource.PlayClipAtPoint(manager.altin, transform.position, 2f);
                    break;
                case "EngelDuvar":
                    OyunAktif = false;
                    OyunBitti = true;
                    Destroy();
                    explosionPosition = gameObject.GetComponent<Collider>().ClosestPoint(other.gameObject.transform.position);
                    //StartCoroutine(Destroy());
                    break;
            }
        }
       
    }


    void Destroy()
    {
        cubesInrow_x = Mathf.RoundToInt(transform.localScale.x / cubeSize);
        cubesInrow_y = Mathf.RoundToInt(transform.localScale.y / cubeSize);
        cubesPivotDistance_x = cubeSize * cubesInrow_x / 2;
        cubesPivotDistance_y = cubeSize * cubesInrow_y / 2;
        cubesPivotDistance_z = cubeSize * cubesInrow_z / 2;
        cubesPivot = new Vector3(cubesPivotDistance_x, cubesPivotDistance_y, cubesPivotDistance_z);
        //dokundugu noktayi alma
        //ContactPoint contact = other.contacts[0];
        // GetComponent<AudioSource>().PlayOneShot(manager.engel, 1f);
        AudioSource.PlayClipAtPoint(manager.engel, transform.position, 2f);
        gameObject.SetActive(false);


        for (int x = 0; x < cubesInrow_x; x++)
        {
            for (int y = 0; y < cubesInrow_y; y++)
            {
                for (int z = 0; z < cubesInrow_z; z++)
                {
                    createPiece(x, y, z);
                }
            }
        }
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, explosionUpward);
            }
        }
    }
    void createPiece(int x, int y, int z)
    {
        //parca olusturma
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //parca boyu ve pozisyonu ayarlama
        piece.transform.position = transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - cubesPivot;
        piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

        ////////////////////////////////
        piece.GetComponent<Renderer>().material = GetComponent<Renderer>().material;
        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = cubeSize;
    }
}
