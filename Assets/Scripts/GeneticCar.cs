using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GeneticCar : MonoBehaviour
{

    public DNA dna;
    public GameObject Wheel1, Wheel2, Weight1, Weight2;
    public SpriteRenderer Wheel1Area, Wheel2Area, Weight1Area, Weight2Area;
    public bool initialized, freezed, activated;
    private const float MoveTreshold = 0.01f;
    private float PreviousX = float.MinValue;
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitCar()
    {
        activated = true;
        GenerateWheel(new Area(Wheel1Area), Wheel1, dna.Wheel1Type , 1);
        GenerateWheel(new Area(Wheel2Area), Wheel2, dna.Wheel2Type , 2);
        if(dna.Weight1_x > -1)
            GenerateWeight(new Area(Weight1Area), Weight1, dna.Weight1Type, 1);
        if (dna.Weight2_x > -1)
            GenerateWeight(new Area(Weight2Area), Weight2, dna.Weight1Type, 2);
        freezed = true;
        StartCoroutine("UnFreeze", gameObject.GetComponent<Rigidbody2D>());
        initialized = true;

    }

    private void GenerateWeight(Area area, GameObject weight, WeightTypes type, int index)
    {
        if (index == 1)
        {
            switch (dna.Weight1Type)
            {
                case WeightTypes.Medium:
                    weight.transform.position = new Vector3(area.sx + dna.Weight1_x * 0.2674508f, weight.transform.position.y, 0);
                    weight.transform.localScale = new Vector3(0.55f, 0.55f, 0.55f);
                    weight.GetComponent<Rigidbody2D>().mass = 0.25f;
                    break;
                case WeightTypes.Small:
                    weight.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
                    weight.transform.position = new Vector3(area.sx + dna.Weight1_x * 0.2674508f, weight.transform.position.y - 0.25f, 0);
                    weight.GetComponent<Rigidbody2D>().mass = 0.15f;
                    break;
                case WeightTypes.Large:
                    weight.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                    weight.transform.position = new Vector3(area.sx + dna.Weight1_x * 0.2674508f, weight.transform.position.y + 0.15f, 0);
                    weight.GetComponent<Rigidbody2D>().mass = 0.5f;
                    break;
            }
            Weight1.SetActive(true);
        }
        else
        {
            switch (dna.Weight2Type)
            {
                case WeightTypes.Medium:
                    weight.transform.position = new Vector3(area.sx + dna.Weight2_x * 0.2674508f, weight.transform.position.y, 0);
                    weight.transform.localScale = new Vector3(0.55f, 0.55f, 0.55f);
                    weight.GetComponent<Rigidbody2D>().mass = 0.25f;
                    break;
                case WeightTypes.Small:
                    weight.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
                    weight.transform.position = new Vector3(area.sx + dna.Weight2_x * 0.2674508f, weight.transform.position.y - 0.25f, 0);
                    weight.GetComponent<Rigidbody2D>().mass = 0.15f;
                    break;
                case WeightTypes.Large:
                    weight.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                    weight.transform.position = new Vector3(area.sx + dna.Weight2_x * 0.2674508f, weight.transform.position.y + 0.15f, 0);
                    weight.GetComponent<Rigidbody2D>().mass = 0.5f;
                    break;
            }
            Weight2.SetActive(true);
        }
    }

    private void GenerateWheel(Area wa, GameObject Wheel, WheelTypes Type, int index)
    {
        if (index == 1)
        {
            Wheel.transform.position = new Vector3(wa.sx + dna.Wheel1_x * 0.2674508f, Wheel.transform.position.y, 0);
            if(dna.Motor1 > -1)
            {
                JointMotor2D motor2D = new JointMotor2D();
                motor2D.motorSpeed = dna.Motor1 * 50f + 500;
                motor2D.maxMotorTorque = 10000f;
                Wheel.GetComponent<WheelJoint2D>().motor = motor2D;
            }

        }
        else
        {
            Wheel.transform.position = new Vector3(wa.sx + dna.Wheel2_x * 0.2674508f, Wheel.transform.position.y, 0);
            if (dna.Motor1 > -1)
            {
                JointMotor2D motor2D = new JointMotor2D();
                motor2D.motorSpeed = dna.Motor2 * 50f + 500;
                motor2D.maxMotorTorque = 10000f;
                Wheel.GetComponent<WheelJoint2D>().motor = motor2D;
            }
        }
        ConfigWheelProperties(Wheel, Type);
        WheelJoint2D wj = Wheel.GetComponent<WheelJoint2D>();
        wj.connectedAnchor.Set(Wheel.transform.position.x, Wheel.transform.position.y);
        wj.connectedBody.WakeUp();
        Wheel.GetComponent<SpriteRenderer>().enabled = true;
        Wheel.GetComponent<WheelJoint2D>().enabled = true;
    }   
    private void ConfigWheelProperties(GameObject Wheel, WheelTypes Type)
    {
        switch (Type)
        {
            case WheelTypes.Small:
                Wheel.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                Wheel.GetComponent<WheelJoint2D>().useMotor = false;
                Wheel.GetComponent<SpriteRenderer>().color = Color.white;
                break;
            case WheelTypes.Medium:
                Wheel.transform.localScale = new Vector3(0.52f, 0.52f, 0.52f);
                Wheel.GetComponent<WheelJoint2D>().useMotor = false;
                Wheel.GetComponent<SpriteRenderer>().color = Color.white;
                break;
            case WheelTypes.Large:
                Wheel.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                Wheel.GetComponent<WheelJoint2D>().useMotor = false;
                Wheel.GetComponent<SpriteRenderer>().color = Color.white;
                break;
            case WheelTypes.SmallMotor:
                Wheel.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                Wheel.GetComponent<WheelJoint2D>().useMotor = true;
                Wheel.GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case WheelTypes.MediumMotor:
                Wheel.transform.localScale = new Vector3(0.52f, 0.52f, 0.52f);
                Wheel.GetComponent<WheelJoint2D>().useMotor = true;
                Wheel.GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case WheelTypes.LargeMotor:
                Wheel.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                Wheel.GetComponent<WheelJoint2D>().useMotor = true;
                Wheel.GetComponent<SpriteRenderer>().color = Color.green;
                break;
        }
    }
    IEnumerator UnFreeze(Rigidbody2D rb)
    {
        yield return new WaitForSeconds(3);
        rb.constraints = RigidbodyConstraints2D.None;
        freezed = false;
    }
    
}
