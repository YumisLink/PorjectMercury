using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISkills : MonoBehaviour
{
    public int skills;
    public Image img;
    private void Start()
    {
        img = GetComponent<Image>();
    }
    private void Update()
    {
        if (skills == 1)
            img.sprite =  Player.player.Skill1.SkillImage;
        if (skills == 2)
            img.sprite = Player.player.Skill2.SkillImage;
    }
}
