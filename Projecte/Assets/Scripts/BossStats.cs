using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossStats : CharacterStats
{
    public GameObject body;
    private bool hit = false;
    private EnemyHUD hud;

    public void Start() {
        hud = GetComponent<EnemyHUD>();
        hud.UpdateHealth(health, baseHealth, shield, baseShield);
        hud.Hide();
    }

    public override void  TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        hud.UpdateHealth(health, baseHealth, shield, baseShield);
    }

    
    public override void Die() {
        animator.SetBool("Die", true);
        StartCoroutine(ChangeSceneAfterDeath());
    }
    
    IEnumerator ChangeSceneAfterDeath() {
        yield return new WaitForSeconds(dieTime);      
        SceneManager.LoadScene("VictoryScene");
        yield return new WaitForSeconds(0.3f);      
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && hit) {
            collision.gameObject.GetComponent<PlayerStats>().TakeDamage(200);
        }
        
    }

    public void SetHit(bool b) {
        hit = b;
    }
    
}
