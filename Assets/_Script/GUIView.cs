using UnityEngine;
using System.Collections;
/// <summary>
/// 脚本功能：(V) GUI显示游戏
/// 版权声明：Copyright (c) 2015 www.lanou3g.com All Rights Reserved
/// 添加对象：Main Camera
/// 创建时间：2015年7月6日
/// </summary>
public class GUIView : MonoBehaviour {
	public int oX = 100;
	public int oY = 300;
	public int size = 80;

	void OnGUI () {
		if (GUILayout.Button ("Restart")) {
			GameController.InitGame();
		}

		for (int i = 0; i < Card.n; i++) {
			for (int j = 0; j < Card.n; j++) {
				// 计算显示区域
				Rect rect = new Rect(oX + j * size, oY - i * size, size, size);
				// 显示内容
				string str = Card.cards[i, j].number + "\n" + Card.cards[i, j].pos;
				GUI.Box (rect, str);
			}
		}
	}
}
