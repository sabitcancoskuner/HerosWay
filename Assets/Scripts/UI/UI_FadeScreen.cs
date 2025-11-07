using UnityEngine;

public class UI_FadeScreen : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void FadeIn()
    {
        animator.SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        animator.SetTrigger("FadeOut");
    }
}
