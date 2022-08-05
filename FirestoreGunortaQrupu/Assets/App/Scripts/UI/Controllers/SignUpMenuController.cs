using UnityEngine;
using DynamicBox.UIViews;
using DynamicBox.EventManagement;
using DynamicBox.UIEvents;

namespace DynamicBox.UIControllers
{
  public class SignUpMenuController : MonoBehaviour
  {
    [SerializeField] private SignUpMenuView view;


		private void OnEnable()
		{
			EventManager.Instance.AddListener<OnSignUpSuccessfulEvent>(OnSignUpSuccessfulEventHandler);

			EventManager.Instance.AddListener<OnSignUpFailedEvent>(OnSignUpFailedEventHandler);
		}

		private void OnDisable()
		{
			EventManager.Instance.RemoveListener<OnSignUpSuccessfulEvent>(OnSignUpSuccessfulEventHandler);

			EventManager.Instance.RemoveListener<OnSignUpFailedEvent>(OnSignUpFailedEventHandler);
		}


		#region Event Handlers

		private void OnSignUpSuccessfulEventHandler(OnSignUpSuccessfulEvent eventDetails)
		{
			StartCoroutine(view.ShowNotification("Logged in successfully",NotificationType.SUCCESS));
		}

		private void OnSignUpFailedEventHandler(OnSignUpFailedEvent eventDetails)
		{
			StartCoroutine(view.ShowNotification("Login failed, same username", NotificationType.FAILED));

		}

		#endregion


		public void OnSignUpButtonPressed(FirestorePlayerData playerData)
		{
      EventManager.Instance.Raise(new OnSignUpButtonPressedEvent(playerData));
		}
  }
}