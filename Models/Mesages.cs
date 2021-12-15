using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymeeDestkopApp.Models
{
    public enum SignupProperty
    {
        Credentiels, FitnessLevel, Goal, WorkoutDays, Height, Weight, Gender, BdDay, BdMonth, BdYear
    }

    public class UserCredentials
    {
        public string FullName { get; set; }
        public string EmailAddr { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }

    public class SignupVMMessage //messagfes used for sign up process
    {
        public SignupProperty SignupDetail { get; set; }
        public object Data { get; set; }

        public SignupVMMessage(SignupProperty type, object data)
        {
            SignupDetail = type;
            Data = data;
        }
    }


    public enum PageIndex //order of slides as organized in mainwindow transitioner
    {
        INTRO_PAGE,
        WHAT_IS_GYMEE,
        START_SIGNUP_OR_LOGIN,
        TERMS_OF_USE,
        SIGNUP_DATA,
        SIGNUP_BODY_DATA,
        LOGIN,
        PRE_WORKOUT,
        COUNT_DOWN_WORKOUT,
        WORKOUT_VIDEO,
        POST_WORKOUT_VIEW,
        POST_WORKOUT_QR,
    }
    public class ChangePageMessage //messages used to change page in mainwindow transitioner when next/prev buttons are clicked
    {
        public PageIndex Index { get; set; }
        public ChangePageMessage(PageIndex index)
        {
            Index = index;
        }
    }


}
