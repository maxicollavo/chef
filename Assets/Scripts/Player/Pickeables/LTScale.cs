using UnityEngine;

public class LTScale : MonoBehaviour
{
    private Vector3 normalScale;
    private Vector3 newScale;

    void Start()
    {
        normalScale = transform.localScale;
        newScale = transform.localScale / 2;

        ScaleAnim();
    }

    void ScaleAnim()
    {
        LeanTween.scale(gameObject, newScale, 2)
            .setOnComplete(ScaleBack);
    }

    void ScaleBack()
    {
        LeanTween.scale(gameObject, normalScale, 2)
            .setOnComplete(ScaleAnim);
    }
}
