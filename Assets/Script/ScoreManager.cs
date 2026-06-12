    using UnityEngine;
    using TMPro;

    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager instance;

        public TextMeshProUGUI scoreText;
        public int score = 0;

        public enum GamePhase { Phase1, Phase2, Phase3 }
        public GamePhase currentPhase = GamePhase.Phase1;

        public event System.Action<GamePhase> OnPhaseChanged;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
            {
                Destroy(gameObject);
                return;
            }

            ResetScore();
        }

        private void Start() => ResetScore();

        public void AddScore(int points)
        {
            score += points;
            UpdateScoreUI();
            CheckPhase();
        }

        public void ResetScore()
        {
            score = 0;
            currentPhase = GamePhase.Phase1;
            UpdateScoreUI();
            OnPhaseChanged?.Invoke(currentPhase);
        }

        private void UpdateScoreUI()
        {
            if (scoreText != null)
                scoreText.text = score.ToString("0");
        }

        private void CheckPhase()
        {
            GamePhase previousPhase = currentPhase;

            if (score >= 300) // VICTORY
            {
                if (currentPhase != GamePhase.Phase3)
                {
                    currentPhase = GamePhase.Phase3;
                    OnPhaseChanged?.Invoke(currentPhase);
                }

                UIManager ui = FindFirstObjectByType<UIManager>();
                if (ui != null) ui.ShowVictory();
                return;
            }
            else if (score >= 200 && currentPhase != GamePhase.Phase3) // PHASE 3 - Enemy 3
            {
                currentPhase = GamePhase.Phase3;
                Debug.Log("=== PHASE 3 START - ENEMY 3 (BOSS) MASUK! ===");
                OnPhaseChanged?.Invoke(currentPhase);
            }
            else if (score >= 100 && currentPhase == GamePhase.Phase1) // PHASE 2 - Enemy 2
            {
                currentPhase = GamePhase.Phase2;
                Debug.Log("=== PHASE 2 START - ENEMY 2 MASUK! ===");
                OnPhaseChanged?.Invoke(currentPhase);
            }
        }
    }