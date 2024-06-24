using UnityEngine;

public class LTMove : MonoBehaviour
{
    [SerializeField] LeanTweenType ltType;

    [SerializeField] float moveDistanceY;
    [SerializeField] float moveDurationY;
    [SerializeField] float moveDistanceZ;
    [SerializeField] float moveDurationZ;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lean"))
        {
            MovePosY();
        }
    }

    void MovePosY()
    {
        LeanTween.moveLocalY(gameObject.transform.parent.gameObject, gameObject.transform.parent.localPosition.y + moveDistanceY, moveDurationY)
            .setEaseInBack()
            .setOnComplete(() => MovePosZ(gameObject.transform.parent.localPosition.y + moveDistanceY));
    }

    void MovePosZ(float finalPosY)
    {
        LeanTween.moveLocal(gameObject.transform.parent.gameObject, new Vector3(gameObject.transform.parent.localPosition.x, finalPosY, gameObject.transform.parent.localPosition.z + moveDistanceZ), moveDurationZ);
    }
}
