using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InitBattleState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
        Debug.Log("Init");
        board.Load(levelData);
        Point p = new Point((int)levelData.tiles[1].x, (int)levelData.tiles[1].y);
        SelectTile(p);
        SpawnTestUnits();
        //owner.battleClockController.Activate();
        yield return null;
        owner.ChangeState<CutSceneState>();
    }
    void SpawnTestUnits()
    {
        string[] enemies = new string[]
        {
            "TestDummyGod",
            "TestDummyWeak",
            "TestDummyEqual"
        };
        List<Tile> locations = new List<Tile>(board.tiles.Values);
        for (int i = 0; i < locations.Count; ++i)
        {
            if (locations[i].pos.x != 5 || locations[i] == null)
            {
                locations.Remove(locations[i]);
                if (i >= 0)
                    i--;
            }
        }
        /*for (int i = 0; i < enemies.Length; ++i)
        {
            GameObject instance = UnitFactory.Create(enemies[i]);
            instance.transform.SetParent(owner.transform.Find("NPCs"));
            int random = UnityEngine.Random.Range(0, locations.Count);
            Tile randomTile = locations[random];
            locations.RemoveAt(random);
            Unit unit = instance.GetComponent<Unit>();
            unit.Place(randomTile);
            unit.dir = Directions.North;
            unit.Match();
            enemyUnits.Add(unit);
        }


        owner.playerUnit = owner.GetComponentInParent<GameController>().GetComponentInChildren<PlayerController>().player;*/
        owner.playerUnit.Place(board.GetTile(new Point(0, 1)));
        owner.playerUnit.Match();
        SelectTile(owner.playerUnit.tile.pos);
    }
}