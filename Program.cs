using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace Homework3
{
    class Program
    {       
        static string UserEmail;
        static void addUser(){ 
                                    using (var db = new UserApp()){
                                    Console.WriteLine("enter first name");
                                    string firstName=Console.ReadLine();
                                    Console.WriteLine("enter last name");
                                    string lastName=Console.ReadLine();
                                    Console.WriteLine("Enter email ");
                                    string email=Console.ReadLine();
                                    DateTime registrationDate=DateTime.Now;                    
                                    User person = new User
                                    {FirstName = firstName, LastName = lastName,Email = email,RegistrationDate= registrationDate};  

                                    db.Add(person);      // Add our entity to the database
                                    db.SaveChanges();               // Save changes to the disk  
                                    
                                }
                    }
            static void listQuestions(){
                                    using (var db = new UserApp())
                                {

                                    foreach (var p in db.Questions)
                                    {
                                       
                                        Console.WriteLine(p);
                                       
                                    }
                               }
                                
                        }
                        
            static void listQuestionsWithoutAnswers(){
                                using (var db = new UserApp())
                                    {
                                        var unanswered =db.Questions.Where(p=>p.QuestionAnswered=="False");
                                        foreach (var p in unanswered )
                                        {
                                            Console.WriteLine($"{p.QuestionId} {p.questionText} {p.questionDate} ");
                                        }
                                }
                        }
            
            static void askQuestion(){
                            
                            using (var db = new UserApp())
                                { 
                                    Console.WriteLine("Enter your question");
                                    string newQuestion=Console.ReadLine();

                                    Question question = new Question{questionText = newQuestion,questionDate=DateTime.Now,QuestionAnswered="False"};
                                    question.user = db.Users.Include(b => b.Questions).Where(b => b.Email==UserEmail).First();  
                                    db.Add(question);      
                                    db.SaveChanges();                                             
                                } 
            }

            static void removeQuestion(){
                                    Console.WriteLine("Please enter the id number of the question you would like to remove.note,you can only remove questions you have entered");
                                    int questionIdToRemove=Convert.ToInt32(Console.ReadLine());
                                 
                                     using (var db = new UserApp())
                                    {

                                      if(!db.Questions.Any(o => o.QuestionId ==questionIdToRemove)){

                                        Console.WriteLine("There is no question with that id number,please try another one");
                                      } else{
                                         Question question = db.Questions.Where(m => m.QuestionId == questionIdToRemove).First();                          
                                         db.Remove(question);
                                         db.SaveChanges();
                                      }
                                    }

            }
            static void AnswerQuestion(){
                                    Console.WriteLine("Please enter the id number of the question you would like to answer.");
                                    int questionIdToAnswer=Convert.ToInt32(Console.ReadLine());
                                    using (var db = new UserApp())
                                    {
                                     if(!db.Questions.Any(o => o.QuestionId ==questionIdToAnswer)){
                                         Console.WriteLine("There is no question with that id number,please try another one");
                                     }else{
                                         Console.WriteLine("Please enter the answer");
                                         string newAnswer=Console.ReadLine();
                                         Question questionIdToUpdate=db.Questions.Where(q=> q.QuestionId ==questionIdToAnswer).First();
                                         User userToUpdate=db.Users.Where(u =>u.Email ==UserEmail).First();
                                         questionIdToUpdate.QuestionAnswered="True";
                                         Answer answer = new Answer{answerText = newAnswer,answerDate=DateTime.Now};
                                         db.Add(answer);      
                                         db.SaveChanges(); 

                                     }
                                    }        

            }


        static void Main(string[] args)
        {           
            
            /* using (var db = new UserApp())
                            {
                                // Useful tactic ONLY in development.
                                // At start of your program, always delete the database and then re-create it
                                // This ensures a fresh database everytime you run your program.
                                db.Database.EnsureDeleted();
                                db.Database.EnsureCreated();
                            }*/

                        
            using (var db = new UserApp())                                
                                {
                                    db.Database.EnsureCreated();// Create our database to begin with.
                                    Console.WriteLine("Enter email ");
                                    UserEmail=Console.ReadLine();
                                if (!db.Users.Any(o => o.Email == UserEmail)) {
                                    addUser();
                                }
                                }
                                

           /* using (var db = new UserApp())          
            {
                                User user1= new User 
                                {
                                    FirstName = "John ",
                                    LastName="k",
                                    Email="jkat1@gmail.com",
                                    Questions = new List<Question>
                                    {
                                        new Question { questionText = "What is Cis?",questionDate=DateTime.Now },                                           
                                    }                                        
                                };
                                    db.Add(user1);                                       
                                    db.SaveChanges();
        }*/ 
            
           int choice;
           bool validChoice;
           bool keepEnteringData;


           do
           {
               keepEnteringData = true;
               Console.WriteLine("Welcome.");
               Console.WriteLine("Please select what you need to do below:");
               Console.WriteLine("1) Add person");
               Console.WriteLine("2) List all questions in database");
               Console.WriteLine("3) List all question without answers in database");
               Console.WriteLine("4) Ask a question");
               Console.WriteLine("5) Remove a question from database");
               Console.WriteLine("6) Answer a question");
               Console.WriteLine("7) Quit");

               do 
               {
                   validChoice = true;
                   Console.Write("Please make a selection: ");
                   try
                   {
                       choice = Convert.ToInt32(Console.ReadLine());
                       switch (choice)
                       {
                           case 1: // add new person
                               addUser();
                               break;
                           case 2: // list all in database
                               listQuestions();
                               break;
                            case 3: // remove a person from the  database
                              listQuestionsWithoutAnswers();
                               break;    
                           case 4: // remove a person from the  database
                              askQuestion();;
                               break;
                           case 5: // add a person in a database
                               removeQuestion();
                               break;
                            case 6: // add a person in a database
                               AnswerQuestion();
                               break;
                           case 7: // User selected Quit
                               Console.WriteLine("Quitting.");
                               keepEnteringData = false;
                               break;
                           default:
                               validChoice = false;
                               Console.WriteLine("Invalid choice. Please try again.");
                               break;
                       }
                   }
                   catch (FormatException)
                   {
                       // This try...catch block catches the FormatException that Convert.ToInt32 will throw
                       // if the user inputs text or something that cannot be converted to an integer.
                       validChoice = false;
                       Console.WriteLine("Invalid choice. Please try again.");
                   }
               } while (validChoice == false); // Inner loop ends when validChoice is true
           } while (keepEnteringData); // Outer loop ends when the user selects quit. 
        
        
        }
    }
}
