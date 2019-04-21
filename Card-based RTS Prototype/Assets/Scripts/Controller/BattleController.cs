using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BattleController : StateMachine {
	public CameraDrone cameraDrone;
	public Board board;
	public LevelData levelData;
	public Point pos;
	//public Unit playerUnit;
	public Tile currentTile { get { return board.GetTile(pos); }}
    //public AbilityHandController abilityHandController;
    //public List<Unit> enemyUnits = new List<Unit>();
    //public StatPanelController statPanelController;
    //public InventoryPanelController inventoryPanelController;
    //public BattleMessageController battleMessageController;
    //public BattleClockController battleClockController;

        //TEMP
    private void Start()
    {
        Activate();
    }

    public void Activate () {
        Debug.Log("Activated");
        //playerUnit = GetComponentInParent<GameController>().playerController.player;
		ChangeState<InitBattleState>();
	}

    void OnEnable()
    {
        //this.AddObserver(EnemyDeath, Unit.EnemyDiedNotification);
    }

    void OnDisable()
    {
        //this.RemoveObserver(EnemyDeath, Unit.EnemyDiedNotification);
    }

    void EnemyDeath(object sender, object enemy)
    {
        //if (enemyUnits.Contains(enemy as Unit))
            //enemyUnits.Remove(enemy as Unit);
    }
}