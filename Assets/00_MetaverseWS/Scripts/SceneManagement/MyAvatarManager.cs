using UnityEngine;
using Normal.Realtime;
using UnityEngine.SceneManagement;

public class MyAvatarManager : MonoBehaviour {
    private RealtimeAvatarManager _manager;

    [SerializeField] string currentAvatarName;

    private void Awake() {
        _manager = GetComponent<RealtimeAvatarManager>();
        _manager.avatarCreated += AvatarCreated;
        _manager.avatarDestroyed += AvatarDestroyed;
    }

    private void AvatarCreated(RealtimeAvatarManager avatarManager, RealtimeAvatar avatar, bool isLocalAvatar) {
        
        if(isLocalAvatar)
        {
            currentAvatarName = avatar.name;
        }

        
        
    }

    private void AvatarDestroyed(RealtimeAvatarManager avatarManager, RealtimeAvatar avatar, bool isLocalAvatar) {
        // Avatar destroyed!
    }
}