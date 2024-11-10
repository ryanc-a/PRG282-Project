using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRG281_Project
{
    internal class DataHandler
    {
        //variables used for data validation
        public bool duplicateID = false, invalidID = false, invalidName = false,invalidCourse = false,invalidAge = false, emptyID = false, emptyName = false,emptyAge = false, emptyCourse;
        public DataHandler() { }
        //opens the filehandler to save and load data as needed
        File_Handler handler = new File_Handler();
        //adds a new student to memory and saves them to the file
        public void AddStudent(List<Student> students, Student newStudent)
        {
            //adds student
            Console.WriteLine("Adding new student....");
            students.Add(newStudent);
            Console.WriteLine("Saving new student to storage...");
            handler.SaveStudents(students);

        }
        //updates a student
        public void UpdateStudent(List<Student> students, Student oldStudent, Student updatedStudent)
        {
            int i = students.LastIndexOf(oldStudent);
            if (i != -1)
            {
                students[i] = updatedStudent;
                handler.SaveStudents(students);
            }
            else
            {
                Console.WriteLine("No matching student found");
            }
            handler.SaveStudents(students);

        }
        //deletes a student
        public void DeleteStudent(List<Student> students, Student deleteStudent)
        {
            students.Remove(deleteStudent);
            handler.SaveStudents(students);

        }
        //generate summary of average age and total students
        public void GenerateSummary(List<Student> students)
        {
            string[] summary = new string[2];
            int totalStudents;
            double totalAge = 0;
            double averageAge;
            //getting student count
            totalStudents = students.Count();
            //getting the thotal age of students
            foreach (Student student in students)
            {
                totalAge += student.Student_Age;
            }
            //getting the average age of students rouded to 2 decimal places
            averageAge = Math.Round((totalAge / totalStudents), 2);
            summary[0] = totalStudents.ToString();
            summary[1] = averageAge.ToString();
            //saves updated students list to text file
            handler.SaveSummary(summary);

        }

        //This section of the class is for input validation

        //checks that new student is valid
        public Student CheckValidAdd(List<Student> students,Student cStudent, string type)
        {
            //reset the chack variables
            duplicateID = false;
            invalidID = false;
            if(type == "update") {invalidID = true;}
            invalidName = false;
            invalidCourse = true;
            invalidAge = false;
            emptyID = false;
            emptyName = false;
            emptyAge = false;
            emptyCourse = false;

            //if there is a student
            if (cStudent != null)
            {
                //checking each student in memory if the meet a certain condition
                foreach (Student student in students)
                {
                    //checks if student already exists
                    if(student.Student_ID == cStudent.Student_ID && type == "add")
                    {
                        duplicateID = true;
                        MessageBox.Show("This student ID already exists, please input a unique id");
                        break;
                    }
                    if(student.Student_ID == cStudent.Student_ID && type == "update")
                    {
                        invalidID = false;
                    }
                }
                //checks if id is valid
                if (cStudent.Student_ID == 0)
                {
                    emptyID= true;
                }
                //checks of age is valid
                if (cStudent.Student_Name == "" ) 
                {
                    emptyName = true;
                }
                //checks of age is valid
                if (cStudent.Student_Age == 0)
                {
                    emptyAge = true;
                }
                //checks if cource is valid
                if (cStudent.Course == "")
                {
                    emptyCourse = true;
                }
                //checks if the name is too long
                if (cStudent.Student_Name.Length > 25)
                {
                    invalidName = true;
                    //MessageBox.Show("This student has a name with too many charecters, please shorten it");
                }
                //checks if age is valid
                if (cStudent.Student_Age < 18 || cStudent.Student_Age > 50)
                {
                    invalidAge = true;
                    //MessageBox.Show("This student age is below the minumum");
                }
                //checks if the course is valid
                if (cStudent.Course == "BIT" || cStudent.Course == "BCOMP" || cStudent.Course == "Degree")
                {
                    invalidCourse = false;
                    //MessageBox.Show("This student does not have a valid course");
                }
            }
            return cStudent;
        }
        //checks if a delete is valid
        public int CheckValidDelete(List<Student> students, int cStudent)
        {
            //reseting check
            invalidID = true;
            foreach (Student student in students)
            {
                //checks if student  exists
                if (student.Student_ID == cStudent)
                {
                    invalidID = false;
                    //MessageBox.Show("This student ID does not exists, please input a valid id");
                    break;
                }
            }
            return cStudent;
        }
        //search for student (move to dh)
        public Student searchStudent(List<Student> students, int searchID) 
        {
            Student result = null;
            foreach (Student student in students)
            {
                if (student.Student_ID == searchID)
                {
                    result = student;
                    break;
                }
            }
            return result;
        }
    }
}
