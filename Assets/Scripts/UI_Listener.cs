using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Listener : MonoBehaviour
{
   
   
        public void MenuToGame(int ID)
        {
        Worker.AudioWorker.PlayAudio(GameData.AudioType.butonclick);

            switch(ID)
            {
                case 0:
                    Worker.UI_Worker.UI_Menu.MenuToSelectCategory();
                    break;
            case 1:
                GameData.instance.uI.menu.BackGround.sprite = GameManager.instance.runTimeData.Backgorunds[0];
                GameData.instance.uI.menu.InGameBackGround.sprite= GameManager.instance.runTimeData.Backgorunds[0];
                Debug.Log("Tıklandı");
                break;
            case 2:
                GameData.instance.uI.menu.BackGround.sprite = GameManager.instance.runTimeData.Backgorunds[1];
                GameData.instance.uI.menu.InGameBackGround.sprite = GameManager.instance.runTimeData.Backgorunds[1];
                Debug.Log("Tıklandı");
                break;
            case 3:
                GameData.instance.uI.menu.BackGround.sprite = GameManager.instance.runTimeData.Backgorunds[2];
                GameData.instance.uI.menu.InGameBackGround.sprite = GameManager.instance.runTimeData.Backgorunds[2];
                Debug.Log("Tıklandı");
                break;
            case 4:
                GameData.instance.uI.menu.BackGround.sprite = GameManager.instance.runTimeData.Backgorunds[3];
                GameData.instance.uI.menu.InGameBackGround.sprite = GameManager.instance.runTimeData.Backgorunds[3];
                Debug.Log("Tıklandı");
                break;

            case 5:
                GameData.instance.uI.menu.BackGround.sprite = GameManager.instance.runTimeData.Backgorunds[4];
                GameData.instance.uI.menu.InGameBackGround.sprite = GameManager.instance.runTimeData.Backgorunds[4];
                Debug.Log("Tıklandı");
                break;
            case 6:
                GameData.instance.uI.menu.BackGround.sprite = GameManager.instance.runTimeData.Backgorunds[5];
                GameData.instance.uI.menu.InGameBackGround.sprite = GameManager.instance.runTimeData.Backgorunds[5];
                Debug.Log("Tıklandı");
                break;
            case 7:
                GameData.instance.uI.menu.BackGround.sprite = GameManager.instance.runTimeData.Backgorunds[6];
                GameData.instance.uI.menu.InGameBackGround.sprite = GameManager.instance.runTimeData.Backgorunds[6];
                Debug.Log("Tıklandı");
                break;
            case 8:
                GameData.instance.uI.menu.BackGround.sprite = GameManager.instance.runTimeData.Backgorunds[7];
                GameData.instance.uI.menu.InGameBackGround.sprite = GameManager.instance.runTimeData.Backgorunds[7];
                Debug.Log("Tıklandı");
                break;


        }
    }
   
       
          
    public void InGameToMenu(int ID)
    {
        Worker.AudioWorker.PlayAudio(GameData.AudioType.butonclick);

        switch (ID)
        {
            case 0:
                Worker.UI_Worker.UI_Menu.InGameToMenu();
                break;
            case 1:
                break;
        }
    }
  

  

    }

