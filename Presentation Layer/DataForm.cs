using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;
using WMPLib;

namespace PRG281_Project
{
    public partial class DataForm : Form
    {
        public DataForm()
        {
            InitializeComponent();
            IDError.Visible = false;
            NameError.Visible = false;
            AgeError.Visible = false;
            CourseError.Visible = false;
            player.URL = "bgMusic.mp3";
        }
        File_Handler fh = new File_Handler();
        DataHandler dh = new DataHandler();
        List<Student> students = new List<Student>();
        BindingSource source = new BindingSource();
        List<Student> lastDel = new List<Student>();
        WindowsMediaPlayer player = new WindowsMediaPlayer();
        bool playing = false;

        //adds a student to the current students
        private void Add_Student_Click(object sender, EventArgs e)
        {
            int id;
            string name;
            int age;
            string course;
            //getting values from form
            if(!int.TryParse(ID.Text,out id))
            {
                dh.invalidID = true;
            }
            name = sName.Text;
            if (!int.TryParse(Age.Text, out age))
            {
                dh.invalidAge = true;
            }
            course = cbCourse.Text;
            //checking if student is valid
            dh.CheckValidAdd(students, new Student(id, name, age, course),"add");
            if (checkData())
            {
                //adds student to list
                dh.AddStudent(students, new Student(id, name, age, course));
                ID.Text = "";
                sName.Text = "";
                Age.Text = "";
                cbUpdateCourse.Text = "";
            }
            //Update display
            RefreashDisplay();
        }
        //displays the current students
        private void View_Student_Click(object sender, EventArgs e)
        {          
            //clears the students list so it is empty before recieving data
            students.Clear();
            //gets the data from the students file and stores it in the students list
            students = fh.GetStudents();
            //sets the data source of the binding source to the stdents list
            source.DataSource = students;
            //sets the source for the datagrid to the binding source
            DataGrid.DataSource = source;
        }
        private void RefreashDisplay()
        {
            //Update display
            //clears the students list so it is empty before recieving data
            students.Clear();
            source.Clear();
            source = new BindingSource();
            //gets the data from the students file and stores it in the students list
            students = fh.GetStudents();
            //sets the data source of the binding source to the stdents list
            source.DataSource = students;
            //sets the source for the datagrid to the binding source
            DataGrid.DataSource = source;
        }
        //updates an existing student using the id to find them
        private void Update_Student_Click(object sender, EventArgs e)
        {
            int searchID = 0;
            string name;
            int age = 0;
            string course;
            Student oldStudent = null;
            //getting values from form
            int.TryParse(UpdateID.Text,out searchID);
            name = UpdateName.Text;
            int.TryParse(UpdateAge.Text, out age);
            course = cbUpdateCourse.Text;
            //search for student 
            if(searchID != 0) 
            { 
            oldStudent = dh.searchStudent(students, searchID);
            }
            if(oldStudent == null) 
            {
                Console.WriteLine("Invalid ID");
            }
            else 
            { 
            if (name=="")
            {
                name =oldStudent.Student_Name;
            }
            if (age == 0)
            {
                age = oldStudent.Student_Age;
            }
            if (course =="")
            {
                course = oldStudent.Course;
            }
            }
            //checking if student is valid
            dh.CheckValidAdd(students, new Student(searchID, name, age, course), "update");
            if (checkData())
            {
                dh.UpdateStudent(students, oldStudent, new Student(searchID, name, age, course));
                UpdateID.Text = "";
                UpdateName.Text = "";
                UpdateAge.Text = "";
                cbUpdateCourse.Text = "";
            }
            //Update display
            RefreashDisplay();
        }

        //generates a summary report
        private void Generate_Summary_Click(object sender, EventArgs e)
        {
            dh.GenerateSummary(students);
            string[] summary = fh.LoadSummary();
            //Display summary
            TotalStudents.Text = summary[0];
            AverageAge.Text = summary[1];
        }

        //deletes a student using their id
        private void Delete_Student_Click(object sender, EventArgs e)
        {
            int searchID = 0;
            Student deleteStudent = null;
            int.TryParse(DeleteID.Text, out searchID);
            //finds the searched student 
            if (searchID != 0)
            {
                deleteStudent = dh.searchStudent(students, searchID);
            }
            if (deleteStudent == null)
            {
                Console.WriteLine("Invalid ID");
            }
            //checking if student is valid
            dh.CheckValidDelete(students, searchID);
            if(checkData()) 
            { 
                dh.DeleteStudent(students,deleteStudent);
                lastDel.Add(deleteStudent);
            }
            //Update display
            RefreashDisplay();
        }
        //undoes the lat delete operation
        private void btnUndoDelete_Click(object sender, EventArgs e)
        {
            if(lastDel.Count() > 0) { 
            //undoes delete
            dh.AddStudent(students, lastDel.Last());
            //removes the undone student
            lastDel.Remove(lastDel.Last());

            }
                RefreashDisplay();
        }

