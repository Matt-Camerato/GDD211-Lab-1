using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject youDiedText;
    [SerializeField] private GameObject milk;

    public void Respawn()
    {
        youDiedText.SetActive(false);
        milk.GetComponent<MilkAbility>().milk = 100;
        milk.GetComponent<Animator>().SetBool("died", false);
        milk.GetComponent<PlayerController>().canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(0);
    }
}
