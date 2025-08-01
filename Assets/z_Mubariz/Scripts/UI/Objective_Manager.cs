using UnityEngine;

public class Objective_Manager : MonoBehaviour
{
    [SerializeField] GameObject grannyBat;
    [SerializeField] AllGameObjsActive diamondAll;
    [SerializeField] AllGameObjsActive keysAll;
    [SerializeField] GameObject granny;
    [SerializeField] Transform GrannyPosition;
    [SerializeField] EnemyWandering enemyWandering;
    [SerializeField] Transform playerCamera;
    [SerializeField] ObjectThrower objectThrower;
    
    [Space(10f)]
    [SerializeField] Transform actualPlayer;


    [SerializeField] GameObject[] objectivesGameObjects;

    [SerializeField] FirstObjective firstObjective;
    [SerializeField] SecondObjective secondObjective;
    [SerializeField] ThirdObjective thirdObjective;
    [SerializeField] BaloonsObjectives fourthObjective;
    [SerializeField] KeysObjective fifthObjective;
    [SerializeField] CatSleepObjective sixthObjective;
    [SerializeField] ToysObjective seventhObjective;
    [SerializeField] FootballObjective eighthObjective;
    [SerializeField] GlassesObjectives ninthObjective;
    [SerializeField] FruitsObjective tenthObjective;
    [SerializeField] PotsObjective eleventhObjective;
    [SerializeField] AudioClip levelCompleteAudio;

    [Header("Cat Positions in missions")]
    public Vector3 catPosIn_FirstMission;
    public float bodyRot1;
    public Vector3 catPosIn_SecondMission;
    public float bodyRot2;
    public Vector3 catPosIn_ThirdMission;
    public float rotation3;
    public Vector3 catPosIn_FourthMission;
    public float rotation4;
    public Vector3 catPosIn_FifthMission;
    public float rotation5;
    public Vector3 catPosIn_SixthMission;
    public float rotation6;
    public Vector3 catPosIn_SeventhMission;
    public float rotation7;
    public Vector3 catPosIn_EightMission;
    public float rotation8;
    public Vector3 catPosIn_NinthMission;
    public float rotation9;
    public Vector3 catPosIn_TenthtMission;
    public float rotation10;
    public Vector3 catPosIn_EleventhMission;
    public float rotation11;

    [Header("Granny Pos and Rot")]
    public Vector3 granPosInFirstMission;
    public float rotGran1;
    public Vector3 granPosInSecondMission;
    public float rotGran2;
    private void OnEnable()
    {
        Invoke(nameof(NextObjective), 0.1f);
    }

    void ChangePlayerPositionTo(Vector3 targerPos, float bodyRot)
    {
        actualPlayer.gameObject.SetActive(false);
        actualPlayer.transform.localPosition = targerPos;
        actualPlayer.localRotation = Quaternion.Euler(0f, bodyRot, 0f); 
        playerCamera.localRotation = Quaternion.Euler(0f, 0f, 0f); 
        actualPlayer.gameObject.SetActive(true);
    }

    void ChangeGrannyPositionTo(Vector3 pos, float RotY)
    {
        granny.SetActive(false);
        GrannyPosition.localPosition = pos;
        GrannyPosition.localRotation = Quaternion.Euler(0, RotY, 0);
        granny.SetActive(true);
    }




