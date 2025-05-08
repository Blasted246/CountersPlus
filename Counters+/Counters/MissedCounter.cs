﻿using CountersPlus.ConfigModels;
using CountersPlus.Counters.Interfaces;
using TMPro;

namespace CountersPlus.Counters
{
    internal class MissedCounter : Counter<MissedConfigModel>, INoteEventHandler
    {
        private int notesMissed = 0;
        private TMP_Text counter;

        public override void CounterInit()
        {
            GenerateBasicText("Misses", out counter);

            if (Settings.HideWhenNone)
                counter.gameObject.SetActive(false);
        }

        public void OnNoteCut(NoteData data, NoteCutInfo info)
        {
            if (Settings.CountBadCuts && !info.allIsOK && data.colorType != ColorType.None)
            {
                UpdateMissCount();
            }
        }

        public void OnNoteMiss(NoteData data)
        {
            if (data.colorType != ColorType.None && data.gameplayType != NoteData.GameplayType.BurstSliderElement)
            {
                UpdateMissCount();
            }
        }

        private void UpdateMissCount()
        {
            notesMissed++;
            counter.text = notesMissed.ToString();

            if (Settings.HideWhenNone && !counter.gameObject.activeSelf)
            {
                counter.gameObject.SetActive(true);
            }
        }
    }
}
