using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 脚本功能：(C) 游戏主逻辑及输入控制
/// 版权声明：Copyright (c) 2015 www.lanou3g.com All Rights Reserved
/// 添加对象：Main Camera
/// 创建时间：2015年7月6日
/// </summary>
public class GameController : MonoBehaviour {
	public static bool isGameOver = false;
	private bool isLock = false;  // 对输入进行加锁
	private string debugStr = ""; // 调试专用字符串
	
	void Start () {
		InitGame();
	}
	
	void Update () {
		// 获取输入的方向
		int h = (int)Input.GetAxis("Horizontal");
		int v = h == 0 ? (int)Input.GetAxis("Vertical") : 0;

		if (!isLock) {  // 如果没加锁
			isLock = true; // 加锁
			if (h != 0 || v != 0) { // 如果有方向
				if( MoveCard(new Vector2(h, v)) > 0) { // 先移动
					CreateCard();                // 再创建
				}
			}
		}

		if (h == 0 && v == 0) { // 如果没有输入，解锁
			isLock = false;
		}
		debugStr = "\n\n\nh:" + h + " v:" + v + "\n" + isLock; 
	}

	// 初始化游戏
	public static void InitGame () {
		for (int i = 0; i < Card.n; i++) {
			for (int j = 0; j < Card.n; j++) {
				Card.cards[i, j] = new Card(0, new Vector2(i, j));
			}
		}
		CreateCard();
		CreateCard();
	}

	// 创建号码牌
	static void CreateCard () {
		List<Vector2> list = new List<Vector2>(); 	// 保存所有空格子位置
		// 遍历所有格子
		for (int i = 0; i < Card.n; i++) {
			for (int j = 0; j < Card.n; j++) {
				// 如果格子为空
				if(Card.cards[i,j].number == 0)
				{
					list.Add(Card.cards[i,j].pos); // 在list中添加该格子位置
				}
			}
		}

		if (list.Count == 0) {
			isGameOver = true;
			return;
		}

		int k = Random.Range(0,list.Count); // 随机一个位置下标
		int x = (int)list[k].x;   // 随机位置的x坐标
		int y = (int)list[k].y;   // 随机位置的y坐标
		int number = Random.Range(0, 10) < 3 ? 4 : 2; // 4: 30%   2: 70%
		Card.cards[x, y] = new Card(number ,list[k]);  // 在随机位置上生成新号码牌

	}

	// 移动号码牌
	int MoveCard(Vector2 dir) {
		int counter = 0;

		string strDir = dir.x + "" + dir.y;
		// 遍历所有可能需要移动的号码牌
		for (int i = 0; i < Card.n; i++) {
			for (int j = 0; j < Card.n - 1; j++) {
				int x1, y1; // 当前卡牌下标
				int x2, y2; // 目标卡牌下标

				switch (strDir) {  // 分方向确认 x1, y1
				case "10":  // -->  Right
					x1 = i;              y1 = Card.n - 2 - j;
					break;
				case "-10": // <--  Left
					x1 = i;              y1 = j + 1;
					break; 
				case "01":  // ^    Up
					x1 = Card.n - 2 - j; y1 = i;
					break;
				case "0-1": // V    Down
					x1 = j + 1;          y1 = i;
					break;
				default:
					x1 = 0;              y1 = 0;
					break;
				}
				// 如果没有卡片，推出当次循环，判断下一个
				if (Card.cards[x1, y1].number == 0) {
					continue;
				}
				// 根据当前坐标和移动方向获取目标坐标
				x2 = x1 + (int)dir.y;
				y2 = y1 + (int)dir.x;

				// 单个卡片循环移动
				while (inBound(x2, y2)) {
					// 停止
					if (Card.cards[x2, y2].number != 0 &&
					    Card.cards[x1, y1].number != Card.cards[x2, y2].number ||
					    Card.cards[x2, y2].merged) {
						break; // 卡片停止移动
					}
					// 合并
					if (Card.cards[x2, y2].number != 0 &&
					    Card.cards[x1, y1].number == Card.cards[x2, y2].number) {
						Card.cards[x2, y2].number *= 2;
						Card.cards[x1, y1].number = 0;
						Card.cards[x2, y2].merged = true; // 合并!
						break; // 卡片停止移动
					}
					// 移动
					if (Card.cards[x2, y2].number == 0) {
						counter++;
						Card.cards[x2, y2].number = Card.cards[x1, y1].number;
						Card.cards[x1, y1].number = 0;
						x1 += (int)dir.y;
						y1 += (int)dir.x;
						x2 = x1 + (int)dir.y;
						y2 = y1 + (int)dir.x;
					} // if
				} // while
			} // for-j
		} // for-i

		// 遍历卡片数组，消除合并标记
		foreach (Card card in Card.cards) {
			card.merged = false;
		}
		return counter;
	}

	bool inBound(int x, int y) {
		if (x >= 0 && x < Card.n && y >= 0 && y < Card.n) {
			return true;
		}
		return false;
	}

	void OnGUI () {
		GUILayout.Label(debugStr);
	}
}