    private void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("LevelFail at " + CurrentLevel());
        }
    }
    public void NextObjective()
    {
        //FIRST OBJECTIVE
        if (PlayerPrefs.GetInt("L1") != 1) //0
        {
            firstObjective.hitToGranny = 0;
            objectivesGameObjects[0].SetActive(true);
            firstObjective.ChangeToNewAnimator();
            ChangePlayerPositionTo(catPosIn_FirstMission,bodyRot1);
            ChangeGrannyPositionTo(granPosInFirstMission,rotGran1);

        }
        //SECOND OBJECTIVE
        else if (PlayerPrefs.GetInt("L2") != 1) //0
        {
            secondObjective.objectsToCollect = 0;
            secondObjective.ChangeAnimatorToKitchen();
            objectivesGameObjects[1].SetActive(true);
            diamondAll.EnableAll();
            ChangePlayerPositionTo(catPosIn_SecondMission, bodyRot2);
            ChangeGrannyPositionTo(granPosInSecondMission, rotGran2);
        }
        //THIRD OBJECTIVE        
        else if (PlayerPrefs.GetInt("L3") != 1) //0
        {
            thirdObjective.hitToGranny = 0;
            objectivesGameObjects[2].SetActive(true);
            ChangePlayerPositionTo(catPosIn_FirstMission, bodyRot1);
            ChangeGrannyPositionTo(granPosInFirstMission, rotGran1);

        }
        //FOURTH OBJECTIVE        
        else if (PlayerPrefs.GetInt("L4") != 1) //0
        {
            fourthObjective.BaloonsPoped = 0;
            objectivesGameObjects[3].SetActive(true);
            ChangePlayerPositionTo(catPosIn_FourthMission,rotation4);


        }
        //FIFTH OBJECTIVE
        else if (PlayerPrefs.GetInt("L5") != 1)
        {
            objectivesGameObjects[4].SetActive(true);
            fifthObjective.keysCount = 0;
            keysAll.EnableAll();
            ChangePlayerPositionTo(catPosIn_FifthMission,rotation5);


        }
        //SIXTH OBJECTIVE
        else if (PlayerPrefs.GetInt("L6") != 1)
        {
            objectivesGameObjects[5].SetActive(true);
            ChangePlayerPositionTo(catPosIn_SixthMission,rotation6);


        }
        //SEVENTH OBJECTIVE
        else if (PlayerPrefs.GetInt("L7") != 1)
        {
            objectivesGameObjects[6].SetActive(true);
            seventhObjective.toysThrown = 0;
            ChangePlayerPositionTo(catPosIn_SeventhMission,rotation7);


        }
        //EIGHT OBJECTIVE
        else if (PlayerPrefs.GetInt("L8") != 1)
        {
            objectivesGameObjects[7].SetActive(true);
            eighthObjective.footballCount = 0;
            ChangePlayerPositionTo(catPosIn_EightMission,rotation8);


        }
        //NINTH OBJECTIVE        
        else if (PlayerPrefs.GetInt("L9") != 1)
        {
            objectivesGameObjects[8].SetActive(true);
            ninthObjective.glassBroken = 0;
            ChangePlayerPositionTo(catPosIn_NinthMission, rotation9);


        }
        //TENTH OBJECTIVE
        else if (PlayerPrefs.GetInt("L10") != 1)
        {
            objectivesGameObjects[9].SetActive(true);
            tenthObjective.eatablesThrown = 0;
            ChangePlayerPositionTo(catPosIn_TenthtMission,rotation10);


        }
        //ELEVENTH OBJECTIVE
        else if (PlayerPrefs.GetInt("L11") != 1)
        {
            objectivesGameObjects[10].SetActive(true);
            eleventhObjective.potsThrown = 0;
            ChangePlayerPositionTo(catPosIn_EleventhMission,rotation11);


        }
        else
        {
            PlayerPrefs.SetInt("L1", 0);
            PlayerPrefs.SetInt("L2", 0);
            PlayerPrefs.SetInt("L3", 0);
            PlayerPrefs.SetInt("L4", 0);
            PlayerPrefs.SetInt("L5", 0);
            PlayerPrefs.SetInt("L6", 0);
            PlayerPrefs.SetInt("L7", 0);
            PlayerPrefs.SetInt("L8", 0);
            PlayerPrefs.SetInt("L9", 0);
            PlayerPrefs.SetInt("L10", 0);
            PlayerPrefs.SetInt("L11", 0);
            NextObjective();
        }
        SFX_Manager.PlaySound(levelCompleteAudio);
        objectThrower.ResetState();
        //grannyBat.SetActive(false);
        EnemyHandler.Instance.SetRotationConstrainToZero();
    }


    
    int GetActiveChildIndex(GameObject parent)
    {
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            Transform child = parent.transform.GetChild(i);

            if (child.gameObject.activeInHierarchy)
            {
                return i; // Return index of the active child
            }
        }

        return -1; // No active child found
    }

    public int CurrentLevel()
    {
        int currentlevel = GetActiveChildIndex(gameObject) + 1;
        return currentlevel;
    }



}


