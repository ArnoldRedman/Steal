using UnityEngine;

public class DialogueItem : MonoBehaviour
{
    [Header("对话id")] 
    public string id;
    [HideInInspector]
    public DialogueItemData dialogueItemData;//详细的对话内容

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        dialogueItemData = GameManager.instance.dialogueItemDict[id];
    }
}
