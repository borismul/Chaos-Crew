using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerPrediction : MonoBehaviour 
{

    List<string> inputsSinceSync = new List<string>();
    List<float> inputTime = new List<float>();

    KeyCode curInput;
    KeyCode prevInput;

    float keyPressTimeStart;

    void Update()
    {
        if (Input.GetKey(KeyCode.None))
        {
            curInput = KeyCode.None;
            Debug.Log("true");
        }

        if (Input.GetKeyUp(curInput))
        {
            inputTime.Add(Time.realtimeSinceStartup - keyPressTimeStart);


            //Debug.Log(inputTime[inputTime.Count-1]);
            
        }

        if (prevInput != curInput)
        {

            if (curInput != KeyCode.None)
            {
                inputsSinceSync.Add(curInput.ToString());
                keyPressTimeStart = Time.realtimeSinceStartup;

                //Debug.Log(inputsSinceSync[inputsSinceSync.Count-1]);
                
            }



        }
        Debug.Log(curInput);

        prevInput = curInput;
    }

    void OnGUI()
    {
        if (curInput != Event.current.keyCode && Event.current.keyCode != KeyCode.None)
        {
            curInput = Event.current.keyCode;
        }

    }

}
