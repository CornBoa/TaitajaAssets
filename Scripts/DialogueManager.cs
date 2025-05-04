using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
	public static DialogueManager instance;
	public TextMeshProUGUI nameText;
	public TextMeshProUGUI dialogueText;
	public GameObject DialoguePanel;
	private Queue<string> sentences;
	public Camera MainCam;
	public UnityEvent OnDIalogueEnd;
	DialogueTrigger CUrrentDialogue;
	public static bool DialogueON;
	public static bool UnlockMovement;
	AudioSource source;
	[SerializeField]List<AudioClip> meows;
	[SerializeField] float charSpeed;
	[SerializeField] int iMod;
	void Start () {
		UnlockMovement = true;
		sentences = new Queue<string>();
        MainCam = Camera.main;
		source = GetComponent<AudioSource>();
    }
	public void StartDialogue (Dialogue dialogue,Transform LookAtMe,DialogueTrigger dialogueTrigger)
	{
		CUrrentDialogue = dialogueTrigger;
		DialogueON = true;
		DialoguePanel.SetActive(true);
		nameText.text = dialogue.name;
		MainCam.GetComponent<CameraLook>().enabled = false;	
        FindAnyObjectByType<CharacterController>().enabled = false;
        FindAnyObjectByType<PlayerMovement>().enabled = false;
        if (LookAtMe == null)MainCam.transform.LookAt(LookAtMe);
        Cursor.lockState = CursorLockMode.Confined;
		sentences.Clear();
		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}
		DisplayNextSentence();
	}
    private void Update()
    {
       
    }
    public void DisplayNextSentence ()
	{
		Debug.Log("NextDialogue");
		if (sentences.Count == 0)
		{
			EndDialogue(CUrrentDialogue);
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		int i = 0;
		foreach (char letter in sentence.ToCharArray())
		{
			i++;
			if (i == iMod)
			{
                source.clip = meows[Random.Range(0, meows.Count)];
				i = 0;
                source.Play();
            }				
            
			dialogueText.text += letter;
			yield return new WaitForSeconds(charSpeed);
		}
	}

	void EndDialogue(DialogueTrigger dialogueTrigger)
	{
		if (UnlockMovement)
		{
            FindAnyObjectByType<PlayerMovement>().enabled = true;
			FindAnyObjectByType<CharacterController>().enabled = true;
        }  
        DialogueON = false;
		DialoguePanel.SetActive (false);
        Cursor.lockState = CursorLockMode.Locked;
        MainCam.GetComponent<CameraLook>().enabled = true;
		dialogueTrigger.OnEnd.Invoke();
    }

}
