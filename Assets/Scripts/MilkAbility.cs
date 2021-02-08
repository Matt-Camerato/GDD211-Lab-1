using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MilkAbility : MonoBehaviour
{
    [SerializeField] private Image milkBar;
    [SerializeField] private GameObject youDiedText;
    [SerializeField] private GameObject milkBallPrefab;

    private Animator anim;

    public float milk = 100;
    private float milkCooldown = 0f;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        milkBar.fillAmount = milk / 100;

        if(milkCooldown != 0)
        {
            milkCooldown -= Time.deltaTime;
            if(milkCooldown < 0)
            {
                milkCooldown = 0;
            }
        }

        if(transform.position.y < -20)
        {
            //when you fall off map, die
            youDiedText.SetActive(true);
            GetComponent<PlayerController>().canMove = false;
            Cursor.lockState = CursorLockMode.None;
        }

        if(milk > 0)
        {
            if (Input.GetMouseButton(0) && milkCooldown == 0 && GetComponent<PlayerController>().canMove)
            {
                Instantiate(milkBallPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z + 1), Quaternion.identity);
                milk -= 10;
                milkCooldown = 1;
            }
        }
        else
        {
            //when you die, turn on death text and play death animation (also prevent player movement)
            youDiedText.SetActive(true);
            anim.SetBool("died", true);
            GetComponent<PlayerController>().canMove = false;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
