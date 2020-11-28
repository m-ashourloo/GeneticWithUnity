using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GenerationData
{
    [SerializeField]
    public List<SerialIzableDna> data = new List<SerialIzableDna>();
    public void AddData(SerialIzableDna dna)
    {
        data.Add(dna);
    }
}
