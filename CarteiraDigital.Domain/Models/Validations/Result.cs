using CarteiraDigital.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarteiraDigital.Domain.Models
{
    public class Result<T> : IResult<T> where T : class
    {
        public bool Success { get; private set; }
        public List<string> Messages { get; private set; }
        public T Model { get; private set; }

        public Result()
        {
            Messages = new List<string>();
        }

        public Result<T> AddError(string message)
        {
            Success = false;
            Messages.Add(message);
            return this;
        }

        public Result<T> AddSuccess(T model, string message = "")
        {
            Model = model;
            Success = true;
            Messages.Add(message);
            return this;
        }

        public Result<T> AddMessage(string message)
        {
            Messages.Add(message);
            return this;
        }

        public static Result<T> BuildError(string message)
           => new Result<T>
           {
               Success = false,
               Messages = new List<string> { message }
           };

        public static Result<T> BuildError(List<string> messages)
          => new Result<T>
          {
              Success = false,
              Messages = messages
          };

        public static Result<T> BuildError(string message, Exception error)
            => new Result<T>
            {
                Success = false,
                Messages = new List<string>
                {
                   $"Message: {message}",
                   $"BaseException : {error.GetBaseException().Message}",
                   $"StackTrace: {error.GetBaseException().StackTrace} "
                }
            };

        public static Result<T> BuildSucess(T model, string message = "")
            => new Result<T>
            {
                Model = model,
                Success = true,
                Messages = new List<string> { message }
            };
    }
}
