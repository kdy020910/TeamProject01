using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;
    public GameObject[] questobject;

    Dictionary<int, QuestData> questList;

    // Start is called before the first frame update
    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    // Update is called once per frame
    void GenerateData()
    {
        questList.Add(10, new QuestData("�ð� ���", new int[] { 1000, 2000 }));
        questList.Add(20, new QuestData("���� ���", new int[] { 5000, 2000 }));
        questList.Add(30, new QuestData("����Ʈ ��!", new int[] { 0 }));/*
        questList.Add(40, new QuestData("����Ʈ ��!", new int[] { 0 }));*/
    }
    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    //�̸��� ������ �ٸ��Լ� �Ű����� ������ �����Լ� ȣ�� ������ �ؿ� �Լ� ȣ��, �����ε��̴�
    public string CheckQuest(int id)
    {

        if (id == questList[questId].npcId[questActionIndex])
        { 
            questActionIndex++;
        }
        
        //����Ʈ ������Ʈ ����
        ControlObject();

        if (questActionIndex == questList[questId].npcId.Length)
        {
            NextQuest();
        }
        return questList[questId].questName;
    }

    public string CheckQuest()
    {
        //����Ʈ �̸�
        return questList[questId].questName;
    }

    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }

    void ControlObject()
    {
        switch(questId)
        {
            case 10:
                if(questActionIndex == 2)
                {
                    questobject[0].SetActive(true);
                }
                break;
            case 20:
                if (questActionIndex == 1)
                {
                    questobject[0].SetActive(false);
                    questobject[1].SetActive(true);
                }
                break;
        }
    }
}