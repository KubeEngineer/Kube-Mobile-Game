using System.Collections;

using UnityEngine;

public class Renklendir : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RenkDegis());
    }
    IEnumerator RenkDegis()
    {
        while(true)
        {
            GetComponent<MeshRenderer>().material.color= Random.ColorHSV();
            yield return new WaitForSeconds(0.2f);
        }
    }
}
