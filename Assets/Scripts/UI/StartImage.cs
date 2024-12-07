using UnityEngine;
using UnityEngine.EventSystems;

namespace DungeonMaster
{
    public class StartImage : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            GameHelper.ChangeGameState(GameState.Lobby);
        }
    }
}
