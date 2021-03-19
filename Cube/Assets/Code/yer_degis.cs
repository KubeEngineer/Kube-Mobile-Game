
using UnityEngine;

public class yer_degis : MonoBehaviour
{
    private Transform karakter;
    // Start is called before the first frame update
    void Start()
    {
        karakter = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (karakter.position.z > transform.position.z + 70)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 200f);
            unusedCollector();
        }
    }

    void unusedCollector()
    {
        GameObject[] engelList = GameObject.FindGameObjectsWithTag("EngelDuvar");
        GameObject[] altinList = GameObject.FindGameObjectsWithTag("Altin");
        foreach (GameObject engel in engelList)
        {
            if (engel.transform.position.z + 5 < karakter.position.z)
            {
                Destroy(engel);
                Klon._engelSayisi--;
            }

        }
        foreach (GameObject altin in altinList)
        {
            if (altin.transform.position.z + 5 < karakter.position.z)
            {
                Destroy(altin);
                Klon._engelSayisi--;
            }

        }
    }
}
