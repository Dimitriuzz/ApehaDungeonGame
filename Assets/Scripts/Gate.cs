using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gate : MonoBehaviour
{
    [SerializeField] private int needKeys;
    [SerializeField] public TMP_Text message;
    [SerializeField] public GameObject gate;

  
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("entered");
        var animation = gate.GetComponent<Animator>();
        collision.transform.root.TryGetComponent<Player>(out var player);

        if (player.keyNumber >= needKeys)
        {
            animation.enabled = true;
        }
        else
            message.text = "��� ����� " + needKeys.ToString() + " ����� ����� ������� ��� �����";

    }

    private void OnTriggerExit(Collider collision)
    {
        message.text = "";
    }
}
