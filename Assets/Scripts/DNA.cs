using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum BodyTypes
{
    FirstBody,
    SecondBody,
    ThirdBody,
    ForthBody,
    FifthBody
}
[System.Serializable]
public enum WheelTypes
{
    SmallMotor,
    MediumMotor,
    LargeMotor,
    Small,
    Medium,
    Large
}
[System.Serializable]
public enum WeightTypes
{
    Small,
    Medium,
    Large,
    NoWeight
}
public class DNA
{
    public BodyTypes Body;
    public int Wheel1_x, Wheel2_x;
    public WheelTypes Wheel1Type, Wheel2Type;
    public int Weight1_x, Weight2_x;
    public WeightTypes Weight1Type, Weight2Type;
    public float Fitness;
    public bool Success;
    public int Motor1, Motor2;
    public DNA()
    {
        SelectRandomBody();
        SelectRandomWheels();
        SelectRandomWeights();
    }
    public DNA(DNA Parent, DNA Partner, float MutationRate = 0.1f)
    {
        float MutationChance = UnityEngine.Random.Range(0.0f, 1.0f);
        if(MutationChance <= MutationRate)
        {
            SelectRandomBody();
        }else
        {
            int chance = UnityEngine.Random.Range(0, 2);
            Body = (chance == 0) ?  Parent.Body : Partner.Body;
        }
        MutationChance = UnityEngine.Random.Range(0.0f, 1.0f);
        if (MutationChance <= MutationRate)
        {
            Array WheelTypeValues = Enum.GetValues(typeof(WheelTypes));
            Wheel1Type = (WheelTypes)WheelTypeValues.GetValue(UnityEngine.Random.Range(0, WheelTypeValues.Length));
        }
        else
        {
            int chance = UnityEngine.Random.Range(0, 2);
            Wheel1Type = (chance == 0) ? Parent.Wheel1Type : Partner.Wheel1Type;
        }
        MutationChance = UnityEngine.Random.Range(0.0f, 1.0f);
        if (MutationChance <= MutationRate)
        {
            Array WheelTypeValues = Enum.GetValues(typeof(WheelTypes));
            Wheel2Type = (WheelTypes)WheelTypeValues.GetValue(UnityEngine.Random.Range(0, WheelTypeValues.Length));
        }
        else
        {
            int chance = UnityEngine.Random.Range(0, 2);
            Wheel2Type = (chance == 0) ? Parent.Wheel2Type : Partner.Wheel2Type;
        }
        MutationChance = UnityEngine.Random.Range(0.0f, 1.0f);
        if (MutationChance <= MutationRate)
        {
            Motor1 = UnityEngine.Random.Range(0, 21);
        }
        else
        {
            int chance = UnityEngine.Random.Range(0, 2);
            Motor1 = (chance == 0) ? Parent.Motor1 : Partner.Motor1;
        }
        MutationChance = UnityEngine.Random.Range(0.0f, 1.0f);
        if (MutationChance <= MutationRate)
        {
            Motor2 = UnityEngine.Random.Range(0, 21);
        }
        else
        {
            int chance = UnityEngine.Random.Range(0, 2);
            Motor2 = (chance == 0) ? Parent.Motor2 : Partner.Motor2;
        }
        MutationChance = UnityEngine.Random.Range(0.0f, 1.0f);
        if (MutationChance <= MutationRate)
        {
            Wheel1_x = UnityEngine.Random.Range(0, 6);
        }
        else
        {
            int chance = UnityEngine.Random.Range(0, 2);
            Wheel1_x = (chance == 0) ? Parent.Wheel1_x : Partner.Wheel1_x;
        }
        MutationChance = UnityEngine.Random.Range(0.0f, 1.0f);
        if (MutationChance <= MutationRate)
        {
            Wheel2_x = UnityEngine.Random.Range(0, 6);
        }
        else
        {
            int chance = UnityEngine.Random.Range(0, 2);
            Wheel2_x = (chance == 0) ? Parent.Wheel2_x : Partner.Wheel2_x;
        }
        MutationChance = UnityEngine.Random.Range(0.0f, 1.0f);
        if (MutationChance <= MutationRate)
        {
            Array WeightTypeValues = Enum.GetValues(typeof(WeightTypes));
            Weight1Type = (WeightTypes)WeightTypeValues.GetValue(UnityEngine.Random.Range(0, WeightTypeValues.Length));
        }
        else
        {
            int chance = UnityEngine.Random.Range(0, 2);
            Weight1Type = (chance == 0) ? Parent.Weight1Type : Partner.Weight1Type;
        }
        MutationChance = UnityEngine.Random.Range(0.0f, 1.0f);
        if (MutationChance <= MutationRate)
        {
            Array WeightTypeValues = Enum.GetValues(typeof(WeightTypes));
            Weight2Type = (WeightTypes)WeightTypeValues.GetValue(UnityEngine.Random.Range(0, WeightTypeValues.Length));
        }
        else
        {
            int chance = UnityEngine.Random.Range(0, 2);
            Weight2Type = (chance == 0) ? Parent.Weight2Type : Partner.Weight2Type;
        }
        MutationChance = UnityEngine.Random.Range(0.0f, 1.0f);
        if (MutationChance <= MutationRate)
        {
            Weight1_x = UnityEngine.Random.Range(0, 6);
        }
        else
        {
            int chance = UnityEngine.Random.Range(0, 2);
            Weight1_x = (chance == 0) ? Parent.Weight1_x : Partner.Weight1_x;
        }
        MutationChance = UnityEngine.Random.Range(0.0f, 1.0f);
        if (MutationChance <= MutationRate)
        {
            Weight2_x = UnityEngine.Random.Range(0, 6);
        }
        else
        {
            int chance = UnityEngine.Random.Range(0, 2);
            Weight2_x = (chance == 0) ? Parent.Weight2_x : Partner.Weight2_x;
        }
        if (Wheel1Type == WheelTypes.SmallMotor || Wheel1Type == WheelTypes.MediumMotor || Wheel1Type == WheelTypes.LargeMotor)
        {
            Motor1 = UnityEngine.Random.Range(0, 21);
        }
        else
        {
            Motor1 = -1;
        }
        if (Wheel2Type == WheelTypes.SmallMotor || Wheel2Type == WheelTypes.MediumMotor || Wheel2Type == WheelTypes.LargeMotor)
        {
            Motor2 = UnityEngine.Random.Range(0, 21);
        }
        else
        {
            Motor2 = -1;
        }
        if (Body == BodyTypes.FifthBody)
        {
            Weight1Type = WeightTypes.NoWeight;
            Weight2Type = WeightTypes.NoWeight;
            Weight1_x = -1;
            Weight2_x = -1;
        }
        else
        {
            if (Weight1Type == WeightTypes.NoWeight)
               Weight1_x = -1;
            if (Weight2Type == WeightTypes.NoWeight)
                Weight2_x = -1;
        }
    }

