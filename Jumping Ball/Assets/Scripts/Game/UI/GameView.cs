using System.Collections;
using Data;
using Game.UI.Swipes;
using UI.Base;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class GameView : MonoBehaviour
    {
        public SwipeDetector SwipeDetector;
        
        [SerializeField] private AnimatedWindow _victoryWindow;
        [SerializeField] private AnimatedWindow _loseWindow;
        
        private GameSettings _gameSettings;

        [Inject]
        public void Construct(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }

        public void ShowLoseWindow()
        {
            StartCoroutine(ShowWindowWithDelay(_loseWindow, _gameSettings.ShowLoseWindowDelay));
        }
        
        public void ShowVictoryWindow()
        {
            StartCoroutine(ShowWindowWithDelay(_victoryWindow, _gameSettings.ShowVictoryWindowDelay));
        }

        private IEnumerator ShowWindowWithDelay(AnimatedWindow window, float delay)
        {
            yield return new WaitForSeconds(delay);
            
            window.Open();
        }
    }
}