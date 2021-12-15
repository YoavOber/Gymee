using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using GymeeDesktopApp.Models;
using System.Net;
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
        public static async Task<bool> Login(string phone, string email)
        {
            var loginBody = new
            {
                phoneNumber = phone,
                email = email.ToLower()
            };

            var request = new RestRequest("loginStation", Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(loginBody);
            IRestResponse response = await client.ExecuteAsync(request);
            return response.StatusCode == HttpStatusCode.OK;
        }

        private static string parseWeeklyWorkout(WeeklyWorkouts workouts)
        {
            if (workouts == WeeklyWorkouts.ONE_OR_TWO)
            {
                return "1_OR_2";
            }
            else if (workouts == WeeklyWorkouts.THREE_OR_FOUR)
            {
                return "3_OR_4";
            }
            else if (workouts == WeeklyWorkouts.FIVE_OR_MORE)
            {
                return "5_OR_MORE";
            }
            else
            {
                return "5_OR_MORE";
            }
        }

        public static async Task<bool> SignUp(User user)
        {
            var registerBody = new
            {
                name = user.FullName,
                branch = user.Branch,
                email = user.Email.ToLower(),
                phoneNumber = user.PhoneNumber,
                password = user.Password,//problem - encode ?
                age = (int)user.Age,
                gender = user.Gender.ToString(),
                height = user.Height,
                weight = user.Weight,
                injuries = user.Injuries.Select(injury => injury.ToString()),
                fitnessLevel = user.FitnessLevel.ToString(),
                fitnessGoal = user.FitnessGoals.ToString(),
                weeklyWorkouts = parseWeeklyWorkout(user.WeeklyWorkouts)
            };

            var request = new RestRequest("registerStation", Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(registerBody);
            IRestResponse response = await client.ExecuteAsync(request);
            return response.StatusCode == HttpStatusCode.OK;
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
    }
}
