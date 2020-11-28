using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class SerialIzableDna
{
    [SerializeField] private BodyTypes Body;
    [SerializeField] private int Wheel1_x, Wheel2_x;
    [SerializeField] private WheelTypes Wheel1Type, Wheel2Type;
    [SerializeField] private int Weight1_x, Weight2_x;
    [SerializeField] private WeightTypes Weight1Type, Weight2Type;
    [SerializeField] private float Fitness;
    [SerializeField] private bool Success;
    [SerializeField] public int Motor1, Motor2;
    [SerializeField] public int Genome;
    public SerialIzableDna(DNA dna, int Genome)
    {
        Body = dna.Body;
        Wheel1_x = dna.Wheel1_x;
        Wheel2_x = dna.Wheel2_x;
        Wheel1Type = dna.Wheel1Type;
        Wheel2Type = dna.Wheel2Type;
        Weight1_x = dna.Weight1_x;
        Weight2_x = dna.Weight2_x;
        Weight1Type = dna.Weight1Type;
        Weight2Type = dna.Weight2Type;
        Fitness = dna.Fitness;
        Success = dna.Success;
        Motor1 = dna.Motor1;
        Motor2 = dna.Motor2;
        this.Genome = Genome;
    }
}
