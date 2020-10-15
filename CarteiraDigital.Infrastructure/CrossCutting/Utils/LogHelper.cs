using NLog;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace CarteiraDigital.Infrastructure.CrossCutting.Utils
{
    public static class LogHelper
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static void Debug(string mensagem, [CallerMemberName] string metodo = null, [CallerFilePath] string diretorio = null)
        => logger.Debug(GetMessageFormat(mensagem, metodo, diretorio));

        public static void Info(string mensagem, [CallerMemberName] string metodo = null, [CallerFilePath] string diretorio = null)
        => logger.Info(GetMessageFormat(mensagem, metodo, diretorio));

        public static void Error(string mensagem, Exception excecao, [CallerMemberName] string metodo = null, [CallerFilePath] string diretorio = null)
        => logger.Error(excecao, GetMessageFormat(mensagem, metodo, diretorio));

        public static void Error(string mensagem, [CallerMemberName] string metodo = null, [CallerFilePath] string diretorio = null)
        => logger.Error(GetMessageFormat(mensagem, metodo, diretorio));

        private static string GetMessageFormat(string mensagem, string metodo, string diretorio)
        {
            var classe = diretorio.Substring(diretorio.LastIndexOf('\\') + 1, diretorio.Length - diretorio.LastIndexOf('\\') - 4);
            return $"{classe} - {metodo}: {mensagem}";
        }
    }
}