using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    public void PlayFighting() //для кнопки "Fighting" в PlayMenu в сцене main
    {
        Application.LoadLevel(2); //открываем боевую сцену (сетевая игра)
    }

    public void SkipIntro() //для пропуска интро и перехода на новую сцену
    {
        Application.LoadLevel(1); //открываем меню
    }

    public void PlaCampaign() //для кнопки "Campaign" в PlayMenu в сцене main
    {
        Application.LoadLevel(3); //открываем сцену кампании (платформер)
    }

    public void Exit() //для кнопки крестик (exit) в сцене main
    {
        Application.Quit(); //выход из приложения
    }
}
