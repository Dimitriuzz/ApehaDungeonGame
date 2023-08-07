using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;


public class Player : MonoBehaviour
{
    public LayerMask canBeClicked;

    private NavMeshAgent character;

    private Animator animator;

    private bool inFight=false;

    private string[] zones = { "голову", "левую руку", "правую руку", "грудь", "ноги" };

    public ToggleGroup defendTogglesGroup;
    public ToggleGroup attackTogglesGroup;

    [SerializeField] private int playerHP;
    [SerializeField] private int playerAttack;
    private int playerCurrentHP;
    [SerializeField] public string playerName;
    [SerializeField] public TMP_Text playerMainNameText;

    [SerializeField] private Image playermainHPBar;
    [SerializeField] private Image playerfightHPBar;
    [SerializeField] private Image enemyfightHPBar;
    [SerializeField] public TMP_Text playermainHPText;
    [SerializeField] public TMP_Text playerfightHPText;
    [SerializeField] public TMP_Text enemyfightHPText;

    [SerializeField] public TMP_Text playerfightText;
    [SerializeField] public TMP_Text enemyfightText;
    [SerializeField] public TMP_Text playerfightNameText;
    [SerializeField] public TMP_Text enemyfightNameText;
    [SerializeField] public TMP_Text keysText;

    [SerializeField] GameObject fightPanel;
    [SerializeField] GameObject deathPanel;

    private Enemy currentEnemy;

    private int playerHit;

    public int keyNumber;






    void Start()
    {
        character = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        playerMainNameText.text = playerName;
        playerCurrentHP = playerHP;
        playermainHPText.text = playerHP.ToString();
    }

    
    void Update()
    {
        if (!inFight)
        {
            if (Input.GetMouseButtonDown(0))
            {
                /*var camera = GetComponentInChildren<Camera>();
                var mousePos2D = Input.mousePosition;
                var screenToCameraDistance = camera.nearClipPlane;

                var mousePosNearClipPlane = new Vector3(mousePos2D.x, mousePos2D.y, screenToCameraDistance);


                var worldPointPos = camera.ScreenToWorldPoint(mousePosNearClipPlane);

                Debug.Log("pressed");
                Debug.Log("moved to" + worldPointPos);

                character.SetDestination(worldPointPos);*/

                //animator.Play("walk");
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                {
                    character.SetDestination(hit.point);


                }

            }
        }
        else character.velocity = Vector3.zero;

        //Debug.Log(character.velocity);

        if (character.velocity == Vector3.zero)
        {
            animator.SetInteger("Walk", 0);
            
        }
        else
        {
            animator.SetInteger("Walk", 1);
            
        }

        keysText.text = keyNumber.ToString();

        }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("walk");

        if (collision.transform.root.TryGetComponent<Enemy>(out var enemy))
        {
            Debug.Log("idle");
            inFight = true;
            currentEnemy = enemy;
            fightPanel.SetActive(true);
            playerfightNameText.text = playerName;
            enemyfightNameText.text= currentEnemy.Name;
            playerfightHPText.text = playerCurrentHP.ToString();
            enemyfightHPText.text = currentEnemy.HP.ToString();
           



        }
    }

    public void Fight()
    {
        animator.Play("atack shield");

        var enemyanim = currentEnemy.GetComponent<Animator>();

        enemyanim.Play("Attack");

        int enemyHit = Random.Range(0, 5);
        int enemyBlock = Random.Range(0, 5);
        int enemyDamage = Random.Range(0, currentEnemy.Attack+1);
        int playerDamage = Random.Range(0, playerAttack+1);
        bool isBlocked=false;
        var toggles = defendTogglesGroup.GetComponentsInChildren<Toggle>();

        if (Random.Range(0,100)<10)
        {
            enemyfightText.text = playerName + " <color=green>увернулся</color> от удара " + currentEnemy.Name + " в " + zones[enemyHit];
        }
        else
        {
            

            for(int i=0; i<5; i++)
            {
                if(toggles[i].isOn&&enemyHit==i)
                {
                    isBlocked = true;
                    if(Random.Range(0, 100) < 10)
                    {
                        enemyfightText.text = currentEnemy.Name+ " <color=blue>пробил блок</color> и нанес " + playerName + " " + enemyDamage.ToString()+" урона в " + zones[enemyHit];
                        playerCurrentHP -= enemyDamage;
                        if (playerCurrentHP < 0) playerCurrentHP = 0;
                    }
                    else
                    {
                        enemyfightText.text = playerName + " <color=#a52a2aff>заблокировал</color> удар " + currentEnemy.Name + " в " + zones[enemyHit];
                    }
                }
            }
            if (!isBlocked)
            {
                enemyfightText.text = currentEnemy.Name + " нанес " + playerName + " " + enemyDamage.ToString() + " урона в " + zones[enemyHit];
                playerCurrentHP -= enemyDamage;
                if (playerCurrentHP < 0) playerCurrentHP = 0;
            }

            }
        isBlocked = false;
        

        toggles = attackTogglesGroup.GetComponentsInChildren<Toggle>();

        for (int i = 0; i < 5; i++)
        { if (toggles[i].isOn) playerHit = i; }

            if (Random.Range(0, 100) < 10)
        {
            playerfightText.text = currentEnemy.Name + " <color=green>увернулся</color> от удара " + playerName+ " в "+ zones[playerHit];
        }
        else
        {
            

            
                if (playerHit==enemyBlock)
                {
                    isBlocked = true;
                    if (Random.Range(0, 100) < 10)
                    {
                        playerfightText.text = playerName + " <color=blue>пробил блок</color> и нанес " +  currentEnemy.Name+ " " + playerDamage.ToString() + " урона в " + zones[playerHit];
                        currentEnemy.currentHP -= playerDamage;
                        if (currentEnemy.currentHP < 0) currentEnemy.currentHP = 0;
                    }
                    else
                    {
                        playerfightText.text = currentEnemy.Name + " <color=#a52a2aff>заблокировал</color> удар " + playerName + " в " + zones[playerHit];
                    }
                }
            }
            if (!isBlocked)
            {
                playerfightText.text = playerName + " нанес " + currentEnemy.Name + " " + playerDamage.ToString() + " урона в " + zones[playerHit];
                currentEnemy.currentHP -= playerDamage;
                if (currentEnemy.currentHP < 0) currentEnemy.currentHP = 0;
            }
        

        playermainHPBar.fillAmount = (float)playerCurrentHP / (float)playerHP;
        playerfightHPBar.fillAmount = (float)playerCurrentHP / (float)playerHP;
        enemyfightHPBar.fillAmount = (float)currentEnemy.currentHP / (float)currentEnemy.HP;
        playermainHPText.text = playerCurrentHP.ToString();
        playerfightHPText.text = playerCurrentHP.ToString();
        enemyfightHPText.text = currentEnemy.currentHP.ToString();


        if (currentEnemy.currentHP==0)
        {
            fightPanel.SetActive(false);
            Instantiate(currentEnemy.deathEffect, currentEnemy.transform.position,Quaternion.identity);
            Instantiate(currentEnemy.drop, currentEnemy.transform.position,Quaternion.identity);
            Debug.Log(currentEnemy.transform.position);
            Destroy(currentEnemy.gameObject);
            inFight = false;
            animator.Play("idle1");

        }

        if (playerCurrentHP==0)
        {
            inFight = true;
            animator.Play("death");
            fightPanel.SetActive(false);
            deathPanel.SetActive(true);
        }


    }
}
