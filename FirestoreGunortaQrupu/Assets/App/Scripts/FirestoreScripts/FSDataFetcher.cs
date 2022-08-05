using TMPro;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Collections.Generic;

public class FSDataFetcher : MonoBehaviour
{
  [SerializeField] private TMP_Text userNameText;

  [SerializeField] private TMP_Text userNickNameText;

  private FirebaseFirestore firestoreInstance;


	private void Start()
	{
		firestoreInstance = FirebaseFirestore.DefaultInstance;

		SetDataOnUI();
	}

	private void SetDataOnUI()
	{
		DocumentReference playerDataRef = 
			firestoreInstance.Collection("PlayerLoginData").Document("VaqifQuluzada");

		playerDataRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
		{
			if (task.Result.Exists)
			{
				FirestorePlayerData playerData = task.Result.ConvertTo<FirestorePlayerData>();

				userNameText.text = playerData.UserName;

				userNickNameText.text = playerData.UserNickName;

			}
			else
			{
				Debug.LogWarning("No data found");
			}
		}
		);
	}
}
