using System.Collections.Generic;
using UnityEngine;

class AnimalManager : MonoBehaviour
{   //동물들의 정보를 관리한다.
    //AnimalSaving 을통해 동물들의 정보를 저장하고 로드하며.
    //AnimalData를 통해 불러온 동물의 정보를 확인한다.

    public List<AnimalSaving> animalSavings = new List<AnimalSaving>(); //동물 데이터 저장.
    public class AnimalSaving
    {
        public AnimalSaving(string animalName, int buildingIndex, int currentDate = 0, int level = 0, bool pet = false)
        {
            AnimalName = animalName;
            BuildingIndex = buildingIndex;
            CurrentDate = currentDate;
            Level = level;
            Pet = pet;
        }
        public string AnimalName;
        public int BuildingIndex;
        public int CurrentDate;
        public int Level;
        public bool Pet;
    }

    [SerializeField] TextAsset AnimalDataCsv;
    List<Dictionary<string, string>> AnimalData;

    private void Awake()
    {
        if (AnimalDataCsv) AnimalData = new ParseCsvFile().ParseCsv(AnimalDataCsv.text);
    }

    public void AddNewAnimal(string animalName, int buildingIndex)
    {
        animalSavings.Add(new AnimalSaving(animalName, buildingIndex));
    }

    public void SetAnimalsInsideFarm(int buildingIndex)
    {
        if(animalSavings.Count == 0 || animalSavings == null)
        {
            return;
        }

        List<int> thisBuildingAnimal = new List<int>();

        for(int i = 0; i < animalSavings.Count; i++)
        {
            if(animalSavings[i].BuildingIndex == buildingIndex)
            {
                Debug.Log("동물을 생성합니다.");
                Debug.Log($"동물번호 : {i}.");
                Debug.Log($"동물이름 : {animalSavings[i].AnimalName}.");
            }
        }

    }
}