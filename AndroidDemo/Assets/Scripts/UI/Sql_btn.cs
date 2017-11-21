using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sql_btn : UIBase {

	
	void Start () {

        AddComponentToChild();
        AddButtonLisenter("Button2", Sql); 
	}
	
	// Update is called once per frame
	void Sql () {

        Debug.Log("数据库");
	}
}
