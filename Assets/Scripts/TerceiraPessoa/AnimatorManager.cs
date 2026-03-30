using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator;
    int horizontal;
    int vertical;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }

    public void PlayTargetAnimation(string targetAnimation, bool isInteracting)
    {
        animator.SetBool("isInteracting", isInteracting);
        animator.CrossFade(targetAnimation, 0.2f);
    }

    public void UpdateAnimatorValues(float horizontalMove, float verticalMove)
    {
        // Snap de animação
        float snappedHorizontal = 0;
        float snappedVertical = 0;

        if (horizontalMove > 0f && horizontalMove < 0.55f)
        {
            snappedHorizontal = 0.5f;
        }
        else if (horizontalMove > 0.55f)
        {
            snappedHorizontal = 1f;
        }
        else if (horizontalMove < 0f && horizontalMove > -0.55f)
        {
            snappedHorizontal = -0.5f;
        }
        else if (horizontalMove < -0.55f)
        {
            snappedHorizontal = -1f;
        }
        if (verticalMove > 0f && verticalMove < 0.55f)
        {
            snappedVertical = 0.5f;
        }
        else if (verticalMove > 0.55f)
        {
            snappedVertical = 1f;
        }
        else if (verticalMove < 0f && verticalMove > -0.55f)
        {
            snappedVertical = -0.5f;
        }
        else if (verticalMove < -0.55f)
        {
            snappedVertical = -1f;
        }

        animator.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
    }
}
