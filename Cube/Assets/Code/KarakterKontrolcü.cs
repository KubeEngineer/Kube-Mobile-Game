
using UnityEngine;
using System.Collections;

public class KarakterKontrolcü : MonoBehaviour
{
    public static bool OyunAktif;
    public static bool OyunBitti;
    public ParticleSystem efect;
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
    private IEnumerator currentLerp;
    float cubesPivotDistance_x;
    float cubesPivotDistance_y;
    float cubesPivotDistance_z;
    Vector3 cubesPivot;
    Material materialRoad, materialFloor;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().material = Resources.Load("Material/" + PlayerPrefs.GetString("GecerliKarakter"), typeof(Material)) as Material;
        materialRoad = Resources.Load("Material/yol", typeof(Material)) as Material;
        materialFloor = Resources.Load("Material/zemin", typeof(Material)) as Material;
        cam = Camera.main;
        yol = materialRoad.color;
        zemin = materialFloor.color;
        backGround = cam.backgroundColor;

        OyunAktif = true;
        OyunBitti = false;
        materialRoad.color = Random.ColorHSV();
        materialFloor.color = Random.ColorHSV();
        cam.backgroundColor = Random.ColorHSV();
    }

    IEnumerator coroutineLerp()
    {
        yield return new WaitWhile(colorLerp);
    }

    bool colorLerp()
    {

        materialRoad.color = Color.Lerp(materialRoad.color, yol, Time.deltaTime * 3);
        materialFloor.color = Color.Lerp(materialFloor.color, zemin, Time.deltaTime * 3);
        cam.backgroundColor = Color.Lerp(cam.backgroundColor, backGround, Time.deltaTime * 3);

        return materialRoad.color != yol || materialFloor.color != zemin || cam.backgroundColor != backGround;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.isEduDone)
        {
            switch (other.gameObject.tag)
            {

                case "Altin":
                    efect.transform.position = other.gameObject.transform.position;
                    var col = efect.colorOverLifetime;
                    Gradient grad = new Gradient();
                    Color startColor = other.gameObject.GetComponent<MeshRenderer>().material.color;
                    Destroy(other.gameObject);
                    grad.SetKeys(new GradientColorKey[]{new GradientColorKey(startColor,0.0f),new GradientColorKey(Color.red,1.0f)},
                    new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0f, 1.0f) });
                    col.color = grad;
                    if(efect.isPlaying) {
                        efect.Stop();
                        print(efect.isStopped);
                    }
                    efect.Play();
                    print(efect.isPlaying);
                    manager.skorHesap(2);
                    AudioSource.PlayClipAtPoint(manager.altin, transform.position, 2f);

                    
                    break;
                case "Renklendir":
                    Destroy(other.gameObject);
                    changeColor();
                    manager.skorHesap(4);
                    AudioSource.PlayClipAtPoint(manager.altin, transform.position, 2f);
                    break;
                case "EngelDuvar":
                    OyunAktif = false;
                    OyunBitti = true;
                    Destroy();
                    explosionPosition = gameObject.GetComponent<Collider>().ClosestPoint(other.gameObject.transform.position);
                    break;
            }
        }
        else
        {
            switch (other.gameObject.tag)
            {
                case "AltinEdu":
                    EduManager.skorHesap(10);
                    Destroy(other.gameObject);
                    AudioSource.PlayClipAtPoint(manager.altin, transform.position, 2f);
                    break;
                case "RenklendirEdu":
                    Destroy(other.gameObject);

                    AudioSource.PlayClipAtPoint(manager.altin, transform.position, 2f);
                    changeColor();


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

    void changeColor()
    {
        yol = Random.ColorHSV();
        zemin = Random.ColorHSV();
        backGround = Random.ColorHSV();
        StopAllCoroutines();
        StartCoroutine(coroutineLerp());
    }

    void Destroy()
    {
        cubesInrow_x = Mathf.RoundToInt(transform.localScale.x / cubeSize);
        cubesInrow_y = Mathf.RoundToInt(transform.localScale.y / cubeSize);
        cubesPivotDistance_x = cubeSize * cubesInrow_x / 2;
        cubesPivotDistance_y = cubeSize * cubesInrow_y / 2;
        cubesPivotDistance_z = cubeSize * cubesInrow_z / 2;
        cubesPivot = new Vector3(cubesPivotDistance_x, cubesPivotDistance_y, cubesPivotDistance_z);
        // dokundugu noktayi alma
        // ContactPoint contact = other.contacts[0];
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
