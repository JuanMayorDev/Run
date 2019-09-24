using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool standing;
    public Material mat;
    public float glow_speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Glow")
        {
            Debug.LogError("Hey");
            mat = collision.transform.GetComponent<Renderer>().material;
            StartCoroutine("Glow");
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.transform.tag == "Glow")
        {
            //Debug.LogError(collision.transform.GetComponent<MeshRenderer>().material);
            standing = false;
        }
    }
    IEnumerator Glow()
    {
        standing = true;

        while (standing)
        {
            mat.SetFloat("Vector1_C5C79D8E", mat.GetFloat("Vector1_C5C79D8E") + Time.deltaTime * glow_speed);
            yield return new WaitForEndOfFrame();
        }


        yield return null;
    }
}
