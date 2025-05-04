using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;
	public Transform Lookat;
	public UnityEvent OnEnd;
	public bool doOnStart = false;
    private void Start()
    {
        if(doOnStart) 
        {
            StartCoroutine(startdi());
        }
    }
    public void TriggerDialogue ()
	{
        if(FindAnyObjectByType<DialogueManager>() != null) FindAnyObjectByType<DialogueManager>().StartDialogue(dialogue, Lookat,this);
    }
    IEnumerator startdi()
    {
        yield return null;
        TriggerDialogue();
    }
}
