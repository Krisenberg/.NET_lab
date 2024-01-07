using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using static System.Formats.Asn1.AsnWriter;

namespace List_08.Controllers
{
    public class GameController : Controller
    {
        //private static int? _scope = null;
        //private static int? _randValue = null;
        //private static int _userGuessCounter;
        //private static Random _random = new Random();
        private int? _scope;
        private int? _randValue;
        private int? _userGuessCounter;
        private Random _random = new Random();

        public IActionResult Set(int scope)
        {
            //_scope = scope;
            HttpContext.Session.SetInt32("scope", scope);
            string scopeString = $"[0, {scope}]";
            ViewData["Scope"] = scopeString;
            HttpContext.Session.Remove("randValue");

            return View();
        }
        public IActionResult Draw()
        {
            string viewMessage = "You must set the scope before drawing the target number.";
            _scope = HttpContext.Session.GetInt32("scope");
            _randValue = (_scope is not null) ? _random.Next(0,_scope.Value) : null;
            if (_randValue.HasValue) 
            { 
                HttpContext.Session.SetInt32("randValue", _randValue.Value);
                HttpContext.Session.SetInt32("userGuessCounter", 0);
                viewMessage = "The target number has been drawn. You can start guessing!";
            }
            ViewData["Message"] = viewMessage;
            //int? userGuessCounter = HttpContext.Session.GetInt32("userGuessCounter");
            //if (userGuessCounter == null) { userGuessCounter = 0; }
            //HttpContext.Session.SetInt32("userGuessCounter", userGuessCounter.Value);

            return View();
        }
        public IActionResult Guess(int userGuess)
        {
            //ViewData["GuessCounter"] = (++_userGuessCounter);
            string feedback;
            string feedbackCSSclass;
            string scope;
            _randValue = HttpContext.Session.GetInt32("randValue");

            if (_randValue is null)
            {
                ViewData["GuessCounter"] = "";
                feedback = "You must set a scope and draw a random target number first!";
                feedbackCSSclass = "not_set_parameters";
                scope = "[0, ?]";
            }
            else
            {
                _userGuessCounter = HttpContext.Session.GetInt32("userGuessCounter");
                HttpContext.Session.SetInt32("userGuessCounter", _userGuessCounter.Value + 1);
                ViewData["GuessCounter"] = (++_userGuessCounter);

                feedback = (userGuess > _randValue.Value) ? "Your guess is too high!" :
                ((userGuess < _randValue.Value) ? "Your guess is too low!" : "Your guess is correct!");

                feedbackCSSclass = (userGuess > _randValue.Value) ? "too_high" :
                ((userGuess < _randValue.Value) ? "too_low" : "correct");

                _scope = HttpContext.Session.GetInt32("scope");
                scope = $"[0, {_scope.Value}]";
            }
            ViewData["Feedback"] = feedback;
            ViewData["FeedbackCSSclass"] = feedbackCSSclass;
            ViewData["Scope"] = scope;
            ViewData["Guess"] = $"{userGuess}";


            if (_randValue.HasValue && userGuess == _randValue.Value)
            {
                HttpContext.Session.Remove("randValue");
                //_randValue = null;
            }
            return View();
        }
    }
}
