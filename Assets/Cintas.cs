using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cintas : MonoBehaviour
{
    public float speed = 100f;
    public Vector3 Dir = new Vector3(1, 0, 0);
    public List<GameObject> OnBelts;
    public bool IsMoving = false;
    void Update()
    {
        if (IsMoving) Moving();

        if (IsMoving == false)
        {
            Moving();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Lever"))
        {
            Dir = new Vector3(0, 0, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnBelts.Add(collision.gameObject);
        if (collision.gameObject.CompareTag("PlayerBody"))
            NewPlayerMovement.Instance.onBelts = true;
    }


    private void OnCollisionExit(Collision collision)
    {
        OnBelts.Remove(collision.gameObject);
        if (collision.gameObject.CompareTag("PlayerBody"))
            NewPlayerMovement.Instance.onBelts = false;
    }


    public void Moving()
    {
        for (int i = 0; i <= OnBelts.Count - 1; i++)
        {
            OnBelts[i].GetComponent<Rigidbody>().velocity = speed * Dir * Time.deltaTime;
        }
    }

    public void NegativeMove()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            IsMoving = !IsMoving;
        }
    }
}
