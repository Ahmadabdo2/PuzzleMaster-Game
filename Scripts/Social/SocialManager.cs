using UnityEngine;
using System.Collections.Generic;
using Firebase.Database;

public class SocialManager : MonoBehaviour
{
    public static SocialManager Instance { get; private set; }

    [System.Serializable]
    public class Friend
    {
        public string friendId;
        public string friendName;
        public int friendLevel;
        public int friendStars;
    }

    private List<Friend> friendsList = new List<Friend>();
    private FirebaseDatabase firebaseDB;
    private string currentUserId;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        firebaseDB = FirebaseDatabase.DefaultInstance;
        currentUserId = PlayerPrefs.GetString("UserID");
    }

    public void AddFriend(string friendId, string friendName)
    {
        var friend = new Friend
        {
            friendId = friendId,
            friendName = friendName,
            friendLevel = 1,
            friendStars = 0
        };

        friendsList.Add(friend);
        
        // Save to Firebase
        string json = JsonUtility.ToJson(friend);
        firebaseDB.GetReference($"users/{currentUserId}/friends/{friendId}").SetRawJsonValueAsync(json);
        
        Debug.Log($"Added friend: {friendName}");
    }

    public void RemoveFriend(string friendId)
    {
        var friend = friendsList.Find(x => x.friendId == friendId);
        if (friend != null)
        {
            friendsList.Remove(friend);
            firebaseDB.GetReference($"users/{currentUserId}/friends/{friendId}").RemoveValueAsync();
            Debug.Log($"Removed friend: {friend.friendName}");
        }
    }

    public void ShareAchievement(string achievement)
    {
        var shareData = new { userId = currentUserId, achievement = achievement, timestamp = System.DateTime.Now };
        string json = JsonUtility.ToJson(shareData);
        firebaseDB.GetReference($"social/shares/{System.Guid.NewGuid()}").SetRawJsonValueAsync(json);
        Debug.Log($"Shared achievement: {achievement}");
    }

    public void InviteFriend(string friendUserId)
    {
        var invitation = new { from = currentUserId, to = friendUserId, timestamp = System.DateTime.Now };
        string json = JsonUtility.ToJson(invitation);
        firebaseDB.GetReference($"invitations/{System.Guid.NewGuid()}").SetRawJsonValueAsync(json);
        Debug.Log($"Sent invitation to: {friendUserId}");
    }

    public void ChallengePlayer(string friendId, int wagerCoins = 100)
    {
        var challenge = new { challenger = currentUserId, opponent = friendId, wager = wagerCoins, status = "pending" };
        string json = JsonUtility.ToJson(challenge);
        firebaseDB.GetReference($"challenges/{System.Guid.NewGuid()}").SetRawJsonValueAsync(json);
        Debug.Log($"Challenged player: {friendId} for {wagerCoins} coins");
    }

    public List<Friend> GetFriendsList() => friendsList;
}
