﻿using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
