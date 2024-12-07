using System;
using System.Collections.Generic;

namespace DungeonMaster
{
    public class SignalBus
    {
        public Action<int> OnChangeRerollPrice;
        public Action OnChangeUnlockPrice;
        public Action OnSkillComplete;
        public Action OnStartTurn;
        public Action OnEndTurn;
        public Action OnNext;
    }
}