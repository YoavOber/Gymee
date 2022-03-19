using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using GymeeDesktopApp.Models;
using System.Net;
using Newtonsoft.Json;
using RestSharp;

namespace GymeeDestkopApp.Services
{
    public class GymeeAuthenticateService
    {
        //static Uri apiLocation = new Uri("https://gymeefit.com/_functions/");
        static Uri apiLocation = new Uri("https://app.gymeefit.com/_functions");

        static RestClient client = new RestClient(apiLocation)
        {
            Timeout = -1
        };

        public struct LoginResult
        {
            public bool loggedIn;
            public string email;
            public string name;
            public FitnessLevel fitnessLevel;
            public string token;
        }


        public static async Task<LoginResult> Login(string phone, string email)
        {
            var loginBody = new
            {
                email = email.ToLower(),
                phoneNumber = phone
            };

            var request = new RestRequest("loginStation", Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(loginBody);
            IRestResponse response = await client.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<LoginResult>(response.Content);
        }

        private static string parseWeeklyWorkout(WeeklyWorkouts workouts)
        {
            if (workouts == WeeklyWorkouts.ONE_OR_TWO)
            {
                return "1-2 Days";
            }
            else if (workouts == WeeklyWorkouts.THREE_OR_FOUR)
            {
                return "3-4 Days";
            }
            else if (workouts == WeeklyWorkouts.FIVE_OR_MORE)
            {
                return "5+ Days";
            }
            else
            {
                return "5+ Days";
            }
        }

        private static string parseFitnessGoal(Goal g)
        {
            switch (g)
            {
                case Goal.GENERAL: return "General Fitness";
                case Goal.INCREASE_MUSCLE: return "Muscle Mass";
                case Goal.TONE_MUSCLE: return "Sculpt Body";
                default: return "General Fitness";
            }
        }

        public struct SignUpResult
        {
            public bool success;
            public string error;
        }

        public static async Task<SignUpResult> SignUp(User user)
        {

            var registerBody = new
            {
                name = user.FullName,
                branch = user.Branch,
                email = user.Email.ToLower(),
                phoneNumber = user.PhoneNumber,
                password = user.Password,//problem - encode ?
                dob = user.DateOfBirth,
                gender = user.Gender.ToString(),
                height = user.Height,
                weight = user.Weight,
                injuries = user.Injuries.Select(injury => injury.ToString()),
                fitnessLevel = user.FitnessLevel.ToString(),
                fitnessGoal = parseFitnessGoal(user.FitnessGoals),
                weeklyWorkouts = parseWeeklyWorkout(user.WeeklyWorkouts)
            };

            var request = new RestRequest("registerStation", Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(registerBody);
            IRestResponse response = await client.ExecuteAsync(request);
            //todo nadav ?- extract exact error message from server
            return JsonConvert.DeserializeObject<SignUpResult>(response.Content);
        }

        public static async Task<bool> userExists(string email)
        {
            var searchBody = new
            {
                email = email.ToLower()
            };

            var request = new RestRequest("userExists", Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(searchBody);

            IRestResponse response = await client.ExecuteAsync(request);
            return response.StatusCode == HttpStatusCode.OK;
        }

        public static async Task<bool> onAssessmentDone(string email, string name)
        {
            var body = new
            {
                email = email.ToLower(),
                name = name
            };

            var request = new RestRequest("assessmentDone", Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(body);

            IRestResponse response = await client.ExecuteAsync(request);
            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
