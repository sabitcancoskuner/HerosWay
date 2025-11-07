using UnityEngine;

public class UI_LevelUpText : MonoBehaviour
{
    [SerializeField] private float yOffset;
    [SerializeField] private float duration;

    private float counter;

    private void Start() {
        counter = duration;
    }

    private void Update() {
        transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + yOffset * Time.deltaTime);
        counter -= Time.deltaTime;
        if (counter < 0)
        {
            DestroyText();
        }
    }

    private void DestroyText()
    {
        Destroy(gameObject);
    }
}
