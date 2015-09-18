using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameRule : MonoBehaviour {

	// パネルの数.
	const int ALL_PANELS = 16;

	// パネルのプレハブ.
	[SerializeField]GameObject PanelPrefab;

	// カンバス.
	[SerializeField]GameObject Canvas;

	// パネルオブジェクトの格納.
	GameObject[] PanelObjects = new GameObject[ALL_PANELS]; 

	// パネル用意.
	int[] Panel= new int[ALL_PANELS];

	// Use this for initialization
	void Start () 
	{
		for (int i = 0; i < ALL_PANELS; i++) 
		{
			CreatePanelObjects( i );
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// ２つのパネルの位置を入れ替える.
	void Swap(int i, int j)
	{
		int tmp = Panel [i];
		Panel [i] = Panel [j];
		Panel [j] = tmp;
	}

	// パネル生成.
	void CreatePanelObjects(int panel)
	{
		PanelObjects[panel] = Instantiate (PanelPrefab, new Vector3((float)panel * 30.0f, 50.0f, 0.0f), Quaternion.identity) as GameObject;
		PanelObjects[panel].GetComponentInChildren<Text>().text = panel.ToString();
		PanelObjects [panel].transform.parent = Canvas.transform;
	}

}
