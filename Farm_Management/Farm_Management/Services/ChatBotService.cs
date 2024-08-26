using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using Farm_Management.Models;

namespace Farm_Management.Services
{

    public class ChatBotService
    {
        FARMEntities db = new FARMEntities();
        #region ##########Chat Area ##########
        public List<ProductDTO> GetProducts(string search)
        {
            return (from p in db.FARM_CHAT_QUESTIONS
                    where p.CHAT_Q_STATUS == 0 && p.CHAT_Q_DESCRIPTION.Contains(search)
                    select new ProductDTO { Name = p.CHAT_Q_DESCRIPTION }).ToList();
        }

        internal bool SaveNewQuestion(string question, string username)
        {
            bool result = false;
            try
            {
                FARM_CHAT_QUESTIONS newQuestion = new FARM_CHAT_QUESTIONS();
                newQuestion.CHAT_Q_ANSWER_ID = 0;
                newQuestion.CHAT_Q_DESCRIPTION = question;
                newQuestion.CHAT_Q_STATUS = 2;
                newQuestion.CHAT_Q_CREATE_BY = username;
                newQuestion.CHAT_Q_CREATE_DATE = System.DateTime.Now;

                db.FARM_CHAT_QUESTIONS.Add(newQuestion);
                db.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        #endregion

        #region ########## Chat Index ##########

        internal bool EditQuestion(int QuestionId, string Question, string Username)
        {
            bool Result = false;
            try
            {
                FARM_CHAT_QUESTIONS question = db.FARM_CHAT_QUESTIONS.Where(x => x.CHAT_Q_ID == QuestionId).FirstOrDefault();
                question.CHAT_Q_DESCRIPTION = Question;
                question.CHAT_Q_MODIFIED_BY = Username;
                question.CHAT_Q_MODIFIED_DATE = System.DateTime.Now;
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                Result = true;
            }
            catch (Exception ex)
            {
                Result = false;
            }
            return Result;
        }

        internal bool ActiveQuestion(int questionId, string Username)
        {
            bool Result = false;
            try
            {
                FARM_CHAT_QUESTIONS question = db.FARM_CHAT_QUESTIONS.Where(x => x.CHAT_Q_ID == questionId).FirstOrDefault();
                question.CHAT_Q_STATUS = 0;
                question.CHAT_Q_MODIFIED_BY = Username;
                question.CHAT_Q_MODIFIED_DATE = System.DateTime.Now;
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                Result = true;
            }
            catch (Exception ex)
            {
                Result = false;
            }
            return Result;
        }

        internal bool InactiveQuestion(int questionId, string Username)
        {
            bool Result = false;
            try
            {
                FARM_CHAT_QUESTIONS question = db.FARM_CHAT_QUESTIONS.Where(x => x.CHAT_Q_ID == questionId).FirstOrDefault();
                question.CHAT_Q_STATUS = 1;
                question.CHAT_Q_MODIFIED_BY = Username;
                question.CHAT_Q_MODIFIED_DATE = System.DateTime.Now;
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                Result = true;
            }
            catch (Exception ex)
            {
                Result = false;
            }
            return Result;
        }

        internal bool ActiveAnswer(int AnswerID, string Username)
        {
            bool Result = false;
            try
            {
                FARM_CHAT_ANSWERS question = db.FARM_CHAT_ANSWERS.Where(x => x.CHAT_A_ID == AnswerID).FirstOrDefault();
                question.CHAT_A_STATUS = 0;
                question.CHAT_A_MODIFIED_BY = Username;
                question.CHAT_A_MODIFIED_DATE = System.DateTime.Now;
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                Result = true;
            }
            catch (Exception ex)
            {
                Result = false;
            }
            return Result;
        }

        internal bool InactiveAnswer(int AnswerID, string Username)
        {
            bool Result = false;
            try
            {
                FARM_CHAT_ANSWERS question = db.FARM_CHAT_ANSWERS.Where(x => x.CHAT_A_ID == AnswerID).FirstOrDefault();
                question.CHAT_A_STATUS = 1;
                question.CHAT_A_MODIFIED_BY = Username;
                question.CHAT_A_MODIFIED_DATE = System.DateTime.Now;
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                Result = true;
            }
            catch (Exception ex)
            {
                Result = false;
            }
            return Result;
        }

        internal string GetQuestionByID(int questionId)
        {
            FARM_CHAT_QUESTIONS Question = db.FARM_CHAT_QUESTIONS.Where(x => x.CHAT_Q_ID == questionId).FirstOrDefault();
            return Question.CHAT_Q_DESCRIPTION;
        }

        internal string GetAnswerByQuestionId(int questionId)
        {
            string Result = "";
            FARM_CHAT_QUESTIONS QuestionRecord = db.FARM_CHAT_QUESTIONS.Where(x => x.CHAT_Q_ID == questionId).FirstOrDefault();
            FARM_CHAT_ANSWERS AnswerRecord = db.FARM_CHAT_ANSWERS.Where(x => x.CHAT_A_ID == QuestionRecord.CHAT_Q_ANSWER_ID).FirstOrDefault();
            if (AnswerRecord != null)
            {
                Result = AnswerRecord.CHAT_A_DESCRIPTION;
            }
            return Result;
        }

        internal int SaveNewAnswer(string answer, string username)
        {
            int result = 0;
            try
            {
                FARM_CHAT_ANSWERS newAnswer = new FARM_CHAT_ANSWERS();
                newAnswer.CHAT_A_DESCRIPTION = answer;
                newAnswer.CHAT_A_STATUS = 0;
                newAnswer.CHAT_A_CREATE_BY = username;
                newAnswer.CHAT_A_CREATE_DATE = System.DateTime.Now;

                db.FARM_CHAT_ANSWERS.Add(newAnswer);
                db.SaveChanges();
                result = newAnswer.CHAT_A_ID;
            }
            catch (Exception ex)
            {
                result = 0;
            }
            return result;
        }

        internal void SendEmailForUSer(string username, int QuestionId, int answerId)
        {
            FARM_CHAT_QUESTIONS Question = db.FARM_CHAT_QUESTIONS.Where(x => x.CHAT_Q_ID == QuestionId).FirstOrDefault();
            FARM_CHAT_ANSWERS Answer = db.FARM_CHAT_ANSWERS.Where(x => x.CHAT_A_ID == answerId).FirstOrDefault();
            string QuestionDescription = Question.CHAT_Q_DESCRIPTION;

            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            QuestionDescription = textInfo.ToTitleCase(QuestionDescription);

            try
            {
                Member loagingTable = db.Members.Where(x => x.Name == Question.CHAT_Q_CREATE_BY).FirstOrDefault();
                Login log = db.Logins.Where(x => x.User_Name == Question.CHAT_Q_DESCRIPTION).FirstOrDefault();
                if (loagingTable != null)
                {
                    if (log.Role == "User")
                    {
                        Member userdetails = db.Members.Where(x => x.Name == Question.CHAT_Q_CREATE_BY).FirstOrDefault();

                        var companyemailDetails = db.tblCompanies.FirstOrDefault();
                        string from = companyemailDetails.Email;
                        string fromname = companyemailDetails.CompanyName;
                        string from_password = companyemailDetails.Email_Password;

                        string to = userdetails.Email;
                        string Toname = userdetails.Name;

                        string subject = "About you Asked Question from our Evento Chatbot";
                        string body = "You are asking from us <br /> " + QuestionDescription + " <br /> Answer Is <br /> " + Answer.CHAT_A_DESCRIPTION;

                        var fromAddress = new MailAddress(from, fromname);
                        var toAddress = new MailAddress(to, Toname);
                        var smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(fromAddress.Address, from_password)
                        };

                        using (var message = new MailMessage(fromAddress, toAddress)
                        {
                            Subject = subject,
                            Body = body
                        })

                        {
                            smtp.Send(message);
                        }



                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        internal bool UpdateAnswerIdInQuestion(int questionId, int answerId, string Username)
        {
            bool Result = false;
            try
            {
                FARM_CHAT_QUESTIONS question = db.FARM_CHAT_QUESTIONS.Where(x => x.CHAT_Q_ID == questionId).FirstOrDefault();
                question.CHAT_Q_STATUS = 0;
                question.CHAT_Q_ANSWER_ID = answerId;
                question.CHAT_Q_MODIFIED_BY = Username;
                question.CHAT_Q_MODIFIED_DATE = System.DateTime.Now;
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                Result = true;
            }
            catch (Exception ex)
            {
                Result = false;
            }
            return Result;
        }

        internal bool EditAnswer(int answerId, string answer, string username)
        {
            bool Result = false;
            try
            {
                FARM_CHAT_ANSWERS Answer = db.FARM_CHAT_ANSWERS.Where(x => x.CHAT_A_ID == answerId).FirstOrDefault();
                Answer.CHAT_A_DESCRIPTION = answer;
                Answer.CHAT_A_MODIFIED_BY = username;
                Answer.CHAT_A_MODIFIED_DATE = System.DateTime.Now;
                db.Entry(Answer).State = EntityState.Modified;
                db.SaveChanges();
                Result = true;
            }
            catch (Exception ex)
            {
                Result = false;
            }
            return Result;
        }

        internal string GetAnswerById(int answerId)
        {
            FARM_CHAT_ANSWERS Answer = db.FARM_CHAT_ANSWERS.Where(x => x.CHAT_A_ID == answerId).FirstOrDefault();
            return Answer.CHAT_A_DESCRIPTION;
        }

        #endregion
    }
}