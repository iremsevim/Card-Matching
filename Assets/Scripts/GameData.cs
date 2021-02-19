using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameData : MonoBehaviour
{
    public static GameData instance;
    public UI uI;
    public List<AudioData> AllAudios;
    

    public void Awake()
    {
        instance = this;
    }
    [System.Serializable]
    public class UI
    {
        public UI_MENU menu;
        public UI_InGame game;

        public GameObject SoundOnButton;
        public GameObject SoundOffButton;


       public class Window
        {
            public GameObject mainobject;
            public void Show()
            {
                mainobject.SetActive(true);
            }
            public void Hide()
            {
                mainobject.SetActive(false);
            }

        }
        [System.Serializable]
        public class UI_MENU:Window
        {
            
            public UI_SelectorCatoryMenu selectorCatoryMenu;
            public UI_LoginScreen loginScreen;
            public UI_DialogScreen dialogScreen;
            public Image BackGround;
            public Image InGameBackGround;
            public Text usertext;
            
           
            [System.Serializable]
            public class UI_SelectorCatoryMenu:Window
            {
                public Transform carrier;
                public GameObject prefab;
                public List<SelectItemProfil> AllCategoryItem;
              
                [System.Serializable]
                public class SelectItemProfil
                {
                    public string title;
                    public List<string> Words;
                    public List<Sprite> Images;
                       
                }
            }
            [System.Serializable]
            public class UI_LoginScreen:Window
            {
                public InputField usernamefield;
            }
            [System.Serializable]
            public class UI_DialogScreen:Window
            {
                public Text desc;
            }
        }
        [System.Serializable]
        public class UI_InGame:Window
        {
            public InGame_CardDatas cardDatas;
            public Text gametitle;
            public Text scoretext;
            public TextMeshPro hinttxt;
            public ParticleSystem Confettiparticle;
            public GameObject DestroyCardParticle;
            public Text GameFinishedText;
            public Text ExtraScoreText;
            [System.Serializable]
           public class InGame_CardDatas
            {
                public GameObject cardprefab_image;
                public GameObject cardprefab_text;
                public Transform carrierparent;
            }

        }
      
    }

    [System.Serializable]
    public class AudioData
    {

        public AudioType audioID;
        public AudioClip audioclip;
        
    }
    public enum AudioType
    {
        butonclick=0,trueevent=1,falseevent=2,gamestart=3,win=4,
    }
}

