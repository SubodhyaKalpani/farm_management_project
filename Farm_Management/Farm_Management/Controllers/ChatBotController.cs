using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Farm_Management.Models;
using Farm_Management.Services;

namespace Farm_Management.Controllers
{
    public class ChatBotController : Controller
    {
        FARMEntities db = new FARMEntities();
        public ChatBotService chatbotservice = new ChatBotService();
        // GET: ChatBot

        #region##########Chat Area##########
        
        [HttpPost]
        public JsonResult GetQuestions(string search)
        {
            List<ProductDTO> allsearch = new List<ProductDTO>();
            List<FARM_CHAT_QUESTIONS> IsRec = db.FARM_CHAT_QUESTIONS.Where(x => x.CHAT_Q_STATUS == 0 && x.CHAT_Q_DESCRIPTION.Contains(search)).ToList();
            if (IsRec.Count > 0)
            {
                allsearch = chatbotservice.GetProducts(search);
            }

            return new JsonResult { Data = allsearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult ChatAnswerByQuestion(string Question)
        {
            bool Result = false;
            string Answer = "";
            string username = System.Web.HttpContext.Current.Session["User_Name"].ToString();

            List<FARM_CHAT_QUESTIONS> Questions = db.FARM_CHAT_QUESTIONS.Where(x => x.CHAT_Q_STATUS == 0 && x.CHAT_Q_DESCRIPTION.Contains(Question)).ToList();
            if (Questions.Count > 0)
            {
                int QuestionID = Questions.FirstOrDefault().CHAT_Q_ANSWER_ID;
                FARM_CHAT_ANSWERS AnswerRec = db.FARM_CHAT_ANSWERS.Where(x => x.CHAT_A_ID == QuestionID && x.CHAT_A_STATUS == 0).FirstOrDefault();
                if (AnswerRec != null)
                {
                    Answer = AnswerRec.CHAT_A_DESCRIPTION;
                }
                else
                {
                    Answer = "No";
                    Result = chatbotservice.SaveNewQuestion(Question, username);
                }
            }
            else
            {
                Answer = "No";
                Result = chatbotservice.SaveNewQuestion(Question, username);
            }

            return Json(Answer, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region##########Chat Index ##########
        public ActionResult Index()
        {
            ChatBotVM ViewModel = new ChatBotVM();
            ViewModel.QuestionList = db.FARM_CHAT_QUESTIONS.ToList();
            ViewModel.AnswerList = db.FARM_CHAT_ANSWERS.ToList();
            return View(ViewModel);
        }

        public JsonResult SaveQuestion(string Question)
        {
            bool Result = false;
            string username = System.Web.HttpContext.Current.Session["User_Name"].ToString();
            Result = chatbotservice.SaveNewQuestion(Question, username);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditQuestion(string Question, int QuestionId)
        {
            bool Result = false;
            string username = System.Web.HttpContext.Current.Session["User_Name"].ToString();
            Result = chatbotservice.EditQuestion(QuestionId, Question, username);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeStatus(int Id, string Ststus, string Type)
        {
            bool Result = false;
            string username = System.Web.HttpContext.Current.Session["User_Name"].ToString();
            if (Type == "Question")
            {
                if (Ststus == "Active")
                {
                    Result = chatbotservice.ActiveQuestion(Id, username);
                }
                else
                {
                    Result = chatbotservice.InactiveQuestion(Id, username);
                }
            }
            else
            {
                if (Ststus == "Active")
                {
                    Result = chatbotservice.ActiveAnswer(Id, username);
                }
                else
                {
                    Result = chatbotservice.InactiveAnswer(Id, username);
                }
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetQuestion(int QuestionId)
        {
            string Result = "";
            Result = chatbotservice.GetQuestionByID(QuestionId);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AnswerIndex(int QuestionId)
        {
            ChatBotVM ViewModel = new ChatBotVM();
            ViewModel.QuestionRecord = db.FARM_CHAT_QUESTIONS.Where(x => x.CHAT_Q_ID == QuestionId).FirstOrDefault();
            ViewModel.QuestionList = db.FARM_CHAT_QUESTIONS.ToList();
            ViewModel.AnswerList = db.FARM_CHAT_ANSWERS.ToList();
            string Answer = chatbotservice.GetAnswerByQuestionId(QuestionId);
            ViewBag.AnswerDescription = Answer;
            return View(ViewModel);
        }

        public JsonResult SaveAnswer(int QuestionId, string Answer)
        {
            bool Result = false;
            int AnswerId = 0;
            string username = System.Web.HttpContext.Current.Session["User_Name"].ToString();
            AnswerId = chatbotservice.SaveNewAnswer(Answer, username);
            if (AnswerId > 0)
            {
                chatbotservice.SendEmailForUSer(username, QuestionId, AnswerId);
                Result = chatbotservice.UpdateAnswerIdInQuestion(QuestionId, AnswerId, username);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateAnswer(int AnswerId, string Answer)
        {
            bool Result = false;
            string username = System.Web.HttpContext.Current.Session["User_Name"].ToString();
            Result = chatbotservice.EditAnswer(AnswerId, Answer, username);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SetAnswer(int QuestionId, int AnswerId)
        {
            bool Result = false;
            string username = System.Web.HttpContext.Current.Session["User_Name"].ToString();
            if (AnswerId > 0)
            {
                chatbotservice.SendEmailForUSer(username, QuestionId, AnswerId);
                Result = chatbotservice.UpdateAnswerIdInQuestion(QuestionId, AnswerId, username);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAnswer(int AnswerId)
        {
            string Result = "";
            Result = chatbotservice.GetAnswerById(AnswerId);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}