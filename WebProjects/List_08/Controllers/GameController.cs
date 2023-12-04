using Microsoft.AspNetCore.Mvc;
using System;

namespace List_08.Controllers
{
    public class GameController : Controller
    {
        private static int? _scope = null;
        private static int? _randValue = null;
        private static int _userGuessCounter;
        private static Random _random = new Random();

        public IActionResult Set(int scope)
        {
            _scope = scope;
            string scopeString = $"[0, {_scope.Value}]";
            ViewData["Scope"] = scopeString;

            return View();
        }
        public IActionResult Draw()
        {
            _randValue = (_scope is not null) ? _random.Next(0,_scope.Value) : null;
            _userGuessCounter = 0;

            return View();
        }
        public IActionResult Guess(int userGuess)
        {
            //ViewData["GuessCounter"] = (++_userGuessCounter);
            string feedback;
            string feedbackCSSclass;
            string scope;

            if (_randValue is null)
            {
                ViewData["GuessCounter"] = "";
                feedback = "You must set a scope and draw a random target number first!";
                feedbackCSSclass = "not_set_parameters";
                scope = "[0, ?]";
            }
            else
            {
                ViewData["GuessCounter"] = (++_userGuessCounter);

                feedback = (userGuess > _randValue) ? "Your guess is too high!" :
                ((userGuess < _randValue) ? "Your guess is too low!" : "Your guess is correct!");

                feedbackCSSclass = (userGuess > _randValue) ? "too_high" :
                ((userGuess < _randValue) ? "too_low" : "correct");

                scope = $"[0, {_scope.Value}]";
            }
            ViewData["Feedback"] = feedback;
            ViewData["FeedbackCSSclass"] = feedbackCSSclass;
            ViewData["Scope"] = scope;
            ViewData["Guess"] = $"{userGuess}";


            if (userGuess == _randValue)
            {
                _randValue = null;
            }
            return View();
        }
    }
}
