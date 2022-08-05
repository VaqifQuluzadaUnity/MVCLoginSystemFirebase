using UnityEngine;
using DynamicBox.UIControllers;
using TMPro;
using UnityEngine.UI;
using System.Collections;

namespace DynamicBox.UIViews
{
	public class SignUpMenuView : MonoBehaviour
	{
		[Header("Controller reference")]
		[SerializeField] private SignUpMenuController controller;

		[Header("View elements")]
		[SerializeField] private TMP_InputField userNameInputField;

		[SerializeField] private TMP_InputField userNickNameInputField;

		[SerializeField] private TMP_InputField passwordInputField;

		[SerializeField] private TMP_InputField confirmPassInputField;

		[SerializeField] private Button signUpButton;

		[SerializeField] private TMP_Text notificationText;

		public void OnInputFieldChanged()
		{
			if (string.IsNullOrEmpty(userNameInputField.text) ||
				string.IsNullOrEmpty(userNickNameInputField.text) ||
				string.IsNullOrEmpty(passwordInputField.text) ||
				string.IsNullOrEmpty(confirmPassInputField.text))
			{
				signUpButton.interactable = false;
				return;
			}

			signUpButton.interactable = true;

		}

		public void OnSignUpButtonPressed()
		{
			if (passwordInputField.text != confirmPassInputField.text)
			{
				StartCoroutine(ShowNotification("Password doesen't match", NotificationType.WARNING));
				return;
			}

			FirestorePlayerData playerData = new FirestorePlayerData
			{
				UserName = userNameInputField.text,
				UserNickName = userNickNameInputField.text,
				UserPass = passwordInputField.text
			};


			controller.OnSignUpButtonPressed(playerData);
		}


		public IEnumerator ShowNotification(string notifText,NotificationType notifType)
		{
			switch (notifType)
			{
				case NotificationType.SUCCESS:
					notificationText.color = Color.green;
					break;

				
				case NotificationType.WARNING:
					notificationText.color = Color.yellow;
					break;
				case NotificationType.FAILED:
					notificationText.color = Color.red;
					break;
			}

			notificationText.text = notifText;

			notificationText.gameObject.SetActive(true);

			yield return new WaitForSeconds(1f);

			notificationText.gameObject.SetActive(false);

		}
	}

}

public enum NotificationType {NONE,SUCCESS,WARNING,FAILED }