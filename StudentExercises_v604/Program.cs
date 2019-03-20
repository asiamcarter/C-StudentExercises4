using StudentExercises_v604.Data;
using StudentExercises_v604.Model;
using System;
using System.Collections.Generic;

namespace StudentExercises_v604
{
    class Program
    {
        static void Main(string[] args)
        {
            Repository repository = new Repository();
            List<Exercise> exercises = repository.GetAllExercises();

            Console.WriteLine("All Exercises");

            foreach (Exercise exercise in exercises)
            {
                Console.WriteLine(exercise.Name, exercise.Language);
            }
        
            Pause();

            List<Exercise> JavaScriptExercises = repository.GetJavaScriptExercises();

            Console.WriteLine("All JavaScript Exercises");

            foreach (Exercise exercise in JavaScriptExercises)
            {
                Console.WriteLine(exercise.Name, exercise.Language);
            }

            Pause();

            Exercise SQLExercise = new Exercise
            { 
                Name = "Linq Exercise",
                Language = "SQL"
            };

            repository.AddExercise(SQLExercise);

            Console.WriteLine("Exercises, After Adding New Exercise");

            repository.GetAllExercises();
            foreach (Exercise exercise in exercises)
            {
                Console.WriteLine(exercise.Name, exercise.Language);
            }

            Pause();

            List<Instructor> InstructorsWithCohort = repository.GetInstructorsWithCohort();

            Console.WriteLine("All Instructors With Cohort");

            foreach (Instructor instructor in InstructorsWithCohort)
            {
                Console.WriteLine($"{instructor.FirstName} {instructor.LastName} Cohort: {instructor.cohort.Name} ");
            }

            Pause();

            Instructor Madi = new Instructor
            {
                FirstName = "Madi",
                LastName = "Peper",
                SlackHandle = "@peps",
                CohortId = 3
            };

            repository.AddInstructor(Madi);
            repository.GetInstructorsWithCohort();
            foreach (Instructor instructor in InstructorsWithCohort)
            {
                Console.WriteLine($"{instructor.FirstName} {instructor.LastName} Cohort: {instructor.cohort.Name} ");
            }

            Pause();
            repository.AddStudentExercise(2, 2);
            List<StudentExercise> AllStudentExercises = repository.GetStudentExercises();
            Console.WriteLine("All Student Exercises");
            foreach (StudentExercise se in AllStudentExercises)
            {
                Console.WriteLine($"{se.Student.FirstName} {se.Student.LastName} is working on {se.Exercise.Name}");
            }
            Pause();
        }
        public static void Pause()
        {
            Console.WriteLine();
            Console.Write("Press any key...");
            Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
