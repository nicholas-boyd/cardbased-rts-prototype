﻿using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Board : MonoBehaviour 
{
	[SerializeField] List<GameObject> tilePrefabs;
    [SerializeField] public Dictionary<Point, Tile> tiles = new Dictionary<Point, Tile>();

	[HideInInspector] public int width, depth;
	public Point min { get { return _min; }}
	public Point max { get { return _max; }}
	Point _min;
	Point _max;

	public void Load (LevelData data) {
        Debug.Log("Loading");
		_min = new Point(int.MaxValue, int.MaxValue);
		_max = new Point(int.MinValue, int.MinValue);
		for (int i = 0; i < data.tiles.Count; ++i) {
			if (i == 0) {
				width = data.tiles [i].x;
				depth = data.tiles [i].y;
				continue;
			}
			GameObject instance = data.tiles[i].x < width / 2 ? Instantiate(tilePrefabs[0]) as GameObject : Instantiate(tilePrefabs[1]) as GameObject;
            instance.name = "Tile " + i;
            instance.transform.SetParent(this.transform);
			Tile t = instance.GetComponent<Tile>();
			t.Load(data.tiles[i]);
			tiles.Add(t.pos, t);
			_min.x = Mathf.Min(_min.x, t.pos.x);
			_min.y = Mathf.Min(_min.y, t.pos.y);
			_max.x = Mathf.Max(_max.x, t.pos.x);
			_max.y = Mathf.Max(_max.y, t.pos.y);
		}
	}

	public Tile GetTile (Point p) {
		return tiles.ContainsKey(p) ? tiles[p] : null;
	}

	public List<Tile> Search (Tile start, Func<Tile, Tile, bool> addTile) {
		List<Tile> retValue = new List<Tile>();
		retValue.Add(start);
		ClearSearch();
		Queue<Tile> checkNext = new Queue<Tile>();
		Queue<Tile> checkNow = new Queue<Tile>();
		start.distance = 0;
		checkNow.Enqueue(start);
		Point[] dirs = new Point[4] {
			new Point(0, 1),
			new Point(0, -1),
			new Point(1, 0),
			new Point(-1, 0)
		};
		while (checkNow.Count > 0) {
			Tile t = checkNow.Dequeue();
			for (int i = 0; i < 4; ++i) {
				Tile next = GetTile(t.pos + dirs[i]);
				if (next == null || next.distance <= t.distance + 1)
					continue;
				if (addTile(t, next)) {
					next.distance = t.distance + 1;
					next.prev = t;
					checkNext.Enqueue(next);
					retValue.Add(next);
				}
			}
			if (checkNow.Count == 0)
				SwapReference(ref checkNow, ref checkNext);
		}

		return retValue;
	}

	void ClearSearch () {
		foreach (Tile t in tiles.Values) {
			t.prev = null;
			t.distance = int.MaxValue;
		}
	}

	void SwapReference (ref Queue<Tile> a, ref Queue<Tile> b) {
		Queue<Tile> temp = a;
		a = b;
		b = temp;
	}
}