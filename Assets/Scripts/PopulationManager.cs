using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulationManager : MonoBehaviour
{
    public GameObject Camera;
    private List<DNA> genes = new List<DNA>();
    public int PopulationSize = 5;
    private GameObject PresentAgent;
    private GeneticCar GC;
    private int AgentIndex;
    private float PreviousX = float.MinValue;
    private const float MoveTreshold = 0.2f;
    public Transform SpawnPoint, EndPoint;
    public GameObject   FirstBody,
                        SecondBody,
                        ThirdBody,
                        ForthBody,
                        FifthBody;
    private float BestDistance = float.MinValue, GenerationBestDistance = float.MinValue;
    public Text GenerationText, PopulationText, MutationRateText, BestDistanceText, CurrentDistanceText, GenomeText;
    private int Generation;
    public float cutoff = 0.3f;

    private void Awake()
    {
        PopulationInit();
        InitUI();
    }

    void Start()
    {
        Debug.Log(EndPoint.position.x - SpawnPoint.position.x);
    }

    void FixedUpdate()
    {
         CurrentDistanceText.GetComponent<Text>().text = "Current Distance: " + (PresentAgent.transform.position.x - SpawnPoint.position.x).ToString();
        if(PresentAgent.transform.position.x > GenerationBestDistance + SpawnPoint.position.x)
        {
            GenerationBestDistance = PresentAgent.transform.position.x - SpawnPoint.position.x;
            BestDistanceText.text = "Best Distance: " + GenerationBestDistance.ToString();
        }
        if (PresentAgent.transform.position.x >= EndPoint.position.x)
        {
            TerminateAgent(true);
        }

    }
    private void PopulationInit()
    {
        for(int i = 0; i < PopulationSize; i++)
        {
            genes.Add(new DNA());
        }
        AgentIndex = 0;
        SpawnCar();

    }
    private void CheckMovement()
    {
        if (!GC.freezed && GC.initialized && GC.activated)
        {
            if (PreviousX == float.MinValue)
            {
                PreviousX = transform.position.x;
            }
            else if (PresentAgent.transform.position.x < PreviousX + MoveTreshold && PresentAgent.transform.position.x > PreviousX - MoveTreshold)
            {
                TerminateAgent();
            }
            PreviousX = PresentAgent.transform.position.x;
            if (PresentAgent.transform.position.x >= BestDistance)
            {
                BestDistance = PresentAgent.transform.position.x;
            }
            else
            {
                TerminateAgent();
            }
        }
    }
    private void SpawnCar()
    {
        if(AgentIndex >= PopulationSize)
        {
            NextGeneration();
            return;
        }
        switch (genes[AgentIndex].Body)
        {
            case BodyTypes.FirstBody:
                PresentAgent = Instantiate(FirstBody, SpawnPoint.position, Quaternion.identity);
                break;
            case BodyTypes.SecondBody:
                PresentAgent = Instantiate(SecondBody, SpawnPoint.position, Quaternion.identity);
                break;
            case BodyTypes.ThirdBody:
                PresentAgent = Instantiate(ThirdBody, SpawnPoint.position, Quaternion.identity);
                break;
            case BodyTypes.ForthBody:
                PresentAgent = Instantiate(ForthBody, SpawnPoint.position, Quaternion.identity);
                break;
            case BodyTypes.FifthBody:
                PresentAgent = Instantiate(FifthBody, SpawnPoint.position, Quaternion.identity);
                break;
        }
        Camera.GetComponent<cameraFollow>().player = PresentAgent.transform;
        GC = PresentAgent.GetComponent<GeneticCar>();
        GC.dna = genes[AgentIndex];
        GC.InitCar();
        InvokeRepeating("CheckMovement",6f, 5f);
        GenomeText.text = "Genome: " + (AgentIndex + 1).ToString();
    }

    private void NextGeneration()
    {
        SaveData();
        int survivorCut = Mathf.RoundToInt(PopulationSize * cutoff);
        List<DNA> survivors = new List<DNA>();
        for (int i = 0; i < survivorCut; i++)
        {
            survivors.Add(GetFittest());
        }
        genes.Clear();
        while(genes.Count < PopulationSize)
        {
            for (int i = 0; i < survivors.Count; i++)
            {
                genes.Add(new DNA(survivors[i], survivors[UnityEngine.Random.Range(0, Mathf.RoundToInt(survivorCut / 3) + 1)]));
                if (genes.Count >= PopulationSize)
                    break;
            }
        }
        AgentIndex = 0;
        Generation++;
        GenerationText.text = "Generation: " + (Generation + 1).ToString();
        SpawnCar();

    }
    private DNA GetFittest()
    {
        DNA tmp = null;
        int index = -1;
        float MaxFitness = float.MinValue;
        for (int i = 0; i < genes.Count; i++)
        {
            if(genes[i].Fitness > MaxFitness)
            {
                MaxFitness = genes[i].Fitness;
                tmp = genes[i];
                index = i;
            }
        }
        genes.RemoveAt(index);
        return tmp;
    }
    private float Fitness()
    {
        if (BestDistance > PresentAgent.transform.position.x)
            return (EndPoint.position.x - SpawnPoint.position.x) / (BestDistance - SpawnPoint.position.x);
        return (EndPoint.position.x - SpawnPoint.position.x) / (PresentAgent.transform.position.x - SpawnPoint.position.x);
    }
    private void TerminateAgent(bool Sucsess = false)
    {
        GC.activated = false;
        GC.dna.Fitness = (PresentAgent.transform.position.x - SpawnPoint.position.x) / (EndPoint.position.x - SpawnPoint.position.x);
        GC.dna.Success = Sucsess;
        PreviousX = float.MinValue;
        BestDistance = float.MinValue;
        PresentAgent.SetActive(false);
        Destroy(PresentAgent.gameObject);
        CancelInvoke("CheckMovement");
        AgentIndex++;
        SpawnCar();
    }
    private void InitUI()
    {
        GenerationText.text = "Generation: " +  (Generation + 1).ToString();
        PopulationText.text = "Population: " + PopulationSize.ToString();
        MutationRateText.text = "Mutation Rate: 10%";

    }
    private void SaveData()
    {
        GenerationData Data = new GenerationData();
        for (int i = 0; i < genes.Count; i++)
        {
            Data.AddData(new SerialIzableDna(genes[i], i + 1));
        }
        System.IO.File.WriteAllText(Application.persistentDataPath + "/Generation" + (Generation + 1).ToString() + ".json", JsonUtility.ToJson(Data,true));
    }
}
