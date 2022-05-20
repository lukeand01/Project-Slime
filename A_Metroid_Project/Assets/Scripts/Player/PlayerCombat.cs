using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerCombat : MonoBehaviour
{
    //RECEIVE DAMAGE.
    //ATTACK.




    [Header("PROJECTILS")]
    [SerializeField] GameObject gooProjectil;
    [SerializeField] GameObject stoneProjectil;

    int currentProjectil;
    public int totalProjectils = 1;

    [Header("PROJECTIL AMMO")]
    public int stoneQuantity = 1;
    public int webQuantity;

    [Header("HEALTH")]
    public float maxHealth = 100;
    public float currentHealth;
    float initialHealth; //THIS IS THE DECAYING BAR HEALTH.


    [Header("HEALTH UI")] //MAYBE I SHOULDNT HAVE DEPENDICES. BUT I NEED TO CONTROL HERE FOR 
    [SerializeField] GameObject healthBarBackground;
    [SerializeField] Image currentHealthBar;
    [SerializeField] Image initialHealthbar;

    [Header("RESOURCE UI")]
    [SerializeField] Image shooterImage;
    [SerializeField] TextMeshProUGUI shooterName; //THIS IS FOR TESTING.
    [SerializeField] Image shooterAmmoImage;
    [SerializeField] TextMeshProUGUI shooterAmmoText;
    [SerializeField] Image QButton;

    [Header("LAYERS")]
    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask invicibleLayer;

    [Header("COLOR")]
    [SerializeField] Color normalColor;
    [SerializeField] Color hurtColor;

    private void Start()
    {
        //THIS MIGHT CHANGE WHEN WE ARE DEALING WITH SAVED DATA.
        currentHealth = 1;
        initialHealth = currentHealth;
    }

    private void Update()
    {
        //JUST FOR TEST
       

    }

    #region SHOOT

    public void HandleShooter(Vector3 shootDir)
    {
        if (currentProjectil == 0)
        {
            ShootGoo(shootDir);
            return;
        }
        if (currentProjectil == 1)
        {
            ShootStone(shootDir);
            return;
        }
        if (currentProjectil == 2)
        {
            ShootWeb(shootDir);
            return;
        }
    }

    public void ShootGoo(Vector3 shootDir)
    {
        //THE FIRST VERSION. JUST SHOOT UP, LEFT OR RIGHT.
        //FIRE FROM THE MOUTH.
        GameObject mouth = gameObject.transform.GetChild(1).gameObject;
        //JUST DEAL DAMAGE BUT DO NOT PUSH IT.
        GameObject newObject = Instantiate(gooProjectil, mouth.transform.position, Quaternion.identity);
        newObject.GetComponent<ProjectilBase>().SetUp(shootDir, 2);
    }

    public void ShootStone(Vector3 shootDir)
    {
        if (stoneQuantity <= 0) return;
        GameObject mouth = gameObject.transform.GetChild(1).gameObject;
        GameObject newObject = Instantiate(stoneProjectil, mouth.transform.position, Quaternion.identity);
        //THIS NEEDS TO PUS THE ENEMY BACK.
        newObject.GetComponent<ProjectilBase>().SetUp(shootDir, 2);
        stoneQuantity -= 1;
        ShooterUI();
    }
    public void ShootWeb(Vector3 shootDir)
    {
        ShooterUI();
    }


    public void ChangeShooter()
    {
        currentProjectil++;

        if (currentProjectil >= totalProjectils)
        {
            currentProjectil = 0;
        }

        ShooterUI();
    }

    void ShooterUI()
    {
        if (currentProjectil == 0)
        {
            shooterName.text = "Goo";
            shooterAmmoImage.gameObject.SetActive(false);
        }
        if (currentProjectil == 1)
        {
            shooterName.text = "Stone";
            shooterAmmoImage.gameObject.SetActive(true);
            shooterAmmoText.text = stoneQuantity.ToString();
        }
        if (currentProjectil == 2)
        {
            shooterName.text = "Web";
            shooterAmmoImage.gameObject.SetActive(true);
            shooterAmmoText.text = webQuantity.ToString();
        }
    }
    #endregion




    #region HEALTH FUNCTIONS

    public void LoseHealth(PlayerHandler handler, float amount, GameObject attacker)
    {

        //DOES SOME COOL EFFECT.
        //ALSO NEED TO BE PUSHED BACK.

        Push(handler, attacker);

        StartCoroutine(ProcessHealth(amount));
        StartCoroutine(ProcessInvicibility(2));

        if (currentHealth <= 0) Die();
    }
    public void RegainHealth()
    {

    }
    public void GainMaxHealth(float amount)
    {
        //THE RATIO? 10 = 0.1
        maxHealth += amount;
        currentHealth += amount;
        initialHealth += amount;

        float scaled = amount * 0.01f;

       Vector3 vector =  new Vector3(1, 0, 0);
       healthBarBackground.transform.localScale +=  vector * scaled;

        currentHealthBar.fillAmount = (float)currentHealth / maxHealth;

    }

    void Die()
    {
        //WE BECOME UNTARGETTABLE.
        //PLAY ANIMATION.
        //GIVE THE PLAYER THE CHOICE TO GO TO MENU OR RESPAWN.
        //RESPAWN IN THE LAST PLACE.
        //YOU ALWAYS RESPAWN AT THE LAST SAVE.

        Debug.Log("died");
        gameObject.layer = 12;
        Observer.instance.OnDeathMenu(0);
    }

    IEnumerator ProcessHealth(float damage)
    {
       //


        
        //THIS PROCESS IS FINE.
        for (int i = 0; i < damage; i++)
        {
            currentHealth--;
            currentHealthBar.fillAmount = (float)currentHealth / maxHealth;
            yield return new WaitForSeconds(0.03f);
        }



        for (int i = 0; i < damage; i++)
        {
            initialHealth--;
            initialHealthbar.fillAmount = (float)initialHealth / maxHealth;

            //IN HERE WE BREAK IN CASE AND REPLENISH THIS QUANTITY TO THE PLAYER;

            yield return new WaitForSeconds(0.2f);
        }

    }

    IEnumerator ProcessInvicibility(float seconds)
    {
        SpriteRenderer sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        //BECOME INVICIBLE
        sr.color = hurtColor;
        gameObject.layer = LayerMask.NameToLayer("Invicible");
        yield return new WaitForSeconds(seconds);
        sr.color = normalColor;
        gameObject.layer = LayerMask.NameToLayer("Player");
        //RETURN TO NORMAL

    }

    void Push(PlayerHandler handler, GameObject attacker)
    {
        Vector3 directionTowardsPlayer = (transform.position - attacker.transform.position).normalized;
        int direction = 0;


        if (directionTowardsPlayer.x > 0)
        {
            direction = 1;
        }
        if (directionTowardsPlayer.x < 0)
        {
            direction = -1;
        }

        //DIFFERENT THINGS SHOULD PUSH WITH DIFFERENT FORCES.
        handler.rb.AddForce(new Vector2(direction * 2, 1.5f), ForceMode2D.Impulse);

    }
    #endregion


    #region HELP FUNCTIONS
    public void GainRock(int quantity)
    {
        //THERE SHOULD BE A LIMIT
        stoneQuantity += quantity;
    }

    #endregion


}
