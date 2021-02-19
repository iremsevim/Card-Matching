using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public static class Worker
{
    #region UI
    public static class UI_Worker
    {

        public static class UI_Menu
        {
            public static void MenuToSelectCategory()
            {
                GameData.instance.uI.menu.Hide();
                GameManager.instance.CreateSelectCategoryButton();
                GameData.instance.uI.menu.selectorCatoryMenu.Show();
            }
            public static void SelectCategoryToInGame()
            {
                GameData.instance.uI.menu.selectorCatoryMenu.Hide();
             

                GameData.instance.uI.game.Show();
                GameManager.instance.runTimeData.IsGameStarted = true;


                GameManager.instance.CreateCard();
                GameManager.instance.runTimeData.hindcont = 2;
                GameManager.instance.runTimeData.findedtextID = -1;
                GameManager.instance.runTimeData.findedımageID = -1;
                GameManager.instance.runTimeData.truematchcount = 0;
                GameManager.instance.runTimeData.falsematchcount = 0;
                GameManager.instance.runTimeData.scorecount = 0;
                GameManager.instance.runTimeData.currentscore = 0;
                GameManager.instance.runTimeData.GameTime = 0;

            }
            public static void ShowDialog(string desc)
            {
                GameData.instance.uI.menu.dialogScreen.desc.text = desc;
                GameData.instance.uI.menu.dialogScreen.Show();
            }
            public static void LoginScreenTomenu()
            {
                GameData.instance.uI.menu.loginScreen.Hide();
                GameData.instance.uI.menu.Show();
                GameData.instance.uI.menu.usertext.text = GameManager.instance.runTimeData.loginusername;
            }
            public static void InGameToMenu()
            {
                 GameData.instance.StartCoroutine(WebApi.instance.Service(WebApi.SendTimeOnlyUrl(GameManager.instance.runTimeData.loginuserID, GameManager.instance.runTimeData.GameCode, ((int)GameManager.instance.runTimeData.GameTime).ToString()), x => Debug.Log(x)));

                GameData.instance.uI.game.Hide();
                
                foreach (Transform item in GameData.instance.uI.game.cardDatas.carrierparent)
                {
                    GameData.Destroy(item.gameObject);
                }
                GameManager.instance.runTimeData.AllObject.RemoveAll(x => x.gameObject);
                GameManager.instance.runTimeData.allımages = new List<Sprite>();
                GameManager.instance.runTimeData.Alltext = new List<string>();
                GameManager.instance.runTimeData.IsGameStarted = false;

                GameData.instance.uI.menu.Show();
            }
        }
    }
    public static class AudioWorker
    {
        public static void PlayAudio(GameData.AudioType type)
        {
            GameData.AudioData findedaudio = GameData.instance.AllAudios.Find(x => x.audioID == type);
            GameObject audiosource = new GameObject();
            audiosource.AddComponent<AudioSource>();
            audiosource.GetComponent<AudioSource>().PlayOneShot(findedaudio.audioclip);
            GameData.Destroy(audiosource.gameObject, 2f);
        }
    }

    #endregion

    public static class InGameWorker
    {
     
    }
}
public static class UI_InGame
{
    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(this IList<T> list)
    {

        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }

    }
    public static void Positionandomizer(this List<Transform> transforms)
    {
        List<Vector3> newpos=transforms.ConvertAll(x => x.transform.position);
        newpos.Shuffle();
        for (int i = 0; i < transforms.Count; i++)
        {
            transforms[i].position = newpos[i];
        }

      


    }
}

