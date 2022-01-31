using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    public void Fail()
    {
        anim.SetTrigger("Hook");
    }
}