    private void SelectRandomBody()
    {
        Array BodyValues = Enum.GetValues(typeof(BodyTypes));
        Body = (BodyTypes)BodyValues.GetValue(UnityEngine.Random.Range(0, BodyValues.Length));
    }
    private void SelectRandomWheels()
    {
        Array WheelTypeValues = Enum.GetValues(typeof(WheelTypes));
        Wheel1Type = (WheelTypes)WheelTypeValues.GetValue(UnityEngine.Random.Range(0, WheelTypeValues.Length));
        Wheel2Type = (WheelTypes)WheelTypeValues.GetValue(UnityEngine.Random.Range(0, WheelTypeValues.Length));
        Wheel1_x = UnityEngine.Random.Range(0,6);
        Wheel2_x = UnityEngine.Random.Range(0, 6);
        if(Wheel1Type == WheelTypes.SmallMotor || Wheel1Type == WheelTypes.MediumMotor || Wheel1Type == WheelTypes.LargeMotor)
        {
            Motor1 = UnityEngine.Random.Range(0, 21);
        }else
        {
            Motor1 = -1;
        }
        if (Wheel2Type == WheelTypes.SmallMotor || Wheel2Type == WheelTypes.MediumMotor || Wheel2Type == WheelTypes.LargeMotor)
        {
            Motor2 = UnityEngine.Random.Range(0, 21);
        }
        else
        {
            Motor2 = -1;
        }
    }
    private void SelectRandomWeights()
    {
        Array WeightTypeValues = Enum.GetValues(typeof(WeightTypes));
        Weight1Type = (WeightTypes)WeightTypeValues.GetValue(UnityEngine.Random.Range(0, WeightTypeValues.Length));
        Weight2Type = (WeightTypes)WeightTypeValues.GetValue(UnityEngine.Random.Range(0, WeightTypeValues.Length));
        Weight1_x = UnityEngine.Random.Range(0, 6);
        Weight2_x = UnityEngine.Random.Range(0, 6);
        if(Body == BodyTypes.FifthBody)
        {
            Weight1Type = WeightTypes.NoWeight;
            Weight2Type = WeightTypes.NoWeight;
            Weight1_x = -1;
            Weight2_x = -1;
        }
        else
        {
            if (Weight1Type == WeightTypes.NoWeight)
                Weight1_x = -1;
            if (Weight2Type == WeightTypes.NoWeight)
                Weight2_x = -1;
        }
    }
}
