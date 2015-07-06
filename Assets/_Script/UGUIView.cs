using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 脚本功能：(V) UGUI显示游戏
/// 版权声明：Copyright (c) 2015 www.lanou3g.com All Rights Reserved
/// 添加对象：Main Camera
/// 创建时间：2015年7月6日
/// </summary>
public class UGUIView : MonoBehaviour {
	public GameObject cardPref;

	private Dictionary<int, Color> colors = new Dictionary<int, Color>();

	private int size = 100;
	private int oX = -200 + 50;
	private int oY = -200 + 50;
	
	void Awake () {
		colors.Add( 0, new Color( 205f / 255f, 193f / 255f, 181f / 255f ));
		colors.Add( 2, new Color( 225f / 255f, 214f / 255f, 204f / 255f ));
		colors.Add( 4, new Color( 229f / 255f, 216f / 255f, 196f / 255f ));
		colors.Add( 8, new Color( 233f / 255f, 178f / 255f, 134f / 255f ));
		colors.Add(16, new Color( 229f / 255f, 160f / 255f, 122f / 255f ));
		colors.Add(32, new Color( 237f / 255f, 138f / 255f, 116f / 255f ));
		colors.Add(64, new Color( 238f / 255f, 103f / 255f,  77f / 255f ));
		colors.Add(128, new Color( 233f / 255f, 204f / 255f,  124f / 255f ));
		colors.Add(512, Color.yellow);
		colors.Add(1024, Color.red);
		colors.Add (2048, Color.black);
	}

	// Use this for initialization
	void Start () {
		for (int i = 0; i < Card.n; i++) {
			for (int j = 0; j < Card.n; j++) {
				GameObject c = Instantiate(cardPref) as GameObject;
				c.transform.SetParent(transform);
				c.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(oX + j * size,oY + i * size, 0);
				c.transform.localScale = new Vector3(1, 1, 1);

				string showNum = Card.cards[i, j].number == 0 ? "" : "" + Card.cards[i, j].number;
				c.transform.GetChild(0).GetComponent<Text>().text = showNum;
				c.GetComponent<Image>().color = colors[Card.cards[i, j].number];
			}
		}
	}

	void ShowCards() {
		for (int i = 0; i < Card.n; i++) {
			for (int j = 0; j < Card.n; j++) {
				Transform c = transform.GetChild(i * Card.n + j);
				string showNum = Card.cards[i, j].number == 0 ? "" : "" + Card.cards[i, j].number;
				c.GetChild(0).GetComponent<Text>().text = showNum;
				c.GetComponent<Image>().color = colors[Card.cards[i, j].number];
			}
		}
	}

	// Update is called once per frame
	void Update () {
		ShowCards();
	}
}
