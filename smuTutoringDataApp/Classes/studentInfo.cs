
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Windows;

namespace smuTutoringDataApp
{
    class studentInfo
    {
        //user information 
        public string userFirstName { get; set; }
        public string userLastName { get; set; }
        public string userID { get; set; }
        public List<string> userCourseList { get; set; }
        public List<string> userTutorList { get; set; }
        public bool userWaiver { get; set; }


        //***********************************************
        //Method Name: AssignData
        //Description: This method creates a new user
        //object via gathering info about the related
        //id for the student from the master student
        //table. We then pull the info for the current
        //user and assign data via that method. 
        //*********************************************** 
        public void AssignData()
        {
            //check to see if we have path set
            if (programSettings.studentInfoCSVPath != null)
            {
                //create new csInputClass Obj
                csvInputClass userCompTable = new csvInputClass();

                //load the master student info file
                userCompTable.loadSetFromCSV(programSettings.studentInfoCSVPath);

                //find the user via their id
                rowToPullFrom(userID, userCompTable.compTable);
            }

            //otherwiser show error
            else
            {
                MessageBox.Show("Error: Please select Master Student CSV lcation in Settings");
            }
        }

        //******************************************
        //Method Name: AssignTutorList
        //Description: This method loads a list
        //from a .csv file using the csvInputClass
        //this this is then used to fill the student
        //tutor list.
        //******************************************
        public void AssignTutorList()
        {
            //check to see if we have file path
            if (programSettings.masterTutorListPath != null)
            {
                //create obj & load table
                csvInputClass userTutorTable = new csvInputClass();
                userTutorTable.loadSetFromCSV(programSettings.masterTutorListPath);

                //get the correct column and set data
                tutorColumnToPullFrom(userTutorTable.compTable);
            }
            //otherwise notify user of error
            else
            {
                MessageBox.Show("Error: No master tutor list selected");
            }
        }

        //**********************************************
        //Method Name: tutorColumnToPullFrom
        //Description: this method takes in a datatable,
        //takes the first column minus the header and 
        //put that data into a list. This list is one
        //of the user properties.
        //**********************************************
        public void tutorColumnToPullFrom(DataTable tutorTable)
        {

            try
            {
                //Set list to be of the correct size
                userTutorList = new List<string>(tutorTable.Rows.Count);

                //pull and set data
                foreach (DataRow row in tutorTable.Rows)
                {
                    userTutorList.Add(row[0].ToString());
                }
            }

            //In the event of an error.
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //********************************************
        //Method Name getCourseCheckList
        //Description: This method creates and returns
        //a list that is filled with the current STEM
        //course codes that are being offered. This is
        //used by a different method to filter out non
        //STEM courses.
        //********************************************
        public List<string> getCourseCheckList()
        {
            //Create new list to hold course codes
            List<string> compList = null;

            //Check to see if we have file path
            if (programSettings.masterCourseCheckListPath != null)
            {
                //Create new data table from file
                csvInputClass stemCheckTable = new csvInputClass();
                stemCheckTable.loadSetFromCSV(programSettings.masterCourseCheckListPath);

                try
                {
                    //Make compList the correct size
                    compList = new List<string>(stemCheckTable.compTable.Rows.Count);

                    //Add data to compList
                    foreach (DataRow row in stemCheckTable.compTable.Rows)
                    {
                        compList.Add(row[0].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            //Otherwise show error messeage w/ problem
            else
            {
                MessageBox.Show("Error: No Master STEM course list selected");
            }

            //Return the final list. 
            return compList;
        }

        //******************************************************
        //Method Name: rowToPullFrom
        //Description: This method takes the user id and
        //a data table(the comparison master student data table)
        //it then selects the correct row that holds the user 
        //data based on the student id. It then takes that data
        //and assigns it to various properties that relate to 
        //the user.
        //*******************************************************
        public void rowToPullFrom(string originalUserPid, DataTable csvDataTable)
        {

            try
            {
                //Create new data row array from the rows that hold user information. NOTE: I would love to take the first column and just search that w/o using the name  
                DataRow[] classTemp = csvDataTable.Select("PC_ID='" + originalUserPid + "'");

                //Take first row of array and set it to a single row obj
                DataRow singleInfoRow = classTemp[0]; //redundant may not need

                //Assign user course list from classTemp array
                userCourseList = classTemp.AsEnumerable().Select(x => x.Field<string>("Class").ToString()).ToList();

                //Fill new comparison STEM course list
                List<string> compList = getCourseCheckList();

                //Create new final list set as the same size as compList
                List<string> finalList = new List<string>(compList.Count);

                //For stripping course code 
                string tempCode = null;

                //Take each item in the userCourseList
                foreach (string item in userCourseList)
                {
                    //Get the first 3 letters e.g MTH
                    tempCode = item.Substring(0, 3);

                    //Trim any additional space
                    tempCode = tempCode.Trim();

                    //Take tempcode and compare it to our approved course list
                    for (int i = 0; i < compList.Count; i++)
                    {
                        //If it approves add it to the final list
                        if (tempCode == compList[i])
                        {
                            finalList.Add(item);
                        }
                    }
                }

                //set the userCouseList to our final filtered list
                userCourseList = finalList;

                //set the userFirstName property
                userFirstName = singleInfoRow[2].ToString();
                userLastName = singleInfoRow[1].ToString();

            }
            //Catch the most likely error
            catch
            {
                MessageBox.Show("Error: No record found to match");
            }
        }
    }
}