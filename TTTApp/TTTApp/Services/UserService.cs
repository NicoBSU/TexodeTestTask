using System.Collections.Generic;
using System.Linq;
using TTTApp.Enums;
using TTTApp.Models;

namespace TTTApp.Services
{
    public class UserService
    {
        private JsonReader _jsonReader;

        public UserService(JsonReader jsonReader)
        {
            _jsonReader = jsonReader;
        }

        public Dictionary<string, UserData> GetAllUsersData()
        {
            var userDataDictionary = new Dictionary<string, UserData>();
            
            for (int i = 0; i < 30; i++)
            {
                var userList = _jsonReader.LoadData(i+1);
                foreach (var user in userList)
                {
                    if (!userDataDictionary.ContainsKey(user.UserName))
                    {
                        userDataDictionary.Add(user.UserName, new UserData
                        {
                            UserName = user.UserName,
                            DailyResultsList = new List<int> { user.Steps },
                            Ranks = new List<int> { user.Rank },
                            Statuses = new List<TrainingStatus> { user.Status }
                        });
                    }
                    else
                    {
                        userDataDictionary[user.UserName].DailyResultsList.Add(user.Steps);
                        userDataDictionary[user.UserName].Ranks.Add(user.Rank);
                        userDataDictionary[user.UserName].Statuses.Add(user.Status);
                    }
                }
            }

            foreach (var user in userDataDictionary.Keys)
            {
                FindAverageStepsForUser(userDataDictionary, user);
                FindBestResultForUser(userDataDictionary, user);
                FindWorstResultForUser(userDataDictionary, user);
            }
            return userDataDictionary;
        }


        public void FindAverageStepsForUser(Dictionary<string, UserData> userDataDictionary, string userName)
        {
            var averageSteps = userDataDictionary[userName].DailyResultsList.Average();
            userDataDictionary[userName].AverageSteps = averageSteps;
        }

        public void FindWorstResultForUser(Dictionary<string, UserData> userDataDictionary, string userName)
        {
            var worstResult = userDataDictionary[userName].DailyResultsList.Min();
            userDataDictionary[userName].WorstResult = worstResult;
        }

        public void FindBestResultForUser(Dictionary<string, UserData> userDataDictionary, string userName)
        {
            var bestResult = userDataDictionary[userName].DailyResultsList.Max();
            userDataDictionary[userName].BestResult = bestResult;
        }
    }
}
