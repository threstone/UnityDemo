using UnityEngine;

public class SkillDisplayController : EntityController
{

    private Animator animator;
    private bool animationPlayed = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (!animationPlayed && animator.GetCurrentAnimatorStateInfo(0).IsName("Play"))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                gameObject.SetActive(false);
                animationPlayed = true;
            }
        }
    }

    public override void Destroy()
    {
        Destroy(gameObject);
    }
}
