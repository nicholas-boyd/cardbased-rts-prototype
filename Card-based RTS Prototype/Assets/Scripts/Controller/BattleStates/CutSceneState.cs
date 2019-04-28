using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CutSceneState : BattleState
{
    ConversationController conversationController;
    ConversationData data;
    protected override void Awake()
    {
        base.Awake();
        conversationController = owner.GetComponentInChildren<ConversationController>();
        data = Resources.Load<ConversationData>("Conversations/SampleScene");
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (data)
            Resources.UnloadAsset(data);
    }
    public override void Enter()
    {
        base.Enter();
        conversationController.Show(data);
    }
    protected override void AddListeners()
    {
        base.AddListeners();
        this.AddObserver(OnCompleteConversation, ConversationController.ConversationOverNotification);
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        this.RemoveObserver(OnCompleteConversation, ConversationController.ConversationOverNotification);
    }
    protected override void OnFire(object sender, object args)
    {
        base.OnFire(sender, args);
        conversationController.Next();
    }
    void OnCompleteConversation(object sender, object args)
    {
        owner.ChangeState<CombatBattleState>();
    }
}