using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Unit : MonoBehaviour
{
    //public UnitRecipe recipe;
    public Tile tile { get; protected set; }
    //public AbilityDeck deck;
    //public AbilityDeck discards;
    //public List<Card> hand;
    public int handSize = 4;
    public Directions dir;
    public bool acting;
    public bool beingHealed;
    public bool dying;
    public int DeckCount;
    public int flashCount;
    public ParticleSystem death;
    //EasingControl ec;
    //Inventory inventory;

    public const string EnemyDiedNotification = "ENEMY_DIED_NOTIFICATION";

    void Start()
    {
        acting = false;
        dying = false;
        flashCount = 4;
        Transform inv = this.transform.Find("Inventory");
        if (inv == null)
        {
            GameObject i = new GameObject("Inventory");
            i.transform.parent = this.transform;
            inv = i.transform;
        }
        //inventory = inv.gameObject.AddComponent<Inventory>();
    }

    void OnEnable()
    {
        //this.AddObserver(OnHit, DamageAbilityEffect.DamageDealtNotification);
        //this.AddObserver(OnHeal, HealAbilityEffect.UnitHealedNotification);
        //this.AddObserver(OnDeath, Stats.DeathNotification, GetComponent<Stats>());
    }

    void OnDisable()
    {
        //this.RemoveObserver(OnHit, DamageAbilityEffect.DamageDealtNotification);
        //this.RemoveObserver(OnHeal, HealAbilityEffect.UnitHealedNotification);
        //this.RemoveObserver(OnDeath, Stats.DeathNotification, GetComponent<Stats>());
    }

    void OnHit(object sender, object target)
    {
        //Info<Unit, Unit, int> info = target as Info<Unit, Unit, int>;
        //if (info.arg1.Equals(this))
          //  StartCoroutine(Flash(Color.red));
    }

    void OnHeal(object sender, object target)
    {
        //Info<Unit, Unit, int> info = target as Info<Unit, Unit, int>;
        //if (info.arg1.Equals(this))
          //  StartCoroutine(Flash(Color.green));
    }

    void OnDeath(object sender, object target)
    {
        /*if (this != null)
        {
            if (GetComponent<Stats>()[StatTypes.HP] != 0)
            {
                Debug.Log("False death report");
            }
            else
            {
                StartCoroutine(Flash(Color.clear));
                StartCoroutine(Die());
            }
        }*/
    }

    /*IEnumerator Die()
    {
        if (GetComponent<Alliance>().type == Alliances.Enemy)
            this.PostNotification(EnemyDiedNotification, this);
        ParticleSystem dying = Instantiate(death);
        dying.transform.position = tile.center;
        ec = gameObject.AddComponent<EasingControl>();
        ec.duration = 0.2f;
        ec.equation = EasingEquations.EaseInOutQuad;
        ec.endBehaviour = EasingControl.EndBehaviour.Constant;
        ec.updateEvent += OnUpdateEvent;
        while (dying.isPlaying || ec.IsPlaying)
            yield return null;
        Destroy(this.gameObject);
        this.tile.content = null;
    }*/

    void OnUpdateEvent(object sender, EventArgs e)
    {
        //GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, ec.currentValue);
    }

    IEnumerator Flash(Color color)
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        for (int i = 0; i < flashCount; i++)
        {
            sprite.color = color;
            yield return new WaitForSeconds(0.06f);
            sprite.color = Color.white;
            yield return new WaitForSeconds(0.06f);
        }
    }

    void Update()
    {
        //DeckCount = deck.Count;
    }

    /*public PlayerData GetPlayerData()
    {
        PlayerData ret = new PlayerData();
        ret.recipe = this.recipe.name;

        Card[] copy = deck.deck.ToArray();
        string[] cards = new string[copy.Length];
        for (int i = 0; i < copy.Length; i++)
        {
            Card c = copy[i];
            if (c != null)
            {
                cards[i] = c.GetName();
                Debug.Log(cards[i]);
            }
        }
        ret.deck = cards;
        ret.stats = this.GetComponent<Stats>().GetStatsData();
        //ret.inventory = GetComponentInParent<GameController>()..GetData();

        return ret;
    }*/

    public void Place(Tile target)
    {
        // Make sure old tile location is not still pointing to this unit
        if (tile != null && tile.content == gameObject)
            tile.content = null;

        // Link unit and tile references
        tile = target;

        if (target != null)
            target.content = gameObject;
    }

    public void Match()
    {
        transform.localPosition = new Vector3(tile.center.x, tile.center.y + GetComponent<SpriteRenderer>().bounds.size.y / 2, tile.center.z);
        transform.localEulerAngles = dir.ToEuler();
    }
}

[Serializable]
public class PlayerData
{
    public string recipe;
    public string[] deck;
    //public StatsData stats;
    //public InventoryData inventory;
}