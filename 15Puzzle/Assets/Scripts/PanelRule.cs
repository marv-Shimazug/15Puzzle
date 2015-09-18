using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PanelRule : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	private Vector3 KeepPos;

	// 現在あたっている.
	private Collider2D nowHit;

	// 相手をはじく.
	public bool Refrect;

	// 選択したパネルの番号取得.
	private int SelectPanelNum;
	public int GetPanelNum{get{return SelectPanelNum;}}

	const string NONE_PANEL = "空";

	// 押されているのが自分かどうか.
	private bool OnClickFlag;

	// データ送信用.
	private GameRule GameRuleObject;

	void Start()
	{
		GameRuleObject = GameObject.Find ("GameRule").GetComponent<GameRule>();
	}

	// 更新.
	void Update()
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			// 選択したパネル番号取得.
			SelectPanelNum = int.Parse(this.GetComponentInChildren<Text>().text);
			Debug.Log(SelectPanelNum);
		}
	}

	public void OnBeginDrag(PointerEventData ped)
	{
		// 最前面に描画.
		this.transform.SetAsLastSibling ();
		// 選択したパネル番号取得.
		SelectPanelNum = int.Parse(this.GetComponentInChildren<Text>().text);
		// ゲームルールにパネル番号送信.
		GameRuleObject.SelectedPanel = SelectPanelNum;
		// 元の座標をキープ.
		KeepPos = this.GetComponent<RectTransform> ().transform.position;
	}

	public void OnDrag(PointerEventData ped) 
	{
		// 座標移動.
		this.GetComponent<RectTransform> ().transform.position = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0.0f);
	}

	public void OnEndDrag(PointerEventData ped) 
	{
		// ゲームルールのパネル番号初期化.
		GameRuleObject.SelectedPanel = -1;
		// 移動できる場所でないなら移動させない.
		this.GetComponent<RectTransform> ().transform.position = KeepPos;
	}


	void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log ("当たった：" + col.GetComponentInChildren<Text>().text);
		if (NONE_PANEL == col.GetComponentInChildren<Text> ().text)
		{
			KeepPos = col.GetComponent<RectTransform>().position;
		}
	}

	void OnCollisionExit2D()
	{
		Debug.Log ("離れた!!");
		this.GetComponent<BoxCollider2D> ().enabled = true;
	}

}
