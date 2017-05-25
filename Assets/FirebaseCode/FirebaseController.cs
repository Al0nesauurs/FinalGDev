using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.UI;

public class FirebaseController : MonoBehaviour
{
	private DatabaseReference reference;
	public InputField _Email;
	public InputField _Password;
    string[] id = new string[1000];
    int[] score = new int[1000];
    int iter = 0;
	public Text Name0, Name1, Name2;
	public Text Score0, Score1, Score2;

	void Start()
	{
		// ใช้สำหรับอ้างอิง Firebase Project
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://forest-e5661.firebaseio.com/");

		// สำหรับใช้ในการอ้างอิง Firebase
		reference = FirebaseDatabase.DefaultInstance.RootReference;
        for(int i=0; i<score.Length;i++)
        {
            score[i] = -1;
        }
	}

	public void WriteToniData()
	{
		// ทำการเขียนเขียนข้อมูลว่างๆ เพื่อนำ Key มาอ้างอิง และทำการ Update ข้อมูล
		string key = reference.Child("toni-test").Push().Key;
		Dictionary<string, Object> childUpdates = new Dictionary<string, Object>();
		// เขียนข้อมูลลง Model
		HunterData tData = new HunterData();
		tData.body = MScoreManager.Score;
		tData.uid = _Email.text;
		//tData.body = _Password.text;
		//tData.uid = _Email.text;
		string json = JsonUtility.ToJson(tData);
		// เขียนข้อมูลลง Firebase
		reference.Child("toni-test").Child(key).SetRawJsonValueAsync(json);

	}

	public void RaadAllData()
	{
		FirebaseDatabase.DefaultInstance.GetReference("toni-test")
		// หากข้อมูลมีการเปลี่ยนแหลงให้ทำการอ่านและแสดง
			.ValueChanged += HandleValueChanged;
	}

	private void HandleValueChanged(object sender, ValueChangedEventArgs args)
	{
		if (args.DatabaseError != null)
		{
			Debug.LogError(args.DatabaseError.Message);
			return;
		}
		// อ่าน Key เพื่อใช้แสดงผล
		List<string> keys = args.Snapshot.Children.Select(s => s.Key).ToList();
		foreach (var key in keys)
		{
			DisplayData(args.Snapshot, key);
		}

        Bubblesort();
	}
	// ใช้สำหรับ แสดงข้อมูลที่โหลดครับ
	void DisplayData(DataSnapshot snapshot, string key)
	{
		string j = snapshot.Child(key).GetRawJsonValue();
		HunterData u = JsonUtility.FromJson<HunterData>(j);
		//Debug.Log(u.uid + " " + u.body);
        id[iter] = u.uid;
        score[iter] = u.body;
        iter++;
    }
    void Bubblesort()
    {

        string strtemp = "";
        int temp = 0;

        for (int write = 0; write < score.Length; write++)
        {
            for (int sort = 0; sort < score.Length - 1; sort++)
            {
                if (score[sort] < score[sort + 1])
                {
                    temp = score[sort + 1];
                    score[sort + 1] = score[sort];
                    score[sort] = temp;
                    strtemp = id[sort + 1];
                    id[sort + 1] = id[sort];
                    id[sort] = strtemp;
                }
            }
        }
        //ปริ้นค่า
        for (int i = 0; i < score.Length; i++)
        {
            if(score[i]!=-1)
            {
                Debug.Log("Name= "+id[i]+" score = "+score[i]);
            }
			if (i == 0) {
				Name0.text = id [i];
				Score0.text = score [i].ToString();
			}
			if (i == 1) {
				Name1.text = id [i];
				Score1.text = score [i].ToString();
			}
			if (i == 2) {
				Name2.text = id [i];
				Score2.text = score [i].ToString();
			}
        }
    }
}