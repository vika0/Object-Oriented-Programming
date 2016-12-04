using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace StudentuPrograma
{
    public class OneStudent
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Group { get; set; }
        public int NumberOfMarks { get; set; }
        public int[] MarksArray { get; set; }

        public OneStudent(string name, string surname, string group, int numberOfMarks, int[] marksArray)
        {
            Name = name;
            Surname = surname;
            Group = group;
            NumberOfMarks = numberOfMarks;
            MarksArray = marksArray;
        }
    }

    public class Faculty
    {
        private string Fakultetas { get; set; }
        public OneStudent[] studentsArray { get; set; }
        public int studentCount { get; set; }


        public Faculty(int count)
        {
            studentCount = 0;
            studentsArray = new OneStudent[count];
        }

        public void AddData(OneStudent students)
        {
            studentsArray[studentCount] = students;
            studentCount++;
        }


        /* public void AddMarks(int mark)
         {
             studentsMarks[markCount++] = students;
             markCount++;
         }*/
    }
    //---------------
    class Program
    {
        public const int MaxStud = 50;

        //------------------------------------PAGRINDINE-PROGRAMA
        static void Main(string[] args)
        {
            Faculty allStudents = new Faculty(MaxStud);
            //Faculty AvResults = new Faculty(MaxStud);
            allStudents = ReadData();
            AverageResults(allStudents);
            //pointAverage = FindAverage(allStudents);
            /*for (int i = 0; i < allStudents.studentCount; i++)
            {
                for (int j = 0; i < allStudents.studentsArray[i].NumberOfMarks; i++)
                {
                    
                }
            }*/

        }
        //------------------------------------

        private static Faculty ReadData()
        {
            Faculty allStudents = new Faculty(MaxStud);
            using (StreamReader reader = new StreamReader(@"Informatika.txt"))
            {
                string line = null;
                //line = reader.ReadLine();

                while (null != (line = reader.ReadLine()))
                {
                    string[] values = line.Split(';');
                    string surname = values[0];
                    string name = values[1];
                    string group = values[2];
                    int numberOfMarks = int.Parse(values[3]);
                    string[] value = values[4].Split(' ');
                    int[] marksArray = new int[numberOfMarks];
                    //Console.WriteLine("{0}", name);
                    for (int i = 0; i < numberOfMarks; i++)
                    {
                        marksArray[i] = int.Parse(value[i]);
                        //Console.WriteLine(" {0} ", marksArray[i]);   
                    }

                    OneStudent student = new OneStudent(name, surname, group, numberOfMarks, marksArray);
                    allStudents.AddData(student);


                }
            }
            return allStudents;
        }
        //-----------------------------------

        public const int MaxGroups = 5;
        public const int MaxMarks = 7;
        public static void AverageResults(Faculty allStudents)
        {
            int[] GroupArray = new int[MaxGroups * MaxMarks];
            string[] GroupNameArray = new string[MaxGroups];
            double[] StudentAverage = new double[MaxStud];
            double[] StudentSum = new double[MaxStud];
            for (int i = 0; i < allStudents.studentCount; i++)
            {
                int sum = 0;
                for (int j = 0; j < allStudents.studentsArray[i].NumberOfMarks; j++)
                {
                    sum += allStudents.studentsArray[i].MarksArray[j];
                }
                StudentAverage[i] = sum / allStudents.studentsArray[i].NumberOfMarks + (sum % allStudents.studentsArray[i].NumberOfMarks)*0.1;
                //Console.WriteLine("{0} {1}",allStudents.studentsArray[i].Group, StudentAverage[i]);
                
            }
            for (int i = 0; i < allStudents.studentCount; i++)
            {
                for (int j = i+1; i < allStudents.studentCount; i++)
			{
                if (allStudents.studentsArray[i].Group == allStudents.studentsArray[j].Group)
                {
                    GroupNameArray[i] = allStudents.studentsArray[i].Group;
                    StudentSum[i] = (StudentSum[i] + StudentAverage[i]) / 2 + (StudentSum[i] + StudentAverage[i])/0.2;
                    //Console.WriteLine("{0} {1} {2}", GroupNameArray[i], StudentSum[i]);  
                    Console.WriteLine("{0}", StudentAverage[i]); 
                }
                    
			}
              
            }
        }

    }
}
