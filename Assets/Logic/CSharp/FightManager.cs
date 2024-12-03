using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightManager : MonoBehaviour
{
    public static FightManager instance;
    public float hp = 350;
    public float mp = 200;
    public float timeAttack = 60;
    public int currentFightAnimation;
    public GameObject damageEffect10hp;
    public GameObject damageEffect5hp;
    public GameObject damageItem;
    public Sprite[] PowerHeadChek;
    public SpriteRenderer Circle;
    public SpriteRenderer Head;
    public Animator FightAnimator;
    public string[] FightAnimations;
    public string FinalFightAnimation;
    public Transform DETarget;
    public bool devMode;
    private void Start()
    {
        instance = this;
    }
    void OnDestroy()
    {
        Platform.init = false;
        Platform.platforms.Clear();
    }
    public void putDamage()
    {
        if (timeAttack <= 0)
        {
          
            if (sp > 0)
            {
                instance.hp -= 10;
                Instantiate(damageEffect10hp, DETarget.position, DETarget.rotation);
                var pos = Instantiate(damageItem, DETarget.position, DETarget.rotation).GetComponent<HyperbolicPoint>().HyperboilcPoistion;
                pos.applyRotation(UnityEngine.Random.Range(-3f, 3f));
                pos.applyTranslationY(UnityEngine.Random.Range(-3f, 3f));
                pos.applyTranslationZ(UnityEngine.Random.Range(-3f, 3f));
            }
            else
            {
                instance.hp -= 5;
                Instantiate(damageEffect5hp, DETarget.position, DETarget.rotation);
            }

            RunBattle();
        }
    }
    public void grabDamage()
    {
        if (timeAttack <= 0)
        {
            instance.hp -= 5;
            Instantiate(damageEffect5hp, DETarget.position, DETarget.rotation);
        }
    }
    public void ActFindDamage()
    {
        if (timeAttack <= 0)
        {
            for (int i = 0; i < 4 - (sp > 0 ? 0 : 2); i++)
            {
                var pos = Instantiate(damageItem, DETarget.position, DETarget.rotation).GetComponent<HyperbolicPoint>().HyperboilcPoistion;
                pos.applyRotation(UnityEngine.Random.Range(-3f, 3f));
                pos.applyTranslationY(UnityEngine.Random.Range(-3f, 3f));
                pos.applyTranslationZ(UnityEngine.Random.Range(-3f, 3f));
            }
            RunBattle();
        }
    }
    public void RunBattle()
    {
        if (timeAttack <= 0)
        {
            sp -= 1;
            instance.mp -= 5;
            if (hp < 0 || mp < 0) FightAnimator.SetTrigger(FinalFightAnimation); else FightAnimator.SetTrigger(FightAnimations[currentFightAnimation]);
            currentFightAnimation = (currentFightAnimation + 1) % FightAnimations.Length;
            timeAttack = 8;
        }
    }
    public int sp = 2;
    public bool sleep;
    public int timeEffect = 0;
    private void OnGUI()
    {
        if (hp > 125 && mp > 0) Head.sprite = PowerHeadChek[0];
        if (hp > 125 && mp < 0) Head.sprite = PowerHeadChek[1];
        if (hp < 125 && mp > 0) Head.sprite = PowerHeadChek[2];
        if (hp < 0 && mp < 0) Head.sprite = PowerHeadChek[3];
        if (timeAttack > 0) 
        {
            timeAttack -= Time.deltaTime;
            if (Teleport.hp == 0) Circle.color = Color.green;
            if (Teleport.hp > 0) Circle.color = Color.cyan;
            if (sleep)
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)|| Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)|| Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {

                    Platform.init = false;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
#if UNITY_64
            if (!devMode)
            {
#endif
                if (Platform.platforms.Count <= 0 && Platform.init)
                {
                    Platform.init = false;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

                }
#if UNITY_64
            }
#endif
        }
        else
        {
            if (FightManager.instance.timeEffect<=0)
            {
                HyperbolicCamera.Main().Speed = 1;
            }
            timeEffect -= 1;
            timeAttack = 0;
            sleep = false;
            if (Teleport.hp == 0) if (sp > 0) Circle.color = Color.white;
            if (Teleport.hp > 0) if (sp > 0) Circle.color = Color.blue;
            if (sp <= 0) Circle.color = Color.red;
        }
#if UNITY_64
        if (devMode)
        {
            GUI.Label(new Rect(0, 0, 200, 50), "Hp : " + hp);
            GUI.Label(new Rect(0, 20, 200, 50), "Time of the attack | min : " + (int)(timeAttack / 60) + " | sec : " + MathF.Round(timeAttack % 60, 1));
            GUI.Label(new Rect(0, 55, 200, 50), "Count Poatfom under you : " + Platform.platforms.Count);
            GUI.Label(new Rect(0, 75, 200, 50), "Mp : " + mp); 
        }
#endif
    }
}
