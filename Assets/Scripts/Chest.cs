using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject message;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider collision)
    {
        message.SetActive(true);

    }

    private void OnTriggerExit(Collider collision)
    {
        message.SetActive(false);

    }


    public void Quit()
    {
        Application.Quit();
    }
}
