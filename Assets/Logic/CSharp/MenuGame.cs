using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[System.Serializable]
public class ButtonUT
{
    public string nameFunction;
    public string nameAltFunction;
    public Text nameButton;
    public SpriteRenderer spriteButton;
}

public class MenuGame : MonoBehaviour
{
    public ButtonUT[] buts;
    public int current;
    public Color textNormal = Color.white;
    public Color textSelect = Color.yellow;
    public bool Vertical;
    private void Start()
    {
        HyperbolicCamera.Main().Speed = 1;
        Iterfases = objFind.ArrayByType<TagIterface>();
        if (GetComponent<TagIterface>())
        {
            if (GetComponent<TagIterface>().Tag == "Main")
            {
                MainMenu();
            }
        }
    }
    public void offHeart()
    {
        for (int i = 0; i < buts.Length; i++)
        {
            buts[i].spriteButton.enabled = false;
            buts[i].nameButton.color = textNormal;
        }
    }
    void Update()
    {
        if (Vertical)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                current = (current - 1) % buts.Length;
                if (current < 0)
                {
                    current = buts.Length - 1;
                }
                Instantiate(Resources.Load<GameObject>("Move"));
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                current = (current + 1) % buts.Length;
                Instantiate(Resources.Load<GameObject>("Move"));
            } 
        }
        if (!Vertical)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                current = (current - 1) % buts.Length;
                if (current < 0)
                {
                    current = buts.Length - 1;
                    Instantiate(Resources.Load<GameObject>("Move"));
                }
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                current = (current + 1) % buts.Length;
                Instantiate(Resources.Load<GameObject>("Move"));
            }
        }
        for (int i = 0;i < buts.Length; i++)
        {
            if (i == current)
            {
                buts[i].spriteButton.enabled = true;
                buts[i].nameButton.color = textSelect;
                if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return))
                {
                    Invoke(buts[i].nameFunction, 1);
                    Instantiate(Resources.Load<GameObject>("Click"));
                }
                if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
                {
                    Invoke(buts[i].nameAltFunction, 1);
                    Instantiate(Resources.Load<GameObject>("Click"));
                }
            }
            if (i != current)
            {
                buts[i].spriteButton.enabled = false;
                buts[i].nameButton.color = textNormal;
            }
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void EnterStart()
    {
        SceneManager.LoadScene("Start");
    }
    public void Fight()
    {
        FightManager.instance.putDamage();
    }
    public TagIterface[] Iterfases;
  
    public void ActMenu()
    {
        foreach (TagIterface item in Iterfases)
        {
            item.GetComponent<MenuGame>().offHeart();
            if (item.Tag == "Act")
            {
                item.gameObject.layer = 5;
                item.GetComponent<MenuGame>().enabled = true;
            }
            else
            {
                item.gameObject.layer = 3;
                item.GetComponent<MenuGame>().enabled = false;
            }
        }
    }
    public void Check()
    {
        ConsoleBattle.console.data.text = "* Цветик Атака 10 Защита 10-i Здоровье " + FightManager.instance.hp + " Выносливость " + FightManager.instance.mp;
        FightManager.instance.RunBattle(); MainMenu();
    }
    public void AttackBoost()
    {
        ConsoleBattle.console.data.text = "* Разброс булав'ок предметы атаки добавлены на карту";
        FightManager.instance.ActFindDamage(); MainMenu();
    }
    public void AttackBoost2()
    {
        FightManager.instance.sp -= 1;
        ConsoleBattle.console.data.text = "* Разброс лед(о)>енЁ(с)>цов предметы атаки добавлены на карту (новой идеи) этот концепт полное безумие";
        FightManager.instance.ActFindDamage(); FightManager.instance.ActFindDamage(); MainMenu();
    }
    public void Sleep()
    {
        FightManager.instance.sp = 3;
        ConsoleBattle.console.data.text = "* Класно спите : 3";
        FightManager.instance.sleep = true;
        FightManager.instance.RunBattle(); MainMenu();
    }
    public void SpeedUp()
    {

        ConsoleBattle.console.data.text = "* Салат просто ултра помог здоровью вы обожаете алегорию вашаскорость увеличена : 3";
        if (FightManager.instance.timeEffect < 0)
        {
            HyperbolicCamera.Main().Speed = 4;
            FightManager.instance.timeEffect = 2;
        }

        FightManager.instance.RunBattle(); MainMenu();
    }
    public void spare()
    {

        ConsoleBattle.console.data.text = "* Пощада.";
        if (FightManager.instance.hp > 0)
        {
            if (FightManager.instance.mp < 0)
            {
                SceneManager.LoadScene("Slide9");
                ConsoleBattle.console.data.text += " Удалась!";
            }else
            ConsoleBattle.console.data.text += " Не жертва!";
        }
        else
        {
            ConsoleBattle.console.data.text += " Он был убит как ты пощадишь!";
        }

        MainMenu();
    }
    public void help()
    {

        ConsoleBattle.console.data.text = "* Помощь.";
        if (FightManager.instance.hp > 0)
        {
            if (FightManager.instance.hp < 125)
            {
                if (FightManager.instance.mp < 0)
                {
                    SceneManager.LoadScene("Slide10");
                    ConsoleBattle.console.data.text += " Удалась!";
                }
                else
                    ConsoleBattle.console.data.text += " Не устал!";
            }
            else
                ConsoleBattle.console.data.text += " Не в беде!";
        }
        else
        {
            ConsoleBattle.console.data.text += " Ему конец настал уже позно его нет!";
        }

        MainMenu();
    }
    public void kill()
    {

        ConsoleBattle.console.data.text = "* Добивание.";
        if (FightManager.instance.hp < 0)
        {
            
                    SceneManager.LoadScene("Slide11");
                    ConsoleBattle.console.data.text += " Удалось!";
             
        }
        else
        {
            ConsoleBattle.console.data.text += " Он жив!";
        }

        MainMenu();
    }
    public void PointHp()
    {

        ConsoleBattle.console.data.text = "* Помог здоровью борщ это полезно жаль не сразу *вы его съели* : 3";

      if(Teleport.hp == 0)  Teleport.hp = 3;
        FightManager.instance.RunBattle(); MainMenu();
    }
    public void MainMenu()
    {
        foreach (TagIterface item in Iterfases)
        {
            item.GetComponent<MenuGame>().offHeart();
            if (item.Tag == "Main")
            {
                item.gameObject.layer = 5;
                item.GetComponent<MenuGame>().enabled = true;
            }
            else
            {
                item.gameObject.layer = 3;
                item.GetComponent<MenuGame>().enabled = false;
            }
        }
    }
    public void MersyMenu()
    {
        foreach (TagIterface item in Iterfases)
        {
            item.GetComponent<MenuGame>().offHeart();
            if (item.Tag == "Mersy")
            {
                item.gameObject.layer = 5;
                item.GetComponent<MenuGame>().enabled = true;
            }
            else
            {
                item.gameObject.layer = 3;
                item.GetComponent<MenuGame>().enabled = false;
            }
        }
    }
    public void Invetory()
    {
        foreach (TagIterface item in Iterfases)
        {
            item.GetComponent<MenuGame>().offHeart();
            if (item.Tag == "Invetory")
            {
                item.gameObject.layer = 5;
                item.GetComponent<MenuGame>().enabled = true;
            }
            else
            {
                item.gameObject.layer = 3;
                item.GetComponent<MenuGame>().enabled = false;
            }
        }
    }
    public void Next()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void End()
    {
        SceneManager.LoadScene("End");
    }
    public void Back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
