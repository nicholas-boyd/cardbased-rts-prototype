using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class ConversationPanel : MonoBehaviour
{
    public Text message;
    public Image speaker;
    public GameObject nextArrow;
    public Panel panel;
    void Start()
    {
        Vector3 pos = nextArrow.transform.localPosition;
        nextArrow.transform.localPosition = new Vector3(pos.x, pos.y + 5, pos.z);
        Tweener t = nextArrow.transform.MoveToLocal(new Vector3(pos.x, pos.y - 5, pos.z), 0.5f, EasingEquations.EaseInQuad);
        t.easingControl.loopType = EasingControl.LoopType.PingPong;
        t.easingControl.loopCount = -1;
    }
    public IEnumerator Display(SpeakerData sd)
    {
        speaker.sprite = sd.speaker;
        //speaker.SetNativeSize();
        for (int i = 0; i < sd.messages.Count; ++i)
        {
            message.text = sd.messages[i];
            nextArrow.SetActive(i != sd.messages.Count - 1);
            yield return null;
        }
    }
}