using StudentExercises_v604.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace StudentExercises_v604.Data
{
    class Repository
    {
        public SqlConnection Connection

        {
            get
            {
                string _connectionString = "Server=DESKTOP-7FFQBEO\\SQLEXPRESS; Database=StudentExerciseDB; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
                return new SqlConnection(_connectionString);
            }
        }
        //Query the database for all the Exercises.
        public List<Exercise> GetAllExercises()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                cmd.CommandText = @"SELECT Id, Name,  Language FROM Exercise";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Exercise> exercises = new List<Exercise>();
                    while (reader.Read())
                    {
                        int idColPosition = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idColPosition);
                        int exerciseName = reader.GetOrdinal("Name");
                        string exerciseNameValue = reader.GetString(exerciseName);
                        int LangName = reader.GetOrdinal("Language");
                        string exerciseLanguage = reader.GetString(LangName);

                        Exercise exercise = new Exercise
                        {
                            Id = idValue,
                            Name = exerciseNameValue,
                            Language = exerciseLanguage
                        };
                        exercises.Add(exercise);
                    }
                    reader.Close();
                    return exercises;
                }
            }
        }
        //Find all the exercises in the database where the language is JavaScript.
        public List<Exercise> GetJavaScriptExercises()
        {
            using (SqlConnection conn = Connection)
            {

                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())

                {

                    cmd.CommandText = "SELECT Id,Name, Language FROM Exercise WHERE Language = 'JavaScript'";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Exercise> JSExercises = new List<Exercise>();

                    while (reader.Read())

                    {

                        int IdPosition = reader.GetOrdinal("Id");

                        int IdValue = reader.GetInt32(IdPosition);

                        int ExerciseNamePosition = reader.GetOrdinal("Name");

                        string ExerciseValue = reader.GetString(ExerciseNamePosition);

                        int LanguagePosition = reader.GetOrdinal("Language");

                        string LanguageValue = reader.GetString(LanguagePosition);

                        Exercise JSExercise = new Exercise

                        {
                            Id = IdValue,
                            Name = ExerciseValue,
                            Language = LanguageValue
                        };

                        JSExercises.Add(JSExercise);

                    }

                    reader.Close();
                    return JSExercises;

                }

            }

        }



        //Insert a new exercise into the database.

        public void AddExercise(Exercise exercise)

        {

            using (SqlConnection conn = Connection)

            {

                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())

                {

                    cmd.CommandText = $@"INSERT INTO Exercise(Name,Language) Values(@Name, @Language)";

                    cmd.Parameters.Add(new SqlParameter("@Name", exercise.Name));

                    cmd.Parameters.Add(new SqlParameter("@Language", exercise.Language));

                    cmd.ExecuteNonQuery();

                }

            }

        }

        //Find all instructors in the database.Include each instructor's cohort.



        public List<Instructor> GetInstructorsWithCohort()

        {

            using (SqlConnection conn = Connection)

            {

                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())

                {

                    cmd.CommandText = @"SELECT Instructor.Id, Instructor.FirstName, Instructor.LastName, Cohort.Id as CohortId, Cohort.Name as CohortName FROM Instructor

                    LEFT JOIN Cohort ON Instructor.CohortId = Cohort.Id";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Instructor> Instructors = new List<Instructor>();

                    while (reader.Read())
                    {

                        int InstructorIdPosition = reader.GetOrdinal("Id");

                        int InstructorIdValue = reader.GetInt32(InstructorIdPosition);

                        int InstructorFirstNamePosition = reader.GetOrdinal("FirstName");

                        string InstructorFirstNameValue = reader.GetString(InstructorFirstNamePosition);

                        int InstructorLastNamePosition = reader.GetOrdinal("LastName");

                        string InstructorLastNameValue = reader.GetString(InstructorLastNamePosition);

                        int CohortIdPosition = reader.GetOrdinal("CohortId");

                        int CohortIdValue = reader.GetInt32(CohortIdPosition);

                        int CohortNamePosition = reader.GetOrdinal("CohortName");

                        string CohortNameValue = reader.GetString(CohortNamePosition);

                        Cohort cohort = new Cohort

                        {

                            Id = CohortIdValue,
                            Name = CohortNameValue

                        };

                        Instructor instructor = new Instructor

                        {
                            Id = InstructorIdValue,
                            FirstName = InstructorFirstNameValue,
                            LastName = InstructorLastNameValue,
                            CohortId = CohortIdValue,
                            cohort = cohort
                        };

                        Instructors.Add(instructor);

                    }

                    reader.Close();

                    return Instructors;

                }

            }

        }

        //Insert a new instructor into the database.Assign the instructor to an existing cohort.

        public void AddInstructor(Instructor instructor)

        {

            using (SqlConnection conn = Connection)

            {

                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())

                {

                    cmd.CommandText = $@"INSERT INTO Instructor(FirstName,LastName,SlackHandle,CohortId) Values(@FirstName, @LastName, @SlackHandle, @CohortId)";

                    cmd.Parameters.Add(new SqlParameter("@FirstName", instructor.FirstName));

                    cmd.Parameters.Add(new SqlParameter("@LastName", instructor.LastName));

                    cmd.Parameters.Add(new SqlParameter("@SlackHandle", instructor.SlackHandle));

                    cmd.Parameters.Add(new SqlParameter("@CohortId", instructor.CohortId));

                    cmd.ExecuteNonQuery();

                }

            }

        }
    }
}
