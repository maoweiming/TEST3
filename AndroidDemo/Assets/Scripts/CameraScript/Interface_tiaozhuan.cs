using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WearHFPlugin;

public class Interface_tiaozhuan : MonoBehaviour {

    Button nameText;
    Button deterMineButton;
	void Start () {
        nameText = transform.GetComponentInChildren<Button>();
        Debug.Log(nameText.name);
        Debug.Log("Nihao");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
