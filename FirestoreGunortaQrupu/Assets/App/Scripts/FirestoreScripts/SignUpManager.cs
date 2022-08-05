using UnityEngine;
using DynamicBox.EventManagement;
using DynamicBox.UIEvents;
using Firebase.Firestore;
using Firebase.Extensions;

public class SignUpManager : MonoBehaviour
{
	private FirebaseFirestore firestoreInstance;


	private void Start()
	{
		firestoreInstance = FirebaseFirestore.DefaultInstance;
	}

	private void OnEnable()
	{
		EventManager.Instance.AddListener<OnSignUpButtonPressedEvent>(OnSignUpButtonPressedEventHandler);
	}

	private void OnDisable()
	{
		EventManager.Instance.RemoveListener<OnSignUpButtonPressedEvent>(OnSignUpButtonPressedEventHandler);

	}

	private void OnSignUpButtonPressedEventHandler(OnSignUpButtonPressedEvent eventDetails)
	{
		Query playerDataQuery = firestoreInstance.Collection("PlayerLoginData").
			WhereEqualTo("UserNickName", eventDetails.PlayerData.UserNickName);


		playerDataQuery.GetSnapshotAsync().ContinueWithOnMainThread(task=>
		{
			if (task.Result.Count > 0)
			{
				EventManager.Instance.Raise(new OnSignUpFailedEvent());
				return;
			}


			DocumentReference playerDocRef =
			firestoreInstance.Collection("PlayerLoginData").
			Document(eventDetails.PlayerData.UserNickName);

			playerDocRef.SetAsync(eventDetails.PlayerData);

			EventManager.Instance.Raise(new OnSignUpSuccessfulEvent());

		}
		);


		
	}
}
