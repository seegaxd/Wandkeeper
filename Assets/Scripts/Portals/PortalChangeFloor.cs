using UnityEngine.SceneManagement;

public class PortalChangeFloor : Portal
{
    public override void TeleportTo()
    {
        SceneManager.LoadScene("Game");
    }
}
