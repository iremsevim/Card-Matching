using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;
using System.Runtime.InteropServices;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public RunTimeData runTimeData;


    private void Awake()
    {
        instance = this;
        deta x = new deta();
    }
    public void Update()
    {
        if(runTimeData.IsGameStarted)
        {
            runTimeData.GameTime += Time.deltaTime;
        }
    }
    public void CreateSelectCategoryButton()
    {
        foreach (Transform item in GameData.instance.uI.menu.selectorCatoryMenu.carrier)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in GameData.instance.uI.menu.selectorCatoryMenu.AllCategoryItem)
        {
            GameObject createditem = Instantiate(GameData.instance.uI.menu.selectorCatoryMenu.prefab, GameData.instance.uI.menu.selectorCatoryMenu.carrier);
            createditem.GetComponent<SelectCategoryItem>().SetUp(item.title, () =>
            {
                Worker.AudioWorker.PlayAudio(GameData.AudioType.butonclick);
                Debug.Log(item.title);
                GameData.instance.uI.game.gametitle.text = item.title;
                GameData.instance.uI.game.gametitle.text = item.title;

                GameData.UI.UI_MENU.UI_SelectorCatoryMenu.SelectItemProfil findeditem = GameData.instance.uI.menu.selectorCatoryMenu.AllCategoryItem.Find(x => x == item);
                runTimeData.allımages.AddRange(findeditem.Images);
                runTimeData.Alltext.AddRange(findeditem.Words);


                Worker.UI_Worker.UI_Menu.SelectCategoryToInGame();
            });
        }
    }
    public void CreateCard()
    {
        runTimeData.AllObject.ForEach(x => Destroy(x.gameObject));
        runTimeData.AllObject.Clear();


        Worker.AudioWorker.PlayAudio(GameData.AudioType.gamestart);

        runTimeData.currentscore = 0;
        runTimeData.IsGameStarted = true;
        runTimeData.findedtextID = -1;
        runTimeData.findedımageID = -1;


        List<Sprite> fakelist = runTimeData.allımages.ToList();
        List<Transform> fakepointlist = runTimeData.AllCardPoints.ToList();
        List<string> fakestringlist = runTimeData.Alltext.ToList();

        Debug.Log(runTimeData.totalgameobjectccount = runTimeData.allımages.Count + runTimeData.Alltext.Count);
        for (int i = 0; i < runTimeData.allımages.Count; i++)
        {
            int index = i;

            Transform cardcreatedpoint = fakepointlist[Random.Range(0, fakelist.Count)];
            fakepointlist.Remove(cardcreatedpoint);
            Sprite sprite = fakelist[Random.Range(0, fakelist.Count)];
            fakelist.Remove(sprite);

            GameObject cardcreated = Instantiate(GameData.instance.uI.game.cardDatas.cardprefab_image, cardcreatedpoint.transform.position, Quaternion.identity);
            cardcreated.transform.SetParent(GameData.instance.uI.game.cardDatas.carrierparent);
            cardcreated.GetComponent<MatchingCard_Image>().cardımage.sprite = sprite;
            runTimeData.AllObject.Add(cardcreated.transform);
            cardcreated.GetComponent<MatchingCard_Image>().OnCicked = () =>
              {
                
                 
                  //Eşleştirmeyi bulabilmek için ID bulunan kısım
                  Sprite finded = cardcreated.GetComponent<MatchingCard_Image>().cardımage.sprite;
                  Debug.Log(finded);
                  if (runTimeData.findedımageID == -1)
                      runTimeData.findedımageID = runTimeData.allımages.FindIndex(x => x == finded);
                  else
                  {
                      runTimeData.findedtextID = runTimeData.allımages.FindIndex(x => x == finded);
                  }
                  Debug.Log(runTimeData.findedımageID);

                  TryMatchControl();


              };
            cardcreated.name = runTimeData.allımages.FindIndex(x => x == sprite).ToString();

        }
         int textcount = runTimeData.Alltext.Count;
        for (int i = 0; i < runTimeData.Alltext.Count; i++)
        {
           
            Transform cardcreatedpoint = fakepointlist[Random.Range(0, fakelist.Count)];
            fakepointlist.Remove(cardcreatedpoint);
            string text = fakestringlist[Random.Range(0, fakestringlist.Count)];
            fakestringlist.Remove(text);
            GameObject createdtext = Instantiate(GameData.instance.uI.game.cardDatas.cardprefab_text, cardcreatedpoint.transform.position, Quaternion.identity);
            createdtext.transform.SetParent(GameData.instance.uI.game.cardDatas.carrierparent);
            runTimeData.AllObject.Add(createdtext.transform);
            createdtext.GetComponent<MatchingCard_Text>().text.text = text;
            createdtext.GetComponent<MatchingCard_Text>().OnCicked = () =>
             {
                
                
                 //Eşleştirmeyi bulabilmek için ID bulunan kısım
                 string finded = createdtext.GetComponent<MatchingCard_Text>().text.text;
                 Debug.Log(finded);

                 if (runTimeData.findedtextID == -1)

                     runTimeData.findedtextID = runTimeData.Alltext.FindIndex(x => x == finded);
                 else
                 {
                     runTimeData.findedımageID = runTimeData.Alltext.FindIndex(x => x == finded);
                 }

                 Debug.Log(runTimeData.findedtextID);

                 TryMatchControl();

             };
            createdtext.name = runTimeData.Alltext.FindIndex(x => x == text).ToString();

        }
        runTimeData.AllObject.Positionandomizer();

       Transform selecteddeliverypoint=runTimeData.AllObject[Random.Range(0, runTimeData.AllObject.Count)];

        int step = 0;
        foreach (var item in runTimeData.AllObject)
        {
            Vector3 first = item.eulerAngles;
            item.Rotate(0, 0, 90);
            step++;
            Vector3 firstpoint = item.transform.position;
            item.transform.position = selecteddeliverypoint.position;
            float time = Random.Range(0.3f, 0.6f);
            item.DORotate(first, time);
            item.transform.DOMove(firstpoint,time);
        }
    }
    public void HindButon()
    {
        if (runTimeData.hindcont <= 0) return;
        if (runTimeData.findedtextID == -1 && runTimeData.findedımageID == -1) return;//kartların ikisi kapalı
        if (runTimeData.findedtextID != -1 && runTimeData.findedımageID != -1) return;//kartların ikisi açık

        if (runTimeData.findedımageID!=-1) //Image kartı açıksa
        {
            List<Transform> filter = runTimeData.AllObject.FindAll(x => x.GetComponent<MatchingCard_Text>());
           Transform openedcard=filter.Find(x => x.name == runTimeData.findedımageID.ToString());
            openedcard.GetComponent<MatchingCard_Text>().OnClicked();

        }
        else if(runTimeData.findedtextID!=-1)//Text kartı açıksa
        {
            List<Transform> filter = runTimeData.AllObject.FindAll(x => x.GetComponent<MatchingCard_Image>());
            Transform openedcard = filter.Find(x => x.name == runTimeData.findedtextID.ToString());
            openedcard.GetComponent<MatchingCard_Image>().OnClicked();
        }

         runTimeData.hindcont--;
        GameData.instance.uI.game.hinttxt.text =runTimeData.hindcont.ToString();


    }
    public void GameFinished()
    {
     

        Worker.AudioWorker.PlayAudio(GameData.AudioType.win);

        int extrascore = (int)(runTimeData.SuccesRate);
        runTimeData.currentscore += extrascore;
        runTimeData.scorecount += extrascore;
        GameData.instance.uI.game.scoretext.text = runTimeData.scorecount.ToString();
        GameData.instance.uI.game.ExtraScoreText.text = "BONUS :"+extrascore.ToString();
        GameData.instance.uI.game.scoretext.text = runTimeData.scorecount.ToString();

        Debug.Log("bitti");
        StartCoroutine(WebApi.instance.Service(WebApi.SendTimeAndScoreUrl(runTimeData.loginuserID, runTimeData.GameCode, runTimeData.currentscore.ToString(), ((int)runTimeData.GameTime).ToString()), x => { Debug.Log(x); }));
        StartCoroutine(WebApi.instance.Service(WebApi.SendSuccessRateUrl(runTimeData.loginuserID, runTimeData.GameCode, runTimeData.SuccesRate.ToString()), x => { Debug.Log(x); }));

        //StartCoroutine(WebApi.instance.Service(WebApi.UpdateDataUrl(runTimeData.loginuserID, runTimeData.GameCode, ((int)runTimeData.GameTime).ToString(), runTimeData.currentscore.ToString(),runTimeData.SuccesRate.ToString()),x=> { Debug.Log(x); }));
        runTimeData.falsematchcount = 0;
        runTimeData.truematchcount = 0;
        runTimeData.IsGameStarted = false;
        runTimeData.currentscore = 0;
        runTimeData.GameTime = 0;
        GameData.instance.uI.game.Confettiparticle.Play();
        GameData.instance.uI.game.GameFinishedText.gameObject.SetActive(true);
        GameData.instance.uI.game.GameFinishedText.transform.DOScale(GameData.instance.uI.game.GameFinishedText.transform.localScale * 2, 0.15f).OnComplete(() =>
        { GameData.instance.uI.game.GameFinishedText.transform.DOScale(GameData.instance.uI.game.GameFinishedText.transform.localScale / 2, 0.15f); });

        IEnumerator build()
        {
            yield return new WaitForSeconds(4f);
            GameData.instance.uI.game.GameFinishedText.gameObject.SetActive(false);

            CreateCard();
        }
        StartCoroutine(build());
    }
    
    public void TryMatchControl()
    {

        if (runTimeData.findedımageID == -1 || runTimeData.findedtextID == -1)
        {
            return;
        }


        if (runTimeData.findedımageID == runTimeData.findedtextID)
        {
            Debug.Log("Eşletirme Başarılı");
            runTimeData.scorecount += 10;
            runTimeData.currentscore += 10;
            GameData.instance.uI.game.scoretext.text = runTimeData.scorecount.ToString();


            runTimeData.truematchcount++;
            runTimeData.totalgameobjectccount -= 2;
            if(runTimeData.totalgameobjectccount<=0)
             {
               GameFinished();

            }
            Worker.AudioWorker.PlayAudio(GameData.AudioType.trueevent);

           
            runTimeData.IsControl = true;

            foreach (var item in runTimeData.AllObject)
            {
                if (item.name == runTimeData.findedtextID.ToString() || item.name == runTimeData.findedımageID.ToString())
                {
                    StartCoroutine(TrueClickedDelay(item));

                }
            }
            runTimeData.findedtextID = -1;
            runTimeData.findedımageID = -1;
        }
        else
        {
            foreach (var item in runTimeData.AllObject)
            {
                item.DOShakePosition(1.5f);
            }
            runTimeData.falsematchcount++;
            Worker.AudioWorker.PlayAudio(GameData.AudioType.falseevent);
            runTimeData.AllObject.ForEach(x => x.GetComponent<Collider2D>().enabled = false);
            StartCoroutine(FalseClicked());
        }



        IEnumerator TrueClickedDelay(Transform item)
        {
            yield return new WaitForSeconds(0.5f);
            item.gameObject.SetActive(false);
            GameObject created = Instantiate(GameData.instance.uI.game.DestroyCardParticle, item.position, Quaternion.identity);
            Destroy(created, 2f);
        }
    }
    public IEnumerator FalseClicked()
    {

        yield return new WaitForSeconds(2f);
        foreach (var item in runTimeData.AllObject)
        {
            if (item.name != runTimeData.findedtextID.ToString() || item.name != runTimeData.findedımageID.ToString())
            {
                item.GetComponent<MatchingCardBase>().card_front.SetActive(false);
            }
        }
        runTimeData.AllObject.ForEach(x => x.GetComponent<Collider2D>().enabled = true);
        runTimeData.findedtextID = -1;
        runTimeData.findedımageID = -1;
    }
    public void SoundOffOrOn(bool Status)
    {
        AudioListener.volume = Status ? 1 : 0;
        GameData.instance.uI.SoundOffButton.SetActive(Status);
        GameData.instance.uI.SoundOnButton.SetActive(!Status);

    }
    public void Login()
    {
        string username = GameData.instance.uI.menu.loginScreen.usernamefield.text;
        StartCoroutine(WebApi.instance.Service(WebApi.RegisterUrl(username),
        x => 
        {
            //Onresponse
            if (x=="null")
            {
                // hatalı bilgi girisi
                Worker.UI_Worker.UI_Menu.ShowDialog("Invalid Username");
                Debug.Log("girilen bilgiler hatalıdır");
            }
            else if(x.Length>0)
            {
                //giris basarılı
                runTimeData.isloggedin = true;
                runTimeData.loginusername = username;
                runTimeData.loginuserID = x;
                Worker.UI_Worker.UI_Menu.LoginScreenTomenu();
            }
        }, 
        x => 
        {
            //on connection error
            Debug.Log("Something went wrong");
            Worker.UI_Worker.UI_Menu.ShowDialog("Something Went Wrong");
        }));

    }

   
}
[System.Serializable]
public class RunTimeData
    {
        public List<Transform> AllCardPoints;
        public List<Sprite> allımages;
        public List<string> Alltext;
        public List<Transform> AllObject;
        public int findedtextID;
        public  int findedımageID;
        public bool IsControl = false;
        public int scorecount;
        public int currentscore;
        public int hindcont = 2;
       public List<Sprite> Backgorunds;
       public string loginusername;
       public string loginuserID;
       public bool isloggedin;
       public bool IsGameStarted = false;
       public int totalgameobjectccount;
      public float GameTime;
      public string GameCode;
      public int falsematchcount;
    public int truematchcount; 

    public float SuccesRate
    {
       get
        {
            float total = (truematchcount + falsematchcount);
            if (truematchcount <= 0) return 0;
            return ((float)truematchcount/total)*100;
        }
    }
    

        
}

public class deta
{
    public string x;
    public deta()
    {
        x = "10";
    }
    ~deta()
    {
        Debug.Log("Hello");
    }
}