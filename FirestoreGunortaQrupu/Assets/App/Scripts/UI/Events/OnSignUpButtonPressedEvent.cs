using DynamicBox.EventManagement;

namespace DynamicBox.UIEvents
{
	public class OnSignUpButtonPressedEvent:GameEvent
	{
		public FirestorePlayerData PlayerData;

		public OnSignUpButtonPressedEvent(FirestorePlayerData _playerData)
		{
			PlayerData = _playerData;
		}
	}
}

