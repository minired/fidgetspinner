using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi.SavedGame;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Fidget.Player;
namespace Fidget.Common
{
    public class GooglePlay : MonoBehaviour
    {
        GameData slot0;
        bool isSaving;

        GooglePlayAchievement achievement = new GooglePlayAchievement();
        static bool isTryLogin = false;

        public string[] levelAchievementCode = { "CgkIyIDh6tIfEAIQCg", "CgkIyIDh6tIfEAIQCw", "CgkIyIDh6tIfEAIQDA", "CgkIyIDh6tIfEAIQDQ", "CgkIyIDh6tIfEAIQDg", "CgkIyIDh6tIfEAIQDw",
        "CgkIyIDh6tIfEAIQEA", "CgkIyIDh6tIfEAIQEQ", "CgkIyIDh6tIfEAIQEg", "CgkIyIDh6tIfEAIQEw", "CgkIyIDh6tIfEAIQFA", "CgkIyIDh6tIfEAIQFQ"  };

        public int[] levelAchivementLevel = { 10, 50, 100, 200, 300, 400, 500, 600, 700, 800, 900, 999 };


        public string[] coinAchievementCode = { "CgkIyIDh6tIfEAIQAQ", "CgkIyIDh6tIfEAIQAw", "CgkIyIDh6tIfEAIQBQ", "CgkIyIDh6tIfEAIQFg", "CgkIyIDh6tIfEAIQCQ", "CgkIyIDh6tIfEAIQFw" };

        public ulong[] coinAchivementAmount = { 10000, 100000, 1000000, 5000000,  10000000, 50000000 };


        public string[] timedSpinSpeedAchievementCode = { "CgkIyIDh6tIfEAIQGA", "CgkIyIDh6tIfEAIQGQ", "CgkIyIDh6tIfEAIQGg", "CgkIyIDh6tIfEAIQGw", "CgkIyIDh6tIfEAIQHA", "CgkIyIDh6tIfEAIQHQ" };

        public float[] timedSpinSpeedAchivementSpeed = { 100f, 200f, 300f, 400f, 500f, 600f };


        void Awake()
        {
        }


        public void Init()
        {
            if (GameInfo.googlePlayInit)
                return;

#if UNITY_ANDROID 

            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);

            // recommended for debugging:
            PlayGamesPlatform.DebugLogEnabled = false;

            //Activate the Google Play Games platform
            PlayGamesPlatform.Activate();
#endif
            GameInfo.googlePlayInit = true;
        }

        // Use this for initialization
        void Start()
        {
            
        }

        public void LoginWithInit()
        {
            if (!GameInfo.IsIOS)
            {
                Init();
                Login();
            }
        }

        public void UpdateCheckAchievements()
        {
            if (Social.localUser.authenticated)
                CheckAllAchievemet();
        }




        void Login()
        {
            if (Social.localUser.authenticated)
                return;
            if (isTryLogin)
                return;
            isTryLogin = true;
            Social.localUser.Authenticate((bool success) =>
            {
                // handle success or failure
                if (success)
                {
                    User.Instance.GoogleId = Social.localUser.id;
                }
                else
                {
                }
                //if (success && !GameInfo.IsIOS)
                //    CheckAllAchievemet();
            });
        }


        public void ShowAchievements()
        {
            if (!Social.localUser.authenticated)
                return;
            Social.ShowAchievementsUI();
        }



        void CheckMapAchievement()
        {
            bool[] array = PlayMission.Instance.GetMapMission();
            for (int i = 0; i < array.Length; ++i)
            {
                if (array[i] == true)
                {
                    Social.ReportProgress(achievement.GetMapCode(i), 100.0f, (bool success) =>
                    {
                        // handle success or failure
                        if (success)
                        {
                            PlayMission.Instance.SetMapMission(i, true);
                            PlayMission.Instance.SetMapMissionSuccess(i, true);
                        }
                    });
                }
            }
        }


        void CheckCharacterAchievement()
        {
            bool[] array = PlayMission.Instance.GetCharacterMission();
            for (int i = 0; i < array.Length; ++i)
            {
                if (array[i] == true)
                {
                    Social.ReportProgress(achievement.GetCharacterCode(i), 100.0f, (bool success) =>
                    {
                        // handle success or failure
                        if (success)
                        {
                            PlayMission.Instance.SetCharacterMission(i, true);
                            PlayMission.Instance.SetCharacterMissionSuccess(i, true);
                        }
                    });
                }
            }
        }


