using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Image UnderAttackBlack;
    public static float BlackTime = 0;
    public static UiManager Manager;


    public Image BossesHp;
    public Image Spirit;
    public Image Health;
    public Text ItemTextShower;

    public static Role player;
    public static List<Role> Bosses = new List<Role>();

    void Awake()
    {
        //if (!Manager)
        Bosses.Clear();
        Manager = this;
    }
    private void Update()
    {
        BlackTime -= Time.deltaTime;
        if (BlackTime >= 0)
        {
            var k = UnderAttackBlack.color;
            k.a = Mathf.Min(0.5f, BlackTime / 0.4f);
            UnderAttackBlack.color = k;
        }
        else
        {
            var k = UnderAttackBlack.color;
            k.a = 0;
            UnderAttackBlack.color = k;
        }

        if (Bosses.Count == 0)
        {

            BossesHp.fillAmount = 0;
        }
        else
        {
            BossesHp.fillAmount = Bosses[0].Health/Bosses[0].Properties.MaxHealth;
        }
        Spirit.fillAmount = player.Spirit / player.Properties.MaxSpirit;
        Health.fillAmount = player.Health / player.Properties.MaxHealth;
    }
    public static void ShowItemDetail(float tim,string str)
    {
        Manager.ItemTextShower.GetComponent<ItemTextShower>().Show(tim, str);
    }
    public static void CreateDamageShow(Damage dam,Vector2 position,float mut)
    {
        var go = Instantiate(GameManager.UI[0],GameManager.Canvas.transform);
        var text = go.GetComponent<Text>();

        text.text = Convert.ToInt32(dam.FinalDamage).ToString();
        text.color = Color.red;

        var font = go.GetComponent<Font>();

        Lib.SetMultScale(go.gameObject, mut, mut);
        font.v2d = position + new Vector2(UnityEngine.Random.Range(-0.2f,0.2f),3+UnityEngine.Random.Range(-0.2f,0.2f));
    }
    public static void CreateNumShow(float k, Vector2 position,Color color)
    {
        if (k == 0)
            return;
        var go = Instantiate(GameManager.UI[0], GameManager.Canvas.transform);
        var text = go.GetComponent<Text>();

        text.text = Convert.ToInt32(k).ToString();
        text.color = color;

        var font = go.GetComponent<Font>();
        font.v2d = position + new Vector2(UnityEngine.Random.Range(-0.2f, 0.2f), 3 + UnityEngine.Random.Range(-0.2f, 0.2f));
    }
}