        //exits the program
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //checks for data errors and displays error messages
        public bool checkData()
        {
            //returns if any data is invalid
            bool valid = true;
            //reset the error messages
            IDError.Text = string.Empty;
            NameError.Text = string.Empty;
            NameUpError.Text = string.Empty;
            AgeError.Text = string.Empty;
            AgeUpError.Text = string.Empty;
            CourseError.Text = string.Empty;
            CourseUpError.Text = string.Empty;
            IDError.Visible = false;
            IDUpError.Visible = false;
            IDDelError.Visible = false;
            NameError.Visible = false;
            NameUpError.Visible = false;
            AgeError.Visible = false;
            AgeUpError.Visible = false;
            CourseError.Visible = false;
            CourseUpError.Visible = false;
            ID.ForeColor = Color.Black;
            UpdateID.ForeColor = Color.Black;
            sName.ForeColor = Color.Black;
            UpdateName.ForeColor = Color.Black;
            Age.ForeColor = Color.Black;
            UpdateAge.ForeColor = Color.Black;
            cbCourse.ForeColor = Color.Black;
            cbUpdateCourse.ForeColor = Color.Black;
            //displays error messages
            if (dh.duplicateID)
            {
                IDError.Visible=true;
                ID.ForeColor = Color.Red;
                IDError.Text = "This student ID already exists";
                valid = false;
            }
            if (dh.emptyID)
            {
                IDError.Visible = true;
                IDUpError.Visible = true;
                IDDelError.Visible = true;
                ID.ForeColor = Color.Red;
                IDError.Text = "please enter an id";
                IDUpError.Text = "please enter an id";
                valid = false;
            }
            if (dh.invalidID)
            {
                IDError.Visible = true;
                IDUpError.Visible = true;
                IDDelError.Visible = true;
                ID.ForeColor = Color.Red;
                IDError.Text = "This student ID is invalid";
                IDUpError.Text = "This student ID is invalid";
                IDDelError.Text = "This student ID is invalid";
                valid = false;
            }
            if (dh.invalidName)
            {
                NameError.Visible = true;
                NameUpError.Visible = true;
                sName.ForeColor = Color.Red;
                UpdateName.ForeColor = Color.Red;
                NameError.Text = "name has too many characters";
                NameUpError.Text = "name has too many characters";
                valid = false;
            }
            if (dh.emptyName) 
            {   
                NameError.Visible = true;
                NameUpError.Visible = true;
                sName.ForeColor = Color.Red;
                UpdateName.ForeColor = Color.Red;
                NameError.Text = "please enter a name";
                NameUpError.Text = "please enter a name";
                valid = false;
            }
            if (dh.invalidAge)
            {
                AgeError.Visible = true;
                AgeUpError.Visible = true;
                Age.ForeColor = Color.Red;
                UpdateAge.ForeColor = Color.Red;
                AgeError.Text = "age must be above 18 and below 50";
                AgeUpError.Text = "age must be above 18 and below 50";
                valid = false;
            }
            if (dh.emptyAge)
            {
                AgeError.Visible = true;
                AgeUpError.Visible = true;
                Age.ForeColor = Color.Red;
                UpdateAge.ForeColor = Color.Red;
                AgeError.Text = "please enter a age";
                AgeUpError.Text = "please enter a age";
                valid = false;
            }
            if (dh.invalidCourse)
            {
                CourseError.Visible = true;
                CourseUpError.Visible = true;
                cbCourse.ForeColor = Color.Red;
                cbUpdateCourse.ForeColor = Color.Red;
                CourseError.Text = "course must be valid";
                CourseUpError.Text = "course must be valid";
                valid = false;
            }
            if (dh.emptyCourse)
            {
                CourseError.Visible = true;
                CourseUpError.Visible = true;
                cbCourse.ForeColor = Color.Red;
                cbUpdateCourse.ForeColor = Color.Red;
                CourseError.Text = "please select a course";
                CourseUpError.Text = "please select a course";
                valid = false;
            }
            return valid;
        }

        //section for accessing and reading from grid
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            source.MovePrevious();
            readGrid();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            source.MoveFirst();
            readGrid();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            source.MoveNext();
            readGrid();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            source.MoveLast();
            readGrid();
        }

        //adds the current row data to the text boxes for convenience
        private void readGrid()
        {
            //gets the current row splits it into the different parts and stores them in an array
            if(source.Current != null) { 
            string[] data =  source.Current.ToString().Split(',');
            //assigning data values to various textboxes
            //for the add tab
            ID.Text = data[0];
            sName.Text = data[1];
            Age.Text = data[2];
            cbCourse.Text = data[3];
            //for the update tab
            UpdateID.Text = data[0];
            UpdateName.Text = data[1];
            UpdateAge.Text = data[2];
            cbUpdateCourse.Text = data[3];
            //for the delete tab
            DeleteID.Text = data[0];
            }
        }
        //aadd musuc XD
        private void DataForm_Load(object sender, EventArgs e)
        {
            player.controls.play();
            playing = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (playing)
            {
                player.controls.pause();
                playing = false;
            }
            else
            { 
                player.controls.play();
                playing = true;
            }
        }
    }
}
