using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymeeDesktopApp.Models
{
    public enum Injury
    {
        NULL,//this is defaut value
        LEG, HAND, WRIST, FINGERS, ELBOW, SHOULDER, ANKLE, ARM, NECK, BACK
    };
    public enum FitnessLevel
    {
        NULL,
        BEGINNER, INTERMEDIATE, ADVANCED
    };
    public enum Goal
    {
        NULL,
        INCREASE_MUSCLE, TONE_MUSCLE, GENERAL
    };

    public enum Gender
    {
        NULL,
        M,
        F,
        O
    }

    public enum WeeklyWorkouts
    {
        NULL,
        ONE_OR_TWO = 'O',// "1_OR_2",
        THREE_OR_FOUR = 'T',// "3_OR_4",
        FIVE_OR_MORE = 'F'//'"5_OR_MORE"
    }

    public class User
    {
        public string Branch { get; set; }//automatic probably set by environment variable change by machine
        public string FullName { get; set; }//full name
        public string Email { get; set; }//Email
        public string PhoneNumber { get; set; }//Phone Number
        public string Password { get; set; }//User Password
        public float Age { get; set; }//Age
        public Gender Gender { get; set; }// Gender
        public uint Height { get; set; }//height in centimeters
        public float Weight { get; set; }//weight in kilograms
        public Injury[] Injuries { get; set; }//past injuries  
        public FitnessLevel FitnessLevel { get; set; }//trainee's fitness level
        public Goal FitnessGoals { get; set; }//trainee's goal
        public WeeklyWorkouts WeeklyWorkouts { get; set; }// Trainee's estimate of weekly workout days preference

    }
}