        void CheckCoinAchievement()
        {
            bool[] array = PlayMission.Instance.GetCoinMission();
            for (int i = 0; i < array.Length; ++i)
            {
                if (array[i] == true)
                {
                    Social.ReportProgress(achievement.GetCoinCode(i), 100.0f, (bool success) =>
                    {
                        // handle success or failure
                        if (success)
                        {
                            PlayMission.Instance.SetCoinMission(i, true);
                            PlayMission.Instance.SetCoinMissionSuccess(i, true);
                        }
                    });
                }
            }
        }


        void CheckLevelAchievement()
        {
            bool[] array = PlayMission.Instance.GetLevelMission();
            for (int i = 0; i < array.Length; ++i)
            {
                if (array[i] == true)
                {
                    Social.ReportProgress(achievement.GetLevelCode(i), 100.0f, (bool success) =>
                    {
                        // handle success or failure
                        if (success)
                        {
                            PlayMission.Instance.SetLevelMission(i, true);
                            PlayMission.Instance.SetLevelMissionSuccess(i, true);
                        }
                    });
                }
            }
        }

        void CheckStageFloorAchievement()
        {
            bool[] array = PlayMission.Instance.GetStageFloorMission();
            for (int i = 0; i < array.Length; ++i)
            {
                if (array[i] == true)
                {
                    Social.ReportProgress(achievement.GetStageFloorCode(i), 100.0f, (bool success) =>
                    {
                        // handle success or failure
                        if (success)
                        {
                            PlayMission.Instance.SetStageFloorMission(i, true);
                            PlayMission.Instance.SetStageFloorMissionSuccess(i, true);
                        }
                    });
                }
            }
        }


        void CheckReviveAchievement()
        {
            bool[] array = PlayMission.Instance.GetReviveMission();
            for (int i = 0; i < array.Length; ++i)
            {
                if (array[i] == true)
                {
                    Social.ReportProgress(achievement.GetReviveCode(i), 100.0f, (bool success) =>
                    {
                        // handle success or failure
                        if (success)
                        {
                            PlayMission.Instance.SetReviveMission(i, true);
                            PlayMission.Instance.SetReviveMissionSuccess(i, true);
                        }
                    });
                }
            }
        }


        void CheckAllAchievemet()
        {
            if (GameInfo.IsIOS)
                return;
            CheckAllLevelMission(User.Instance.LevelMissionGrade);
            CheckAllCoinMission(User.Instance.CoinMissionGrade);
            CheckAllSpeedMission(User.Instance.TimedSpeedMissionGrade);
        }

