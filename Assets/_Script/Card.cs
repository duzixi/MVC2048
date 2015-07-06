using UnityEngine;
using System.Collections;
/// <summary>
/// 脚本功能：(M) 卡牌属性及卡牌数组
/// 添加对象：无
///	创建时间：2015年7月6日
///	知识要点：
/// 1. static
/// </summary>
public class Card {
	public static int n = 4;
	// 保存所有卡牌
	public static Card[,] cards = new Card[n, n]; 

	public int number;
	public bool merged; // 是否合并过
	public Vector2 pos; // 当前位置

	// 构造器
	public Card(int number, Vector2 pos) {
		this.number = number;
		this.pos = pos;
		this.merged = false;
	}
}
