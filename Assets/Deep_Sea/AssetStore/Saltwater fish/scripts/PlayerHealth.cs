using UnityEngine;

using UnityEngine.UI;
 
public class PlayerHealth : MonoBehaviour
{
    public Image healthBar;
    public float maxHealth = 100f;
    public static float health;
    public GameObject deathCanvas;
     
    void Start ()
    {
        health = maxHealth;
    }
    private void Update() 
    {
        if(health < 1)
        {
           this.GetComponent<FlyCamera>().enabled = false;
           deathCanvas.SetActive(true);
        }
    }
 
    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.fillAmount = health/maxHealth;
    }
}
