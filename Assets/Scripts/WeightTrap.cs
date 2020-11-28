using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightTrap : MonoBehaviour
{
    public float TopForce, Treshold;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(CalculateMass(other.transform.parent.gameObject) > Treshold)
        {
            other.transform.parent.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f,TopForce));
            StartCoroutine("DisableCar", other.transform.parent.gameObject);
        }
               
    }
    private float CalculateMass(GameObject p)
    {
        float w = 0f;
        foreach (Transform child in p.transform)
        {
            Rigidbody2D rbc;
            if(child.gameObject.TryGetComponent<Rigidbody2D>(out rbc))
            if(rbc.bodyType == RigidbodyType2D.Dynamic)
            {
                w += child.gameObject.GetComponent<Rigidbody2D>().mass;
            }
        }
        w += p.GetComponent<Rigidbody2D>().mass;
        return w;
    }
    IEnumerator DisableCar(GameObject car)
    {
        yield return new WaitForSeconds(2);
        car.SetActive(false);
    }
}
