using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.G2OM;

public class ObjectFocus : MonoBehaviour, IGazeFocusable
{

    [Header("Object Focus Config:")]
    [Tooltip("In Seconds.")]
    [SerializeField] float startCountAt = 5f;

    private string nameToRegister;
    private float focusSeconds;
    private float time;

    private bool hasFocus = false;

    private ObjectData objectData = new ObjectData();
    private DataController dataController;

    private void Start() {
        objectData.name = gameObject.name;
        objectData.focusSeconds = 0;
        objectData.focusTimes = 0;

        ResetData();

        dataController = FindObjectOfType<DataController>();
        dataController.AddObjectFocus(objectData);

    }

    public void GazeFocusChanged(bool hasFocus) {
        this.hasFocus = hasFocus;
        if (hasFocus) {
            StartCoroutine(CountFocusTime());

        } else {
            StopCoroutine(CountFocusTime());
            objectData.focusSeconds += System.Math.Round(focusSeconds, 2);
            //Debug.Log(nameToRegister + ": " + objectData.focusSeconds + " seconds;");
            //Debug.Log(nameToRegister + ": " + objectData.focusTimes + " time;");
            dataController.UpdateData();
            ResetData();

        }

    }

    IEnumerator CountFocusTime() {
        while (hasFocus) {
            if (time <= startCountAt) {
                time += Time.deltaTime;

            } else {
                if (focusSeconds == 0)
                    objectData.focusTimes += 1;

                focusSeconds += Time.deltaTime;

            }
            yield return null;

        }

    }

    private void ResetData() {
        time = 0;
        focusSeconds = 0;

    }

}
