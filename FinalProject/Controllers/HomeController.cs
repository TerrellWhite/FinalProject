﻿using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Configuration;
using FinalProject.Models;
using System;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            UserInput lastInput;
            HowsLifeEntities ORM = new HowsLifeEntities();
            lastInput = ORM.UserInputs.ToList()[ORM.UserInputs.ToList().Count - 1];
            ViewBag.userData = lastInput;

            List<string> stateCodes = new List<string> { "01", "02", "04", "05", "06", "08", "09", "10", "12", "13", "15", "16", "17", "18", "19", "20", "21", "22", "23",
                                                         "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42",
                                                         "44", "45", "46", "47", "48", "49", "50", "51", "53", "54", "55", "56"};
            List<string> stateLabels = new List<string> { "Alabama", "Alaska", "Arizona", "Arkansas", "California", "Colorado", "Connecticut", "Delaware", "Florida", "Georgia",
                                                          "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", "Kansas", "Kentucky", "Louisiana", "Maine", "Maryland", "Massachusettes",
                                                          "Michigan", "Minnesota", "Mississippi", "Missouri", "Montana", "Nebraska", "Nevada", "New Hampshire", "New Jersey", "New Mexico",
                                                          "New York", "North Carolina", "North Dakota", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Rhode Island", "South Carolina",
                                                          "South Dakota", "Tennessee", "Texas", "Utah", "Vermont", "Virgina", "Washington", "West Virginia", "Wisconsin", "Wyoming"};
            int codeIndexState = stateCodes.IndexOf(lastInput.state);
            string correspondingLabelState = stateLabels[codeIndexState];
            ViewBag.StateCodes = stateCodes;
            ViewBag.StateLabels = stateLabels;
            ViewBag.UserStateCode = lastInput.state;
            ViewBag.UserStateLabel = correspondingLabelState;

            List<string> ageCodes = new List<string> { "DP05_0004E", "DP05_0005E", "DP05_0006E", "DP05_0007E", "DP05_0008E", "DP05_0009E", "DP05_0010E", "DP05_0011E", "DP05_0012E", "DP05_0013E", "DP05_0014E", "DP05_0015E", "DP05_0016E" };
            List<string> ageLabels = new List<string> { "Under 5 years", "5 to 9 years", "10 to 14 years", "15 to 19 years", "20 to 24 years", "25 to 34 years", "35 to 44 years", "45 to 54 years", "55 to 59 years", "60 to 64 years", "65 to 74 years", "75 to 84 years", "85 years and over" };
            List<int> ageValues = new List<int> { 4, 9, 14, 19, 24, 34, 44, 54, 59, 64, 74, 84, 90 };

            int codeIndexAge = ageCodes.IndexOf(lastInput.age);
            string correspondingLabelAge = ageLabels[codeIndexAge];
            int correspondingValueAge = ageValues[codeIndexAge];
            ViewBag.AgeValue = correspondingValueAge;
            ViewBag.AgeLabel = correspondingLabelAge;

            List<string> genderCodes = new List<string> { "DP05_0002E", "DP05_0003E" };
            List<string> genderLabels = new List<string> { "Male", "Female" };

            int codeIndexGender = genderCodes.IndexOf(lastInput.gender);
            string correspondingLabelGender = genderLabels[codeIndexGender];
            ViewBag.GenderLabel = correspondingLabelGender;

            List<string> incomeCodes = new List<string> { "DP03_0052E", "DP03_0053E", "DP03_0054E", "DP03_0055E", "DP03_0056E", "DP03_0057E", "DP03_0058E", "DP03_0059E", "DP03_0060E", "DP03_0061E" };
            List<string> incomeLabels = new List<string> { "Less than $10,000", "$10,000 to $14,999", "$15,000 to $24,999", "$25,000 to $34,999", "$35,000 to $49,999", "$50,000 to $74,999", "$75,000 to $99,999", "$100,000 to $149,999", "$150,000 to $199,999", "$200,000 or more" };
            List<int> incomeValues = new List<int> { 10000, 15000, 25000, 35000, 50000, 75000, 100000, 150000, 200000, 250000 };
            

            int codeIndexIncome = incomeCodes.IndexOf(lastInput.incomerange);
            string correspondingLabelIncome = incomeLabels[codeIndexIncome];
            int correspondingValueIncome = incomeValues[codeIndexIncome];
            ViewBag.IncomeCodes = incomeCodes;
            ViewBag.IncomeLabels = incomeLabels;
            ViewBag.UserIncomeCode = lastInput.incomerange;
            ViewBag.IncomeValue = correspondingValueIncome;
            ViewBag.IncomeLabel = correspondingLabelIncome;

            List<string> educationCodes = new List<string> { "DP02_0060E", "DP02_0061E", "DP02_0062E", "DP02_0064E", "DP02_0065E" };
            List<string> educationLabels = new List<string> { "Some Highschool", "Highschool Graduate", "Some College", "Bachelor's Degree", "Graduate Degree" };
            
            int codeIndexEducation = educationCodes.IndexOf(lastInput.collegeeducation);
            string correspondingLabelEducation = educationLabels[codeIndexEducation];
            ViewBag.EducationLabels = correspondingLabelEducation;

            List<string> marriageCodes = new List<string> { "DP02_0004E","DP02_0010E" }; //index 22/23 data 
            List<string> marriageLabels = new List<string> { "Married", "Not Married" };

            int codeIndexmarriage = marriageCodes.IndexOf(lastInput.maritalstatus);
            string correspondingLabelmarriage = marriageLabels[codeIndexmarriage];
            ViewBag.marriageLabels = correspondingLabelmarriage;

            List<string> kidCodes = new List<string> {/*"DP02_0005E"*/ "DP02_0013E", /*"DP02_0004E"*/"DP02_0001E"}; 
            List<string> kidLabels = new List<string> { "Has Children", "No Children" };
            string userKidCode = "";
            if (lastInput.haschildren == true)
            {
                userKidCode = "DP02_0013E";
            }
            else
            {
                userKidCode = "DP02_0001E";
            }
            int codeIndexKids = kidCodes.IndexOf(userKidCode);
            string correspondingLabelKids = kidLabels[codeIndexKids];
            ViewBag.userKidCodeLabel = correspondingLabelKids;
            
            //-----------------------------------------------------------------------------------

            HttpWebRequest apiRequest = WebRequest.CreateHttp($"https://api.census.gov/data/2016/acs/acs1/profile?get=NAME" +
            $",DP05_0002E,DP05_0003E" + //gender test1[1-2]
            $",DP05_0008E,DP05_0009E,DP05_0010E,DP05_0011E,DP05_0012E,DP05_0013E,DP05_0014E,DP05_0015E,DP05_0016E" + //age test1[3-11]
            $",DP03_0052E,DP03_0053E,DP03_0054E,DP03_0055E,DP03_0056E,DP03_0057E,DP03_0058E,DP03_0059E,DP03_0060E,DP03_0061E" + //income test1[12-21]
            $",DP02_0004E,DP02_0010E" + //marriage status test1[22-23]
            $",DP02_0001E,DP02_0013E" + //have kids test1[24-25]
            $",{lastInput.gender},{lastInput.age},{lastInput.incomerange},{lastInput.collegeeducation},{lastInput.maritalstatus}" + // test1[26-30]
            $"&for=state:{lastInput.state}");

            apiRequest.Headers.Add("X-Census-Key", ConfigurationManager.AppSettings["X-Census-Key"]); // used to add keys.
            apiRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";
            HttpWebResponse apiResponse = (HttpWebResponse)apiRequest.GetResponse();

            if (apiResponse.StatusCode == HttpStatusCode.OK) 
            {
                StreamReader responseData = new StreamReader(apiResponse.GetResponseStream());
                string data = responseData.ReadToEnd(); 

                JArray jsonData = JArray.Parse(data);

                ViewBag.test1 = jsonData[1];
            }
            
            HttpWebRequest apiRequest_2 = WebRequest.CreateHttp($"https://api.census.gov/data/2016/acs/acs1/profile?get=NAME" +
                $",DP02_0060E,DP02_0061E,DP02_0062E,DP02_0064E,DP02_0065E" + //educational attainment - test2[1-5]
                $",DP04_0127E,DP04_0128E,DP04_0129E,DP04_0130E,DP04_0131E,DP04_0132E,DP04_0133E" + //gross rent paid per month - test2[6-12]
                $",DP04_0094E,DP04_0095E,DP04_0096E,DP04_0097E,DP04_0098E,DP04_0099E,DP04_0100E,DP04_0101E" + //amount paid on mortgage per month - test2[13-20]
                $",DP04_0103E,DP04_0104E,DP04_0105E,DP04_0106E,DP04_0107E,DP04_0108E" + //amount paid per month on house/no mortgage - test2[21-26]
                $",{lastInput.gender},{lastInput.age},{lastInput.incomerange},{lastInput.collegeeducation},{lastInput.maritalstatus},{userKidCode}" + // test2[27-32]
                $"&for=state:{lastInput.state}");
            /**/
            apiRequest_2.Headers.Add("X-Census-Key", ConfigurationManager.AppSettings["X-Census-Key"]); // used to add keys.
            apiRequest_2.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";

            HttpWebResponse apiResponse_2 = (HttpWebResponse)apiRequest_2.GetResponse();
            if (apiResponse_2.StatusCode == HttpStatusCode.OK) // (== 200) if we get status of 200, things are good.
            {
                StreamReader responseData_2 = new StreamReader(apiResponse_2.GetResponseStream());// use System.IO
                string data = responseData_2.ReadToEnd(); //reads data from the response

                JArray jsonData_2 = JArray.Parse(data);
                
                ViewBag.test2 = jsonData_2[1];
            }

            //-------------------------------------------------------------------------------------------------------------------

            ViewBag.EducationSuggestion = " ";
            if (lastInput.collegeeducation == "DP02_0060E" || lastInput.collegeeducation == "DP02_0061E" || lastInput.collegeeducation == "DP02_0062E")
            {
                ViewBag.EducationSuggestion = "*On average, college graduates earn $1 million more in earnings over their lifetime. The median yearly income gap between high school and college graduates is around $17,500. Maybe you should get a degree!";
            }

            ViewBag.HousingSuggestion = " ";
            if (lastInput.residentialstatus == "rent")
            {
                if ((correspondingValueIncome / 12) / 3 < Int32.Parse(lastInput.grosserent))
                {
                    ViewBag.HousingSuggestion = "*Based on our 1/3 rule, you're spending too much on rent!";
                }
            }

            if(lastInput.residentialstatus == "own")
            {
                if((correspondingValueIncome / 12) / 3 < Int32.Parse(lastInput.grosserent))
                {
                    ViewBag.HousingSuggestion = "*Based on our 1/3 rule, you're spending too much on your mortgage!";
                }
            }

            ViewBag.StateSuggestion1 = " ";
            if( ViewBag.IncomeValue < 50000 )
            {
                List<string> states = new List<string> { "West Virginia", "Arkansas", "Mississippi"};
                Random rnd = new Random();
                ViewBag.StateSuggestion1 = "*Based on your income you would do better in " + states[rnd.Next(1, 3)];
            }

            ViewBag.StateSuggestion2 = " ";
            if(lastInput.maritalstatus == "DP02_0010E" && lastInput.gender == "DP05_0002E")
            {
                List<string> states = new List<string> { "Rhode Island", "Delaware", "Alabama" };
                Random rnd = new Random();
                ViewBag.StateSuggestion2 = "*Based on how many girls you're not in a relationship with, you should live in " + states[rnd.Next(1, 3)];
            }

            if (lastInput.maritalstatus == "DP02_0010E" && lastInput.gender == "DP05_0003E")
            {
                List<string> states = new List<string> { "Alaska", "North Dakota" };
                Random rnd = new Random();
                ViewBag.StateSuggestion2 = "*There are a lot of dudes in " + states[rnd.Next(1,2)];
            }

            ViewBag.StateSuggestion3 = " ";
            if (lastInput.residentialstatus == "rent" && (correspondingValueIncome / 12) / 3 < Int32.Parse(lastInput.grosserent))
            {
                List<string> states = new List<string> { "Toledo, Ohio.. on second thought, move to Memphis or something, no one likes Ohio", "Glendale, Arizona", "Kansas City, Missouri" };
                Random rnd = new Random();
                ViewBag.StateSuggestion3 = "*For lower rent you could move to " + states[rnd.Next(1,3)];
            }

            if(lastInput.residentialstatus == "own" && (correspondingValueIncome / 12) / 3 < Int32.Parse(lastInput.grosserent))
            {
                List<string> states = new List<string> { "Rhode Island", "Connecticut", "Nevada" };
                Random rnd = new Random();
                ViewBag.StateSuggestion3 = "*For lower average mortgage rates you could move to " + states[rnd.Next(1, 3)];
            }  

            ViewBag.StateSuggestion4 = " ";
            if(lastInput.collegeeducation == "DP02_0064E" || lastInput.collegeeducation == "DP02_0065E")
            {
                List<string> states = new List<string> { "Mississippi", "West Virginia", "Louisiana" };
                Random rnd = new Random();
                ViewBag.StateSuggestion4 = "*If you want to be one of the most educated people in the state you should move to " + states[rnd.Next(1,3)];
            }

            double kidCost = 237095.50;
            string kidNo = "";
            if(lastInput.numberofkids == 1)
            {
                kidNo = "kid";
            }
            else
            {
                kidNo = "kids";
            }
            ViewBag.KidsSuggestion = "";
            if(lastInput.numberofkids > 0)
            {
                ViewBag.KidsSuggestion = $"*Research estimates that your {lastInput.numberofkids} {kidNo} will cost {lastInput.numberofkids * kidCost:C} to raise to the age of 18. Kids are expensive!";
                    
            }


            Session["1"] = ViewBag.test1;
            Session["2"] = ViewBag.test2;

            return View();
        }
        
        //public ActionResult NewState(string stateid)
        //{
        //    //pulled data from cookies and populated 2 new viewbags with previous
        //    ViewBag.PrevState1 = Session["1"];
        //    ViewBag.PrevState2 = Session["2"];

        //    HowsLifeEntities ORM = new HowsLifeEntities();
        //    UserInput lastInput = ORM.UserInputs.ToList()[ORM.UserInputs.ToList().Count - 1];
        //    ViewBag.userData = lastInput;

        //    List<string> ageCodes = new List<string> { "DP05_0004E", "DP05_0005E", "DP05_0006E", "DP05_0007E", "DP05_0008E", "DP05_0009E", "DP05_0010E", "DP05_0011E", "DP05_0012E", "DP05_0013E", "DP05_0014E", "DP05_0015E", "DP05_0016E" };
        //    List<string> ageLabels = new List<string> { "Under 5 years", "5 to 9 years", "10 to 14 years", "15 to 19 years", "20 to 24 years", "25 to 34 years", "35 to 44 years", "45 to 54 years", "55 to 59 years", "60 to 64 years", "65 to 74 years", "75 to 84 years", "85 years and over" };
        //    List<int> ageValues = new List<int> { 4, 9, 14, 19, 24, 34, 44, 54, 59, 64, 74, 84, 90 };

        //    int codeIndexAge = ageCodes.IndexOf(lastInput.age);
        //    string correspondingLabelAge = ageLabels[codeIndexAge];
        //    int correspondingValueAge = ageValues[codeIndexAge];
        //    ViewBag.AgeValue = correspondingValueAge;
        //    ViewBag.AgeLabel = correspondingLabelAge;

        //    List<string> genderCodes = new List<string> { "DP05_0002E", "DP05_0003E" };
        //    List<string> genderLabels = new List<string> { "Male", "Female" };

        //    int codeIndexGender = genderCodes.IndexOf(lastInput.gender);
        //    string correspondingLabelGender = genderLabels[codeIndexGender];
        //    ViewBag.GenderLabel = correspondingLabelGender;

        //    List<string> incomeCodes = new List<string> { "DP03_0052E", "DP03_0053E", "DP03_0054E", "DP03_0055E", "DP03_0056E", "DP03_0057E", "DP03_0058E", "DP03_0059E", "DP03_0060E", "DP03_0061E" };
        //    List<string> incomeLabels = new List<string> { "Less than $10,000", "$10,000 to $14,999", "$15,000 to $24,999", "$25,000 to $34,999", "$35,000 to $49,999", "$50,000 to $74,999", "$75,000 to $99,999", "$100,000 to $149,999", "$150,000 to $199,999", "$200,000 or more" };
        //    List<int> incomeValues = new List<int> { 10000, 15000, 25000, 35000, 50000, 75000, 100000, 150000, 200000, 250000 };
            

        //    int codeIndexIncome = incomeCodes.IndexOf(lastInput.incomerange);
        //    string correspondingLabelIncome = incomeLabels[codeIndexIncome];
        //    int correspondingValueIncome = incomeValues[codeIndexIncome];
        //    ViewBag.IncomeValue = correspondingValueIncome;
        //    ViewBag.IncomeLabel = correspondingLabelIncome;

        //    List<string> educationCodes = new List<string> { "DP02_0060E", "DP02_0061E", "DP02_0062E", "DP02_0064E", "DP02_0065E" };
        //    List<string> educationLabels = new List<string> { "Some Highschool", "Highschool Graduate", "Some College", "Bachelor's Degree", "Graduate Degree" };
            
        //    int codeIndexEducation = educationCodes.IndexOf(lastInput.collegeeducation);
        //    string correspondingLabelEducation = educationLabels[codeIndexEducation];
        //    ViewBag.EducationLabels = correspondingLabelEducation;

        //    List<string> marriageCodes = new List<string> { "DP02_0004E","DP02_0010E" }; //index 22/23 data 
        //    List<string> marriageLabels = new List<string> { "Married", "Not Married" };

        //    int codeIndexmarriage = marriageCodes.IndexOf(lastInput.maritalstatus);
        //    string correspondingLabelmarriage = marriageLabels[codeIndexmarriage];
        //    ViewBag.marriageLabels = correspondingLabelmarriage;

        //    List<string> kidCodes = new List<string> {/*"DP02_0005E"*/ "DP02_0013E", /*"DP02_0004E"*/"DP02_0001E"}; 
        //    List<string> kidLabels = new List<string> { "Has Children", "No Children" };
        //    string userKidCode = "";
        //    if (lastInput.haschildren == true)
        //    {
        //        userKidCode = "DP02_0013E";
        //    }
        //    else
        //    {
        //        userKidCode = "DP02_0001E";
        //    }
        //    int codeIndexKids = kidCodes.IndexOf(userKidCode);
        //    string correspondingLabelKids = kidLabels[codeIndexKids];
        //    ViewBag.userKidCodeLabel = correspondingLabelKids;
            
        //    //-----------------------------------------------------------------------------------

        //    HttpWebRequest apiRequest = WebRequest.CreateHttp($"https://api.census.gov/data/2016/acs/acs1/profile?get=NAME" +
        //    $",DP05_0002E,DP05_0003E" + //gender test1[1-2]
        //    $",DP05_0008E,DP05_0009E,DP05_0010E,DP05_0011E,DP05_0012E,DP05_0013E,DP05_0014E,DP05_0015E,DP05_0016E" + //age test1[3-11]
        //    $",DP03_0052E,DP03_0053E,DP03_0054E,DP03_0055E,DP03_0056E,DP03_0057E,DP03_0058E,DP03_0059E,DP03_0060E,DP03_0061E" + //income test1[12-21]
        //    $",DP02_0004E,DP02_0010E" + //marriage status test1[22-23]
        //    $",DP02_0001E,DP02_0013E" + //have kids test1[24-25]
        //    $",{lastInput.gender},{lastInput.age},{lastInput.incomerange},{lastInput.collegeeducation},{lastInput.maritalstatus}" + // test1[26-30]
        //    $"&for=state:{stateid}");

        //    apiRequest.Headers.Add("X-Census-Key", ConfigurationManager.AppSettings["X-Census-Key"]); // used to add keys.
        //    apiRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";
        //    HttpWebResponse apiResponse = (HttpWebResponse)apiRequest.GetResponse();

        //    if (apiResponse.StatusCode == HttpStatusCode.OK) // (== 200) if we get status of 200, things are good.
        //    {
        //        StreamReader responseData = new StreamReader(apiResponse.GetResponseStream());// use System.IO
        //        string data = responseData.ReadToEnd(); //reads data from the response

        //        JArray jsonData = JArray.Parse(data);

        //        ViewBag.test1 = jsonData[1];
        //    }
            
        //    HttpWebRequest apiRequest_2 = WebRequest.CreateHttp($"https://api.census.gov/data/2016/acs/acs1/profile?get=NAME" +
        //        $",DP02_0060E,DP02_0061E,DP02_0062E,DP02_0064E,DP02_0065E" + //educational attainment - test2[1-5]
        //        $",DP04_0127E,DP04_0128E,DP04_0129E,DP04_0130E,DP04_0131E,DP04_0132E,DP04_0133E" + //gross rent paid per month - test2[6-12]
        //        $",DP04_0094E,DP04_0095E,DP04_0096E,DP04_0097E,DP04_0098E,DP04_0099E,DP04_0100E,DP04_0101E" + //amount paid on mortgage per month - test2[13-20]
        //        $",DP04_0103E,DP04_0104E,DP04_0105E,DP04_0106E,DP04_0107E,DP04_0108E" + //amount paid per month on house/no mortgage - test2[21-26]
        //        $",{lastInput.gender},{lastInput.age},{lastInput.incomerange},{lastInput.collegeeducation},{lastInput.maritalstatus},{userKidCode}" + // test2[27-32]
        //        $"&for=state:{stateid}");
        //    /**/
        //    apiRequest_2.Headers.Add("X-Census-Key", ConfigurationManager.AppSettings["X-Census-Key"]); // used to add keys.
        //    apiRequest_2.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";

        //    HttpWebResponse apiResponse_2 = (HttpWebResponse)apiRequest_2.GetResponse();
        //    if (apiResponse_2.StatusCode == HttpStatusCode.OK) // (== 200) if we get status of 200, things are good.
        //    {
        //        StreamReader responseData_2 = new StreamReader(apiResponse_2.GetResponseStream());// use System.IO
        //        string data = responseData_2.ReadToEnd(); //reads data from the response

        //        JArray jsonData_2 = JArray.Parse(data);

        //        ViewBag.test2 = jsonData_2[1];
        //    }

        //    //-------------------------------------------------------------------------------------------------------------------

        //    ViewBag.EducationSuggestion = " ";
        //    if (lastInput.collegeeducation == "DP02_0060E" || lastInput.collegeeducation == "DP02_0061E" || lastInput.collegeeducation == "DP02_0062E")
        //    {
        //        ViewBag.EducationSuggestion = "On average, college graduates earn $1 million more in earnings over their lifetime. The median yearly income gap between high school and college graduates is around $17,500. Maybe you should get a degree!";
        //    }

        //    ViewBag.HousingSuggestion = " ";
        //    if (lastInput.residentialstatus == "rent")
        //    {
        //        if ((correspondingValueIncome / 40) < Int32.Parse(lastInput.grosserent))
        //        {
        //            ViewBag.HousingSuggestion = "You're spending too much on rent! Have you looked anywhere less expensive?";
        //        }
        //    }

            
        //    double kidCost = 237095.50;
        //    string kidNo = "";
        //    if(lastInput.numberofkids == 1)
        //    {
        //        kidNo = "kid";
        //    }
        //    else
        //    {
        //        kidNo = "kids";
        //    }
        //    ViewBag.KidsSuggestion = "";
        //    if(lastInput.numberofkids > 0)
        //    {
        //        ViewBag.KidsSuggestion = $"Research estimates that your {lastInput.numberofkids} {kidNo} will cost {lastInput.numberofkids * kidCost:C} to raise to the age of 18. Kids are expensive!";
                    
        //    }
        //    return View();
        //}

        public ActionResult NewIncome(string stateid, string incomerange)
        {
            //pulled data from cookies and populated 2 new viewbags with previous
            ViewBag.PrevState1 = Session["1"];
            ViewBag.PrevState2 = Session["2"];

            HowsLifeEntities ORM = new HowsLifeEntities();
            UserInput lastInput = ORM.UserInputs.ToList()[ORM.UserInputs.ToList().Count - 1];
            ViewBag.userData = lastInput;

            List<string> stateCodes = new List<string> { "01", "02", "04", "05", "06", "08", "09", "10", "12", "13", "15", "16", "17", "18", "19", "20", "21", "22", "23",
                                                         "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42",
                                                         "44", "45", "46", "47", "48", "49", "50", "51", "53", "54", "55", "56"};
            List<string> stateLabels = new List<string> { "Alabama", "Alaska", "Arizona", "Arkansas", "California", "Colorado", "Connecticut", "Delaware", "Florida", "Georgia",
                                                          "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", "Kansas", "Kentucky", "Louisiana", "Maine", "Maryland", "Massachusettes",
                                                          "Michigan", "Minnesota", "Mississippi", "Missouri", "Montana", "Nebraska", "Nevada", "New Hampshire", "New Jersey", "New Mexico",
                                                          "New York", "North Carolina", "North Dakota", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Rhode Island", "South Carolina",
                                                          "South Dakota", "Tennessee", "Texas", "Utah", "Vermont", "Virgina", "Washington", "West Virginia", "Wisconsin", "Wyoming"};
            int codeIndexState = stateCodes.IndexOf(lastInput.state);
            string correspondingLabelState = stateLabels[codeIndexState];
            ViewBag.StateCodes = stateCodes;
            ViewBag.StateLabels = stateLabels;
            ViewBag.UserStateCode = lastInput.state;
            ViewBag.UserStateLabel = correspondingLabelState;

            List<string> ageCodes = new List<string> { "DP05_0004E", "DP05_0005E", "DP05_0006E", "DP05_0007E", "DP05_0008E", "DP05_0009E", "DP05_0010E", "DP05_0011E", "DP05_0012E", "DP05_0013E", "DP05_0014E", "DP05_0015E", "DP05_0016E" };
            List<string> ageLabels = new List<string> { "Under 5 years", "5 to 9 years", "10 to 14 years", "15 to 19 years", "20 to 24 years", "25 to 34 years", "35 to 44 years", "45 to 54 years", "55 to 59 years", "60 to 64 years", "65 to 74 years", "75 to 84 years", "85 years and over" };
            List<int> ageValues = new List<int> { 4, 9, 14, 19, 24, 34, 44, 54, 59, 64, 74, 84, 90 };

            int codeIndexAge = ageCodes.IndexOf(lastInput.age);
            string correspondingLabelAge = ageLabels[codeIndexAge];
            int correspondingValueAge = ageValues[codeIndexAge];
            ViewBag.AgeValue = correspondingValueAge;
            ViewBag.AgeLabel = correspondingLabelAge;

            List<string> genderCodes = new List<string> { "DP05_0002E", "DP05_0003E" };
            List<string> genderLabels = new List<string> { "Male", "Female" };

            int codeIndexGender = genderCodes.IndexOf(lastInput.gender);
            string correspondingLabelGender = genderLabels[codeIndexGender];
            ViewBag.GenderLabel = correspondingLabelGender;

            List<string> incomeCodes = new List<string> { "DP03_0052E", "DP03_0053E", "DP03_0054E", "DP03_0055E", "DP03_0056E", "DP03_0057E", "DP03_0058E", "DP03_0059E", "DP03_0060E", "DP03_0061E" };
            List<string> incomeLabels = new List<string> { "Less than $10,000", "$10,000 to $14,999", "$15,000 to $24,999", "$25,000 to $34,999", "$35,000 to $49,999", "$50,000 to $74,999", "$75,000 to $99,999", "$100,000 to $149,999", "$150,000 to $199,999", "$200,000 or more" };
            List<int> incomeValues = new List<int> { 10000, 15000, 25000, 35000, 50000, 75000, 100000, 150000, 200000, 250000 };


            int codeIndexIncome = incomeCodes.IndexOf(lastInput.incomerange);
            string correspondingLabelIncome = incomeLabels[codeIndexIncome];
            int correspondingValueIncome = incomeValues[codeIndexIncome];
            ViewBag.IncomeCodes = incomeCodes;
            ViewBag.IncomeLabels = incomeLabels;
            ViewBag.UserIncomeCode = lastInput.incomerange;
            ViewBag.IncomeValue = correspondingValueIncome;
            ViewBag.IncomeLabel = correspondingLabelIncome;

            int newCodeIndexIncome = incomeCodes.IndexOf(incomerange);
            string newCorrespondingLabelIncome = incomeLabels[newCodeIndexIncome];
            int newCorrespondingValueIncome = incomeValues[newCodeIndexIncome];
            ViewBag.NewIncomeValue = newCorrespondingValueIncome;
            ViewBag.NewIncomeLabel = newCorrespondingLabelIncome;

            List<string> educationCodes = new List<string> { "DP02_0060E", "DP02_0061E", "DP02_0062E", "DP02_0064E", "DP02_0065E" };
            List<string> educationLabels = new List<string> { "Some Highschool", "Highschool Graduate", "Some College", "Bachelor's Degree", "Graduate Degree" };

            int codeIndexEducation = educationCodes.IndexOf(lastInput.collegeeducation);
            string correspondingLabelEducation = educationLabels[codeIndexEducation];
            ViewBag.EducationLabels = correspondingLabelEducation;

            List<string> marriageCodes = new List<string> { "DP02_0004E", "DP02_0010E" }; //index 22/23 data 
            List<string> marriageLabels = new List<string> { "Married", "Not Married" };

            int codeIndexmarriage = marriageCodes.IndexOf(lastInput.maritalstatus);
            string correspondingLabelmarriage = marriageLabels[codeIndexmarriage];
            ViewBag.marriageLabels = correspondingLabelmarriage;

            List<string> kidCodes = new List<string> {/*"DP02_0005E"*/ "DP02_0013E", /*"DP02_0004E"*/"DP02_0001E" };
            List<string> kidLabels = new List<string> { "Has Children", "No Children" };
            string userKidCode = "";
            if (lastInput.haschildren == true)
            {
                userKidCode = "DP02_0013E";
            }
            else
            {
                userKidCode = "DP02_0001E";
            }
            int codeIndexKids = kidCodes.IndexOf(userKidCode);
            string correspondingLabelKids = kidLabels[codeIndexKids];
            ViewBag.userKidCodeLabel = correspondingLabelKids;

            //-----------------------------------------------------------------------------------

            HttpWebRequest apiRequest = WebRequest.CreateHttp($"https://api.census.gov/data/2016/acs/acs1/profile?get=NAME" +
            $",DP05_0002E,DP05_0003E" + //gender test1[1-2]
            $",DP05_0008E,DP05_0009E,DP05_0010E,DP05_0011E,DP05_0012E,DP05_0013E,DP05_0014E,DP05_0015E,DP05_0016E" + //age test1[3-11]
            $",DP03_0052E,DP03_0053E,DP03_0054E,DP03_0055E,DP03_0056E,DP03_0057E,DP03_0058E,DP03_0059E,DP03_0060E,DP03_0061E" + //income test1[12-21]
            $",DP02_0004E,DP02_0010E" + //marriage status test1[22-23]
            $",DP02_0001E,DP02_0013E" + //have kids test1[24-25]
            $",{lastInput.gender},{lastInput.age},{incomerange},{lastInput.collegeeducation},{lastInput.maritalstatus}" + // test1[26-30]
            $"&for=state:{stateid}");

            apiRequest.Headers.Add("X-Census-Key", ConfigurationManager.AppSettings["X-Census-Key"]); // used to add keys.
            apiRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";
            HttpWebResponse apiResponse = (HttpWebResponse)apiRequest.GetResponse();

            if (apiResponse.StatusCode == HttpStatusCode.OK) // (== 200) if we get status of 200, things are good.
            {
                StreamReader responseData = new StreamReader(apiResponse.GetResponseStream());// use System.IO
                string data = responseData.ReadToEnd(); //reads data from the response

                JArray jsonData = JArray.Parse(data);

                ViewBag.test1 = jsonData[1];
            }

            HttpWebRequest apiRequest_2 = WebRequest.CreateHttp($"https://api.census.gov/data/2016/acs/acs1/profile?get=NAME" +
                $",DP02_0060E,DP02_0061E,DP02_0062E,DP02_0064E,DP02_0065E" + //educational attainment - test2[1-5]
                $",DP04_0127E,DP04_0128E,DP04_0129E,DP04_0130E,DP04_0131E,DP04_0132E,DP04_0133E" + //gross rent paid per month - test2[6-12]
                $",DP04_0094E,DP04_0095E,DP04_0096E,DP04_0097E,DP04_0098E,DP04_0099E,DP04_0100E,DP04_0101E" + //amount paid on mortgage per month - test2[13-20]
                $",DP04_0103E,DP04_0104E,DP04_0105E,DP04_0106E,DP04_0107E,DP04_0108E" + //amount paid per month on house/no mortgage - test2[21-26]
                $",{lastInput.gender},{lastInput.age},{incomerange},{lastInput.collegeeducation},{lastInput.maritalstatus},{userKidCode}" + // test2[27-32]
                $"&for=state:{stateid}");
            /**/
            apiRequest_2.Headers.Add("X-Census-Key", ConfigurationManager.AppSettings["X-Census-Key"]); // used to add keys.
            apiRequest_2.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";

            HttpWebResponse apiResponse_2 = (HttpWebResponse)apiRequest_2.GetResponse();
            if (apiResponse_2.StatusCode == HttpStatusCode.OK) // (== 200) if we get status of 200, things are good.
            {
                StreamReader responseData_2 = new StreamReader(apiResponse_2.GetResponseStream());// use System.IO
                string data = responseData_2.ReadToEnd(); //reads data from the response

                JArray jsonData_2 = JArray.Parse(data);

                ViewBag.test2 = jsonData_2[1];
            }

            //-------------------------------------------------------------------------------------------------------------------

            ViewBag.EducationSuggestion = " ";
            if (lastInput.collegeeducation == "DP02_0060E" || lastInput.collegeeducation == "DP02_0061E" || lastInput.collegeeducation == "DP02_0062E")
            {
                ViewBag.EducationSuggestion = "*On average, college graduates earn $1 million more in earnings over their lifetime. The median yearly income gap between high school and college graduates is around $17,500. Maybe you should get a degree!";
            }

            ViewBag.HousingSuggestion = " ";
            if (lastInput.residentialstatus == "rent")
            {
                if ((correspondingValueIncome / 40) < Int32.Parse(lastInput.grosserent))
                {
                    ViewBag.HousingSuggestion = "*You're spending too much on rent! Have you looked anywhere less expensive?";
                }
            }


            double kidCost = 237095.50;
            string kidNo = "";
            if (lastInput.numberofkids == 1)
            {
                kidNo = "kid";
            }
            else
            {
                kidNo = "kids";
            }
            ViewBag.KidsSuggestion = "";
            if (lastInput.numberofkids > 0)
            {
                ViewBag.KidsSuggestion = $"*Research estimates that your {lastInput.numberofkids} {kidNo} will cost {lastInput.numberofkids * kidCost:C} to raise to the age of 18. Kids are expensive!";

            }
            return View();
        }
        public ActionResult PresentInput()
        {
            HowsLifeEntities ORM = new HowsLifeEntities();
            
            UserInput lastInput = ORM.UserInputs.ToList()[ORM.UserInputs.ToList().Count - 1];
            ViewBag.userInput = lastInput;
            return View("PresentInput");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Meet the Developers.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Questions()
        {
            ViewBag.Message = "Your form page.";
            return View();
        }
        public ActionResult frontendtemp()
        {
            return View();
        }
        public ActionResult apiTest(string state)
        {
            HttpWebRequest apiRequest = WebRequest.CreateHttp("https://api.census.gov/data/2016/acs/acs1/profile?get=NAME,DP02_0001E,DP02_0002E,DP02_0003E&for=state:" + state + "&for=cd115:*");
            apiRequest.Headers.Add("X-Census-Key", ConfigurationManager.AppSettings["X-Census-Key"]); // used to add keys.
            apiRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";
            HttpWebResponse apiResponse = (HttpWebResponse)apiRequest.GetResponse();
            if (apiResponse.StatusCode == HttpStatusCode.OK) // (== 200) if we get status of 200, things are good.
            {
                StreamReader responseData = new StreamReader(apiResponse.GetResponseStream());// use System.IO
                string data = responseData.ReadToEnd(); //reads data from the response
                JArray jsonData = JArray.Parse(data);
                ViewBag.test1 = jsonData/*[1]*/;
                //ViewBag.triviadate = jsonCensusData["year"];
            }
            return View();
        }
        public ActionResult QuestionsTemp()
        {
            return View();
        }
        public ActionResult TempOut(string state)
        {
            //apis for income
            HttpWebRequest apiRequest = WebRequest.CreateHttp("https://api.census.gov/data/2016/acs/acs1/profile?get=NAME,DP03_0052PE,DP03_0053PE,DP03_0054PE,DP03_0055PE,DP03_0056PE,DP03_0057PE,DP03_0058PE,DP03_0059PE,DP03_0060PE,DP03_0061PE&for=state:" + state);
            apiRequest.Headers.Add("X-Census-Key", ConfigurationManager.AppSettings["X-Census-Key"]); // used to add keys.
            apiRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";
            HttpWebResponse apiResponse = (HttpWebResponse)apiRequest.GetResponse();
            if (apiResponse.StatusCode == HttpStatusCode.OK) // (== 200) if we get status of 200, things are good.
            {
                StreamReader responseData = new StreamReader(apiResponse.GetResponseStream());// use System.IO
                string data = responseData.ReadToEnd(); //reads data from the response
                JArray jsonData = JArray.Parse(data);
                List<JToken> listy = jsonData.ToList();
                ViewBag.test1 = Convert.ToString(listy[1][0]);
               // List<string> listofIncome = listy.Where(x => !(x == null ||  x == listy[listy.Count -1])).ToList();


                double Less10 = Convert.ToDouble(listy[1][1]);
                ViewBag.Less10 = Less10;
                double to14 = Convert.ToDouble(listy[1][2]);
                ViewBag.to14 = to14;
                double to24 = Convert.ToDouble(listy[1][3]);
                ViewBag.to24 = to24;
                double to34 = Convert.ToDouble(listy[1][4]);
                ViewBag.to34 = to34;
                double to49 = Convert.ToDouble(listy[1][5]);
                ViewBag.to49 = to49;
                double to74 = Convert.ToDouble(listy[1][6]);
                ViewBag.to74 = to74;
                double to99 = Convert.ToDouble(listy[1][7]);
                ViewBag.to99 = to99;
                double to149 = Convert.ToDouble(listy[1][8]);
                ViewBag.to149 = to149;
                double to199 = Convert.ToDouble(listy[1][9]);
                ViewBag.to199 = to199;
                ////ViewBag.triviadate = jsonCensusData["year"];
            }

            return View();
        }
        private string JsonConvert(JToken jToken)
        {
            throw new NotImplementedException();
        }
    }
}