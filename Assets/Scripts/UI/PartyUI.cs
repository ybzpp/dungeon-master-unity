using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace DungeonMaster
{
    public class PartyUI : ElementUI
    {
        [SerializeField] private Image[] _partyImages;

        public void UpdatePartyImages(List<Boy> boys)
        {
            for (int i = 0; i < _partyImages.Length; i++)
            {
                if (_partyImages[i] != null)
                {
                    if (i < boys.Count)
                    {
                        _partyImages[i].sprite = boys[i].Data.Ico;
                        _partyImages[i].enabled = true;
                    }
                    else
                    {
                        _partyImages[i].enabled = false;
                    }
                }
            }
        }
    }
}
