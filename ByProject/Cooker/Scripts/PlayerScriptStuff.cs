using UnityEngine;
using UnityEngine.Events;

public class PlayerScriptStuff : MonoBehaviour
{
    Animator animator;
    public UnityEvent one,two,three,four,five;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    public void StartTurnOne()
    {
        one.Invoke();
    }
    public void StartTurnTwo()
    {
        two.Invoke();
    }
    public void StartTurnThree()
    {
        three.Invoke(); 
    }
    public void StartTurnFour()
    {
        four.Invoke();
    }
    public void Evaluation()
    {
        five.Invoke();
    }
    public void GoAnim(int i)
    {
        switch (i)
        {
            case 1:
                animator.SetTrigger("GoOne");
                break;
            case 2:
                animator.SetTrigger("GoTwo");
                break;
            case 3:
                animator.SetTrigger("GoThree");
                break;
            case 4:
                animator.SetTrigger("GoFour");
                break;
            case 5:
                animator.SetTrigger("GoFive");
                break;

        }
    }
}
