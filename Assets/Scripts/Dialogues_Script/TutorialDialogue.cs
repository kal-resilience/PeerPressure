using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VIDE_Data;
public class TutorialDialogue : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private VIDE_Assign dialogueNameToLoad;
    private Text textComponent;

    void Start()
    {
        textComponent = dialogueBox.GetComponentInChildren<Text>();

        VD.OnNodeChange += OnNodeChangeHandler;
        VD.OnEnd += OnEndHandler;

        if (dialogueNameToLoad != null)
        {
            VD.BeginDialogue(dialogueNameToLoad);
        }
    }

    void OnDestroy()
    {
        // Unsubscribe (important!)
        VD.OnNodeChange -= OnNodeChangeHandler;
        VD.OnEnd -= OnEndHandler;
    }

    void OnNodeChangeHandler(VD.NodeData data)
    {
        textComponent.text = data.comments[data.commentIndex];
    }

    void OnEndHandler(VD.NodeData data)
    {
        dialogueBox.SetActive(false);
        VD.EndDialogue();
    }

    // Advance dialogue when clicking
    public void OnPointerClick(PointerEventData eventData)
    {
        VD.Next(); // Go to the next node
    }
}
