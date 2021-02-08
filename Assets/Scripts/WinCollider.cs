using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCollider : MonoBehaviour
{
    [SerializeField] private GameObject youWinText;
    [SerializeField] private GameObject milk;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            youWinText.SetActive(true);
            milk.GetComponent<PlayerController>().canMove = false;
            milk.GetComponent<Animator>().SetBool("won", true);
        }
    }
}
