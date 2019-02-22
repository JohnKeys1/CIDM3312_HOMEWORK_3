using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
namespace Homework3
{
    

    public class UserApp : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = database.db");
        }

        public DbSet<User> Users {get; set;}
        public DbSet<Question> Questions {get; set;}
        public DbSet<Answer> Answers {get; set;}
    }

    public class User
    {
        public int UserId {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string Email {get; set;}
        public DateTime RegistrationDate {get; set;}
        public List<Question> Questions{get; set;} 
        public List<Answer> Answers{get; set;} 

        public override string ToString()
        {
            return $"User {UserId} - {FirstName}";
        }
    }

   
    public class Question
    {
        public int QuestionId {get; set;}
        public string questionText {get; set;}
        public DateTime questionDate {get; set;}
        public int UserId {get; set;} // Foreign key
        public string QuestionAnswered {get; set;}
        public User user {get; set;}
        
        public List<Answer> Answers{get; set;} 
        
        public override string ToString()
        {
        return $"Question -{QuestionId} {questionText}-{questionDate}";
        }
    }

     public class Answer
    {
        public int AnswerId {get; set;}
        public string answerText {get; set;}
        public DateTime answerDate {get; set;}
        public int UserId {get; set;}
        public User user {get; set;}
        public int QuestionId {get; set;}
        public Question question {get; set;} // Navigation property.
        public override string ToString()
        {
            return $"Answer {QuestionId}-{ AnswerId} - {answerText} -{answerDate}";
        }
    }

    
}



