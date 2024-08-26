using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farm_Management.Models
{
    public class ChatBotVM
    {
        public FARM_CHAT_QUESTIONS QuestionRecord { get; set; }
        public List<FARM_CHAT_ANSWERS> AnswerList { get; set; }
        public List<FARM_CHAT_QUESTIONS> QuestionList { get; set; }
    }
    public class ProductDTO
    {
        public string Name { get; set; }
        // Other field you may need from the Product entity
    }
}