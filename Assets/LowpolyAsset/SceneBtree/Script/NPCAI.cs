using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NPCAI : MonoBehaviour
{
    public enum State { Idle, Wander, Interact }
    public State currentState;

    private NavMeshAgent agent;
    public Transform[] wanderPoints;
    private int currentIndex;

    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogueLines;
    private int dialogueIndex;
    private bool isTalking = false;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = State.Wander;
        NextWander();
    }

    void Update()
    {
        if (currentState == State.Wander && agent.remainingDistance < 0.5f)
            NextWander();
        if (currentState == State.Interact && isTalking && Input.GetKeyDown(KeyCode.Space))
        {
            ShowDialogue();
        }
    }

    void NextWander()
    {
        currentIndex = Random.Range(0, wanderPoints.Length);
        agent.destination = wanderPoints[currentIndex].position;
    }

    public void Interact()
    {
        currentState = State.Interact;
        agent.ResetPath();
        dialoguePanel.SetActive(true);
        dialogueIndex = 0;
        isTalking = true;
        ShowDialogue();
    }

    public void ShowDialogue()
    {
        if (dialogueIndex < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[dialogueIndex];
            dialogueIndex++;
        }
        else
        {
            dialoguePanel.SetActive(false);
            currentState = State.Wander;
            isTalking = false;
            NextWander();
        }
    }
}
