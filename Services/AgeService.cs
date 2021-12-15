using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymeeDestkopApp.Services {
    class AgeService 
    {
        //definitelt didnt copy this from stackoverflow
        static public int CalculateAge(DateTime dob)
        {
            int age = 0;
            age = DateTime.Now.AddYears(-dob.Year).Year;
            return age;
        }
    }
}
