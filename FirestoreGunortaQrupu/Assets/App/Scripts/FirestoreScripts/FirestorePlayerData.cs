using Firebase.Firestore;
using System.Collections.Generic;

[FirestoreData]
public class FirestorePlayerData
{
	[FirestoreProperty]
	public string UserName { get; set; }

	[FirestoreProperty]
	public string UserNickName { get; set; }

	[FirestoreProperty]
	public string UserPass { get; set; }
}