        bool IsUpperLevel(int level)
        {
            ExpTable exptable = new ExpTable();
            int cur = exptable.GetLevel(User.Instance.Exp);

            if(cur >= level)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        void CheckLevelMission(int level, string missionCode)
        {
            if (IsUpperLevel(level))
            {
                Social.ReportProgress(missionCode, 100.0f, (bool success) =>
                {
                    // handle success or failure
                    if (success)
                    {
                        User.Instance.LevelMissionGrade++;
                    }
                });
            }
        }


        void CheckAllLevelMission(int grade)
        {
            for(int i=0; i < levelAchievementCode.Length; ++i)
            {
                if (grade == i)
                {
                    CheckLevelMission(levelAchivementLevel[i], levelAchievementCode[i]);
                }
            }
        }





        void CheckCoinMission(ulong coin, string missionCode)
        {
            if (User.Instance.Coin >= coin)
            {
                Social.ReportProgress(missionCode, 100.0f, (bool success) =>
                {
                    // handle success or failure
                    if (success)
                    {
                        User.Instance.CoinMissionGrade++;
                    }
                });
            }
        }

        void CheckAllCoinMission(int grade)
        {
            for (int i = 0; i < coinAchievementCode.Length; ++i)
            {
                if (grade == i)
                {
                    CheckCoinMission(coinAchivementAmount[i], coinAchievementCode[i]);
                }
            }
        }


        void CheckTimedSpinSpeedMission(float speed, string missionCode)
        {
            if (User.Instance.TimedSpinHighSpeed >= speed)
            {
                Social.ReportProgress(missionCode, 100.0f, (bool success) =>
                {
                    // handle success or failure
                    if (success)
                    {
                        User.Instance.TimedSpeedMissionGrade++;
                    }
                });
            }
        }
        void CheckAllSpeedMission(int grade)
        {
            for (int i = 0; i < timedSpinSpeedAchievementCode.Length; ++i)
            {
                if (grade == i)
                {
                    CheckTimedSpinSpeedMission(timedSpinSpeedAchivementSpeed[i], timedSpinSpeedAchievementCode[i]);
                }
            }
        }





        public void SaveGame()
        {
            if (!Social.localUser.authenticated)
                return;

            isSaving = true;
#if UNITY_ANDROID
            ((PlayGamesPlatform)Social.Active).SavedGame.ShowSelectSavedGameUI(
                "저장할 슬롯을 선택하세요.", 2, true, true, SavedGameSelected);
#endif
        }


        public void LoadGame()
        {
            if (!Social.localUser.authenticated)
                return;

            isSaving = false;
#if UNITY_ANDROID
            ((PlayGamesPlatform)Social.Active).SavedGame.ShowSelectSavedGameUI(
                "로드할 슬롯을 선택하세요.", 2, false, false, SavedGameSelected);
#endif
        }

        public void SavedGameSelected(SelectUIStatus _status, ISavedGameMetadata _game)
        {
            if (_status == SelectUIStatus.SavedGameSelected)
            {
                string _filename = _game.Filename;
                if (isSaving && (_filename == null || _filename.Length == 0))
                {
                    // 새로 저장하기
                    _filename = "save" + DateTime.Now.ToBinary();
                }
                if (isSaving)
                {
                    // 저장하기
                    slot0.State = "Saving to " + _filename;
                }
                else
                {
                    // 불러오기
                    slot0.State = "Loading from " + _filename;
                }
#if UNITY_ANDROID
                // 파일을 읽고 쓰기 전에 열어야만 한다
                ((PlayGamesPlatform)Social.Active).SavedGame
                    .OpenWithAutomaticConflictResolution(_filename,
                            DataSource.ReadCacheOrNetwork,
                                ConflictResolutionStrategy.UseLongestPlaytime,
                                SavedGameOpened);
#endif
            }
            else
            {
                Debug.LogWarning("Error selecting save game: " + _status);
            }

        }

        public void SavedGameOpened(SavedGameRequestStatus _status, ISavedGameMetadata _game)
        {
            if (_status == SavedGameRequestStatus.Success)
            {
                if (isSaving)
                {
#if UNITY_ANDROID
                    // 스트링 데이터를 바이트로 바꿔서 메타 정보와 함꼐 저장한다
                    slot0.State = "Opened, now writing";
                    byte[] data = slot0.ToBytes();
                    TimeSpan playedTime = slot0.TotalPlayingTime;
                    SavedGameMetadataUpdate.Builder builder =
                        new SavedGameMetadataUpdate.Builder()
                            .WithUpdatedPlayedTime(playedTime)
                            .WithUpdatedDescription("Saved Game at " +
                            DateTime.Now);
                    SavedGameMetadataUpdate updatedMetadata = builder.Build();
                    ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(
                        _game, updatedMetadata, data, SavedGameWritten);
#endif
                }
                else
                {
#if UNITY_ANDROID
                    // 우선 파일을 읽어온다
                    slot0.State = "Opened, reading...";
                    ((PlayGamesPlatform)Social.Active).SavedGame
                        .ReadBinaryData(_game, SavedGameLoaded);
#endif
                }
            }
            else
            {
                Debug.LogWarning("Error opening game: " + _status);
            }
        }

        public void SavedGameLoaded(SavedGameRequestStatus _status, byte[] _data)
        {
            if (_status == SavedGameRequestStatus.Success)
            {
                // 불러온 바이트 데이터를 게임데이터로 바꾼다
                slot0 = GameData.FromBytes(_data);
                SceneManager.LoadScene("Splash");
            }
            else
            {
                Debug.LogWarning("Error reading game: " + _status);
            }
        }

        public void SavedGameWritten(SavedGameRequestStatus _status, ISavedGameMetadata _game)
        {
            if (_status == SavedGameRequestStatus.Success)
            {
                // 성공적으로 저장되었다
                slot0.State = "Saved!";
            }
            else
            {
                Debug.LogWarning("Error saving game: " + _status);
            }
        }



        // Update is called once per frame
        void Update()
        {


        }
    }
}