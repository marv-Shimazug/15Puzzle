using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameRule : MonoBehaviour {

	// パネルの数.
	const int ALL_PANELS = 16;

	// パネル座標の基準.
	readonly Vector2 BASE_POS = new Vector2 (-150.0f, 80.0f);

	// シャッフルの回数.
	const int SHUFFLE_NUM = 1000;

	// UIパネルのプレハブ.
	[SerializeField]GameObject PanelPrefab;
	// UI背景パネルのプレハブ.
	[SerializeField]GameObject BackPanelPrefab;

	// UIカンバス.
	[SerializeField]GameObject Canvas;

	// UIパネルオブジェクトの格納.
	GameObject[] PanelObjects = new GameObject[ALL_PANELS]; 
	GameObject[] BackObjects = new GameObject[ALL_PANELS];

	// パネル用意.
	int[] Panel= new int[ALL_PANELS];

	// 選択中のパネル番号.
	public int SelectedPanel;

	// マウスカーソルの下にあるオブジェクト.
	GameObject UnderMouseObj;

	void Awake()
	{
		Screen.SetResolution(1024, 768, false);
	}

	// 初期化.
	void Start () 
	{
		SelectedPanel = -1;

		// 描画順の関係で、同じfor文で回せない.
		for (int i = 0; i < ALL_PANELS; i++) 
		{
			// ベースとなる.
			UICreatePanelObjects( i, BackPanelPrefab, BackObjects, true );
		}
		for (int i = 0; i < ALL_PANELS - 1; i++) 
		{

			// 移動のための.
			UICreatePanelObjects( i, PanelPrefab, PanelObjects, false );
		}
	}
	
	// 更新.
	void Update () 
	{
		int i, x, y = 0;
		x = 0;

		/*
		if (Input.GetMouseButtonDown (0)) 
		{
			Debug.Log("選択中パネル：" + SelectedPanel);
			x = (int)PanelObjects[SelectedPanel].GetComponent<RectTransform>().position.x;
			y = (int)PanelObjects[SelectedPanel].GetComponent<RectTransform>().position.y;
		}
		*/


		int pn = y * 4 + x;
		if (x > 0 && 15 == Panel [pn - 1]) Swap(pn - 1, pn);
		if (x < 3 && 15 == Panel [pn + 1]) Swap (pn, pn + 1);
		if (y > 0 && 15 == Panel [pn - 4]) Swap (pn - 4, pn);
		if (y < 3 && 15 == Panel [pn + 4]) Swap (pn, pn + 4);

		// ゲーム終了判定.
		for (i = 0; i < ALL_PANELS; i++) 
		{
			if(Panel[i] != i)break;
			if(ALL_PANELS == i)
			{
				// クリア.
				Debug.Log("クリア！");
			}
		}

	}

	// ２つのパネルの位置を入れ替える.
	void Swap(int i, int j)
	{
		int tmp = Panel [i];
		Panel [i] = Panel [j];
		Panel [j] = tmp;
	}

	// パズル生成.
	void SetPuzzle()
	{
		for (int i = 0; i < ALL_PANELS; i++) 
		{
			Panel[i] = i;
		}
	}

	// シャッフル.
	void PanelShuffle()
	{
		int i, pn = 0;
		for(i = 0; i < SHUFFLE_NUM; i++)
		{
			for(int j = 0; j < ALL_PANELS; j++)
			{
				// 空きパネルをpnに格納.
				if(ALL_PANELS - 1 == Panel[j])
				{
					pn = j;
				}
				// 空きパネル座標.
				int x = pn % 4;
				int y = pn / 4;
				// 空きパネルと入れ替える方向をランダムで決める.
				int dir = Random.Range(1, 9999) % 4;
				switch(dir)
				{
				case 0: if(x > 0) Swap(pn - 1, pn); break;
				case 1: if(x < 3) Swap(pn, pn + 1); break;
				case 2: if(y > 0) Swap(pn, pn + 4); break;
				case 3: if(y < 3) Swap(pn, pn + 4); break;
				}
			}
		}
	}

	// パネルUI生成.
	void UICreatePanelObjects(int panel, GameObject prefab, GameObject[]  objects, bool back)
	{
		// インスタンス作成.
		objects[panel] = Instantiate (prefab) as GameObject;

		// 番号表示.
		if (false == back) 
		{
			PanelObjects [panel].GetComponentInChildren<Text> ().text = panel.ToString ();
		}

		// 親オブジェクト設定.
		objects [panel].transform.parent = Canvas.transform;

		// 座標設定.
		Vector3 Pos = Vector3.zero;
		Pos.x = (float)(panel % 4);
		Pos.y = (float)(panel / 4);
		if (true == back) 
		{
			Pos.z = -1.0f;
		} 
		else 
		{
			Pos.z = 0.0f;
		}
		objects[panel].GetComponent<RectTransform>().localPosition = new Vector3(Pos.x * 50.0f + BASE_POS.x, BASE_POS.y - (Pos.y * 50.0f), Pos.z);
	}

	// 空パネル生成.
	void EmptyPanel(int panel, GameObject prefab)
	{

	}

}
