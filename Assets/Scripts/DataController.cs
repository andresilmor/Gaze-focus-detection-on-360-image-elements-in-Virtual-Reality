using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour
{
    [Header("Data Controller Config:")]
    [SerializeField] string fileName;

    private List<ObjectData> objectsData = new List<ObjectData>();

    public void AddObjectFocus(ObjectData objectData) {
        objectsData.Add(objectData);

    }

    public void UpdateData() {
        QuickSortData(0, objectsData.Count - 1);
        
        /*foreach (ObjectData objectData in objectsData) {
            Debug.Log("Nome: " + objectData.name + " | Times: " + objectData.focusTimes + " | Seconds: " + objectData.focusSeconds);
        }*/
        RegisterData();

    }

    private void RegisterData() {
        string data = "";

        for (int index = 0; index < objectsData.Count; index += 1) {
            data += JsonUtility.ToJson(objectsData[index], true);

            if (index + 1 < objectsData.Count)
                data += ", ";

        }

        data = "{\n\"objectsData\": [\n" + data + "]\n}";

        System.IO.File.WriteAllText(Application.dataPath + "/Data.json", data);

    }

    private void QuickSortData(int low, int high) {
        if (low < high) {
            int pivot = Partition(low, high);

            QuickSortData(low, pivot - 1);
            QuickSortData(pivot + 1, high);

        }

    }

    private int Partition(int low, int high) {
        ObjectData temp;
        double pivot = objectsData[high].focusSeconds;
        int lowIndex = low - 1;
        
        for (int index = low; index < high; index += 1) {
            if (objectsData[index].focusSeconds >= pivot) {
                lowIndex += 1;

                temp = objectsData[lowIndex];
                objectsData[lowIndex] = objectsData[index];
                objectsData[index] = temp;

            }

        }

        temp = objectsData[lowIndex + 1];
        objectsData[lowIndex + 1] = objectsData[high];
        objectsData[high] = temp;

        return lowIndex + 1;
    }


}
